using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SkillEditor
{
    [Serializable]
    [XmlType("Animation")]
    public class SkillTriggerAnimation : SkillTriggerBase
    {
        public string AnimationName;

        public List<string> Animations;

        public SkillTriggerAnimation()
            : base(SkillTriggerType.Animation)
        {
        }

        protected override void Trigger()
        {

        }

        public override void Stop()
        {

        }
    }
}
