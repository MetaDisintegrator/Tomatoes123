using Game.Battle.Chess;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.System
{
    public class Chesses
    {
        List<BattleChess> activeChessList;
        List<BattleChess> toRemoveChessList;

        public void ReceiveVisitor(ChessesVisitor visitor)
        {
            foreach (var chess in activeChessList)
            { 
                visitor.Visit(chess);
            }
        }
        public void Add(BattleChess chess)
        { 
            activeChessList.Add(chess);
        }
        public Chesses()
        { 
            activeChessList = new List<BattleChess>();
            toRemoveChessList = new List<BattleChess>();
            
        }
    }
    /// <summary>
    /// 战场棋子访问器
    /// 使用对象池管理，使用完结果后归还
    /// </summary>
    public class ChessesVisitor
    {
        static SimpleObjectPool<ChessesVisitor> pool;
        public List<BattleChess> Result { get; set; }
        public Func<BattleChess, bool> Condition { get; set; }

        public void Visit(BattleChess activeChess)
        {
            if (Condition(activeChess))
            { 
                Result.Add(activeChess);
            }
        }

        #region 创建和回收
        public ChessesVisitor()
        { 
            Result = ListPool<BattleChess>.Get();
        }
        static ChessesVisitor()
        {
            pool = new SimpleObjectPool<ChessesVisitor>(
                () => new ChessesVisitor(),
                visitor => visitor.Result.Clear()
                );
        }
        public static ChessesVisitor Get(Func<BattleChess,bool> condition)
        { 
            ChessesVisitor res = pool.Allocate();
            res.Condition = condition;
            return res;
        }
        ~ChessesVisitor() 
        {
            ListPool<BattleChess>.Release(Result);
        }
        public void Recycle()
        {
            pool.Recycle(this);
        }
        #endregion
    }
}

