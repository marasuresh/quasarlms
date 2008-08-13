<%@ Page 
    Language="C#" 
    MasterPageFile="~/Tools/Template.master" 
    AutoEventWireup="true" 
    CodeFile="AdvancedOfflineChange.aspx.cs" 
    Inherits="Tools_Administration_AdvancedOfflineChange" 
    Title="Untitled Page"
    Transaction="Required" 
%>
<%@ Register Src="UserDetails.ascx" TagName="UserDetails" TagPrefix="uc1" %>
<%@ Register Src="UserChangeOfflineControl.ascx" TagName="UserChangeOfflineControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc1:UserDetails ID="UserDetails1" runat="server" />
    <br />
    <br />
Параметри користувача для роботи у режимі Off-Line.<br />
Не змінюйте ці параметри, якщо система під'єднана до серверу <strong>LDAP</strong>
чи <strong>ИРБИС</strong>.<br />
<br />
    <uc2:UserChangeOfflineControl id="UserChangeOfflineControl1" runat="server">
    </uc2:UserChangeOfflineControl>
</asp:Content>

