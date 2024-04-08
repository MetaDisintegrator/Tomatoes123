using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enum.Chess;
using Game.Enum.Main;
using System.Linq;

namespace Game.Enum.Main
{
    public enum E_Realm
    {
        None = 0,
        Arcane = 1,
        PawVerse = 2,
        Technosphere = 3,
    }
}
namespace Game.Enum.Chess
{
    public enum E_ChessRole
    { 
        None = 0,
        Tank = 1,
        DPS = 2,
        Assassin = 3,
    }
}
namespace Game.Data.Chess
{
    /// <summary>
    /// 个体或化身配置
    /// </summary>
    public interface ICharacterConfigData
    {
        public E_Realm Realm { get; }
        public E_ChessRole defaultRole { get; }
        public string Name { get; }
        public string Description { get; }
        AttrConfigData GetAttrConfig(int lv = -1);
        public string ResPath { get; }
        public List<ISkillConfigData> Skills { get; }
    }
    /// <summary>
    /// 族配置
    /// </summary>
    public interface ITribeConfigData
    {
        /// <summary>
        /// 族名也是化身配置路径
        /// </summary>
        public string Name { get; }
        public string Description { get; }
        ICharacterConfigData GetAvatar(int tribeId);
        ICharacterConfigData RandAvatar(E_Realm realm);
        ISkillConfigData RandWarpSkill();
    }
    /// <summary>
    /// 技能配置
    /// </summary>
    public interface ISkillConfigData
    {
        public string Name { get; }
        public string Description { get; }
        public float CoolDown { get; }
        /// <summary>
        /// 技能图标，可无
        /// </summary>
        public string ResPath { get; }
        public string SOPath { get; }
    }
    /// <summary>
    /// 属性配置
    /// </summary>
    public interface IAttrConfigData
    {
        public float MaxHealth { get; }
        public float PhysicalResistance { get; }
        public float MagicalResistance { get; }
        public float Attack { get; }
        public float AttackRate { get; }
        public float AttackRange { get; }
        public float CritRate { get; }
        public float CritMultiplier { get; }
    }
    /// <summary>
    /// 装备配置
    /// </summary>
    public interface IGearConfigData
    {
        public string Name { get; }
        public string Description { get; }
        public string ResPath { get; }
        public string SOPath { get; }
        AttrConfigData GetAttrConfig(int lv = -1);
        public List<ISkillConfigData> Skills { get; }
    }

    public class AttrConfigData : IAttrConfigData
    {
        public float MaxHealth { get; protected set; }

        public float PhysicalResistance { get; protected set; }

        public float MagicalResistance { get; protected set; }

        public float Attack { get; protected set; }

        public float AttackRate { get; protected set; }

        public float AttackRange { get; protected set; }

        public float CritRate { get; protected set; }

        public float CritMultiplier { get; protected set; }
    }

    public class SkillConfigData : ISkillConfigData
    {
        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public float CoolDown { get; protected set; }

        public string ResPath { get; protected set; }

        public string SOPath { get; protected set; }
    }

    public class AvatarConfigData : ICharacterConfigData
    {
        public AttrConfigData attrConfig;

        public E_Realm Realm { get; protected set; }
        public E_ChessRole defaultRole { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string ResPath { get; protected set; }
        public List<ISkillConfigData> Skills { get; protected set; }

        public AttrConfigData GetAttrConfig(int lv = -1)
        {
            return attrConfig;
        }
    }

    public class TribeConfigData : ITribeConfigData
    {
        public Dictionary<int, ICharacterConfigData> avatars;
        public Dictionary<E_Realm, (List<(float randWeight, int id)> rand,float range)> avatarRand;

        public Dictionary<int, ISkillConfigData> warpSkills;
        public List<(float randWeight, int id)> warpSkillRand;
        public float warpSkillRandRange;

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public ICharacterConfigData GetAvatar(int tribeId)
        {
            //加载，返回
            return default;
        }
        public ICharacterConfigData RandAvatar(E_Realm realm)
        {
            float rand = Random.Range(0, avatarRand[realm].range);
            int id = avatarRand[realm].rand.Last().id;
            foreach (var data in avatarRand[realm].rand)
            {
                rand -= data.randWeight;
                if (rand < 0) 
                {
                    id = data.id;
                    break;
                }
            }
            //加载，返回
            return default;
        }

        public ISkillConfigData RandWarpSkill()
        {
            float rand = Random.Range(0, warpSkillRandRange);
            int id = warpSkillRand.Last().id;
            foreach (var data in warpSkillRand)
            {
                rand -= data.randWeight;
                if (rand < 0)
                {
                    id = data.id;
                    break;
                }
            }
            //加载，返回
            return default;
        }
    }

    public class IndividualConfigData : ICharacterConfigData
    {
        public List<AttrConfigData> attrConfigs;

        public E_Realm Realm => E_Realm.None;

        public E_ChessRole defaultRole { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public string ResPath { get; protected set; }

        public List<ISkillConfigData> Skills { get; protected set; }

        public AttrConfigData GetAttrConfig(int lv = -1)
        {
            return attrConfigs[lv-1];
        }
    }
    
    public class GearConfigData : IGearConfigData
    {
        public List<AttrConfigData> attrConfigs;
        public string Name { get; }
        public string Description { get; }
        public string ResPath { get; }
        public string SOPath { get; }
        public AttrConfigData GetAttrConfig(int lv = -1)
        { 
            return attrConfigs[lv-1];
        }
        public List<ISkillConfigData> Skills { get; }
    }
}