using Game.Tool;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class AddDiagramDirectoryCommand : AbstractCommand<int>, ICommand<int>
    {
        int newID;
        string newFileName;
        string directoryFullName;
        protected override int OnExecute()
        {
            IDiagramDirectoryModel diagramDirectoryModel = this.GetModel<IDiagramDirectoryModel>();
            Dictionary<int, string> directory = diagramDirectoryModel.Directory;
            //�������
            if (newFileName == null || newFileName.Length == 0) return 3;//�ļ���Ϊ��
            if ((directory.ContainsKey(newID) && directory[newID] == newFileName) || (!directory.ContainsKey(newID) && !directory.ContainsValue(newFileName)))//�Ϸ�
            {
                //��Ӳ��洢
                directory.TryAdd(newID, newFileName);
                this.SendCommand(new SaveDiagramDirectoryCommand());
                return 0;
            }
            if (directory.ContainsValue(newFileName)) return 1;//�ļ����Ѿ�����(����ID��һ��)
            if (directory.ContainsKey(newID)) return 2;//ID�Ѿ�����(�����ļ�����һ��)
            return 0;
        }
        public AddDiagramDirectoryCommand(string filePath,string directoryName,int ID,string fileName) 
        {
            directoryFullName = Path.Combine(filePath, directoryName);
            this.newID = ID;
            this.newFileName = fileName;
        }
    }
}

