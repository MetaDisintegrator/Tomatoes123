using Game.Tool;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor.Items
{
    public class ItemInt : Item
    {
        private int value;
        public ItemInt(string title):base(title) { }

        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder,new InPoint(E_NodeData.Int, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
                value = EditorGUILayout.DelayedIntField(value);
        }

        #region DiagramData
        protected override void OnWriteDiagramData(FileStream fs)
        {
            BinKit.Write(fs, false, value);
        }
        public override void ReadDiagramData(ByteArray BA)
        {
            value = BinKit.Read<int>(BA);
        }
        #endregion
    }

    public class ItemFloat : Item
    {
        private float value;
        public ItemFloat(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder, new InPoint(E_NodeData.Float, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
                value = EditorGUILayout.DelayedFloatField(value);
        }

        #region DiagramData
        protected override void OnWriteDiagramData(FileStream fs)
        {
            BinKit.Write(fs, false, value);
        }
        public override void ReadDiagramData(ByteArray BA)
        {
            value = BinKit.Read<float>(BA);
        }
        #endregion
    }

    public class ItemBool : Item
    {
        private bool value;
        public ItemBool(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder, new InPoint(E_NodeData.Bool, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
            value = EditorGUILayout.Toggle(value);
        }

        #region DiagramData
        protected override void OnWriteDiagramData(FileStream fs)
        {
            BinKit.Write(fs, false, value);
        }
        public override void ReadDiagramData(ByteArray BA)
        {
            value = BinKit.Read<bool>(BA);
        }
        #endregion
    }
}

