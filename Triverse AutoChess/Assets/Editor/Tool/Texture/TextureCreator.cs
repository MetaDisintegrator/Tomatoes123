using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Editor.Texture
{
    public class TextureCreator : EditorWindow
    {
        //通用
        private Color color = Color.red;
        private Color backColor = Color.clear;
        private string textureName = "NewTexture";
        private (int width, int height) size = (100, 100);
        private (int width, int height) newSize = (100, 100);
        private Texture2D texture;
        //圆
        private float r;
        //菱形
        private float diamondR;
        //方向
        private float squareA;

        private void OnEnable()
        {
            texture = new Texture2D(size.width, size.height);
        }
        private void OnGUI()
        {
            //纹理名称
            textureName = EditorGUILayout.TextField(new GUIContent("纹理名称"),textureName);
            //宽高
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("纹理宽高");
            newSize.width = EditorGUILayout.IntField(new GUIContent("宽"), newSize.width);
            newSize.height = EditorGUILayout.IntField(new GUIContent("高"), newSize.height);
            EditorGUILayout.EndHorizontal();
            //颜色
            color = EditorGUILayout.ColorField(new GUIContent("前景色"),color);
            backColor = EditorGUILayout.ColorField(new GUIContent("后景色"), backColor);
            if (GUILayout.Button("交换"))
                SwitchColor();
            //新建
            if (GUILayout.Button("新建"))
                NewTexture();
            //填充
            if (GUILayout.Button("填充"))
                Fill();
            //圆
            r = EditorGUILayout.FloatField(new GUIContent("圆半径"), r);
            if (GUILayout.Button("圆形"))
                FillCircle(r);
            //菱形
            diamondR = EditorGUILayout.FloatField(new GUIContent("菱形半对角线"), diamondR);
            if (GUILayout.Button("菱形"))
                FillDiamond(diamondR);
            //方形
            squareA = EditorGUILayout.FloatField(new GUIContent("方形半边长"), squareA);
            if (GUILayout.Button("方形"))
                FillSquare(squareA);
            //保存
            if (GUILayout.Button("保存"))
            {
                if (texture != null)
                {
                    byte[] bytes = texture.EncodeToPNG();
                    using (FileStream fs = File.OpenWrite(Path.Combine(Application.dataPath, "Editor Default Resources", "Texture", "Simple", textureName + ".png")))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Flush();
                    }
                    AssetDatabase.Refresh();
                }
            }
            //查看图片
            GUILayout.Label(new GUIContent("预览:"));
            GUILayout.Label(new GUIContent(texture));

            if (GUI.changed)
            { 
                texture.Apply();
                Repaint();
            }
        }

        private void SwitchColor()
        {
            Color temp = color;
            color = backColor;
            backColor = temp;
        }
        private void NewTexture()
        {
            texture = new Texture2D(newSize.width, newSize.height);
            size = newSize;
        }
        private void Fill()
        {
            Color[] colors = new Color[size.width * size.height];
            for (int i = 0; i < size.width * size.height; i++)
            {
                colors[i] = color;
            }
            texture.SetPixels(0, 0, size.width, size.height, colors);
            GUI.changed = true;
        }
        private void FillCircle(float r)
        {
            Vector2 radiu;
            for (int i = 0; i < size.width; i++)
            {
                for (int j = 0; j < size.height; j++)
                {
                    radiu = new Vector2(i - (float)size.width / 2, j - (float)size.height / 2);
                    if (radiu.magnitude <= r)
                    {
                        texture.SetPixel(i, j, color);
                    }
                }
            }
            GUI.changed = true;
        }
        private void FillDiamond(float r)
        {
            float radiu;
            for (int i = 0; i < size.width; i++)
            {
                for (int j = 0; j < size.height; j++)
                {
                    radiu = Mathf.Abs(i - (float)size.width / 2) + Mathf.Abs(j - (float)size.height / 2);
                    if (radiu <= r)
                    {
                        texture.SetPixel(i, j, color);
                    }
                }
            }
            GUI.changed = true;
        }
        private void FillSquare(float a)
        {
            Vector2 radiu;
            for (int i = 0; i < size.width; i++)
            {
                for (int j = 0; j < size.height; j++)
                {
                    radiu = new Vector2(i - (float)size.width / 2, j - (float)size.height / 2);
                    radiu = radiu.Abs();
                    if (radiu.x <= a && radiu.y <=a)
                    {
                        texture.SetPixel(i, j, color);
                    }
                }
            }
            GUI.changed = true;
        }

        #region 唤起窗口
        [MenuItem("编辑器/纹理工具")]
        public static void OpenWindow()
        {
            TextureCreator win = EditorWindow.GetWindow<TextureCreator>("纹理工具");
            win.Show();
        }
        #endregion
    }
}

