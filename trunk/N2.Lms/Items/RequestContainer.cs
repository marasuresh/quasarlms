namespace N2.Lms.Items
{
	using N2.Definitions;
	using N2.Details;
	using N2.Installation;
	using N2.Integrity;
	using N2.Templates.Items;
	using N2.Workflow.Items;

	[Definition(
		"Request Container",
		"RequestContainer",
		"",
		"",
		2000,
		Installer = InstallerHint.NeverRootOrStartPage)]
	[WithEditableTitle("Title", 10, LocalizationClassKey = "RequestContainer")]
	[RestrictParents(typeof(IStructuralPage), typeof(IStorageItem))]
	[AllowedChildren(typeof(Request))]
	public partial class RequestContainer: ContentItem, IWorkflowItemContainer
	{

		public RequestContainer()
		{
			this.Name = "requests";
			this.Title = global::N2.Lms.Strings.RequestContainer_Title;
		}

		#region System properties
		
		public override string IconUrl { get { return "~/Lms/UI/Img/04/50.png"; } }
		public override string TemplateUrl { get { return "~/Templates/UI/Parts/Empty.ascx"; } }
		public override bool IsPage { get { return false; } }
		public override bool Visible { get { return false; } set { base.Visible = value; } }

		#endregion System properties

		[EditableItem("Workflow", 107)]
		public Workflow Workflow {
			get { return this.GetDetail<Workflow>(
				"Workflow",
				this.GetOrFindOrCreateChild<Workflow>(
					"Workflow",
					wf => this.InitializeDefaultWorkflow(wf))); }
			set { this.SetDetail<Workflow>("Workflow", value); }
		}
	}
}
