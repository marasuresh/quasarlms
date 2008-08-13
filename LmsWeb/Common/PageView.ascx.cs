namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// Отображает html страницу
	/// </summary>
	public partial  class PageView : DCE.BaseWebControl
	{
      //protected System.Web.UI.WebControls.Xml Xml1;
      public string url = DCE.Service.DefaultLangPath + "About.htm";

		protected void Page_Load(object sender, System.EventArgs e)
		{
         url = DCE.Service.GetLanguagePath(this.Page) + "About.htm";
         object newsUrl = this.Request.Form["newsUrl"];
         if (newsUrl != null)
            this.url = (string) newsUrl;
         else
            Response.Redirect(Resources.PageUrl.PAGE_MAIN + "?index=5&cset=News");
      }
	}
}
