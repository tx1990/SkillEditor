using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillEditor
{
	public class SkillTriggerFxObject : ScriptableObject, IToSkillTriggerBase
	{
		[Header("Fx")]public SkillTriggerFx Trigger;

		public static explicit operator SkillTriggerFxObject(SkillTriggerBase t)
		{
			if(!(t is SkillTriggerFx trigger)) return null;
			var o = CreateInstance<SkillTriggerFxObject>();
			o.Trigger = trigger;
			return o;
		}

		public SkillTriggerBase ToSkillTriggerBase()
		{
			return Trigger;
		}
	}
}