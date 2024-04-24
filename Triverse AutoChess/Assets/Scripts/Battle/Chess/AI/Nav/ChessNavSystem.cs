using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess.AI
{
    public interface IChessNavSystem : ISystem
    { 
        Pos GridPosition { get; set; }
    }
    public class ChessNavSystem : AbstractSystem, IChessNavSystem
    {
        public Pos GridPosition { get; set; }
        protected override void OnInit()
        {
        }
    }
}
