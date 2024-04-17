using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class DestroyAllCommand : AbstractCommand, ICommand
    {
        protected override void OnExecute()
        {
            IZoneModel model = this.GetModel<IZoneModel>();
            model.DoPostOrder((zone) => (zone as Zone).OnDestroy());
            model.Trees.Clear();
        }
    }
}
