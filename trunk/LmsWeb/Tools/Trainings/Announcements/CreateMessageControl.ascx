<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateMessageControl.ascx.cs" Inherits="Trainings_Announcements_CreateMessageControl" %>
<table width=100%>
<tr>
    <td>Дата</td>
    <td><asp:Calendar ID="dateCalendar" runat="server"></asp:Calendar></td> 
</tr>
<tr>
    <td>Автор</td>
    <td><asp:Label ID="authorLabel" runat="server"/></td> 
</tr>
<tr>
    <td valign=top>Оголошення</td>
    <td valign=top><asp:TextBox ID="messageTextBox" runat="server" Width=100% Height=20em/></td> 
</tr>
<tr>
    <td colspan=2>
        <asp:Button ID="createButton" runat="server" Text="Створити" OnClick="createButton_Click" />
        <asp:Button ID="cancelButton" runat="server" Text="Відмінити" OnClick="cancelButton_Click" /></td>
</tr>
</table>