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
using System.Collections.Generic;

public partial class Tools_DepartmentReports_ReportTableControl : System.Web.UI.UserControl
{
    protected StudentsReportsData data;
    List<Tools_DepartmentReports_RegionSubControl> childRegionControls;

    bool m_ShowAllStudents;

    public bool ShowFilter
    {
        get { return ReportFilterControl1.Visible; }
        set { ReportFilterControl1.Visible = value; }
    }

    public bool ShowAllStudents
    {
        get { return m_ShowAllStudents; }
        set { m_ShowAllStudents = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool refreshCache = true;
        if( this.IsPostBack )
            refreshCache = false;

        data = StudentsReportsDataBuilder.GetReportsData(refreshCache, ShowAllStudents);

        BuildChildren();
    }

    void BuildChildren()
    {
        regionsPlaceHolder.Controls.Clear();
        ReportFilterControl1.ApplyFilters(StudentsReportsDataBuilder.GetFilters(data));

        Dictionary<string, string> regionNameList = new Dictionary<string, string>();
        foreach( StudentsReportsData.StudentsRow studentRow in data.Students )
        {
            if( studentRow.IsRegionNameNull() )
                regionNameList[""] = "";
            else
                regionNameList[studentRow.RegionName] = "";
        }

        childRegionControls = new List<Tools_DepartmentReports_RegionSubControl>(regionNameList.Count);
        foreach( string regionName in regionNameList.Keys )
        {
            Tools_DepartmentReports_RegionSubControl regionControl = (Tools_DepartmentReports_RegionSubControl)LoadControl("RegionSubControl.ascx");
            regionControl.Bind(data, regionName);
            if( !regionControl.GetVisibleByFilter() )
                continue;

            childRegionControls.Add(regionControl);

            regionControl.ParentDeepLevel = 0;
        }

        childRegionControls.Sort();

        foreach( Tools_DepartmentReports_RegionSubControl regionControl in childRegionControls )
        {
            regionsPlaceHolder.Controls.Add(regionControl);
        }
    }

    public string SortColumn
    {
        get { return StudentsReportsDataBuilder.GetFilters(data).SortColumn; }
        set { StudentsReportsDataBuilder.GetFilters(data).SortColumn = value; }
    }

    protected void dateSortLink_Click(object sender, EventArgs e)
    {
        SortColumn = StudentsReportsDataBuilder.SortColumn.Date;
        BuildChildren();
    }
    protected void tryCountSortLink_Click(object sender, EventArgs e)
    {
        SortColumn = StudentsReportsDataBuilder.SortColumn.TryCount;
        BuildChildren();
    }
    protected void questionCountSortLink_Click(object sender, EventArgs e)
    {
        SortColumn = StudentsReportsDataBuilder.SortColumn.QuestionCount;
        BuildChildren();
    }
    protected void requiredPointsSortLink_Click(object sender, EventArgs e)
    {
        SortColumn = StudentsReportsDataBuilder.SortColumn.RequiredPoints;
        BuildChildren();
    }
    protected void collectedPointsSortLink_Click(object sender, EventArgs e)
    {
        SortColumn = StudentsReportsDataBuilder.SortColumn.CollectedPoints;
        BuildChildren();
    }
    protected void answerPercentLink_Click(object sender, EventArgs e)
    {
        SortColumn = StudentsReportsDataBuilder.SortColumn.AnswerPercent;
        BuildChildren();
    }

    protected void nameSortLink_Click(object sender, EventArgs e)
    {
        SortColumn = StudentsReportsDataBuilder.SortColumn.Name;
        BuildChildren();
    }
}
