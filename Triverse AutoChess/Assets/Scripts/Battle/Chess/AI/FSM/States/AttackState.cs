using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.AI
{
    public class AttackState : AbstractState<E_ChessState, IChessFSMSystem>
    {
        public AttackState(FSM<E_ChessState> fsm, IChessFSMSystem target) : base(fsm, target)
        {
        }
    }
}