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
        //ͨ��
        private Color color = Color.red;
        private Color backColor = Color.clear;
        private string textureName = "NewTexture";
        private (int width, int height) size = (100, 100);
        private (int width, int height) newSize = (100, 100);
        private Texture2D texture;
        //Բ
        private float r;
        //����
        private float diamondR;
        //����
        private float squareA;

        private void OnEnable()
        {
            texture = new Texture2D(size.width, size.height);
        }
        private void OnGUI()
        {
            //��������
            textureName = EditorGUILayout.TextField(new GUIContent("��������"),textureName);
            //���
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("������");
            newSize.width = EditorGUILayout.IntField(new GUIContent("��"), newSize.width);
            newSize.height = EditorGUILayout.IntField(new GUIContent("��"), newSize.height);
            EditorGUILayout.EndHorizontal();
            //��ɫ
            color = EditorGUILayout.ColorField(new GUIContent("ǰ��ɫ"),color);
            backColor = EditorGUILayout.ColorField(new GUIContent("��ɫ"), backColor);
            if (GUILayout.Button("����"))
                SwitchColor();
            //�½�
            if (GUILayout.Button("�½�"))
                NewTexture();
            //���
            if (GUILayout.Button("���"))
                Fill();
            //Բ
            r = EditorGUILayout.FloatField(new GUIContent("Բ�뾶"), r);
            if (GUILayout.Button("Բ��"))
                FillCircle(r);
            //����
            diamondR = EditorGUILayout.FloatField(new GUIContent("���ΰ�Խ���"), diamondR);
            if (GUILayout.Button("����"))
                FillDiamond(diamondR);
            //����
            squareA = EditorGUILayout.FloatField(new GUIContent("���ΰ�߳�"), squareA);
            if (GUILayout.Button("����"))
                FillSquare(squareA);
            //����
            if (GUILayout.Button("����"))
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
            //�鿴ͼƬ
            GUILayout.Label(new GUIContent("Ԥ��:"));
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

        #region ���𴰿�
        [MenuItem("�༭��/������")]
        public static void OpenWindow()
        {
            TextureCreator win = EditorWindow.GetWindow<TextureCreator>("������");
            win.Show();
        }
        #endregion
    }
}

