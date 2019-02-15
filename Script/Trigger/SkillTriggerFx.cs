using System;
using System.Xml.Serialization;
using UnityEngine;

namespace SkillEditor
{
    [Serializable]
    [XmlType("Fx")]
    public class SkillTriggerFx : SkillTriggerBase
    {
        [ResourcesName(typeof(GameObject))]
        public string FxName;

        public string BindPoint;

        public Vector3 Position;

        public Vector3 Rotation;

        public Vector3 Scale;

        public SkillTriggerFx()
            : base(SkillTriggerType.Fx)
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
