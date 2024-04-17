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
                Debug.LogError("ͼ���ļ�������");
                return;
            }
            //׼��
            INodeModel nodeModel = this.GetModel<INodeModel>();
            IZoneModel zoneModel = this.GetModel<IZoneModel>();
            INodeFactorySystem nodeFactory = this.GetSystem<INodeFactorySystem>();
            //���
            this.SendCommand(new DestroyAllCommand());
            ByteArray BA = new ByteArray(File.ReadAllBytes(fullName));
            //����
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
            //�ڵ�
            Dictionary<INode,List<E_NodeData>> specialRequests = new Dictionary<INode, List<E_NodeData>>();
            DoRepeat(() =>
            {
                //��ȡ
                int ownerID = BinKit.Read<int>(BA);
                Vector2 pos = BinKit.Read<Vector2>(BA);
                int typeID = BinKit.Read<int>(BA);
                int ID = BinKit.Read<int>(BA);
                //ת��
                IZone ownerZone = zoneModel.FindTreeNode(ownerID).value;
                //����
                cNode.Set(ownerZone, pos, typeID, ID);
                INode node = this.SendCommand(cNode);
                //����ڵ�
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
            //��Ŀ
            DoRepeat(() =>
            {
                //��ȡλ��
                int ownerID = BinKit.Read<int>(BA);
                int itemIndex = BinKit.Read<int>(BA);
                //����Item��ȡ��Ϣ
                IItem item= nodeModel.Nodes[ownerID].Items[itemIndex];
                item.ReadDiagramData(BA);
            });
            //����
            DoRepeat(() =>
            {
                //��ȡ
                int startNodeID = BinKit.Read<int>(BA);
                int startPointIndex = BinKit.Read<int>(BA);
                int endNodeID = BinKit.Read<int>(BA);
                int endPointIndex = BinKit.Read<int>(BA);
                //ת��
                IPoint start = nodeModel.Nodes[startNodeID].OutPoints[startPointIndex];
                IPoint end = nodeModel.Nodes[endNodeID].InPoints[endPointIndex];
                //����
                cConnect.Set(start, end);
                this.SendCommand(cConnect);
            });
            //id����
            nodeFactory.SetIDGen(BinKit.Read<int>(BA), BinKit.Read<int>(BA));
            //��ȡ����¼�
            this.SendEvent(new LoadDiagramEvent(ID, fileName));

            //�ڲ�����
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

