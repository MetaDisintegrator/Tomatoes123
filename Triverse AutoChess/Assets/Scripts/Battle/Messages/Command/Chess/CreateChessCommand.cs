using Game.Battle.Chess;
using Game.Battle.System;
using Game.Data.Profile;
using Game.Enum.Main;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Command.Chess
{
    public class CreateChessCommand : AbstractCommand<BattleChess>, ICommand<BattleChess>
    {
        IChessProfileData profile;
        E_Realm realm;
        E_BattleSide side;
        protected override BattleChess OnExecute()
        {
            BattleChess res = this.GetSystem<IChessFactorySystem>().CreateChess(profile, side);
            this.GetSystem<IChessSystem>().AddChess(realm, res);
            return res;
        }
        public CreateChessCommand(IChessProfileData profile, E_Realm realm, E_BattleSide side)
        {
            this.profile = profile;
            this.realm = realm;
            this.side = side;
        }
    }
}

