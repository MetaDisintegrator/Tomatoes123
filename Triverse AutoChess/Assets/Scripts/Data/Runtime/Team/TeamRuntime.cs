using Game.Enum.Main;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Runtime
{
    /// <summary>
    /// �����Ա��Ϣ�Ͳ�����Ϣ
    /// </summary>
    public interface ITeamRuntimeData : IModel
    { 
    
    }
    public class TeamRuntimeData : AbstractModel, ITeamRuntimeData
    {
        Dictionary<E_Realm, ReleamTeamRuntimeData> teams;
        Battlearray battlearray;

        protected override void OnInit()
        {
            throw new System.NotImplementedException();
        }
    }
}

