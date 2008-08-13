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
	/// ¬ывод статистики по тестам, практическим работам
	/// </summary>
	public partial class Statistics : Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string cset = this.Request["cset"];
			
			try {
				if (string.IsNullOrEmpty(cset)) {
					cset = "TestStat";
				}
				
				this.PlaceHolder1.Controls.Add(this.LoadControl("Common\\" + cset + ".ascx"));
			}
			catch (System.IO.FileNotFoundException) {
			}
		}
	}
}
