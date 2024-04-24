using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Node.Skill
{
    /// <summary>
    /// 一个解算器对应一个最外层触发区域，但并不一定是一个技能
    /// </summary>
    public class SkillResolver
    {
        //需要从解算器获取，在复制解算器时需要带着的信息TODO
        //解算单元
        Dictionary<int, SkillResolverNode> nodes;
        //重置器TODO
        //复制器TODO
        //遍历触发器TODO
    }
}

