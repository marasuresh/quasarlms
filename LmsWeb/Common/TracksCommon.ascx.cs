namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// Список и содержание треков курсов
	/// </summary>
	public partial  class TracksCommon : DCE.BaseWebControl
	{
      private System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

      protected void Page_Load(object sender, System.EventArgs e)
		{
         DCE.Service.LoadXmlDoc(this.Page, doc, "CoursesCommon.xml");

		 System.Data.DataSet dsTracks = Track_Select();
         System.Data.DataTable tableTracks = dsTracks.Tables["Tracks"];
         doc.DocumentElement.InnerXml += dsTracks.GetXml();

         
         System.Data.DataTable tableCourses = null;

		 DataSet dsCourses = Course_Select();
         tableCourses = dsCourses.Tables["Courses"];

         System.Xml.XmlNode cNode = doc.SelectSingleNode("xml/CoursesList");
         if (cNode != null)
         {
            cNode.InnerXml += dsCourses.GetXml();
         }

         System.Xml.Xsl.XslTransform trans = new System.Xml.Xsl.XslTransform();
         trans.Load(this.Page.MapPath(@"~/xsl/TracksCommon.xslt"));
         
         this.Xml1.Document = doc;
         this.Xml1.TransformArgumentList = new System.Xml.Xsl.XsltArgumentList();
         this.Xml1.Transform = trans;
      }

		private DataSet Course_Select()
		{
			string select0 = "select c.id as cid, dbo.GetStrContentAlt(c.Name, '" +
				  LocalisationService.Language + @"', l.Abbr) as Name, 
               dbo.GetContentAlt(c.DescriptionShort,'" + LocalisationService.Language + @"', l.Abbr) as Description,
               dbo.GetStrContentAlt(ct.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Type,
               dbo.GetStrContentAlt(ctr.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Track,
               dbo.GetStrContentAlt(ctr.Description,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as TDescription,
               dbo.CourseDuration(c.id) as Duration, c.Code, c.isReady, ctr.id as TrackId,
               c.CPublic, c.Cost1, c.Cost2
            from dbo.CourseType ct right join
               dbo.Courses c inner join dbo.Languages l on l.id=c.CourseLanguage
               on c.Type=ct.id, CTracks ctr 
            where c.id in (select id from GroupMembers where mGroup=ctr.Courses) and c.IsReady=1
            order by dbo.GetStrContentAlt(ctr.Name, '" +
				  LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"')";


			DataSet dsCourses = dbData.Instance.getDataSet(select0, "DataSet", "Courses");
			return dsCourses;
		}

		private System.Data.DataSet Track_Select()
		{
			string select = @"select distinct ctr.id as TrackId,
               dbo.GetStrContentAlt(ctr.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Track,
               dbo.GetStrContentAlt(ctr.Description,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as TDescription
            from CTracks ctr, Courses c
            where c.id in (select id from GroupMembers where mGroup=ctr.Courses) and c.IsReady=1";

			DataSet dsTracks = dbData.Instance.getDataSet(select, "DataSet", "Tracks");
			return dsTracks;
		}
	}
}
