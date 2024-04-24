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
            system.RegisterZone(1, system.QZoneBuilder("CD���"));
            system.RegisterZone(100, system.QZoneBuilder("�ܵ��˺����"));
        }

        protected override void InitNode(INodeFactorySystemInit system)
        {
            IItemFactorySystem item = this.GetSystem<IItemFactorySystem>();

            #region ����ڵ�
            system.RegisterNode(-1,
                builder =>
                    system.QBuildNode("��CD����", "").Invoke(builder)
                    .BuildItem(new OutLabelItem("����",E_NodeData.Control))
                    , E_SpecialNode.Trigger);
            system.RegisterNode(-100,
                builder =>
                system.QBuildNode("�ܵ��˺�","").Invoke(builder)
                .BuildItem(new OutLabelItem("����",E_NodeData.Control))
                .BuildItem(new OutLabelItem("�˺���Դ",E_NodeData.Chess))
                .BuildItem(new OutLabelItem("�ǳ����˺�", E_NodeData.Bool))
                , E_SpecialNode.Trigger);
            system.RegisterNode(-1001,
                builder =>
                system.QBuildNode("���ӵ������","").Invoke(builder)
                .BuildItem(item.GetLabelItem("����"))
                .BuildPoint(new InPoint(E_NodeData.Chess,E_NodeDataScale.Multiple))
                .BuildPoint(new OutPoint(E_NodeData.Chess,E_NodeDataScale.Single))
                .BuildPoint(new UnknowPoint())
                ,E_SpecialNode.Entrance);
            system.RegisterNode(-2001,
                builder =>
                system.QBuildNode("ѭ�����", "").Invoke(builder)
                .BuildPoint(new UnknowPoint())
                , E_SpecialNode.CycleEntrance);
            system.RegisterNode(-2002,
                builder =>
                system.QBuildNode("����", "").Invoke(builder)
                .BuildItem(item.GetRepeaterItem(E_NodeData.Control,"����"))
                .BuildPoint(new UnknowPoint())
                , E_SpecialNode.Exit);
            #endregion

            system.RegisterNode(1,
                (builder) =>
                    system.QBuildNode("����", "").Invoke(builder)
                    .BuildItem(item.GetRepeaterItem(E_NodeData.Control, "����"))
                    .BuildItem(new ItemToolBar("�����","��","��","��","��"))
                    .BuildItem(item.GetSimpleItem(E_NodeData.Float,"a"))
                    .BuildItem(item.GetSimpleItem(E_NodeData.Float,"b"))
                    .BuildPoint(new OutPoint(E_NodeData.Float, E_NodeDataScale.Single))
                    ,E_SpecialNode.None);
            system.RegisterNode(2,
                (builder) =>
                    system.QBuildNode("�Ƚ�", "").Invoke(builder)
                    .BuildItem(item.GetRepeaterItem(E_NodeData.Control,"����"))
                    .BuildItem(new ItemToolBar("�����", ">", "<", "!=", "=="))
                    .BuildItem(item.GetSimpleItem(E_NodeData.Float, "a"))
                    .BuildItem(item.GetSimpleItem(E_NodeData.Float, "b"))
                    .BuildPoint(new OutPoint(E_NodeData.Bool, E_NodeDataScale.Single))
                    , E_SpecialNode.None);
            system.RegisterNode(101,
                builder =>
                system.QBuildNode("��ǰ����Ŀ��", "").Invoke(builder)
                .BuildItem(item.GetRepeaterItem(E_NodeData.Control, "����"))
                .BuildPoint(new OutPoint(E_NodeData.Chess, E_NodeDataScale.Single))
                , E_SpecialNode.None);
        }
    }
}
