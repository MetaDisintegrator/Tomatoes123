using Game.Battle.Chess;
using QFramework;
using QFramework.MyCustomExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.BattlePointEvent
{
    public struct EventChessCome
    {
        public BattleChess chess;

        public EventChessCome(BattleChess chess)
        { 
            this.chess=chess;
        }
    }
}
