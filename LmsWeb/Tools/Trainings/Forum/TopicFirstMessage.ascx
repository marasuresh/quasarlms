<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopicFirstMessage.ascx.cs" Inherits="Trainings_Forum_TopicFirstMessage" %>
<asp:DetailsView ID="FirstMessageDetailsView" runat="server" AutoGenerateRows="False"
    DataKeyNames="ID" DataSourceID="FirstMessageDataSource" Width="100%" meta:resourcekey="FirstMessageDetailsViewResource1">
    <Fields>
        <asp:BoundField DataField="StudentFullName" HeaderText="Author" ReadOnly="True" SortExpression="StudentFullName" meta:resourcekey="BoundFieldResource1" />
        <asp:BoundField DataField="Topic" HeaderText="Topic" SortExpression="Topic" meta:resourcekey="BoundFieldResource2" />
        <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" meta:resourcekey="BoundFieldResource3" />
        <asp:BoundField DataField="LastPostDate" HeaderText="Last post date" ReadOnly="True"
            SortExpression="LastPostDate" meta:resourcekey="BoundFieldResource4" />
        <asp:BoundField DataField="MessageCount" HeaderText="MessageCount" ReadOnly="True"
            SortExpression="MessageCount" meta:resourcekey="BoundFieldResource5" />
        <asp:CheckBoxField DataField="Blocked" HeaderText="Blocked" SortExpression="Blocked" meta:resourcekey="CheckBoxFieldResource1" />
    </Fields>
</asp:DetailsView>
<asp:SqlDataSource ID="FirstMessageDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_Trainings_Forum_GetTopicFirstMessage" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter Name="trainingID" QueryStringField="id" />
        <asp:QueryStringParameter Name="topicID" QueryStringField="topic" />
    </SelectParameters>
</asp:SqlDataSource>
