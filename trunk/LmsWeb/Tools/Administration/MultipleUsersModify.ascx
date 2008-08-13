<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultipleUsersModify.ascx.cs" Inherits="Administration_MultipleUsersModify" %>
<%@ Register Src="MultiUserList.ascx" TagName="MultiUserList" TagPrefix="uc1" %>
<table width=100%><tr><td valign=top width=90%>
    <uc1:MultiUserList ID="MultiUserList1" runat="server" />
</td><td valign=top align=center>
    <asp:Button ID="synchronizeIrbisButton" runat="server" Text="Синхронізувати" Width="100%" OnClick="synchronizeIrbisButton_Click" /><br />
    <br />
    <asp:Button ID="addToGroupButton" runat="server" Text="Додати до групи..." OnClick="addToGroupButton_Click" Width="100%" /><br />
    <br />
    <asp:Button ID="removeFromGroupButton" runat="server" Text="Виключити з групи..." OnClick="removeFromGroupButton_Click" Width="100%" /><br />
    <br />
    <asp:Button ID="applyRoleButton" runat="server" OnClick="applyRoleButton_Click" Text="Призначити на роль..."
        Width="100%" />
</td></tr></table>

