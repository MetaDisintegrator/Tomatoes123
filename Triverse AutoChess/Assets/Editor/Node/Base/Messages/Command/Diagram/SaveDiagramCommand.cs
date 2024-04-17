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
            //����
            int count =0;
            IZoneModel zoneModel = this.GetModel<IZoneModel>();
            zoneModel.DoPreOrder(_=>count++);
            BinKit.Write(fs, false,count);
            zoneModel.DoPreOrder(zone => zone.WriteDiagramData(fs));
            //�ڵ�
            INodeModel nodeModel = this.GetModel<INodeModel>();
            BinKit.Write(fs, false,nodeModel.Nodes.Count);
            foreach (var node in nodeModel.Nodes) 
            {
                node.Value.WriteDiagramData(fs);
            }
            //��Ŀ
            IItemModel itemModel = this.GetModel<IItemModel>();
            BinKit.WriteList(itemModel.Items, fs, (item, fs) => item.WriteDiagramData(fs));
            //����
            IConnectModel connectModel = this.GetModel<IConnectModel>();
            BinKit.Write(fs,false,connectModel.Connects.Count);
            foreach (var connect in connectModel.Connects)
            { 
                connect.Value.WriteDiagramData(fs);
            }
            //id����
            INodeFactorySystem nodeFactorySystem = this.GetSystem<INodeFactorySystem>();
            BinKit.Write(fs, false, nodeFactorySystem.ZoneIdGen, nodeFactorySystem.NodeIdGen);
        }
        public SaveDiagramCommand(string fileName, string filePath)
        { 
            fullName = Path.Combine(filePath, fileName);
        }
    }
}

