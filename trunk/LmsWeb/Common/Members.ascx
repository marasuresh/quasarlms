<%@ Reference Page="~/Learn/Trainings.aspx" %>
<%@ Control Language="c#" Inherits="DCE.Common.Members" CodeFile="Members.ascx.cs" %>
<%@ Import Namespace="System.Xml" %>

<script language="javascript"><!--
 function resizeFrame()
 {
 theHeight = document.getElementById("contFrameId").contentWindow.document.body.scrollHeight;
 document.getElementById("contFrameId").style.height = theHeight;
 }
--></script>

<table	cellspacing="0"
		cellpadding="0"
		width="100%"
		border="0"
		align="center"
		class="InnerTable">
	<tr valign="top">
		<td width="75%" style="PADDING-TOP: 7px">
			<h3 class="cap3"><%= Resources.Members.Caption %></h3>
			<h3 class="cap4" title='<%= this.Training.Course.Description %>'>
				<%= this.Training.Course.Title %>
			<br/><a><%= string.Format(@"{0:d}&nbsp;/&nbsp;{0:d}",
							this.Training.StartOn,
							this.Training.FinishOn) %></a></h3>
		</td>
	</tr>
</table>

<hr/>
	<p		class="CenterColumn">
		<asp:Image ID="Image1"
				runat="server"
				ImageUrl="~/images/bullet.gif"
				Width="14"
				Height="14"
				border="0" />
		&nbsp;&nbsp;&nbsp;
		<strong	class="yellow">
			<%= Resources.Members.Students %></strong></p>
<table class="TableList" CellPadding="3" CellSpacing="0">
	<tr><th><%= Resources.Members.Members_Header_Photo %></th>
		<th><%= Resources.Members.Members_Header_Name %></th>
		<th><%= Resources.Members.Members_Header_email %></th>
	</tr>
<% var i = 0; %>
<% foreach (var _user in this.Training.Members) { %>
<% i++; %>
	<tr bgcolor='<%= 0 == i % 2 ? "#F6F6F6" : "#ffffff" %>'>
		<td width="80"><img src="<%= _user["Photo"] %>" class="HeadCenter" /></td>
		<td width="40%" class="HeadCenter" nowrap="nowrap"><%= _user.Title %></td>
		<td width="55%" class="HeadCenter"><a href="mailto:<%= _user.Email %>"><%= _user.Email %></a></td>
	</tr>
<% } %>
</table>