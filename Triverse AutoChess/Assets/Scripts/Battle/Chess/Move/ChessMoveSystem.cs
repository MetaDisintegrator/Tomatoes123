using Game.Battle.BattlePointEvent;
using Game.Battle.Chess.Data;
using Game.Battle.Chess.Event;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.Move
{
    public interface IChessMoveSystem : ISystem
    {
        Pos GridPosition { get; set; }
        Pos NavPosition { get; }
        /// <summary>
        /// �ƶ������
        /// ���ؿմ��������ƶ�
        /// </summary>
        public IMovePoint Strategy { get; set; }
        
        void Start();
        void Stop();
    }
    public interface IMovePoint
    {
        BattlePoint CalNextPoint(BattlePoint current);
        
    }
    public class ChessMoveSystem : AbstractSystem, IChessMoveSystem
    {
        public Pos GridPosition 
        {
            get => currentPoint.gridPosition;
            set=>currentPoint= this.GetSystem<IBattleFieldDataSystem>().Battlefield.GetPoint(value);
        }
        public Pos NavPosition => targetPoint != null ? targetPoint.gridPosition : GridPosition;
        BattlePoint currentPoint;
        Vector3 startPosition;
        BattlePoint targetPoint;
        /// <summary>
        /// �ƶ���ʱ
        /// </summary>
        float moveDuration;
        IAction moveAction;
        IActionController controller;
        bool moving;

        public IMovePoint Strategy { get; set; }

        protected override void OnInit()
        {
            moveAction = ActionKit.Repeat()
                .Condition(MoveInit)
                .Parallel(s =>
                    s.Delay(() => moveDuration/2, MoveHalf)
                    .Lerp(0, 1, ()=>moveDuration, MoveDuration))
                .Callback(MoveDone);
        }

        public void Start()
        {
            moving = true;
            if (controller == null)
                controller = moveAction.Start(this.GetSystem<IChessMonoSystem>().Mono);
            else
                controller.Reset();
        }
        public void Stop() 
        {
            moving= false;
            controller.Pause();
        }

        #region �ڲ�
        private bool MoveInit()
        {
            IBattleChessDataModel data = this.GetModel<IBattleChessDataModel>();
            //Ѱ����ĩ��(һ��)
            targetPoint = Strategy.CalNextPoint(currentPoint);
            startPosition = data.Position;
            //ȷ�Ͽ����ƶ�
            if (targetPoint != null && targetPoint.CanMoveOn)
            {
                //Ԥ��
                targetPoint.SendEvent(new EventChessReserve(data.Architecture));
                //����ʱ��
                float dis = Vector3.Distance(startPosition, targetPoint.position);
                moveDuration = dis / data.Profile.DynamicAttr.Speed;
                //Debug.Log("CanMove");
                return true;
            }
            //�����ƶ����֡��Ϊ�Ѿ��ƶ����
            this.SendEvent<EventStepDone>();
            return false;
        }
        private void MoveHalf()
        {
            BattleChess architecture = this.GetModel<IBattleChessDataModel>().Architecture;
            //֪ͨͼ��
            currentPoint.SendEvent(new EventChessLeave(architecture));
            targetPoint.SendEvent(new EventChessCome(architecture));
            //�ı�����
            currentPoint = targetPoint;
        }
        private void MoveDuration(float x)
        {
            this.GetModel<IBattleChessDataModel>().Chess.transform.Position(startPosition + x * (targetPoint.position - startPosition));
        }
        private void MoveDone()
        {
            this.SendEvent<EventStepDone>();
        }
        #endregion
    }
}

