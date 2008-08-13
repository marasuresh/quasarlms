<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintDepartmentStruct.aspx.cs" Inherits="Tools_TutorReport_PrintDepartmentStruct" %>

<%@ Register Src="../DepartmentReports/ReportTableControl.ascx" TagName="ReportTableControl"
    TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Звіт Щоденник Тютора за органiзацiйною структурою</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:ReportTableControl ID="ReportTableControl1" runat="server" ShowFilter="false" />
    
    </div>
    </form>
</body>
</html>
