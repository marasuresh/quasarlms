<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateUserEditor.ascx.cs" Inherits="Tools_Administration_CreateUserEditor" %>
<%@ Register Src="../RegionEditControl.ascx" TagName="RegionEditControl" TagPrefix="uc1" %>
<%@ Register Src="RoleSelect.ascx" TagName="RoleSelect" TagPrefix="uc2" %>
<table width=100%>
<tr><td width=20%>
        Логін користувача
    </td><td>
        <asp:TextBox ID="loginTextBox" runat="server" Width=100%></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="loginTextBox"
            Display="Dynamic" ErrorMessage="Вкажіть логін"></asp:RequiredFieldValidator>
</td></tr>
<tr><td>
        Прізвище, Ім'я, По-батькові
    </td><td>
        <asp:TextBox ID="fullNameTextBox" runat="server" Width=100%></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="fullNameTextBox"
            Display="Dynamic" ErrorMessage="Вкажіть П.І.Б."></asp:RequiredFieldValidator>
</td></tr>
<tr><td>
        email
    </td><td>
        <asp:TextBox ID="emailTextBox" runat="server" Width=100%></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="loginTextBox"
            Display="Dynamic" ErrorMessage="Вкажіть email"></asp:RequiredFieldValidator>
</td></tr>
<tr><td>
        Пароль
    </td><td>
        <asp:TextBox ID="passwordTextBox" runat="server" Width=100%></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="loginTextBox"
            Display="Dynamic" ErrorMessage="Вкажіть пароль"></asp:RequiredFieldValidator>
</td></tr>
<tr><td>
        Регіон
    </td><td>
        <uc1:RegionEditControl ID="RegionEditControl1" runat="server" />
</td></tr>
<tr><td>
        Роль
    </td><td>
        <uc2:RoleSelect ID="RoleSelect1" runat="server" />
</td></tr>
</table>
<br />
<asp:Button ID="createButton" runat="server" Text="Створити" OnClick="createButton_Click" />
<asp:Button ID="cancelButton" runat="server" Text="Відмінити" OnClick="cancelButton_Click" />