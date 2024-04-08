using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Game.Tool
{
    public interface IBinConverter { }
    public interface IBinConverter<T> : IBinConverter
    {
        T Read(ByteArray BA);
        void Write(T value, FileStream fs);
    }
    public static class BinKit
    {
        static BinKit()
        { 
            converters = new Dictionary<Type, IBinConverter> ();
            converters.Add(typeof(int),new IntBinConverter());
        }
        static Dictionary<Type, IBinConverter> converters;
        public static IBinConverter<T> GetConverter<T>()
        { 
            return converters[typeof(T)] as IBinConverter<T>;
        }
        public static List<T> ReadList<T>(ByteArray BA, Func<ByteArray,T> reader)
        { 
            List<T> list = new List<T>();
            int count = GetConverter<int>().Read(BA);
            for (int i = 0; i < count; i++)
            {
                list.Add(reader(BA));
            }
            return list;
        }
        public static void WriteList<T>(List<T> value, FileStream fs, Action<T,FileStream> writer)
        {
            GetConverter<int>().Write(value.Count, fs);
            for (int i = 0; i < value.Count; i++)
            {
                writer(value[i], fs);
            }
        }
        public static Dictionary<TKey, TValue> ReadDict<TKey, TValue>(ByteArray BA, Func<ByteArray, TKey> keyReader,
            Func<ByteArray, TValue> valueReader)
        {
            Dictionary<TKey,TValue> dict = new Dictionary<TKey,TValue>();
            int count = GetConverter<int>().Read(BA);
            for (int i = 0; i < count; i++)
            {
                dict.Add(keyReader(BA), valueReader(BA));
            }
            return dict;
        }
        public static void WriteDict<TKey, Tvalue>(Dictionary<TKey, Tvalue> value, FileStream fs, Action<TKey, FileStream> keyWriter,
            Action<Tvalue, FileStream> valueWriter)
        {
            GetConverter<int>().Write(value.Count,fs);
            foreach (var key in value.Keys.ToArray())
            {
                keyWriter(key,fs);
                valueWriter(value[key], fs);
            }
        }
    }
    public class ByteArray
    {
        public byte[] data;
        public int readIndex;
        public ByteArray(byte[] buffer)
        {
            this.data = buffer;
            readIndex = 0;
        }
    }
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
}

