using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Node.Skill
{
    public class SkillResolverNode
    {
        //已接受数据量
        int recvAvailable;
        //需接收数据量
        int recvRequire;
        //节点ID
        int ID;
        //是否已解算
        bool triggered;
        //层级(区域)
        int resolveLayer;
        //第二层级(给特殊节点)
        int secondResolveLayer;
        //接收器列表
        List<INodeReceiver> receivers;
        //发送器列表
        List<INodeSender> senders;
    }
}

