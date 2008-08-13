<%@ Control
		Language="C#"
		AutoEventWireup="true"
		CodeFile="CourseSubControl.ascx.cs"
		Inherits="StudentReports_CourseSubControl"
		ClassName="CourseSubControl" %>
<%@ Reference Control="~/StudentReports/ThemeSubControl.ascx" %>
<%@ Reference Control="~/StudentReports/TestSubControl.ascx" %>
<%@ Register Src="IndentControl.ascx" TagName="IndentControl" TagPrefix="uc1" %>
<%@ Register Src="ExpandCollapse.ascx" TagName="ExpandCollapse" TagPrefix="uc2" %>
<tr><td nowrap=true>
    <uc1:IndentControl ID="leadIndentControl" runat="server"/>
    <uc2:ExpandCollapse ID="expandCollapse" runat="server" OnClick="expandCollapse_Click" />
    <asp:Label ID="courseNameLabel" runat="server"/>
</td><!-- калькуляции -->
    <th> <asp:Label ID="dateLabel" runat="server" /> </th>
    <th><asp:Label ID="tryCountLabel" runat="server" /></th>
    <th><asp:Label ID="questionCountLabel" runat="server" /></th>    
    <th><asp:Label ID="averageRequiredPointsLabel" runat="server" /></th>
    <th><asp:Label ID="averagePointsLabel" runat="server" /></th>
    <th><asp:Label ID="averageRightAnswerPercentLabel" runat="server" /></th>
</tr>
<asp:PlaceHolder ID="childThemesPlaceHolder" runat="server"/>
<asp:PlaceHolder ID="childTestsPlaceHolder" runat="server"/>