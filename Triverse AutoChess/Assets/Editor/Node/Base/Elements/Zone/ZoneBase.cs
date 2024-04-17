using Game.Tool;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.NodeEditor
{
    public interface IZone : IZoneContent,IDiagramData
    { 
        public int ID { get; }
        public Vector2 Min { get; set; }
        public Vector2 Max { get; set; }
        public IZone Father { get; }
        public List<INode> Nodes { get; }

        public INode Trigger { get; set; }
        public INode Entrance { get; set; }
        public INode CycleEntrance { get; set; }
        public INode Exit { get; set; }
        InPoint HandleSpecialConnect(E_NodeData dataType,E_SpecialNode callerType);


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

        #region SpecialNodes
        public INode Trigger { get; set; }
        public INode Entrance { get; set; }
        public INode CycleEntrance { get; set; }
        public INode Exit { get; set; }
        List<PointGroup> pointGroups;
        public InPoint HandleSpecialConnect(E_NodeData dataType, E_SpecialNode callerType)
        {
            IItemFactorySystem itemFactory = this.GetSystem<IItemFactorySystem>();
            InPoint res = null;
            IItem repeater;
            IItem simple;
            switch (callerType)
            {
                case E_SpecialNode.None:
                    Debug.LogError("非特殊节点调用了特殊节点连接处理");
                    break;
                case E_SpecialNode.Entrance:
                    AddCycle();
                    //返回入口点
                    res = repeater.Points.First(point => point is InPoint) as InPoint; 
                    break;
                case E_SpecialNode.CycleEntrance:
                    AddCycle();
                    //返回入口点
                    res = simple.Points.First(point => point is InPoint) as InPoint;
                    break;
                case E_SpecialNode.Exit:
                    repeater = itemFactory.GetRepeaterItem(dataType, "区域输出");
                    (Exit as INodeBuilder).BuildItem(repeater);
                    Exit.MoveUnknown();
                    RegisterGroup(repeater);
                    res = repeater.Points.First(point => point is InPoint) as InPoint;
                    break;
            }
            return res;
            void RegisterGroup(params IItem[] items)
            {
                PointGroup pointGroup = new();
                foreach (var item in items)
                {
                    item.Dynamic = true;
                    foreach (var point in item.Points)
                    {
                        pointGroup.points.Add(point);
                    }
                }
                pointGroups.Add(pointGroup);
            }
            void AddCycle()
            {
                repeater = itemFactory.GetRepeaterItem(dataType, "入口输入");
                simple = itemFactory.GetSimpleItem(dataType, "回环输入");
                //添加条目
                (Entrance as INodeBuilder).BuildItem(repeater);
                (CycleEntrance as INodeBuilder).BuildItem(simple);
                //移动Unknown
                Entrance.MoveUnknown();
                CycleEntrance.MoveUnknown();
                RegisterGroup(repeater, simple);
            }
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

        #region DiagramData
        public void WriteDiagramData(FileStream fs)
        {
            //父区域ID
            BinKit.Write(fs, false, Father != null ? Father.ID : -1);
            //pos
            BinKit.Write(fs, false, Rect.position);
            //区域类型
            BinKit.Write(fs, false, TypeID);
            //ID
            BinKit.Write(fs, false, ID);
        }
        #endregion

        public void RemoveNode(Node node)
        {
            node.OnDestroy();
            Nodes.Remove(node);
        }
    }
}
