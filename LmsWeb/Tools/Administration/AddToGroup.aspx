<%@ Page 
    Language="C#"
    MasterPageFile="~/Tools/Template.master"
    AutoEventWireup="true" 
    CodeFile="AddToGroup.aspx.cs" 
    Inherits="Administration_AddToGroup" 
    Title="Untitled Page" 
    Transaction="Required"
 %>
<%@ Register Src="AddToOtherGroupList.ascx" TagName="AddToOtherGroupList" TagPrefix="uc1" %>
<%@ Register Src="UserDetails.ascx" TagName="UserDetails" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc2:UserDetails ID="UserDetails1" runat="server" />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Додати до групи:"></asp:Label><br />
    <uc1:AddToOtherGroupList ID="AddToOtherGroupList1" runat="server" />
</asp:Content>

