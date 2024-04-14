using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface IStyleFactorySystemInit : ISystem
    {
        void RegisterBoxStyle(string name, string resName);
        void RegisterPointStyle(E_NodeData dataType, E_NodeDataScale scale, string resName);
        void RegisterPointStyle(E_NodeData dataType, E_NodeDataScale scale);
        void RegisterColor(E_NodeData dataType, string htmlString);
    }
    public interface IStyleFactorySystem : IStyleFactorySystemInit
    {
        GUIStyle GetGUIStyle(string name);
        GUIStyle GetGUIStyle(E_NodeData dataType, E_NodeDataScale scale);
        Color GetColor(E_NodeData dataType);
    }
    public class StyleFactorySystem : AbstractSystem, IStyleFactorySystem
    {
        private readonly string SimplePath = "Texture/Simple/";
        private StringBuilder nameBuilder;
        Dictionary<string, GUIStyle> styles;
        Dictionary<E_NodeData, Color> colors;
        protected override void OnInit()
        {
            nameBuilder = new StringBuilder();
            styles = new Dictionary<string, GUIStyle>();
            colors = new Dictionary<E_NodeData, Color>();
        }

        public GUIStyle GetGUIStyle(string name)
        {
            if (!styles.ContainsKey(name))
                Debug.LogError("Style名称不存在");
            else
                return styles[name];
            return null;
        }

        public GUIStyle GetGUIStyle(E_NodeData dataType, E_NodeDataScale scale)
        {
            return GetGUIStyle(BuildName(dataType, scale));
        }

        public Color GetColor(E_NodeData dataType)
        {
            if (!colors.ContainsKey(dataType))
                Debug.LogError("颜色名称不存在");
            else
                return colors[dataType];
            return Color.white;
        }

        void IStyleFactorySystemInit.RegisterBoxStyle(string name,string resName)
        {
            GUIStyle style = new GUIStyle();
            style.normal.background = DoLoad<Texture2D>(SimplePath + resName + ".png");
            style.alignment = TextAnchor.UpperCenter;
            RegisterStyle(name, style);
        }
        void IStyleFactorySystemInit.RegisterPointStyle(E_NodeData dataType, E_NodeDataScale scale, string resName)
        {
            GUIStyle style = new GUIStyle();
            style.normal.background = DoLoad<Texture2D>(SimplePath + resName + ".png");

            RegisterStyle(BuildName(dataType, scale), style);
        }
        void IStyleFactorySystemInit.RegisterPointStyle(E_NodeData dataType, E_NodeDataScale scale) 
            => (this as IStyleFactorySystemInit).RegisterPointStyle(dataType, scale, BuildName(dataType, scale));
        void IStyleFactorySystemInit.RegisterColor(E_NodeData dataType,string htmlString)
        {
            if (colors.ContainsKey(dataType))
                Debug.LogError("颜色名称冲突");
            else
            {
                colors.Add(dataType, ColorUtility.TryParseHtmlString(htmlString, out Color temp) ? temp : Color.black);
            }
        }

        #region 内部
        private void RegisterStyle(string name, GUIStyle style)
        {
            if (styles.ContainsKey(name))
                Debug.LogError("Style名称冲突");
            else
                styles.Add(name, style);
        }
        private string BuildName(E_NodeData dataType, E_NodeDataScale scale)
        {
            nameBuilder.Clear();
            nameBuilder.Append(dataType.ToString("G"));
            nameBuilder.Append(scale.ToString("G"));
            return nameBuilder.ToString();
        }
        private T DoLoad<T>(string path) where T : class
        {
            return EditorGUIUtility.LoadRequired(path) as T;
        }
        #endregion
    }
}

