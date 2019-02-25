using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace SkillEditor
{
    public partial class SkillEntity
    {
        [XmlAttribute] public float LifeTime { get; set; }

        private float m_curTime;

        public SkillEntity()
        {
            SkillTriggers = new List<SkillTriggerBase>();
        }

        public void Play()
        {
            m_curTime = 0;
            foreach (var trigger in SkillTriggers)
            {
                trigger.Reset();
            }
        }

        public void Stop()
        {
            foreach (var trigger in SkillTriggers)
            {
                trigger.Stop();
            }
        }

        public void Update()
        {
            m_curTime += Time.deltaTime;
            foreach (var trigger in SkillTriggers)
            {
                trigger.Update(m_curTime);
            }
        }
    }
}
