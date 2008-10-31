using N2.ACalendar;
using System.Web.Security;
using System.Collections.Generic;


public partial class ACalendar_UI_ACalendarList :   N2.Templates.Web.UI.TemplatePage<N2.ACalendar.ACalendarList>
{

    protected N2.Collections.ItemList ACalendaries
    {
        get
        {
            return this.CurrentItem.GetChildren();
            //return CurrentItem.ACalendarContainer.MyCalendars  ;  
        }

    }
    protected bool IsEditable
    {
        get
        {
            return Roles.IsUserInRole(this.User.Identity.Name, "Administrators");
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
 
    //protected void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    //{
    //    //var cals = (this.CurrentItem.Parent).Children as IList<N2.ACalendar.ACalendar>;
    //    string strURL = "~/Reporting/ReportFiles/";
    //    string path = Server.MapPath("../../Reporting/ReportFiles/");
    //    strURL += ExcelExport.ExportToFile(ACalendaries, path);
    //    //Response.Redirect( ExcelExport.ExportToFile(cals, path))
    //    this.lnkExcel.NavigateUrl = strURL;
    //    this.lnkExcel.Text = "Академические календари";



    //}

    protected void btnXL_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        string strURL = "~/Reporting/ReportFiles/";
        string path = Server.MapPath("../../Reporting/ReportFiles/");
        strURL += ExcelExport.ExportToFile(ACalendaries, path);
        //Response.Redirect( ExcelExport.ExportToFile(cals, path))
        this.lnkExcel.NavigateUrl = strURL;
        this.lnkExcel.Text = "Академические календари";

    }
}