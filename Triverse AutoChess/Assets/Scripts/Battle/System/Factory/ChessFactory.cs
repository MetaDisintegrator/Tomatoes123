using Game.Battle.Chess;
using Game.Battle.Chess.AI;
using Game.Battle.Chess.Anim;
using Game.Battle.Chess.Data;
using Game.Battle.Chess.Event;
using Game.Data.Profile;
using Game.Enum.Main;
using QAssetBundle;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle
{
    public interface IChessFactorySystem : ISystem
    {
        BattleChess CreateChess(IChessProfileData profile, E_BattleSide side);
    }
    public class ChessFactorySystem : AbstractSystem, IChessFactorySystem
    {
        #region ·��
        private readonly string Bundle_Name = Chessprefab.BundleName;
        #endregion

        private ResLoader loader;
        public BattleChess CreateChess(IChessProfileData profile, E_BattleSide side)
        {
            GameObject chessGO;
            BattleChess chess;
            //ʵ����prefab
            chessGO = Object.Instantiate(loader.LoadSync<GameObject>(Bundle_Name, profile.Config.PrefabName));
            //������������
            chess = new BattleChess();
            InitChess(chess);
            //������GO�����
            chess.SetChess(chessGO);
            chess.Side = side;
            //�������GO�ĳ�ʼ������
            chess.SendEvent(new EventChessInit(chessGO));
            chess.SendEvent(new EventLateChessInit());
            return chess;
        }

        private void InitChess(BattleChess chess)
        {
            chess.MakeSureArchitecture();

            chess.RegisterSystem<IChessNavSystem>(new ChessNavSystem());
            chess.RegisterSystem<IChessAnimSystem>(new ChessAnimSystem());
            chess.RegisterSystem<IChessMonoSystem>(new ChessMonoSystem());
            chess.RegisterSystem<IChessFSMSystem>(new ChessFSMSystem());

            chess.RegisterModel<IBattleChessDataModel>(new BattleChessDataModel());
        }

        protected override void OnInit()
        {
            loader = ResLoader.Allocate();
        }
        protected override void OnDeinit()
        {
            base.OnDeinit();
            loader.Recycle2Cache();
        }
    }
}

