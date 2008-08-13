<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserChangeOfflineControl.ascx.cs" Inherits="Tools_Administration_UserChangeOfflineControl" %>
<%@ Register Src="../RegionEditControl.ascx" TagName="RegionEditControl" TagPrefix="uc2" %>
<table width=100%><tr><td width=90%>
    <asp:TextBox ID="passwordTextBox" TextMode=Password runat="server" Width=100%></asp:TextBox>
</td><td align=right>
    <asp:Button ID="changePasswordButton" runat="server" Text="Змінити пароль" OnClick="changePasswordButton_Click" />
</td></tr><tr><td colspan=2>
    <asp:Label ID="passwordChangedLabel" runat="server" ForeColor="Red">Пароль змінено</asp:Label>
</td></tr></table>
<br />
<br />
<table width=100%><tr><td width=90%>
    <uc2:RegionEditControl ID="RegionEditControl1" runat="server" />
</td><td align=right>
    <asp:Button ID="changeRegionButton" runat="server" Text="Змінити регіон" OnClick="changeRegionButton_Click" />
</td></tr><tr><td colspan=2>
    <asp:Label ID="regionChangeLabel" runat="server" ForeColor="Red">Регіон змінено</asp:Label>
</td></tr></table>
