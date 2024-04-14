using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface INodeModel:IModel
    {
        public Dictionary<int, INode> Nodes { get; }
        public EasyEvent Add { get; }
        public EasyEvent Remove { get; }
    }

    public class NodeModel : AbstractModel, INodeModel
    {
        public EasyEvent Add { get; set; }
        public EasyEvent Remove { get; set; }
        public Dictionary<int, INode> Nodes { get; set; }

        protected override void OnInit()
        {
            Nodes = new Dictionary<int, INode>();
        }
    }
}

