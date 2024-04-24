using Game.Battle;
using Game.Battle.System;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameArchitecture : Architecture<GameArchitecture>, IArchitecture
    {
        protected override void Init()
        {
            RegisterSystem<IChessFactorySystem>(new ChessFactorySystem());
            RegisterSystem<IChessSystem>(new ChessSystem());
            RegisterSystem<IBattlefieldSystem>(new BattlefieldSystem());

            RegisterUtility<IGetBattlefieldUtility>(new TesterGetBattlefieldUtility());
        }
    }
}

