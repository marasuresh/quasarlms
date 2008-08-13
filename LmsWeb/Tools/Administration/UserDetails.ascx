<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserDetails.ascx.cs" Inherits="Administration_UserDetails" %>
<%@ Register Src="RoleSelect.ascx" TagName="RoleSelect" TagPrefix="uc1" %>
<asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataSourceID="UserDetailsDataSource"
    Width="100%">
    <Fields>
        <asp:BoundField DataField="Login" HeaderText="Ім'я LDAP" SortExpression="Login" />
        <asp:BoundField DataField="FullName" HeaderText="ПІБ" ReadOnly="True" SortExpression="FullName" />
        <asp:BoundField DataField="EMail" HeaderText="EMail" SortExpression="EMail" />
        <asp:BoundField DataField="JobPosition" HeaderText="Посада" SortExpression="JobPosition" />
        <asp:BoundField DataField="Comments" HeaderText="Примітки" SortExpression="Comments" />
        <asp:BoundField DataField="RegionName" HeaderText="Регіон" ReadOnly="True" SortExpression="RegionName" />
        <asp:BoundField DataField="RoleName" HeaderText="Роль" ReadOnly="True" SortExpression="RoleName" />
    </Fields>
</asp:DetailsView>
<asp:SqlDataSource ID="UserDetailsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_Access_Users_UserTitle" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    <SelectParameters>
        <asp:QueryStringParameter Name="userID" QueryStringField="id" />
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
    </SelectParameters>
</asp:SqlDataSource>
<br />
