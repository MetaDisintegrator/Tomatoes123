using Game.Data.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Profile
{
    public interface IGearProfile
    {
        float this[string key] { get; set; }
        int Lv { get; }
        IGearConfigData GearConfig { get; }
        List<ISkillConfigData> SkillConfigDatas { get; }
    }
}
