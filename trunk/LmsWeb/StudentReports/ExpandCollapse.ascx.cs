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

public partial class StudentReports_ExpandCollapse : System.Web.UI.UserControl
{
    const string MinusText = "–";
    const string PlusText = "+";

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        Page.RegisterRequiresControlState(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool Collapsed
    {
        get { return plusMinusButton.Text == PlusText; }
        set
        {
            if( value == this.Collapsed )
                return;
            SetCollapsed(value);
        }
    }

    public event EventHandler Click;

    protected override void LoadControlState(object savedState)
    {
        if( savedState is bool )
            SetCollapsed((bool)savedState);
    }

    protected override object SaveControlState()
    {
        return Collapsed;
    }

    void SetCollapsed(bool collapsed)
    {
        if( collapsed )
            plusMinusButton.Text = PlusText;
        else
            plusMinusButton.Text = MinusText;
    }

    protected void plusMinusButton_Click(object sender, EventArgs e)
    {
        SetCollapsed(!Collapsed);

        EventHandler temp = Click;
        if( temp != null )
            temp(this,EventArgs.Empty);
    }
}
