<%@ Control Language="C#" 
       Inherits="ACalendar"
       CodeFile="ACalendar.ascx.cs"  %>
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
			<h3 class="cap4" title='<%= this.CurrentItem.Text %>'></h3>
			
			<% if(!string.IsNullOrEmpty(this.CurrentItem.Duration.ToString())) { %>
				<p>:&nbsp;<%= this.CurrentItem.Title%></p>
			<% } %>
			
<%--			<iframe	name="contFrame"
					width="100%"
					id="contFrameId"
					frameborder="no"
					height="100%"
					align="top"
					scrolling="no"
					src='<%= (this.CurrentItem.DescriptionUrl ?? "about:blank") %>'>
			</iframe>
--%>			
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