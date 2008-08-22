using System;
using System.Data;

namespace DCE
{
	/// <summary>
	/// Выбор свободного курса для изучения
	/// </summary>
	public partial class FreeCourses : DCE.BaseWebPage
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			DataTable tableCourses = null;
			DataSet dsCourses = DceAccessLib.DAL.CourseController.SelectFreeCourses();
			tableCourses = dsCourses.Tables["item"];
			
			if(tableCourses != null && tableCourses.Rows.Count > 0) {
				Guid? cId = GuidService.Parse(this.Request["cId"]);
				DCE.Service.courseId =cId.HasValue ? cId : (Guid?)tableCourses.Rows[0]["cId"];
				this.LeftMenu.doc.LoadXml("<xml>"+dsCourses.GetXml()+"</xml>");
			} else { // нет свободных курсов
				this.LeftMenu.doc.LoadXml("<xml/>");
			}
		}
	}
}
