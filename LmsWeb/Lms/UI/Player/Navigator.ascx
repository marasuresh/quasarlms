<%@ Control
		Language="C#"
		ClassName="PlayerNavigation"
		Inherits="N2.Web.UI.ContentUserControl`1[[N2.Lms.Items.TrainingWorkflow.TrainingTicket, N2.Lms]], N2" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="N2.Collections" %>
<%@ Import Namespace="N2.Web" %>

<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Training _training = this.CurrentItem.Training;
		
		this.Controls.Add(
			Tree.From(_training.Workflow)
				.ExcludeRoot(true)
				.Filters(new TypeFilter(typeof(ScheduledTopic)))
				.LinkProvider(item => Link.To(new Link(
					item.Title,
					Url.Parse(Link.To(this.CurrentItem).Url)
						.AppendSegment("topic")
						.AppendSegment(item is ScheduledTopic ? ((ScheduledTopic)item).Topic.Name : string.Empty)
						.ToString()
					))
				)
				//.ClassProvider(item => )
				.ToControl()
		);
	}
</script>
