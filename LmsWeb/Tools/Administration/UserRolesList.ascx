<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserRolesList.ascx.cs" Inherits="Administration_UserRolesList" %>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
    DataSourceID="UserGroupsDataSource" Width="100%" BackColor=White GridLines=None CssClass=dataList EmptyDataText="No groups." OnRowCommand="GridView1_RowCommand" >
    <Columns>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
            <ItemStyle Width="1px" />
            <ItemTemplate>
                <asp:Image ID="studentIconImage" runat="server" ImageUrl="~/Images/card-people.gif"
                    meta:resourcekey="studentIconImageResource1" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
            <ItemStyle Width="90%" />
        </asp:BoundField>
        <asp:ButtonField HeaderText="Remove" Text="x" CommandName="Remove">
            <ItemStyle HorizontalAlign="Center" />
        </asp:ButtonField>
    </Columns>
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
</asp:GridView>
<asp:SqlDataSource ID="UserGroupsDataSource" runat="server" CancelSelectOnNullParameter="False"
    ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>" ProviderName="<%$ ConnectionStrings:Dce2005ConnectionString.ProviderName %>"
    SelectCommand="dcetools_Access_Users_GetUserGroups" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:QueryStringParameter Name="userID" QueryStringField="id" />
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
    </SelectParameters>
</asp:SqlDataSource>