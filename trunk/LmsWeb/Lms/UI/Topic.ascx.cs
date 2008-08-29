
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using N2.Lms.Items;
	using N2.Details;
	using N2.Resources;
	using N2.Web.UI;
	/// <summary>
	/// Просмотр учебных материалов по теме
	/// </summary>
public partial class Topic : ContentUserControl<N2.Lms.Items.Topic>
{
	protected override void OnLoad(EventArgs e)
	{
		Register.JQuery(this.Page);
		Register.StyleSheet(this.Page, "~/Lms/UI/Js/jQuery.tabs.css");
		Register.JavaScript(this.Page, "~/Lms/UI/Js/jQuery.tabs.js");

		base.OnLoad(e);
	}

	Training m_training;
	protected Training Training
	{
		get
		{
			return this.m_training
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
}