using Game.Battle.Chess.Event;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.AI
{
    public enum E_ChessState
    {
        None = 0,
        Idle,
        Chase,
        Attack,
        Rigid,
        Dead
    }
    public interface IChessFSMSystem : ISystem
    { 
    
    }
    public class ChessFSMSystem : AbstractSystem, IChessFSMSystem
    {
        FSM<E_ChessState> FSM { get; set; }

        protected override void OnInit()
        {
            FSM = new FSM<E_ChessState>();
            FSM.AddState(E_ChessState.Idle, new IdleState(FSM, this));
            FSM.AddState(E_ChessState.Chase, new ChaseState(FSM, this));
            FSM.AddState(E_ChessState.Attack, new AttackState(FSM, this));
            FSM.AddState(E_ChessState.Rigid, new RigidState(FSM, this));
            FSM.AddState(E_ChessState.Dead, new DeadState(FSM, this));

            FSM.StartState(E_ChessState.Idle);

            FSM.OnStateChanged((last, current) => { Debug.Log($"{last}=>{current}"); });

            this.RegisterEvent<EventLateChessInit>(_ => 
            {
                IChessMonoSystem mono = this.GetSystem<IChessMonoSystem>();
                mono.RegisterUpdate(() => FSM.Update());
            });
        }

        protected override void OnDeinit()
        {
            base.OnDeinit();
            IChessMonoSystem mono = this.GetSystem<IChessMonoSystem>();
            mono.UnregisterUpdate(() => FSM.Update());
        }
    }
}
