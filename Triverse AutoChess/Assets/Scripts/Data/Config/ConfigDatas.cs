using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enum.Main;
using System.Linq;

namespace Game.Data.Config
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
        public float EvationRate { get; }
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
        public int lvBelong;

        public float MaxHealth { get; set; }

        public float PhysicalResistance { get; set; }

        public float MagicalResistance { get; set; }

        public float Attack { get; set; }

        public float AttackRate { get; set; }

        public float AttackRange { get; set; }

        public float CritRate { get; set; }

        public float CritMultiplier { get; set; }

        public float EvationRate { get; set; }
    }

    public class SkillConfigData : ISkillConfigData
    {
        public int id;

        public string Name { get; set; }

        public string Description { get; set; }

        public float CoolDown { get; set; }

        public string ResPath { get; set; }

        public string SOPath { get; set; }
    }

    public class WarpSkillConfigData : SkillConfigData, ISkillConfigData
    {
        public float randWeight;
    }

    public class AvatarConfigData : ICharacterConfigData
    {
        public AttrConfigData attrConfig;
        public int Id;
        public float randWeight;

        public E_Realm Realm { get; set; }
        public E_ChessRole defaultRole { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ResPath { get; set; }
        /// <summary>
        /// 读写只存ID
        /// </summary>
        public List<ISkillConfigData> Skills { get; set; }


        public AttrConfigData GetAttrConfig(int lv = -1)
        {
            return attrConfig;
        }
    }

    public class TribeConfigData : ITribeConfigData
    {
        public int id;

        public Dictionary<E_Realm, (List<(float randWeight, int id)> rand,float range)> avatarRand;
        /// <summary>
        /// 读写时data部分只存ID
        /// </summary>
        public List<(float randWeight, ISkillConfigData data)> warpSkillRand;
        public float warpSkillRandRange;

        public string Name { get; set; }

        public string Description { get; set; }

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
            ISkillConfigData res = warpSkillRand.Last().data;
            foreach (var data in warpSkillRand)
            {
                rand -= data.randWeight;
                if (rand < 0)
                {
                    res = data.data;
                    break;
                }
            }
            return res;
        }
    }

    public class IndividualConfigData : ICharacterConfigData
    {
        public int id;

        public List<AttrConfigData> attrConfigs;

        public E_Realm Realm => E_Realm.None;

        public E_ChessRole defaultRole { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ResPath { get; set; }
        /// <summary>
        /// 读写只存ID
        /// </summary>
        public List<ISkillConfigData> Skills { get; set; }

        public AttrConfigData GetAttrConfig(int lv = -1)
        {
            return attrConfigs[lv-1];
        }
    }
    
    public class GearConfigData : IGearConfigData
    {
        public int id;

        public List<AttrConfigData> attrConfigs;
        public string Name { get; set;}
        public string Description { get; set;}
        public string ResPath { get; set;}
        public string SOPath { get; set;}
        public AttrConfigData GetAttrConfig(int lv = -1)
        { 
            return attrConfigs[lv-1];
        }
        /// <summary>
        /// 读写只存ID
        /// </summary>
        public List<ISkillConfigData> Skills { get; set; }
    }
}