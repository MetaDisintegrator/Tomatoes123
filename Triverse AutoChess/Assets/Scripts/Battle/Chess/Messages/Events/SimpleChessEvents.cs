using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.Event
{
    public struct EventChessInit
    {
        public GameObject Chess { get; private set; }

        public EventChessInit(GameObject chess)
        {
            this.Chess = chess;
        }
    }

    public struct EventLateChessInit
    {
    }

    public struct EventChessStartAct
    { }
}

