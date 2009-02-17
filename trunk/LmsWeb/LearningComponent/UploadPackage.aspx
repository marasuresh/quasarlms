<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadPackage.aspx.cs" Inherits="UploadPackage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
     MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
     LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
     NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
     
<head runat="server">

    <title>Upload Package</title>
	<script runat="server">
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Register.JQuery(this.Page);
			Register.JavaScript(this.Page, "UI/Js/Script.js");
		}
	</script>
    
    <script type="text/javascript" defer="defer">
    	DeletePackages_Main();
    </script>

    <link rel="stylesheet" href="Styles.css" type="text/css" />

    <style type="text/css">
    HTML
    {
        overflow: hidden;
    }
    BODY
    {
        height: 100%;
        margin: 0;
    }
    .MessagePanel .Content_
    {
        overflow-x: hidden; /* works best in IE */
    }
    </style>

</head>

<body >
        
    <form id="pageForm" target="ThisDialog" runat="server">
    
    <!-- panel that displays when this form is loaded -->
    <asp:Panel ID="UploadPanel" CssClass="UploadPanel" runat="server">
    
        <p class="InstructionHeader">1. Select a package (SCORM .zip file or Class Server .lrm file) to upload.</p>
        <p class="Indent"><asp:FileUpload ID="UploadedPackageFile" runat="server" Width="100%" /></p>
                
        <p class="InstructionHeader">2. Click the Upload button.</p>
        <p class="Center">
            <asp:Button ID="UploadPackageButton" runat="server" Text="Upload" OnClick="UploadPackageButton_OnClick"
                 OnClientClick="setTimeout(PleaseWait, 1)" />
            <button onclick="window.close()" style="margin-left: 8pt">Cancel</button>
        </p>

    </asp:Panel>

    <!-- panel that displays to provide feedback after the upload operation -->
    <asp:Panel ID="MessagePanel" CssClass="MessagePanel" visible="false" runat="server">
        
        <asp:Panel ID="WarningIntro" CssClass="Top_" visible="false" runat="server">
			<!-- panel used to display a list of warnings -->
            The package was uploaded, but the following problem(s) were encountered:
        </asp:Panel>
        <asp:Panel ID="WarningMessages" CssClass="Content_" visible="false" runat="server">
            <asp:BulletedList ID="ScrollingMessagesList" CssClass="List_" BulletStyle="Numbered" runat="server" />
		</asp:Panel>
		
        <asp:Panel ID="ErrorIntro" CssClass="Top_" visible="false" runat="server">
			<!-- panel used to display an error message -->
            The package could not be uploaded due to the following problem(s):
        </asp:Panel>
        <asp:Panel ID="ErrorMessage" CssClass="Content_" Style="height: 60%; padding: 8pt" visible="false" runat="server" >
            <asp:BulletedList ID="ErrorMessageScrollingList" CssClass="List_" BulletStyle="circle" runat="server" />
         </asp:Panel>

        <!-- buttons at the bottom -->
        <asp:Panel ID="Buttons" CssClass="Bottom_" runat="server">
			<button onclick="window.close()" style="width: 60pt">OK</button>
        </asp:Panel>

        <!-- hidden table containing the results of the upload, for transfer to the main page via JScript -->
        <asp:Table ID="TrainingGrid" CssClass="Grid Hidden" runat="server" />

    </asp:Panel>
        
    <asp:Literal ID="UpdateParentPageScript" Visible="false" runat="server">
    <script type="text/javascript" defer="defer">
    	DeletePackages_UpdateParentPageScript();
    </script>
    </asp:Literal>
        
    <asp:Literal ID="CloseDialogScript" Visible="false" runat="server">
    <script type="text/javascript" defer="true">
    	DeletePackages_CloseDialogScript();
    </script>
    </asp:Literal>

    </form>

</body>

</html>
