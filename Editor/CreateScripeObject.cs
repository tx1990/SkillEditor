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
                builder.Append("using UnityEngine;\n\n");
                builder.Append("namespace SkillEditor\n{\n");
                builder.Append($"\tpublic class {item.Value.Name}Object : SkillTriggerObjectBase\n\t{{\n");

                builder.Append($"\t\t[Header(\"{item.Key.ToString()}\")]public {item.Value.Name} Trigger;\n\n");

                builder.Append("\t\tpublic override void SetTriggerBase(SkillTriggerBase t)\n\t\t{\n");
                builder.Append($"\t\t\tif(!(t is {item.Value.Name} trigger)) return;\n");
                builder.Append("\t\t\tTrigger = trigger;\n\t\t}\n\n");

                builder.Append("\t\tpublic override SkillTriggerBase GetTriggerBase()\n\t\t{\n");
                builder.Append("\t\t\treturn Trigger;\n\t\t}\n");

                builder.Append("\t}\n}");

                SkillHelper.CreateFile(builder.ToString(),
                    $"{Application.dataPath}/Editor/ScriptObject/{item.Value.Name}Object.cs");
            }

            builder.Clear();
            builder.Append("using System.Collections.Generic;\n");
            builder.Append("using System.Xml.Serialization;\n\n");
            builder.Append("namespace SkillEditor\n{\n");
            builder.Append("\tpublic partial class SkillEntity\n\t{\n");
            builder.Append("\t\t[");
            foreach (var item in types)
            {
                builder.Append($"XmlArrayItem(typeof({item.Value.Name})),");
            }
            builder.Append("]\n");
            builder.Append("\t\tpublic List<SkillTriggerBase> SkillTriggers { get; private set; }\n");
            builder.Append("\t}\n}");
            SkillHelper.CreateFile(builder.ToString(), $"{Application.dataPath}/Script/SkillEntity2.cs");

            AssetDatabase.Refresh();
        }
    }
}
