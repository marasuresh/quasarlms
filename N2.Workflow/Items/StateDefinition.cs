using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Workflow.Items
{
	using N2;
	using N2.Details;
	using N2.Definitions;
	using N2.Integrity;
	using N2.Collections;

	[Definition]
	[RestrictParents(typeof(Workflow))]
	[WithEditableName("Name", 01)]
	public class StateDefinition: ContentItem
	{
		[EditableImage("Icon", 07)]
		public string Icon {
			get { return this.GetDetail("Icon") as string; }
			set { this.SetDetail<string>("Icon", value); }
		}

		public override bool IsPage { get { return false; } }

		public override string Title
		{
			get { return base.Title ?? this.Name; }
			set { base.Title = value; }
		}

		public override string IconUrl { get { return
			string.IsNullOrEmpty(this.Icon)
				? this.GetIconUrl() ?? base.IconUrl
				: base.IconUrl; } }
		
		public override string TemplateUrl { get { return "~/Workflow/UI/StateDefinition.ascx"; } }

		public IEnumerable<ActionDefinition> Actions {
			get {
				return this.GetChildren(
					new N2.Collections.TypeFilter(typeof(ActionDefinition))).Cast<ActionDefinition>();
			}
		}
	}
}
