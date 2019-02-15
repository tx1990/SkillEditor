using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SkillEditor
{
    public static class SkillHelper
    {
        public static Dictionary<SkillTriggerType, Type> SkillTriggerTypes;

        static SkillHelper()
        {
            SkillTriggerTypes = new Dictionary<SkillTriggerType, Type>();
            foreach (var value in Enum.GetValues(typeof(SkillTriggerType)))
            {
                var s = $"SkillEditor.SkillTrigger{value.ToString()}";
                var type = Type.GetType(s, true, true);
                SkillTriggerTypes.Add((SkillTriggerType) value, type);
            }
        }

        public static void XmlSerializeToFile<T>(T t, string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                var serializer = new XmlSerializer(t.GetType());

                var settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.NewLineChars = "\r\n";
                settings.Encoding = encoding;
                settings.IndentChars = "    ";

                settings.OmitXmlDeclaration = true;

                var nameSpaces = new XmlSerializerNamespaces();
                nameSpaces.Add(string.Empty, string.Empty);

                using (var writer = XmlWriter.Create(file, settings))
                {
                    serializer.Serialize(writer, t, nameSpaces);
                    writer.Close();
                }
            }
        }

        public static T XmlDeserializeFromFile<T>(string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            var xml = File.ReadAllText(path, encoding);
            var mySerializer = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream(encoding.GetBytes(xml)))
            {
                using (var sr = new StreamReader(ms, encoding))
                {
                    return (T) mySerializer.Deserialize(sr);
                }
            }
        }

        public static T LoadFromAssetName<T>(string name, params string[] searchFolders) where T : Object
        {
            var guid = UnityEditor.AssetDatabase.FindAssets(name, searchFolders);
            if (guid != null && guid.Length > 0)
            {
                for (int i = 0; i < guid.Length; i++)
                {
                    var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid[i]);
                    var fileName = Path.GetFileNameWithoutExtension(path);
                    if (fileName == name)
                    {
                        var t = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
                        if (t != null)
                        {
                            return t;
                        }
                    }
                }
            }

            return null;
        }

        public static T LoadFromResource<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public static Object LoadFromResource(string path, Type type)
        {
            return Resources.Load(path, type);
        }

        public static void CreateFile(string text, string path)
        {
            File.WriteAllText(path, text);
        }
    }
}
