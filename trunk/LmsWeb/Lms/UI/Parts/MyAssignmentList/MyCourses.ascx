<%@ Control
		Language="C#"
		AutoEventWireup="true"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.MyAssignmentList, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>

<script runat="server">
	void BindData()
	{
		this.rpt.DataSource = this.CurrentItem.MyAvailableCourses;
		this.rpt.DataBind();
	}

	protected override void OnInit(EventArgs e)
	{
		this.rpt.ItemCommand += (o, e0) => {
			var _courseId = int.Parse(((string)e0.CommandArgument));
			Course _course = this.Engine.Persister.Get<Course>(_courseId);
			
			this.CurrentItem.RequestContainer.SubscribeTo(
				_course,
				this.Page.User.Identity.Name);
			
			Debug.WriteLine("e.CommandArgument: " + _courseId.ToString(), "Lms");
			BindData();
		};
		
		this.BindData();
		base.OnInit(e);
	}
</script>

<asp:Repeater
		runat="server"
		ID="rpt">
	<HeaderTemplate>
		<table	cellspacing="0"
				cellpadding="3"
				border="0"
				align="center">
			<tr><th></th>
				<th><%= Resources.CourseRequests.tableAccessibleCourses_tableHeader_02%></th>
				<th><%= Resources.CourseRequests.tableAccessibleCourses_tableHeader_03%></th></tr>
	</HeaderTemplate>
	<FooterTemplate></table></FooterTemplate>
	<ItemTemplate>
		<tr bgcolor='<%# 2 == Container.ItemIndex % 2 ? "#FFFFFF" : "#F6F6F6" %>'>
			<td><asp:ImageButton
						runat="server"
						ImageUrl="~/Lms/UI/Img/03/02.png"
						CommandArgument='<%# Eval("ID") %>'
						CommandName="PostRequest"
						AlternateText='<%$ Resources: CourseRequests, tableAccessibleCourses_tableHeader_01 %>' />
					<td><n2:DatePicker
								runat="server"
								SelectedDate='<%# System.DateTime.Now %>' /></td>
					<td title="<%# Eval("DescriptionUrl") %>">
						<asp:HyperLink
								runat="server"
								NavigateUrl='<%# ((Course)Container.DataItem).Url %>'
								Text='<%# Eval("Title") %>' /></td>
				</tr>
	</ItemTemplate>
</asp:Repeater>
