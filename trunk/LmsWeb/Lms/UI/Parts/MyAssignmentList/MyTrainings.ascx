<%@ Control
		Language="C#"
		AutoEventWireup="true"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.MyAssignmentList, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="N2.Lms.Items.Lms.RequestStates" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>
<%@ Import Namespace="N2.Lms.Items.TrainingWorkflow" %>

<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		this.rpt.ItemCommand += (_o, _e) => {
			if (string.Equals(_e.CommandName, "MarkComplete", StringComparison.InvariantCultureIgnoreCase)) {
				var _state = N2.Context.Persister.Get<ApprovedState>(int.Parse((string)_e.CommandArgument));
				var _request = _state.Parent;
				_request.PerformAction(
					"Finish",
					Profile.UserName,
					"Finished by " + Context.User.Identity.Name,
					new Dictionary<string, object>{{ "Grade", 5 }});
				this.BindData(_request.Parent as RequestContainer);
			}
		};
		
		if (!this.IsPostBack) {
			this.BindData(this.CurrentItem.RequestContainer);
		}
		
		base.OnInit(e);
	}

	void BindData(RequestContainer rc)
	{
		this.rpt.DataSource = rc.MyApprovedApplications;
		this.rpt.DataBind();
	}
</script>

<asp:Repeater runat="server" ID="rpt">
	<HeaderTemplate><table></HeaderTemplate>
	<FooterTemplate></table></FooterTemplate>
	<ItemTemplate>
	<tr><td><a href='<%# this.ResolveClientUrl(string.Concat("~/Lms/UI/Player.aspx?id=", Eval("Ticket.ID").ToString())) %>'><%# Eval("Ticket.Training") != null ? Eval("Ticket.Training.Title") : "Error: Training is NULL" %></a></td>
		<td><asp:ImageButton
					runat="server"
					ID="btnMarkComplete"
					ImageUrl="~/Lms/UI/Img/cancel.png"
					AlternateText="Закончить"
					CommandName="MarkComplete"
					CommandArgument='<%# Eval("ID") %>' /></td></tr>
	</ItemTemplate>
</asp:Repeater>