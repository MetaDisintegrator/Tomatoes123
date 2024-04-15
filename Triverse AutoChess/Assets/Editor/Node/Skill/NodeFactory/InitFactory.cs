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
            system.RegisterZone(1, system.QZoneBuilder("测试区域"));
        }

        protected override void InitNode(INodeFactorySystemInit system)
        {
            IItemFactorySystem item = this.GetSystem<IItemFactorySystem>();

            #region 特殊节点
            system.RegisterNode(-10,
                (builder) =>
                    system.QBuildNode("测试入口", "").Invoke(builder)
                        .BuildItem(item.GetRepeaterItem(E_NodeData.Int, "主"))
                        .BuildPoint(new UnknowPoint())
                        , E_SpecialNode.Entrance);
            system.RegisterNode(-9,
                (builder) =>
                system.QBuildNode("测试回环","").Invoke(builder)
                .BuildPoint(new UnknowPoint())
                , E_SpecialNode.CycleEntrance);
            system.RegisterNode(-8,
                (builder) =>
                system.QBuildNode("测试出口", "").Invoke(builder)
                .BuildItem(item.GetSimpleItem(E_NodeData.Bool, "打破循环"))
                .BuildPoint(new UnknowPoint())
                , E_SpecialNode.Exit);
            #endregion

            system.RegisterNode(1,
                (builder) =>
                    system.QBuildNode("测试", "").Invoke(builder)
                      .BuildItem(item.GetSimpleItem(E_NodeData.Int, "int"))
                      .BuildItem(item.GetSimpleItem(E_NodeData.Float, "float"))
                      .BuildPoint(new OutPoint(E_NodeData.Int, E_NodeDataScale.Single))
                      , E_SpecialNode.None);
            system.RegisterNode(2,
                (builder) =>
                system.QBuildNode("大于","").Invoke(builder)
                .BuildItem(item.GetSimpleItem(E_NodeData.Float,"大"))
                .BuildItem(item.GetSimpleItem(E_NodeData.Float,"小"))
                .BuildPoint(new OutPoint(E_NodeData.Bool,E_NodeDataScale.Multiple))
                , E_SpecialNode.None);
        }
    }
}
