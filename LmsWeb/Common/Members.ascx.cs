namespace DCE.Common
{
	using System;
	using System.Web;
	using System.Data;
	using System.Web.UI.WebControls;
	/// <summary>
	/// Участники обучения
	/// </summary>
	public partial  class Members : DCE.BaseTrainingControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack) {
				Guid? reqTrId = PageParameters.trId;
				
				if (reqTrId.HasValue) {
					DCE.Service.TrainingID = reqTrId;
				}
			}
		}

		protected void odsStudents_Selecting(object sender, System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs e)
		{
			e.InputParameters["trainingId"] = DCE.Service.TrainingID;
		}
		
		protected void fvStudents_DataBound(object sender, EventArgs e)
		{
			this.Session["courseName"] = ((DataRowView)((FormView)sender).DataItem).Row["Name"];
		}
}
}
