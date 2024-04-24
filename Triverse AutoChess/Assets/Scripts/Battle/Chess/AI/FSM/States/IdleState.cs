using Game.Battle.Chess.Event;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.AI
{
    public class IdleState : AbstractState<E_ChessState, IChessFSMSystem>
    {
        public IdleState(FSM<E_ChessState> fsm, IChessFSMSystem target) : base(fsm, target)
        {
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            //Debug.Log("1");
            mTarget.RegisterEvent<EventChessStartAct>(StartAct);
        }

        protected override void OnExit()
        {
            base.OnExit();
            mTarget.UnRegisterEvent<EventChessStartAct>(StartAct);
        }

        private void StartAct(EventChessStartAct e) => mFSM.ChangeState(E_ChessState.Chase);
    }
}

