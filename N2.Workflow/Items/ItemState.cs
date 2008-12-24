using System;
//#define CheckWorkflow
namespace N2.Workflow.Items
{
	using System.Web.UI.WebControls;
	using N2.Details;
	using N2.Web.UI;
	using N2.Definitions;
	using N2.Persistence;
	using N2.Edit.Trash;

	[Disable, Definition, NotThrowable, NotVersionable]
	[ItemAuthorizedRoles("Administrators")]
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
#endif
		public StateDefinition FromState {
			get {
				var _previous = this.PreviousState;

				return
					null != _previous && null != _previous.ToState
						? _previous.ToState
//TODO remove when remaining old-fashioned items will extinct
						: this.GetDetail<StateDefinition>("FromState", null)
						?? this.Action.Parent as StateDefinition;
			}
		}

#if CheckWorkflow
		[EditableLink(
			"Current State",
			220,
			ContainerName = "workflow")]
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
#endif
		public ActionDefinition Action {
			get { return this.GetDetail("Action") as ActionDefinition; }
			set { this.SetDetail<ActionDefinition>("Action", value); }
		}

		public ItemState PreviousState {
			get { return this.GetDetail<ItemState>("PreviousState", null); }
			set { this.SetDetail<ItemState>("PreviousState", value); }
		}

		[Editable(
			"Assigned To",
			typeof(N2.Web.UI.WebControls.SelectUser),
			"SelectedUser",
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

		public override bool Visible { get { return false; } }
	}
}
