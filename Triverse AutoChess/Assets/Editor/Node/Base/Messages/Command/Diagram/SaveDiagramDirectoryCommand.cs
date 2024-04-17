using Game.Tool;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class SaveDiagramDirectoryCommand : AbstractCommand, ICommand
    {
        protected override void OnExecute()
        {
            IDiagramDirectoryModel model = this.GetModel<IDiagramDirectoryModel>();
            string directoryFullName = Path.Combine(model.FilePath, model.DirectoryName);
            using FileStream fs = File.Open(directoryFullName, FileMode.OpenOrCreate);
            BinKit.WriteDict(model.Directory, fs, (key, fs) => BinKit.Write(fs, false, key), (value, fs) => { BinKit.Write(fs, false, value); });
        }
    }
}

