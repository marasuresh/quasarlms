<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestResultSubControl.ascx.cs" Inherits="StudentReports_TestResultSubControl" %>
<%@ Reference Control="TestAnswerSubControl.ascx" %>
<%@ Register Src="IndentControl.ascx" TagName="IndentControl" TagPrefix="uc1" %>
<%@ Register Src="ExpandCollapse.ascx" TagName="ExpandCollapse" TagPrefix="uc2" %>
<tr><td nowrap=nowrap>
    <table cellpadding=0 cellspacing=0><tr><td nowrap=nowrap valign=top>
    <uc1:IndentControl ID="leadIndentControl" runat="server"/>
    <uc2:ExpandCollapse ID="expandCollapse" runat="server" OnClick="expandCollapse_Click" />
   </td><td valign=top>
    <asp:Label ID="studentLabel" runat="server"/>
    <span ID="moreStudentElement" runat=server style="font-size: 80%"></span>
   </td></tr>
   </table> 
    
</td><!-- калькуляции -->
    <th> <asp:Label ID="dateLabel" runat="server" /> </th>
    <th><asp:Label ID="tryCountLabel" runat="server" /></th>
    <th><asp:Label ID="questionCountLabel" runat="server" /></th>    
    <th><asp:Label ID="averageRequiredPointsLabel" runat="server" /></th>
    <th><asp:Label ID="averagePointsLabel" runat="server" /></th>
    <th><asp:Label ID="averageRightAnswerPercentLabel" runat="server" /></th>
</tr>
<asp:PlaceHolder ID="answersPlaceHolder" runat="server"/>