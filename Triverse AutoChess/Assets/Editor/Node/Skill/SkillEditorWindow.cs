using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.NodeEditor
{
    public partial class SkillEditorWindow : Window<SkillEditorWindow>
    {
        [MenuItem("±à¼­Æ÷/¼¼ÄÜ±à¼­Æ÷")]
        public static void Open()
        {
            DoOpenWindow("¼¼ÄÜ±à¼­Æ÷");
        }
        protected override void InitPath(IDiagramDirectoryModel model)
        {
            model.FilePath = Path.Combine(Application.dataPath, "Editor", "Data", "Skill");
        }
    }
}
