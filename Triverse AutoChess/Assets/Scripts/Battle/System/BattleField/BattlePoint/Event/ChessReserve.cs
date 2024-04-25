using Game.Battle.Chess;
using QFramework;
using QFramework.MyCustomExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.BattlePointEvent
{
    public struct EventChessReserve
    {
        public BattleChess chess;
        public EventChessReserve(BattleChess chess)
        { 
            this .chess = chess;
        }
    }
}

