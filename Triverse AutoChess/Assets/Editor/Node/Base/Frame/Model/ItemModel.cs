using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface IItemModel : IModel
    {
        public List<IItem> Items { get; }
    }
    public class ItemModel : AbstractModel, IItemModel
    {
        public List<IItem> Items { get; set; }

        protected override void OnInit()
        {
            Items = new List<IItem>();
        }
    }
}

