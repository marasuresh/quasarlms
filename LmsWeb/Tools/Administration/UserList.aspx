<%@ Page 
    Language="C#" 
    MasterPageFile="~/Tools/Template.master" 
    AutoEventWireup="true" 
    CodeFile="UserList.aspx.cs" 
    Inherits="Administration_UserList" 
    Title="Untitled Page"
    Transaction="Required"
%>
<%@ Register Src="UserList.ascx" TagName="UserList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc1:UserList ID="UserList1" runat="server" PageSize="30" />
    <br />
    <br />
    <asp:Button ID="synchronizeButton" runat="server" Text="Синхронізувати" />
</asp:Content>

