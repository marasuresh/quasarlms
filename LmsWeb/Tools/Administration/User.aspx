<%@ Page 
    Language="C#"
    MasterPageFile="~/Tools/Template.master"
    AutoEventWireup="true"
    CodeFile="User.aspx.cs"
    Inherits="Administration_User"
    Title="Untitled Page"
    Transaction="Required"
%>
<%@ Register Src="UserRolesList.ascx" TagName="UserRolesList" TagPrefix="uc2" %>
<%@ Register Src="UserDetails.ascx" TagName="UserDetails" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
<table width=100%><tr><td width=90% valign=top>
    <uc1:UserDetails ID="UserDetails1" runat="server" />
</td><td valign=top>
    <asp:Button ID="irbisSynchronizeButton" runat="server" Text="Синхронізувати" Width="100%" OnClick="irbisSynchronizeButton_Click" /><br />
    <br />
    <asp:Button ID="changeRoleButton" runat="server" OnClick="changeRoleButton_Click"
        Text="Змінити роль" Width="100%" /></td></tr>
</table>
    <br />
    <table width=100%><tr><td>
        <asp:Label ID="memberOfLabel" runat="server" Text="Зараховано до:"></asp:Label>
    </td><td align=right>
        <asp:Button ID="includeToOtherGroupButton" runat="server" Text="Додати до іншої групи..." OnClick="includeToOtherGroupButton_Click" />
    </td></tr></table>

    <uc2:UserRolesList ID="UserRolesList1" runat="server" />
    
    <br />
    <br />
    <asp:Button ID="advancedOfflineButton" runat="server" Text="Додаткові налаштунки..." OnClick="advancedOfflineButton_Click" />

</asp:Content>

