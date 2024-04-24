using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Node
{
    public interface INodeReceiver
    { 
        public bool IsConst { get; }
        /// <summary>
        /// 拷贝自身，以通过直接赋值Receiver的方法传递数据
        /// </summary>
        /// <returns></returns>
        INodeReceiver Self2Data();
    }

    public interface INodeReceiver<TValue> : INodeReceiver
    { 
        public TValue Value { get; }
    }
    public abstract class AbstractNodeReveicer<TValue> : INodeReceiver<TValue>
    {
        public bool IsConst { get; }
        public TValue Value { get; }

        public abstract INodeReceiver Self2Data();
    }
}
