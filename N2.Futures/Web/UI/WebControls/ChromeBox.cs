using System.Web.UI.WebControls;

namespace N2.Web.UI.WebControls
{
	public class ChromeBox: Panel
	{
		public ChromeBox()
		{
			this.CssClass = "grid";
		}

		public override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			writer.Write(@"<div class='rounded'><div class='top-outer'><div class='top-inner'><div class='top'></div></div></div><div class='mid-outer'><div class='mid-inner'><div class='mid'>");
		}

		public override void RenderEndTag(System.Web.UI.HtmlTextWriter writer)
		{
            writer.Write(@"</div></div></div><div class='bottom-outer'><div class='bottom-inner'><div class='bottom'></div></div></div></div>");
			base.RenderEndTag(writer);
		}
	}
}
