using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Trainings_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( string.IsNullOrEmpty(Request["id"]) )
            Response.Redirect("~/Tools/Trainings");

        bool canWrite = new TrainingQueriesTableAdapters.dcetools_Access_GetCanObjectID_WriteTableAdapter().GetData(
            CurrentUser.Region.ID,
            PageParameters.ID)[0].Column1;

        TrainingDetails1.ReadOnly = !canWrite;
        TrainingSchedule1.ReadOnly = !canWrite;
    }
    protected void studentsButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Students/Default.aspx?id=" + Request["id"]);
    }
    protected void testsButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tests/Default.aspx?id=" + Request["id"]);
    }
    protected void forumButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Forum/Default.aspx?id=" + Request["id"]);
    }
    protected void announcementsButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Announcements/Default.aspx?id=" + Request["id"]);
    }
    protected void tasksButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tasks/Default.aspx?id=" + Request["id"]);
    }
}
