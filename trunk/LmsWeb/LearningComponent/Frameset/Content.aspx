<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>
<%@ Page
	Language = "C#"
	Inherits = "Microsoft.LearningComponents.Frameset.Frameset_Content"
%>
<script runat="server">
	//TODO move to OnInit
	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		Register.StyleSheet(this.Page, "Theme/Styles.css");
		Register.JQuery(this.Page);
		Register.JavaScript(this.Page, "Include/FramesetMgr.js");
		Register.JavaScript(this.Page,
/*  NOTE: The following HTML is *ONLY* ever rendered if there was an error in displaying the content page, and 
			 the content frame is currently the postable frame. In most normal processing, the entire page is replaced with 
			 the content rendered from the package. */
// Get frameset manager
@"frameMgr = API_GetFramesetManager();"
+
// Set data on frameset manager
 GetFrameMgrInit() 
+ @"
frameMgr.WaitForContentCompleted(0);    // since it's an error condition, don't wait for any more content frame loads
", ScriptOptions.DocumentReady);
	}
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
     MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
     LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
     NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
<html>
<head runat="server" />
<body class="ErrorBody">
	<form id="formId" runat="server">
	<% if (HasError) { %>
	<table border="0" width="100%" id="table1" style="border-collapse: collapse">
		<tr><td width="60">
				<p align="center">
					<img border="0" src="<%=FramesetPath %>Theme/<%=ErrorIcon%>" width="49" height="49">
			</td>
			<td class="ErrorTitle"><%=ErrorTitleHtml %></td></tr>
		<tr><td width="61">&nbsp;</td>
			<td width="100%"><hr /></td></tr>
		<tr><td width="61">&nbsp;</td>
			<td class="ErrorMessage"><%=ErrorMessageHtml %></td></tr>
	</table>
	<% } %>
	</form>
</body>
</html>
