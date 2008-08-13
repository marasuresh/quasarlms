namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Xml;
	using System.Xml.Xsl;
	/// <summary>
	/// Просмотр учебных материалов по теме
	/// </summary>
	public partial class ContentView : DCE.BaseTrainingControl
	{
		public string AbsoluteViewPath;
		public string FileName;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Guid? trId = DCE.Service.TrainingID;
			string courseId = DCE.Service.courseId.ToString();
			XmlDocument doc = new XmlDocument();
			string CoursesRoot = this.CoursesRootUrl;
			Guid? themeId = PageParameters.ID;

			if (themeId.HasValue && trId.HasValue) {
				doc.LoadXml("<xml/>");
				DataSet dsContents = Courses_GetContentDS(CoursesRoot, themeId, this.ResolveUrl(@"~\" + Settings.getValue("Courses/root")));
				DataTable tableContent = dsContents.Tables["contentItem"];

				if (tableContent != null && tableContent.Rows.Count > 0) {
					bool hasLang = false; // Есть ли content для выбранного языка

					foreach (DataRow row in tableContent.Rows) {
						if (row["lang"].ToString() != row["DefLang"].ToString()) {
							hasLang = true;
							break;
						}
					}

					if (hasLang) {

						for (int i = tableContent.Rows.Count - 1; i >= 0; i--) {
							DataRow row = tableContent.Rows[i];

							if (row["lang"].ToString() == row["DefLang"].ToString()) {
								tableContent.Rows.RemoveAt(i);
							}
						}
					}

					this.Session["themeName"] = tableContent.Rows[0]["Name"].ToString();
					string cont = dsContents.GetXml();
					doc.DocumentElement.InnerXml += cont;
				}

				object cn = this.Session["courseName"];

				if (cn != null) {
					string courseName = (string)cn;
					doc.DocumentElement.InnerXml += "<Course>" + courseName + "</Course>";
				}

				DataSet dsTr = Training_Select(trId);
				doc.DocumentElement.InnerXml += dsTr.GetXml();
			}

			XslTransform trans = new XslTransform();
			trans.Load(this.Page.MapPath(@"~\xsl\ContentView.xslt"));

			this.Xml1.Document = doc;
			this.Xml1.TransformArgumentList = new XsltArgumentList();
			this.Xml1.TransformArgumentList.AddParam("LangPath", "",
				this.ResolveUrl(@"~\" + DCE.Service.GetLanguagePath(this.Page)));
			this.Xml1.Transform = trans;
		}

		static DataSet Training_Select(Guid? trId)
		{
			string select = @"select 
                  dbo.GetStrContentAlt(c.Name, '" + LocalisationService.Language + @"', l.Abbr) as tName
               FROM  Trainings t 
                  INNER JOIN
                  Courses c inner join dbo.Languages l 
                     ON l.id=c.CourseLanguage
                     ON t.Course = c.id 
               WHERE (t.id = '" + trId + "')";

			return dbData.Instance.getDataSet(select, "trainings", "training");
		}

		static DataSet Courses_GetContentDS(string CoursesRoot, Guid? themeId, string croot)
		{
			string select = string.Format(@"
SELECT	'{0}' as cRoot,
		'{1}' as PublicRoot, 
		ct.DataStr AS url,
		c.DiskFolder,
		l.Abbr AS lang,
		cl.Abbr as DefLang,
		dbo.GetStrContentAlt(t.Name,'{2}',cl.Abbr) as Name,
		c.CPublic
FROM	Content ct
	INNER JOIN Themes t
		ON ct.eid = t.Content 
	INNER JOIN Languages l
		ON ct.Lang = l.id 
	inner join Courses c
		on c.id=dbo.CourseofTheme(t.id)
	INNER JOIN Languages cl
		ON c.CourseLanguage = cl.id 
WHERE	(t.id = '{3}') 
		AND (l.Abbr = '{2}' or l.Abbr = cl.Abbr)
ORDER BY ct.COrder, ct.Lang",
						CoursesRoot,
						croot,
						LocalisationService.Language,
						themeId);

			return dbData.Instance.getDataSet(select, "dsContent", "contentItem");
		}
	}
}
