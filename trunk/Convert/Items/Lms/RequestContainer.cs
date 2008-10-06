namespace N2.Lms.Items
{
	using System.Collections.Generic;
	using N2.Definitions;
	using N2.Details;
	using N2.Installation;
	using N2.Integrity;
	using N2.Templates.Items;
	using N2.Workflow.Items;

	[Definition("Request Container", "RequestContainer", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[WithEditableTitle("Title", 10)]
	[ItemAuthorizedRoles(Roles = new string[0])]
	[RestrictParents(typeof(IStructuralPage))]
	[AllowedChildren(typeof(Request))]
	public partial class RequestContainer: ContentItem, IWorkflowItemContainer
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/04/50.png"; } }

		//public override bool IsPage { get { return false; } }

		public override bool Visible { get { return false; } set { base.Visible = value; } }

		[EditableChildren("Pending requests", "", "Requests", 110)]
		public virtual IList<ContentItem> Requests
		{
			get { return GetChildren(); }
		}

		[EditableLink("Workflow", 107)]
		public Workflow Workflow {
			get { return this.GetDetail("Workflow") as Workflow; }
			set { this.SetDetail<Workflow>("Workflow", value); }
		}
	}
}
