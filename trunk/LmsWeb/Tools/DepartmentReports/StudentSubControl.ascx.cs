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

public partial class Tools_DepartmentReports_StudentSubControl : System.Web.UI.UserControl,
    IComparable<Tools_DepartmentReports_StudentSubControl>
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    int m_ParentDeepLevel;

    StudentsReportsData.StudentsRow m_StudentRow;

    List<Tools_DepartmentReports_CourseSubControl> childCourseControls = new List<Tools_DepartmentReports_CourseSubControl>();


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

            foreach( Tools_DepartmentReports_CourseSubControl courseControl in this.childCourseControls )
            {
                courseControl.ParentDeepLevel = this.ParentDeepLevel + 1;
            }
        }
    }


    public StudentsReportsData.StudentsRow StudentRow
    {
        get { return m_StudentRow; }
        set
        {
            m_StudentRow = value;
            BuildChildren();
            UpdateData();
        }
    }

    public bool GetVisibleByFilter()
    {
        if( StudentsReportsDataBuilder.GetFilters(StudentRow).Comment != null )
        {
            if( (StudentRow.Comments + "").IndexOf(
                StudentsReportsDataBuilder.GetFilters(StudentRow).Comment,
                StringComparison.OrdinalIgnoreCase) < 0 )
                return false;
        }

        if( StudentsReportsDataBuilder.GetFilters(StudentRow).StudentName != null )
        {
            if( (StudentRow.FullName + "").IndexOf(
                StudentsReportsDataBuilder.GetFilters(StudentRow).StudentName,
                StringComparison.OrdinalIgnoreCase) < 0 )
                return false;
        }

        string groupNameFilter = StudentsReportsDataBuilder.GetFilters(StudentRow).GroupName;
        if(  groupNameFilter != null )
        {
            bool foundFilterGroup = false;
            foreach( StudentsReportsData.StudentGroupRow sg in StudentRow.GetStudentGroupRows() )
            {
                if( (sg.GroupsRow.Name + "").IndexOf(
                    groupNameFilter,
                    StringComparison.OrdinalIgnoreCase) < 0 )
                {
                    foundFilterGroup = true;
                    break;
                }
            }
            if( !foundFilterGroup )
                return false;
        }

        if( childCourseControls.Count > 0 )
            return true;
        else
            return false;
    }

    void UpdateData()
    {
        this.studentNameLabel.Text = StudentRow.FullName;

        List<DateTime> completionDateCollect = new List<DateTime>();
        m_TryCount = 0;

        m_QuestionCount = 0;
        m_TotalRequiredPoints = 0;
        m_CollectedPoints = 0;
        m_TotalAnswerCount = 0;
        m_RightAnswerCount = 0;

        foreach( Tools_DepartmentReports_CourseSubControl courseControl in childCourseControls )
        {
            completionDateCollect.AddRange(courseControl.CompletionDates);
            m_TryCount += courseControl.TryCount;
            m_QuestionCount += courseControl.QuestionCount;
            m_TotalRequiredPoints += courseControl.TotalRequiredPoints;
            m_CollectedPoints += courseControl.CollectedPoints;
            m_TotalAnswerCount += courseControl.TotalAnswerCount;
            m_RightAnswerCount += courseControl.RightAnswerCount;
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
        coursesPlaceHolder.Controls.Clear();

        List<StudentsReportsData.CoursesRow> courses = new List<StudentsReportsData.CoursesRow>();
        foreach( StudentsReportsData.TestResultsRow testResultRow in StudentRow.GetTestResultsRows() )
        {
            courses.Add(testResultRow.TestsRow.CoursesRow);
        }

        childCourseControls.Clear();
        foreach( StudentsReportsData.CoursesRow courseRow in courses )
        {
            Tools_DepartmentReports_CourseSubControl courseControl = (Tools_DepartmentReports_CourseSubControl)LoadControl("CourseSubControl.ascx");
            courseControl.Bind(this.StudentRow, courseRow);
            if( !courseControl.GetVisibleByFilter() )
                continue;

            childCourseControls.Add(courseControl);

            courseControl.ParentDeepLevel = 0;
        }

        childCourseControls.Sort();

        foreach( Tools_DepartmentReports_CourseSubControl courseControl in childCourseControls )
        {
            coursesPlaceHolder.Controls.Add(courseControl);
        }
    }

    protected void expandCollapse_Click(object sender, EventArgs e)
    {
        coursesPlaceHolder.Visible = !expandCollapse.Collapsed;
    }

    public int CompareTo(Tools_DepartmentReports_StudentSubControl other)
    {
        switch( StudentsReportsDataBuilder.GetFilters(StudentRow).SortColumn )
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
                    this.StudentRow.FullName,
                    other.StudentRow.FullName,
                    StringComparison.OrdinalIgnoreCase);
        }
    }
}
