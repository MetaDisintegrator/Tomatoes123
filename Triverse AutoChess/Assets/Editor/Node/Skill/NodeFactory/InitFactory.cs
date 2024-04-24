using Editor.NodeEditor.Items;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public partial class SkillEditorWindow : Window<SkillEditorWindow>
    {
        protected override void InitZone(INodeFactorySystemInit system)
        {
            system.RegisterZone(1, system.QZoneBuilder("CD扳机"));
            system.RegisterZone(100, system.QZoneBuilder("受到伤害扳机"));
        }

        protected override void InitNode(INodeFactorySystemInit system)
        {
            IItemFactorySystem item = this.GetSystem<IItemFactorySystem>();

            #region 特殊节点
            system.RegisterNode(-1,
                builder =>
                    system.QBuildNode("当CD结束", "").Invoke(builder)
                    .BuildItem(new OutLabelItem("控制",E_NodeData.Control))
                    , E_SpecialNode.Trigger);
            system.RegisterNode(-100,
                builder =>
                system.QBuildNode("受到伤害","").Invoke(builder)
                .BuildItem(new OutLabelItem("控制",E_NodeData.Control))
                .BuildItem(new OutLabelItem("伤害来源",E_NodeData.Chess))
                .BuildItem(new OutLabelItem("是持续伤害", E_NodeData.Bool))
                , E_SpecialNode.Trigger);
            system.RegisterNode(-1001,
                builder =>
                system.QBuildNode("棋子迭代入口","").Invoke(builder)
                .BuildItem(item.GetLabelItem("棋子"))
                .BuildPoint(new InPoint(E_NodeData.Chess,E_NodeDataScale.Multiple))
                .BuildPoint(new OutPoint(E_NodeData.Chess,E_NodeDataScale.Single))
                .BuildPoint(new UnknowPoint())
                ,E_SpecialNode.Entrance);
            system.RegisterNode(-2001,
                builder =>
                system.QBuildNode("循环入口", "").Invoke(builder)
                .BuildPoint(new UnknowPoint())
                , E_SpecialNode.CycleEntrance);
            system.RegisterNode(-2002,
                builder =>
                system.QBuildNode("出口", "").Invoke(builder)
                .BuildItem(item.GetRepeaterItem(E_NodeData.Control,"控制"))
                .BuildPoint(new UnknowPoint())
                , E_SpecialNode.Exit);
            #endregion

            system.RegisterNode(1,
                (builder) =>
                    system.QBuildNode("计算", "").Invoke(builder)
                    .BuildItem(item.GetRepeaterItem(E_NodeData.Control, "控制"))
                    .BuildItem(new ItemToolBar("运算符","加","减","乘","除"))
                    .BuildItem(item.GetSimpleItem(E_NodeData.Float,"a"))
                    .BuildItem(item.GetSimpleItem(E_NodeData.Float,"b"))
                    .BuildPoint(new OutPoint(E_NodeData.Float, E_NodeDataScale.Single))
                    ,E_SpecialNode.None);
            system.RegisterNode(2,
                (builder) =>
                    system.QBuildNode("比较", "").Invoke(builder)
                    .BuildItem(item.GetRepeaterItem(E_NodeData.Control,"控制"))
                    .BuildItem(new ItemToolBar("运算符", ">", "<", "!=", "=="))
                    .BuildItem(item.GetSimpleItem(E_NodeData.Float, "a"))
                    .BuildItem(item.GetSimpleItem(E_NodeData.Float, "b"))
                    .BuildPoint(new OutPoint(E_NodeData.Bool, E_NodeDataScale.Single))
                    , E_SpecialNode.None);
            system.RegisterNode(101,
                builder =>
                system.QBuildNode("当前锁定目标", "").Invoke(builder)
                .BuildItem(item.GetRepeaterItem(E_NodeData.Control, "控制"))
                .BuildPoint(new OutPoint(E_NodeData.Chess, E_NodeDataScale.Single))
                , E_SpecialNode.None);
        }
    }
}
