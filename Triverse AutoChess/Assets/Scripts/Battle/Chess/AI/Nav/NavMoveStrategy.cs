using Game.Battle.Chess.AI;
using Game.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.Move
{
    public struct NavMoveStrategy : IMovePoint
    {
        IChessNavSystem nav;
        public BattlePoint CalNextPoint(BattlePoint current)
        {
            List<BattlePoint> route = nav.Route;
            int idx = route.IndexOf(current);
            if (idx>=0&&idx<route.Count-1)
                return route[idx+1];
            return null;
        }

        public NavMoveStrategy(IChessNavSystem nav)
        { 
            this.nav = nav;
        }
    }
}
