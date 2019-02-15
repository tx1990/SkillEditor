using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace SkillEditor
{
    public class CreateScripeObject
    {
        [MenuItem("SkillEditor/CreateSkillObject")]
        public static void CreateSkillObject()
        {
            var builder = new StringBuilder();
            var types = SkillHelper.SkillTriggerTypes;
            foreach (var item in types)
            {
                builder.Clear();
                builder.Append("using System;\n");
                builder.Append("using System.Collections;\n");
                builder.Append("using System.Collections.Generic;\n");
                builder.Append("using UnityEngine;\n\n");
                builder.Append("namespace SkillEditor\n{\n");
                builder.Append($"\tpublic class {item.Value.Name}Object : ScriptableObject, IToSkillTriggerBase\n\t{{\n");

                builder.Append($"\t\t[Header(\"{item.Key.ToString()}\")]public {item.Value.Name} Trigger;\n\n");

                builder.Append($"\t\tpublic static explicit operator {item.Value.Name}Object(SkillTriggerBase t)\n\t\t{{\n");
                builder.Append($"\t\t\tif(!(t is {item.Value.Name} trigger)) return null;\n");
                builder.Append($"\t\t\tvar o = CreateInstance<{item.Value.Name}Object>();\n");
                builder.Append("\t\t\to.Trigger = trigger;\n");
                builder.Append("\t\t\treturn o;\n\t\t}\n\n");

                builder.Append("\t\tpublic SkillTriggerBase ToSkillTriggerBase()\n\t\t{\n");
                builder.Append("\t\t\treturn Trigger;\n\t\t}\n");

                builder.Append("\t}\n}");

                SkillHelper.CreateFile(builder.ToString(),
                    $"{Application.dataPath}/Editor/ScriptObject/{item.Value.Name}Object.cs");
            }

            AssetDatabase.Refresh();
        }
    }
}
