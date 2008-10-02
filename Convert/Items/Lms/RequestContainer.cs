namespace N2.Lms.Items
{
	using System.Linq;
	using System.Collections.Generic;
	using N2.Definitions;
	using N2.Details;
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Integrity;
	using N2.Persistence;
	using N2.Templates.Items;
	using N2.Workflow;
	using N2.Workflow.Items;
	using N2.Lms.Items.Lms.RequestStates;
	
	[Definition("Request Container", "RequestContainer", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[WithEditableTitle("Title", 10)]
	[ItemAuthorizedRoles(Roles = new string[0])]
	[RestrictParents(typeof(IStructuralPage))]
	[AllowedChildren(typeof(Request))]
	public class RequestContainer: ContentItem, IWorkflowItemContainer
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/04/50.png"; } }

		public override bool IsPage { get { return false; } }

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

		public IEnumerable<ApprovedState> MyApprovedApplications {
			get {
				return
					from _req in this.GetChildren(/*filter by current user*/).OfType<Request>()
					let _currentState = _req.GetCurrentState() as ApprovedState
					where null != _currentState
					select _currentState;
			}
		}
	}
}
