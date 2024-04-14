using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface IConnect
    {
        public OutPoint Start { get; }
        public InPoint End { get; }
        void Render();
    }
    public class Connect : NodeEditorElement, IConnect
    {
        public OutPoint Start { get; set; }
        public InPoint End { get; set; }
        Color color;

        public Connect(OutPoint start, InPoint end)
        {
            Start = start;
            End = end;
            color = this.GetSystem<IStyleFactorySystem>().GetColor(start.DataType);
        }

        public void Render()
        {
            Handles.DrawBezier(
                Start.Center,
                End.Center,
                Start.Center + Vector2.right * 50,
                End.Center + Vector2.left * 50,
                color,
                null,
                2
                );
        }
    }
}

