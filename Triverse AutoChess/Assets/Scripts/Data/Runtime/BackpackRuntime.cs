using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Runtime
{
    public interface IBackpackRuntimeData : IModel
    {

    }
    public class BackpackRuntimeData : AbstractModel, IBackpackRuntimeData
    {
        protected override void OnInit()
        {
            throw new System.NotImplementedException();
        }
    }
}
