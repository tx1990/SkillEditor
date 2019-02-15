using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace SkillEditor
{
    public class SkillEntity
    {
        [XmlArrayItem(typeof(SkillTriggerAnimation)),
         XmlArrayItem(typeof(SkillTriggerFx))]
        public List<SkillTriggerBase> SkillTriggers { get; private set; }

        [XmlAttribute] public float LifeTime { get; set; }

        private float m_curTime;

        public SkillEntity()
        {
            SkillTriggers = new List<SkillTriggerBase>();
        }

        public void Play()
        {
            m_curTime = 0;
            for (int i = 0, count = SkillTriggers.Count; i < count; i++)
            {
                SkillTriggers[i].Reset();
            }
        }

        public void Stop()
        {
            for (int i = 0, count = SkillTriggers.Count; i < count; i++)
            {
                SkillTriggers[i].Stop();
            }
        }

        public void Update()
        {
            m_curTime += Time.deltaTime;
            for (int i = 0, count = SkillTriggers.Count; i < count; i++)
            {
                SkillTriggers[i].Update(m_curTime);
            }
        }
    }
}
