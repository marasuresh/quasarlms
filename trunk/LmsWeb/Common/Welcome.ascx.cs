namespace DCE.Common
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using System.Xml;
	using N2.Lms.Items;
	using Rhino.Mocks;
	
	/// <summary>
	/// Страница Welcome.
	/// </summary>
	public partial  class Welcome : DCE.BaseTrainingControl
	{
		Training m_training;
		protected Training Training { get; set; }



		protected IEnumerable<TestResult> Results { get; set; }

		bool? m_blocked;
		protected bool IsBlocked {
			get {
				return (this.m_blocked ?? (this.m_blocked = this.GetBlocking())).Value;
			}
		}
		bool GetBlocking()
		{
			using (var _ctx = new Lms.LmsDataContext()) {
				return 
					_ctx.TrainingBlockings.Any(_b =>
						_b.Student == CurrentUser.UserID
							&& _b.Training == (PageParameters.trId ?? Guid.Empty));
			}
		}

		protected DateTime? ParseDateTimeDefault(string value)
		{
			DateTime _result;
			return DateTime.TryParse(value, out _result) ? _result : default(DateTime?);
		}

		void SetMocks()
		{
			this.Training = new Training {
				Name = "Training Id 1",
				Title = "Training 1",
				Course = new Course {
				},
			};
			
			this.Results = new[] {
				new TestResult {
					AllowedAttempts = 3,
					AttemptsCount = 4,
					CompletedOn = DateTime.Now,
					IsComplete = true,
					IsSkipped = false,
					Points = 123,
					Theme = "Theme 1",

					Test = new Test {
						Name = "0000",
						Parent = this.Training.Course,
					}
				},
				//MockRepository.GenerateStub<TestResult>()
			};
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.SetMocks();
			
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
					DCE.Service.CourseLanguage = tableCourses.Rows[0]["CourseLanguage"].ToString();
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
	}
}
