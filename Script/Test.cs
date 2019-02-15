using System;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace SkillEditor
{
    public class Test : MonoBehaviour
    {
        public SkillTriggerAnimation[] Trigger;
        [Header("aaa")]
        public int Value;

        [ContextMenu("CreateXml")]
        private void CreateXml()
        {
            var ani1 = new SkillTriggerAnimation
            {
                AnimationName = "aaa",
                Time = Vector2.one,
            };

            var ani2 = new SkillTriggerAnimation
            {
                AnimationName = "bbb",
                Time = Vector2.left,
            };

            var fx = new SkillTriggerFx()
            {
                FxName = "ccc",
                BindPoint = "Head",
                Position = Vector3.zero,
                Time = Vector2.down,
            };

            var entity = new SkillEntity
            {
                LifeTime = 2.5f,
                SkillTriggers = {ani1, ani2, fx},
            };

            SkillHelper.XmlSerializeToFile(entity, Application.dataPath + "/test.xml", Encoding.UTF8);
        }

        [ContextMenu("LoadXml")]
        private void LoadXml()
        {
            var entity =
                SkillHelper.XmlDeserializeFromFile<SkillEntity>(Application.dataPath + "/test.xml", Encoding.UTF8);
            for (int i = 0; i < entity.SkillTriggers.Count; i++)
            {

            }
        }

        [ContextMenu("GetTypeTest")]
        private void GetTypeTest()
        {
            foreach (var value in Enum.GetValues(typeof(SkillTriggerType)))
            {
                var s = $"SkillEditor.SkillTrigger{value.ToString()}";
                var type = Type.GetType(s, true, true);
                Debug.LogWarning(type+ ":" + s);
            }
        }
    }
}
