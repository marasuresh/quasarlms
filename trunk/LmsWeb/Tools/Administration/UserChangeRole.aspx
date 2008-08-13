<%@ Page
    Language="C#"
    MasterPageFile="~/Tools/Template.master"
    AutoEventWireup="true"
    CodeFile="UserChangeRole.aspx.cs"
    Inherits="Administration_UserChangeRole"
    Title="Untitled Page"
    Transaction="Required" 
%>
<%@ Register Src="UserChangeRole.ascx" TagName="UserChangeRole" TagPrefix="uc1" %>
<%@ Register Src="UserDetails.ascx" TagName="UserDetails" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc2:UserDetails ID="UserDetails1" runat="server" />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Призначити на роль:"></asp:Label><br />
    <uc1:UserChangeRole ID="UserChangeRole1" runat="server" />
</asp:Content>

