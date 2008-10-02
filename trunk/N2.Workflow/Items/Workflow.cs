namespace N2.Workflow.Items
{
	using N2;
	using N2.Details;
	using N2.Definitions;
	using N2.Integrity;

	[Definition("Workflow", "Workflow")]
	[WithEditableName("Name", 01)]
	[ItemAuthorizedRoles("Administrators")]
	public class Workflow : ContentItem
	{
		[EditableLink("Initial State", 05)]
		public StateDefinition InitialState {
			get {
				return this.GetDetail<StateDefinition>(
					"InitialState",
					item => this.GetChild("new") as StateDefinition);
			}
			set { this.SetDetail<StateDefinition>("InitialState", value); }
		}

		public override bool IsPage { get { return false; } }
		public override string TemplateUrl { get { return "~/Workflow/UI/Workflow.ascx"; } }

		public override string Title {
			get { return base.Title ?? this.Name; }
			set { base.Title = value; }
		}

		public override string IconUrl { get { return
			null == this.InitialState
				? string.Format("~/Workflow/UI/Img/03/{0}.png", 41)
				: this.InitialState.IconUrl; } }
	}
}