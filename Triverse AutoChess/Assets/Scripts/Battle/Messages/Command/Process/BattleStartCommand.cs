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
            //���ӻ
            this.SendCommand<ChessStartActCommand>();
            //�������
            this.GetSystem<IBattleManageSystem>().BattleStart();
        }
    }
}

