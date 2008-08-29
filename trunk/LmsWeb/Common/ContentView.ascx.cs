namespace DCE.Common
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using N2.Lms.Items;
	using N2.Details;
	using N2.Resources;
	/// <summary>
	/// Просмотр учебных материалов по теме
	/// </summary>
	[Obsolete]
	public partial class ContentView : DCE.BaseTrainingControl
	{
		protected override void OnLoad(EventArgs e)
		{
			Register.JQuery(this.Page);
			Register.StyleSheet(this.Page, "~/Lms/UI/Js/jQuery.tabs.css");
			Register.JavaScript(this.Page, "~/Lms/UI/Js/jQuery.tabs.js");
			
			base.OnLoad(e);
		}

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

		Topic m_theme;
		protected Topic Theme {
			get {
				return this.m_theme
					?? (this.m_theme = Courses_GetContentDS(PageParameters.ID));
			}
		}

		Topic Courses_GetContentDS(Guid? themeId)
		{
			using (var _ctx = new Lms.LmsDataContext()) {
				var _result = (
					from _theme in _ctx.Themes
					join _course in _ctx.Courses
						on _ctx.CourseOfTheme(_theme.id) equals _course.id
					join _lang in _ctx.Languages
						on _course.CourseLanguage equals _lang.id
					where _theme.id == themeId
					select new {
						name = _theme.id.ToString(),
						title = _ctx.GetStrContentAlt(
							_theme.Name,
							LocalisationService.Language,
							_lang.Abbr),
						content =
							from _ct in _ctx.Contents
							join _l in _ctx.Languages
								on _ct.Lang equals _l.id
							where _ct.eid == _theme.Content
							where (_l.Abbr == LocalisationService.Language
								|| _l.Abbr == _lang.Abbr)
							select _ct.DataStr,
					}).First();
				
				var _topic = new Topic {
					Name = _result.name,
					Title = _result.title,
				};

				_topic.GetDetailCollection("Content", true).AddRange(_result.content);

				return _topic;
			}
		}
	}
}
