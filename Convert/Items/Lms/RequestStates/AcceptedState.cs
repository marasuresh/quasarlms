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
	public class AcceptedState: ItemState
	{
		[EditableTextBox(
			"Grade",
			12,
			Required = true,
			ValidationExpression="\\d+")]
		public int Grade {
			get { return this.GetDetail<int>("Grade", 1); }
			set { this.SetDetail<int>("Grade", value); }
		}
	}
}
