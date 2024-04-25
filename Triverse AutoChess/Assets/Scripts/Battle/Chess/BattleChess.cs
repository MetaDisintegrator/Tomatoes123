using Game.Battle.Chess.Data;
using Game.Battle.Chess.AI;
using Game.Data;
using QFramework;
using QFramework.MyCustomExtension;
using UnityEngine;
using Game.Battle.Chess.Anim;
using Game.Enum.Main;
using Game.Battle.Chess.Move;
using Game.Data.Profile;

namespace Game.Battle.Chess
{
    public class BattleChess : ArchitectureInstance<BattleChess>
    {
        public void SetChess(GameObject chess) => GetModel<IBattleChessDataModel>().SetChess(chess);
        public Transform Transform => GetModel<IBattleChessDataModel>().Chess.transform;
        public Pos GridPosition
        {
            get => GetSystem<IChessMoveSystem>().GridPosition;
            set => GetSystem<IChessMoveSystem>().GridPosition = value;
        }
        /// <summary>
        /// 寻路使用的位置
        /// </summary>
        public Pos NavPosition => GetSystem<IChessMoveSystem>().NavPosition;
            
        public E_BattleSide Side
        {
            get => GetModel<IBattleChessDataModel>().Side;
            set => GetModel<IBattleChessDataModel>().Side = value;
        }
        public E_Realm Realm
        {
            get => GetModel<IBattleChessDataModel>().Realm;
            set => GetModel<IBattleChessDataModel>().Realm = value;
        }
        public IChessProfileData Profile
        {
            get => GetModel<IBattleChessDataModel>().Profile;
            set => GetModel<IBattleChessDataModel>().SetProfile(value);
        }
    }
}

