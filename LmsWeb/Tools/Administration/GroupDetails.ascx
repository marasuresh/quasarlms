<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupDetails.ascx.cs" Inherits="Tools_Administration_GroupDetails" %>
<asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False"
    Width="100%">
    <Fields>
        <asp:TemplateField HeaderText="Група" SortExpression="Name">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Container.DataItem %>'></asp:TextBox>
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Container.DataItem %>'></asp:TextBox>
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("Groups.aspx", Container.DataItem) %>'
                    Text='<%# Container.DataItem %>'></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
    </Fields>
</asp:DetailsView>
