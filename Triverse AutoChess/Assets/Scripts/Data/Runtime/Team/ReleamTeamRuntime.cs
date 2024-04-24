using Game.Data.Profile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Runtime
{
    public interface IReleamTeamRuntimeData
    { 
        
    }
    public class ReleamTeamRuntimeData : IReleamTeamRuntimeData
    {
        Dictionary<int, CharacterProfileData> members;
    }
}

