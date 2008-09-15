using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Workflow.Items
{
	using N2;
	using N2.Definitions;
	using N2.Integrity;
	using N2.Details;

	[Definition]
	[WithEditableName("Name", 01)]
	[RestrictParents(typeof(StateDefinition))]
	public class ActionDefinition: ContentItem
	{
		[EditableLink("Destination State", 13)]
		public StateDefinition LeadsTo {
			get { return this.GetDetail("LeadsTo") as StateDefinition; }
			set { this.SetDetail<StateDefinition>("LeadsTo", value); }
		}

		public override bool IsPage { get { return false; } }

		public override string Title {
			get { return (base.Title ?? this.Name) + (this.LeadsTo != null ? "&mdash;" + this.LeadsTo.Title : string.Empty); }
			set { base.Title = value; }
		}

		public override string IconUrl { get {
			return this.LeadsTo.Icon ?? string.Format("~/Workflow/UI/Img/03/0{0}.png",
				this.LeadsTo == null ? 1 : 2); } }
	}
}
