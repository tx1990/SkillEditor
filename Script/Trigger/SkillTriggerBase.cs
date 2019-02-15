using System;
using System.Xml.Serialization;
using UnityEngine;

namespace SkillEditor
{
    public abstract class SkillTriggerBase
    {
        private enum TriggerState
        {
            NotTrigger = 0,
            Trigger = 1,
            Over = 2,
        }

        [XmlIgnore] public readonly SkillTriggerType TriggerType;
        public Vector2 Time;

        private TriggerState m_curState;

        protected SkillTriggerBase(SkillTriggerType triggerType)
        {
            TriggerType = triggerType;
        }

        public virtual void Reset()
        {
            m_curState = TriggerState.NotTrigger;
        }

        protected abstract void Trigger();

        public abstract void Stop();

        public void Update(float curTime)
        {
            switch (m_curState)
            {
                case TriggerState.NotTrigger:
                    if (curTime >= Time.x)
                    {
                        Trigger();
                        m_curState = TriggerState.Trigger;
                    }

                    break;
                case TriggerState.Trigger:
                    if (curTime >= Time.y)
                    {
                        Stop();
                        m_curState = TriggerState.Over;
                    }

                    break;
                case TriggerState.Over:
                    break;
                default:
                    return;
            }
        }
    }
}
