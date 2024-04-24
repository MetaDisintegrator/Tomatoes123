using Game.Battle.Chess;
using Game.Battle.Chess.Event;
using Game.Battle.System;
using Game.Enum.Main;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Command.Chess
{
    public class ChessStartActCommand : AbstractCommand, ICommand
    {
        protected override void OnExecute()
        {
            DoStartAct(E_Realm.Arcane);
            DoStartAct(E_Realm.PawVerse);
            DoStartAct(E_Realm.Technosphere);
        }

        private void DoStartAct(E_Realm realm)
        {
            List<BattleChess> chesses = ListPool<BattleChess>.Get();
            this.GetSystem<IChessSystem>().SendVisitor(realm, null,chesses);
            foreach (var chess in chesses) 
            {
                chess.SendEvent<EventChessStartAct>();
            }
            ListPool<BattleChess>.Release(chesses);
        }
    }
}

