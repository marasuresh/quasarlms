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
	public partial  class CourseIntro : DCE.BaseWebControl
	{
		protected override void OnInit(EventArgs e)
		{
			if (null != this.Course) {
				this.Session["courseName"] = this.Course;

				this.Course["MetaKeywrods"] = this.Course.Keywords;
				this.Course["MetaDescription"] = this.Course.Description;

				var _metaApplier = new N2.Templates.SEO.TitleAndMetaTagApplyer(
					this.Page, this.Course);
			}

			base.OnInit(e);
		}

		Course m_course;
		protected Course Course {
			get { return this.m_course ?? (this.m_course = this.GetCourse()); }
		}

		Course GetCourse()
		{
			string _code = this.Request["code"];
			Guid? _id = GuidService.Parse(this.Request["cId"]);

			return DceAccessLib.DAL.CourseController.SelectByCodeOrId(
					_code,
					_id);
		}

		protected void odsCourse_Selecting(object sender, System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs e)
		{
			e.InputParameters["reqCourseCode"] = this.Request["code"];
			e.InputParameters["reqCourseId"] = GuidService.Parse(this.Request["cId"]);
			e.InputParameters["CoursesRoot"] = this.ResolveUrl(DCE.Settings.getCoursesRoot());
		}
}
}