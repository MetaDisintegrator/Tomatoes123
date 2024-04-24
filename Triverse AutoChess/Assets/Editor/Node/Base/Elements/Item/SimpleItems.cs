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
        public ItemInt(string title) : base(title) { }

        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder, new InPoint(E_NodeData.Int, E_NodeDataScale.Single));
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

    public class ItemMapPoint : Item
    {
        private Pos value;
        public ItemMapPoint(string title) : base(title) { }

        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder, new InPoint(E_NodeData.MapPoint, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
            GUILayout.BeginHorizontal();
            value.x = EditorGUILayout.DelayedIntField(value.x);
            value.y = EditorGUILayout.DelayedIntField(value.y);
            GUILayout.EndHorizontal();
        }

        #region DiagramData
        protected override void OnWriteDiagramData(FileStream fs)
        {
            BinKit.Write(fs, false, value);
        }
        public override void ReadDiagramData(ByteArray BA)
        {
            value = BinKit.Read<Pos>(BA);
        }
        #endregion
    }

    public class ItemMapArea : Item
    {
        private List<Pos> value;
        public ItemMapArea(string title) : base(title) { }

        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder, new InPoint(E_NodeData.MapArea, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
            if (GUILayout.Button("编辑"))
            { }
        }

        #region DiagramData
        protected override void OnWriteDiagramData(FileStream fs)
        {
            BinKit.WriteList(value, fs, (pos, fs) => BinKit.Write(fs, false, pos));
        }
        public override void ReadDiagramData(ByteArray BA)
        {
            value = BinKit.ReadList(BA, ba => BinKit.Read<Pos>(ba));
        }
        #endregion
    }

    public class ItemProjecticle : Item
    {
        private int value;
        public ItemProjecticle(string title) : base(title) { }

        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder, new InPoint(E_NodeData.Projecticle, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
            value = EditorGUILayout.DelayedIntField(new GUIContent("(战场ID)"), value);
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

    public class ItemChess : Item
    {
        private int value;
        public ItemChess(string title) : base(title) { }

        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder, new InPoint(E_NodeData.Chess, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
            value = EditorGUILayout.DelayedIntField(new GUIContent("(战场ID)"), value);
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

    public class ItemIndividualData : Item
    {
        private int value;
        public ItemIndividualData(string title) : base(title) { }

        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder, new InPoint(E_NodeData.IndividualData, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
            value = EditorGUILayout.DelayedIntField(new GUIContent("(个体ID)"), value);
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

    public class ItemString : Item
    {
        private string value;
        public ItemString(string title) : base(title) { }

        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder, new InPoint(E_NodeData.String, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
            value = EditorGUILayout.DelayedTextField(value);
        }

        #region DiagramData
        protected override void OnWriteDiagramData(FileStream fs)
        {
            BinKit.Write(fs, false, value);
        }
        public override void ReadDiagramData(ByteArray BA)
        {
            value = BinKit.Read<string>(BA);
        }
        #endregion
    }

    public class ItemControl : Item
    {
        public ItemControl(string title) : base(title, 20, 20) { }

        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder, new InPoint(E_NodeData.Control, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            return;
        }

        #region DiagramData
        protected override void OnWriteDiagramData(FileStream fs)
        {
            return;
        }
        public override void ReadDiagramData(ByteArray BA)
        {
            return;
        }
        #endregion
    }

    public class ItemToolBar : Item
    {
        private int value;
        private string[] options;
        public ItemToolBar(string title,params string[] options) : base(title) 
        {
            this.options = options;
        }

        public override void RequirePoints(INodeBuilder builder)
        {
            DoRequirePoint(builder, new InPoint(E_NodeData.Int, E_NodeDataScale.Single));
        }

        protected override void OnRender(bool connected)
        {
            if (connected) return;
            value = GUILayout.Toolbar(value, options);
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
}
