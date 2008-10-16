using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACalendar_UI_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IEnumerable<int> array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        this._eventList.DataBind();
        //_eventList.


    }
}
