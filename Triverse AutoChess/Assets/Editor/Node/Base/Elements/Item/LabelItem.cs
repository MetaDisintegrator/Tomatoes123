using Game.Tool;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class LabelItem : Item
    {
        public LabelItem(string title) : base(title,20,20) { }

        

        public override void RequirePoints(INodeBuilder builder)
        {
        }

        protected override void OnRender(bool connected)
        {
        }

        public override void ReadDiagramData(ByteArray BA)
        {
            return;
        }
        protected override void OnWriteDiagramData(FileStream fs)
        {
            return;
        }
    }
}

