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

public partial class StudentReports_ThemeSubControl : System.Web.UI.UserControl, IComparable<StudentReports_ThemeSubControl>
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    int m_ParentDeepLevel = 0;

    StudentsReportsData.ThemesRow m_ThemeRow;

    List<StudentReports_TestSubControl> childTestControls;
    List<StudentReports_ThemeSubControl> childThemeControls;

    IList<DateTime> m_CompletionDates;
    int m_TryCount;

    int m_QuestionCount;
    int m_TotalRequiredPoints;
    int m_TotalPoints;
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

    public int TotalPoints
    {
        get { return m_TotalPoints; }
    }

    public int TotalAnswerCount
    {
        get { return m_TotalAnswerCount; }
    }

    public int RightAnswerCount
    {
        get { return m_RightAnswerCount; }
    }

    public IList<DateTime> CompletionDates
    {
        get { return m_CompletionDates; }
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

    public int ParentDeepLevel
    {
        get { return m_ParentDeepLevel; }
        set
        {
            m_ParentDeepLevel = value;
            foreach( StudentReports_TestSubControl testControl in this.childTestControls )
            {
                testControl.ParentDeepLevel = this.ParentDeepLevel + 1;
            }
            foreach( StudentReports_ThemeSubControl themeControl in this.childThemeControls )
            {
                themeControl.ParentDeepLevel = this.ParentDeepLevel + 1;
            }
        }
    }

    
    public StudentsReportsData.ThemesRow ThemeRow
    {
        get { return m_ThemeRow; }
        set
        {
            m_ThemeRow = value;
            BuildChildren();
            UpdateTheme();
        }
    }

    void UpdateTheme()
    {
        List<DateTime> completionDateCollect = new List<DateTime>();
        m_TryCount = 0;

        m_QuestionCount = 0;
        m_TotalRequiredPoints = 0;
        m_TotalPoints = 0;
        m_TotalAnswerCount = 0;
        m_RightAnswerCount = 0;

        foreach( StudentReports_ThemeSubControl themeControl in childThemeControls )
        {
            completionDateCollect.AddRange(themeControl.CompletionDates);
            m_TryCount += themeControl.TryCount;
            m_QuestionCount += themeControl.QuestionCount;
            m_TotalRequiredPoints += themeControl.TotalRequiredPoints;
            m_TotalPoints += themeControl.TotalPoints;
            m_TotalAnswerCount += themeControl.TotalAnswerCount;
            m_RightAnswerCount += themeControl.RightAnswerCount;
        }

        foreach( StudentReports_TestSubControl testControl in childTestControls )
        {
            completionDateCollect.AddRange(testControl.CompletionDates);
            m_TryCount += testControl.TryCount;
            m_QuestionCount += testControl.QuestionCount;
            m_TotalRequiredPoints += testControl.TotalRequiredPoints;
            m_TotalPoints += testControl.TotalPoints;
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
            m_QuestionCount == 0 ? m_TotalPoints.ToString() : (m_TotalPoints / m_QuestionCount).ToString("0.0");

        averageRightAnswerPercentLabel.Text =
            (m_TotalAnswerCount == 0 ? (m_RightAnswerCount * 100).ToString() : (m_RightAnswerCount * 100 / m_TotalAnswerCount).ToString())
        + "%";

    }

    void BuildChildren()
    {
        themeNameLabel.Text = ThemeRow.Name;
        
        int resultCount = 0;
        foreach( StudentsReportsData.TestsRow t in ThemeRow.GetTestsRows() )
        {
            resultCount += t.GetTestResultsRows().Length;
        }

        themeNameLabel.Text += " (" + ThemeRow.GetThemesRows().Length +" тем, "+ resultCount + " тестів)";


        BuildTests();
        BuildThemes();
    }

    void BuildTests()
    {
        List<StudentsReportsData.TestsRow> tests = new List<StudentsReportsData.TestsRow>();
        foreach( StudentsReportsData.TestsRow testRow in ThemeRow.GetTestsRows() )
        {
            tests.Add(testRow);
        }

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

    void BuildThemes()
    {
        List<StudentsReportsData.ThemesRow> themes = new List<StudentsReportsData.ThemesRow>();
        foreach( StudentsReportsData.ThemesRow themeRow in ThemeRow.GetThemesRows() )
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

    public int CompareTo(StudentReports_ThemeSubControl other)
    {
        switch( StudentsReportsDataBuilder.GetFilters(ThemeRow).SortColumn )
        {
            case StudentsReportsDataBuilder.SortColumn.AnswerPercent:
                return this.AnswerPercent.CompareTo(other.AnswerPercent);

            case StudentsReportsDataBuilder.SortColumn.CollectedPoints:
                return this.TotalPoints.CompareTo(other.TotalPoints);

            case StudentsReportsDataBuilder.SortColumn.QuestionCount:
                return this.QuestionCount.CompareTo(other.QuestionCount);

            case StudentsReportsDataBuilder.SortColumn.RequiredPoints:
                return this.TotalRequiredPoints.CompareTo(other.TotalRequiredPoints);

            case StudentsReportsDataBuilder.SortColumn.TryCount:
                return this.TryCount.CompareTo(other.TryCount);

            default:
                return string.Compare(
                    this.ThemeRow.Name,
                    other.ThemeRow.Name,
                    StringComparison.OrdinalIgnoreCase);
        }
    }

    public bool GetVisibleByFilter()
    {
        string filterTheme = StudentsReportsDataBuilder.GetFilters(ThemeRow).Theme;
        if( filterTheme != null )
        {
            if( !IsThemeVisibleByName(ThemeRow,filterTheme)
                && !IsThemeVisibleByParentThemeName(ThemeRow,filterTheme)
                && !IsThemeVisibleByChildThemeName(ThemeRow,filterTheme) )
                return false;
        }

        if( childThemeControls.Count > 0 || childTestControls.Count > 0 )
            return true;
        else
            return false;
    }

    static bool IsThemeVisibleByName(StudentsReportsData.ThemesRow themeRow, string name)
    {
        return themeRow.Name.IndexOf(
                name,
                StringComparison.OrdinalIgnoreCase) >= 0;
    }

    static bool IsThemeVisibleByParentThemeName(StudentsReportsData.ThemesRow themeRow, string name)
    {
        StudentsReportsData.ThemesRow parentThemeRow = themeRow.ThemesRowParent;
        if( parentThemeRow == null )
            return false;
        else
            return IsThemeVisibleByName(parentThemeRow, name) || IsThemeVisibleByParentThemeName(parentThemeRow, name);
    }

    static bool IsThemeVisibleByChildThemeName(StudentsReportsData.ThemesRow themeRow, string name)
    {
        foreach( StudentsReportsData.ThemesRow childThemeRow in themeRow.GetThemesRows() )
        {
            if( IsThemeVisibleByName(childThemeRow, name) )
                return true;
            if( IsThemeVisibleByChildThemeName(childThemeRow, name) )
                return true;
        }

        return false;
    }
}