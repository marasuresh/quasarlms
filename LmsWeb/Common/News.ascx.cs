namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using System.Xml;
	/// <summary>
	/// Новости
	/// </summary>
	public partial  class News : DCE.BaseWebControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
         DCE.Service.LoadXmlDoc(this.Page, doc, "News.xml");

		 System.Data.DataTable tableNews = DceAccessLib.DAL.NewsController.Select();
		tableNews.DataSet.DataSetName = "Items";
		 string dsStr = tableNews.DataSet.GetXml();
         dsStr = dsStr.Replace("&lt;", "<");
         dsStr = dsStr.Replace("&gt;", ">");
         doc.DocumentElement.InnerXml += dsStr;

			this.DataBind();
      }
	}
}
