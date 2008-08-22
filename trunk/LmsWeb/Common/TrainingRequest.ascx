<%@ Control Language="c#" Inherits="DCE.Common.TrainingRequest" CodeFile="TrainingRequest.ascx.cs" %>
<%@ Import Namespace="System.Linq" %>
	<%--<p>
		<asp:Label runat="server" AssociatedControlID="tbKeywords" Text="<%$ Resources: CourseRequests, EnterKeywords %>" />
		<asp:TextBox runat="server" ID="tbKeywords" />
		<asp:Button runat="server" ID="btnSearch" Text="<%$ Resources: CourseRequests, Search %>" />
	</p>--%>
	<%--
	<xsl:if test="xml/NotFound">
		<h3 class="failure"><xsl:value-of select="xml/NotFoundA"/></h3>
	</xsl:if>--%>
	
	<% if (this.Requests.Any()) { %>
		<br/><h3 class="cap2"><%= Resources.CourseRequests.tableExisdataTRequests_Caption %></h3>
		<p class="help"><%= Resources.CourseRequests.Help1 %><br/>
			<%= Resources.CourseRequests.Help2 %></p>
		
		<table cellspacing="0" cellpadding="3" border="1" width="100%" align="center" class="TableList">
			<tr><th><%= Resources.CourseRequests.tableExisdataTRequests_tableHeader_01 %></th>
				<th><%= Resources.CourseRequests.tableExisdataTRequests_tableHeader_02 %></th>
				<th><%= Resources.CourseRequests.tableExisdataTRequests_tableHeader_03 %></th>
				<th><%= Resources.CourseRequests.tableExisdataTRequests_tableHeader_04 %></th></tr>
			<% var i = 0; %>
			<% foreach (var _request in this.Requests) { %>
			<% i++; %>
				<tr bgcolor='<%= i%2==0 ? "#FFFFFF" : "#F6F6F6" %>'>
					<td width="80" align="center" nowrap="true">
						<a href="?del=<%= _request.Name %>"><%= Resources.CourseRequests.tableExisdataTRequests_tableHeader_01 %></a></td>
					<td width="100" align="center" nowrap="true">
						<a><%= _request.StartDate %></a></td>
					<td width="40%" title="<%= _request.Course.Description %>">
						<a href="../Lms/UI/CourseInfo.aspx?cid=<%= _request.Course.Name %>">
							<%= _request.Course.Title %></a></td>
					<td width="55%"><%= _request.Comments %></td>
				</tr>
			<% } %>
		</table>
		<hr/>
	<% } %>
	<% if (this.Courses.Any()) { %>
		<h3 class="cap2"><%= Resources.CourseRequests.tableAccessibleCourses_Caption %>
			<% if (false) { %>&nbsp;(<%= Resources.CourseRequests.Founds%>)<% } %></h3>
		<p class="help"><%= Resources.CourseRequests.tableAccessibleCourses_Note %></p>
		<table	cellspacing="0"
				cellpadding="3"
				border="1"
				width="100%"
				align="center"
				class="TableList">
			<tr><th><%= Resources.CourseRequests.tableAccessibleCourses_tableHeader_01 %></th>
				<th><%= Resources.CourseRequests.tableAccessibleCourses_tableHeader_02 %></th>
				<th><%= Resources.CourseRequests.tableAccessibleCourses_tableHeader_03 %></th>
				<th><%= Resources.CourseRequests.tableAccessibleCourses_tableHeader_04 %></th></tr>
			<% var i = 0; %>
			<% foreach(var _course in this.Courses) { %>
			<% i++; %>
				<tr bgcolor='<%= 0 == i % 2 ? "#FFFFFF" : "#F6F6F6" %>'>
					<td width="80" align="center" nowrap="true">
						<a href="?add=<%= _course.Name %>"><%= Resources.CourseRequests.tableAccessibleCourses_tableHeader_01 %></a>
					<td		width="100"
							align="center"
							nowrap="true">
						<input	type="text"
								name="date"
								class="clear" 
								value='<%= System.DateTime.Now %>' /></td>
					<td		width="40%"
							title="<%= _course.Description %>">
						<a href="../Lms/UI/CourseInfo.aspx?cid=<%= _course.Name %>"><%= _course.Title %></a></td>
					<td		width="55%"
							title="<%= Resources.CourseRequests.tableAccessibleCourses_CommentAlt %>">
						<input	type="text"
								name="comment"
								class="clear"
								maxlength="255"/></td>
				</tr>
			<% } %>
		</table>
	<% } %>
<%--
	<xsl:if test="count(xml/ds/Area)>0">
		<p class="help"><xsl:value-of select="xml/Help0"/></p>
		
		<ul>
			<xsl:for-each select="xml/ds/Area">
				<li><h3 class="cap3"><a href="javascript:menuClick('', 'TrainingRequest', '', '{id}', '', '')"><xsl:value-of select="Name"/></a>
					</h3>
				</li>
			</xsl:for-each>
		</ul>
	</xsl:if>
--%>