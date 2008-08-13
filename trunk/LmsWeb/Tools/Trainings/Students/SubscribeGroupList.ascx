<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubscribeGroupList.ascx.cs" Inherits="Trainings_Students_SubscribeGroupList" %>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
    DataSourceID="SqlDataSource1" CssClass="dataList" BackColor="White" GridLines="None" OnRowCommand="GridView1_RowCommand" >
    <Columns>
          <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
            <ItemStyle Width="1px" />
            <ItemTemplate>
                <asp:Image ID="studentIconImage" runat="server" ImageUrl="~/Images/Student16-brown2.gif" meta:resourcekey="studentIconImageResource1" />
            </ItemTemplate>
        </asp:TemplateField>
      <asp:BoundField DataField="Name" HeaderText="Група" SortExpression="Name" >
          <ItemStyle Width="90%" />
      </asp:BoundField>
        <asp:ButtonField CommandName="Subscribe" Text="Підписати" HeaderText="Усіх" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:ButtonField>
        <asp:ButtonField CommandName="Unsubscribe" Text="Виключити" HeaderText="Усіх" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:ButtonField>
    </Columns>
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="select * from Groups&#13;&#10;where type=14&#13;&#10;" CancelSelectOnNullParameter="False"></asp:SqlDataSource>
