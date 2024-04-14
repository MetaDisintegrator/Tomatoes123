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
                        .SetAsEntrance()
                        , true);
            system.RegisterNode(-9,
                (builder) =>
                system.QBuildNode("测试回环","").Invoke(builder)
                .BuildPoint(new UnknowPoint())
                .SetAsCycleEntrance()
                , true);
            #endregion

            system.RegisterNode(1,
                (builder) =>
                    system.QBuildNode("测试", "").Invoke(builder)
                      .BuildItem(item.GetSimpleItem(E_NodeData.Int, "int"))
                      .BuildItem(item.GetSimpleItem(E_NodeData.Float, "float"))
                      .BuildPoint(new OutPoint(E_NodeData.Int, E_NodeDataScale.Single))
                      , false);
            system.RegisterNode(2,
                (builder) =>
                system.QBuildNode("大于","").Invoke(builder)
                .BuildItem(item.GetSimpleItem(E_NodeData.Float,"大"))
                .BuildItem(item.GetSimpleItem(E_NodeData.Float,"小"))
                .BuildPoint(new OutPoint(E_NodeData.Bool,E_NodeDataScale.Multiple))
                , false);
        }
    }
}
