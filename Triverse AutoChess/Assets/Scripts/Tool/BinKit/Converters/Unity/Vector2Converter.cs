using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Tool
{
    struct Vector2BinConverter : IBinConverter<Vector2>
    {
        public Vector2 Read(ByteArray BA)
        {
            Vector2 res = new Vector2(BinKit.Read<float>(BA), BinKit.Read<float>(BA));
            return res;
        }

        public void Write(Vector2 value, FileStream fs)
        {
            BinKit.Write(fs, false, value.x, value.y);
        }
    }
}