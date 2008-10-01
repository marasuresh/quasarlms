<%@ Control
		Language="C#"
		ClassName="TrainingPlayer"
		Inherits="N2.Web.UI.ContentUserControl`1[[N2.Lms.Items.TrainingList, N2.Lms]], N2" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="N2.Collections" %>
<%@ Import Namespace="N2.Web" %>

<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Training _training = 
			this.CurrentItem.MyStartedTrainings.First().Training;
		
		this.Controls.Add(
			Tree.From(_training.Workflow)
				.ExcludeRoot(true)
				.Filters(new TypeFilter(typeof(ScheduledTopic)))
				.ToControl()
		);
	}
</script>
