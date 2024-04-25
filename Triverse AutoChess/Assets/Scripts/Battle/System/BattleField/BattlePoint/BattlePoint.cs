using Game.Battle.BattlePointEvent;
using Game.Battle.Chess;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Game
{
    public class BattlePoint
    {
        public Vector3 position;
        public Pos gridPosition;

        TypeEventSystem eventSystem;
        List<BattleChess> currentChesses;
        BattleChess toComeChess;
        public bool CanMoveOn => currentChesses.Count == 0 && toComeChess == null;

        public BattlePoint(Vector3 position, Pos gridPosition)
        {
            this.position = position;
            this.gridPosition = gridPosition;
            this.eventSystem = new TypeEventSystem();
            currentChesses = ListPool<BattleChess>.Get();
            toComeChess = null;
            EventInit();
        }
        ~BattlePoint()
        {
            ListPool<BattleChess>.Release(currentChesses);
        }

        public void SendEvent<T>(T e) => eventSystem.Send(e);

        private void EventInit()
        {
            eventSystem.Register<EventChessReserve>(e =>
            {
                if (toComeChess != null)
                    Debug.LogError($"位于{gridPosition}的图格重复预订");
                toComeChess = e.chess;
            });
            eventSystem.Register<EventChessLeave>(e =>
            {
                currentChesses.Remove(e.chess);
            });
            eventSystem.Register<EventChessCome>(e =>
            {
                if (currentChesses.Count > 0)
                {
                    //挤压判定
                }
                if (toComeChess != null && toComeChess == e.chess)
                    toComeChess = null;
                currentChesses.Add(e.chess);
            });
        }
    }
}

