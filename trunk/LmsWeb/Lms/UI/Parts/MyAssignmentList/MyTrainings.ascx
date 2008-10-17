<%@ Control
		Language="C#"
		AutoEventWireup="true"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.MyAssignmentList, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>
<%@ Import Namespace="N2.Lms.Items.TrainingWorkflow" %>

<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		if (!this.IsPostBack) {
			this.BindData();
		}
		base.OnInit(e);
	}

	void BindData()
	{
		this.rpt.DataSource = this.CurrentItem.RequestContainer.MyActiveTrainingTickets;
		this.rpt.DataBind();
	}
</script>

<asp:Repeater runat="server" ID="rpt">
	<HeaderTemplate><table></HeaderTemplate>
	<FooterTemplate></table></FooterTemplate>
	<ItemTemplate>
	<tr><td><a href='<%# this.ResolveClientUrl((string)Eval("Url")) %>'><%# Eval("Training") != null ? Eval("Training.Title") : "Error: Training is NULL" %></a></td></tr>
	</ItemTemplate>
</asp:Repeater>