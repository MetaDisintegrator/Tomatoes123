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
            system.RegisterZone(1, system.QZoneBuilder("��������"));
        }

        protected override void InitNode(INodeFactorySystemInit system)
        {
            IItemFactorySystem item = this.GetSystem<IItemFactorySystem>();

            #region ����ڵ�
            system.RegisterNode(-10,
                (builder) =>
                    system.QBuildNode("�������", "").Invoke(builder)
                        .BuildItem(item.GetRepeaterItem(E_NodeData.Int, "��"))
                        .BuildPoint(new UnknowPoint())
                        .SetAsEntrance()
                        , true);
            system.RegisterNode(-9,
                (builder) =>
                system.QBuildNode("���Իػ�","").Invoke(builder)
                .BuildPoint(new UnknowPoint())
                .SetAsCycleEntrance()
                , true);
            #endregion

            system.RegisterNode(1,
                (builder) =>
                    system.QBuildNode("����", "").Invoke(builder)
                      .BuildItem(item.GetSimpleItem(E_NodeData.Int, "int"))
                      .BuildItem(item.GetSimpleItem(E_NodeData.Float, "float"))
                      .BuildPoint(new OutPoint(E_NodeData.Int, E_NodeDataScale.Single))
                      , false);
            system.RegisterNode(2,
                (builder) =>
                system.QBuildNode("����","").Invoke(builder)
                .BuildItem(item.GetSimpleItem(E_NodeData.Float,"��"))
                .BuildItem(item.GetSimpleItem(E_NodeData.Float,"С"))
                .BuildPoint(new OutPoint(E_NodeData.Bool,E_NodeDataScale.Multiple))
                , false);
        }
    }
}
