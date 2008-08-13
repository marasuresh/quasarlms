<%@ Page
    Language="C#"
    MasterPageFile="~/Tools/Template.master"
    AutoEventWireup="true"
    CodeFile="Create.aspx.cs"
    Inherits="Trainings_Announcements_Create"
    Title="Untitled Page" %>
<%@ Register Src="CreateMessageControl.ascx" TagName="CreateMessageControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc1:CreateMessageControl ID="CreateMessageControl1" runat="server" />
</asp:Content>

