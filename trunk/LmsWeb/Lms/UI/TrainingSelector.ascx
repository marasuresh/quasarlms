<%@ Control
		Language="C#"
		ClassName="TrainingSelector"
		Inherits="N2.Web.UI.ContentUserControl`1[[N2.Lms.Items.TrainingList, N2.Lms]], N2" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>
<%@ Import Namespace="N2.Lms.Items.Lms.RequestStates" %>
<%@ Import Namespace="N2.Lms.Items.TrainingWorkflow" %>

<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);

		//if (!this.IsPostBack) {
		this.rptApprovedApplications.DataSource = this.CurrentItem.MyApprovedApplications;
		this.rptApprovedApplications.DataBind();
		//}
	}
	
	void List_Command(object sender, CommandEventArgs e)
	{
		switch (e.CommandName) {
			case "StartLearning":
				int _applicationId = Int32.Parse((string)e.CommandArgument);
				ApprovedState _application = N2.Context.Persister.Get<ApprovedState>(_applicationId);
				
				this.Profile.CurrentTrainingId = _application.Training.ID;
				
				TrainingTicket _ticket = this.Engine.Definitions.CreateInstance<TrainingTicket>(_application);
				Debug.WriteLine("e.CommandArgument: " + e.CommandArgument);
				_ticket.SavedBy = this.Profile.UserName;
				_ticket.Title = _application.Training.Title;
				this.Engine.Persister.Save(_ticket);

				this.Response.Redirect(this.CurrentItem.Url);
				
				break;
		}
	}

</script>

<asp:Repeater
		runat="server"
		ID="rptApprovedApplications"
		OnItemCommand="List_Command">
	
	<HeaderTemplate>
	<table	cellspacing="0"
				cellpadding="3"
				border="1"
				width="100%"
				align="center"
				class="TableList">
		<tr><th></th><th>Title</th></tr>
	</HeaderTemplate>
	
	<FooterTemplate>
	</table>
	</FooterTemplate>
	
	<ItemTemplate>
		<tr><td><asp:LinkButton ID="LinkButton1"
						runat="server"
						CommandArgument="<%# ((ApprovedState)Container.DataItem).ID %>"
						CommandName="StartLearning">
						<asp:Image ImageUrl="~/Lms/UI/Img/03/31.png" runat="server" />Start Learning
						</asp:LinkButton>
						</td>
			<td><%# ((ApprovedState)Container.DataItem).Training.Title %></td></tr>
	</ItemTemplate>
</asp:Repeater>