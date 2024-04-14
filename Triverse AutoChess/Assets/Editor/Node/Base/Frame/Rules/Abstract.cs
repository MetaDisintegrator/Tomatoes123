using Microsoft.SqlServer.Server;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor
{
    public abstract class NodeEditorElement : IController
    {
        public IArchitecture GetArchitecture()
        {
            return NodeEditorArchitecture.Interface;
        }
    }

    public abstract class NodeBoxElement : NodeEditorElement, INodeBoxBuilder
    {
        #region Rect
        protected NodeBoxElement Parent { get; set; }
        private Vector2 FatherPos => Parent == null ? Vector2.zero : Parent.Rect.position;
        private Rect local;
        private Rect global;
        public Rect Rect
        {
            get => global;
            set
            {
                global = value;
                ApplyGlobal2Local();
            }
        }
        public Vector2 Center
        {
            get => global.center;
            set
            {
                global.center = value;
                ApplyGlobal2Local();
            }
        }
        public Vector2 Min
        {
            get => global.min;
            set
            {
                global.min = value;
                ApplyGlobal2Local();
            }
        }
        public Vector2 Max
        {
            get => global.max;
            set
            {
                global.max = value;
                ApplyGlobal2Local();
            }
        }
        public float Height
        {
            get => global.height;
            set
            {
                global.height = value;
                ApplyGlobal2Local();
            }
        }
        #endregion

        private string title;
        private GUIStyle highlight;
        private GUIStyle normal;
        public bool isDragged;
        public bool isSelected;

        private void ApplyGlobal2Local() => local.position = global.position - FatherPos;
        private void ApplyLocal2Global() => global.position = local.position + FatherPos;

        #region 渲染
        public void Render()
        {
            ApplyLocal2Global();
            //Box
            RenderBox(title, normal, highlight);
            //内容
            RenderContent();
        }

        protected virtual void RenderBox(string title, GUIStyle normal, GUIStyle highlight) 
        {
            GUI.Box(Rect, title, isSelected ? highlight : normal);
        }
        protected virtual void RenderContent() { }
        #endregion

        #region 事件处理
        public void ProcessEvent(Event e, ref bool reflected)
        {
            OnProcessEvent(e, ref reflected);
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (CanBecomeSelected(ref reflected))
                        { 
                            isSelected = true;
                            DragStart();
                        }
                        else
                            isSelected = false;
                    }
                    else if (e.button == 1)
                    {
                        if (CanBecomeSelected(ref reflected))
                        {
                            isSelected = true;
                            OnMenu(e.mousePosition);
                        }
                        else
                            isSelected = false;
                    }
                    GUI.changed = true;
                    break;
                case EventType.MouseDrag:
                    if (e.button == 0 && isDragged)
                        Drag(e);
                    break;
                case EventType.MouseUp:
                    if (e.button == 0)
                        DragEnd();
                    break;
            }

            bool CanBecomeSelected(ref bool reflected)
            {
                if (Rect.Contains(e.mousePosition) && !reflected)
                {
                    reflected = true;
                    return true;
                }
                return false;
            }
        }
        protected virtual void OnProcessEvent(Event e, ref bool reflected) { }
        private void DragStart()
        {
            isDragged = true;
        }
        private void Drag(Event e)
        {
            global.position += e.delta;
            ApplyGlobal2Local();
            e.Use();
        }
        private void DragEnd()
        {
            isDragged = false;
        }
        protected virtual void OnMenu(Vector2 mousePosition) { }
        #endregion

        #region 销毁
        public abstract void OnDestroy();
        #endregion

        #region Builder接口
        INodeBoxBuilder INodeBoxBuilder.BuildFather(NodeBoxElement father)
        {
            this.Parent = father;
            return this;
        }

        INodeBoxBuilder INodeBoxBuilder.BuildSize(Vector2 size)
        {
            this.global.size = size;
            return this;
        }

        INodeBoxBuilder INodeBoxBuilder.BuildPosition(Vector2 position)
        {
            this.global.position = position;
            ApplyGlobal2Local();
            return this;
        }

        INodeBoxBuilder INodeBoxBuilder.BuildTitle(string title)
        {
            this.title = title;
            return this;
        }

        INodeBoxBuilder INodeBoxBuilder.BuildStyles(GUIStyle normal, GUIStyle highlight)
        {
            this.normal = normal;
            this.highlight = highlight;
            return this;
        }

        NodeBoxElement INodeBoxBuilder.Complete()
        {
            return this;
        }
        #endregion
    }
}