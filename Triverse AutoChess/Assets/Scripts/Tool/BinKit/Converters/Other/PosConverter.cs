using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Tool
{
    struct PosBinConverter : IBinConverter<Pos>
    {
        public Pos Read(ByteArray BA)
        {
            Pos res = new Pos(BinKit.Read<int>(BA), BinKit.Read<int>(BA));
            return res;
        }

        public void Write(Pos value, FileStream fs)
        {
            BinKit.Write(fs, false, value.x, value.y);
        }
    }
}
