using UnityEngine;

namespace SkillEditor
{
	public class SkillTriggerAnimationObject : SkillTriggerObjectBase
	{
		[Header("Animation")]public SkillTriggerAnimation Trigger;

		public override void SetTriggerBase(SkillTriggerBase t)
		{
			if(!(t is SkillTriggerAnimation trigger)) return;
			Trigger = trigger;
		}

		public override SkillTriggerBase GetTriggerBase()
		{
			return Trigger;
		}
	}
}