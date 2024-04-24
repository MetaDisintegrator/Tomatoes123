using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Tool
{
    public static class BattlePointSetTool
    {
        public static BattlePoint FindNearest(this List<BattlePoint> list, BattlePoint point,Func<BattlePoint,bool> condition)
        {
            condition ??= _ => true;
            List<BattlePoint> conditionList = list.Where(condition).ToList();
            if(conditionList==null) return null;
            BattlePoint res = conditionList[0];
            int dis = Pos.ManhattanDis(res.gridPosition, point.gridPosition);
            foreach(BattlePoint p in conditionList) 
            {
                int temp = Pos.ManhattanDis(p.gridPosition, point.gridPosition);
                if (temp < dis)
                { 
                    res = p;
                    dis = temp;
                }
            }
            return res;
        }
        
    }
}

