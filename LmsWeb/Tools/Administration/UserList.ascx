<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserList.ascx.cs" Inherits="Administration_UserList" %>
<%@ Register Src="../../Security/UserListWithSearch/SearchableUserList.ascx" TagName="SearchableUserList"
    TagPrefix="uc1" %>
<asp:TextBox ID="searchTextBox" runat="server" Width="100%"></asp:TextBox><br />
<asp:Button ID="searchButton" runat="server" Text="Знайти користувача" /><br />
<br />
<asp:GridView ID="searchUsersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
    DataSourceID="SearchUsersDataSource" Width="100%" BackColor=White GridLines=None CssClass=dataList AllowPaging="True" PageSize="20" AllowSorting="True">
    <Columns>
          <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
            <ItemStyle Width="1px" />
            <ItemTemplate>
                <asp:Image ID="studentIconImage" runat="server" ImageUrl="~/Images/Student16-brown2.gif" meta:resourcekey="studentIconImageResource1" ToolTip='<%# Eval("Login") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:HyperLinkField
            DataNavigateUrlFields="ID"
            DataNavigateUrlFormatString="User.aspx?id={0}"
            DataTextField="FirstName"
            HeaderText="Ім'я"
            SortExpression="FirstName" />
        <asp:HyperLinkField
            DataNavigateUrlFields="ID"
            DataNavigateUrlFormatString="User.aspx?id={0}"
            DataTextField="Patronymic"
            HeaderText="По-батькові"
            SortExpression="Patronymic" />
        <asp:HyperLinkField
            DataNavigateUrlFields="ID"
            DataNavigateUrlFormatString="User.aspx?id={0}"
            DataTextField="LastName"
            HeaderText="Прізвище"
            SortExpression="LastName" />
        <asp:BoundField DataField="EMail" HeaderText="EMail" SortExpression="EMail" />
        <asp:BoundField DataField="Login" HeaderText="(login)" SortExpression="Login" />
        <asp:BoundField DataField="JobPosition" HeaderText="Посада" SortExpression="JobPosition" />
        <asp:BoundField DataField="Comments" HeaderText="Примітки" SortExpression="Comments" />
        <asp:BoundField DataField="RoleName" HeaderText="Роль" SortExpression="RoleName" />
        <asp:BoundField DataField="RegionName" HeaderText="Регіон" ReadOnly="True" SortExpression="RegionName" />
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Button ID="editMiltipleUsersButton" runat="server" OnClick="editMiltipleUsersButton_Click"
                    Text="Змінити загалом" />
            </HeaderTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:CheckBox ID="selectUserCheckBox" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
</asp:GridView>
<asp:SqlDataSource ID="SearchUsersDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_Access_Users_FindUsers" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:ControlParameter ControlID="searchTextBox" Name="searchString" PropertyName="Text"
            Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
