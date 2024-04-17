using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor.Items
{
    public class ItemIntRepeater : ItemInt
    {
        public ItemIntRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.Int, E_NodeDataScale.Single));
        }
    }

    public class ItemFloatRepeater : ItemFloat
    {
        public ItemFloatRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.Float, E_NodeDataScale.Single));
        }
    }

    public class ItemBoolRepeater : ItemBool
    {
        public ItemBoolRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.Int, E_NodeDataScale.Single));
        }
    }
}
