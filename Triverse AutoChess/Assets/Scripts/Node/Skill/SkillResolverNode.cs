using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Node.Skill
{
    public class SkillResolverNode
    {
        //�ѽ���������
        int recvAvailable;
        //�����������
        int recvRequire;
        //�ڵ�ID
        int ID;
        //�Ƿ��ѽ���
        bool triggered;
        //�㼶(����)
        int resolveLayer;
        //�ڶ��㼶(������ڵ�)
        int secondResolveLayer;
        //�������б�
        List<INodeReceiver> receivers;
        //�������б�
        List<INodeSender> senders;
    }
}

