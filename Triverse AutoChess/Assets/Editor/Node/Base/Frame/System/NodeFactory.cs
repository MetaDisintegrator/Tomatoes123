using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Editor.NodeEditor
{
    public interface INodeFactorySystemInit : ISystem
    {
        public int NodeIdGen { get; }
        public int ZoneIdGen { get; }
        void RegisterNode(int typeId, Func<INodeBuilder,INodeBuilder> builder, E_SpecialNode specialType);
        void RegisterZone(int typeId, Func<IZoneBuilder, IZoneBuilder> builder);
    }
    public interface INodeFactorySystem:INodeFactorySystemInit
    {
        IZone GenerateZone(int typeId, IZone parent, Vector2 pos);
        INode GenerateNode(int typeId, IZone ownerZone, Vector2 pos);
        IZone GenerateZoneWithID(int typeId, IZone parent, Vector2 pos, int ID);
        INode GenerateNodeWithID(int typeId, IZone ownerZone, Vector2 pos, int ID);
        public void SetIDGen(int zone, int node);
    }

    public static class NodeFactoryInitExtention
    {
        public static Func<INodeBuilder,INodeBuilder> QBuildNode(this INodeFactorySystemInit self,string title, string SoName, 
            string normalStyleName = "NodeNormal", string highlightStyleName = "NodeHighlight", float width = 160)
        {
            IStyleFactorySystem styleFactory = self.GetSystem<IStyleFactorySystem>();
            return (builder) =>
            {
                builder
                    .BuildID(self.NodeIdGen)
                    .BuildSO(SoName)
                    .BuildSize(new Vector2(width, 0))
                    .BuildTitle(title)
                    .BuildStyles(styleFactory.GetGUIStyle(normalStyleName), styleFactory.GetGUIStyle(highlightStyleName));
                return builder;
            };
        }

        public static Func<IZoneBuilder,IZoneBuilder> QZoneBuilder(this INodeFactorySystemInit self, string title = "",
            string normalStyleName = "ZoneNormal", string highlightStyleName = "ZoneHighlight", float width = 200, float height = 200)
        {
            IStyleFactorySystem styleFactory = self.GetSystem<IStyleFactorySystem>();
            return (builder) =>
            {
                builder
                    .BuildID(self.ZoneIdGen)
                    .BuildSize(new Vector2(width, height))
                    .BuildTitle(title)
                    .BuildStyles(styleFactory.GetGUIStyle(normalStyleName), styleFactory.GetGUIStyle(highlightStyleName));
                return builder;
            };
        }
    }

    public class NodeFactorySystem : AbstractSystem, INodeFactorySystem
    {
        #region Node
        Dictionary<int, Func<IZone, Vector2, INode>> nodeFactory;
        int _nodeIdGen;
        public int NodeIdGen { get => _nodeIdGen++; set => _nodeIdGen = value; }

        
        public INode GenerateNode(int typeId, IZone ownerZone, Vector2 pos)
        {
            INode res = CreateNode(typeId, ownerZone, pos);
            AddNode(ownerZone.ID, res);
            return res;
        }
        public INode GenerateNodeWithID(int typeId, IZone ownerZone, Vector2 pos, int ID)
        {
            INode res = CreateNode(typeId, ownerZone, pos);
            (res as INodeBuilder).BuildID(ID);
            AddNode(ownerZone.ID, res);
            return res;
        }
        private INode CreateNode(int typeId, IZone ownerZone, Vector2 pos)
        {
            INode res = null;
            if (!nodeFactory.ContainsKey(typeId))
                Debug.LogError("节点类型ID不存在");
            else
            {
                res = nodeFactory[typeId].Invoke(ownerZone, pos);
            }
            return res;
        }
        private void AddNode(int parentId, INode node)
        { 
            IZone zone = this.GetModel<IZoneModel>().FindTreeNode(parentId).value;
            zone.Nodes.Add(node);
            this.GetModel<INodeModel>().Nodes.Add(node.ID, node);
        }
        
        void INodeFactorySystemInit.RegisterNode(int typeId, Func<INodeBuilder,INodeBuilder> builder, E_SpecialNode specialType)
        {
            if (nodeFactory.ContainsKey(typeId))
                Debug.LogError("节点类型ID重复"+typeId);
            else
            {
                nodeFactory.Add(typeId, (zone, pos) => 
                {
                    INodeBuilder obj = new Node();
                    IStyleFactorySystem styleFactory = this.GetSystem<IStyleFactorySystem>();
                    builder(obj)
                        .BuildType(typeId)
                        .BuildFather(zone)
                        .SetSpecial(specialType)
                        .BuildPosition(pos);
                    return obj.Complete();
                });
            }
        }
        #endregion

        #region Zone
        Dictionary<int, Func<IZone, Vector2, IZone>> zoneFactory;
        int _zoneIdGen;
        public int ZoneIdGen { get => _zoneIdGen++; set => _zoneIdGen = value; }

        public IZone GenerateZone(int typeId, IZone parent, Vector2 pos)
        {
            IZone res = zoneFactory[typeId].Invoke(parent, pos);
            AddZone(parent == null ? -1 : parent.ID, res);
            return res;
        }
        public IZone GenerateZoneWithID(int typeId, IZone parent, Vector2 pos, int ID)
        {
            IZone res = zoneFactory[typeId].Invoke(parent, pos);
            (res as IZoneBuilder).BuildID(ID);
            AddZone(parent == null ? -1 : parent.ID, res);
            return res;
        }
        private void AddZone(int parentId, IZone zone)
        {
            IZoneModel model = this.GetModel<IZoneModel>();
            ZoneTreeNode parentNode = model.FindTreeNode(parentId);
            if (parentNode == null)
                model.Trees.Add(new ZoneTreeNode(zone));
            else
            {
                parentNode.childs ??= new List<ZoneTreeNode>();
                parentNode.childs.Add(new ZoneTreeNode(zone, parentNode));
            }
        }

        public void RegisterZone(int typeId, Func<IZoneBuilder,IZoneBuilder> builder)
        {
            if (zoneFactory.ContainsKey(typeId))
                Debug.LogError("区域类型ID重复" + typeId);
            else
            {
                zoneFactory.Add(typeId, (zone, pos) =>
                {
                    IZoneBuilder obj = new Zone();
                    builder.Invoke(obj)
                        .BuildType(typeId)
                        .BuildFather(zone)
                        .BuildPosition(pos);
                    return obj.Complete();
                });
            }
        }
        #endregion

        #region Init
        protected override void OnInit()
        {
            nodeFactory = new Dictionary<int, Func<IZone, Vector2, INode>>();
            zoneFactory = new Dictionary<int, Func<IZone, Vector2, IZone>>();
            NodeIdGen = 1;
            ZoneIdGen = 1;
        }
        protected override void OnDeinit()
        {
            base.OnDeinit();
        }
        #endregion

        public void SetIDGen(int zone, int node)
        {
            ZoneIdGen = zone;
            NodeIdGen = node;
        }
    }
}
