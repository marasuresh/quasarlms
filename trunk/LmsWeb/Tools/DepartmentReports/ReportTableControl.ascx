<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportTableControl.ascx.cs" Inherits="Tools_DepartmentReports_ReportTableControl" %>
<%@ Register Src="~/StudentReports/ReportFilterControl.ascx" TagName="ReportFilterControl" TagPrefix="uc1" %>
<%@ Reference Control="RegionSubControl.ascx" %>
<uc1:ReportFilterControl ID="ReportFilterControl1" runat="server" />
<table width=100% border=1 style="font-size: 90%">
<tr>
    <th><asp:LinkButton ID="nameSortLink" runat="server" OnClick="nameSortLink_Click">Назва</asp:LinkButton></th>
    
    <th><asp:LinkButton ID="dateSortLink" runat="server" OnClick="dateSortLink_Click">Дата</asp:LinkButton></th>
    <th><asp:LinkButton ID="tryCountSortLink" runat="server" OnClick="tryCountSortLink_Click">Кількість спроб</asp:LinkButton></th>
    <th><asp:LinkButton ID="questionCountSortLink" runat="server" OnClick="questionCountSortLink_Click">Кількість питань</asp:LinkButton></th>    
    <th><asp:LinkButton ID="requiredPointsSortLink" runat="server" OnClick="requiredPointsSortLink_Click">Прохідний бал</asp:LinkButton></th>
    <th><asp:LinkButton ID="collectedPointsSortLink" runat="server" OnClick="collectedPointsSortLink_Click">Набрані бали</asp:LinkButton></th>
    <th><asp:LinkButton ID="answerPercentLink" runat="server" OnClick="answerPercentLink_Click">Відсоток правильних відповідей</asp:LinkButton></th>
</tr>
    <asp:PlaceHolder ID="regionsPlaceHolder" runat="server"/>
</table>
