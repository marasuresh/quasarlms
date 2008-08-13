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

public partial class StudentReports_ReportFilterControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( !Page.IsPostBack )
        {
            regionDropDownList.Items.Add(new ListItem("Всi", ""));
            foreach( Regions.RegionInfo rgn in Regions.GetAllRegionsNoGlobal() )
            {
                regionDropDownList.Items.Add(new ListItem(rgn.Name, rgn.Name));
            }

            regionDropDownList.SelectedIndex = 0;

            groupDropDownList.Items.Add(new ListItem("Всi", ""));
            foreach( AdditionalReportsData.GroupsRow groupRow in new AdditionalReportsDataTableAdapters.GroupsTableAdapter().GetData().Rows )
            {
                groupDropDownList.Items.Add(new ListItem(groupRow.Name, groupRow.Name));
            }
            groupDropDownList.SelectedIndex = 0;





            if( Session["ReportFilter_StartDate"] is DateTime )
            {
                DateTime date = (DateTime)Session["ReportFilter_StartDate"];
                startDateCalendar.VisibleDate = date.Date;
                startDateCalendar.SelectedDate = date.Date;
                
                useStartDateCheckBox.Checked = true;
                startDateCalendar.Visible = true;
            }
            else
            {
                useStartDateCheckBox.Checked = false;
                startDateCalendar.Visible = false;
            }

            if( Session["ReportFilter_EndDate"] is DateTime )
            {
                DateTime date = (DateTime)Session["ReportFilter_EndDate"];
                endDateCalendar.VisibleDate = date.Date;
                endDateCalendar.SelectedDate = date.Date;

                useEndDateCheckBox.Checked = true;
                endDateCalendar.Visible = true;
            }
            else
            {
                useEndDateCheckBox.Checked = false;
                endDateCalendar.Visible = false;
            }

            studentTextBox.Text = Session["ReportFilter_StudentName"] as string;
            
            if( Session["ReportFilter_Group"] is int )
                try { groupDropDownList.SelectedIndex = (int)Session["ReportFilter_Group"]; }
                catch { }

            commentTextBox.Text = Session["ReportFilter_Comment"] as string;

            if( Session["ReportFilter_Region"] is int )
                try { regionDropDownList.SelectedIndex = (int)Session["ReportFilter_Region"]; }
                catch { }

            courseDomainTextBox.Text = Session["ReportFilter_CourseDomain"] as string;
            courseTextBox.Text = Session["ReportFilter_Course"] as string;
            themeTextBox.Text = Session["ReportFilter_Theme"] as string;
            testTextBox.Text = Session["ReportFilter_TestText"] as string;
        }
        else
        {
            if( useStartDateCheckBox.Checked )
            {
                Session["ReportFilter_StartDate"] = startDateCalendar.SelectedDate;
                startDateCalendar.Visible = true;
            }
            else
            {
                Session["ReportFilter_StartDate"] = null;
                startDateCalendar.Visible = false;
            }

            if( useEndDateCheckBox.Checked )
            {
                Session["ReportFilter_EndDate"] = endDateCalendar.SelectedDate;
                endDateCalendar.Visible = true;
            }
            else
            {
                Session["ReportFilter_EndDate"] = null;
                endDateCalendar.Visible = false;
            }
                                                                                                                  
            Session["ReportFilter_StudentName"] = studentTextBox.Text;
            Session["ReportFilter_Group"] = groupDropDownList.SelectedIndex;
            Session["ReportFilter_Comment"] = commentTextBox.Text;
            Session["ReportFilter_Region"] = regionDropDownList.SelectedIndex;
                                                                                                                  
            Session["ReportFilter_CourseDomain"] = courseDomainTextBox.Text;
            Session["ReportFilter_Course"] = courseTextBox.Text;
            Session["ReportFilter_Theme"] = themeTextBox.Text;
            Session["ReportFilter_TestText"] = testTextBox.Text;
        }
    }

    public void ApplyFilters(StudentsReportsData.FiltersRow filterRow)
    {
        if( useStartDateCheckBox.Checked )
            filterRow.StartDate = startDateCalendar.SelectedDate;
        else
            filterRow.SetStartDateNull();

        if( useEndDateCheckBox.Checked )
            filterRow.EndDate = endDateCalendar.SelectedDate;
        else
            filterRow.SetEndDateNull();

        filterRow.StudentName = GetText(studentTextBox.Text);
        filterRow.GroupName = GetText(groupDropDownList.SelectedValue);
        filterRow.Comment = GetText(commentTextBox.Text);
        filterRow.Region = GetText(regionDropDownList.SelectedValue);

        filterRow.CourseDomain = GetText(courseDomainTextBox.Text);
        filterRow.Course = GetText(courseTextBox.Text);
        filterRow.Theme = GetText(themeTextBox.Text);
        filterRow.TestText = GetText(testTextBox.Text);
    }

    static string GetText(string text)
    {
        if( string.IsNullOrEmpty(text) || text.Trim().Length == 0 )
            return null;
        else
            return text.Trim();
    }

    static DateTime? GetDate(string text)
    {
		DateTime resultDate;
		return !string.IsNullOrEmpty(text)
				&& DateTime.TryParse(text, out resultDate)
			? resultDate
			: (DateTime?)null;
    }
}
