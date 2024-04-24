using Game.Enum.Main;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IGetBattlefieldUtility : IUtility
    {
        BattlePoint[,] Get(E_Realm realm);
    }
}

