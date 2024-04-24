using Game.Node.Skill;
using QFramework;
using System.Collections;
using System.Collections.Generic;

namespace Game.Battle.Chess.AI
{
    public class AttackQueueSystem : AbstractSystem, ISystem
    {
        Queue<SkillResolver> skillQueue;

        protected override void OnInit()
        {
            
        }
    }
}
