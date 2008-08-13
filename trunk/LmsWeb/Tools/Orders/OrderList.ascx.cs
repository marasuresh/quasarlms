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

public partial class Orders_OrderList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int PageSize
    {
        get { return ordersGridView.PageSize; }
        set { ordersGridView.PageSize = value; }
    }

    protected void ordersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName == "Subscribe" )
        {
            Response.Redirect("~/Tools/Orders/Subscribe.aspx?id=" + GridViewHelpers.GetKeyByCommandArgument(
                e.CommandArgument,
                ordersGridView));
        }
        else if( e.CommandName == "Reject" )
        {
            OnRejectOrder(GridViewHelpers.GetKeyByCommandArgument(
                e.CommandArgument,
                ordersGridView));
        }
    }

    private void OnRejectOrder(Guid orderID)
    {
        OrdersQueriesTableAdapters.QueriesTableAdapter spAdapter = new OrdersQueriesTableAdapters.QueriesTableAdapter();
        spAdapter.dcetools_Orders_RejectOrder(
            CurrentUser.Region.ID,
            orderID);
        Page.DataBind();
    }
}
