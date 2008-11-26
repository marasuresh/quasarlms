using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N2.Messaging.Messaging.UI.Parts
{
    public partial class SelectUsers : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string SelectedUser
        {
            get { return windowOpen.Text; }
            set { windowOpen.Text = value; }
        }
    }
}