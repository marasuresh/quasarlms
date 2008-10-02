<%@ Control
		Language="C#"
		AutoEventWireup="true"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.MyRequests, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>

<script runat="server">
	protected void List_Command(object sender, CommandEventArgs e)
	{
		switch (e.CommandName) {
			case "DeleteRequest": {
					var _request = this.Engine.Persister.Get<Request>(int.Parse((string)e.CommandArgument));
					Trace.Write("Lms", _request.GetType().Name);
					_request.PerformAction("Cancel", Profile.UserName, "Canceled");
					//this.Engine.Persister.Save(_request);
					BindData();
				}
				break;
		}
	}
	
	void BindData()
	{
		this.rptRequests.DataSource = this.CurrentItem.RequestContainer.MyApprovedApplications;
		this.rptRequests.DataBind();
	}

	protected override void OnInit(EventArgs e)
	{
		this.BindData();
		base.OnInit(e);
	}
</script>
<asp:Repeater
			runat="server"
			ID="rptRequests"
			Visible='<%# this.CurrentItem.RequestContainer.MyApprovedApplications.Any() %>'
			OnItemCommand="List_Command">
				<HeaderTemplate>
				<br/><h3 class="cap2"><%= Resources.CourseRequests.tableExisdataTRequests_Caption %></h3>
		<p class="help"><%= Resources.CourseRequests.Help1 %><br/>
			<%= Resources.CourseRequests.Help2 %></p>
		
		<table cellspacing="0" cellpadding="3" border="1" width="100%" align="center" class="TableList">
			<tr><th><%= Resources.CourseRequests.tableExisdataTRequests_tableHeader_01 %></th>
				<th><%= Resources.CourseRequests.tableExisdataTRequests_tableHeader_02 %></th>
				<th><%= Resources.CourseRequests.tableExisdataTRequests_tableHeader_03 %></th>
				<th><%= Resources.CourseRequests.tableExisdataTRequests_tableHeader_04 %></th></tr>
				</HeaderTemplate>
				
				<FooterTemplate>
				</table><hr/>
				</FooterTemplate>
				
				<ItemTemplate>
					<tr bgcolor='<%# 0 % 2==0 ? "#FFFFFF" : "#F6F6F6" %>'>
					<td width="80" align="center" nowrap="true">
						<asp:LinkButton
								runat="server"
								ID="lbDeleteRequest"
								CommandArgument='<%# Eval("Parent.ID") %>'
								CommandName="DeleteRequest"
								Text="<%$ Resources: CourseRequests, tableExisdataTRequests_tableHeader_01 %>" /></td>
					<td width="100" align="center" nowrap="true">
						<a><%# Eval("Training.Published") %></a></td>
					<td width="40%" title="<%# Eval("Training.Course.DescriptionUrl") %>">
						<a href="../Lms/UI/CourseInfo.aspx?cid=<%# Eval("Training.Course.Name") %>">
							<%# Eval("Training.Title") %></a></td>
					<td width="55%"><%# Eval("Parent.Comments") %></td>
				</tr>

				</ItemTemplate>
			</asp:Repeater>
