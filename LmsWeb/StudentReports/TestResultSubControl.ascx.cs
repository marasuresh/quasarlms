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

public partial class StudentReports_TestResultSubControl : System.Web.UI.UserControl, IComparable<StudentReports_TestResultSubControl>
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( !IsPostBack )
        {
            expandCollapse.Collapsed = true;
            answersPlaceHolder.Visible = !expandCollapse.Collapsed;
        }
    }


    int m_ParentDeepLevel = 0;

    StudentsReportsData.TestResultsRow m_TestResultsRow;

    List<StudentReports_TestAnswerSubControl> childAnswerControls;



    DateTime? m_CompletionDate;
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

    public DateTime? CompletionDate
    {
        get { return m_CompletionDate; }
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
            leadIndentControl.Indent = ParentDeepLevel;
        }
    }

    public StudentsReportsData.TestResultsRow TestResultsRow
    {
        get { return m_TestResultsRow; }
        set
        {
            m_TestResultsRow = value;
            BuildChildren();
            UpdateTestResult();
        }
    }

    void UpdateTestResult()
    {
        if( TestResultsRow.IsCompletionDateNull() )
            m_CompletionDate = null;
        else
            m_CompletionDate = TestResultsRow.CompletionDate;

        m_TryCount = TestResultsRow.TryCount;

        m_QuestionCount = TestResultsRow.GetTestAnswersRows().Length;
        m_TotalAnswerCount = TestResultsRow.GetTestAnswersRows().Length;

        m_TotalRequiredPoints = 0;
        m_TotalPoints = 0;
        m_RightAnswerCount = 0;

        foreach( StudentReports_TestAnswerSubControl answerControl in this.childAnswerControls )
        {
            m_TotalRequiredPoints += answerControl.RequiredPoints;
            m_TotalPoints += answerControl.Points;
            if( answerControl.IsRightAnswer )
                m_RightAnswerCount++;
        }

        m_TotalPoints = TestResultsRow.Points;


        if( CompletionDate == null )
            dateLabel.Text = "";
        else
            dateLabel.Text = CompletionDate.Value.ToShortDateString();

        tryCountLabel.Text = m_TryCount.ToString();
        questionCountLabel.Text = m_QuestionCount.ToString();

        averageRequiredPointsLabel.Text =
            m_QuestionCount == 0 ? m_TotalRequiredPoints.ToString() : (m_TotalRequiredPoints / m_QuestionCount).ToString("0.0");

        averagePointsLabel.Text =
            m_QuestionCount == 0 ? m_TotalPoints.ToString() : (m_TotalPoints / m_QuestionCount).ToString("0.0");

        averageRightAnswerPercentLabel.Text =
            (m_TotalAnswerCount == 0 ? (m_RightAnswerCount * 100).ToString() : (m_RightAnswerCount * 100 / m_TotalAnswerCount).ToString())
        + "%";

        studentLabel.Text = TestResultsRow.StudentsRow.FullName + " (" + TestResultsRow.GetTestAnswersRows().Length + " відповідей)";

        if( TestResultsRow.StudentID != CurrentUser.UserID )
        {
            moreStudentElement.InnerHtml =
                "<br>"+
                Server.HtmlEncode(TestResultsRow.StudentsRow.RegionName) + "<br>\r\n" +
                Server.HtmlEncode(TestResultsRow.StudentsRow.JobPosition) + "<br>\r\n" +
                Server.HtmlEncode(TestResultsRow.StudentsRow.Comments);
        }
    }

    void BuildChildren()
    {
        List<StudentsReportsData.TestAnswersRow> answers = new List<StudentsReportsData.TestAnswersRow>();
        foreach( StudentsReportsData.TestAnswersRow answerRow in TestResultsRow.GetTestAnswersRows() )
        {
            answers.Add(answerRow);
        }

        // ВОПРОСЫ всегда сортируются в своём порядке,
        // так как они идут в тесте
        answers.Sort(
            delegate(StudentsReportsData.TestAnswersRow answerRow1, StudentsReportsData.TestAnswersRow anserRow2)
            {
                return answerRow1.TestQuestionsRow.OrderIndex.CompareTo(anserRow2.TestQuestionsRow.OrderIndex);
            });

        childAnswerControls = new List<StudentReports_TestAnswerSubControl>(answers.Count);
        foreach( StudentsReportsData.TestAnswersRow answerRow in answers )
        {
            StudentReports_TestAnswerSubControl answerControl = (StudentReports_TestAnswerSubControl)LoadControl("TestAnswerSubControl.ascx");
            answerControl.TestAnswersRow = answerRow;
            childAnswerControls.Add(answerControl);

            answerControl.ParentDeepLevel = this.ParentDeepLevel + 1;

            answersPlaceHolder.Controls.Add(answerControl);
        }
    }

    protected void expandCollapse_Click(object sender, EventArgs e)
    {
        answersPlaceHolder.Visible = !expandCollapse.Collapsed;
    }

    public int CompareTo(StudentReports_TestResultSubControl other)
    {
        switch( StudentsReportsDataBuilder.GetFilters(TestResultsRow).SortColumn )
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

            case StudentsReportsDataBuilder.SortColumn.Name:
                {
                    int compareStudents = string.Compare(
                        this.TestResultsRow.StudentsRow.FullName,
                        other.TestResultsRow.StudentsRow.FullName,
                        StringComparison.OrdinalIgnoreCase);

                    return compareStudents;
                }

            default:
                if( CompletionDate == null )
                {
                    return (CompletionDate != null).CompareTo(other.CompletionDate != null);
                }
                else
                {
                    return
                        this.CompletionDate.Value.CompareTo(other.CompletionDate.Value);
                }
        }
    }

    public bool GetVisibleByFilter()
    {
        if( StudentsReportsDataBuilder.GetFilters(TestResultsRow).StudentName != null )
        {
            if( (TestResultsRow.StudentsRow.FullName+"").IndexOf(
                StudentsReportsDataBuilder.GetFilters(TestResultsRow).StudentName,
                StringComparison.OrdinalIgnoreCase) < 0 )
                return false;
        }

        if( StudentsReportsDataBuilder.GetFilters(TestResultsRow).Comment != null )
        {
            if( (TestResultsRow.StudentsRow.Comments+"").IndexOf(
                StudentsReportsDataBuilder.GetFilters(TestResultsRow).Comment,
                StringComparison.OrdinalIgnoreCase) < 0 )
                return false;
        }

        if( StudentsReportsDataBuilder.GetFilters(TestResultsRow).Region != null )
        {
            if( (TestResultsRow.StudentsRow.RegionName+"").IndexOf(
                StudentsReportsDataBuilder.GetFilters(TestResultsRow).Region,
                StringComparison.OrdinalIgnoreCase) < 0 )
                return false;
        }

        if( StudentsReportsDataBuilder.GetFilters(TestResultsRow).GroupName != null )
        {
            bool foundFilterGroup = false;
            foreach( StudentsReportsData.StudentGroupRow sg in TestResultsRow.StudentsRow.GetStudentGroupRows() )
            {
                if( (sg.GroupsRow.Name+"").IndexOf(
                    StudentsReportsDataBuilder.GetFilters(TestResultsRow).GroupName,
                    StringComparison.OrdinalIgnoreCase) < 0 )
                {
                    foundFilterGroup = true;
                    break;
                }
            }
            if( !foundFilterGroup )
                return false;
        }

        if( !StudentsReportsDataBuilder.GetFilters(TestResultsRow).IsStartDateNull() )
        {
            if( this.CompletionDate!=null
                && this.CompletionDate.Value.Date<StudentsReportsDataBuilder.GetFilters(TestResultsRow).StartDate.Date )
                return false;
        }

        if( !StudentsReportsDataBuilder.GetFilters(TestResultsRow).IsEndDateNull() )
        {
            if( this.CompletionDate==null
                || this.CompletionDate.Value.Date>StudentsReportsDataBuilder.GetFilters(TestResultsRow).EndDate.Date )
                return false;
        }

        string testFilter = StudentsReportsDataBuilder.GetFilters(TestResultsRow).TestText;
        if( testFilter != null )
        {
            bool found = false;

            foreach( StudentsReportsData.TestAnswersRow answerRow in TestResultsRow.GetTestAnswersRows() )
            {
                if( answerRow.TestQuestionsRow.ContentText.IndexOf(
                    testFilter,
                    StringComparison.OrdinalIgnoreCase)>=0 )
                {
                    found = true;
                    break;
                }

                if( answerRow.AnswerText.IndexOf(
                    testFilter,
                    StringComparison.OrdinalIgnoreCase) >= 0 )
                {
                    found = true;
                    break;
                }
            }

            if( !found )
                return false;
        }



        if( childAnswerControls.Count > 0 )
            return true;
        else
            return false;
    }
}