using Game.Battle.Chess.Event;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess
{
    public interface IChessMonoSystem : ISystem
    {
        void RegisterUpdate(Action action);
        void UnregisterUpdate(Action action);
        void RegisterDestroy(Action action);
        void UnregisterDestroy(Action action);
    }
    public class ChessMonoSystem : AbstractSystem, IChessMonoSystem
    {
        ChessBehaviour mono;

        public void RegisterDestroy(Action action) => mono.DestroyCallback += action;

        public void RegisterUpdate(Action action) => mono.UpdateCallback += action;

        public void UnregisterDestroy(Action action) => mono.DestroyCallback -= action;

        public void UnregisterUpdate(Action action) => mono.UpdateCallback -= action;

        protected override void OnInit()
        {
            this.RegisterEvent<EventChessInit>(e =>
            {
                mono = e.Chess.GetOrAddComponent<ChessBehaviour>();
            });
        }
    }
}

