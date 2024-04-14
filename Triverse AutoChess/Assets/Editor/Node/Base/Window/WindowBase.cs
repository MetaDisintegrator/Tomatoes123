using Editor.NodeEditor.Command;
using QFramework;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor
{
    public abstract class Window<T> : EditorWindow, IController where T : Window<T>
    {
        #region 初始化
        private void OnEnable()
        {
            IStyleFactorySystem styleFactorySytem = this.GetSystem<IStyleFactorySystem>();         
            INodeFactorySystem nodeFactorySystem = this.GetSystem<INodeFactorySystem>();
            IMenuSystem menuSystem = this.GetSystem<IMenuSystem>();
            InitStyle(styleFactorySytem);
            InitZone(nodeFactorySystem);
            InitNode(nodeFactorySystem);
            InitWindowMenu(menuSystem,nodeFactorySystem);
            InitZoneMenu(menuSystem, nodeFactorySystem);
            InitNodeMenu(menuSystem, nodeFactorySystem);
            #region 命令
            renderCommand = new RenderCommand();
            eventCommand = new EventCommand();
            arrangeCommand = new ArrangeCommand();
            #endregion
        }
        private void OnDisable() 
        {
            GetArchitecture().Deinit();
        }
        protected abstract void InitWindowMenu(IMenuSystemInit system,INodeFactorySystem factory);
        protected abstract void InitZoneMenu(IMenuSystemInit system, INodeFactorySystem factory);
        protected abstract void InitNodeMenu(IMenuSystemInit system, INodeFactorySystem factory);
        protected abstract void InitStyle(IStyleFactorySystemInit system);
        protected abstract void InitNode(INodeFactorySystemInit system);
        protected abstract void InitZone(INodeFactorySystemInit system);
        #endregion

        #region 更新
        RenderCommand renderCommand;
        EventCommand eventCommand;
        ArrangeCommand arrangeCommand;
        private void OnGUI()
        {
            this.SendCommand(renderCommand);
            this.SendCommand(arrangeCommand);
            eventCommand.Set(Event.current);
            if (!this.SendCommand(eventCommand)) 
                WindowEvent(Event.current);
            FixedWindowEvent(Event.current);
            if(GUI.changed)
                Repaint();
        }

        private void WindowEvent(Event e)
        {
            switch (e.type)
            { 
                case EventType.MouseDown:
                    if(e.button==1)
                        OpenWindowMenu(e);
                    break;
                case EventType.MouseDrag:
                    if(e.button==2)
                    {
                        foreach (var root in this.GetModel<IZoneModel>().Trees)
                        {
                            root.value.Center += e.delta;
                        }
                        GUI.changed = true;
                    }
                    break;
            }
        }
        private void FixedWindowEvent(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                        this.GetSystem<IConnectSystem>().Selecting = null;
                    break;
            }
        }
        #endregion

        protected static void DoOpenWindow(string title)
        {
            EditorWindow window = GetWindow<T>(title);
            window.Show();
        }

        public IArchitecture GetArchitecture()
        {
            return NodeEditorArchitecture.Interface;
        }
        
        private void OpenWindowMenu(Event e)
        {
            this.GetSystem<IMenuSystem>().ShowWindowMenu(e.mousePosition, null);
        }
    }
}
