using Game.Enum.Main;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TesterGetBattlefieldUtility : IGetBattlefieldUtility
    {
        List<Transform> lines;

        public BattlePoint[,] Get(E_Realm realm)
        {
            BattlePoint[,] res = new BattlePoint[5, 18];
            Transform curLine;
            for (int i = 0; i < lines.Count; i++)
            {
                curLine = lines[i];
                for (int j = 0; j < curLine.childCount; j++)
                {
                    res[i, j] = new BattlePoint(curLine.GetChild(j).position, new Pos(i, j));
                    //Debug.Log($"{i},{j}:{curLine.GetChild(i).name}");
                }
            }
            return res;
        }
        public TesterGetBattlefieldUtility()
        {
            lines = new List<Transform>();
            Transform root = GameObject.Find("Map").transform;
            for (int i = 0; i < root.childCount; i++)
            { 
                lines.Add(root.GetChild(i));
            }
        }
    }
}
