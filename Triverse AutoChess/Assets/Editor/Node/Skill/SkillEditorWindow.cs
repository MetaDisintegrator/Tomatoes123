using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor
{
    public partial class SkillEditorWindow : Window<SkillEditorWindow>
    {
        [MenuItem("�༭��/���ܱ༭��")]
        public static void Open()
        {
            DoOpenWindow("���ܱ༭��");
        }
    }
}
