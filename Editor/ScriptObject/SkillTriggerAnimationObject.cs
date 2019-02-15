using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillEditor
{
	public class SkillTriggerAnimationObject : ScriptableObject, IToSkillTriggerBase
	{
		[Header("Animation")]public SkillTriggerAnimation Trigger;

		public static explicit operator SkillTriggerAnimationObject(SkillTriggerBase t)
		{
			if(!(t is SkillTriggerAnimation trigger)) return null;
			var o = CreateInstance<SkillTriggerAnimationObject>();
			o.Trigger = trigger;
			return o;
		}

		public SkillTriggerBase ToSkillTriggerBase()
		{
			return Trigger;
		}
	}
}