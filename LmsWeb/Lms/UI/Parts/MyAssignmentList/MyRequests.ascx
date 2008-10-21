<%@ Control
		Language="C#"
		AutoEventWireup="true"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.MyAssignmentList, N2.Lms]], N2.Templates" %>
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
					_request.PerformGenericAction("Cancel", Profile.UserName, "Canceled");
					//this.Engine.Persister.Save(_request);
					BindData();
				}
				break;
		}
	}
	
	void BindData()
	{
		this.rptRequests.DataSource = this.CurrentItem.RequestContainer.MyPendingRequests;
		this.rptRequests.DataBind();
	}

	protected override void OnInit(EventArgs e)
	{
		this.BindData();
		base.OnInit(e);
	}
</script>
<p class="help"><%= Resources.CourseRequests.Help1 %><br/>
			<%= Resources.CourseRequests.Help2 %></p>
<asp:Repeater
			runat="server"
			ID="rptRequests"
			Visible='<%# this.CurrentItem.RequestContainer.MyApprovedApplications.Any() %>'
			OnItemCommand="List_Command">
				<HeaderTemplate>
		
		<table cellspacing="0" cellpadding="3" align="center">
			<tr>
				<th><%= Resources.CourseRequests.tableExisdataTRequests_tableHeader_03 %></th>
				<th></th></tr>
				
				</HeaderTemplate>
				
				<FooterTemplate>
				</table>
				</FooterTemplate>
				
				<ItemTemplate>
					<tr bgcolor='<%# Container.ItemIndex % 2  == 1 ? "#FFFFFF" : "#F6F6F6" %>'>
				
					<td title="<%# Eval("Course.DescriptionUrl") %>">
						<%# N2.Web.Link.To(((Request)Container.DataItem).Course).ToString() %>
						
						<em><%# Eval("Comments") %></em>
						
						<small><%# ((Request)Container.DataItem).Published.Value.ToShortDateString() %></small>
						</td>
					<td><asp:ImageButton
								runat="server"
								ID="lbDeleteRequest"
								ImageUrl="~/Lms/UI/Img/03/01.png"
								CommandArgument='<%# Eval("ID") %>'
								CommandName="DeleteRequest"
								AlternateText="<%$ Resources: CourseRequests, tableExisdataTRequests_tableHeader_01 %>" /></td>
				</tr>

				</ItemTemplate>
			</asp:Repeater>