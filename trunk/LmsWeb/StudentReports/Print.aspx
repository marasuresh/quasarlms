<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="StudentReports_Print" %>

<%@ Register Src="ReportTableControl.ascx" TagName="ReportTableControl" TagPrefix="uc1" %>
<html>
<head runat="server">
    <title>Щоденник слухача</title>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:ReportTableControl ID="ReportTableControl1" runat="server" ShowFilter="false" />
    </form>
</body>
</html>
