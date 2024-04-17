using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class DestroyZoneCommand : AbstractCommand, ICommand
    {
        IZone zone;
        protected override void OnExecute()
        {
            IZoneModel model = this.GetModel<IZoneModel>();
            ZoneTreeNode node = model.FindTreeNode(zone.ID);
            model.DoPostOrder((zone) => (zone as Zone).OnDestroy(),node);
            if (node.parentZone != null)
            {
                node.parentZone.childs.Remove(node);
            }
            else
            {
                model.Trees.Remove(node);
            }
        }

        public DestroyZoneCommand(IZone zone)
        { 
            this.zone = zone;
        }
    }
}

