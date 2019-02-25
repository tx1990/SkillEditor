using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace SkillEditor
{
    public class SkillEditor : EditorWindow
    {
        private List<SerializedObject> m_objects;

        private float m_lifeTime = 5;
        private Vector2 m_scrollPos;
        private SkillTriggerType m_curType;
        private int m_deleteIndex = -1;

        private readonly string m_defaultPath = "/Config";
        private string m_filePath = string.Empty;

        [MenuItem("SkillEditor/SkillEditor")]
        static void Init()
        {
            var window =
                (SkillEditor) EditorWindow.GetWindow(typeof(SkillEditor), false, "SkillEditor");
            if (window != null)
            {
                window.Show();
            }
        }

        void OnEnable()
        {
            m_objects = new List<SerializedObject>();
        }

        void OnGUI()
        {
            ShowEditGui();
            ShowSkillEntityGui();
        }

        private void ShowEditGui()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("New"))
            {
                m_filePath = EditorUtility.SaveFilePanel("New",
                    Application.dataPath + m_defaultPath, "", "xml");
                m_objects.Clear();
                var entity = new SkillEntity { LifeTime = m_lifeTime };
                SkillHelper.XmlSerializeToFile(entity, m_filePath, Encoding.UTF8);
            }
            if (GUILayout.Button("Save"))
            {
                var entity = new SkillEntity { LifeTime = m_lifeTime };
                for (int i = 0, count = m_objects.Count; i < count; i++)
                {
                    if (m_objects[i].targetObject is SkillTriggerObjectBase objectBase)
                    {
                        entity.SkillTriggers.Add(objectBase.GetTriggerBase());
                    }
                }

                SkillHelper.XmlSerializeToFile(entity, m_filePath, Encoding.UTF8);
            }
            if (GUILayout.Button("Save as"))
            {
                m_filePath = EditorUtility.SaveFilePanel("Save As...",
                    Application.dataPath + m_defaultPath, "", "xml");

                var entity = new SkillEntity { LifeTime = m_lifeTime };
                for (int i = 0, count = m_objects.Count; i < count; i++)
                {
                    if (m_objects[i].targetObject is SkillTriggerObjectBase objectBase)
                    {
                        entity.SkillTriggers.Add(objectBase.GetTriggerBase());
                    }
                }

                SkillHelper.XmlSerializeToFile(entity, m_filePath, Encoding.UTF8);
            }
            if (GUILayout.Button("Load"))
            {
                m_filePath = EditorUtility.OpenFilePanel("Load",
                    Application.dataPath + m_defaultPath, "xml");
                var entity = SkillHelper.XmlDeserializeFromFile<SkillEntity>(m_filePath, Encoding.UTF8);
                m_objects.Clear();
                foreach (var trigger in entity.SkillTriggers)
                {
                    var s = $"SkillEditor.SkillTrigger{trigger.TriggerType.ToString()}Object";
                    var o = CreateInstance(s);
                    if (o is SkillTriggerObjectBase objectBase)
                    {
                        objectBase.SetTriggerBase(trigger);
                        m_objects.Add(new SerializedObject(objectBase));
                    }
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
            {
                var s = $"SkillEditor.SkillTrigger{m_curType.ToString()}Object";
                var o = CreateInstance(s);
                if (o != null)
                {
                    m_objects.Add(new SerializedObject(o));
                }
            }

            m_curType = (SkillTriggerType) EditorGUILayout.EnumPopup(m_curType);
            EditorGUILayout.EndHorizontal();
        }

        private void ShowSkillEntityGui()
        {
            m_lifeTime = EditorGUILayout.FloatField("LifeTime", m_lifeTime);

            m_scrollPos = EditorGUILayout.BeginScrollView(m_scrollPos);

            for (int i = 0, count = m_objects.Count; i < count; i++)
            {
                EditorGUILayout.BeginVertical("box");

                EditorGUILayout.BeginHorizontal();

                var trigger = m_objects[i].FindProperty("Trigger");

                EditorGUILayout.BeginVertical("box");

                if (GUILayout.Button("X", GUILayout.ExpandWidth(false), GUILayout.Width(400)))
                {
                    m_deleteIndex = i;
                }

                EditorGUILayout.PropertyField(trigger, true, GUILayout.ExpandWidth(false), GUILayout.Width(400));
                m_objects[i].ApplyModifiedProperties();

                EditorGUILayout.EndVertical();

                var startTimeProp = trigger.FindPropertyRelative("StartTime");
                var endTimeProp = trigger.FindPropertyRelative("EndTime");
                var startTime = startTimeProp.floatValue;
                var endTime = endTimeProp.floatValue;
                EditorGUILayout.MinMaxSlider(ref startTime, ref endTime, 0, m_lifeTime, GUILayout.ExpandWidth(true));
                startTime = Mathf.Clamp(startTime, 0, m_lifeTime);
                endTime = Mathf.Clamp(endTime, startTime, m_lifeTime);
                startTimeProp.floatValue = startTime;
                endTimeProp.floatValue = endTime;

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();

                EditorGUILayout.Space();
            }

            if (m_deleteIndex >= 0)
            {
                m_objects.RemoveAt(m_deleteIndex);
                m_deleteIndex = -1;
            }

            EditorGUILayout.EndScrollView();

        }
    }
}
