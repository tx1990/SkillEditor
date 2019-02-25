using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SkillEditor
{
    [CustomPropertyDrawer(typeof(ResourcesNameAttribute))]
    public class ResourcesNameDrawer : PropertyDrawer
    {
        private Object m_res;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (ResourcesNameAttribute) attribute;
            if (m_res == null && !string.IsNullOrEmpty(property.stringValue))
            {
                m_res = SkillHelper.LoadFromResource(property.stringValue, attr.ResourceType);
            }

            var newRes = EditorGUILayout.ObjectField(new GUIContent(property.name), m_res, attr.ResourceType, false, GUILayout.ExpandWidth(false), GUILayout.Width(400));

            if (m_res != newRes)
            {
                property.stringValue = newRes == null ? string.Empty : newRes.name;
                m_res = newRes;
            }
        }
    }

}