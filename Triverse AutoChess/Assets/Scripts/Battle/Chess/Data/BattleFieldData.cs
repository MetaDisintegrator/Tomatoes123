using Game.Data.Profile;
using Game.Data;
using Game.Enum.Main;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Battle.System;

namespace Game.Battle.Chess.Data
{
    public interface IBattleFieldDataSystem : ISystem
    {
        Battlefield Battlefield { get; }
        IChessSystem ChessSystem { get; }
    }
    public class BattleFieldDataSystem : AbstractSystem, IBattleFieldDataSystem
    {
        public Battlefield Battlefield => GetGameSystem<IBattlefieldSystem>().GetBattlefield(this.GetModel<IBattleChessDataModel>().Realm);
        public IChessSystem ChessSystem => GetGameSystem<IChessSystem>();

        private T GetGameSystem<T>() where T : class, ISystem => GameArchitecture.Interface.GetSystem<T>();
        protected override void OnInit()
        {
        }
    }
}
