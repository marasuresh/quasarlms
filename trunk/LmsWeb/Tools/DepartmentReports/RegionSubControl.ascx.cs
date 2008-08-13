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

public partial class Tools_DepartmentReports_RegionSubControl : System.Web.UI.UserControl,
    IComparable<Tools_DepartmentReports_RegionSubControl>
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    int m_ParentDeepLevel = 0;

    string m_RegionName;
    StudentsReportsData m_Data;

    List<Tools_DepartmentReports_DepartmentSubControl> childDepartmetControls = new List<Tools_DepartmentReports_DepartmentSubControl>();

    IList<DateTime> m_CompletionDates;
    int m_TryCount;

    int m_QuestionCount;
    int m_TotalRequiredPoints;
    int m_CollectedPoints;
    int m_TotalAnswerCount;
    int m_RightAnswerCount;

    public int TryCount
    {
        get { return m_TryCount; }
    }

    public int QuestionCount
    {
        get { return m_QuestionCount; }
    }

    public int TotalRequiredPoints
    {
        get { return m_TotalRequiredPoints; }
    }

    public int CollectedPoints
    {
        get { return m_CollectedPoints; }
    }

    public int TotalAnswerCount
    {
        get { return m_TotalAnswerCount; }
    }

    public int RightAnswerCount
    {
        get { return m_RightAnswerCount; }
    }

    public double AnswerPercent
    {
        get
        {
            return
                m_TotalAnswerCount == 0 ?
                m_RightAnswerCount * 100 :
                m_RightAnswerCount * 100 / m_TotalAnswerCount;
        }
    }

    public IList<DateTime> CompletionDates
    {
        get { return m_CompletionDates; }
    }


    public int ParentDeepLevel
    {
        get { return m_ParentDeepLevel; }
        set
        {
            m_ParentDeepLevel = value;
            leadIndentControl.Indent = ParentDeepLevel + 1;

            foreach( Tools_DepartmentReports_DepartmentSubControl departmentControl in this.childDepartmetControls )
            {
                departmentControl.ParentDeepLevel = this.ParentDeepLevel + 1;
            }
        }
    }

    public StudentsReportsData Data
    {
        get { return m_Data; }
    }

    public string RegionName
    {
        get { return m_RegionName; }
    }

    public void Bind(
        StudentsReportsData data,
        string regionName)
    {
        this.m_Data = data;
        this.m_RegionName = regionName;

        BuildChildren();
        UpdateData();
    }

    void UpdateData()
    {
        if( this.Data == null )
            return;

        List<DateTime> completionDateCollect = new List<DateTime>();
        m_TryCount = 0;

        m_QuestionCount = 0;
        m_TotalRequiredPoints = 0;
        m_CollectedPoints = 0;
        m_TotalAnswerCount = 0;
        m_RightAnswerCount = 0;

        foreach( Tools_DepartmentReports_DepartmentSubControl departmentControl in childDepartmetControls )
        {
            completionDateCollect.AddRange(departmentControl.CompletionDates);
            m_TryCount += departmentControl.TryCount;
            m_QuestionCount += departmentControl.QuestionCount;
            m_TotalRequiredPoints += departmentControl.TotalRequiredPoints;
            m_CollectedPoints += departmentControl.CollectedPoints;
            m_TotalAnswerCount += departmentControl.TotalAnswerCount;
            m_RightAnswerCount += departmentControl.RightAnswerCount;
        }

        m_CompletionDates = completionDateCollect.AsReadOnly();

        if( completionDateCollect.Count == 0 )
            dateLabel.Text = "";
        else if( completionDateCollect.Count == 1 )
            dateLabel.Text = completionDateCollect[0].ToShortDateString();
        else if( completionDateCollect.Count == 2 )
            dateLabel.Text = completionDateCollect[0].ToShortDateString() + ", " + completionDateCollect[1].ToShortDateString();
        else
            dateLabel.Text = completionDateCollect[0].ToShortDateString() + " — " + completionDateCollect[completionDateCollect.Count - 1].ToShortDateString();

        tryCountLabel.Text = m_TryCount.ToString();
        questionCountLabel.Text = m_QuestionCount.ToString();

        averageRequiredPointsLabel.Text =
            m_QuestionCount == 0 ? m_TotalRequiredPoints.ToString() : (m_TotalRequiredPoints / m_QuestionCount).ToString("0.0");

        averagePointsLabel.Text =
            m_QuestionCount == 0 ? m_CollectedPoints.ToString() : (m_CollectedPoints / m_QuestionCount).ToString("0.0");

        averageRightAnswerPercentLabel.Text = AnswerPercent.ToString("0.0") + "%";
    }

    void BuildChildren()
    {
        if( m_Data == null )
            return;

        regionNameLabel.Text = this.RegionName;

        childDepartmentsPlaceHolder.Controls.Clear();

        Dictionary<string, string> departmentNameList = new Dictionary<string, string>();
        foreach( StudentsReportsData.StudentsRow studentRow in Data.Students )
        {
            string region;
            if( studentRow.IsRegionNameNull() )
                region = "";
            else
                region = studentRow.RegionName;

            if( region != this.RegionName )
                continue;

            string department;
            if( studentRow.IsCommentsNull() )
                department = "";
            else
                department = studentRow.Comments;

            departmentNameList[department] = "";
        }

        childDepartmetControls = new List<Tools_DepartmentReports_DepartmentSubControl>(departmentNameList.Count);
        foreach( string departmentName in departmentNameList.Keys )
        {
            Tools_DepartmentReports_DepartmentSubControl departmentControl = (Tools_DepartmentReports_DepartmentSubControl)LoadControl("DepartmentSubControl.ascx");

            departmentControl.Bind(this.Data, this.RegionName, departmentName);

            if( !departmentControl.GetVisibleByFilter() )
                continue;

            childDepartmetControls.Add(departmentControl);

            departmentControl.ParentDeepLevel = 0;
        }

        childDepartmetControls.Sort();

        foreach( Tools_DepartmentReports_DepartmentSubControl departmentControl in childDepartmetControls )
        {
            childDepartmentsPlaceHolder.Controls.Add(departmentControl);
        }
    }

    public int CompareTo(Tools_DepartmentReports_RegionSubControl other)
    {
        switch( StudentsReportsDataBuilder.GetFilters(Data).SortColumn )
        {
            case StudentsReportsDataBuilder.SortColumn.AnswerPercent:
                return this.AnswerPercent.CompareTo(other.AnswerPercent);

            case StudentsReportsDataBuilder.SortColumn.CollectedPoints:
                return this.CollectedPoints.CompareTo(other.CollectedPoints);

            case StudentsReportsDataBuilder.SortColumn.QuestionCount:
                return this.QuestionCount.CompareTo(other.QuestionCount);

            case StudentsReportsDataBuilder.SortColumn.RequiredPoints:
                return this.TotalRequiredPoints.CompareTo(other.TotalRequiredPoints);

            case StudentsReportsDataBuilder.SortColumn.TryCount:
                return this.TryCount.CompareTo(other.TryCount);

            default:
                return string.Compare(
                    this.RegionName,
                    other.RegionName,
                    StringComparison.OrdinalIgnoreCase);
        }
    }

    protected void expandCollapse_Click(object sender, EventArgs e)
    {
        childDepartmentsPlaceHolder.Visible = !expandCollapse.Collapsed;
    }

    public bool GetVisibleByFilter()
    {
        if( StudentsReportsDataBuilder.GetFilters(Data).Region!=null )
        {
            if( RegionName.IndexOf(
                StudentsReportsDataBuilder.GetFilters(Data).Region,
                StringComparison.OrdinalIgnoreCase) < 0 )
                return false;
        }

        if( childDepartmetControls.Count > 0 )
            return true;
        else
            return false;
    }
}
