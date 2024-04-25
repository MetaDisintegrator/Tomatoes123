using Game.Battle.Command.Chess;
using Game.Battle.System;
using QFramework;
using QFramework.MyCustomExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Command
{
    public class BattleStartCommand : PoolCommand<BattleStartCommand>, ICommand
    {
        protected override void OnExecuteCommand()
        {
            //棋子活动
            this.SendCommand<ChessStartActCommand>();
            //管理器活动
            this.GetSystem<IBattleManageSystem>().BattleStart();
        }
    }
}

