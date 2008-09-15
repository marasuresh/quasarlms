namespace N2.Workflow.Items
{
	using System.Web.UI.WebControls;
	using N2.Details;

	[Definition]
	[N2.Persistence.NotVersionable]
	public class ItemState: ContentItem
	{
		[EditableTextBox("Comment", 13, Rows=3, TextMode = TextBoxMode.MultiLine)]
		public string Comment {
			get { return this.GetDetail("Comment") as string; }
			set { this.SetDetail<string>("Comment", value); }
		}

		public StateDefinition FromState {
			get { return this.GetDetail("FromState") as StateDefinition; }
			set { this.SetDetail<StateDefinition>("FromState", value); }
		}

		public StateDefinition ToState {
			get { return this.GetDetail("ToState") as StateDefinition; }
			set { this.SetDetail<StateDefinition>("ToState", value); }
		}

		public ActionDefinition Action {
			get { return this.GetDetail("Action") as ActionDefinition; }
			set { this.SetDetail<ActionDefinition>("Action", value); }
		}

		public override string Title {
			get { return base.Title ?? string.Format(
				"{0} by {1}",
				this.Updated,
				this.SavedBy); }
			set { base.Title = value; }
		}

		public override bool IsPage { get { return false; } }

		public override string IconUrl {
			get { return null != this.ToState ? this.ToState.Icon : string.Empty; }
		}
	}
}
