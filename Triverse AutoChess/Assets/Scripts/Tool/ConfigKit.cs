using Game.Data.Config;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Tool
{
    public static class ConfigKit
    {
        #region Â·¾¶
        static readonly string ConfigPath = Path.Combine(Application.streamingAssetsPath, "Config");
        static readonly string Tribe_ConfigPath = Path.Combine(ConfigPath, "Tribe");
        static readonly string Avatar_ShareConfigPath = Path.Combine(ConfigPath, "Avatar");
        static readonly string Individual_ConfigPath = Path.Combine(ConfigPath, "Individual");
        static readonly string Gear_ConfigPath = Path.Combine(ConfigPath, "Gear");
        static readonly string WarpSkill_ConfigData = Path.Combine(ConfigPath, "WarpSkill");
        static readonly string Skill_ConfigPath = Path.Combine(ConfigPath, "Skill");
        static readonly string Event_ConfigPath = Path.Combine(ConfigPath, "Event");
        #endregion

        static Dictionary<int, ITribeConfigData> tribes;
        static Dictionary<int, IChessConfigData> individuals;
        static Dictionary<int, IChessConfigData> avatars;
        static Dictionary<int, IGearConfigData> gears;
        static Dictionary<int, ISkillConfigData> warpSkills;
        static Dictionary<int, ISkillConfigData> skills;
        //ÊÂ¼þ TODO
    }

}

