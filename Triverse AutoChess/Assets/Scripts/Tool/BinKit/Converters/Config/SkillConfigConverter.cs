using Game.Data.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Game.Tool
{
    struct SkillConfigDataConverter: IBinConverter<SkillConfigData>
    {
        public SkillConfigData Read(ByteArray BA)
        {
            SkillConfigData res = new SkillConfigData();
            res.id = BinKit.Read<int>(BA);
            res.CoolDown = BinKit.Read<float>(BA);
            res.Name = BinKit.Read<string>(BA);
            res.Description = BinKit.Read<string>(BA);           
            res.ResPath = BinKit.Read<string>(BA);
            res.SOPath = BinKit.Read<string>(BA);
            return res;
        }

        public void Write(SkillConfigData value,FileStream fs)
        {
            BinKit.Write<int>(fs, false, value.id);
            BinKit.Write<float>(fs, false, value.CoolDown);
            BinKit.Write<string>(fs, false,
                value.Name
                value.Description
                value.ResPath
                value.SOPath);
        }
    }

    struct SkillConfigDataSafeConverter:IBinConverter<SkillConfigData>
    {
        public SkillConfigData Read(ByteArray BA)
        {
            SkillConfigData res = new SkillrConfigData();

            IBinConverter<string> nameConverter = BinKit.GetConverter<string>();
            Dictionary<string, FieldInfo> fieldMap = new Dictionary<string, FieldInfo>();
            foreach(var fieldInfo in typeof (SkillConfigData).GetFields())
            {
                fieldMap.Add(fieldInfo.Name, fieldInfo);
            }

            string name = nameConverter.Read(BA);
            while (name!=BinKit.Safe_End)
            {
                switch(name)
                {
                    
                }
            }

        }
}
