<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupUserListControl.ascx.cs" Inherits="Tools_Administration_GroupUserListControl" %>
<asp:GridView ID="groupUserGridView" runat="server" AutoGenerateColumns="False">
    <Columns>
          <asp:TemplateField>
            <ItemStyle Width="1px" />
            <ItemTemplate>
                <asp:Image ID="studentIconImage" runat="server" ImageUrl="~/Images/Student16-brown2.gif" meta:resourcekey="studentIconImageResource1" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Слухач" SortExpression="UserName">
			<ItemTemplate>
				<%# Container.DataItem %>
			</ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
