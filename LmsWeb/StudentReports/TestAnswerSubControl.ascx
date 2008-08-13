<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestAnswerSubControl.ascx.cs" Inherits="StudentReports_TestAnswerSubControl" %>
<%@ Register Src="IndentControl.ascx" TagName="IndentControl" TagPrefix="uc1" %>
<%@ Register Src="ExpandCollapse.ascx" TagName="ExpandCollapse" TagPrefix="uc2" %>
<tr><td>
    <uc1:IndentControl ID="leadIndentControl" runat="server"/>
    <asp:Label ID="answerLabel" runat="server"/>
</td><!-- калькуляции -->
    <th> <asp:Label ID="dateLabel" runat="server" /> </th>
    <th><asp:Label ID="tryCountLabel" runat="server" /></th>
    <th><asp:Label ID="questionCountLabel" runat="server" /></th>    
    <th><asp:Label ID="averageRequiredPointsLabel" runat="server" /></th>
    <th><asp:Label ID="averagePointsLabel" runat="server" /></th>
    <th><asp:Label ID="averageRightAnswerPercentLabel" runat="server" /></th>
</tr>
