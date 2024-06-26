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
            system.RegisterWindowMenu("开启新区域", (pos, _) => { factory.GenerateZone(1, null, pos); }, null);
            system.RegisterWindowMenu("禁用测试", (pos, _) => { factory.GenerateZone(1, null, pos); }, (_,_) => false);
        }

        protected override void InitZoneMenu(IMenuSystemInit system, INodeFactorySystem factory)
        {
            system.RegisterZoneMenu("测试/测试区域", (pos, zone) => { factory.GenerateZone(1, zone, pos); }, null);
            system.RegisterZoneMenu("特殊/棋子迭代", (pos, zone) => { factory.GenerateNode(-1001, zone, pos); }, null);
            system.RegisterZoneMenu("特殊/循环入口", (pos, zone) => { factory.GenerateNode(-2001, zone, pos); }, null);
            system.RegisterZoneMenu("特殊/循环出口", (pos, zone) => { factory.GenerateNode(-2002, zone, pos); }, null);
            system.RegisterZoneMenu("测试/扳机", (pos, zone) => { factory.GenerateNode(-1, zone, pos); }, null);
            system.RegisterZoneMenu("测试/测试节点", (pos, zone) => { factory.GenerateNode(1, zone, pos); }, null);
            system.RegisterZoneMenu("比较/大于", (pos, zone) => { factory.GenerateNode(2, zone, pos); }, (pos, _) => { return pos.x > 100; });
            system.RegisterZoneMenu("删除", (_, zone) => { this.SendCommand(new DestroyZoneCommand(zone)); },null) ;
        }

        protected override void InitNodeMenu(IMenuSystemInit system, INodeFactorySystem factory)
        {
            system.RegisterNodeMenu("删除", (_, node) => { this.SendCommand(new DestroyNodeCommand(node)); },(_,node)=>node.SpecialType==E_SpecialNode.None);
        }
    }
}
