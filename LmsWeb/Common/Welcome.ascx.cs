namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Xml;

	/// <summary>
	/// Страница Welcome.
	/// </summary>
	public partial  class Welcome : DCE.BaseTrainingControl
	{
		bool m_blocking;
		protected bool Blocking {
			get {
				return this.m_blocking;
			}
		}

		protected DateTime? ParseDateTimeDefault(string value)
		{
			DateTime _result;
			return DateTime.TryParse(value, out _result) ? _result : default(DateTime?);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.doc.InnerXml = "<xml/>";
			
			Guid? reqTrId = PageParameters.trId;
			
			if (reqTrId.HasValue) {
				DCE.Service.TrainingID = reqTrId;
			}
			
			Guid? courseId = DCE.Service.courseId;
			Guid? trainingId = DCE.Service.TrainingID;
			string CoursesRoot = this.ResolveUrl(DCE.Settings.getCoursesRoot());
			Guid? studentId = CurrentUser.UserID;
			string cdAction = this.Request.Form["cdAction"];
			
			if (!string.IsNullOrEmpty(cdAction)) {
				string checkUseCD = this.Request.Form["checkUseCD"];
				string defPath=this.Request.Form["cdDefPath"];
				string currPath=this.Request.Form["cdPath"];
				
				if(studentId.HasValue && trainingId.HasValue) {
					string select;
					DataSet ds = CCPath_Select(trainingId, studentId);
					DataTable tablePath = ds.Tables["CDPath"];
					
					if (tablePath != null) {
						DataRow row = null;
						
						if (tablePath.Rows.Count == 0) {
							row = tablePath.NewRow();
							row["studentId"] = studentId;
							row["trainingId"] = trainingId;
							tablePath.Rows.Add(row);
						} else {
							row = tablePath.Rows[0];
						}
						
						if (!string.IsNullOrEmpty(checkUseCD)) {
							
							if (!string.IsNullOrEmpty(currPath) && currPath != defPath) {
								row["cdPath"]=currPath;
							}
							
							row["useCDLib"]=true;
						} else {
							row["useCDLib"]=false;
						}
						
						dbData.Instance.UpdateDataSet("select * from CDPath", "CDPath", ref ds);
					}
				}
			}
			
			Guid? qwId = GuidService.Parse(this.Request.Form["qwId"]);
			
			if(qwId.HasValue) {
				this.Session["Back"]=this.Request.Url.AbsoluteUri;
				this.Response.Redirect(Resources.PageUrl.PAGE_TRAININGS + "?index="+this.Request["index"]+
					"&cset=Questionnaire&qId="+qwId);
			}
			
			if (trainingId.HasValue && studentId.HasValue) {
				bool ie = Request.Browser.Browser.ToUpper().IndexOf("IE") > -1;

				DataSet dsCourses = Courses_Select(trainingId, CoursesRoot, studentId, ie);
				DataTable tableCourses = dsCourses.Tables["Courses"];
				
				if (tableCourses != null && tableCourses.Rows.Count == 1) {
					Session["CourseLanguage"] = tableCourses.Rows[0]["CourseLanguage"].ToString();
				}

				DataSet dsBlocking = Blocking_Select(trainingId, studentId);
				DataTable tableBlock = dsBlocking.Tables["Blocking"];
				
				if (tableBlock != null && tableBlock.Rows.Count > 0) {
					this.m_blocking = true;
				}
				
				if (tableCourses != null && tableCourses.Rows.Count == 1) {
					DCE.Service.courseId = courseId = (Guid?)tableCourses.Rows[0]["id"];
					this.Session["courseName"] = tableCourses.Rows[0]["Name"].ToString();
					
					XmlDocument tdoc = new XmlDocument();
					tdoc.LoadXml(dsCourses.GetXml());
					doc.DocumentElement.InnerXml += tdoc.SelectSingleNode("dataSet/Courses").InnerXml;
					
					this.fvCourse.DataSource = dsCourses;
				}
			}
			
			if (!this.IsPostBack) {
				this.DataBind();
			}
		}

		static DataSet Courses_Select(Guid? trainingId, string CoursesRoot, Guid? studentId, bool ie)
		{
			string select = string.Format(@"
select distinct
		dbo.GetStrContentAlt(c.Name, '{0}', l.Abbr) as Name,
		dbo.GetContentAlt(c.DescriptionShort,'{0}', l.Abbr) as Description,
		'{1}' as cRoot,
		c.DiskFolder,
		l.Abbr as CourseLanguage,
		dbo.GetStrContentAlt(c.DescriptionLong,'{0}', l.Abbr) as FullDescription,
		c.id,
		t.StartDate,
		t.EndDate,
		t.Code,
		cd.useCDLib,
		cd.cdPath,
		t.Instructors,
		t.Curators,
		ts.id as qId,
		tr.id as qrId,
		'{2}' as ie
FROM	TestResults tr
	RIGHT OUTER JOIN Tests ts
	RIGHT OUTER JOIN Trainings t
	INNER JOIN Courses c
	inner join dbo.Languages l
		on l.id=c.CourseLanguage
		ON t.Course = c.id 
		ON ts.id = c.StartQuestionnaire 
	inner join Students s on s.id='{3}'
		on tr.Test = ts.id
		and tr.Student='{3}'
	left join CDPath cd
		on cd.studentId=s.id
		and cd.trainingId=t.id
WHERE	(t.id = '{4}')
		and t.isActive=1",
							LocalisationService.Language,
							CoursesRoot,
							ie.ToString(),
							studentId,
							trainingId);

			return dbData.Instance.getDataSet(select, "dataSet", "Courses");
		}

		static DataSet Blocking_Select(Guid? trainingId, Guid? studentId)
		{
			string select0 = string.Format(@"
select	id
from	dbo.TrainingBlocking
where	Student='{0}'
		and Training='{1}'",
					studentId,
					trainingId);

			return dbData.Instance.getDataSet(select0, "DataSet", "Blocking");
		}

		static DataSet CCPath_Select(Guid? trainingId, Guid? studentId)
		{
			string select = string.Format(@"
select	*
from	CDPath 
where	studentId='{0}'
		and trainingId='{1}'",
					studentId,
					trainingId);

			return dbData.Instance.getDataSet(select, "dataSet", "CDPath");
		}

		protected void odsTestResults_Selecting(object sender, System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs e)
		{
			e.InputParameters["courseId"] = DCE.Service.courseId;
			e.InputParameters["studentId"] = CurrentUser.UserID;
		}
		
		protected void odsBulletins_Selecting(object sender, System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs e)
		{
			e.InputParameters["trainingId"] = DCE.Service.TrainingID;
		}
}
}
