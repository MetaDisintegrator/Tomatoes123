using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Editor.NodeEditor.Query
{
    public class NodeListQuery : AbstractQuery<List<INode>>, IQuery<List<INode>>
    {
        protected override List<INode> OnDo()
        {
            return this.GetModel<INodeModel>().Nodes.Values.ToListPooled<INode>();
        }
    }
}
