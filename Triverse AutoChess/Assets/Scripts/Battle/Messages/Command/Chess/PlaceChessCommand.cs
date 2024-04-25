using Game.Battle.BattlePointEvent;
using Game.Battle.Chess;
using Game.Enum.Main;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Command.Chess
{
    public class PlaceChessCommand : AbstractCommand, ICommand
    {
        Pos gridPos;
        E_Realm realm;
        BattleChess chess;

        protected override void OnExecute()
        {
            BattlePoint point = this.GetSystem<IBattlefieldSystem>().GetBattlefield(realm).GetPoint(gridPos);
            //position
            Vector3 pos = point.position;
            chess.Transform.Position(pos);
            //棋子记录grid位置
            chess.GridPosition = gridPos;
            //事件
            point.SendEvent(new EventChessCome(chess));
        }
        public PlaceChessCommand(Pos gridPos, E_Realm realm, BattleChess chess)
        {
            this.gridPos = gridPos;
            this.realm = realm;
            this.chess = chess;
        }
    }
}

