using System;
using System.Xml.Serialization;
using UnityEngine;

namespace SkillEditor
{
    public enum SkillTriggerType
    {
        Animation,
        Fx,
    }

    public abstract class SkillTriggerBase
    {
        private enum TriggerState
        {
            NotTrigger = 0,
            Trigger = 1,
            Over = 2,
        }

        [XmlIgnore] public readonly SkillTriggerType TriggerType;
        public float StartTime;
        public float EndTime;

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
                    if (curTime >= StartTime)
                    {
                        Trigger();
                        m_curState = TriggerState.Trigger;
                    }

                    break;
                case TriggerState.Trigger:
                    if (curTime >= EndTime)
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
