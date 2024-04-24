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
            //position
            Vector3 pos = this.GetSystem<IBattlefieldSystem>().GetBattlefield(realm).GetPoint(gridPos).position;
            chess.Transform.Position(pos);
            //Æå×Ó¼ÇÂ¼gridÎ»ÖÃ
            chess.GridPosition = gridPos;
        }
        public PlaceChessCommand(Pos gridPos, E_Realm realm, BattleChess chess)
        {
            this.gridPos = gridPos;
            this.realm = realm;
            this.chess = chess;
        }
    }
}

