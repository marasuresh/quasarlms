namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// Библиотека
	/// </summary>
	public partial  class Library : DCE.BaseTrainingControl
	{
      public string AbsoluteViewPath;

		protected void Page_Load(object sender, System.EventArgs e)
		{
         Guid? trId = DCE.Service.TrainingID;
         Guid? courseId = DCE.Service.courseId;

         System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

         Guid? themeId  = PageParameters.ID;
         
			doc.LoadXml("<xml/>");
         object cn = this.Session["courseName"];
         if (cn != null)
         {
            string courseName = (string) cn;
            doc.DocumentElement.InnerXml += "<Course>"+courseName+"</Course>";
         }
         if(themeId.HasValue) {

			 System.Data.DataSet dsContents = Theme_Select(themeId);
            System.Data.DataTable tableContent = dsContents.Tables["contentItem"];
            if (tableContent != null && tableContent.Rows.Count > 0)
            {
               this.Session["themeName"] = tableContent.Rows[0]["Name"].ToString();
               string cont = dsContents.GetXml();
               doc.DocumentElement.InnerXml += cont;
            }
         }
         
         System.Xml.Xsl.XslTransform trans = new System.Xml.Xsl.XslTransform();
         trans.Load(this.Page.MapPath(@"~/xsl/Library.xslt"));
         
         this.Xml1.Document = doc;
         this.Xml1.TransformArgumentList = new System.Xml.Xsl.XsltArgumentList();
         this.Xml1.Transform = trans;
      }

		private System.Data.DataSet Theme_Select(Guid? themeId)
		{
			string select = @"SELECT dbo.IsEmptyString(c.DataStr, '') AS url, 
               dbo.GetStrContentAlt(t.Name, '" +
			   LocalisationService.Language + "','" + defLang + @"') as Name, l.Abbr AS lang 
               FROM  Content c INNER JOIN Themes t ON c.eid = t.Content INNER JOIN
               Languages l ON c.Lang = l.id WHERE (t.id = '"
			   + themeId + "') AND (l.Abbr = '" + LocalisationService.Language + "') ORDER BY c.COrder, c.Lang";
			DataSet dsContents = dbData.Instance.getDataSet(select, "ds", "contentItem");
			return dsContents;
		}

//      protected override void Render(System.Web.UI.HtmlTextWriter output)
//      {
//         base.Render(output);
//         this.Server.Execute("http://www.google.com");
//      }
	}
}
