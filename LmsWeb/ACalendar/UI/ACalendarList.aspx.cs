using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using N2.ACalendar;



public partial class ACalendar_UI_ACalendarList : N2.Web.UI.ContentPage<ACalendarList>
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected ACalendar[] ACalendars
    {
        get
        {
            return (from child in this.CurrentItem.ACalendarContainer.Children.OfType<ACalendar>() select child).ToArray();
                    //where string.Equals(child.To, Profile.UserName, StringComparison.OrdinalIgnoreCase)
                    
        }
    } 
}
