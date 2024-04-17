using Game.Tool;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface IDirectoryUtility:IUtility
    {
        Dictionary<int, string> GetDirectory(string fullName);
    }
    public class DirectoryUtility:IDirectoryUtility
    {
        public Dictionary<int, string> GetDirectory(string fullName)
        { 
            if(!File.Exists(fullName)) return new Dictionary<int, string>();
            ByteArray BA = new ByteArray(File.ReadAllBytes(fullName));
            return BinKit.ReadDict(BA, ba => BinKit.Read<int>(ba), ba => BinKit.Read<string>(ba));
        }
    }
}

