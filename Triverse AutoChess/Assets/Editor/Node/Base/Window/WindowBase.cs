using Editor.NodeEditor.Command;
using Editor.NodeEditor.Query;
using QFramework;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditor.VersionControl;
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
            IDiagramDirectoryModel directoryModel = this.GetModel<IDiagramDirectoryModel>();
            InitStyle(styleFactorySytem);
            InitZone(nodeFactorySystem);
            InitNode(nodeFactorySystem);
            InitWindowMenu(menuSystem,nodeFactorySystem);
            InitZoneMenu(menuSystem, nodeFactorySystem);
            InitNodeMenu(menuSystem, nodeFactorySystem);
            InitPath(directoryModel);

            #region 命令
            renderCommand = new RenderCommand();
            eventCommand = new EventCommand();
            arrangeCommand = new ArrangeCommand();
            #endregion

            #region 事件
            this.RegisterEvent<LoadDiagramEvent>(OnLoad);
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
        protected abstract void InitPath(IDiagramDirectoryModel model);
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
            WindowToolBar();
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

        #region 工具栏
        int id;
        string fileName;
        private void WindowToolBar()
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("文件夹:" + this.GetModel<IDiagramDirectoryModel>().FilePath);
            GUILayout.Label("ID:", GUILayout.Width(40));
            id = EditorGUILayout.IntField(id,GUILayout.Width(40));
            GUILayout.Label("文件名:", GUILayout.Width(60));
            fileName = EditorGUILayout.TextField(fileName);
            if (GUILayout.Button(new GUIContent("保存")))
                OnSaveClick();
            if (GUILayout.Button(new GUIContent("所有图表")))
                OnOpenDirectory();

            GUILayout.EndHorizontal();
        }

        private void OnSaveClick()
        {
            IDiagramDirectoryModel diagramDirectoryModel = this.GetModel<IDiagramDirectoryModel>();
            int res = this.SendCommand(new AddDiagramDirectoryCommand(diagramDirectoryModel.FilePath, diagramDirectoryModel.DirectoryName, id, fileName));
            switch (res) 
            {
                case 1:
                    Debug.LogError("文件名已存在");
                    return;
                case 2:
                    Debug.LogError("ID已被占用");
                    return;
                case 3:
                    Debug.LogError("文件名不能为空");
                    return;
            }
            this.SendCommand(new SaveDiagramCommand(fileName, diagramDirectoryModel.FilePath));
            AssetDatabase.Refresh();
        }
        private void OnOpenDirectory()
        {
            EditorWindow window = GetWindow<DiagramDirectoryWindow>("所有图表");
            window.Show();
        }
        private void OnLoad(LoadDiagramEvent e)
        {
            id = e.ID;
            fileName = e.fileName;
            GUI.changed = true;
            Focus();
        }
        #endregion

        #region 其他
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
        #endregion
    }
}
