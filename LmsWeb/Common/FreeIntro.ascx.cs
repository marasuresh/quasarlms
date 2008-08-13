namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// Welcome страница свободного курса
	/// </summary>
	public partial  class FreeIntro : DCE.BaseTrainingControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			DCE.Service.LoadXmlDoc(this.Page, doc, "Welcome.xml");
			
			DataTable tableCourses = null;
			
			Guid? courseId = DCE.Service.courseId;
			DataSet dsCourses = Course_Select(courseId);
			if (null != dsCourses) {
				tableCourses = dsCourses.Tables["Courses"];
			}
			
			if (tableCourses != null && tableCourses.Rows.Count == 1) {
				Session["CourseLanguage"] = tableCourses.Rows[0]["CourseLanguage"].ToString();
				this.Session["courseName"] = tableCourses.Rows[0]["Name"].ToString();
				System.Xml.XmlDocument tdoc = new System.Xml.XmlDocument();
				tdoc.LoadXml(dsCourses.GetXml());
				
				doc.DocumentElement.InnerXml += tdoc.SelectSingleNode("dataSet/Courses").InnerXml;

				System.Xml.XmlNode startNode = doc.SelectSingleNode("xml/Start");
				startNode.InnerXml += "<href>" + this.ResolveUrl(Resources.PageUrl.PAGE_TRAINING + "?cId="+courseId) +"</href>";
			} else if (this.Page.GetType().BaseType == typeof(DCE.FreeCourses)) {
				this.Response.Redirect(Resources.PageUrl.PAGE_MAIN + "?index="
					+this.Request["index"]+"&cset=FreeIntro");
			}

			System.Xml.Xsl.XslTransform trans = new System.Xml.Xsl.XslTransform();
			trans.Load(this.Page.MapPath(@"~\xsl\FreeIntro.xslt"));
			
			this.Xml1.Document = doc;
			this.Xml1.TransformArgumentList = new System.Xml.Xsl.XsltArgumentList();
			this.Xml1.TransformArgumentList.AddParam("LangPath", "", 
			DCE.Service.GetLanguagePath(this.Page));
			this.Xml1.Transform = trans;
		}

		private System.Data.DataSet Course_Select(Guid? courseId)
		{
			string CoursesRoot = DCE.Settings.getCoursesRoot();

			System.Data.DataSet dsCourses = null;
			if (courseId.HasValue) {
				string select = @"select c.id, 
                  dbo.GetStrContentAlt(c.Name, '" + LocalisationService.Language + @"', l.Abbr) as Name, 

                  '" + CoursesRoot + @"' as cRoot, c.DiskFolder, l.Abbr as CourseLanguage,
                  dbo.GetStrContentAlt(c.DescriptionLong,'" + LocalisationService.Language + @"', l.Abbr) as FullDescription, 
                  
                  dbo.GetContentAlt(c.DescriptionShort,'" + LocalisationService.Language + @"', l.Abbr) as Description
               from dbo.Courses c, dbo.Languages l
               where l.id=c.CourseLanguage and c.id='" + courseId + "' and isReady=1 and CPublic=1";

				dsCourses = dbData.Instance.getDataSet(select, "dataSet", "Courses");


			}
			return dsCourses;
		}
	}
}
