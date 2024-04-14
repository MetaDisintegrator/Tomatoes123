using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor.Items
{
    public class ItemIntRepeater : ItemInt
    {
        private int value;
        public ItemIntRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.Int, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
            value = EditorGUILayout.DelayedIntField(value);
        }
    }

    public class ItemFloatRepeater : ItemFloat
    {
        private float value;
        public ItemFloatRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.Float, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
            value = EditorGUILayout.DelayedFloatField(value);
        }
    }

    public class ItemBoolRepeater : ItemBool
    {
        private bool value;
        public ItemBoolRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.Int, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
            value = EditorGUILayout.Toggle(value);
        }
    }
}
