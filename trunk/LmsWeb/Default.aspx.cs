namespace DCE
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : N2.Web.UI.ContentPage
	{
      protected void Page_Load(object sender, System.EventArgs e)
      {
         this.Response.Redirect(Resources.PageUrl.PAGE_MAIN + "?index=1");
      }
	}
}
