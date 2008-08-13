<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DebugDumpReport.aspx.cs" Inherits="StudentReports_DebugDumpReport" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Tables[<asp:Label ID="tableCountLabel" runat="server" Text="Label"></asp:Label>] 
        <br />
        <asp:Label ID="detailsLabel" runat="server" Text="Label"></asp:Label>        
        <br />
        <br />
    
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    
    </div>
    </form>
</body>
</html>
