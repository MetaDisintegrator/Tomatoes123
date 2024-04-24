using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.AI
{
    public class RigidState : AbstractState<E_ChessState, IChessFSMSystem>
    {
        public RigidState(FSM<E_ChessState> fsm, IChessFSMSystem target) : base(fsm, target)
        {
        }
    }
}
