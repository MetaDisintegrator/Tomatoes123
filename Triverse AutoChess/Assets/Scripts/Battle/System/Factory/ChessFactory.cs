using Game.Battle.Chess;
using Game.Battle.Chess.AI;
using Game.Battle.Chess.Anim;
using Game.Battle.Chess.Data;
using Game.Battle.Chess.Event;
using Game.Battle.Chess.Move;
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
        #region 路径
        private readonly string Bundle_Name = Chessprefab.BundleName;
        #endregion

        private ResLoader loader;
        public BattleChess CreateChess(IChessProfileData profile, E_BattleSide side)
        {
            GameObject chessGO;
            BattleChess chess;
            //实例化prefab
            chessGO = Object.Instantiate(loader.LoadSync<GameObject>(Bundle_Name, profile.Config.PrefabName));
            //初步创建对象
            chess = new BattleChess();
            InitChess(chess);
            chess.MakeSureArchitecture();
            //对象与GO相关联
            chess.SetChess(chessGO);
            //信息传递
            chess.Side = side;
            chess.Profile = profile;
            //完成依赖GO的初始化工作
            chess.SendEvent(new EventChessInit(chessGO));
            chess.SendEvent(new EventLateChessInit());
            return chess;
        }

        private void InitChess(BattleChess chess)
        {
            chess.RegisterSystem<IChessNavSystem>(new ChessNavSystem());
            chess.RegisterSystem<IChessAnimSystem>(new ChessAnimSystem());
            chess.RegisterSystem<IChessMonoSystem>(new ChessMonoSystem());
            chess.RegisterSystem<IChessFSMSystem>(new ChessFSMSystem());
            chess.RegisterSystem<IChessMoveSystem>(new ChessMoveSystem());
            chess.RegisterSystem<IBattleFieldDataSystem>(new BattleFieldDataSystem());

            chess.RegisterModel<IBattleChessDataModel>(new BattleChessDataModel());

            chess.RegisterUtility<IChessNavUtility>(new ChessNavUtility());
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

