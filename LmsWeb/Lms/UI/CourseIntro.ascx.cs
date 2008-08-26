namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Xml;
	using System.Web.UI.HtmlControls;
	using System.Linq;
	using N2.Lms.Items;
	
	/// <summary>
	/// Отображение информации о курсе
	/// </summary>
	public partial  class CourseIntro : N2.Web.UI.UserControl<Course>
	{
		protected override void OnInit(EventArgs e)
		{
			if (null != this.CurrentItem) {
				this.Session["courseName"] = this.CurrentItem.Title;

				this.CurrentItem["MetaKeywrods"] = this.CurrentItem.Keywords;
				this.CurrentItem["MetaDescription"] = this.CurrentItem.Description;

				var _metaApplier = new N2.Templates.SEO.TitleAndMetaTagApplyer(
					this.Page, this.CurrentItem);
			}

			base.OnInit(e);
		}

		Course GetCourse()
		{
			string _code = this.Request["code"];
			Guid? _id = GuidService.Parse(this.Request["cId"]);

			return DceAccessLib.DAL.CourseController.SelectByCodeOrId(
					_code,
					_id);
		}
	}
}