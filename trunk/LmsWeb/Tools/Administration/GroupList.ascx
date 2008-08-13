<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupList.ascx.cs" Inherits="Administration_GroupList" %>
&nbsp;
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
	<ContentTemplate>
<asp:GridView
		ID="GridView1"
		runat="server"
		AutoGenerateColumns="False"
		DataSource='<%# System.Web.Security.Roles.GetAllRoles() %>'
		BackColor="White"
		GridLines="None" OnRowEditing="GridView1_RowEditing">
    <Columns>
        <asp:CommandField ShowDeleteButton="True" DeleteText="x" >
            <ItemStyle Font-Bold="True" Font-Size="XX-Small" ForeColor="Red" VerticalAlign="Middle" Width="1em" />
        </asp:CommandField>
          <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
            <ItemStyle Width="1px" />
            <ItemTemplate>
                <asp:Image ID="studentIconImage" runat="server" ImageUrl="~/Images/card-people.gif"
                    meta:resourcekey="studentIconImageResource1" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Група" SortExpression="Name">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Width="100%" Text='<%# Container.DataItem %>'></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:HyperLink
						ID="HyperLink1"
						runat="server"
						NavigateUrl='<%# string.Format("GroupDetails.aspx?group={0}", Container.DataItem) %>'
						Text='<%# Container.DataItem %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowEditButton="True" ButtonType="Button" CancelText="Відмінити" DeleteText="Видалити" EditText="Змінити" InsertText="Додати" NewText="Створити" SelectText="Обрати" UpdateText="Зберегти" >
            <ItemStyle HorizontalAlign="Center" Width="10em" />
        </asp:CommandField>
    </Columns>
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
</asp:GridView>
<asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="Додати групу..." />
	</ContentTemplate>
</asp:UpdatePanel>
