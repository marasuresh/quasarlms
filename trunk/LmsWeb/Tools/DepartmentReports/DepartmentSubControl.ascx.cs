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

public partial class Tools_DepartmentReports_DepartmentSubControl : System.Web.UI.UserControl,
    IComparable<Tools_DepartmentReports_DepartmentSubControl>
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    StudentsReportsData m_Data;
    string m_DepartmentName;
    string m_RegionName;
    int m_ParentDeepLevel;

    List<Tools_DepartmentReports_StudentSubControl> childStudentControls = new List<Tools_DepartmentReports_StudentSubControl>();

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

            foreach( Tools_DepartmentReports_StudentSubControl studentControl in this.childStudentControls )
            {
                studentControl.ParentDeepLevel = this.ParentDeepLevel + 1;
            }
        }
    }

    public StudentsReportsData Data
    {
        get { return m_Data; }
    }

    public string DepartmentName
    {
        get { return m_DepartmentName; }
    }

    public string RegionName
    {
        get { return m_RegionName; }
    }

    public void Bind(
        StudentsReportsData data,
        string regionName,
        string departmentName)
    {
        this.m_Data = data;
        this.m_RegionName = regionName;
        this.m_DepartmentName = departmentName;

        BuildChildren();
        UpdateData();
    }

    protected void expandCollapse_Click(object sender, EventArgs e)
    {
        studentsPlaceHolder.Visible = !expandCollapse.Collapsed;
    }

    public bool GetVisibleByFilter()
    {
        //if( StudentsReportsDataBuilder.GetFilters(Data). != null )
        //{
        //    if( RegionName.Name.IndexOf(
        //        StudentsReportsDataBuilder.GetFilters(Data).Region,
        //        StringComparison.OrdinalIgnoreCase) < 0 )
        //        return false;
        //}

        if( childStudentControls.Count > 0 )
            return true;
        else
            return false;
    }

    void BuildChildren()
    {
        if( this.Data == null )
            return;

        studentsPlaceHolder.Controls.Clear();

        List<StudentsReportsData.StudentsRow> students = new List<StudentsReportsData.StudentsRow>();
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

            if( department != this.DepartmentName )
                continue;

            students.Add(studentRow);
        }

        childStudentControls = new List<Tools_DepartmentReports_StudentSubControl>(students.Count);
        foreach( StudentsReportsData.StudentsRow studentRow in students )
        {
            Tools_DepartmentReports_StudentSubControl studentControl = (Tools_DepartmentReports_StudentSubControl)LoadControl("StudentSubControl.ascx");
            studentControl.StudentRow = studentRow;
            if( !studentControl.GetVisibleByFilter() )
                continue;

            childStudentControls.Add(studentControl);

            studentControl.ParentDeepLevel = 0;
        }

        childStudentControls.Sort();

        foreach( Tools_DepartmentReports_StudentSubControl studentControl in childStudentControls )
        {
            studentsPlaceHolder.Controls.Add(studentControl);
        }
    }

    void UpdateData()
    {
        if( this.Data == null )
            return;

        departmentNameLabel.Text = this.DepartmentName;

        List<DateTime> completionDateCollect = new List<DateTime>();
        m_TryCount = 0;

        m_QuestionCount = 0;
        m_TotalRequiredPoints = 0;
        m_CollectedPoints = 0;
        m_TotalAnswerCount = 0;
        m_RightAnswerCount = 0;

        foreach( Tools_DepartmentReports_StudentSubControl studentControl in childStudentControls )
        {
            completionDateCollect.AddRange(studentControl.CompletionDates);
            m_TryCount += studentControl.TryCount;
            m_QuestionCount += studentControl.QuestionCount;
            m_TotalRequiredPoints += studentControl.TotalRequiredPoints;
            m_CollectedPoints += studentControl.CollectedPoints;
            m_TotalAnswerCount += studentControl.TotalAnswerCount;
            m_RightAnswerCount += studentControl.RightAnswerCount;
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

    public int CompareTo(Tools_DepartmentReports_DepartmentSubControl other)
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
                    this.DepartmentName,
                    other.DepartmentName,
                    StringComparison.OrdinalIgnoreCase);
        }
    }
}
