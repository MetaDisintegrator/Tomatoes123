using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.NodeEditor
{
    public interface IZone : IZoneContent
    { 
        public int ID { get; }
        public Vector2 Min { get; set; }
        public Vector2 Max { get; set; }
        public IZone Father { get; }
        public List<INode> Nodes { get; }

        public INode Entrance { get; set; }
        public INode CycleEntrance { get; set; }
        InPoint AddCycleItem(E_NodeData dataType);


        void RemoveNode(Node node);
    }

    public class Zone : NodeBoxElement, IZone, IZoneBuilder
    {
        #region 绘图数据
        public int TypeID { get; set; }
        public IZone Father { get; set; }
        #endregion

        #region 转化数据
        public int ID { get; set; }
        #endregion

        #region 通用数据
        public List<INode> Nodes { get; set; }
        #endregion

        #region BoxElement
        #region 渲染
        protected override void RenderBox(string title, GUIStyle normal, GUIStyle highlight)
        {
            float boarderWidth = isSelected ? 20 : 10;
            Vector2 rTop = new(Rect.xMax, Rect.yMin);
            Vector2 lBottom = new(Rect.xMin, Rect.yMax);

            RendLine(Rect.min, rTop, Vector2.up, title);
            RendLine(rTop, Rect.max, Vector2.left);
            RendLine(Rect.max, lBottom, Vector2.down);
            RendLine(lBottom, Rect.min, Vector2.right);

            void RendLine(Vector2 start, Vector2 end, Vector2 norm, string text = "")
            {
                Rect rect = new Rect(start, end + norm * boarderWidth - start);
                GUI.Box(rect, text, isSelected ? highlight : normal);
            }
        }

        protected override void RenderContent()
        {
            foreach (INode node in Nodes)
            {
                node.Render();
            }
        }
        #endregion

        #region 事件
        protected override void OnProcessEvent(Event e, ref bool reflected)
        {
            foreach (INode node in Nodes)
            {
                node.ProcessEvent(e, ref reflected);
            }
            CheckGroups();
        }

        protected override void OnMenu(Vector2 mousePosition)
        {
            this.GetSystem<IMenuSystem>().ShowZoneMenu(mousePosition, this);
        }
        #endregion

        public override void OnDestroy()
        {
            Dictionary<int, INode> model = this.GetModel<INodeModel>().Nodes;
            foreach (INode node in Nodes)
            {
                (node as Node).OnDestroy();
                model.Remove(node.ID);
            }
        }
        #endregion

        #region Builder
        IZoneBuilder IZoneBuilder.BuildType(int typeId)
        {
            this.TypeID = typeId;
            return this;
        }

        IZoneBuilder IZoneBuilder.BuildID(int id)
        {
            this.ID = id;
            return this;
        }

        IZone IZoneBuilder.Complete()
        {
            pointGroups = new List<PointGroup>();
            Nodes = new List<INode>();
            return this;
        }

        IZoneBuilder IZoneBuilder.BuildFather(IZone father)
        {
            this.Parent = father as Zone;
            this.Father = father;
            return this;
        }
        #endregion

        #region Cycle
        public INode Entrance { get; set; }
        public INode CycleEntrance { get; set; }
        List<PointGroup> pointGroups;
        public InPoint AddCycleItem(E_NodeData dataType)
        {
            IItemFactorySystem itemFactory = this.GetSystem<IItemFactorySystem>();
            IItem repeater = itemFactory.GetRepeaterItem(dataType,"入口输入");
            IItem simple = itemFactory.GetSimpleItem(dataType, "回环输入");
            PointGroup pointGroup = new();
            //添加条目
            Entrance.BuildItem(repeater);
            CycleEntrance.BuildItem(simple);
            //返回值
            InPoint res = simple.Points[0] as InPoint;
            //添加管理
            foreach (var point in repeater.Points)
            {
                pointGroup.points.Add(point);
            }
            foreach (var point in simple.Points)
            { 
                pointGroup.points.Add(point);
            }
            pointGroups.Add(pointGroup);
            //移动Unknown
            Entrance.MoveUnknown();
            CycleEntrance.MoveUnknown();
            return res;
        }

        void CheckGroups()
        {
            for (int i = 0; i < pointGroups.Count; i++)
            {
                if (!pointGroups[i].CheckHasConnect())
                {
                    pointGroups[i].OnDestroy();
                    pointGroups.RemoveAt(i);
                    break;
                }
            }
        }
        #endregion

        public void RemoveNode(Node node)
        {
            node.OnDestroy();
            Nodes.Remove(node);
        }
    }
}
