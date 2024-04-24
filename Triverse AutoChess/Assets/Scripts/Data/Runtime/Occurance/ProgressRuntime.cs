using Game.Enum.Main;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Runtime
{
    public interface IProgressRuntimeData : IModel
    { 
        public bool HasSave { get; set; }
        public int CurrentFloor { get; set; }
        public E_Occurance CurrentEvent { get; set; }
    }
    public class ProgressRuntime : AbstractModel, IProgressRuntimeData
    {
        public bool HasSave { get; set; }
        public int CurrentFloor { get; set; }
        public E_Occurance CurrentEvent { get; set; }

        protected override void OnInit()
        {
            //∂¡»°
        }
    }
}

