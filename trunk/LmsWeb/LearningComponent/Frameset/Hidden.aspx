<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>
<%@ Page
	Language		= "C#"
	Inherits		= "Microsoft.LearningComponents.Frameset.Frameset_Hidden"
	ValidateRequest	= "false"
%>
<script runat="server">
	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		Register.JQuery(this.Page);
		Register.JavaScript(this.Page, "Include/FramesetMgr.js");
		Register.JavaScript(this.Page, @"
// Get frameset manager
var frameMgr = API_GetFramesetManager();
// Set data on frameset manager
"
+ this.GetFrameMgrInit()
+ @"
// Register with framemanager that loading is complete
frameMgr.RegisterFrameLoad(HIDDEN_FRAME);
", ScriptOptions.DocumentReady);
	}
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
     MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
     LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
     NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
     
<head runat="server"/>
<body>
    <form id="form1" runat="server">
    <div>
    <% WriteHiddenControls(); %>
    </div>
    </form>
</body>
</html>
