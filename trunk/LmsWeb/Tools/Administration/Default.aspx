<%@ Page 
    Language="C#" 
    MasterPageFile="~/Tools/Template.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="Administration_Default" 
    Title="Untitled Page" 
    Transaction="Required" 
%>
<%@ Register Src="~/Security/UserListWithSearch/SearchableUserList.ascx" TagName="SearchableUserList"
    TagPrefix="uc3" %>

<%@ Register Src="GroupList.ascx" TagName="GroupList" TagPrefix="uc2" %>

<%@ Register Src="UserList.ascx" TagName="UserList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <asp:HyperLink ID="usersHyperLink" runat="server" Font-Bold="True" NavigateUrl="~/Tools/Administration/UserList.aspx">Користувачі:</asp:HyperLink><br />
    <uc1:UserList ID="UserList1" runat="server" PageSize="5" />
    <br />
    <table width=100%><tr><td width=50% valign=top>
        <asp:Localize ID="Localize1" runat="server">Для групової зміни ролей чи підписа на тренінг, можуть використовуватися групи. Перелік груп ви маєте можливість редагувати та переглянути усіх користувачів цієї групи.</asp:Localize>
    </td><td width=50% valign=top>
        <asp:HyperLink ID="groupsHyperLink" runat="server" Font-Bold="True" NavigateUrl="~/Tools/Administration/Groups.aspx">Групи:</asp:HyperLink><br />
        <uc2:GroupList id="GroupList1" runat="server">
        </uc2:GroupList>
    </td></tr></table>
    <br />
    <asp:Button ID="synchronizeButton" runat="server" Text="Синхронізувати" OnClick="synchronizeButton_Click" />&nbsp;<asp:Button
        ID="createUserButton" runat="server" Text="Створити користувача" OnClick="createUserButton_Click" />
    
</asp:Content>

