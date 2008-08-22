namespace DCE.Common
{
	using System;
	using System.Web;
	using System.Data;
	using System.Web.UI.WebControls;
	using N2.Lms.Items;
	using System.Linq;

	/// <summary>
	/// Участники обучения
	/// </summary>
	public partial class Members : DCE.BaseTrainingControl
	{
		Training m_training;
		protected Training Training {
			get {
				return this.m_training ?? (this.m_training = this.GetTraining());
			}
		}

		Training GetTraining()
		{
			using (var _ctx = new Lms.LmsDataContext()) {
				var _tr = _ctx.Trainings.Single(_t =>
					_t.id == DCE.Service.TrainingID);

				return new Training {
					Name = _tr.id.ToString(),
					Members = DceAccessLib.DAL.StudentController.SelectByTraining(_tr.id),
					Title = _ctx.GetStrContentAlt(_tr.Name, LocalisationService.Language, LocalisationService.DefaultLanguage),
					StartOn = _tr.StartDate ?? DateTime.Now,
					FinishOn = _tr.EndDate ?? DateTime.MaxValue,
					Course = DceAccessLib.DAL.CourseController.SelectByCodeOrId(string.Empty, _tr.Course),
				};
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack) {
				Guid? reqTrId = PageParameters.trId;

				if (reqTrId.HasValue) {
					DCE.Service.TrainingID = reqTrId;
				}
			}
		}
	}
}
