<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegionSubControl.ascx.cs" Inherits="Tools_DepartmentReports_RegionSubControl" %>
<%@ Reference Control="~/Tools/DepartmentReports/DepartmentSubControl.ascx" %>
<%@ Register Src="~/StudentReports/IndentControl.ascx" TagName="IndentControl" TagPrefix="uc1" %>
<%@ Register Src="~/StudentReports/ExpandCollapse.ascx" TagName="ExpandCollapse" TagPrefix="uc2" %>
<tr><td nowrap=nowrap>
    <uc1:IndentControl ID="leadIndentControl" runat="server" />
    <uc2:ExpandCollapse ID="expandCollapse" runat="server" OnClick="expandCollapse_Click" />
    <asp:Label ID="regionNameLabel" runat="server"/>
</td><!-- калькуляции -->
    <th> <asp:Label ID="dateLabel" runat="server" /> </th>
    <th><asp:Label ID="tryCountLabel" runat="server" /></th>
    <th><asp:Label ID="questionCountLabel" runat="server" /></th>    
    <th><asp:Label ID="averageRequiredPointsLabel" runat="server" /></th>
    <th><asp:Label ID="averagePointsLabel" runat="server" /></th>
    <th><asp:Label ID="averageRightAnswerPercentLabel" runat="server" /></th>
</tr>
<asp:PlaceHolder ID="childDepartmentsPlaceHolder" runat="server"/>
