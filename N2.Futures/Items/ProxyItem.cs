using N2;
using N2.Details;
using N2.Templates.Items;

namespace N2.Templates.Items
{
	/// <summary>
	/// Display an arbitrary aspx page through a dynamic template
	/// </summary>
	[Definition]
	public class ProxyItem : AbstractContentPage, IStructuralPage
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/03/15.png"; } }

		[EditableTextBox("Url", 100)]
		public new string Url
		{
			get { return (string)this.GetDetail("TemplateUrl"); }
			set { this.SetDetail<string>("TemplateUrl", value); }
		}

		public override string TemplateUrl { get { return this.Url; } }
	}
}