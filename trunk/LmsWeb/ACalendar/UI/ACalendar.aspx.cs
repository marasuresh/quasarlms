using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using N2.ACalendar;
using N2.Resources;

public partial class ACalendar_UI_ACalendar :  N2.Templates.Web.UI.TemplatePage<N2.ACalendar.ACalendar>
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
 
    protected AEvent[] AEvents
    {
        get
        {
            //return new string[] { "учеба", "учеба", "учеба", "тесты", "учеба", "учеба", "учеба", "тесты", "экзамены", "каникулы" };

            return (from child in this.CurrentItem.Events select child).ToArray();
            //where string.Equals(child.To, Profile.UserName, StringComparison.OrdinalIgnoreCase)

        }
    }

    protected override void OnLoad(EventArgs e)
    {
        Register.JQuery(this.Page);
        Register.StyleSheet(this.Page, "~/Lms/UI/Js/jCal.small.css");
        Register.JavaScript(this.Page, "~/Lms/UI/Js/jCal.js");
        Register.JavaScript(this.Page, "~/Lms/UI/Js/jQuery.animate.clip.js");
        Register.JavaScript(this.Page, "~/Lms/UI/Js/jQuery.color.js");

        base.OnLoad(e);
    }


    protected string week_name (int n)
    {
        DateTime first_sept = new DateTime(DateTime.Now.Year, 9, 1);
        int _d = 7 - Convert.ToInt32(first_sept.DayOfWeek);
        DateTime first_dayofweek = new DateTime(DateTime.Now.Year, 9, 1 + _d);
        string _ret = n.ToString() +  " (";
        if (n == 1)
            _ret += first_sept.ToShortDateString() + " - " + first_dayofweek.ToShortDateString();
        else
        {
            _ret += first_dayofweek.AddDays((7 * (n - 2)) + 1).ToShortDateString();
            _ret += " - " + first_dayofweek.AddDays(7 * (n - 1)).ToShortDateString();
        }
        return _ret + ")";
    
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        var  cals =   ((ACalendarContainer)this.CurrentItem.Parent).Children;
        Response.Redirect(Server.MapPath("../../Upload") + "/" + ExcelExport.ExportToFile(cals));

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {



    }
}
