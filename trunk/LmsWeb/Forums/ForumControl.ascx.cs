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

public partial class Forums_ForumControl : System.Web.UI.UserControl
{
	const string VS_TRAINING_ID = "TrainingId";
	public Guid? TrainingId {
		get {
			return PageParameters.trId;
			//return (Guid?)this.ViewState[VS_TRAINING_ID];
		}
		/*set {
			this.ViewState[VS_TRAINING_ID] = value;
			this.ForumTopicList1.DataBind();
			this.CreateTopicControl1.DataBind();
		}*/
	}
	
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!this.IsPostBack) {
			//this.TrainingId = PageParameters.trId;
			this.ForumTopicList1.DataBind();
			this.CreateTopicControl1.DataBind();
		}
    }
}
