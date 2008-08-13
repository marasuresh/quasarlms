<%@ Page 
    Language="C#" 
    MasterPageFile="~/Tools/Template.master" 
    AutoEventWireup="true" 
    CodeFile="MultiAddToGroup.aspx.cs" 
    Inherits="Administration_MultiAddToGroup" 
    Title="Untitled Page"
    Transaction="Required" 
%>
<%@ Register Src="UserList.ascx" TagName="UserList" TagPrefix="uc1" %>
<%@ Register Src="MultiUserList.ascx" TagName="MultiUserList" TagPrefix="uc2" %>
<%@ Register Src="MultiAddToGroupControl.ascx" TagName="MultiAddToGroupControl" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <uc2:MultiUserList ID="MultiUserList1" runat="server" />
    <br />
    <asp:Label ID="addUsersToGroupLabel" runat="server" Text="Додати до групи:"></asp:Label><br />
    <uc3:MultiAddToGroupControl ID="MultiAddToGroupControl1" runat="server" />
</asp:Content>

