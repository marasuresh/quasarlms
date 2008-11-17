using System.Web.UI.WebControls;

namespace N2.Web.UI.WebControls
{
	using System;
	using N2.Resources;

	/// <summary>
	/// Display a frame with rounded corners via CSS
	/// </summary>
	public class ChromeBox: Panel
	{
		public ChromeBox()
		{
			this.CssClass = "grid";
		}

		protected override void OnInit(System.EventArgs e)
		{
			Array.ForEach(
				new[] {"N2.Futures.Css.grid.css", "N2.Futures.Css.round.css", "N2.Futures.Css.grid-fix-n2.css"},
				url => Register.StyleSheet(
					this.Page,
					this.Page.ClientScript.GetWebResourceUrl(
						typeof(ChromeBox), url)));
			
			base.OnInit(e);
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
