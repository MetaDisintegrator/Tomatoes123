using Game.Data.Chess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Game.Tool
{
    struct AttrConfigDataConverter : IBinConverter<AttrConfigData>
    {
        public AttrConfigData Read(ByteArray BA)
        {
            AttrConfigData res = new AttrConfigData();
            res.lvBelong = BinKit.Read<int>(BA);
            res.MaxHealth = BinKit.Read<float>(BA);
            res.PhysicalResistance = BinKit.Read<float>(BA);
            res.MagicalResistance = BinKit.Read<float>(BA);
            res.Attack = BinKit.Read<float>(BA);
            res.AttackRate = BinKit.Read<float>(BA);
            res.AttackRange = BinKit.Read<float>(BA);
            res.CritRate = BinKit.Read<float>(BA);
            res.CritMultiplier = BinKit.Read<float>(BA);
            return res;
        }

        public void Write(AttrConfigData value, FileStream fs)
        {
            BinKit.Write<int>(fs, false, value.lvBelong);
            BinKit.Write<float>(fs, false,
                value.MaxHealth,
                value.PhysicalResistance,
                value.MagicalResistance,
                value.Attack,
                value.AttackRate,
                value.AttackRange,
                value.CritRate,
                value.CritMultiplier);
        }
    }

    struct AttrConfigDataSafeConverter : IBinConverter<AttrConfigData>
    {
        public AttrConfigData Read(ByteArray BA)
        {
            AttrConfigData res = new AttrConfigData();

            IBinConverter<string> nameConverter = BinKit.GetConverter<string>();
            //根据反射创建映射
            Dictionary<string,FieldInfo> fieldMap = new Dictionary<string,FieldInfo>();
            foreach (var fieldInfo in typeof(AttrConfigData).GetFields())
            {
                fieldMap.Add(fieldInfo.Name, fieldInfo);
            }
            //自行传入要读的类型
            string name = nameConverter.Read(BA);
            while (name != BinKit.Safe_End) 
            {
                switch (name)
                {
                    case nameof(AttrConfigData.lvBelong):
                        fieldMap[name].SetValue(res, BinKit.Read<int>(BA));
                        break;
                    case nameof(AttrConfigData.MaxHealth):
                    case nameof(AttrConfigData.PhysicalResistance):
                    case nameof(AttrConfigData.MagicalResistance):
                    case nameof(AttrConfigData.Attack):
                    case nameof(AttrConfigData.AttackRate):
                    case nameof(AttrConfigData.AttackRange):
                    case nameof(AttrConfigData.CritRate):
                    case nameof(AttrConfigData.CritMultiplier):
                        fieldMap[name].SetValue(res, BinKit.Read<float>(BA));
                        break;
                }
            }
            return res;
        }

        public void Write(AttrConfigData value, FileStream fs)
        {
            BinKit.WriteWithName<int>(fs, true,
                (nameof(value.lvBelong), value.lvBelong));
            BinKit.WriteWithName<float>(fs, true,
                (nameof(value.MaxHealth), value.MaxHealth),
                (nameof(value.PhysicalResistance), value.PhysicalResistance),
                (nameof(value.MagicalResistance), value.MagicalResistance),
                (nameof(value.Attack), value.Attack),
                (nameof(value.AttackRate), value.AttackRate),
                (nameof(value.AttackRange), value.AttackRange),
                (nameof(value.CritRate), value.CritRate),
                (nameof(value.CritMultiplier), value.CritMultiplier)
                );
            BinKit.Write<string>(fs, false, BinKit.Safe_End);
        }
    }
}
