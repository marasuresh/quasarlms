namespace N2.Workflow.Items
{
	using N2;
	using N2.Details;
	using N2.Definitions;
	using N2.Integrity;
	using N2.Templates.UI.Items;

	[Definition("Workflow", "Workflow")]
	[WithEditableName("Name", 01)]
	public class Workflow : ContentItem
	{
		[EditableLink("Initial State", 05)]
		public StateDefinition InitialState {
			get { return this.GetDetail("InitialState") as StateDefinition; }
			set { this.SetDetail<StateDefinition>("InitialState", value); }
		}

		public override bool IsPage { get { return false; } }

		public override string Title {
			get { return base.Title ?? this.Name; }
			set { base.Title = value; }
		}

		public override string IconUrl { get { return
			null == this.InitialState
				? string.Format("~/Workflow/UI/Img/03/{0}.png", this.InitialState == null ? 41 : 35)
				: this.InitialState.Icon; } }
	}
}