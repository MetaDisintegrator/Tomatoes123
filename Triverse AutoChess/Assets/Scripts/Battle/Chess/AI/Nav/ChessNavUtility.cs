using Game.Tool;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Battle.Chess.AI
{
    public interface IChessNavUtility : IUtility
    {
        void DoNav(Battlefield field, Pos start, Pos end, float Range, List<BattlePoint> buffer);
    }
    public class ChessNavUtility : IChessNavUtility
    {
        
        public void DoNav(Battlefield field,Pos start, Pos end, float Range, List<BattlePoint> buffer)
        {
            LinkedList<NavNode> visit = new LinkedList<NavNode>();
            List<NavNode> done = ListPool<NavNode>.Get();
            List<BattlePoint> targets = ListPool<BattlePoint>.Get();
            
            BattlePoint startPoint = field.GetPoint(start);
            //边界条件(无需移动)
            BattlePoint endPoint = field.GetPoint(end);
            if (Vector3.Distance(startPoint.position, endPoint.position) <= Range) return;
            //初始条件
            field.FindByDis(startPoint, Range, targets, p => p.CanMoveOn);
            visit.AddFirst(NavNode.Get(0,CalRemain(startPoint), startPoint));
            //循环查找
            while (true)
            {
                //查找最近
                NavNode nearest = visit.First.Value;
                //查找下一步
                List<BattlePoint> newConsiders = ListPool<BattlePoint>.Get();
                field.FindBySide(nearest.point, newConsiders, p => p.CanMoveOn);
            }

            int CalRemain(BattlePoint point)
            {
                BattlePoint nearest = targets.FindNearest(point,p=>p.CanMoveOn);
                return Pos.ManhattanDis(nearest.gridPosition, point.gridPosition);
            }
        }

        class NavNode
        {
            public NavNode parent;
            public BattlePoint point;
            public int already;
            public int remain;
            public int Distance => already + remain;

            #region 分配和回收
            static SimpleObjectPool<NavNode> pool;
            static NavNode()
            {
                pool = new SimpleObjectPool<NavNode>(
                    () => new NavNode());
            }
            public static NavNode Get(int already, int remain, BattlePoint point, NavNode parent = null)
            {
                NavNode res = pool.Allocate();
                res.already = already;
                res.remain = remain;
                res.point = point;
                res.parent = parent;
                return res;
            }
            public void Recycle() => pool.Recycle(this);
            #endregion
        }
    }
}

