using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Info
{
    /// <summary>
    /// 存储着生成一个棋子所必须的信息，用于留存战斗数据
    /// </summary>
    public interface IChessInfo
    {

    }
    /// <summary>
    /// 友方棋子必要信息，通过队内ID识别
    /// </summary>
    public class CharacterChessInfo : IChessInfo
    { 
    
    }
    /// <summary>
    /// 敌方棋子或其他个体必要信息，通过ID和等级等变量识别
    /// </summary>
    public class IndividualChessInfo : IChessInfo
    { 
    }
}
