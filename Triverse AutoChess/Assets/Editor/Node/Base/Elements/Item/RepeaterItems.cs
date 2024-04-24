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

    public class ItemMapPointRepeater : ItemMapPoint
    {
        public ItemMapPointRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.MapPoint, E_NodeDataScale.Single));
        }
    }

    public class ItemMapAreaRepeater : ItemMapArea
    {
        public ItemMapAreaRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.MapArea, E_NodeDataScale.Single));
        }
    }

    public class ItemProjecticleRepeater : ItemProjecticle
    {
        public ItemProjecticleRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.Projecticle, E_NodeDataScale.Single));
        }
    }

    public class ItemChessRepeater : ItemChess
    {
        public ItemChessRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.Chess, E_NodeDataScale.Single));
        }
    }

    public class ItemIndividualDataRepeater : ItemIndividualData
    {
        public ItemIndividualDataRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.IndividualData, E_NodeDataScale.Single));
        }
    }

    public class ItemStringRepeater : ItemString
    {
        public ItemStringRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.String, E_NodeDataScale.Single));
        }
    }

    public class ItemControlRepeater : ItemControl
    {
        public ItemControlRepeater(string title) : base(title) { }
        public override void RequirePoints(INodeBuilder builder)
        {
            base.RequirePoints(builder);
            DoRequirePoint(builder, new OutPoint(E_NodeData.Control, E_NodeDataScale.Single));
        }
    }
}
