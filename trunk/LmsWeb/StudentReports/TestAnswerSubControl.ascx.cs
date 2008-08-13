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

public partial class StudentReports_TestAnswerSubControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    int m_ParentDeepLevel = 0;

    StudentsReportsData.TestAnswersRow m_TestAnswersRow;



    int m_RequiredPoints;
    int m_Points;
    bool m_IsRightAnswer;

    public int RequiredPoints
    {
        get { return m_RequiredPoints; }
    }

    public int Points
    {
        get { return m_Points; }
    }

    public bool IsRightAnswer
    {
        get { return m_IsRightAnswer; }
    }


    public int ParentDeepLevel
    {
        get { return m_ParentDeepLevel; }
        set
        {
            m_ParentDeepLevel = value;
            leadIndentControl.Indent = ParentDeepLevel + 1;
        }
    }

    public StudentsReportsData.TestAnswersRow TestAnswersRow
    {
        get { return m_TestAnswersRow; }
        set
        {
            m_TestAnswersRow = value;
            UpdateTestAnswer();
        }
    }

    private void UpdateTestAnswer()
    {
        m_RequiredPoints = TestAnswersRow.TestQuestionsRow.Points;
        m_Points = TestAnswersRow.Points;
        m_IsRightAnswer = (TestAnswersRow.Points>0);

        answerLabel.Text = TestAnswersRow.TestQuestionsRow.ContentText;

        dateLabel.Text = TestAnswersRow.AnswerTimeSeconds + " сек";
        averagePointsLabel.Text = this.Points.ToString();
        averageRequiredPointsLabel.Text = this.RequiredPoints.ToString();
        if( this.IsRightAnswer )
            averageRightAnswerPercentLabel.Text = "*";
    }
}
