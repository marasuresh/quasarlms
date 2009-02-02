namespace N2.Lms.Items
{
	using Details;
	using Installation;
	using Integrity;
	using Templates.Items;

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
	public partial class RequestContainer: ItemContainer<Request>
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
	}
}
