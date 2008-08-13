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

public partial class StudentReports_CourseSubControl : System.Web.UI.UserControl, IComparable<StudentReports_CourseSubControl>
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    int m_ParentDeepLevel = 0;

    StudentsReportsData.CoursesRow m_CoursesRow;

    List<StudentReports_TestSubControl> childTestControls;
    List<StudentReports_ThemeSubControl> childThemeControls;


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

            foreach( StudentReports_ThemeSubControl themeControl in this.childThemeControls )
            {
                themeControl.ParentDeepLevel = this.ParentDeepLevel + 1;
            }
            foreach( StudentReports_TestSubControl testControl in this.childTestControls )
            {
                testControl.ParentDeepLevel = this.ParentDeepLevel + 1;
            }
        }
    }

    public StudentsReportsData.CoursesRow CourseRow
    {
        get { return m_CoursesRow; }
        set
        {
            m_CoursesRow = value;
            BuildChildern();
            UpdateData();
        }
    }

    void UpdateData()
    {
        List<DateTime> completionDateCollect = new List<DateTime>();
        m_TryCount = 0;

        m_QuestionCount = 0;
        m_TotalRequiredPoints = 0;
        m_CollectedPoints = 0;
        m_TotalAnswerCount = 0;
        m_RightAnswerCount = 0;

        foreach( StudentReports_ThemeSubControl themeControl in childThemeControls )
        {                                                                                               
            completionDateCollect.AddRange(themeControl.CompletionDates);
            m_TryCount += themeControl.TryCount;
            m_QuestionCount += themeControl.QuestionCount;
            m_TotalRequiredPoints += themeControl.TotalRequiredPoints;
            m_CollectedPoints += themeControl.TotalPoints;
            m_TotalAnswerCount += themeControl.TotalAnswerCount;
            m_RightAnswerCount += themeControl.RightAnswerCount;
        }

        foreach( StudentReports_TestSubControl testControl in childTestControls )
        {
            completionDateCollect.AddRange(testControl.CompletionDates);
            m_TryCount += testControl.TryCount;
            m_QuestionCount += testControl.QuestionCount;
            m_TotalRequiredPoints += testControl.TotalRequiredPoints;
            m_CollectedPoints += testControl.TotalPoints;
            m_TotalAnswerCount += testControl.TotalAnswerCount;
            m_RightAnswerCount += testControl.RightAnswerCount;
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

        averageRightAnswerPercentLabel.Text = AnswerPercent.ToString("0.0")+ "%";
    }

    private void BuildChildern()
    {
        courseNameLabel.Text = CourseRow.Name;

        BuildTests();
        BuildThemes();
    }

    void BuildTests()
    {
        List<StudentsReportsData.TestsRow> tests = new List<StudentsReportsData.TestsRow>();
        foreach( StudentsReportsData.TestsRow testRow in CourseRow.GetTestsRows() )
        {
            tests.Add(testRow);
        }

        tests.Sort(
            delegate(StudentsReportsData.TestsRow testRow1, StudentsReportsData.TestsRow testRow2)
            {
                return testRow1.Points.CompareTo(testRow2.Points);
            });

        childTestControls = new List<StudentReports_TestSubControl>(tests.Count);
        foreach( StudentsReportsData.TestsRow testRow in tests )
        {
            StudentReports_TestSubControl testControl = (StudentReports_TestSubControl)LoadControl("TestSubControl.ascx");
            testControl.TestsRow = testRow;
            if( !testControl.GetVisibleByFilter() )
                continue;

            childTestControls.Add(testControl);

            testControl.ParentDeepLevel = this.ParentDeepLevel + 1;
        }

        foreach( StudentReports_TestSubControl testControl in childTestControls )
        {
            childTestsPlaceHolder.Controls.Add(testControl);
        }
    }


    private void BuildThemes()
    {
        List<StudentsReportsData.ThemesRow> themes = new List<StudentsReportsData.ThemesRow>();
        foreach( StudentsReportsData.ThemesRow themeRow in CourseRow.GetThemesRows() )
        {
            themes.Add(themeRow);
        }

        childThemeControls = new List<StudentReports_ThemeSubControl>(themes.Count);
        foreach( StudentsReportsData.ThemesRow themeRow in themes )
        {
            StudentReports_ThemeSubControl themeControl = (StudentReports_ThemeSubControl)LoadControl("ThemeSubControl.ascs");
            themeControl.ThemeRow = themeRow;
            if( !themeControl.GetVisibleByFilter() )
                continue;

            childThemeControls.Add(themeControl);

            themeControl.ParentDeepLevel = this.ParentDeepLevel + 1;
        }

        childThemeControls.Sort();

        foreach( StudentReports_ThemeSubControl themeControl in childThemeControls )
        {
            childThemesPlaceHolder.Controls.Add(themeControl);
        }
    }

    protected void expandCollapse_Click(object sender, EventArgs e)
    {
        childThemesPlaceHolder.Visible = !expandCollapse.Collapsed;
        childTestsPlaceHolder.Visible = !expandCollapse.Collapsed;
    }

    public bool GetVisibleByFilter()
    {
        if( StudentsReportsDataBuilder.GetFilters(CourseRow).Course != null )
        {
            if( CourseRow.Name.IndexOf(
                StudentsReportsDataBuilder.GetFilters(CourseRow).Course,
                StringComparison.OrdinalIgnoreCase) < 0 )
                return false;
        }

        if( childThemeControls.Count > 0 || childTestControls.Count > 0 )
            return true;
        else
            return false;
    }

    public int CompareTo(StudentReports_CourseSubControl other)
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
