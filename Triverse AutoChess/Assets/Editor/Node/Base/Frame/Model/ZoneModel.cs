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
        /// �������
        /// </summary>
        /// <param name="action"></param>
        void DoPostOrder(Action<IZone> action);
        /// <summary>
        /// ����������Զ����ڵ�
        /// </summary>
        /// <param name="action"></param>
        void DoPostOrder(Action<IZone> action, ZoneTreeNode root);
        /// <summary>
        /// ���������ÿ�����ڵ��µ��ֽڵ�Ϊһ��
        /// </summary>
        /// <param name="action"></param>
        void DoPostOrder(Action<IEnumerable<IZone>,IZone> action);
        /// <summary>
        /// ǰ�����
        /// </summary>
        /// <param name="action"></param>
        void DoPreOrder(Action<IZone> action);
        /// <summary>
        /// ���ҽڵ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ZoneTreeNode FindNode(int id);
    }

    public class ZoneModel : AbstractModel, IZoneModel
    {
        public List<ZoneTreeNode> Trees { get; set; }

        #region ��������
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

