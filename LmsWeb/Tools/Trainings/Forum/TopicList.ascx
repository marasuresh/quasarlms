<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopicList.ascx.cs" Inherits="Trainings_Forum_TopicList" %>
<asp:GridView ID="TopicsGridView" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" CssClass="dataList" DataKeyNames="ID" DataSourceID="TopicsDataSource"
    EmptyDataText="No topics." OnRowCommand="TopicsGridView_RowCommand" GridLines="None" meta:resourcekey="TopicsGridViewResource1">
    <Columns>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:Image ID="topicIconImage" runat="server" ImageUrl="~/Images/card-people.gif" meta:resourcekey="topicIconImageResource1" />
            </ItemTemplate>
            <ItemStyle Width="1px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Topic" ShowHeader="False" SortExpression="Topic" meta:resourcekey="TemplateFieldResource2">
            <ItemStyle Width="100%" />
            <ItemTemplate>
                <asp:LinkButton ID="topicLinkButton" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>'
                    Text='<%# Eval("Topic") + " ("+Eval("MessageCount")+")" %>'
                    ToolTip='<%# Eval("Message") %>' CommandName="ShowTopic"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="StudentFullName" HeaderText="Author" ReadOnly="True" SortExpression="StudentFullName" meta:resourcekey="BoundFieldResource1" >
            <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="LastPostDate" HeaderText="Last Date" ReadOnly="True" SortExpression="LastPostDate" meta:resourcekey="BoundFieldResource2">
            <ItemStyle Wrap="False" />
        </asp:BoundField>
    </Columns>
    <RowStyle BackColor="White" />
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
</asp:GridView>
<asp:SqlDataSource ID="TopicsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_Trainings_Forum_GetTopicList" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnSelected="TopicsDataSource_Selected">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter Name="trainingID" QueryStringField="id" />
    </SelectParameters>
</asp:SqlDataSource>
