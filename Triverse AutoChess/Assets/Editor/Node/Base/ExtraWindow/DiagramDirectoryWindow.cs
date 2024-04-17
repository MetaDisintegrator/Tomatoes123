using Editor.NodeEditor.Query;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class DiagramDirectoryWindow : EditorWindow, IController
    {
        Dictionary<int, string> diagramDirectory;
        bool lastFrameAlt;
        Vector2 scrollPos;
        private void OnEnable()
        {
            diagramDirectory = this.SendQuery(new DiagramDirectoryQuery());
        }


        private void OnGUI()
        {
            if (diagramDirectory == null) return;
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            //пео╒
            foreach (var pair in diagramDirectory)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("ID:" + pair.Key,GUILayout.Width(40));
                GUILayout.Label(pair.Value);
                if (GUILayout.Button("╪сть",GUILayout.Width(40)))
                    OnLoadClick(pair.Key,pair.Value);
                DeleteButton(pair.Key);
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
            if (this.GetModel<IDiagramDirectoryModel>().CheckRemove())
            {
                this.SendCommand(new SaveDiagramDirectoryCommand());
            }
            if(GUI.changed)
                Repaint();
        }
        private void OnLoadClick(int ID,string fileName)
        {
            this.SendCommand(new ReadDiagramCommand(ID,fileName, this.GetModel<IDiagramDirectoryModel>().FilePath));
        }
        private void DeleteButton(int id)
        {
            if (Event.current.alt)
            {
                if (!lastFrameAlt)
                {
                    GUI.changed = true;
                    lastFrameAlt = true;
                }
                if (GUILayout.Button("и╬ЁЩ", GUILayout.Width(40)))
                    OnDeleteClick(id);
            }
            else if (lastFrameAlt)
            {
                GUI.changed = true;
                lastFrameAlt = false;
            }
        }
        private void OnDeleteClick(int id)
        {
            this.SendCommand(new RemoveDiagramCommand(id));
            GUI.changed= true;
        }

        public IArchitecture GetArchitecture()
        {
            return NodeEditorArchitecture.Interface;
        }
    }
}
