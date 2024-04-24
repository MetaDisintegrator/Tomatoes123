using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Node
{
    public interface INodeSender
    {
        public List<Pos> TargetPoints { get; }
        void Send(INodeReceiver dataContainer);
    }
    public interface INodeSender<TValue> : INodeSender
    { 
        void Send(TValue value);
    }
    public abstract class AbstractNodeSender<TValue> : INodeSender<TValue>
    {
        public List<Pos> TargetPoints { get; }

        public abstract void Send(TValue value);
        public abstract void Send(INodeReceiver dataContainer);
    }
}

