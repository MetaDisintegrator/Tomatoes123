using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enum.Main
{
    /// <summary>
    /// 界
    /// </summary>
    public enum E_Realm
    {
        None = 0,
        Arcane = 1,
        PawVerse = 2,
        Technosphere = 3,
    }
    /// <summary>
    /// 棋子定位
    /// </summary>
    public enum E_ChessRole
    {
        None = 0,
        Tank = 1,
        DPS = 2,
        Assassin = 3,
    }
    /// <summary>
    /// 事件类型
    /// </summary>
    public enum E_Occurance
    {
        None = 0,
        Battle = 1,
        Shop = 2,
        Inn = 3,
        Unique = 4,
    }
    public enum E_BattleSide
    { 
        None =0,
        Player,
        Enemy
    }
}