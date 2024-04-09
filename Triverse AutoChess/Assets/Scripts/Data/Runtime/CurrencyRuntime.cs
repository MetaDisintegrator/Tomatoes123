using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Runtime
{
    public interface ICurrencyRuntimeData : IModel
    {
        public int Coin { get; set; }
    }

    public class CurrencyRuntimeData : AbstractModel, ICurrencyRuntimeData
    {
        public int Coin { get; set; }

        protected override void OnInit()
        {
            //∂¡»°
        }
    }
}
