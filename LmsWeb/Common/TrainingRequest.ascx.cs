namespace DCE.Common
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using N2.Lms.Items;

	/// <summary>
	/// Подача заявки на курс
	/// </summary>
	public partial  class TrainingRequest : DCE.BaseWebControl
	{
		IEnumerable<CourseRequest> m_requests;
		protected IEnumerable<CourseRequest> Requests {
			get {
				return this.m_requests ?? (this.m_requests = this.GetCourseRequests());
			}
		}
		IEnumerable<CourseRequest> GetCourseRequests()
		{
			using (var _ctx = new Lms.LmsDataContext()) {
				return (
					from _req in _ctx.CourseRequests
					where _req.Student == CurrentUser.UserID
					select new CourseRequest {
						Name = _req.id.ToString(),
						Course = DceAccessLib.DAL.CourseController.SelectByCodeOrId(string.Empty, _req.Course),
						StartDate = _req.StartDate,
						RequestDate = _req.RequestDate,
						Comments = _req.Comments,
					}).ToList();
			}
		}

		IEnumerable<Course> m_courses;
		protected IEnumerable<Course> Courses {
			get {
				return this.m_courses ?? (this.m_courses = this.GetCourses());
			}
		}
		IEnumerable<Course> GetCourses()
		{
			using (var _ctx = new Lms.LmsDataContext()) {
				return (
					from _course in _ctx.Courses
					join _lang in _ctx.Languages
						on _course.CourseLanguage equals _lang.id
					where !(from _req in _ctx.CourseRequests
						where _req.Student == CurrentUser.UserID
						select _req.Course).Contains(_course.id)
					orderby _ctx.GetStrContentAlt(_course.Name, LocalisationService.Language, _lang.Abbr)
					select new Course {
						Name = _course.id.ToString(),
						Title = _ctx.GetStrContentAlt(_course.Name, LocalisationService.Language, _lang.Abbr),
						Description = _ctx.GetContentAlt(_course.DescriptionShort, LocalisationService.Language, _lang.Abbr),
					}).ToList();
			}
		}

		static void AddCourseRequest(Guid courseId, DateTime startDate, string user, string comment)
		{
			using (var _ctx = new Lms.LmsDataContext()) {
				_ctx.CourseRequests.InsertOnSubmit(new Lms.CourseRequest {
					Course = courseId,
					StartDate = startDate,
					RequestDate = DateTime.Now,
					Comments = comment,
					Student = CurrentUser.UserID ?? Guid.Empty,
				});
				
				_ctx.SubmitChanges();
			}
		}

		static void RemoveCourseRequest(Guid requestId)
		{
			using (var _ctx = new Lms.LmsDataContext()) {
				_ctx.CourseRequests.DeleteOnSubmit(
					_ctx.CourseRequests.Single(_req => _req.id == requestId));
				_ctx.SubmitChanges();
			}
		}

		void SetMocks()
		{
			this.m_requests = new[] { new CourseRequest {
				Comments="Comments 1",
				Title = "Title 1",
				Course = new Course {
					Description = "Course description 1",
					Name = "Course Id 1",
					Title = "Course Title 1"
				}
			}};

			this.m_courses = new[] { new Course {
				Title = "Course 2",
				Description = "Course Description 2",
			}, new Course {
				Title = "Course m",
				Description = "Course Description m",
			}};
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			//SetMocks();
			
			var _id = Guid.Empty;
			if (GuidService.TryParse(this.Request["add"], out _id)) {
				AddCourseRequest(_id, DateTime.Now, Profile.UserName, string.Empty);
				Response.Redirect(Request.FilePath);
				Response.End();
			} else if (GuidService.TryParse(this.Request["del"], out _id)) {
				RemoveCourseRequest(_id);
				Response.Redirect(Request.FilePath);
				Response.End();
			}
		}
	}
}
