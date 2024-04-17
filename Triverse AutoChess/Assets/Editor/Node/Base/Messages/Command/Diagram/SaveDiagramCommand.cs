using Game.Tool;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class SaveDiagramCommand : AbstractCommand, ICommand
    {
        string fullName;
        protected override void OnExecute()
        {
            using FileStream fs = File.Open(fullName, FileMode.OpenOrCreate);
            //区域
            int count =0;
            IZoneModel zoneModel = this.GetModel<IZoneModel>();
            zoneModel.DoPreOrder(_=>count++);
            BinKit.Write(fs, false,count);
            zoneModel.DoPreOrder(zone => zone.WriteDiagramData(fs));
            //节点
            INodeModel nodeModel = this.GetModel<INodeModel>();
            BinKit.Write(fs, false,nodeModel.Nodes.Count);
            foreach (var node in nodeModel.Nodes) 
            {
                node.Value.WriteDiagramData(fs);
            }
            //条目
            IItemModel itemModel = this.GetModel<IItemModel>();
            BinKit.WriteList(itemModel.Items, fs, (item, fs) => item.WriteDiagramData(fs));
            //连接
            IConnectModel connectModel = this.GetModel<IConnectModel>();
            BinKit.Write(fs,false,connectModel.Connects.Count);
            foreach (var connect in connectModel.Connects)
            { 
                connect.Value.WriteDiagramData(fs);
            }
            //id生成
            INodeFactorySystem nodeFactorySystem = this.GetSystem<INodeFactorySystem>();
            BinKit.Write(fs, false, nodeFactorySystem.ZoneIdGen, nodeFactorySystem.NodeIdGen);
        }
        public SaveDiagramCommand(string fileName, string filePath)
        { 
            fullName = Path.Combine(filePath, fileName);
        }
    }
}

