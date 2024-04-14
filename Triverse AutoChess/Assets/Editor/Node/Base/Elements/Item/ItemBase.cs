using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface IItem : INodeContent
    {
        public float Width { get; set; }
        public float CenterHeight { get; }
        public List<Point> Points { get; }
        void RequirePoints(INodeBuilder builder);
        void RemoveSelf();
    }

    public abstract class Item : NodeEditorElement, IItem
    {
        #region 绘图数据
        private Rect rect;
        private string title;
        private float connHeight;
        private float deConnHeight;
        public float Width { get => rect.width; set => rect.width = value; }
        protected float Height { get => rect.height; set => rect.height = value; }
        public float CenterHeight { get; set; }
        private bool connected;
        #endregion

        #region 渲染
        public float CalCenterHeight(ref float currentBottom)
        {
            currentBottom += Height;
            return currentBottom - Height / 2;
        }
        public void Render(Vector2 centerPos)
        { 
            rect.center = centerPos;
            CenterHeight = centerPos.y;
            Height = connected?connHeight: deConnHeight;
            GUILayout.BeginArea(rect);
            GUILayout.Label(title);
            OnRender(connected);
            GUILayout.EndArea();
        }
        #endregion

        #region 构造
        public List<Point> Points { get; set; }
        public Item(string title, float connHeight = 20, float deConnHeight = 40)
        { 
            this.title = title;
            this.connHeight = connHeight;
            this.deConnHeight = deConnHeight;
            this.Height = deConnHeight;
            this.Points = new List<Point>();
        }
        public abstract void RequirePoints(INodeBuilder builder);
        protected void DoRequirePoint(INodeBuilder builder,InPoint inPoint)
        {
            builder.BuildPoint(inPoint);
            inPoint.RelateItem = this;
            inPoint.onConnect = () => { connected = true; };
            inPoint.onDisConnect = () => { connected = false; };
            Points.Add(inPoint);
        }
        protected void DoRequirePoint(INodeBuilder builder, OutPoint outPoint)
        {
            builder.BuildPoint(outPoint);
            outPoint.RelateItem = this;
            Points.Add(outPoint);
        }
        #endregion

        /// <summary>
        /// 具体条目内容
        /// </summary>
        protected abstract void OnRender(bool connected);

        public virtual void OnDestroy() { }

        public void RemoveSelf()
        {
            Points[0].OwnerNode.Items.Remove(this);
        }
    }
}

