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

    protected N2.ACalendar.ACalendar[] ACalendars
    {
        get
        {
            return (from child in CurrentItem.ACalendarContainer.Children.OfType<N2.ACalendar.ACalendar>() select child).ToArray();
                    //where string.Equals(child.To, Profile.UserName, StringComparison.OrdinalIgnoreCase)
                    
        }
    }

    //protected ACalendar[] ACalendaries
    //{
    //    get
    //    {
    //        return (from child in this.CurrentItem.ACalendarContainer.ACalendars select child).ToArray();
    //        //where string.Equals(child.To, Profile.UserName, StringComparison.OrdinalIgnoreCase)

    //    }
    //    set
    //    {
    //        //this.CurrentItem.Events = value;
    //    }
    }


