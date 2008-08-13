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

public partial class Trainings_TrainingList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int PageSize
    {
        get { return TrainingsGridView.PageSize; }
        set { TrainingsGridView.PageSize = value; }
    }
    protected void TrainingTitlesDataSource_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}
