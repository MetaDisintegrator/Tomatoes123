using Game.Battle.Chess.Data;
using Game.Battle.Chess.Event;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.Anim
{
    public interface IChessAnimSystem : ISystem
    {
        void ChangeFace();
    }
    public class ChessAnimSystem : AbstractSystem, IChessAnimSystem
    {
        Transform Transform => this.GetModel<IBattleChessDataModel>().Chess.transform;
        Animator animator;

        int face;

        protected override void OnInit()
        {
            face = 1;
            this.RegisterEvent<EventChessInit>(_ => 
            {
                if (this.GetModel<IBattleChessDataModel>().Side == Enum.Main.E_BattleSide.Enemy)
                    ChangeFace();
            });
        }

        public void ChangeFace()
        {
            face *= -1;
            Transform.LocalScaleX(Transform.localScale.x * -1);
        }
    }
}

