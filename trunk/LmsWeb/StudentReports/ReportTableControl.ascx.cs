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

public partial class StudentReports_ReportTableControl : System.Web.UI.UserControl
{
	#region Fields
	protected StudentsReportsData data;
    List<StudentReports_CourseDomainSubControl> childDomainControls;
    bool m_ShowAllStudents;
	#endregion Fields
	#region Properties
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
	public string SortColumn
	{
		get { return StudentsReportsDataBuilder.GetFilters(data).SortColumn; }
		set { StudentsReportsDataBuilder.GetFilters(data).SortColumn = value; }
	}
	#endregion Properties
	#region Event handlers
	protected void Page_Load(object sender, EventArgs e)
    {
        bool refreshCache = true;
        if( this.IsPostBack )
            refreshCache = false;

        data = StudentsReportsDataBuilder.GetReportsData(refreshCache, ShowAllStudents);

        BuildChildren();
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
	#endregion Event handlers
	#region Methods
	void BuildChildren()
	{
		topLevelDomainsPlaceHolder.Controls.Clear();
		ReportFilterControl1.ApplyFilters(StudentsReportsDataBuilder.GetFilters(data));

		List<StudentsReportsData.CourseDomainRow> topLevelDomains = new List<StudentsReportsData.CourseDomainRow>();
		foreach(StudentsReportsData.CourseDomainRow domainRow in data.CourseDomain) {
			if(domainRow.IsParentIDNull())
				topLevelDomains.Add(domainRow);
		}

		childDomainControls = new List<StudentReports_CourseDomainSubControl>(topLevelDomains.Count);
		foreach(StudentsReportsData.CourseDomainRow domainRow in topLevelDomains) {
			StudentReports_CourseDomainSubControl domainControl = (StudentReports_CourseDomainSubControl)LoadControl("CourseDomainSubControl.ascx");
			domainControl.CourseDomainRow = domainRow;
			if(!domainControl.GetVisibleByFilter())
				continue;

			childDomainControls.Add(domainControl);

			domainControl.ParentDeepLevel = 0;
		}

		childDomainControls.Sort();

		foreach(StudentReports_CourseDomainSubControl domainControl in childDomainControls) {
			topLevelDomainsPlaceHolder.Controls.Add(domainControl);
		}
	}
	#endregion Methods
}
