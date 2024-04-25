using Game.Battle.Chess;
using QFramework;
using QFramework.MyCustomExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.BattlePointEvent
{
    public struct EventChessLeave
    {
        public BattleChess chess;

        public EventChessLeave(BattleChess chess)
        { 
            this.chess=chess;
        }
    }
}
