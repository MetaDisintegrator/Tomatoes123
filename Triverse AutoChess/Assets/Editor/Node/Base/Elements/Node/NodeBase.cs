using Microsoft.SqlServer.Server;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface INode : IZoneContent, INodeBuilder
    {
        public int ID { get; }
        public bool IsSpecial { get; }
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
        
    }

    public class Node : NodeBoxElement, INode
    {
        static readonly float headOffset = 20;
        float currentBottom;
        #region 绘图数据
        public int TypeID { get; set; }
        public bool IsSpecial { get; set; }
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
        bool isEntrance;
        bool isCycleEntrance;
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

        INodeBuilder INodeBuilder.BuildSecondZone(IZone secondZone)
        {
            this.SecondZone = secondZone;
            return this;
        }

        INodeBuilder INodeBuilder.BuildToken(bool isSpecial)
        {
            this.IsSpecial = isSpecial;
            return this;
        }

        INode INodeBuilder.Complete()
        {
            if (isEntrance) OwnerZone.Entrance = this;
            if (isCycleEntrance) OwnerZone.CycleEntrance = this;
            return this;
        }

        INodeBuilder INodeBuilder.BuildItem(IItem item)
        {
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
        INodeBuilder INodeBuilder.SetAsEntrance()
        {
            isEntrance = true;
            return this;
        }

        INodeBuilder INodeBuilder.SetAsCycleEntrance()
        {
            isCycleEntrance = true;
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
    }
}
