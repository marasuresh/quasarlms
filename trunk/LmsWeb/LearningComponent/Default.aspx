<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Import Namespace="N2.Resources" %>
<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>
<script runat="server">
	
protected override void  OnInit(EventArgs e)
{
	Register.JQuery(this.Page);
	Register.JavaScript(this.Page, "UI/Js/Script.js");
	Register.StyleSheet(this.Page, "Styles.css");
	Register.StyleSheet(this.Page, "UI/Css/Styles.css");
	base.OnInit(e);
}
	
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
     MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
     LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
     NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>BasicWebPlayer</title>
</head>

<body style="overflow:auto">
    
    <form id="pageForm" runat="server">

        <!-- title panel -->
        <asp:Panel ID="Title" CssClass="Title" runat="server">
			<table style="width: 100%">
				<tr>
					<td class="Left_">My Training (<asp:Label ID="MyUserName" runat="server" Text="Label" />)</td>
					<td class="Right_"><asp:Label ID="VersionNumber" runat="server" /></td>
				</tr>
			</table>
        </asp:Panel>

        <!-- action links -->
        <asp:Panel ID="Nav" CssClass="Nav" runat="server">
        <a href="javascript:UploadPackage()">Upload Training Package</a>
        <span class="Sep">|</span>
        <asp:HyperLink ID="DeletePackagesLink" NavigateUrl="javascript:DeletePackages()" runat="server">Delete Selected Packages</asp:HyperLink>
        </asp:Panel>

        <asp:Panel ID="TrainingPanel" runat="server">
            <!-- table of this user's training -->
            <asp:Table ID="TrainingGrid" CssClass="Grid" runat="server">
                <asp:TableHeaderRow CssClass="Header_">
                    <asp:TableCell CssClass="Select_"><input id="SelectAll" type="checkbox" title="Select All" onclick="OnSelectAllClicked()" /></asp:TableCell>
                    <asp:TableCell CssClass="Name_">Name</asp:TableCell>
                    <asp:TableCell CssClass="Uploaded_">Uploaded</asp:TableCell>
                    <asp:TableCell CssClass="Status_">Status</asp:TableCell>
                    <asp:TableCell CssClass="Score_">Score</asp:TableCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </asp:Panel>

        <asp:Panel ID="NoTrainingMessage" runat="server">
            <!-- message that's displayed if the TrainingGrid is empty (and is therefore hidden) -->
            <asp:Table ID="NoTrainingMessageTable" CssClass="MessageTable" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <p>Click <span style="font-weight: bold">Upload Training Package </span> above to upload a training package.</p>
                        <p>(Training packages can be SCORM 2004, SCORM 1.2, Class Server LRM,<br/> or Class Server IMS+ format.)</p>
				    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>

    </form>

</body>

</html>
