using Game.Battle.Chess.Event;
using Game.Battle.Chess;
using Game.Enum.Main;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.System
{
    public interface IBattleManageSystem : ISystem
    {
        void BattleStart();
        void BattleEnd();
    }
    public class BattleManageSystem : AbstractSystem, IBattleManageSystem
    {
        protected override void OnInit()
        {
        }

        public void BattleStart()
        {
            //controller = ActionKit
            //    .Repeat()
            //    .Delay(moveTurnDuration,()=>UpdateMoveTurn(E_BattleSide.Enemy))
            //    .Delay(moveTurnDuration,()=>UpdateMoveTurn(E_BattleSide.Player))
            //    .StartGlobal();
        }

        public void BattleEnd()
        { 
            //controller.Deinit();
            //controller = null;
        }

        //private void UpdateMoveTurn(E_BattleSide side)
        //{
        //    DoExecute(E_Realm.Arcane);
        //    DoExecute(E_Realm.PawVerse);
        //    DoExecute(E_Realm.Technosphere);

        //    void DoExecute(E_Realm realm)
        //    {
        //        List<BattleChess> chesses = ListPool<BattleChess>.Get();
        //        this.GetSystem<IChessSystem>().SendVisitor(realm, chess => chess.Side == side, chesses);
        //        foreach (var chess in chesses)
        //        {
        //            chess.SendEvent<EventMoveTurn>();
        //        }
        //    }
        //}
    }
}

