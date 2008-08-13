<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultiUserChangeRole.ascx.cs" Inherits="Administration_MultiUserChangeRole" %>
<%@ Register Src="RoleSelect.ascx" TagName="RoleSelect" TagPrefix="uc1" %>
<uc1:RoleSelect ID="RoleSelect1" runat="server" />
&nbsp;<asp:Button ID="setRoleButton" runat="server" OnClick="setRoleButton_Click"
    Text="Встановити роль" />
