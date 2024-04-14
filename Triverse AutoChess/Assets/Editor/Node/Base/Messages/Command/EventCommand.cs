using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor.Command
{
    public class EventCommand : AbstractCommand<bool>, ICommand<bool>
    {
        public Event e;
        public bool reflected;
        protected override bool OnExecute()
        {
            this.GetModel<IZoneModel>().DoPostOrder((zone) => zone.ProcessEvent(e, ref reflected));
            return reflected;
        }
        public void Set(Event e)
        { 
            this.e = e;
            reflected = false;
        }
    }
}

