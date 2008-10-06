<%@ Control
		Language="C#"
		ClassName="TrainingPlayer"
		Inherits="N2.Web.UI.ContentUserControl`1[[N2.Lms.Items.TrainingWorkflow.TrainingTicket, N2.Lms]], N2" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="N2.Templates.Items" %>
<%@ Import Namespace="N2.Templates.Web.UI" %>

<script runat="server">
	protected override void OnLoad(EventArgs e)
	{
		if (null != this.CurrentItem.CurrentTopic) {

			var _uc = (TemplateUserControl<AbstractContentPage, Topic>)((N2.Definitions.IContainable)this.CurrentItem.CurrentTopic).AddTo(this);
			_uc.CurrentItem = this.CurrentItem.CurrentTopic;
			
			System.Diagnostics.Trace.WriteLine("Player: " + this.CurrentItem.CurrentTopic.Title, "Lms");
		}
		base.OnLoad(e);
	}
</script>
