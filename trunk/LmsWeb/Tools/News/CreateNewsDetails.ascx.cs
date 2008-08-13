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

public partial class News_CreateNewsDetails : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        titleTextBox.Text = Resources.CreateNewsResources.Title;
        contentTextBox.Text = Resources.CreateNewsResources.Content;
        RegionEditControl1.RegionGuid = CurrentUser.Region.ID;
		if(!this.IsPostBack) {
			tbDate.DataBind();
		}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    Guid? HomeRegion
    {
        get { return CurrentUser.Region.ID; }
    }

    Guid? SelectedRegion
    {
        get { return RegionEditControl1.RegionGuid; }
    }

    public event EventHandler NewsItemCreated;
    public event EventHandler CancelClick;

    protected void createButton_Click(object sender, EventArgs e)
    {
        NewsQueriesTableAdapters.QueriesTableAdapter createNewsAdapter = new NewsQueriesTableAdapters.QueriesTableAdapter();

        createNewsAdapter.dcetools_News_CreateDetails(
            HomeRegion,
            DateTime.Parse(this.tbDate.Text),
            titleTextBox.Text,
            contentTextBox.Text,
            urlTextBox.Text,
            SelectedRegion);


        EventHandler temp = NewsItemCreated;
        if( temp != null )
            temp(this,EventArgs.Empty);
        else
            Response.Redirect("Default.aspx");
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        EventHandler temp = CancelClick;
        if( temp != null )
            temp(this,EventArgs.Empty);
        else
            Response.Redirect("Default.aspx");
    }
}
