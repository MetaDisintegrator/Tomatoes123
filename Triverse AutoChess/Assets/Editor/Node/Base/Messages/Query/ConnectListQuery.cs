using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Editor.NodeEditor.Query
{
    public class ConnectListQuery : AbstractQuery<List<IConnect>>, IQuery<List<IConnect>>
    {
        protected override List<IConnect> OnDo()
        {
            return this.GetModel<IConnectModel>().Connects.Values.ToListPooled<IConnect>();
        }
    }
}
