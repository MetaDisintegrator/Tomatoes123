using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using Game.Data.Chess;

namespace Game.Tool
{
    public static partial class BinKit
    {
        #region SetUp
        static BinKit()
        {
            converters = new Dictionary<Type, IBinConverter>()
            {
                { typeof(int), new IntBinConverter() },
                { typeof(float), new FloatBinConverter() },
                { typeof(bool), new BooleanBinConverter() },
                { typeof(string), new StringBinConverter() },
                { typeof(AttrConfigData),new AttrConfigDataConverter() }
            };
            safeConverters = new Dictionary<Type, IBinConverter>()
            {
                { typeof(AttrConfigData),new AttrConfigDataSafeConverter() }
            };
        }
        #endregion
    }
    #region BaseConverters
    struct IntBinConverter : IBinConverter<int>
    {
        public int Read(ByteArray BA)
        {
            int res = BitConverter.ToInt32(BA.data, BA.readIndex);
            BA.readIndex += 4;
            return res;
        }

        public void Write(int value, FileStream fs)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            fs.Write(buffer, 0, buffer.Length);
        }
    }
    struct FloatBinConverter : IBinConverter<float>
    {
        public float Read(ByteArray BA)
        {
            float res = BitConverter.ToSingle(BA.data, BA.readIndex);
            BA.readIndex += 4;
            return res;
        }

        public void Write(float value, FileStream fs)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            fs.Write(buffer, 0, buffer.Length);
        }
    }
    struct BooleanBinConverter : IBinConverter<bool>
    {
        public bool Read(ByteArray BA)
        {
            return BitConverter.ToBoolean(BA.data, BA.readIndex++);
        }

        public void Write(bool value, FileStream fs)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            fs.Write(buffer, 0, buffer.Length);
        }
    }
    struct StringBinConverter : IBinConverter<string>
    {
        public string Read(ByteArray BA)
        {
            int len = BinKit.GetConverter<int>().Read(BA);
            string res = Encoding.UTF8.GetString(BA.data, BA.readIndex, len);
            BA.readIndex += len;
            return res;
        }

        public void Write(string value, FileStream fs)
        {
            byte[] content = Encoding.UTF8.GetBytes(value);
            int len = content.Length;
            byte[] head = BitConverter.GetBytes(len);
            byte[] buffer = new byte[len+sizeof(int)];
            head.CopyTo(buffer, 0);
            content.CopyTo(buffer, sizeof(int));
            fs.Write(buffer, 0, buffer.Length);
        }
    }

    #endregion
}