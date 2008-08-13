<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CourseDomainSubControl.ascx.cs" Inherits="StudentReports_CourseDomainSubControl" %>
<%@ Reference Control="~/StudentReports/CourseSubControl.ascx" %>
<%@ Register Src="IndentControl.ascx" TagName="IndentControl" TagPrefix="uc1" %>
<%@ Register Src="ExpandCollapse.ascx" TagName="ExpandCollapse" TagPrefix="uc2" %>
<tr><td nowrap=true>
    <uc1:IndentControl ID="leadIndentControl" runat="server" />
    <uc2:ExpandCollapse ID="expandCollapse" runat="server" OnClick="expandCollapse_Click" />
    <asp:Label ID="domainNameLabel" runat="server"/>
</td><!-- калькуляции -->
    <th> <asp:Label ID="dateLabel" runat="server" /> </th>
    <th><asp:Label ID="tryCountLabel" runat="server" /></th>
    <th><asp:Label ID="questionCountLabel" runat="server" /></th>    
    <th><asp:Label ID="averageRequiredPointsLabel" runat="server" /></th>
    <th><asp:Label ID="averagePointsLabel" runat="server" /></th>
    <th><asp:Label ID="averageRightAnswerPercentLabel" runat="server" /></th>
</tr>
<asp:PlaceHolder ID="childDomainsPlaceHolder" runat="server"/>
<asp:PlaceHolder ID="childCoursesPlaceHolder" runat="server"/>
