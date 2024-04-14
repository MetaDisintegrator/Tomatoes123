using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface IConnectSystem : ISystem
    {
        public IPoint Selecting { get; set;  }
        void Connect(OutPoint start, InPoint end);
        void DisConnect(InPoint end);
    }
    public class ConnectSystem : AbstractSystem, IConnectSystem

    {
        public IPoint Selecting { get; set;  }

        public void Connect(OutPoint start, InPoint end)
        {
            start.Connect(end);
            end.Connect(start);
            end.OwnerNode.OnRecv(start.OwnerNode);
            this.GetModel<IConnectModel>().Connects.Add(end,new Connect(start, end));
        }
        public void DisConnect(InPoint end)
        {
            Dictionary<InPoint, IConnect> dic = this.GetModel<IConnectModel>().Connects;
            if (!dic.ContainsKey(end)) return;
            OutPoint start = dic[end].Start;
            end.Disconnect(start);
            start.Disconnect(end);
            end.OwnerNode.OnDisRecv(start.OwnerNode);
            dic.Remove(end);
        }

        protected override void OnInit()
        {
            Selecting = null;
        }
    }
}
