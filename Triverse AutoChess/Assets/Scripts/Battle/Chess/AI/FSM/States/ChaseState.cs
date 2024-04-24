using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.AI
{
    public class ChaseState : AbstractState<E_ChessState, IChessFSMSystem>
    {
        public ChaseState(FSM<E_ChessState> fsm, IChessFSMSystem target) : base(fsm, target)
        {
        }
    }
}
