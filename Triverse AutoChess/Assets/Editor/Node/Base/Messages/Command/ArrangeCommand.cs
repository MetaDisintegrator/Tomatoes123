using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public class ArrangeCommand : AbstractCommand, ICommand
    {
        protected override void OnExecute()
        {
            this.GetModel<IZoneModel>().DoPostOrder((zones, parent) => 
            {
                List<NodeBoxElement> boxs = ListPool<NodeBoxElement>.Get();
                if (zones != null)
                    foreach (var zone in zones)
                    {
                        boxs.Add(zone as Zone);
                    }
                if (parent != null)
                    foreach (var node in parent.Nodes)
                    {
                        boxs.Add(node as Node);
                    }
                ArrangeRectsWithoutOverlap(boxs);
                if (parent != null)
                    ResizeParent(boxs, parent as Zone);
                ListPool<NodeBoxElement>.Release(boxs);
            });
        }

        bool DoRectsOverlap(Rect rect1, Rect rect2)
        {
            return rect1.xMin < rect2.xMax &&
                   rect1.xMax > rect2.xMin &&
                   rect1.yMin < rect2.yMax &&
                   rect1.yMax > rect2.yMin;
        }
        void ArrangeRectsWithoutOverlap(List<NodeBoxElement> rects)
        {
            for (int i = 0; i < rects.Count; i++)
            {
                Rect currentRect = rects[i].Rect;
                for (int j = 0; j < rects.Count; j++)
                {
                    if (i != j)
                    {
                        Rect otherRect = rects[j].Rect;
                        if (DoRectsOverlap(currentRect, otherRect))
                        {
                            // 尝试移动当前矩形以避免重叠  
                            rects[i].Center += (rects[i].Center - rects[j].Center).normalized * Time.deltaTime;
                            break; 
                        }
                    }
                }
            }
        }
        void ResizeParent(List<NodeBoxElement> childs,NodeBoxElement parent)
        {
            Vector2 lastPos = parent.Rect.position;
            if(childs==null|| childs.Count==0) return;
            Vector2 min = childs[0].Min;
            Vector2 max = childs[0].Max;
            foreach (NodeBoxElement child in childs)
            { 
                min = Vector2.Min(min, child.Min);
                max = Vector2.Max(max, child.Max);
            }
            parent.Min = min - Vector2.one * 30;
            parent.Max = max + Vector2.one * 30;
            //子元素绝对坐标保持不变
            foreach (NodeBoxElement child in childs)
            { 
                child.Center = child.Center;
            }
        }
    }
}

