using Game.Battle.Chess.Data;
using Game.Battle.Chess.Event;
using Game.Battle.Chess.Move;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.AI
{
    public interface IChessNavSystem : ISystem
    { 
        List<BattlePoint> Route { get; }
        /// <summary>
        /// 寻找目标，返回是否找到
        /// </summary>
        /// <param name="alternatives"></param>
        /// <param name="targetBuffer"></param>
        /// <returns></returns>
        bool FindTarget(List<BattleChess> alternatives,out BattleChess targetBuffer);
    }
    public class ChessNavSystem : AbstractSystem, IChessNavSystem
    {
        bool updated;
        List<BattlePoint> _route;
        public List<BattlePoint> Route
        {
            get
            {
                CheckPathUpdate();
                return _route;
            }
        }

        private void CheckPathUpdate()
        {
            if(updated) return;
            //准备
            _route.Clear();
            IBattleChessDataModel data = this.GetModel<IBattleChessDataModel>();
            //寻路
            bool reachable = this.GetUtility<IChessNavUtility>().DoNav(
                this.GetSystem<IBattleFieldDataSystem>().Battlefield,
                this.GetSystem<IChessMoveSystem>().GridPosition,
                data.Target,
                data.Profile.DynamicAttr.AttackRange,
                _route);
            if(!reachable) 
                this.SendEvent<EventNavUnreachable>();
        }
        public bool FindTarget(List<BattleChess> alternatives,out BattleChess targetBuffer)
        {
            //准备
            targetBuffer = null;
            if (alternatives.Count == 0) return false;
            ResetPath();
            IBattleChessDataModel data = this.GetModel<IBattleChessDataModel>();
            IChessNavUtility nav = this.GetUtility<IChessNavUtility>();
            Battlefield ownerField = this.GetSystem<IBattleFieldDataSystem>().Battlefield;
            Pos startPos = this.GetSystem<IChessMoveSystem>().GridPosition;
            float range = data.Profile.DynamicAttr.AttackRange;
            //寻路并比较
            BattleChess target = null;
            BattleChess closestTarget = null;
            List<BattlePoint> shortest = null;
            List<BattlePoint> closest = null;
            foreach (var chess in alternatives)
            {
                List<BattlePoint> temp = ListPool<BattlePoint>.Get();
                if (!nav.DoNav(ownerField, startPos, chess, range, temp))//不可到达
                {
                    if (CalShortestRoute(temp, ref closest))
                        closestTarget = chess;
                }
                else
                { 
                    if(CalShortestRoute(temp, ref shortest))
                        target = chess;
                }
            }
            //赋值
            _route = shortest??closest;
            targetBuffer = target ?? closestTarget;
            return true;
                
            bool CalShortestRoute(List<BattlePoint> temp,ref List<BattlePoint> history)
            {
                if (history == null)
                {
                    history = temp;
                    return true;
                }
                else if(temp.Count<history.Count)
                {
                    ListPool<BattlePoint>.Release(history);
                    history = temp;
                    return true;
                }
                else
                    ListPool<BattlePoint>.Release(temp);
                return false;
            }
        }
        private void ResetPath()
        { 
            if(_route==null) return;
            _route.Clear();
        }

        protected override void OnInit()
        {
            this.RegisterEvent<EventLateChessInit>(_ =>
            {
                this.GetSystem<IChessMonoSystem>().RegisterUpdate(Update);
            });
        }
        protected override void OnDeinit()
        {
            base.OnDeinit();
            this.GetSystem<IChessMonoSystem>().UnregisterUpdate(Update);
        }

        private void Update() => updated = false;
    }
}
