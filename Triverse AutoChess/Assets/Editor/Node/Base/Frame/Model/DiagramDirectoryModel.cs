using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface IDiagramDirectoryModelInit:IModel
    {
        public string FilePath { get; set; }
    }
    public interface IDiagramDirectoryModel:IDiagramDirectoryModelInit
    {
        public string DirectoryName { get; }
        public Dictionary<int, string> Directory { get; }
        void Add2Remove(int ID);
        bool CheckRemove();
    }
    public class DiagramDirectoryModel:AbstractModel,IDiagramDirectoryModel
    {
        static readonly string _directoryName = "directory";
        public string DirectoryName => _directoryName;
        public string FilePath { get; set; }
        int idGen;
        Dictionary<int, string> _directory;
        public Dictionary<int, string> Directory { 
            get
            {
                if (_directory == null)
                    _directory = this.GetUtility<IDirectoryUtility>().GetDirectory(Path.Combine(FilePath, DirectoryName));
                return _directory;
            }
        }
        List<int> toRemove;

        public void Add2Remove(int ID)
        { 
            toRemove.Add(ID);
        }
        public bool CheckRemove()
        {
            bool res = toRemove.Count > 0;
            foreach (var ID in toRemove)
            { 
                Directory.Remove(ID);
            }
            toRemove.Clear();
            return res;
        }

        protected override void OnInit()
        {
            toRemove = new List<int>();
        }
    }
}

