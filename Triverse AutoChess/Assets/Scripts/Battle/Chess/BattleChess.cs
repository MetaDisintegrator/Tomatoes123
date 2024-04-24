using Game.Battle.Chess.Data;
using Game.Battle.Chess.AI;
using Game.Data;
using QFramework;
using QFramework.MyCustomExtension;
using UnityEngine;
using Game.Battle.Chess.Anim;
using Game.Enum.Main;

namespace Game.Battle.Chess
{
    public class BattleChess : ArchitectureInstance<BattleChess>
    {
        public void SetChess(GameObject chess) => GetModel<IBattleChessDataModel>().SetChess(chess);
        public Transform Transform => GetModel<IBattleChessDataModel>().Chess.transform;
        public Pos GridPosition 
        { 
            get => GetSystem<IChessNavSystem>().GridPosition;
            set => GetSystem<IChessNavSystem>().GridPosition = value;
        }
        public E_BattleSide Side
        {
            get => GetModel<IBattleChessDataModel>().Side;
            set => GetModel<IBattleChessDataModel>().Side = value;
        }
    }
}

