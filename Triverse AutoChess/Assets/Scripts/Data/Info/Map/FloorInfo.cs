using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Info
{
    public interface IFloorInfo
    { 
        
    }
    public class FloorInfo : IFloorInfo
    {
        bool isFinal;
        List<IOccuranceInfo> occurances;
    }
}

