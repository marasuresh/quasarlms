using System;
using System.Web.UI;

namespace DCE
{
	using System.Linq;
	using System.Xml.Linq;
	/// <summary>
	/// Выбор тренинга для обучения
	/// </summary>
	public partial class Trainings : DCE.BaseWebPage
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.onLoadCenter();
			Guid? _studentId = CurrentUser.UserID;
			var _trainings = DceAccessLib.DAL.TrainingController.Select(_studentId.Value);

			if (_trainings.Any()) {
				var _first = _trainings.First();
				DCE.Service.TrainingID = GuidService.Parse(_first.Name);

				this.LeftMenu.doc.InnerXml = new XElement("xml",
					new XElement("Items",
						from _training in _trainings
						select new XElement("item",
							new XElement("text", _training.Title),
							new XElement("control", "Welcome"),
							new XElement("id", _training.Name),
							new XElement("trId", _training.Name)))
				).ToString(SaveOptions.DisableFormatting);
				
			} else {
				this.Response.Redirect(Resources.PageUrl.PAGE_MAIN__WELCOME);
			}
		}

		void onLoadCenter()
		{
			string _cset = this.Request["cset"] as string;
			Control _ctl = string.IsNullOrEmpty(_cset)
					? this.LoadControl(@"~\Common\Welcome.ascx")
					: this.LoadControl(@"~\Common\" + _cset + ".ascx")
						?? this.LoadControl(@"~\Common\Welcome.ascx");
			
			this.PlaceHolder1.Controls.Add(_ctl);
		}
   }
}
