<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyncUserBase.aspx.cs" Inherits="Tools_Administration_Irbis_SyncUserBase" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Синхронізація даних</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="titleLabel" runat="server" Font-Bold="True" Font-Size="Large" /><br />
        <asp:Button ID="startButton" runat="server" OnClick="startButton_Click" Text="Синхронізувати Базу Користувачів з LDAP та ИРБИС" /><br />
        <br />
        <asp:Label ID=logLabel runat=server/><br />
    
        <asp:TextBox ID="statusTextBox" runat="server" Width=100% ReadOnly="True"/>
<% PerformSync(); %>
    </div>
    </form>
</body>
</html>
