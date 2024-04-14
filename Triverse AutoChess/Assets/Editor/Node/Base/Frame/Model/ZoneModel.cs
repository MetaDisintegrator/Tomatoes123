using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Antlr3.Runtime.Tree;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class ZoneTreeNode
    {
        public ZoneTreeNode parentZone;
        public IZone value;
        public List<ZoneTreeNode> childs;

        public ZoneTreeNode(IZone value, ZoneTreeNode parent = null)
        { 
            this.value = value;
            this.parentZone = parent;
        }

        public bool HasChild => childs != null && childs.Count > 0;
    }

    public interface IZoneModel : IModel
    {
        public List<ZoneTreeNode> Trees { get; }
        /// <summary>
        /// 后序遍历
        /// </summary>
        /// <param name="action"></param>
        void DoPostOrder(Action<IZone> action);
        /// <summary>
        /// 后序遍历，自定根节点
        /// </summary>
        /// <param name="action"></param>
        void DoPostOrder(Action<IZone> action, ZoneTreeNode root);
        /// <summary>
        /// 后序遍历，每个根节点下的字节点为一组
        /// </summary>
        /// <param name="action"></param>
        void DoPostOrder(Action<IEnumerable<IZone>,IZone> action);
        /// <summary>
        /// 前序遍历
        /// </summary>
        /// <param name="action"></param>
        void DoPreOrder(Action<IZone> action);
        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ZoneTreeNode FindNode(int id);
    }

    public class ZoneModel : AbstractModel, IZoneModel
    {
        public List<ZoneTreeNode> Trees { get; set; }

        #region 遍历方法
        public void DoPostOrder(Action<IZone> action)
        {
            for (int i = Trees.Count - 1; i >= 0; i--)
            {
                PostOrderTree(Trees[i]);
            }

            void PostOrderTree(ZoneTreeNode node)
            {
                if (node.HasChild)
                {
                    foreach (var subTree in node.childs)
                        PostOrderTree(subTree);
                }
                action?.Invoke(node.value);
            }
        }
        public void DoPostOrder(Action<IZone> action, ZoneTreeNode root)
        {
            PostOrderTree(root);
            void PostOrderTree(ZoneTreeNode node)
            {
                if (node.HasChild)
                {
                    foreach (var subTree in node.childs)
                        PostOrderTree(subTree);
                }
                action?.Invoke(node.value);
            }
        }

        public void DoPostOrder(Action<IEnumerable<IZone>,IZone> action)
        {
            List<IZone> rootZones = ListPool<IZone>.Get();
            for (int i = Trees.Count - 1; i >= 0; i--)
            {
                rootZones.Add(Trees[i].value);
                PostOrderTree(Trees[i]);
            }

            action?.Invoke(rootZones,null);
            ListPool<IZone>.Release(rootZones);

            void PostOrderTree(ZoneTreeNode node)
            {
                if (node.HasChild)
                {
                    List<IZone> zones = ListPool<IZone>.Get();
                    foreach (var subTree in node.childs)
                    {
                        zones.Add(subTree.value);
                        PostOrderTree(subTree);
                    }
                    action?.Invoke(zones, node.value);
                    ListPool<IZone>.Release(zones);
                }
                else
                {
                    action?.Invoke(null, node.value);
                }
            }
        }

        public void DoPreOrder(Action<IZone> action)
        {
            foreach (var tree in Trees)
            { 
                PreOrderTree(tree);
            }

            void PreOrderTree(ZoneTreeNode node)
            {
                action?.Invoke(node.value);
                if (!node.HasChild) return;
                foreach (var subTree in node.childs)
                    PreOrderTree(subTree);
            }
        }
        #endregion

        public ZoneTreeNode FindNode(int id)
        { 
            ZoneTreeNode res = null;
            foreach(var tree in Trees) 
            {
                if (FindInTree(tree)) break;
            }
            return res;

            bool FindInTree(ZoneTreeNode node)
            {
                if (node.value.ID.Equals(id))
                { 
                    res = node;
                    return true;
                }
                else if (node.HasChild)
                    foreach (var subTree in node.childs)
                    { 
                        if(FindInTree(subTree)) return true;
                    }
                return false;
            }
        }

        protected override void OnInit()
        {
            Trees = new List<ZoneTreeNode>();
        }
    }
}

