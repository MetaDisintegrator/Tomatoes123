using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class PointGroup
    {
        public List<Point> points = new();
        private bool connectLastFrame = true;

        private bool HasConnect()
        { 
            if (points == null) return false;
            foreach (Point p in points) 
            {
                if(p.Connected) return true;
            }
            return false;
        }

        public bool CheckHasConnect()
        {
            if (HasConnect())
            {
                connectLastFrame = true;
            }
            else
            { 
                if(!connectLastFrame) return false;
                connectLastFrame= false;
            }
            return true;
        }

        public void OnDestroy()
        { 
            while (points .Count>0) 
            {
                points[0].RemoveSelf();
                points.RemoveAt(0);
            }
        }
    }
}

