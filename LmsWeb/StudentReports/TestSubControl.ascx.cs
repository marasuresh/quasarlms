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

public partial class StudentReports_TestSubControl : System.Web.UI.UserControl, IComparable<StudentReports_TestSubControl>
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( !Page.IsPostBack )
        {

        }
    }

    int m_ParentDeepLevel = 0;

    StudentsReportsData.TestsRow m_TestsRow;
    List<StudentReports_TestResultSubControl> childResultControls;


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

    public DateTime? MaxCompletionDate
    {
        get
        {
            if( CompletionDates.Count==0 )
                return null;

            DateTime result = DateTime.MinValue;
            foreach( DateTime dt in CompletionDates )
            {
                result =
                    dt > result ?
                    dt :
                    result;
            }

            return result;
        }
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
            foreach( StudentReports_TestResultSubControl resultControl in childResultControls )
            {
                resultControl.ParentDeepLevel = this.ParentDeepLevel + 1;
            }
        }
    }

    public StudentsReportsData.TestsRow TestsRow
    {
        get { return m_TestsRow; }
        set
        {
            m_TestsRow = value;
            BuildChildren();
            UpdateData();
        }
    }

    void UpdateData()
    {
        List<DateTime> completionDateCollect = new List<DateTime>();
        m_TryCount = 0;

        m_QuestionCount = 0;
        m_TotalRequiredPoints = 0;
        m_TotalPoints = 0;
        m_TotalAnswerCount = 0;
        m_RightAnswerCount = 0;

        foreach( StudentReports_TestResultSubControl resultControl in childResultControls )
        {
            if( resultControl.CompletionDate != null )
                completionDateCollect.Add(resultControl.CompletionDate.Value);

            m_TryCount += resultControl.TryCount;
            m_QuestionCount += resultControl.QuestionCount;
            m_TotalRequiredPoints += resultControl.TotalRequiredPoints;
            m_TotalPoints += resultControl.TotalPoints;
            m_TotalAnswerCount += resultControl.TotalAnswerCount;
            m_RightAnswerCount += resultControl.RightAnswerCount;
        }

        m_CompletionDates = completionDateCollect.AsReadOnly();

    }

    void BuildChildren()
    {
        List<StudentsReportsData.TestResultsRow> results = new List<StudentsReportsData.TestResultsRow>();
        foreach( StudentsReportsData.TestResultsRow resultRow in TestsRow.GetTestResultsRows() )
        {
            results.Add(resultRow);
        }

        childResultControls = new List<StudentReports_TestResultSubControl>(results.Count);
        foreach( StudentsReportsData.TestResultsRow resultRow in results )
        {
            StudentReports_TestResultSubControl resultControl = (StudentReports_TestResultSubControl)LoadControl("TestResultSubControl.ascx");
            resultControl.TestResultsRow = resultRow;
            if( !resultControl.GetVisibleByFilter() )
                continue;

            childResultControls.Add(resultControl);

            resultControl.ParentDeepLevel = this.ParentDeepLevel + 1;
        }

        childResultControls.Sort();

        foreach( StudentReports_TestResultSubControl resultControl in childResultControls )
        {
            this.Controls.Add(resultControl);
        }
    }

    public int CompareTo(StudentReports_TestSubControl other)
    {
        switch( StudentsReportsDataBuilder.GetFilters(TestsRow).SortColumn )
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
                if( MaxCompletionDate == null )
                {
                    return (MaxCompletionDate != null).CompareTo(other.MaxCompletionDate != null);
                }
                else
                {
                    return
                        this.MaxCompletionDate.Value.CompareTo(other.MaxCompletionDate.Value);
                }
        }
    }

    public bool GetVisibleByFilter()
    {
        if( childResultControls.Count > 0 )
            return true;
        else
            return false;
    }
}