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
        public ChessBehaviour Mono { get; }
        void RegisterUpdate(Action action);
        void UnregisterUpdate(Action action);
        void RegisterDestroy(Action action);
        void UnregisterDestroy(Action action);
    }
    public class ChessMonoSystem : AbstractSystem, IChessMonoSystem
    {
        public ChessBehaviour Mono { get; private set; }

        public void RegisterDestroy(Action action) => Mono.DestroyCallback += action;

        public void RegisterUpdate(Action action) => Mono.UpdateCallback += action;

        public void UnregisterDestroy(Action action) => Mono.DestroyCallback -= action;

        public void UnregisterUpdate(Action action) => Mono.UpdateCallback -= action;

        protected override void OnInit()
        {
            this.RegisterEvent<EventChessInit>(e =>
            {
                Mono = e.Chess.GetOrAddComponent<ChessBehaviour>();
            });
        }
    }
}

