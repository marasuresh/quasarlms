namespace DCE.Common
{

	using System.Collections.Generic;
	using System.Linq;
	using N2.Lms.Items;
	/// <summary>
	/// Ñïèñîê êóðñîâ
	/// </summary>
	public partial  class CoursesCommon : DCE.BaseWebControl
	{
		IEnumerable<Course> m_ñourses;
		protected IEnumerable<Course> Courses {
			get {
				
				var _result = this.m_ñourses ?? (this.m_ñourses = this.GetCourses());
				System.Diagnostics.Debug.WriteLine(this.m_ñourses.Count(), "Lms");
				return _result;
			}
		}

		IEnumerable<Course> GetCourses()
		{
			using (var _ctx = new Lms.LmsDataContext()) {
				return (
					from _course in _ctx.Courses
					join _type in _ctx.CourseTypes
						on _course.Type equals _type.id into _ctype
					join _lng in _ctx.Languages
						on _course.CourseLanguage equals _lng.id
					let _name = _ctx.GetStrContentAlt(_course.Name, LocalisationService.Language, _lng.Abbr)
					orderby _name
					from _type in _ctype.DefaultIfEmpty()
					select new Course {
						Name = _course.id.ToString(),
						Title = _ctx.GetStrContentAlt(_course.Name, LocalisationService.Language, _lng.Abbr),
						Description = _ctx.GetContentAlt(_course.DescriptionShort, LocalisationService.Language, _lng.Abbr),
						Type = null != _type
							? _ctx.GetStrContentAlt(_type.Name, LocalisationService.Language, LocalisationService.DefaultLanguage)
							: string.Empty,
						IsReady = _course.isReady,
						IsPublic = _course.CPublic,
						Duration = _ctx.CourseDuration(_course.id) ?? 0
					}
				).ToList();
			}
		}
	}
}
