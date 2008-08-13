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

public partial class Tools_Administration_UserListWithSearch_SearchableUserList : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        Page.RegisterRequiresControlState(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int PageSize
    {
        get { return usersGridView.PageSize; }
        set { usersGridView.PageSize = value; }
    }

    public Guid? SelectedUser
    {
        get { return m_SelectedUser; }
    }

    public event EventHandler<GuidEventArgs> UserClick;

    Guid? m_SelectedUser;

    protected override void LoadControlState(object savedState)
    {
        base.LoadControlState(savedState);
        m_SelectedUser = (Guid?)savedState;
    }

    protected override object SaveControlState()
    {
        return m_SelectedUser;
    }


    protected void usersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName != "SelectUser" )
            return;

        Guid userID = GridViewHelpers.GetKeyByCommandArgument(
            e.CommandArgument,
            usersGridView);

        m_SelectedUser = userID;

        usersGridView.SelectedIndex = int.Parse(e.CommandArgument.ToString(), System.Globalization.CultureInfo.InvariantCulture);

        EventHandler<GuidEventArgs> temp = UserClick;
        if( temp != null )
            temp(this, new GuidEventArgs(userID));
    }

    protected void usersGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //usersGridView.DataKeys
        //e.Row.DataItemIndex
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if( m_SelectedUser==null )
        {
            selectedUserBox.Visible = false;
        }
        else
        {
            selectedUserBox.Visible = true;
            DceUser selectedDceUser = DceUserService.GetUserByID(m_SelectedUser.Value);
            selectedUserLabel.Text = selectedDceUser.FirstName + " " + selectedDceUser.Patronymic + " " + selectedDceUser.LastName;
        }
    }
}