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
		var _courseId = int.Parse(((string)e.CommandArgument));
		Course _course = this.Engine.Persister.Get<Course>(_courseId);

		if (null != _course) {
			Request _request = this.Engine.Definitions.CreateInstance<Request>(this.CurrentItem.RequestContainer);
			Debug.WriteLine("e.CommandArgument: " + e.CommandArgument);
			_request.User = _request.SavedBy = this.Page.User.Identity.Name;
			_request.Title = _request.Name;
			_request.Course = _course;
			this.Engine.Persister.Save(_request);
			BindData();
		}
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

<n2:H4 runat="server" Text="<%$ CurrentItem: Title %>" />
<n2:Box runat="server">
<asp:Repeater
		runat="server"
		ID="rptCourses"
		OnItemCommand="List_Command">
	<HeaderTemplate>
		<p class="help"><%= Resources.CourseRequests.tableAccessibleCourses_Note%></p>
		
		<table	cellspacing="0"
				cellpadding="3"
				border="1"
				width="100%"
				align="center"
				class="TableList">
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
</n2:Box>