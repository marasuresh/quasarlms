<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StudentSubControl.ascx.cs" Inherits="Tools_DepartmentReports_StudentSubControl" %>
<%@ Reference Control="~/Tools/DepartmentReports/CourseSubControl.ascx" %>
<%@ Register Src="~/StudentReports/IndentControl.ascx" TagName="IndentControl" TagPrefix="uc1" %>
<%@ Register Src="~/StudentReports/ExpandCollapse.ascx" TagName="ExpandCollapse" TagPrefix="uc2" %>
<tr><td nowrap=nowrap>
    <uc1:IndentControl ID="leadIndentControl" runat="server" />
    <uc2:ExpandCollapse ID="expandCollapse" runat="server" OnClick="expandCollapse_Click" />
    <asp:Label ID="studentNameLabel" runat="server"/>
</td><!-- калькуляции -->
    <th> <asp:Label ID="dateLabel" runat="server" /> </th>
    <th><asp:Label ID="tryCountLabel" runat="server" /></th>
    <th><asp:Label ID="questionCountLabel" runat="server" /></th>    
    <th><asp:Label ID="averageRequiredPointsLabel" runat="server" /></th>
    <th><asp:Label ID="averagePointsLabel" runat="server" /></th>
    <th><asp:Label ID="averageRightAnswerPercentLabel" runat="server" /></th>
</tr>
<asp:PlaceHolder ID="coursesPlaceHolder" runat="server"/>
