<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Tools_TutorReport_Print" %>
<%@ Register Src="../../StudentReports/ReportTableControl.ascx" TagName="ReportTableControl"
    TagPrefix="uc1" %>
<html>
<head runat="server">
    <title>Звіт Щоденник Тютора</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:ReportTableControl ID="ReportTableControl1" runat="server" ShowAllStudents="true" ShowFilter="false" />
    
    </div>
    </form>
</body>
</html>
