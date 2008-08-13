namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// ѕросмотр и изменение свойств студента
	/// </summary>
	public partial  class UserProperty : DCE.BaseWebControl
	{
		protected void dsUsers_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
		{
			e.Command.Parameters["@id"].Value = CurrentUser.UserID;
		}
		
		protected void fvUserProperties_ItemUpdating(object sender, FormViewUpdateEventArgs e)
		{
			FormView _fv = sender as FormView;
			FormViewRow _row = _fv.Row;
			FileUpload _file = (FileUpload)_row.FindControl("fuPhoto");
			
			if(_file.HasFile) {

				if (!(e.OldValues["Photo"] is Guid) || (Guid)e.OldValues["Photo"] == Guid.Empty) {
					e.NewValues["Photo"] = Guid.NewGuid();
				}

				DceAccessLib.DAL.PhotoController.Update((Guid)e.NewValues["Photo"], _file.FileBytes, _file.PostedFile.ContentType);
			}
		}
}
}
