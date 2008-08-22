using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace N2.Lms.Items
{
	using N2.Security.Items;
	using N2.Details;
	using N2.Integrity;

	[RestrictParents(typeof(Training))]
	[Definition]
	public class TrainingSchedule: ContentItem
	{
		public Training Training
		{
			get { return this.Parent as Training; }
			set { this.Parent = value; }
		}

		[EditableItem("Theme", 10)]
		public Topic Theme {
			get { return this.GetDetail("Theme") as Topic; }
			set { this.SetDetail<Topic>("Theme", value); }
		}

		[EditableCheckBox("Is Open", 30)]
		public bool IsOpen
		{
			get { return (bool?)this.GetDetail("IsOpen") ?? true; }
			set { this.SetDetail<bool>("IsOpen", value); }
		}

		[EditableCheckBox("Is Mandatory", 30)]
		public bool IsMandatory
		{
			get { return (bool?)this.GetDetail("IsMandatory") ?? true; }
			set { this.SetDetail<bool>("IsMandatory", value); }
		}
	}
}
