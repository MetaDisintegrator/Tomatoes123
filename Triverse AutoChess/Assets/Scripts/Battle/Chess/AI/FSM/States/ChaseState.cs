using Game.Battle.Chess.Data;
using Game.Battle.Chess.Event;
using Game.Battle.Chess.Move;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.AI
{
    public class ChaseState : AbstractState<E_ChessState, IChessFSMSystem>
    {
        IBattleChessDataModel data;
        IChessNavSystem nav;
        IBattleFieldDataSystem battleField;
        IChessMoveSystem move;
        public ChaseState(FSM<E_ChessState> fsm, IChessFSMSystem target) : base(fsm, target)
        {
            data = mTarget.GetModel<IBattleChessDataModel>();
            nav = mTarget.GetSystem<IChessNavSystem>();
            battleField = mTarget.GetSystem<IBattleFieldDataSystem>();
            move = mTarget.GetSystem<IChessMoveSystem>();
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            //改变目标
            DoFindTarget();
            mTarget.RegisterEvent<EventNavUnreachable>(DoFindTarget);
            //移动逻辑
            move.Strategy = new NavMoveStrategy(nav);
            move.Start();
            //(正常)退出逻辑
            mTarget.RegisterEvent<EventStepDone>(TrySwitch2Attack);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            //if (Input.GetMouseButtonDown(1))
            //{
            //    move.Stop();
            //}
        }

        protected override void OnExit()
        {
            base.OnExit();
            //注销逻辑
            mTarget.UnRegisterEvent<EventNavUnreachable>(DoFindTarget);
            mTarget.UnRegisterEvent<EventStepDone>(TrySwitch2Attack);
            //移动逻辑
            move.Stop();
        }

        private void DoFindTarget(EventNavUnreachable e = default)
        {
            List<BattleChess> alternatives = ListPool<BattleChess>.Get();
            battleField.ChessSystem.SendVisitor(data.Realm, chess => chess.Side != data.Side, alternatives);
            nav.FindTarget(alternatives, out BattleChess target);
            data.Target = target;
            ListPool<BattleChess>.Release(alternatives);
        }
        private void TrySwitch2Attack(EventStepDone e = default)
        {
            if (CanAttack())
                mFSM.ChangeState(E_ChessState.Attack);
        }

        private bool CanAttack()
        {
            if (data.Target == null) return false;
            float range = data.Profile.DynamicAttr.AttackRange;
            float dis = Vector3.Distance(data.Position, data.Target.Transform.position);
            return dis <= range;
        }
    }
}
