using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface IConnectModel : IModel
    {
        public Dictionary<InPoint,IConnect> Connects { get; }
    }

    public class ConnectModel : AbstractModel, IConnectModel
    {
        /// <summary>
        /// ¼üÎªÖÕµã
        /// </summary>
        public Dictionary<InPoint, IConnect> Connects { get; set; }

        protected override void OnInit()
        {
            Connects = new Dictionary<InPoint, IConnect>();
        }
    }
}
