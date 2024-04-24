using Game.Enum.Main;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle
{
    public interface IBattlefieldSystem : ISystem
    {
        Battlefield GetBattlefield(E_Realm realm);
    }
    public class BattlefieldSystem : AbstractSystem, IBattlefieldSystem
    {
        Dictionary<E_Realm, Battlefield> battlefields;

        public Battlefield GetBattlefield(E_Realm realm)
        {
            if (!battlefields.ContainsKey(realm))
                battlefields.Add(realm, new Battlefield(this.GetUtility<IGetBattlefieldUtility>().Get(realm)));
            return battlefields[realm];
        }

        protected override void OnInit()
        {
            battlefields = new Dictionary<E_Realm, Battlefield>();
        }
    }
    public class Battlefield
    {
        public BattlePoint[,] grid;
        public BattlePoint GetPoint(Pos pos)
        {
            if (!pos.InRange(new Pos(0,0),new Pos(grid.GetLength(0),grid.GetLength(1))))
                return null;
            return grid[pos.x, pos.y];
        }
        public void FindByDis(BattlePoint center, float dis, List<BattlePoint> buffer, Func<BattlePoint, bool> condition)
        {
            condition ??= _ => true;
            foreach (var point in grid)
            {
                if (!condition(point)) 
                    continue;
                if (Vector3.Distance(center.position, point.position) <= dis)
                    buffer.Add(point);
            }
        }
        public void FindBySide(BattlePoint center,List<BattlePoint> buffer, Func<BattlePoint,bool> condition)
        {
            condition ??= _ => true;
            Pos centerPos = center.gridPosition;
            ConsiderSide(new Pos(0, 1));
            ConsiderSide(new Pos(0, -1));
            ConsiderSide(new Pos(1, 0));
            ConsiderSide(new Pos(-1, 0));

            void ConsiderSide(Pos offset)
            { 
                BattlePoint p = GetPoint(centerPos + offset);
                if (p == null) return;
                if(condition(p))
                    buffer.Add(p);
            }
        }

        public Battlefield(BattlePoint[,] grid)
        {
            this.grid = grid;
        }
    }
}

