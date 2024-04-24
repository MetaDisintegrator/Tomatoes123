using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.AI
{
    public class IdleState : AbstractState<E_ChessState, BattleChess>
    {
        public IdleState(FSM<E_ChessState> fsm, BattleChess target) : base(fsm, target)
        {
        }
    }
}

