using Game.Tool;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class ReadDiagramCommand : AbstractCommand, ICommand
    {
        int ID;
        string fileName;
        string fullName;
        int count;
        CreateZoneCommand cZone;
        CreateNodeCommand cNode;
        CreateConnectCommand cConnect;
        protected override void OnExecute()
        {
            if (!File.Exists(fullName))
            {
                Debug.LogError("图表文件不存在");
                return;
            }
            //准备
            INodeModel nodeModel = this.GetModel<INodeModel>();
            IZoneModel zoneModel = this.GetModel<IZoneModel>();
            INodeFactorySystem nodeFactory = this.GetSystem<INodeFactorySystem>();
            //清除
            this.SendCommand(new DestroyAllCommand());
            ByteArray BA = new ByteArray(File.ReadAllBytes(fullName));
            //区域
            DoRepeat(() => 
            {
                int parentID = BinKit.Read<int>(BA);
                Vector2 pos = BinKit.Read<Vector2>(BA);
                int typeID = BinKit.Read<int>(BA);
                int ID = BinKit.Read<int>(BA);
                IZone parent = parentID == -1 ? null : zoneModel.FindTreeNode(parentID).value;
                cZone.Set(parent, pos, typeID, ID);
                this.SendCommand(cZone);
            });
            //节点
            Dictionary<INode,List<E_NodeData>> specialRequests = new Dictionary<INode, List<E_NodeData>>();
            DoRepeat(() =>
            {
                //读取
                int ownerID = BinKit.Read<int>(BA);
                Vector2 pos = BinKit.Read<Vector2>(BA);
                int typeID = BinKit.Read<int>(BA);
                int ID = BinKit.Read<int>(BA);
                //转化
                IZone ownerZone = zoneModel.FindTreeNode(ownerID).value;
                //创建
                cNode.Set(ownerZone, pos, typeID, ID);
                INode node = this.SendCommand(cNode);
                //特殊节点
                switch (node.SpecialType)
                {
                    case E_SpecialNode.Entrance:
                    case E_SpecialNode.Exit:
                        List<E_NodeData> dataTypes = BinKit.ReadList(BA, ba => (E_NodeData)BinKit.Read<int>(ba));
                        specialRequests.Add(node, dataTypes);
                        break;
                }
            });
            foreach (var pair in specialRequests)
            {
                foreach (var dataType in pair.Value)
                {
                    pair.Key.OwnerZone.HandleSpecialConnect(dataType, pair.Key.SpecialType);
                }
            }
            //条目
            DoRepeat(() =>
            {
                //读取位置
                int ownerID = BinKit.Read<int>(BA);
                int itemIndex = BinKit.Read<int>(BA);
                //交给Item读取信息
                IItem item= nodeModel.Nodes[ownerID].Items[itemIndex];
                item.ReadDiagramData(BA);
            });
            //连接
            DoRepeat(() =>
            {
                //读取
                int startNodeID = BinKit.Read<int>(BA);
                int startPointIndex = BinKit.Read<int>(BA);
                int endNodeID = BinKit.Read<int>(BA);
                int endPointIndex = BinKit.Read<int>(BA);
                //转化
                IPoint start = nodeModel.Nodes[startNodeID].OutPoints[startPointIndex];
                IPoint end = nodeModel.Nodes[endNodeID].InPoints[endPointIndex];
                //创建
                cConnect.Set(start, end);
                this.SendCommand(cConnect);
            });
            //id生成
            nodeFactory.SetIDGen(BinKit.Read<int>(BA), BinKit.Read<int>(BA));
            //读取完成事件
            this.SendEvent(new LoadDiagramEvent(ID, fileName));

            //内部函数
            void DoRepeat(Action action)
            {
                count = BinKit.Read<int>(BA);
                for(int i=0;i<count;i++)
                    action();
            }
        }

        public ReadDiagramCommand(int ID,string fileName,string filePath)
        {
            this.ID = ID;
            this.fileName = fileName;
            this.fullName = Path.Combine(filePath, fileName);
            cZone = new CreateZoneCommand();
            cNode = new CreateNodeCommand();
            cConnect = new CreateConnectCommand();
        }
    }
}

