using Game.Data.Chess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Game.Tool
{
    public interface IBinConverter 
    {
    }
    public interface IBinConverter<T> : IBinConverter
    {
        T Read(ByteArray BA);
        void Write(T value, FileStream fs);
    }
    public static partial class BinKit
    {
        public static readonly string Safe_End = "END";
        static Dictionary<Type, IBinConverter> converters;
        static Dictionary<Type, IBinConverter> safeConverters;
        #region GetConverter
        public static IBinConverter<T> GetConverter<T>()
        { 
            return converters[typeof(T)] as IBinConverter<T>;
        }
        public static IBinConverter<T> GetSafeConverter<T>()
        { 
            IBinConverter res;
            if (safeConverters.TryGetValue(typeof(T), out res)) { return res as IBinConverter<T>; }
            else return GetConverter<T>();
        }
        #endregion
        #region Write&Read
        public static void Write<T>(FileStream fs, bool safeMode = false, params T[] values)
        {
            IBinConverter<T> converter = safeMode ? GetSafeConverter<T>() : GetConverter<T>();
            foreach (var value in values)
            {
                converter.Write(value, fs);
            }
        }
        public static T Read<T>(ByteArray BA,bool safeMode = false)
        { 
            IBinConverter<T> converter = safeMode? GetSafeConverter<T>() : GetConverter<T>();
            return converter.Read(BA);
        }
        public static void WriteWithName<T>(FileStream fs, bool safeMode = true, params (string name, T value)[] pairs)
        {
            IBinConverter<T> valueConverter = safeMode ? GetSafeConverter<T>() : GetConverter<T>();
            IBinConverter<string> nameConverter = GetConverter<string>();
            foreach (var pair in pairs)
            {
                nameConverter.Write(pair.name, fs);
                valueConverter.Write(pair.value, fs);
            }
        }
        #endregion
        #region List&Dict
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
        #endregion
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
}

