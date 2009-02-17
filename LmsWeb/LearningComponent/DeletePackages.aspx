<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeletePackages.aspx.cs" Inherits="DeletePackages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
     MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
     LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
     NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
     
<head id="Head1" runat="server">

    <title>Delete Packages</title>

    <script type="text/javascript" defer="defer">
    	DeletePackages_Main();
    </script>
    
<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Register.JQuery(this.Page);
		Register.StyleSheet(this.Page, "UI/Css/Styles.css");
		Register.StyleSheet(this.Page, "UI/Css/CreateAttempt.css");
		Register.JavaScript(this.Page, "UI/Js/Script.js");
	}
</script>

</head>

<body>
    
    <form id="pageForm" target="ThisDialog" runat="server">
    
    <asp:Panel ID="MessagePanel" CssClass="MessagePanel" runat="server">

        <!-- panel that displays when this form is loaded -->
        <asp:Panel ID="ConfirmMessage" CssClass="FixedMessage_" runat="server">
		    <table style="height: 90%; width: 100%;">
		        <tr style="vertical-align: bottom">
                    <td style="text-align: center">Are you sure you want delete the selected package(s)?</td>
			    </tr>
			</table>
		</asp:Panel>
        
		<!-- panel for displaying an error message -->
        <asp:Panel ID="ErrorIntro" CssClass="Top_" visible="false" runat="server">
            The package(s) could not be deleted due to the following problem:
        </asp:Panel>
        <asp:Panel ID="ErrorMessage" CssClass="Content_" visible="false" Style="" runat="server" />

        <!-- buttons at the bottom, in either case -->
        <asp:Panel ID="Buttons" CssClass="Bottom_" runat="server">
            <asp:Button ID="DeletePackagesButton" Text="OK" CssClass="ActionButton" OnClick="DeletePackagesButton_Click"
                OnClientClick="setTimeout(PleaseWait, 1)" runat="server" />
            <asp:Button ID="CloseButton" Text="Cancel" CssClass="ActionButton" OnClientClick="window.close(); return false;" runat="server" />
        </asp:Panel>

    </asp:Panel>
    
    <asp:HiddenField ID="PackagesToDelete" runat="server" />
    <asp:HiddenField ID="PackagesSuccessfullyDeleted" runat="server" />

    </form>
    
    <script type="text/javascript">
    
        // initialize the PackagesToDelete hidden field with the
		// comma-separated list of packages to delete from the parent page
        $('form#pageForm #PackagesToDelete').val(dialogArguments.PackagesToDelete.get());

    </script>

    <asp:Literal ID="UpdateParentPageScript" Visible="false" runat="server">
    <script type="text/javascript" defer="defer">
    	DeletePackages_UpdateParentPageScript();
    </script>
    </asp:Literal>
        
    <asp:Literal ID="CloseDialogScript" Visible="false" runat="server">
    <script type="text/javascript" defer="defer">
    	DeletePackages_CloseDialogScript();
    </script>
    </asp:Literal>

</body>

</html>

