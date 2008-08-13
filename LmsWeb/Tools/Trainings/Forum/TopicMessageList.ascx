<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopicMessageList.ascx.cs" Inherits="Trainings_Forum_TopicMessageList" %>
<asp:GridView ID="MessageListGridView" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="MessageListDataSource" CssClass=forumMessageList meta:resourcekey="MessageListGridViewResource1">
    <Columns>
        <asp:TemplateField HeaderText="Date" SortExpression="PostDate" meta:resourcekey="TemplateFieldResource1">
            <ItemStyle VerticalAlign="Top" Width="1em" Wrap="False" />
            <ItemTemplate>
                <nobr>
                    <asp:Label ID="postDateLabel" runat="server" Text='<%# Eval("PostDate", "{0:D}") + " " +Eval("PostDate", "{0:t}") %>' meta:resourcekey="postDateLabelResource1"></asp:Label><br />
                </nobr> 
                <table><tr><td>
                    <asp:Image ID="studentIconImage" runat="server" ImageUrl="~/Images/Student16-brown2.gif" meta:resourcekey="studentIconImageResource1" />
                </td><td>
                    <nobr>
                    <asp:Label ID="studentLabel" runat="server" Font-Bold="True" Text='<%# Eval("AuthorName") %>' meta:resourcekey="studentLabelResource1"></asp:Label>
                    </nobr>
                </td></tr></table>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Message" SortExpression="Message" meta:resourcekey="TemplateFieldResource2">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Message") %>' meta:resourcekey="TextBox1Resource1"></asp:TextBox>
            </EditItemTemplate>
            <ItemStyle VerticalAlign="Top" Width="90%" />
            <ItemTemplate>
                <asp:Label ID="messageLabel" runat="server" Text='<%# Bind("Message") %>' meta:resourcekey="messageLabelResource1"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="MessageListDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_Trainings_Forum_GetTopicMessages" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter Name="trainingID" QueryStringField="id" />
        <asp:QueryStringParameter Name="topicID" QueryStringField="topic" />
    </SelectParameters>
</asp:SqlDataSource>
