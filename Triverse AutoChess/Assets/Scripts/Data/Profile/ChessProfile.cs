using Game.Data.Config;
using Game.Data.Info;
using Game.Enum.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Profile
{
    public interface IChessProfileData
    {
        //配置信息
        public IChessConfigData Config { get; }
        //最终计算属性
        public AttrData SteadyAttr { get; }
        //状态属性
        public AttrData DynamicAttr { get; }
        //自定义变量
        public float this[string key] { get; set; }
        //装备
        public List<IGearProfileData> Gears { get; set; }
    }
    public interface ICharacterProfileData : IChessProfileData
    {
        public int TeamID { get; }
    }
    public interface IIndividualProfileData : IChessProfileData 
    {
        public int Lv { get; }
    }
    public abstract class ChessProfileData : IChessProfileData
    {
        Dictionary<string, float> customAttrs;
        public float this[string key]
        {
            get
            {
                return customAttrs.TryGetValue(key, out float res) ? res : 0f;
            }
            set
            {
                if (!customAttrs.TryAdd(key, value))
                    customAttrs[key] = value;
            }
        }

        public List<IGearProfileData> Gears { get; set; }

        public IChessConfigData Config { get; set; }
        public AttrData SteadyAttr { get; set; }

        public AttrData DynamicAttr { get; set; }
    }
    public class CharacterProfileData : ChessProfileData, ICharacterProfileData
    {
        public int TeamID { get; set; }
    }
    public class IndividualProfileData : ChessProfileData, IIndividualProfileData
    {
        public int Lv { get; set; }
    }
}

