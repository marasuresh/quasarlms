namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Xml;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// Отображение информации о курсе
	/// </summary>
	public partial  class CourseIntro : DCE.BaseWebControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			DCE.Service.LoadXmlDoc(this.Page, doc, "CourseIntro.xml");
			DataTable tableCourses = null;
			string reqCourseCode = this.Request["code"];
			
			if (reqCourseCode == null) {
				reqCourseCode = "";
			}
			
			Guid? reqCourseId = GuidService.Parse(this.Request["cId"]);
			DataSet dsCourses = DceAccessLib.DAL.CourseController.SelectByCodeOrId(reqCourseCode, reqCourseId, this.ResolveUrl(DCE.Settings.getCoursesRoot()));
			
			if (null != dsCourses) {
				tableCourses = dsCourses.Tables["Courses"];
			}
			
			if (tableCourses != null && tableCourses.Rows.Count > 0) {
				this.Session["courseName"] = tableCourses.Rows[0]["Name"].ToString();
				
				if (!Convert.IsDBNull(tableCourses.Rows[0]["Keywords"])) {
					this.Session["HttpHeadItem"] = 
						"<META content=\""
						+ tableCourses.Rows[0]["Keywords"].ToString()
						+ "\" name=\"Keywords\">";
				}
				
				XmlDocument tdoc = new XmlDocument();
				tdoc.LoadXml(dsCourses.GetXml());
				doc.DocumentElement.InnerXml += tdoc.SelectSingleNode("dataSet/Courses").InnerXml;
				// Получение и вывод дополнений
				DataSet dsAdd = Course_GetAdditions(reqCourseCode);
				DataTable tableAdditions = dsAdd.Tables["Additions"];
				
				if (tableAdditions != null && tableAdditions.Rows.Count > 0) {
					bool hasLang = false; // Есть ли content для выбранного языка
					
					foreach (DataRow row in tableAdditions.Rows) {
						if (row["lang"].ToString() != row["DefLang"].ToString()
								&& !string.IsNullOrEmpty(row["Addition"].ToString())) {
							hasLang = true;
							break;
						}
					}
					
					if (hasLang) {
						for (int i = tableAdditions.Rows.Count - 1; i >= 0; i--) {
							DataRow row = tableAdditions.Rows[i];

							if (row["lang"].ToString() == row["DefLang"].ToString()) {
								tableAdditions.Rows.RemoveAt(i);
							}
						}
					}
					
					doc.DocumentElement.InnerXml += dsAdd.GetXml();
				}
			}
			//DCE.Service.GetLanguagePath(this.Page));
			this.DataBind();
		}

		static DataSet Course_GetAdditions(string reqCourseCode)
		{
			string select = string.Format(@"
SELECT	ct.DataStr AS Addition,
		l.Abbr AS lang,
		cl.Abbr as DefLang,
		c.CPublic
FROM	Content ct
	INNER JOIN Courses c
		ON ct.eid = c.Additions 
	INNER JOIN Languages l
		ON ct.Lang = l.id 
	INNER JOIN Languages cl
		ON c.CourseLanguage = cl.id 
WHERE	(c.Code='{0}') 
		AND (l.Abbr = '{1}' or l.Abbr = cl.Abbr)
ORDER BY
		ct.COrder,
		ct.Lang", reqCourseCode, LocalisationService.Language);

			return dbData.Instance.getDataSet(select, "dsAdd", "Additions");
		}
		
		protected void odsCourse_Selecting(object sender, System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs e)
		{
			e.InputParameters["reqCourseCode"] = this.Request["code"];
			e.InputParameters["reqCourseId"] = GuidService.Parse(this.Request["cId"]);
			e.InputParameters["CoursesRoot"] = this.ResolveUrl(DCE.Settings.getCoursesRoot());
		}
}
}