using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class RemoveDiagramCommand : AbstractCommand, ICommand
    {
        int ID;
        protected override void OnExecute()
        {
            IDiagramDirectoryModel model = this.GetModel<IDiagramDirectoryModel>();
            string fullName = Path.Combine(model.FilePath, model.Directory[ID]);
            if (File.Exists(fullName))
            { 
                File.Delete(fullName);
            }
            model.Add2Remove(ID);
            AssetDatabase.Refresh();
        }
        public RemoveDiagramCommand(int iD)
        {
            ID = iD;
        }
    }
}

