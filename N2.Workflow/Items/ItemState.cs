#define CheckWorkflow
namespace N2.Workflow.Items
{
	using System.Web.UI.WebControls;
	using N2.Details;
	using N2.Web.UI;
	using N2.Definitions;
	using N2.Persistence;
	
	[Definition]
	[Disable]
	[NotVersionable]
#if CheckWorkflow
	[TabPanel("workflow", "Workflow", 200/*, AuthorizedRoles = new[] { "Administrator"}*/)]
#endif
	public class ItemState: ContentItem
	{
		[EditableTextBox("Comment", 13, Rows=3, TextMode = TextBoxMode.MultiLine)]
		public string Comment {
			get { return this.GetDetail("Comment") as string; }
			set { this.SetDetail<string>("Comment", value); }
		}

#if CheckWorkflow
		[EditableLink(
			"Previous State",
			210,
			ContainerName = "workflow")]
#else
		[Editable("Previous State", typeof(HiddenField), "Value", 210)]
#endif
		public StateDefinition FromState {
			get { return this.GetDetail("FromState") as StateDefinition; }
			set { this.SetDetail<StateDefinition>("FromState", value); }
		}

#if CheckWorkflow
		[EditableLink(
			"Current State",
			220,
			ContainerName = "workflow")]
#else
		[Editable("Current State", typeof(HiddenField), "Value", 220)]
#endif
		public StateDefinition ToState {
			get { return this.GetDetail("ToState") as StateDefinition; }
			set { this.SetDetail<StateDefinition>("ToState", value); }
		}

#if CheckWorkflow
		[EditableLink(
			"Action",
			230,
			ContainerName = "workflow")]
#else
		[Editable("Action", typeof(HiddenField), "Value", 230)]
#endif
		public ActionDefinition Action {
			get { return this.GetDetail("Action") as ActionDefinition; }
			set { this.SetDetail<ActionDefinition>("Action", value); }
		}

		[EditableTextBox(
			"Assigned To",
			240
#if CheckWorkflow
			, ContainerName = "workflow"
#endif
			)]
		public string AssignedTo {
			get { return this.GetDetail<string>("AssignedTo", string.Empty); }
			set { this.SetDetail<string>("AssignedTo", value); }
		}

		public override string Title {
			get { return base.Title ?? string.Format(
				"{2} on {0} by {1}",
				this.Updated,
				this.SavedBy,
				this.Action != null ? this.Action.Name : "?"); }
			set { base.Title = value; }
		}

		public override bool IsPage { get { return false; } }

		public override string IconUrl {
			get { return null != this.ToState ? this.ToState.Icon : "~/Workflow/UI/img/46.png"; }
		}

		public override string TemplateUrl { get { return "~/Templates/Secured/Go.aspx"; } }
	}
}
