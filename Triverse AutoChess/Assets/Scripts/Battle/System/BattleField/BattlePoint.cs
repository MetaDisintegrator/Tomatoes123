using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BattlePoint
    {
        public Vector3 position;
        public Pos gridPosition;
        public bool CanMoveOn { get; }
        TypeEventSystem eventSystem;

        public BattlePoint(Vector3 position, Pos gridPosition)
        {
            this.position = position;
            this.gridPosition = gridPosition;
            this.eventSystem = new TypeEventSystem();
        }
    }
}

