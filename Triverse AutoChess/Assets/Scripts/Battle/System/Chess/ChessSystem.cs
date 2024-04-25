using Game.Battle.Chess;
using Game.Enum.Main;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.System
{
    public interface IChessSystem : ISystem
    {
        void AddChess(E_Realm realm, BattleChess chess);
        /// <summary>
        /// ����ĳ�����ڵ���������
        /// </summary>
        /// <param name="realm">��</param>
        /// <param name="condition">������null��ʾ������</param>
        /// <returns></returns>
        void SendVisitor(E_Realm realm, Func<BattleChess, bool> condition, List<BattleChess> buffer);
    }

    public class ChessSystem : AbstractSystem, IChessSystem
    {
        Dictionary<E_Realm, Chesses> chessesMap;

        public void AddChess(E_Realm realm, BattleChess chess)
        {
            //ȷ������ʵ�����ڵĽ�
            chess.Realm = realm;
            //��ӵ�����
            chessesMap[realm].Add(chess);
        }

        public void SendVisitor(E_Realm realm, Func<BattleChess, bool> condition, List<BattleChess> buffer)
        {
            condition ??= _ => true;
            ChessesVisitor visitor = ChessesVisitor.Get(condition);
            chessesMap[realm].ReceiveVisitor(visitor);
            buffer.AddRange(visitor.Result);
            visitor.Recycle();
        }

        protected override void OnInit()
        {
            chessesMap = new Dictionary<E_Realm, Chesses>()
            {
                { E_Realm.Arcane,new Chesses()},
                { E_Realm.PawVerse,new Chesses()},
                { E_Realm.Technosphere,new Chesses()}
            };
        }
    }
}

