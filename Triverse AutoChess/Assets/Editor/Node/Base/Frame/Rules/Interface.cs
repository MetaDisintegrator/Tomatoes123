using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

namespace Editor.NodeEditor
{
    #region ��д
    public interface IRenderData
    {
        void WriteRenderData();
        void ReadRenderData();
    }
    public interface ISolverData
    {
        void WriteSolverData();
        void ReadSolverData();
    }
    #endregion

    #region ��Ⱦ�ʹ��ڱ༭
    public interface IEventTarget
    {
        void ProcessEvent(Event e,ref bool reflected);
    }
    public interface INodeContent
    {
        /// <summary>
        /// �������ģ�����ײ�
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
        /// ȫ��
        /// </summary>
        public Rect Rect { get; }
        /// <summary>
        /// ȫ��
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
        INodeBuilder BuildToken(bool isSpecial);
        INodeBuilder BuildFather(IZone father);
        INodeBuilder BuildSecondZone(IZone secondZone);
        INodeBuilder BuildItem(IItem item);
        INodeBuilder BuildPoint(IPoint point);
        INodeBuilder SetAsEntrance();
        INodeBuilder SetAsCycleEntrance();
        new INode Complete();
    }
    #endregion
}