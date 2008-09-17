<%@ Page
		Language="C#"
		Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Lms.Items.CourseList, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>
<script runat="server">
	protected void List_Command(object sender, CommandEventArgs e)
	{
		switch (e.CommandName) {
			case "PostRequest": {
					Request _request = this.Engine.Definitions.CreateInstance<Request>(this.CurrentItem.RequestContainer);
					Debug.WriteLine("e.CommandArgument: " + e.CommandArgument);
					_request.SavedBy = this.User.Identity.Name;
					_request.Title = _request.Name;
					this.Engine.Persister.Save(_request);
					BindData();
				}
				break;
			case "DeleteRequest": {
					var _request = this.Engine.Persister.Get<Request>(int.Parse((string)e.CommandArgument));
					_request.PerformAction("Cancel", Profile.UserName, "Canceled");
					//this.Engine.Persister.Save(_request);
					BindData();
				}
				break;
		}
	}

	protected IEnumerable<Request> Requests {
		get {
			return
				this.CurrentItem
					.RequestContainer
					.Children
					.OfType<Request>()
					.Where(_r => _r.GetCurrentState().ToState.Name == "Active");
		}
	}
	
	protected IEnumerable<Course> Courses {
		get {
			return
				this.CurrentItem
					.CourseContainer
					.Children
					.OfType<Course>();
		}
	}

	protected override void OnLoad(EventArgs e)
	{
		Debug.WriteLine("Load");
		BindData();
		base.OnLoad(e);
	}

	void BindData()
	{
		this.rptCourses.DataSource = Courses;
		this.rptCourses.DataBind();

		this.rptRequests.DataSource = Requests;
		this.rptRequests.DataBind();
	}
</script>

<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">
	<asp:Repeater
			runat="server"
			ID="rptRequests"
			Visible='<%# this.Requests.Any() %>'
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
								CommandArgument='<%# Eval("ID") %>'
								CommandName="DeleteRequest"
								Text="<%$ Resources: CourseRequests, tableExisdataTRequests_tableHeader_01 %>" /></td>
					<td width="100" align="center" nowrap="true">
						<a><%# Eval("StartDate") %></a></td>
					<td width="40%" title="<%# Eval("Course.DescriptionUrl") %>">
						<a href="../Lms/UI/CourseInfo.aspx?cid=<%# Eval("Course.Name") %>">
							<%# Eval("Course.Title") %></a></td>
					<td width="55%"><%# Eval("Comments") %></td>
				</tr>

				</ItemTemplate>
			</asp:Repeater>

<asp:Repeater
		runat="server"
		ID="rptCourses"
		OnItemCommand="List_Command"
		Visible='<%# this.Courses.Any() %>'>
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
						<input	type="text"
								name="date"
								class="clear" 
								value='<%# System.DateTime.Now %>' /></td>
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
</asp:Content>
