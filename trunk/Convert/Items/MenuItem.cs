namespace N2.Lms.Items
{
	public class MenuItem: ContentItem
	{
		public override string Url {
			get { return this.NavigateUrl ?? base.Url; }
		}

		public string NavigateUrl {
			get { return (string)this.GetDetail("NavigateUrl"); }
			set { this.SetDetail<string>("NavigateUrl", value); }
		}

		public override string TemplateUrl
		{
			get
			{
				return this.NavigateUrl ?? base.TemplateUrl;
			}
		}
	}
}
