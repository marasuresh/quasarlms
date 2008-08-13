<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateTaskDetailsControl.ascx.cs" Inherits="Tools_Trainings_Tasks_CreateTaskDetailsControl" %>
<table width=100%>
<tr><td width=20%>
    Назва
</td>
<td>
    <asp:TextBox ID="nameTextBox" runat="server" Width=100%></asp:TextBox>
</td></tr>
<tr><td width=20% valign=top>
    Текст
</td>
<td valign=top>
    <asp:TextBox ID="contentTextBox" runat="server" Width=100% Height=20em TextMode=MultiLine></asp:TextBox>
</td></tr>
</table>
<asp:Button ID="createButton" runat="server" OnClick="createButton_Click" Text="Створити" />&nbsp;
<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Вiдмiнити" />