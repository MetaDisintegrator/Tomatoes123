using Game.Tool;
using Microsoft.SqlServer.Server;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface INode : IZoneContent,IDiagramData
    {
        public int ID { get; }
        public E_SpecialNode SpecialType { get; set; }
        public IZone OwnerZone { get; }
        public IZone SecondZone { get; }
        public List<IPoint> InPoints { get; set; }
        public List<IPoint> OutPoints { get; set; }
        public List<IItem> Items { get; set; }
        void OnRecv(INode inNode);
        void OnDisRecv(INode inNode);
        bool CanConnect(INode outNode);
        bool CheckLoop(INode outNode);

        void MoveUnknown();

        int IndexofPoint(IPoint point);
        int IndexofItem(IItem item);
    }

    public class Node : NodeBoxElement, INode, INodeBuilder
    {
        static readonly float headOffset = 20;
        float currentBottom;
        #region 绘图数据
        public int TypeID { get; set; }
        public E_SpecialNode SpecialType { get; set; }
        #endregion

        #region 转换数据
        public string SoName;
        #endregion

        #region 通用数据
        public int ID { get; set; }
        public IZone OwnerZone { get; set; }
        public IZone SecondZone { get; set; }
        public List<IPoint> InPoints { get; set; }
        public List<IPoint> OutPoints { get; set; }
        public List<IItem> Items { get; set; }
        #endregion

        #region BoxElement
        protected override void RenderContent()
        {
            float fitHeight = 0;
            //条目
            DoRender(Items, Rect.center.x);
            //节点
            DoRender(InPoints, Rect.xMin);
            DoRender(OutPoints, Rect.xMax);
            Height = fitHeight + 10;
            //实现
            void DoRender<T>(List<T> contents, float x) where T : INodeContent
            {
                currentBottom = Rect.yMin + headOffset;
                for (int i =0;i<contents.Count;i++)
                {
                    contents[i].Render(new Vector2(x, contents[i].CalCenterHeight(ref currentBottom)));
                }
                fitHeight = Mathf.Max(fitHeight, currentBottom - Rect.yMin);
            }
        }

        protected override void OnMenu(Vector2 mousePosition)
        {
            this.GetSystem<IMenuSystem>().ShowNodeMenu(mousePosition, this);
        }
        #endregion

        #region Builder
        
        public Node()
        {
            InPoints = new List<IPoint>();
            OutPoints = new List<IPoint>();
            Items = new List<IItem>();
            inNodes = new List<INode>();
        }
        INodeBuilder INodeBuilder.BuildType(int typeId)
        {
            this.TypeID = typeId;
            return this;
        }

        INodeBuilder INodeBuilder.BuildID(int id)
        {
            this.ID = id;
            return this;
        }

        INodeBuilder INodeBuilder.BuildSO(string SoName)
        {
            this.SoName = SoName;
            return this;
        }

        INodeBuilder INodeBuilder.BuildFather(IZone father)
        {
            this.Parent = father as Zone;
            this.OwnerZone = father;
            return this;
        }

        INode INodeBuilder.Complete()
        {
            switch (SpecialType)
            {
                case E_SpecialNode.Entrance:
                    OwnerZone.Entrance = this;
                    SecondZone = OwnerZone.Father;
                    break;
                case E_SpecialNode.CycleEntrance:
                    OwnerZone.CycleEntrance = this;
                    break;
                case E_SpecialNode.Exit:
                    OwnerZone.Exit = this;
                    SecondZone = OwnerZone.Father;
                    break;
                case E_SpecialNode.Trigger:
                    OwnerZone.Trigger = this;
                    break;
            }
            return this;
        }

        INodeBuilder INodeBuilder.BuildItem(IItem item)
        {
            item.OwnerNode = this;
            item.RequirePoints(this);
            item.Width = Rect.width * 0.9f;
            Items.Add(item);
            return this;
        }

        INodeBuilder INodeBuilder.BuildPoint(IPoint point)
        {
            List<IPoint> list = point.Dir == E_PointDir.In ? InPoints : OutPoints;
            list.Add(point);
            point.OwnerNode = this;
            return this;
        }
        INodeBuilder INodeBuilder.SetSpecial(E_SpecialNode specialType)
        {
            this.SpecialType = specialType;
            return this;
        }
        #endregion

        #region CanConnect
        List<INode> inNodes;
        public void OnRecv(INode inNode)
        {
            inNodes.Add(inNode);
        }

        public void OnDisRecv(INode inNode)
        {
            inNodes.Remove(inNode);
        }

        public bool CanConnect(INode outNode)
        {
            return CheckLoop(outNode) && CheckZone(outNode);
        }
        public bool CheckLoop(INode outNode)
        {
            if (outNode == this) return false;
            if (inNodes.Contains(outNode)) return false;
            foreach (INode node in inNodes)
            {
                if (!node.CheckLoop(outNode)) return false;
            }
            return true;
        }
        private bool CheckZone(INode node)
        {
            return (node.OwnerZone == OwnerZone) ||
                (node.SecondZone != null && node.SecondZone == OwnerZone) ||
                (SecondZone != null && SecondZone == node.OwnerZone) ||
                (SecondZone != null && node.SecondZone != null && SecondZone == node.SecondZone);
        }
        #endregion

        public override void OnDestroy()
        {
            ReleaseAll(Items);
            ReleaseAll(InPoints);
            ReleaseAll(OutPoints);

            static void ReleaseAll<T>(List<T> contents) where T : INodeContent
            {
                if (contents == null) return;
                while (contents.Count > 0)
                {
                    contents[0].OnDestroy();
                    contents.RemoveAt(0);
                }
            }
        }

        public void MoveUnknown()
        {
            for (int i = InPoints.Count - 1; i >= 0; i--)
            {
                if (InPoints[i] is UnknowPoint)
                {
                    InPoints.Add(InPoints[i]);
                    InPoints.RemoveAt(i);
                    break;
                }
            }
        }

        #region DiagramData
        public void WriteDiagramData(FileStream fs)
        {
            //父区域ID
            BinKit.Write(fs,false,OwnerZone.ID);
            //pos
            BinKit.Write(fs, false, Rect.position);
            //节点类型
            BinKit.Write(fs, false, TypeID);
            //ID
            BinKit.Write(fs, false, ID);
            //特殊节点写入
            switch (SpecialType)
            {
                case E_SpecialNode.Entrance:
                case E_SpecialNode.Exit:
                    List<IItem> dynamicItems = Items.Where(item => item.Dynamic).ToList();
                    BinKit.WriteList(dynamicItems, fs, (item, fs) => BinKit.Write(fs, false, (int)item.Points[0].DataType));
                    break;
            }
        }
        public int IndexofPoint(IPoint point)
        { 
            if(point is InPoint) return InPoints.IndexOf(point);
            if(point is OutPoint) return OutPoints.IndexOf(point);
            return -1;
        }
        public int IndexofItem(IItem item)
        { 
            return Items.IndexOf(item);
        }
        #endregion
    }
}
