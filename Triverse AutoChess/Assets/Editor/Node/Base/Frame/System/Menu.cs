using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface IMenuSystemInit : ISystem
    {
        void RegisterWindowMenu(string itemName, Action<Vector2, NodeEditorSetting> action, Func<Vector2, NodeEditorSetting, bool> enableCondition);
        void RegisterZoneMenu(string itemName, Action<Vector2, IZone> action, Func<Vector2, IZone, bool> enableCondition);
        void RegisterNodeMenu(string itemName, Action<Vector2, INode> action, Func<Vector2, INode, bool> enableCondition);
    }
    public interface IMenuSystem : IMenuSystemInit
    {
        void ShowWindowMenu(Vector2 pos, NodeEditorSetting setting);
        void ShowZoneMenu(Vector2 pos, IZone zone);
        void ShowNodeMenu(Vector2 pos, INode node);
    }

    public class MenuSystem : AbstractSystem, IMenuSystem
    {
        #region 结构定义
        struct CustomMenuItem<T>
        {
            public string name;
            public Action<Vector2, T> action;
            public Func<Vector2, T, bool> enableCondition;
            public CustomMenuItem(string name, Action<Vector2, T> action, Func<Vector2, T, bool> enableCondition)
            {
                this.name = name;
                this.action = action;
                this.enableCondition = enableCondition;
            }
            public CustomMenuItem(string name, Action<Vector2, T> action)
            {
                this.name = name;
                this.action = action;
                this.enableCondition = null;
            }
        }
        #endregion

        List<CustomMenuItem<NodeEditorSetting>> windowMenu;
        List<CustomMenuItem<IZone>> zoneMenu;
        List<CustomMenuItem<INode>> nodeMenu;

        #region Register
        void IMenuSystemInit.RegisterWindowMenu(string itemName, Action<Vector2, NodeEditorSetting> action, Func<Vector2, NodeEditorSetting, bool> enableCondition)
        {
            windowMenu.Add(new CustomMenuItem<NodeEditorSetting>(itemName, action, enableCondition));
        }
        void IMenuSystemInit.RegisterZoneMenu(string itemName, Action<Vector2, IZone> action, Func<Vector2, IZone, bool> enableCondition)
        {
            zoneMenu.Add(new CustomMenuItem<IZone>(itemName, action, enableCondition));
        }
        void IMenuSystemInit.RegisterNodeMenu(string itemName, Action<Vector2, INode> action, Func<Vector2, INode, bool> enableCondition)
        {
            nodeMenu.Add(new CustomMenuItem<INode>(itemName, action, enableCondition));
        }
        #endregion

        #region Show
        public void ShowWindowMenu(Vector2 pos, NodeEditorSetting setting)
        {
            ShowMenu(windowMenu, pos, setting);
        }
        public void ShowZoneMenu(Vector2 pos, IZone zone)
        {
            ShowMenu(zoneMenu, pos, zone);
        }
        public void ShowNodeMenu(Vector2 pos, INode node)
        {
            ShowMenu(nodeMenu, pos, node);
        }
        private void ShowMenu<T>(IEnumerable<CustomMenuItem<T>> menuList, Vector2 pos, T arg)
        {
            GenericMenu menu = new GenericMenu();
            foreach (var item in menuList)
            {
                if (item.enableCondition == null || item.enableCondition(pos, arg))
                {
                    menu.AddItem(new GUIContent(item.name), false, () => item.action(pos, arg));
                }
                else
                {
                    menu.AddDisabledItem(new GUIContent(item.name));
                }
            }
            menu.ShowAsContext();
        }
        #endregion


        protected override void OnInit()
        {
            windowMenu = new List<CustomMenuItem<NodeEditorSetting>>();
            zoneMenu = new List<CustomMenuItem<IZone>>();
            nodeMenu = new List<CustomMenuItem<INode>>();
        }
    }
}
