using System.Collections;
using System.Collections.Generic;
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
    }

    public class ItemBool : Item
    {
        private bool value;
        public ItemBool(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder, new InPoint(E_NodeData.Float, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
            value = EditorGUILayout.Toggle(value);
        }
    }

    
}

