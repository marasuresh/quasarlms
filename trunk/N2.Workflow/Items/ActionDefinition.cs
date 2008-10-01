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
		#region Business properties

		[EditableLink("Destination State", 13, Required = true)]
		public StateDefinition LeadsTo {
			get { return this.GetDetail("LeadsTo") as StateDefinition; }
			set { this.SetDetail<StateDefinition>("LeadsTo", value); }
		}

		[EditableStateTypeDropDown(
			Name = "Item State Type",
			Title = "State Type", SortOrder = 23,
			LocalizationClassKey = "Workflow.ActionDefinition")]
		public Type StateType {
			get { return this.GetDetail("StateType") as Type; }
			set { this.SetDetail<Type>("StateType", value); }
		}

		#endregion Business properties

		#region System properties

		public override bool IsPage { get { return false; } }

		public override string Title {
			get { return (base.Title ?? this.Name) + (this.LeadsTo != null ? "&mdash;" + this.LeadsTo.Title : string.Empty); }
			set { base.Title = value; }
		}

		public override string IconUrl { get { return this.LeadsTo.IconUrl; } }

		//public override string TemplateUrl { get { return "~/Templates/Secured/Go.aspx"; } }

		public override string ZoneName {
			get { return base.ZoneName ?? "SiteRight"; }
			set { base.ZoneName = value; }
		}

		#endregion System properties
	}
}
