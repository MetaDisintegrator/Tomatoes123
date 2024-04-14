using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public partial class SkillEditorWindow : Window<SkillEditorWindow>
    {
        protected override void InitWindowMenu(IMenuSystemInit system, INodeFactorySystem factory)
        {
            system.RegisterWindowMenu("����������", (pos, _) => { factory.GenerateZone(1, null, pos); }, null);
            system.RegisterWindowMenu("���ò���", (pos, _) => { factory.GenerateZone(1, null, pos); }, (_,_) => false);
        }

        protected override void InitZoneMenu(IMenuSystemInit system, INodeFactorySystem factory)
        {
            system.RegisterZoneMenu("����/��������", (pos, zone) => { factory.GenerateZone(1, zone, pos); }, null);
            system.RegisterZoneMenu("����/�������", (pos, zone) => { factory.GenerateNode(-10, zone, pos); }, null);
            system.RegisterZoneMenu("����/�ػ����", (pos, zone) => { factory.GenerateNode(-9, zone, pos); }, null);
            system.RegisterZoneMenu("����/���Խڵ�", (pos, zone) => { factory.GenerateNode(1, zone, pos); }, null);
            system.RegisterZoneMenu("�Ƚ�/����", (pos, zone) => { factory.GenerateNode(2, zone, pos); }, (pos, _) => { return pos.x > 100; });
            system.RegisterZoneMenu("ɾ��", (_, zone) => { this.SendCommand(new DestroyZoneCommand(zone)); },null) ;
        }

        protected override void InitNodeMenu(IMenuSystemInit system, INodeFactorySystem factory)
        {
            system.RegisterNodeMenu("ɾ��", (_, node) => { this.SendCommand(new DestroyNodeCommand(node)); },(_,node)=>!node.IsSpecial);
        }
    }
}
