using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface IPoint : INodeContent
    {
        public IItem RelateItem { get; set; }
        public INode OwnerNode { get; set; }
        public E_PointDir Dir { get; }
        public E_NodeData DataType { get; }
        public E_NodeDataScale Scale { get; }
        public Vector2 Center { get; }
        public Action OnDestroyCallback { get; set; }
        void Connect(IPoint other);
        void Disconnect(IPoint other);
        void RemoveSelf();
    }

    public abstract class Point : NodeEditorElement, IPoint
    {
        static readonly float pointGap = 20;

        #region ��ͼ����
        public GUIStyle style;
        private Rect _rect = new Rect(0, 0, 15, 15);
        public Rect Rect { get => _rect; }
        public Vector2 Center => Rect.center;
        #endregion

        #region ͨ������
        public IItem RelateItem { get; set; }
        public INode OwnerNode { get; set; }
        public E_PointDir Dir { get; set; }
        public E_NodeData DataType { get; set; }
        public E_NodeDataScale Scale { get; set; }
        public abstract bool Connected { get; }
        #endregion

        #region ����
        public Point(E_PointDir dir,E_NodeData dataType,E_NodeDataScale scale)
        {
            this.Dir = dir;
            this.DataType = dataType;
            this.Scale = scale;
            style = this.GetSystem<IStyleFactorySystem>().GetGUIStyle(dataType, scale);
        }
        #endregion

        #region ��Ⱦ
        public float CalCenterHeight(ref float currentBottom)
        {
            float center = RelateItem != null ? RelateItem.CenterHeight : currentBottom + pointGap / 2;
            currentBottom = center + pointGap / 2;
            return center;
        }

        public void Render(Vector2 centerPos)
        {
            _rect.center = centerPos;
            if (GUI.Button(Rect, "", style))
            {
                OnClick();
            }
        }
        #endregion

        #region �¼�(����)
        private void OnClick()
        {
            IConnectSystem system = this.GetSystem<IConnectSystem>();
            if (system.Selecting == null)//��һ�����ӵ�
            {
                AsFirstSelected(system);
            }
            else
            {
                TryConnect(system.Selecting, system);
                system.Selecting = null;
            }
        }
        protected abstract void TryConnect(IPoint selectingPoint,IConnectSystem system);
        protected abstract void AsFirstSelected(IConnectSystem system);
        public abstract void Connect(IPoint other);
        public abstract void Disconnect(IPoint other);
        #endregion

        #region ����
        public Action OnDestroyCallback { get; set; }
        public abstract void OnDestroy();
        public void RemoveSelf()
        { 
            RelateItem?.RemoveSelf();
            DoRemoveSelf();
        }
        public abstract void DoRemoveSelf();
        #endregion
    }

    public class InPoint : Point
    {
        public override bool Connected => connected;
        public bool connected;
        public Action onConnect;
        public Action onDisConnect;

        public InPoint(E_NodeData dataType, E_NodeDataScale scale) : base(E_PointDir.In, dataType, scale)
        {
        }

        #region ����
        protected override void TryConnect(IPoint selecting,IConnectSystem system)
        {
            if (Scale == E_NodeDataScale.Single && selecting.Scale == E_NodeDataScale.Multiple) return;//��ģ
            if (selecting.Dir == Dir) return; //����
            if (!selecting.OwnerNode.CanConnect(OwnerNode)) return;//ѭ��
            if (DataType != selecting.DataType)//���
            {
                OnConnectOtherData(selecting as OutPoint);
                return;
            }
            //����������ɾ��
            if (connected)
                system.DisConnect(this);
            system.Connect(selecting as OutPoint, this);
        }
        protected virtual void OnConnectOtherData(OutPoint point) { }

        protected override void AsFirstSelected(IConnectSystem system)
        {
            //�����������ض���
            if (connected)
            {
                system.Selecting = this.GetModel<IConnectModel>().Connects[this].Start;
                system.DisConnect(this);
            }
            else
                system.Selecting = this;
        }

        public override void Connect(IPoint other)
        {
            connected = true;
            onConnect?.Invoke();
        }

        public override void Disconnect(IPoint other)
        {
            connected = false;
            onDisConnect?.Invoke();
        }
        #endregion

        public override void OnDestroy()
        {
            this.OnDestroyCallback?.Invoke();
            this.GetSystem<IConnectSystem>().DisConnect(this);
        }

        public override void DoRemoveSelf()
        {
            OnDestroy();
            OwnerNode.InPoints.Remove(this);
        }
    }

    public class OutPoint : Point
    {
        public List<InPoint> targets;
        public override bool Connected => targets.Count > 0;

        public OutPoint(E_NodeData dataType, E_NodeDataScale scale) : base(E_PointDir.Out, dataType, scale)
        {
            targets = new List<InPoint>();
        }

        #region ����
        protected override void AsFirstSelected(IConnectSystem system)
        {
            system.Selecting = this;
        }

        protected override void TryConnect(IPoint selecting, IConnectSystem system)
        {
            if (Scale == E_NodeDataScale.Multiple && selecting.Scale == E_NodeDataScale.Single) return;//��ģ
            if (selecting.Dir == Dir) return; //����
            if (!OwnerNode.CanConnect(selecting.OwnerNode)) return;//ѭ��
            if (DataType != selecting.DataType) return;//��� 
            system.Connect(this, selecting as InPoint);
        }

        public override void Connect(IPoint other)
        {
            targets.Add(other as InPoint);
        }
        public override void Disconnect(IPoint other)
        {
            targets.Remove(other as InPoint);
        }
        #endregion

        public override void OnDestroy()
        {
            this.OnDestroyCallback?.Invoke();
            IConnectSystem system = this.GetSystem<IConnectSystem>();
            while (targets.Count > 0)
            {
                system.DisConnect(targets[0]);
            }
        }

        public override void DoRemoveSelf()
        {
            OnDestroy();
            OwnerNode.OutPoints.Remove(this);
        }
    }

    public class UnknowPoint : InPoint
    {
        public UnknowPoint() : base(E_NodeData.UnKnown, E_NodeDataScale.Single) { }

        protected override void AsFirstSelected(IConnectSystem system)
        {
            return;
        }

        protected override void OnConnectOtherData(OutPoint point)
        {
            InPoint inPoint = OwnerNode.OwnerZone.HandleSpecialConnect(point,OwnerNode.SpecialType);
            this.GetSystem<IConnectSystem>().Connect(point, inPoint);
        }
    }
}

