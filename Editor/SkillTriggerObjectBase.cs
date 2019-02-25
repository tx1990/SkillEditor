using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillEditor
{
    public abstract class SkillTriggerObjectBase : ScriptableObject
    {
        public abstract SkillTriggerBase GetTriggerBase();
        public abstract void SetTriggerBase(SkillTriggerBase t);
    }

}
