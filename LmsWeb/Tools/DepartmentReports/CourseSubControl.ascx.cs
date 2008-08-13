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

public partial class Tools_DepartmentReports_CourseSubControl : System.Web.UI.UserControl,
    IComparable<Tools_DepartmentReports_CourseSubControl>
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    int m_ParentDeepLevel;

    StudentsReportsData.CoursesRow m_CourseRow;
    StudentsReportsData.StudentsRow m_StudentRow;

    List<StudentReports_TestResultSubControl> childTestResultControls = new List<StudentReports_TestResultSubControl>();


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

            foreach( StudentReports_TestResultSubControl testResultControl in this.childTestResultControls )
            {
                testResultControl.ParentDeepLevel = this.ParentDeepLevel + 1;
            }
        }
    }


    public StudentsReportsData.CoursesRow CourseRow
    {
        get { return m_CourseRow; }
    }

    public StudentsReportsData.StudentsRow StudentRow
    {
        get { return m_StudentRow; }
    }

    public void Bind(
        StudentsReportsData.StudentsRow studentRow,
        StudentsReportsData.CoursesRow courseRow )
    {
        m_StudentRow = studentRow;
        m_CourseRow = courseRow;

        BuildChildren();
        UpdateData();
    }


    public bool GetVisibleByFilter()
    {
        if( StudentsReportsDataBuilder.GetFilters(CourseRow).Course != null )
        {
            if( (StudentRow.Comments + "").IndexOf(
                StudentsReportsDataBuilder.GetFilters(CourseRow).Course,
                StringComparison.OrdinalIgnoreCase) < 0 )
                return false;
        }

        string domainFilter = StudentsReportsDataBuilder.GetFilters(StudentRow).CourseDomain;
        if( domainFilter != null )
        {
            bool foundFilterGroup = false;
            for(
                StudentsReportsData.CourseDomainRow dom = CourseRow.CourseDomainRow;
                dom != null;
                dom = dom.CourseDomainRowParent )
            {
                if( (dom.Name + "").IndexOf(
                    domainFilter,
                    StringComparison.OrdinalIgnoreCase) < 0 )
                {
                    foundFilterGroup = true;
                    break;
                }
            }

            if( !foundFilterGroup )
                return false;
        }

        if( childTestResultControls.Count > 0 )
            return true;
        else
            return false;
    }

    protected void expandCollapse_Click(object sender, EventArgs e)
    {
        testResultsPlaceHolder.Visible = !expandCollapse.Collapsed;
    }

    void UpdateData()
    {
        this.courseNameLabel.Text = CourseRow.Name;

        List<DateTime> completionDateCollect = new List<DateTime>();
        m_TryCount = 0;

        m_QuestionCount = 0;
        m_TotalRequiredPoints = 0;
        m_CollectedPoints = 0;
        m_TotalAnswerCount = 0;
        m_RightAnswerCount = 0;

        foreach( StudentReports_TestResultSubControl testResultControl in childTestResultControls )
        {
            if( testResultControl.CompletionDate!=null )
                completionDateCollect.Add(testResultControl.CompletionDate.Value);
            m_TryCount += testResultControl.TryCount;
            m_QuestionCount += testResultControl.QuestionCount;
            m_TotalRequiredPoints += testResultControl.TotalRequiredPoints;
            m_CollectedPoints += testResultControl.TotalPoints;
            m_TotalAnswerCount += testResultControl.TotalAnswerCount;
            m_RightAnswerCount += testResultControl.RightAnswerCount;
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
        testResultsPlaceHolder.Controls.Clear();

        List<StudentsReportsData.TestResultsRow> testResults = new List<StudentsReportsData.TestResultsRow>();
        foreach( StudentsReportsData.TestResultsRow testResultRow in StudentRow.GetTestResultsRows() )
        {
            if( testResultRow.TestsRow.CourseID != this.CourseRow.ID )
                continue;

            testResults.Add(testResultRow);
        }

        childTestResultControls.Clear();
        foreach( StudentsReportsData.TestResultsRow testResultRow in testResults )
        {
            StudentReports_TestResultSubControl testResultControl = (StudentReports_TestResultSubControl)LoadControl("~/StudentReports/TestResultSubControl.ascx");
            testResultControl.TestResultsRow = testResultRow;
            if( !testResultControl.GetVisibleByFilter() )
                continue;

            childTestResultControls.Add(testResultControl);

            testResultControl.ParentDeepLevel = 0;
        }

        childTestResultControls.Sort();

        foreach( StudentReports_TestResultSubControl testResultControl in childTestResultControls )
        {
            testResultsPlaceHolder.Controls.Add(testResultControl);
        }
    }

    public int CompareTo(Tools_DepartmentReports_CourseSubControl other)
    {
        switch( StudentsReportsDataBuilder.GetFilters(CourseRow).SortColumn )
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
                    this.CourseRow.Name,
                    other.CourseRow.Name,
                    StringComparison.OrdinalIgnoreCase);
        }
    }
}
