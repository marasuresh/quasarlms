using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace N2.Lms.Items
{
	using N2.Security.Items;
	using N2.Details;
	using N2.Integrity;
	using N2.Workflow.Items;

	[RestrictParents(typeof(Workflow))]
	[Definition(Description = @"Top-level topic instance within a Training, represented as a continuous workflow state.")]
	[WithEditablePublishedRange("Published between", 01)]
	public partial class ScheduledTopic: StateDefinition
	{
		#region System properties

		public override string IconUrl {
			get {
				DateTimeOffset _now = DateTimeOffset.Now;
				
				bool _published = 
					!((this.Published.HasValue && _now < this.Published) ||
					(this.Expires.HasValue && _now > this.Expires));

				return string.Format("~/Lms/UI/Img/02/0{0}.png", _published ? 3 : 2);
			}
		}

		public override string ZoneName {
			get { return base.ZoneName ?? string.Empty; }
			set { base.ZoneName = value; }
		}

		public override string TemplateUrl { get { return "~/Lms/UI/Module.ascx"; } }

		#endregion System properties

		#region Lms properties

		[EditableLink("Topic", 03, Required = true)]
		public Topic Topic {
			get { return this.GetDetail<Topic>(
				"Topic",
				item => ((Training)item.Parent.Parent).Course.FindTopic(item.Name)); }
			set { this.SetDetail<Topic>("Topic", value); }
		}

		[EditableTextBox("Title", 05)]
		public override string Title {
			get {
				return base.Title ?? (this.Topic != null ? this.Topic.Title : string.Empty);
			}
			set { base.Title = value; }
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

		#endregion Lms properties
	}
}
