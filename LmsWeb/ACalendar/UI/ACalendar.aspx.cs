using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using N2.ACalendar;

public partial class ACalendar_UI_ACalendar : N2.Web.UI.ContentPage<ACalendar>
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
}
