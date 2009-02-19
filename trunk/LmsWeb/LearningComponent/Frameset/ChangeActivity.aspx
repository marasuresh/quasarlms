<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>
<%@ Page
	Language	= "C#"
	Inherits	= "Microsoft.LearningComponents.Frameset.Frameset_ChangeActivity"
%>
<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Register.StyleSheet(this.Page, "Theme/Styles.css");
		Register.JQuery(this.Page);
		Register.JavaScript(this.Page, "Include/FramesetMgr.js");
		Register.JavaScript(this.Page, @"
var frameMgr = API_GetFramesetManager();
" + (this.HasError
	? @"
frameMgr.SetPostFrame(HIDDEN_FRAME);
frameMgr.SetPostableForm(window.top.frames[MAIN_FRAME].document.getElementById(HIDDEN_FRAME).contentWindow.document.forms[0]);
frameMgr.ShowErrorMessage('" + ErrorTitleHtml + "', '" + ErrorMsgHtml + "')"
	: @"
setTimeout(PleaseWait, 500);" + this.GetFrameMgrInit()
)	+ @"
function PleaseWait() {
	try {
		// clears content from the window and displays a ""Please wait"" message
		document.body.innerHTML = '<table width='100%' class='ErrorTitle'><tr><td align='center'><%=PleaseWaitHtml %></td></tr></table>';
	} catch(e) {
		// only happens in odd boundary cases. Retry the message after another timeout.
		setTimeout(PleaseWait, 500);
	}
}
", ScriptOptions.DocumentReady);
	}
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER. 
     MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
     LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. THERE IS 
     NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
     
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" />
<body class="ErrorBody">
    &nbsp;
</body>
</html>
