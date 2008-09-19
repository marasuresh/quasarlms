using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Lms.Items.Lms.RequestStates
{
	using N2.Details;
	using N2.Workflow.Items;
	using N2.Integrity;

	[RestrictParents(typeof(Request))]
	public class ApprovedState: ItemState
	{
		[EditableLink("Assign Training", 1, Required = true)]
		public Training Training {
			get { return this.GetDetail<Training>("Training", null); }
			set { this.SetDetail<Training>("Training", value); }
		}
	}
}
