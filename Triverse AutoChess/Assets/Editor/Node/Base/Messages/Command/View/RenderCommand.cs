using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor.Command
{
    public class RenderCommand : AbstractCommand, ICommand
    {
        protected override void OnExecute()
        {
            //区和节点
            this.GetModel<IZoneModel>().DoPreOrder((zone) => zone.Render());
            //连接线
            foreach (var line in this.GetModel<IConnectModel>().Connects.Values)
            { 
                line.Render();
            }
            //待连接线
            IPoint selecting = this.GetSystem<IConnectSystem>().Selecting;
            if (selecting != null)
            {
                Vector2 start = selecting.Dir == E_PointDir.Out ? selecting.Center : Event.current.mousePosition;
                Vector2 end = selecting.Dir == E_PointDir.In ? selecting.Center : Event.current.mousePosition;

                Handles.DrawBezier(
                    start,
                    end,
                    start + Vector2.right * 50,
                    end + Vector2.left * 50,
                    this.GetSystem<IStyleFactorySystem>().GetColor(selecting.DataType),
                    null,
                    2
                    );
                GUI.changed = true;
            }
        }
    }
}

