using Game.Battle.Chess.Query;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QFramework.MyCustomExtension
{
    public abstract class PoolQuery<T, TValue> : AbstractQuery<TValue> where T : PoolQuery<T, TValue>, new()
    {
        static SimpleObjectPool<T> pool;
        static PoolQuery()
        {
            pool = new SimpleObjectPool<T>(
                () => new T());
        }
        protected override TValue OnDo()
        {
            ActionKit.Coroutine(Recycle).StartGlobal();
            return OnDoQuery();
        }
        protected abstract TValue OnDoQuery();
        public static T Get()
        {
            return pool.Allocate();
        }
        IEnumerator Recycle()
        {
            yield return 0;
            pool.Recycle(this as T);
        }
    }

    public abstract class PoolCommand<T> : AbstractCommand where T : PoolCommand<T> ,new()
    {
        static SimpleObjectPool<T> pool;
        static PoolCommand()
        {
            pool = new SimpleObjectPool<T>(
                () => new T());
        }
        protected override void OnExecute()
        {
            OnExecuteCommand();
            Recycle();
        }
        protected abstract void OnExecuteCommand();

        public static T Get()
        {
            return pool.Allocate();
        }
        IEnumerator Recycle()
        {
            yield return 0;
            pool.Recycle(this as T);
        }
    }
}



