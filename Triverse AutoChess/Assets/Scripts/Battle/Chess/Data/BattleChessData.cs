using Game.Data;
using Game.Data.Profile;
using Game.Enum.Main;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.Data
{
    public interface IBattleChessDataModel : IModel
    { 
        GameObject Chess { get; }
        IChessProfileData Profile { get; }
        AttrData Attr { get; }
        public E_BattleSide Side { get; set; }
        void SetChess(GameObject chess);
    }
    public class BattleChessDataModel : AbstractModel, IBattleChessDataModel
    {
        public GameObject Chess { get; set; }
        public IChessProfileData Profile { get; set; }
        public AttrData Attr { get; set; }
        public E_BattleSide Side { get; set; }

        public void SetChess(GameObject chess)
        { 
            this.Chess = chess;
        }
        protected override void OnInit()
        {
        }
    }
}

