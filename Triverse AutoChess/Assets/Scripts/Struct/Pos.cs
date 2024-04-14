using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Pos
{
    public int NodeID;
    public int Index;
    public Pos(int NodeID,int Index)
    { 
        this.NodeID = NodeID;
        this.Index = Index;
    }
}
