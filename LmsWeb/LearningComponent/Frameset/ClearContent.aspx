<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>
<%@ Page
	Language	= "C#"
	Inherits	= "Microsoft.LearningComponents.Frameset.Frameset_ClearContent"
%>
<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Register.StyleSheet(this.Page, "Theme/Styles.css");
		Register.JQuery(this.Page);
		Register.JavaScript(this.Page, "Include/FramesetMgr.js");
		Register.JavaScript(this.Page, @"
function PleaseWait() {
	try {
		// clears content from the window and displays a 'Please wait' message
		document.body.innerHTML = ""<table width='100%' class='ErrorTitle'><tr><td align='center'>"
			+ this.PleaseWaitHtml
			+ @"</td></tr></table>"";
	} catch (e) {
		// only happens in odd boundary cases. Retry the message after another timeout.
		setTimeout(PleaseWait, 500);
	}
}

var frameMgr = API_GetFramesetManager();
frameMgr.SetPostFrame(HIDDEN_FRAME);
frameMgr.SetPostableForm(window.top.frames[MAIN_FRAME].document.getElementById(HIDDEN_FRAME).contentWindow.document.forms[0]);
var contentIsCleared = frameMgr.ContentIsCleared();
if (contentIsCleared) {
	setTimeout(PleaseWait, 500);
} else {
	frameMgr.ShowErrorMessage('" +UnexpectedErrorTitleHtml +"', '"+UnexpectedErrorMsgHtml +@"');
}
", ScriptOptions.DocumentReady);
	}
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER. 
     MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
     LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. THERE IS 
     NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
<html>
<head runat="server"/>
<BODY class="ErrorBody">
&nbsp;
</BODY>
</html>
