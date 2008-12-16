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
			get { return
				string.IsNullOrEmpty(this.StateTypeName)
					? typeof(ItemState)
					: Type.GetType(this.StateTypeName, false)
						?? typeof(ItemState);
			}
			set { this.StateTypeName = value.AssemblyQualifiedName; }
		}

		public string StateTypeName {
			get { return this.GetDetail("StateTypeName") as string; }
			set { this.SetDetail<string>("StateTypeName", value); }
		}

		#endregion Business properties

		#region System properties

		public override bool IsPage { get { return false; } }

		public override string Title {
			get { return base.Title ?? this.GetDefaultTitle(); }
		}

		string GetDefaultTitle()
		{
			string _result = this.Name;

			if (null != this.LeadsTo) {
				_result = string.Concat(_result, "&mdash;", this.LeadsTo.Title);
			}

			return _result;
		}

		public override string IconUrl { get { return this.LeadsTo.IconUrl; } }

		public override string TemplateUrl { get { return "~/Templates/UI/Parts/Empty.ascx"; } }

		public override string ZoneName {
			get { return base.ZoneName ?? "SiteRight"; }
		}

		#endregion System properties
	}
}
