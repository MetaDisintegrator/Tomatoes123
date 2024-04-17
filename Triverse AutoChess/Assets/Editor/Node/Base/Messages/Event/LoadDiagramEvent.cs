using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public struct LoadDiagramEvent
    {
        public int ID;
        public string fileName;

        public LoadDiagramEvent(int ID,string fileName)
        { 
            this.ID = ID;
            this.fileName = fileName;
        }
    }
}

