using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Editor.NodeEditor.Query
{
    public class DiagramDirectoryQuery : AbstractQuery<Dictionary<int,string>>, IQuery<Dictionary<int, string>>
    {
        protected override Dictionary<int, string> OnDo()
        {
            return this.GetModel<IDiagramDirectoryModel>().Directory;
        }
    }
}
