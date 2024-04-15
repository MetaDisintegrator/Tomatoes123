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
                        , E_SpecialNode.Entrance);
            system.RegisterNode(-9,
                (builder) =>
                system.QBuildNode("���Իػ�","").Invoke(builder)
                .BuildPoint(new UnknowPoint())
                , E_SpecialNode.CycleEntrance);
            system.RegisterNode(-8,
                (builder) =>
                system.QBuildNode("���Գ���", "").Invoke(builder)
                .BuildItem(item.GetSimpleItem(E_NodeData.Bool, "����ѭ��"))
                .BuildPoint(new UnknowPoint())
                , E_SpecialNode.Exit);
            #endregion

            system.RegisterNode(1,
                (builder) =>
                    system.QBuildNode("����", "").Invoke(builder)
                      .BuildItem(item.GetSimpleItem(E_NodeData.Int, "int"))
                      .BuildItem(item.GetSimpleItem(E_NodeData.Float, "float"))
                      .BuildPoint(new OutPoint(E_NodeData.Int, E_NodeDataScale.Single))
                      , E_SpecialNode.None);
            system.RegisterNode(2,
                (builder) =>
                system.QBuildNode("����","").Invoke(builder)
                .BuildItem(item.GetSimpleItem(E_NodeData.Float,"��"))
                .BuildItem(item.GetSimpleItem(E_NodeData.Float,"С"))
                .BuildPoint(new OutPoint(E_NodeData.Bool,E_NodeDataScale.Multiple))
                , E_SpecialNode.None);
        }
    }
}
