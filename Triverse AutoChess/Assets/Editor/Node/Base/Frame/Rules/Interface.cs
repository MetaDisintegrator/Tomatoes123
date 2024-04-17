using Game.Tool;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

namespace Editor.NodeEditor
{
    #region 读写
    public interface IDiagramData
    {
        void WriteDiagramData(FileStream fs);
    }
    public interface IVarDiagramData : IDiagramData
    {
        void ReadDiagramData(ByteArray BA);
    }
    public interface ISolverData
    {
        void WriteSolverData();
        void ReadSolverData();
    }
    #endregion

    #region 渲染和窗口编辑
    public interface IEventTarget
    {
        void ProcessEvent(Event e,ref bool reflected);
    }
    public interface INodeContent
    {
        /// <summary>
        /// 返回中心，计算底部
        /// </summary>
        /// <param name="currentBottom"></param>
        /// <returns></returns>
        float CalCenterHeight(ref float currentBottom);
        void Render(Vector2 centerPos);
        void OnDestroy();
    }
    public interface IZoneContent : IEventTarget
    {
        /// <summary>
        /// 全局
        /// </summary>
        public Rect Rect { get; }
        /// <summary>
        /// 全局
        /// </summary>
        public Vector2 Center { get; set; }
        void Render();
    }
    #endregion

    #region Builder
    public interface INodeBoxBuilder
    {
        INodeBoxBuilder BuildFather(NodeBoxElement father);
        INodeBoxBuilder BuildSize(Vector2 size);
        INodeBoxBuilder BuildPosition(Vector2 position);
        INodeBoxBuilder BuildTitle(string title);
        INodeBoxBuilder BuildStyles(GUIStyle normal, GUIStyle highlight);
        NodeBoxElement Complete();
    }
    public interface IZoneBuilder : INodeBoxBuilder
    {
        INodeBoxBuilder INodeBoxBuilder.BuildFather(NodeBoxElement father) { return this; }
        IZoneBuilder BuildType(int typeId);
        IZoneBuilder BuildID(int id);
        IZoneBuilder BuildFather(IZone father);
        new IZone Complete();
    }
    public interface INodeBuilder : INodeBoxBuilder
    {
        INodeBoxBuilder INodeBoxBuilder.BuildFather(NodeBoxElement father) { return this; }
        INodeBuilder BuildType(int typeId);
        INodeBuilder BuildID(int id);
        INodeBuilder BuildSO(string SoName);
        INodeBuilder BuildFather(IZone father);
        INodeBuilder BuildItem(IItem item);
        INodeBuilder BuildPoint(IPoint point);
        INodeBuilder SetSpecial(E_SpecialNode specialType);
        new INode Complete();
    }
    #endregion
}