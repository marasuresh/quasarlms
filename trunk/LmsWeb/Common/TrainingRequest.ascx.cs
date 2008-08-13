namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Xml;
	using System.Xml.Xsl;

	/// <summary>
	/// Подача заявки на курс
	/// </summary>
	public partial  class TrainingRequest : DCE.BaseWebControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Guid? studentId = CurrentUser.UserID;
			
			if (!studentId.HasValue) {
				this.Session.Remove("studentId");
				this.Response.Redirect(Resources.PageUrl.PAGE_MAIN + "?index=3&notreg=");
			}
			
			DCE.Service.LoadXmlDoc(this.Page, doc, "CourseRequests.xml");
			// Подача заявок
			string dateFailureId = null;
			#region Submit requests
			DataSet dsCourse = Course_SelectRequestsNotSubscribed(studentId);
			DataTable tableCourse = dsCourse.Tables["Courses"];
			
			if (tableCourse != null && tableCourse.Rows.Count > 0 && studentId.HasValue) {
				string formAction = this.Request.Form["formAction"];
				
				if (formAction == "courseCheck") {
					string id = "";
					
					try {
						
						foreach(DataRow row in tableCourse.Rows) {
							id = row["id"].ToString();
							string checkBox = this.Request.Form[id];
							
							if (checkBox != null) {
								string startDate = this.Request.Form["date"+id];
								string comments = this.Request.Form["comment"+id];
								DateTime sqlDT = DateTime.Now;
								
								if (startDate != null) {
									sqlDT = DateTime.Parse(startDate);
								}
								
								dbData.Instance.ExecSQL(@"
insert into dbo.CourseRequest
	(Student, Course, RequestDate, StartDate, Comments) 
values 
                              ('" +studentId+"','"+id+@"', 
                              {fn NOW()}, 
                              CONVERT(datetime, '"
                           + sqlDT.ToString("s", System.Globalization.DateTimeFormatInfo.InvariantInfo)+"', 126), '"+comments+"')");
							}
						}
					} catch (FormatException) {
						dateFailureId = id;
					}
				}
			}
			#endregion Submit requests
			
			string select;
			// Поданые заявки
			DataSet dsRequest = Course_SelectRequestByStudent(studentId);
			DataTable tableRequest = dsRequest.Tables["Request"];
			
			if (tableRequest != null && tableRequest.Rows.Count > 0 && studentId.HasValue) {
				string formAction = this.Request.Form["formAction"];
				
				if (formAction == "courseUncheck") {
					foreach (DataRow row in tableRequest.Rows) {
						string id = row["id"].ToString();
						string checkBox = this.Request.Form[id];
						
						if (checkBox != null) {
							dbData.Instance.ExecSQL(@"delete from dbo.CourseRequest where id='" + id + "'");
						}
					}
				}
			}
			
			bool isArea = false;
			bool isSearch = false;
			string searchStr = this.Request.Form["keywords"];
			
			if (!string.IsNullOrEmpty(searchStr)) {
				isSearch = true;
				// Поиск и вывод результатов
				dsCourse = Course_SelectByStudent(studentId);
				tableCourse = dsCourse.Tables["Courses"];
				
				if (tableCourse != null && tableCourse.Rows.Count > 0) {
					
					for (int i=tableCourse.Rows.Count-1; i >= 0; i--) {
						DataRow row = tableCourse.Rows[i];
						string[] searchWords = searchStr.Split(new char[] {' ',';',':','.',',','/','\\','?','\'','|','+'});
						bool found = false;
						
						foreach (string word in searchWords) {
							
							if (!string.IsNullOrEmpty(word)) {
								
								if (!Convert.IsDBNull(row["Name"])
										&& row["Name"].ToString().ToLower().IndexOf(word.ToLower()) > -1
										|| !Convert.IsDBNull(row["Area"])
										&& row["Area"].ToString().ToLower().IndexOf(word.ToLower()) > -1
										|| !Convert.IsDBNull(row["Keywords"])
										&& row["Keywords"].ToString().ToLower().IndexOf(word.ToLower()) > -1
										|| !Convert.IsDBNull(row["Author"])
										&& row["Author"].ToString().ToLower().IndexOf(word.ToLower()) > -1
										|| !Convert.IsDBNull(row["Code"])
										&& row["Code"].ToString().ToLower().IndexOf(word.ToLower()) > -1) {
									found = true;
								}
							}
						}

						if (!found) {
							tableCourse.Rows.RemoveAt(i);
						}
					}
					doc.SelectSingleNode("/xml/tableAccessibleCourses").InnerXml += dsCourse.GetXml();
					doc.DocumentElement.InnerXml += tableCourse.Rows.Count == 0 ? "<NotFound>true</NotFound>" : "<Found>true</Found>";
					doc.DocumentElement.InnerXml += "<searchStr>"+searchStr+"</searchStr>";
				}
			} else {
				// Вывод доступных курсов
				Guid? areaId = PageParameters.ID;
				isArea = areaId.HasValue;
				DataSet dsCourse0 = Course_SelectAvailable(studentId, dateFailureId, areaId);
				DataTable tableCourse0 = dsCourse0.Tables["Courses"];

				if (tableCourse0 != null && tableCourse0.Rows.Count > 0 && studentId.HasValue) {
					doc.SelectSingleNode("/xml/tableAccessibleCourses").InnerXml += dsCourse0.GetXml();
				}
			}
			// Вывод поданных заявок
			DataSet dsRequest0 = Course_SelectRequests(studentId);
			DataTable tableRequest0 = dsRequest.Tables["Request"];

			if (tableRequest0 != null && tableRequest0.Rows.Count > 0 && studentId.HasValue) {
				XmlNode requestRoot = doc.SelectSingleNode("/xml/tableExisdataTRequests");
				requestRoot.InnerXml += dsRequest0.GetXml();
			}
			
			if (!isArea && !isSearch) {
				DataSet dsAreas = Areas_Select();
				doc.DocumentElement.InnerXml += dsAreas.GetXml();
			}

			XslTransform trans = new XslTransform();
			trans.Load(this.Page.MapPath(@"~/xsl/CourseRequest.xslt"));
			
			this.Xml1.Document = doc;
			this.Xml1.TransformArgumentList = new XsltArgumentList();
			this.Xml1.Transform = trans;
		}

		static DataSet Course_SelectAvailable(Guid? studentId, string dateFailureId, Guid? areaId)
		{
			string select0 = string.Format(@"
select	c.id as cId,
		c.Code,
		dbo.GetStrContentAlt(c.Name, '{0}', l.Abbr) as name,
		{1} {{ fn NOW() }} as currDate, 
		dbo.GetContentAlt(c.DescriptionShort, '{0}', l.Abbr) as description 
from	dbo.Courses c
	inner join dbo.Languages l
		on l.id = c.CourseLanguage
where	c.id not in (
			select	Course
			from	dbo.CourseRequest 
			where	Student = '{2}') 
		and CPublic=0
		and isReady=1", 
					LocalisationService.Language,
					// Указание на неправильную дату
					!string.IsNullOrEmpty(dateFailureId) ? "dbo.isIdEqual(c.id, '" + dateFailureId + "') as failureDate," : string.Empty,
					studentId);

			if (areaId.HasValue) {
				select0 += " and dbo.isCourseOfArea(c.id, '" + areaId + "')=1";
			} else {
				select0 += " and Area is NULL";
			}

			select0 += " order by dbo.GetStrContentAlt(c.Name, '" + LocalisationService.Language + "',l.Abbr)";
			return dbData.Instance.getDataSet(select0, "dataSet", "Courses");
		}

		static DataSet Course_SelectByStudent(Guid? studentId)
		{
			string select0 = string.Format(@"
select	dbo.GetStrContentAlt(c.Name,'{0}', l.Abbr) as name,
		dbo.GetContentAlt(c.DescriptionShort,'{0}', l.Abbr) as nescription,
		dbo.GetStrContentAlt(cd.Name,'{0}','{1}') as Area,
		dbo.GetStrContentAlt(ct.Name,'{0}','{1}') as Type,
		dbo.GetStrContentAlt(c.Keywords,'{0}', l.Abbr) as Keywords,
		dbo.GetStrContentAlt(c.Author,'{0}', l.Abbr) as Author,
		dbo.CourseDuration(c.id) as Duration,
		c.Code,
		c.isReady,
		{{ fn NOW() }} as currDate, 
		c.CPublic,
		c.Cost1,
		c.Cost2,
		c.id as cId
from	dbo.CourseType ct
	right join dbo.Courses c
		on c.Type=ct.id
	inner join dbo.Languages l
		on l.id=c.CourseLanguage
	left outer join CourseDomain cd
		on cd.id=c.Area 
where	c.id not in (
			select	Course
			from	dbo.CourseRequest 
			where	Student = '{2}') 
					and CPublic=0
					and isReady=1
			order by dbo.GetStrContentAlt(c.Name,'{0}',l.Abbr
		)",			LocalisationService.Language,
					LocalisationService.DefaultLanguage,
					studentId);
			
			return dbData.Instance.getDataSet(select0, "dataSet", "Courses");
		}

		static DataSet Course_SelectRequestsNotSubscribed(Guid? studentId)
		{
			string select0 = @"
select	id
from	dbo.Courses
where	id not in (
			select	Course
			from	dbo.CourseRequest
			where Student = '"
			+ studentId + "')";

			return dbData.Instance.getDataSet(select0, "dataSet", "Courses");
		}

		static DataSet Course_SelectRequestByStudent(Guid? studentId)
		{
			string select0 = @"
select	id
from	CourseRequest
where	Student = '" + studentId + "'";
			return dbData.Instance.getDataSet(select0, "dataSet", "Request");
		}

		static DataSet Course_SelectRequests(Guid? studentId)
		{
			///TODO refactor into separate method
			string select0 = @"select c.Code, c.id as cId,
               dbo.GetStrContentAlt(c.Name,'" + LocalisationService.Language + @"', l.Abbr) as name, 
               dbo.GetContentAlt(c.DescriptionShort,'" + LocalisationService.Language + @"', l.Abbr) as description,
               r.RequestDate, r.StartDate, r.Comments, r.id as id
            from Courses c inner join dbo.Languages l on l.id=c.CourseLanguage,
            CourseRequest r where r.Course = c.id
               and r.Student = '" + studentId + "'";

			return dbData.Instance.getDataSet(select0, "dataSet", "Request");
		}

		static DataSet Areas_Select()
		{
			string select0 = @"
                  select cd.id,
                     dbo.GetStrContentAlt(cd.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Name
                  from dbo.CourseDomain cd
                  where cd.Parent is NULL
                     and dbo.isAreaHasCourses(cd.id)=1";
			return dbData.Instance.getDataSet(select0, "ds", "Area");
		}
	}
}
