using Game.Data;
using Game.Data.Profile;
using Game.Enum.Main;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Battle.Chess.Data
{
    public interface IBattleChessDataModel : IModel
    { 
        BattleChess Architecture { get; }
        GameObject Chess { get; }
        Vector3 Position { get; }
        IChessProfileData Profile { get; }
        AttrData Attr { get; }
        public E_BattleSide Side { get; set; }
        public E_Realm Realm { get; set; }
        public BattleChess Target { get; set; }
        void SetChess(GameObject chess);
        void SetProfile(IChessProfileData profile);
    }
    public class BattleChessDataModel : AbstractModel, IBattleChessDataModel
    {
        public BattleChess Architecture => (this as IBelongToArchitecture).GetArchitecture() as BattleChess;
        public GameObject Chess { get; set; }
        public Vector3 Position => Chess.transform.position;
        public IChessProfileData Profile { get; set; }
        public AttrData Attr { get; set; }
        public E_BattleSide Side { get; set; }
        public E_Realm Realm { get; set; }
        public BattleChess Target { get; set; }

        public void SetChess(GameObject chess) => this.Chess = chess;
        public void SetProfile(IChessProfileData profile) => this.Profile = profile;
        protected override void OnInit()
        {
        }
    }
}

