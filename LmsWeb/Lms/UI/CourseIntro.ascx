<%@ Control Language="c#" Inherits="DCE.Common.CourseIntro" CodeFile="CourseIntro.ascx.cs" %>
<script type="text/javascript" language="javascript"><!--
function resizeFrm(ctid){
	try{
		theHeight = document.getElementById(ctid).contentWindow.document.body.scrollHeight;
		if(theHeight!=0);{
			document.getElementById(ctid).style.height = theHeight+50;
		}
	}catch(e){
		document.getElementById(ctid).scrolling="auto";
		document.getElementById(ctid).style.height = 500;
	}
}
function resizeFrame(){resizeFrm("contFrameId");}
function resizeFrame1(){resizeFrm("contFrameId1");}
//--></script>

<table	cellspacing="0"
		cellpadding="0"
		width="100%"
		border="0"
		align="center"
		class="InnerTable">
	<tr valign="top">
		<td width="75%">
			<h3 class="cap3">
				<%= Resources.CourseIntro.Caption %></h3>
			<h3		class="cap4"
					title='<%= this.Course.Description %>'>
				<%= this.Course.Title %></h3>
			
			<% if(!string.IsNullOrEmpty(this.Course.Author)) { %>
				<p><%= Resources.CourseIntro.AuthorA %>:&nbsp;<%= this.Course.Author %></p>
			<% } %>
			
			<iframe	name="contFrame"
					width="100%"
					id="contFrameId"
					onresize="javascript:resizeFrame();"
					onload="javascript:resizeFrame();"
					frameborder="no"
					height="100%"
					align="top"
					scrolling="no"
					src='<%= (this.Course.DescriptionUrl ?? "about:blank") %>'>
			</iframe>
			
			<% if(!string.IsNullOrEmpty(this.Course.RequirementsUrl)) { %>
				<h3 class="cap3">
					<%= Resources.CourseIntro.RequirementsA %>:</h3>
				<iframe	name="contFrame"
						width="100%"
						id="contFrameId1"
						onresize="resizeFrame1()"
						onLoad="resizeFrame1()"
						frameborder="no"
						height="100%"
						align="top"
						scrolling="no"
						src='<%= this.Course.RequirementsUrl ?? "about:blank" %>'>
				</iframe>
			<% } %>
			<% if(!string.IsNullOrEmpty(this.Course.Additions)) { %>
				<h3 class="cap3">
					<%= Resources.CourseIntro.Additions %>:</h3>
				<small><%= this.Course.Additions %></small>
			<% } %>
		</td>
	</tr>
</table>