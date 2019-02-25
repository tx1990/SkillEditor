using UnityEngine;

namespace SkillEditor
{
	public class SkillTriggerFxObject : SkillTriggerObjectBase
	{
		[Header("Fx")]public SkillTriggerFx Trigger;

		public override void SetTriggerBase(SkillTriggerBase t)
		{
			if(!(t is SkillTriggerFx trigger)) return;
			Trigger = trigger;
		}

		public override SkillTriggerBase GetTriggerBase()
		{
			return Trigger;
		}
	}
}