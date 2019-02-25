using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SkillEditor
{
    [CustomPropertyDrawer(typeof(BindPointAttribute))]
    public class BindPointDrawer : PropertyDrawer
    {
        private int m_index = -1;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (BindPointAttribute)attribute;
            if (attr == null)
            {
                return;
            }

            if (m_index < 0)
            {
                m_index = 0;
                var s = property.stringValue;
                for (int i = 0; i < attr.Points.Length; i++)
                {
                    if (attr.Points[i] == s)
                    {
                        m_index = i;
                        break;
                    }
                }

                property.stringValue = attr.Points[m_index];
            }

            var index = EditorGUILayout.Popup("BindPoint", m_index, attr.Points);
            if (index != m_index)
            {
                m_index = index;
                if (attr.Points.Length > index && index >= 0)
                {
                    property.stringValue = attr.Points[index];
                }
            }
        }
    }
}
