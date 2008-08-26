namespace DCE.Common
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using N2.Lms.Items;
	/// <summary>
	/// Просмотр учебных материалов по теме
	/// </summary>
	public partial class ContentView : DCE.BaseTrainingControl
	{
		Training m_training;
		protected Training Training {
			get { return this.m_training
				?? (this.m_training = Training_Select(DCE.Service.TrainingID));
			}
		}

		static Training Training_Select(Guid? trId)
		{
			using (var _ctx = new Lms.LmsDataContext()) {
				return (
					from _training in _ctx.Trainings
					join _lang in _ctx.Languages
						on _training.Course1.CourseLanguage equals _lang.id
					where _training.id == trId
					select new Training {
						Name = _training.id.ToString(),
						Title = _ctx.GetStrContentAlt(_training.Course1.Name, LocalisationService.Language, _lang.Abbr),
						Course = new Course {
							Name = _training.Course1.id.ToString(),
							DiskFolder = _training.Course1.DiskFolder,
							IsPublic = _training.Course1.CPublic,
						}
					}).FirstOrDefault();
			}
		}

		IEnumerable<Topic> m_themes;
		protected IEnumerable<Topic> Themes {
			get {
				return this.m_themes
					?? (this.m_themes = Courses_GetContentDS(PageParameters.ID));
			}
		}

		IEnumerable<Topic> Courses_GetContentDS(Guid? themeId)
		{
			using (var _ctx = new Lms.LmsDataContext()) {
				return (
					from _theme in _ctx.Themes
					join _ct in _ctx.Contents
						on _theme.Content equals _ct.eid
					join _course in _ctx.Courses
						on _ctx.CourseOfTheme(_theme.id) equals _course.id
					join _lang in _ctx.Languages
						on _course.CourseLanguage equals _lang.id
					join _l in _ctx.Languages
						on _ct.Lang equals _l.id
					where _theme.id == themeId
					where _l.Abbr == LocalisationService.Language
					select new Topic {
						ContentUrl = _ct.DataStr,
						Title = _ctx.GetStrContentAlt(
							_theme.Name,
							LocalisationService.Language,
							_lang.Abbr),
						
					}
				).ToList();
			}
		}
	}
}
