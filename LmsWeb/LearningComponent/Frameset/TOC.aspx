<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>
<%@ Page
	Language		= "C#"
	Inherits		= "Microsoft.LearningComponents.Frameset.Frameset_TOC"
%>
<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);

		Register.StyleSheet(this.Page, "Theme/Styles.css");
		Register.JQuery(this.Page);
		
		new[] { "FramesetMgr", "Toc", "vernum" }.ToList()
			.ForEach(_s => Register.JavaScript(this.Page, "Include/"+_s+".js"));

		Register.JavaScript(this.Page, @"
g_currentActivityId = null;
g_previousActivityId = null;
g_frameMgr = API_GetFramesetManager();
// Tell frameMgr to call back when current activity changes
g_frameMgr.ShowActivityId = SetCurrentElement;
g_frameMgr.ResetActivityId = ResetToPreviousSelection;

// Tell frameMgr to call back with TOC active / inactive state changes
g_frameMgr.SetTocNodes = SetTocNodes;

// Register with framemanager that loading is complete
g_frameMgr.RegisterFrameLoad(TOC_FRAME);
document.body.onclick = body_onclick;
", ScriptOptions.DocumentReady);
	}
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER. 
     MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
     LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. THERE IS 
     NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
<head runat="server" />
<body class='NavBody'>
	<div id="divMain" style="visibility: hidden; margin: 5px">
		<div nowrap="nowrap">
			<!-- <p class="NavClosedPreviousBtnGrphic">&nbsp;</p> -->
			<%=this.GetToc() %>
		</div>
	</div>

	<script type="text/javascript" defer="true">

		// If the version of the page differs from the version of the script, don't render
		if ("<%=TocVersion %>" != JsVersion()) {
			document.writeln("<%=InvalidVersionHtml %>");
		}
	</script>

</body>
</html>
