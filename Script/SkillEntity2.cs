using System.Collections.Generic;
using System.Xml.Serialization;

namespace SkillEditor
{
	public partial class SkillEntity
	{
		[XmlArrayItem(typeof(SkillTriggerAnimation)),XmlArrayItem(typeof(SkillTriggerFx)),]
		public List<SkillTriggerBase> SkillTriggers { get; private set; }
	}
}