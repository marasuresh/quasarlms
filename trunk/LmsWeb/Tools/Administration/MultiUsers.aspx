<%@ Page
    Language="C#"
    MasterPageFile="~/Tools/Template.master"
    AutoEventWireup="true"
    CodeFile="MultiUsers.aspx.cs"
    Inherits="Administration_MultiUsers"
    Title="Untitled Page"
    Transaction="Required" 
%>
<%@ Register Src="MultipleUsersModify.ascx" TagName="MultipleUsersModify" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc1:MultipleUsersModify ID="MultipleUsersModify1" runat="server" />
</asp:Content>

