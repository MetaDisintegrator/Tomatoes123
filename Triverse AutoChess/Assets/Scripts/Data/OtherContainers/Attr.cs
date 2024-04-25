using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    public class AttrData
    {
        public float MaxHealth { get; set; }

        public float PhysicalResistance { get; set; }

        public float MagicalResistance { get; set; }

        public float Attack { get; set; }

        public float AttackRate { get; set; }

        public float AttackRange { get; set; }

        public float CritRate { get; set; }

        public float CritMultiplier { get; set; }
        /// <summary>
        /// …¡±‹¬ 
        /// </summary>
        public float EvationRate { get; set; }

        public readonly float Speed = 5f;
    }
}

