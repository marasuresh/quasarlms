namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for HelpInfo.
	/// </summary>
	public partial class HelpInfo : DCE.BaseWebControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			DCE.Service.LoadXmlDoc(this.Page, doc, "HelpInfo.xml");
			System.Xml.Xsl.XslTransform trans = new System.Xml.Xsl.XslTransform();
			trans.Load(this.Page.MapPath(@"~/xsl/HelpInfo.xslt"));
         
			this.Xml1.Document = doc;
			this.Xml1.TransformArgumentList = new System.Xml.Xsl.XsltArgumentList();
			this.Xml1.Transform = trans;

		}
	}
}
