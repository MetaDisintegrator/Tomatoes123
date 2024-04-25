using Game.Tool;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Game.Battle.Chess.AI
{
    public interface IChessNavUtility : IUtility
    {
        /// <summary>
        /// 寻路，返回是否可以到达
        /// </summary>
        /// <param name="field"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="Range"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        bool DoNav(Battlefield field, Pos start, BattleChess targetChess, float Range, List<BattlePoint> buffer);
    }
    public class ChessNavUtility : IChessNavUtility
    {

        public bool DoNav(Battlefield field, Pos start, BattleChess targetChess, float Range, List<BattlePoint> buffer)
        {
            LinkedList<NavNode> visit = new LinkedList<NavNode>();
            List<BattlePoint> visitPoints = ListPool<BattlePoint>.Get();
            List<BattlePoint> targets = ListPool<BattlePoint>.Get();
            BattlePoint startPoint = field.GetPoint(start);
            NavNode visitNearest = null;
            //边界条件(无目标)
            if (targetChess == null) return false;
            //边界条件(无需移动)
            BattlePoint endPoint = field.GetPoint(targetChess.NavPosition);
            if (Vector3.Distance(startPoint.position, endPoint.position) <= Range) return true;
            //边界条件(无目标点)
            field.FindByDis(endPoint, Range, targets, p => p.CanMoveOn);
            if (targets.Count == 0) return false;
            //初始点
            visit.AddFirst(NavNode.Get(0, startPoint, targets));
            visitPoints.Add(startPoint);
            //循环查找
            bool res;
            while (true)
            {
                //若待处理已经为空，不可到达
                if (visit.Count == 0)
                {
                    //返回历史最近路径
                    BuildRoute(visitNearest);
                    res = false;
                    break;
                }
                //查找最近
                NavNode nearest = visit.First.Value;
                visit.Remove(nearest);
                //保存历史最近
                if(visitNearest==null||nearest.fRemain<visitNearest.fRemain)
                    visitNearest = nearest;
                //若已到达，则构建路径
                if (nearest.remain == 0)
                {
                    BuildRoute(nearest);
                    res = true;
                    break;
                }
                //查找下一步
                List<BattlePoint> newConsiders = ListPool<BattlePoint>.Get();
                field.FindBySide(nearest.point, newConsiders, p => p.CanMoveOn);
                //添加到待处理
                foreach (var p in newConsiders)
                {
                    AddVisit(NavNode.Get(nearest.already + 1, p, targets, nearest));
                }
                ListPool<BattlePoint>.Release(newConsiders);
            }
            ListPool<BattlePoint>.Release(visitPoints);
            ListPool<BattlePoint>.Release(targets);
            return res;

            void AddVisit(NavNode nav)
            {
                //确认为新点
                if (visitPoints.Contains(nav.point)) return;
                //添加到访问过
                visitPoints.Add(nav.point);
                //有序添加到待处理
                LinkedListNode<NavNode> node = visit.First;
                while (node != null) 
                {
                    if (SortVisit(nav,node.Value))
                    {
                        visit.AddBefore(node, nav);
                        break;
                    }
                    node = node.Next;
                }
                if (node == null)
                    visit.AddLast(nav);
            }
            bool SortVisit(NavNode low,NavNode high)
            { 
                if(low.Distance<high.Distance)
                    return true;
                else if(low.Distance==high.Distance && low.fRemain<high.fRemain)
                    return true;
                return false;
            }
            void BuildRoute(NavNode last)
            { 
                NavNode cur = last;
                while (cur != null)
                {
                    buffer.Add(cur.point);
                    cur = cur.parent;
                }
                buffer.Reverse();
            }
        }

        class NavNode
        {
            public NavNode parent;
            public BattlePoint point;
            public int already;
            public int remain;
            public float fRemain;
            public int Distance => already + remain;

            #region 分配和回收
            static SimpleObjectPool<NavNode> pool;
            static NavNode()
            {
                pool = new SimpleObjectPool<NavNode>(
                    () => new NavNode());
            }
            public static NavNode Get(int already,BattlePoint point,List<BattlePoint> targets, NavNode parent = null)
            {
                NavNode res = pool.Allocate();
                res.already = already;
                res.point = point;
                res.parent = parent;
                BattlePoint nearest = targets.FindNearest(point, p => p.CanMoveOn);
                res.remain = Pos.ManhattanDis(nearest.gridPosition, point.gridPosition);
                res.fRemain = Vector3.Distance(nearest.position, point.position);
                return res;
            }
            public void Recycle() => pool.Recycle(this);
            #endregion
        }
    }
}

