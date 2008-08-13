using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

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
			DataSet dsCourses = Course_Select();
			tableCourses = dsCourses.Tables["item"];
			this.leftMenu = this.LeftMenu1;
			
			if(tableCourses != null && tableCourses.Rows.Count > 0) {
				Guid? cId = GuidService.Parse(this.Request["cId"]);
				DCE.Service.courseId =cId.HasValue ? cId : (Guid?)tableCourses.Rows[0]["cId"];
				this.leftMenu.doc.LoadXml("<xml>"+dsCourses.GetXml()+"</xml>");
			} else { // нет свободных курсов
				this.leftMenu.doc.LoadXml("<xml/>");
			}

			this.PlaceHolder1.Controls.Add(this.LoadControl("Common\\FreeIntro.ascx"));
		}

		static DataSet Course_Select()
		{
			string select = string.Format(@"
select	dbo.GetStrContentAlt(c.Name, '{0}',l.Abbr) as text,
		c.id as cId,
		c.id as id
from	dbo.Courses c,
		dbo.Languages l
where	c.isReady=1
		and c.CPublic=1
		and l.id=c.CourseLanguage
", LocalisationService.Language);

			return dbData.Instance.getDataSet(select, "Items", "item");
		}
   }
}
