using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class CreateZoneCommand : AbstractCommand, ICommand
    {
        IZone parent;
        Vector2 pos;
        int typeID;
        int ID;
        protected override void OnExecute()
        {
            this.GetSystem<INodeFactorySystem>().GenerateZoneWithID(typeID, parent, pos,ID);
        }
        public void Set(IZone parent, Vector2 pos, int typeID,int ID)
        {
            this.parent = parent;
            this.pos = pos;
            this.typeID = typeID;
            this.ID = ID;
        }
    }
}
