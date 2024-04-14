using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public enum E_PointDir
    {
        In = 1,
        Out = 2
    }
    public enum E_NodeData
    {
        None = 0,
        Control,
        Int,
        Float,
        Bool,
        MapPoint,
        MapArea,
        Projecticle,
        Chess,
        IndividualData,
        String,
        UnKnown
    }
    public enum E_NodeDataScale
    {
        Single,
        Multiple
    }
}
