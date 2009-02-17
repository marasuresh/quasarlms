<%@ Page Language="C#" AutoEventWireup="true" Inherits="Microsoft.LearningComponents.Frameset.Frameset_Frameset" %>
<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>
<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Register.JQuery(this.Page);
		Register.JavaScript(this.Page, "~/Lms/UI/Js/jquery.debug.js");
	}

	protected override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		if (!ShowError) {// don't write script if there is an error on the page
			// if this package does not require the RTE, then don't write the links?
			new[]{
				"Rte1p2Api.js",
				"parser1p2.js",
				"typevalidators1p2.js",
				"Rte2004Api.js",
				"parser.js",
				"typevalidators.js",
				"RteApiSite.js",
				"FramesetMgr.js"
			}.ToList()
			.ForEach(_s => {
				Register.JavaScript(this.Page, "~/LearningComponent/Frameset/Include/" + _s);
			});

			Register.JavaScript(this.Page, @"
// debugger;

    // Constants
    var SCORM_2004 = 'V1p3';
    var SCORM_12 = 'V1p2';

    // FrameMgr is called from all frames
    var g_frameMgr = new FramesetManager();
    g_frameMgr.DebugLog = function(s) {
		$.log(s);
    };

    // TODO (M2): The following code is only required if the package is SCORM
    var g_scormVersion = '" + this.ScormVersionHtml + @"';	// Version of current session

    var API_1484_11 = null; // Name of RTE object for 2004 -- name is determined by SCORM.
    var API = null; // Name of RTE object for 1.2 -- name is determined by SCORM

    // Internal RTE object -- it's the same object as the api objects, just easier to reference.
    var g_API = g_frameMgr.GetRteApi(g_scormVersion, '" + this.RteRequired + @"');  

    if (g_scormVersion == SCORM_2004)
    {
        API_1484_11 = g_API;
    }
    else
    {
        API  = g_API;
    }", ScriptOptions.ScriptTags);

		}
	}
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
     MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
     LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
     NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
<head runat="server">
    <title><%=PageTitleHtml%></title>
    <LINK rel="stylesheet" type="text/css" href="Theme/Styles.css" />



</head>
<% if (ShowError)
   {  %>
   <body class="ErrorBody">

        <table border="0" width="100%" id="table1" style="border-collapse: collapse">
	    <tr>
		    <td width="60">
		    <p align="center">
		    <img border="0" src="./Theme/Error.gif" width="49" height="49"></td>
		    <td class="ErrorTitle"><% =ErrorTitle %></td>
	    </tr>
	    <tr>
		    <td width="61">&nbsp;</td>
		    <td><hr></td>
	    </tr>
	    <tr>
		    <td width="61">&nbsp;</td>
		    <td class="ErrorMessage"><% =ErrorMsg %></td>
	    </tr>
        </table>

    </body>
   <%} else   // no error, so show frameset	
   { %>
<FRAMESET id="framesetOuter" name="framesetOuter" rows="53,*" framespacing="0" frameborder="0" border="0">

   <FRAME class="frameTitle" name="frameTitle" src="Title.htm" id="frameTitle" 
                    marginwidth="0" marginheight="0" framespacing="0" noresize="noresize" scrolling="no" frameborder="0" />

   <FRAME class="ShellFrame" id="frameLearnTask" name="frameLearnTask" src="<% =MainFramesUrl %>" 
                scrolling="no" marginwidth="0" marginheight="0" framespacing="0" frameborder="0" />

</FRAMESET>
<%} %>
</html>
