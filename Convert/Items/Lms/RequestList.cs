﻿namespace N2.Lms.Items
{
	using N2.Definitions;
	using N2.Details;
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Integrity;
	using N2.Persistence;
	using N2.Templates.Items;
	
	[Definition("Request List", "Requests", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[WithEditableTitle("Title", 10)]
	[RestrictParents(typeof(IStructuralPage))]
	[NotThrowable, NotVersionable]
	public class RequestList: AbstractContentPage
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/04/50.png"; } }
	}
}