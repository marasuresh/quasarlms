<%@ Control
		Language="C#"
		AutoEventWireup="true"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.MyCourses, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>

<script runat="server">
	protected void List_Command(object sender, CommandEventArgs e)
	{
		
	}
	
	void BindData()
	{
		this.rptCourses.DataSource = this.CurrentItem.MyAvailableCourses;
		this.rptCourses.DataBind();
	}

	protected override void OnInit(EventArgs e)
	{
		this.BindData();
		base.OnInit(e);
	}
</script>
<asp:Repeater
		runat="server"
		ID="rptCourses"
		OnItemCommand="List_Command">
	<HeaderTemplate>
	<h3 class="cap2"><%= Resources.CourseRequests.tableAccessibleCourses_Caption%>
			<% if (false) { %>&nbsp;(<%= Resources.CourseRequests.Founds%>)<% } %></h3>
		<p class="help"><%= Resources.CourseRequests.tableAccessibleCourses_Note%></p>
		<table	cellspacing="0"
				cellpadding="3"
				border="1"
				width="100%"
				align="center"
				class="TableList">
			<tr><th><%= Resources.CourseRequests.tableAccessibleCourses_tableHeader_01%></th>
				<th><%= Resources.CourseRequests.tableAccessibleCourses_tableHeader_02%></th>
				<th><%= Resources.CourseRequests.tableAccessibleCourses_tableHeader_03%></th>
				<th><%= Resources.CourseRequests.tableAccessibleCourses_tableHeader_04%></th></tr>
	</HeaderTemplate>
	<FooterTemplate></table></FooterTemplate>
	<ItemTemplate>
		<tr bgcolor='<%# 2 == 0 % 2 ? "#FFFFFF" : "#F6F6F6" %>'>
			<td width="80" align="center" nowrap="true">
				<asp:LinkButton ID="LinkButton1"
						runat="server"
						CommandArgument='<%# Eval("ID") %>'
						CommandName="PostRequest"
						Text='<%$ Resources: CourseRequests, tableAccessibleCourses_tableHeader_01 %>' />
					<td		width="100"
							align="center"
							nowrap="true">
						<n2:DatePicker
								runat="server"
								SelectedDate='<%# System.DateTime.Now %>' /></td>
					<td		width="40%"
							title="<%# Eval("DescriptionUrl") %>">
						<a href="../Lms/UI/CourseInfo.aspx?cid=<%# Eval("Name") %>"><%# Eval("Title") %></a></td>
					<td		width="55%"
							title="<%= Resources.CourseRequests.tableAccessibleCourses_CommentAlt %>">
						<input	type="text"
								name="comment"
								class="clear"
								maxlength="255"/></td>
				</tr>
	</ItemTemplate>
</asp:Repeater>
