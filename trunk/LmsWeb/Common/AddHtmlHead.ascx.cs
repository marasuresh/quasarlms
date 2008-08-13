namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// Довавление значения в заголовой страницы 
	/// (используется для добавления meta keywords)
	/// </summary>
	public partial  class AddHttpHead : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}
      protected override void Render(System.Web.UI.HtmlTextWriter output)
      {
         object item = this.Session["HttpHeadItem"];
         if (item != null)
         {
            output.Write((string)item);
         }
         this.Session.Remove("HttpHeadItem");
      }
	}
}
