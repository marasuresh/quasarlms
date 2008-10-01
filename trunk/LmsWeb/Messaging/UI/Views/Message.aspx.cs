using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using N2.Web.UI;
using N2.Messaging;

public partial class Messaging_UI_Message : N2.Web.UI.ContentPage<Message>
{
    protected override void OnInit(EventArgs e)
    {
        //this.ie.CurrentItem = this.CurrentItem;
        
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        dsMessage.CurrentItem = CurrentItem;
        fvMessage.DataSource = dsMessage;
        fvMessage.DataBind();
    }
}
