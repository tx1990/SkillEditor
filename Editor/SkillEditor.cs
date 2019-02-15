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
            //m_handleses.Clear();
            ShowEditGui();
            ShowSkillEntityGui();
        }

        private void ShowEditGui()
        {
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

            if (GUILayout.Button("Save"))
            {
                var entity = new SkillEntity {LifeTime = m_lifeTime};
                for (int i = 0, count = m_objects.Count; i < count; i++)
                {
                    if (m_objects[i].targetObject is IToSkillTriggerBase toTrigger)
                    {
                        entity.SkillTriggers.Add(toTrigger.ToSkillTriggerBase());
                    }
                }

                SkillHelper.XmlSerializeToFile(entity, $"{Application.dataPath}/test.xml", Encoding.UTF8);
            }
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

                var timeProp = trigger.FindPropertyRelative("Time");
                var time = timeProp.vector2Value;
                EditorGUILayout.MinMaxSlider(ref time.x, ref time.y, 0, m_lifeTime, GUILayout.ExpandWidth(true));
                time.x = Mathf.Clamp(time.x, 0, m_lifeTime);
                time.y = Mathf.Clamp(time.y, time.x, m_lifeTime);
                timeProp.vector2Value = time;

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

        void OnFocus()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
            SceneView.onSceneGUIDelegate += OnSceneGUI;
        }

        void OnDestroy()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            
            HandleUtility.Repaint();
        }
    }
}
