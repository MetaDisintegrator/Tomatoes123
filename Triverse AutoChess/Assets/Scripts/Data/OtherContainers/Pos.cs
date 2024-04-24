using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Pos
{
    public int x;
    public int y;
    public Pos(int x,int y)
    { 
        this.x = x;
        this.y = y;
    }

    public bool InRange(Pos min, Pos max)
    { 
        return this.x>=min.x &&
            this.y>=min.y &&
            this.x<=max.x &&
            this.y<=max.y;
    }

    public static int ManhattanDis(Pos a, Pos b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
    
    public static Pos operator +(Pos a,Pos b) 
    {
        return new Pos(a.x + b.x, a.y + b.y);
    }
}
