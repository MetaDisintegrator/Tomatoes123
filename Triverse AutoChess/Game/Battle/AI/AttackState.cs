using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.AI
{
    public class AttackState : AbstractState<E_ChessState, BattleChess>
    {
        public AttackState(FSM<E_ChessState> fsm, BattleChess target) : base(fsm, target)
        {
        }
    }
}