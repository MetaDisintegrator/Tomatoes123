using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Runtime
{
    public interface IBattleOccuranceRuntimeData : IModel
    { 
    
    }
    public class BattleOccuranceRuntime : AbstractModel, IBattleOccuranceRuntimeData
    {
        public bool finished;
        Battlearray enemyBattlearray;
        protected override void OnInit()
        {
            
        }
    }
}

