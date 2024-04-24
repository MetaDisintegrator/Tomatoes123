using Game.Data.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Profile
{
    public interface IGearProfileData
    {
        public float this[string key] { get; set; }
        public int Lv { get; }
        public IGearConfigData GearConfig { get; }
        public List<ISkillConfigData> SkillConfigDatas { get; }
    }

    public class GearProfileData : IGearProfileData
    {
        Dictionary<string, float> customAttrs;
        public float this[string key]
        {
            get
            { 
                return customAttrs.TryGetValue(key,out float res) ? res : 0f;
            }
            set 
            {
                if(!customAttrs.TryAdd(key, value))
                    customAttrs[key] = value;
            }
        }
        public int Lv { get; }
        public IGearConfigData GearConfig { get; }
        public List<ISkillConfigData> SkillConfigDatas { get; }

        
    }
}
