<%@ Page
	Language="C#"
%>
<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);

		Register.StyleSheet(this.Page, "Theme/Styles.css");
		Register.JQuery(this.Page);
		Register.JavaScript(this.Page, "Include/FramesetMgr.js");
		Register.JavaScript(this.Page, @"
var L_SaveAndClose_TXT = 'Save&nbsp;&amp; Close';
var L_SaveAndCloseTooltip_TXT = 'Save & Close';
	
var frameMgr = API_GetFramesetManager();
frameMgr.RegisterFrameLoad(TITLE_FRAME); 
$('#imgSaveAndClose').attr('title', L_SaveAndCloseTooltip_TXT);
$('#aSaveAndClose').html(L_SaveAndClose_TXT);
$('img#imgSaveAndClose, a#aSaveAndClose').click(function onSaveAndClose() {
	// Save and Close event handler. This clears the content frame and closes the frameset.
	frameMgr.IsClosing(true);
	frameMgr.ClearContentFrameAndPost('S', null);
	return false;
});

", ScriptOptions.DocumentReady);
	}
</script>
<!-- Copyright (c) Microsoft Corporation. All rights reserved. -->
<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER. 
     MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
     LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. THERE IS 
     NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
<html>
<head runat="server" />
<BODY class="ShellTitleServices">

	<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
		<tr valign="top">
			<td valign="top" align="left" width="100%" colspan="3">
				<img	id="topshadow"
						height="6"
						src="Theme/TopShadow.gif"
						width="100%"
						border="0"
						valign="top" />
			</td>
		</tr>
		<tr valign="top" height="100%">
			<td valign="middle" align="left" width="100%">
				<div	style="overflow-y: auto; height: 100%; padding-bottom: 4px; display: none"
						class="ShellTitleText"
						id="txtTitle">&nbsp;</div>
			</td>
			<td		valign="Middle"
					align="right"
					id="imgSaveAndCloseTd">
				<img	id="imgSaveAndClose"
						style="cursor: hand;"
						src="./Images/SaveAndClose.gif"
						onmouseout="document.getElementById('aSaveAndClose').style.textDecoration='none'"
						onmouseover="document.getElementById('aSaveAndClose').style.textDecoration='underline'" />
			</td>
			<td valign="bottom" align="right">
				<div	style='height: 100%; padding: 7px 16px 0px 4px'
						class="ShellSaveText">
					<a		href="#"
							id="aSaveAndClose"
							onmouseout="this.style.textDecoration='none'"
							onmouseover="this.style.textDecoration='underline'">&nbsp;</a></div>
			</td>
		</tr>
	</table>
</BODY>
</html>
