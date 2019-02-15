using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SkillEditor
{
    public class SkillTriggerFxTest : ScriptableObject
    {
        [SerializeField]
        public List<SkillTriggerBase> Triggers = new List<SkillTriggerBase>();

        public int Value;

        public SkillTriggerBase t = new SkillTriggerAnimation();
    }
}
