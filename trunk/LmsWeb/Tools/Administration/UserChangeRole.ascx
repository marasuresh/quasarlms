<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserChangeRole.ascx.cs" Inherits="Administration_UserChangeRole" %>
<%@ Register Src="RoleSelect.ascx" TagName="RoleSelect" TagPrefix="uc1" %>
<uc1:RoleSelect ID="RoleSelect1" runat="server" />
&nbsp;<asp:Button ID="setRoleButton" runat="server" OnClick="setRoleButton_Click"
    Text="Призначити" />
