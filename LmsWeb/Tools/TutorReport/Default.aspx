<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_TutorReport_Default" Title="Звіт Щоденник Тютора" %>
<%@ Register Src="~/StudentReports/ReportTableControl.ascx" TagName="ReportTableControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <table width="100%" class="printer-invisible"><tr><td>
        <!--<asp:Button ID="printButton" runat="server" Text="До друку..." OnClick="printButton_Click" />-->
    </td><td align=right>
        <asp:Button ID="departmentGroupButton" runat="server" Text="За органiзацiйною структурою" OnClick="departmentGroupButton_Click" /> 
    </td></tr></table>
    
    <uc1:ReportTableControl ID="ReportTableControl1" runat="server" ShowAllStudents="true" />
</asp:Content>

