<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="ACalendar_UI_Test" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:ListView ID="_eventList" runat="server" DataSourceID="EntityDataSource1">
    </asp:ListView>
 
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 
    <div style="background-color: #0000FF">
    </div>

        <input id="Hidden1" type="hidden" value="456789" />

    <asp:TextBox ID="TextBox1" runat="server" Visible="true" Width="1px"></asp:TextBox>
 
    </form>
    
</body>
</html>
