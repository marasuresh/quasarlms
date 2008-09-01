<%@ Control
		Language="c#"
		Inherits="CourseIntro"
		CodeFile="CourseIntro.ascx.cs" %>
<%@ Import Namespace="System.Linq" %>
<table	cellspacing="0"
		cellpadding="0"
		width="100%"
		border="0"
		align="center"
		class="InnerTable">
	<tr valign="top">
		<td width="75%">
			<h3 class="cap3"><%= Resources.CourseIntro.Caption %></h3>
			<h3 class="cap4" title='<%= this.CurrentItem.Text %>'>
				<%= this.CurrentItem.Title %></h3>
			
			<% if(!string.IsNullOrEmpty(this.CurrentItem.Author)) { %>
				<p><%= Resources.CourseIntro.AuthorA %>:&nbsp;<%= this.CurrentItem.Author%></p>
			<% } %>
			
			<iframe	name="contFrame"
					width="100%"
					id="contFrameId"
					frameborder="no"
					height="100%"
					align="top"
					scrolling="no"
					src='<%= (this.CurrentItem.DescriptionUrl ?? "about:blank") %>'>
			</iframe>
			
			<% if(!string.IsNullOrEmpty(this.CurrentItem.RequirementsUrl)) { %>
				<h3 class="cap3">
					<%= Resources.CourseIntro.RequirementsA %>:</h3>
				<iframe	name="contFrame"
						width="100%"
						id="contFrameId1"
						frameborder="no"
						height="100%"
						align="top"
						scrolling="no"
						src='<%= this.CurrentItem.RequirementsUrl ?? "about:blank" %>'>
				</iframe>
			<% } %>
			<% var _additions = this.CurrentItem.GetDetailCollection("Additions", false); %>
			<% if (null != _additions && _additions.Count > 0) { %>
				<h3 class="cap3">
					<%= Resources.CourseIntro.Additions %>:</h3>
				<% foreach (string _addition in _additions) { %>
				<p><small><%= _addition %></small></p>
				<% } %>
			<% } %>
		</td>
	</tr>
</table>