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

public partial class StudentReports_CourseDomainSubControl : System.Web.UI.UserControl, IComparable<StudentReports_CourseDomainSubControl>
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    List<StudentReports_CourseDomainSubControl> childDomainControls;
    List<StudentReports_CourseSubControl> childCourseControls;

    int m_ParentDeepLevel = 0;

    bool anyChildVisilble = false;

    StudentsReportsData.CourseDomainRow m_CourseDomainRow;

    int m_TryCount;
    int m_QuestionCount;

    int m_TotalRequiredPoints;
    int m_TotalPoints;

    int m_TotalAnswerCount;
    int m_RightAnswerCount;

    public int ParentDeepLevel
    {
        get { return m_ParentDeepLevel; }
        set
        {
            m_ParentDeepLevel = value;
            leadIndentControl.Indent = ParentDeepLevel + 1;

            foreach( StudentReports_CourseDomainSubControl domainControl in this.childDomainControls )
            {
                domainControl.ParentDeepLevel = this.ParentDeepLevel + 1;
            }
            foreach( StudentReports_CourseSubControl courseControl in this.childCourseControls )
            {
                courseControl.ParentDeepLevel = this.ParentDeepLevel + 1;
            }
        }
    }

    public StudentsReportsData.CourseDomainRow CourseDomainRow
    {
        get { return m_CourseDomainRow; }
        set
        {
            m_CourseDomainRow = value;
            BuildChildren();
            UpdateFileds();
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

    public int CollectedPoints
    {
        get
        {
            return m_TotalPoints;
        }
    }

    public int QuestionCount
    {
        get
        {
            return m_QuestionCount;
        }
    }

    public int RequiredPoints
    {
        get
        {
            return m_TotalRequiredPoints;
        }
    }

    public int TryCount
    {
        get
        {
            return m_TryCount;
        }
    }

    void UpdateFileds()
    {
        List<DateTime> completionDates = new List<DateTime>();
        m_TryCount = 0;
        m_QuestionCount = 0;
        
        m_TotalRequiredPoints = 0;
        m_TotalPoints = 0;

        m_TotalAnswerCount = 0;
        m_RightAnswerCount = 0;

        foreach( StudentReports_CourseSubControl courseControl in childCourseControls )
        {
            completionDates.AddRange(courseControl.CompletionDates);
            m_TryCount += courseControl.TryCount;
            m_QuestionCount += courseControl.QuestionCount;
            m_TotalRequiredPoints += courseControl.TotalRequiredPoints;
            m_TotalPoints += courseControl.CollectedPoints;
            m_TotalAnswerCount += courseControl.TotalAnswerCount;
            m_RightAnswerCount += courseControl.RightAnswerCount;
        }

        if( completionDates.Count == 0 )
            dateLabel.Text = "";
        else if( completionDates.Count == 1 )
            dateLabel.Text = completionDates[0].ToShortDateString();
        else if( completionDates.Count == 2 )
            dateLabel.Text = completionDates[0].ToShortDateString() + ", " + completionDates[1].ToShortDateString();
        else
            dateLabel.Text = completionDates[0].ToShortDateString() + " — " + completionDates[completionDates.Count - 1].ToShortDateString();

        tryCountLabel.Text = m_TryCount.ToString();
        questionCountLabel.Text = m_QuestionCount.ToString();

        averageRequiredPointsLabel.Text =
            AnswerPercent.ToString("0.0");

        averagePointsLabel.Text =
            m_QuestionCount == 0 ? m_TotalPoints.ToString() : (m_TotalPoints / m_QuestionCount).ToString("0.0");

        averageRightAnswerPercentLabel.Text =
            (m_TotalAnswerCount == 0 ? (m_RightAnswerCount * 100).ToString() : (m_RightAnswerCount * 100 / m_TotalAnswerCount).ToString())
        + "%";
    }

    void BuildChildren()
    {
        domainNameLabel.Text = CourseDomainRow.Name;
        BuildChildDomains();
        BuildChildCourses();
    }

    void BuildChildDomains()
    {
        List<StudentsReportsData.CourseDomainRow> childDomains = new List<StudentsReportsData.CourseDomainRow>();
        foreach( StudentsReportsData.CourseDomainRow domainRow in CourseDomainRow.GetCourseDomainRows() )
        {
            childDomains.Add(domainRow);
        }

        childDomainControls = new List<StudentReports_CourseDomainSubControl>(childDomains.Count);
        foreach( StudentsReportsData.CourseDomainRow domainRow in childDomains )
        {
            StudentReports_CourseDomainSubControl domainControl = (StudentReports_CourseDomainSubControl)LoadControl("CourseDomainSubControl.ascx");
            domainControl.CourseDomainRow = domainRow;
            if( !domainControl.GetVisibleByFilter() )
                continue;

            anyChildVisilble = true;

            childDomainControls.Add(domainControl);

            domainControl.ParentDeepLevel = this.ParentDeepLevel + 1;
        }

        childDomainControls.Sort();

        foreach( StudentReports_CourseDomainSubControl domainControl in childDomainControls )
        {
            childDomainsPlaceHolder.Controls.Add(domainControl);
        }        
    }

    public bool GetVisibleByFilter()
    {
        string domainFilter = StudentsReportsDataBuilder.GetFilters(CourseDomainRow).CourseDomain;
        if( domainFilter != null )
        {
            bool foundByName = false;

            Predicate<StudentsReportsData.CourseDomainRow> findRecursiveChild = null;
            findRecursiveChild = delegate(StudentsReportsData.CourseDomainRow childDomainRow)
            {
                foundByName = childDomainRow.Name.IndexOf(
                    domainFilter,
                    StringComparison.OrdinalIgnoreCase) >= 0;

                if( !foundByName )
                {
                    foreach( StudentsReportsData.CourseDomainRow subChildDomainRow in childDomainRow.GetCourseDomainRows() )
                    {
                        foundByName = findRecursiveChild(subChildDomainRow);
                        if( foundByName )
                            break;
                    }
                }

                return foundByName;
            };

            for(
                StudentsReportsData.CourseDomainRow parentDomainRow = CourseDomainRow.CourseDomainRowParent;
                parentDomainRow != null;
                parentDomainRow = parentDomainRow.CourseDomainRowParent )
            {
                foundByName = parentDomainRow.Name.IndexOf(
                    domainFilter,
                    StringComparison.OrdinalIgnoreCase) >= 0;
                if( foundByName )
                    break;
            }

            if( foundByName )
            {
                foundByName = findRecursiveChild(CourseDomainRow);
            }
        }

        if( childCourseControls.Count > 0 || childDomainControls.Count > 0 )
            return true;
        else
            return false;
    }

    void BuildChildCourses()
    {
        List<StudentsReportsData.CoursesRow> childCourses = new List<StudentsReportsData.CoursesRow>();
        foreach( StudentsReportsData.CoursesRow courseRow in CourseDomainRow.GetCoursesRows() )
        {
            childCourses.Add(courseRow);
        }

        childCourseControls = new List<StudentReports_CourseSubControl>(childCourses.Count);
        foreach( StudentsReportsData.CoursesRow courseRow in childCourses )
        {
			ASP.CourseSubControl courseControl = (ASP.CourseSubControl)LoadControl("CourseSubControl.ascx");
            courseControl.CourseRow = courseRow;
            if( !courseControl.GetVisibleByFilter() )
                continue;

            childCourseControls.Add(courseControl);

            courseControl.ParentDeepLevel = this.ParentDeepLevel + 1;
        }

        childCourseControls.Sort();

        foreach( StudentReports_CourseSubControl courseControl in childCourseControls )
        {
            childCoursesPlaceHolder.Controls.Add(courseControl);
        }
    }

    protected void expandCollapse_Click(object sender, EventArgs e)
    {
        childDomainsPlaceHolder.Visible = !expandCollapse.Collapsed;
        childCoursesPlaceHolder.Visible = !expandCollapse.Collapsed;
    }

    public int CompareTo(StudentReports_CourseDomainSubControl other)
    {
        switch( StudentsReportsDataBuilder.GetFilters(CourseDomainRow).SortColumn )
        {
            case StudentsReportsDataBuilder.SortColumn.AnswerPercent:
                return this.AnswerPercent.CompareTo(other.AnswerPercent);

            case StudentsReportsDataBuilder.SortColumn.CollectedPoints:
                return this.CollectedPoints.CompareTo(other.CollectedPoints);

            case StudentsReportsDataBuilder.SortColumn.QuestionCount:
                return this.QuestionCount.CompareTo(other.QuestionCount);

            case StudentsReportsDataBuilder.SortColumn.RequiredPoints:
                return this.RequiredPoints.CompareTo(other.RequiredPoints);

            case StudentsReportsDataBuilder.SortColumn.TryCount:
                return this.TryCount.CompareTo(other.TryCount);

            default:
                return string.Compare(
                    this.CourseDomainRow.Name,
                    other.CourseDomainRow.Name,
                    StringComparison.OrdinalIgnoreCase);
        }
    }
}