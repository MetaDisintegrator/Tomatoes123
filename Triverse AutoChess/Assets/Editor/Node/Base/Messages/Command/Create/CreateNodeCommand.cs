using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class CreateNodeCommand : AbstractCommand<INode>, ICommand<INode>
    {
        IZone ownerZone;
        Vector2 pos;
        int typeID;
        int ID;
        protected override INode OnExecute()
        {
            return this.GetSystem<INodeFactorySystem>().GenerateNodeWithID(typeID, ownerZone, pos, ID);
        }
        public void Set(IZone ownerZone, Vector2 pos, int typeID,int ID)
        {
            this.ownerZone = ownerZone;
            this.pos = pos;
            this.typeID = typeID;
            this.ID = ID;
        }
    }
}
