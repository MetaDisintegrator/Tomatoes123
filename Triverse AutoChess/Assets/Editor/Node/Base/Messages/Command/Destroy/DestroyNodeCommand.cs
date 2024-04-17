using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class DestroyNodeCommand : AbstractCommand, ICommand
    {
        INode node;
        protected override void OnExecute()
        {
            node.OwnerZone.RemoveNode(node as Node);
        }

        public DestroyNodeCommand(INode node)
        {
            this.node = node;
        }
        public DestroyNodeCommand(int id) 
        {
            this.node = this.GetModel<INodeModel>().Nodes[id];
        }
    }
}
