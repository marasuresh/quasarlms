namespace N2.Workflow.Items
{
	using System.Web.UI.WebControls;
	using N2.Details;
	using N2.Web.UI;

	[Definition]
	[N2.Persistence.NotVersionable]
	[TabPanel("workflow", "Workflow", 200, AuthorizedRoles = new[] { "Administrators"})]
	public class ItemState: ContentItem
	{
		[EditableTextBox("Comment", 13, Rows=3, TextMode = TextBoxMode.MultiLine)]
		public string Comment {
			get { return this.GetDetail("Comment") as string; }
			set { this.SetDetail<string>("Comment", value); }
		}

		[EditableLink(
			"Previous State",
			210,
			ContainerName = "workflow")]
		public StateDefinition FromState {
			get { return this.GetDetail("FromState") as StateDefinition; }
			set { this.SetDetail<StateDefinition>("FromState", value); }
		}

		[EditableLink(
			"Current State",
			220,
			ContainerName = "workflow")]
		public StateDefinition ToState {
			get { return this.GetDetail("ToState") as StateDefinition; }
			set { this.SetDetail<StateDefinition>("ToState", value); }
		}

		[EditableLink(
			"Action",
			230,
			ContainerName = "workflow")]
		public ActionDefinition Action {
			get { return this.GetDetail("Action") as ActionDefinition; }
			set { this.SetDetail<ActionDefinition>("Action", value); }
		}

		[EditableTextBox(
			"Assigned To",
			240,
			ContainerName = "workflow")]
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
	}
}
