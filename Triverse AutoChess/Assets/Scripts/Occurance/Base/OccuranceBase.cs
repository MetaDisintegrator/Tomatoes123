using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IOccurance : ISystem
    {
        void Enter();
        void Finish();
    }
    public abstract class AbstractOccurance : AbstractSystem, IOccurance
    {
        public void Enter() 
        {
            OnEnter();
        }
        public void Finish() 
        {
            OnFinish();
        }

        protected abstract void OnEnter();
        protected abstract void OnFinish();
    }
}

