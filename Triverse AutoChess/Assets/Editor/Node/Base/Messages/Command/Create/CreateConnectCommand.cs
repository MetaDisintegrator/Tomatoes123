using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class CreateConnectCommand : AbstractCommand, ICommand
    {
        IPoint start;
        IPoint end;
        protected override void OnExecute()
        {
            end.TryConnect(start, this.GetSystem<IConnectSystem>());
        }
        public void Set(IPoint start, IPoint end)
        { 
            this.start = start;
            this.end = end;
        }
    }
}
