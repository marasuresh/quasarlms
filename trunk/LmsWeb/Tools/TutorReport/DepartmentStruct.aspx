<%@ Page 
    Language="C#"
    MasterPageFile="~/Tools/Template.master"
    AutoEventWireup="true"
    CodeFile="DepartmentStruct.aspx.cs"
    Inherits="Tools_TutorReport_DepartmentStruct"
    Title="Звіт Щоденник Тютора за структурою" %>
<%@ Register Src="../DepartmentReports/ReportTableControl.ascx" TagName="ReportTableControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <table width="100%">
        <tr>
            <td>
                <asp:Button ID="printButton" runat="server" Text="До друку..." OnClick="printButton_Click" />
            </td>
            <td align="right">
                <asp:Button ID="departmentGroupButton" runat="server" Text="За курсами" OnClick="departmentGroupButton_Click" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <uc1:ReportTableControl ID="ReportTableControl1" runat="server" />
</asp:Content>

