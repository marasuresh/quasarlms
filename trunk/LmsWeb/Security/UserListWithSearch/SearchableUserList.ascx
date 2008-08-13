<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchableUserList.ascx.cs" Inherits="Tools_Administration_UserListWithSearch_SearchableUserList" %>
<table width=100%><tr><td valign=top width=10% style="height: 133px">
    <asp:TextBox ID="searchTextBox" runat="server"></asp:TextBox><br />
    <div align=right>
    <asp:Button ID="searchButton" runat="server" Text="Пошук  >" />
    </div><br />
   <div runat=server id=selectedUserBox visible=false>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Student16-brown2.gif" /> 
        <asp:Label ID="selectedUserLabel" runat="server" Text="Label"></asp:Label> 
    </div>  
</td><td valign=top style="height: 133px" rowspan=2>
    <asp:GridView ID="usersGridView" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" DataSourceID="defaultAllUserListDataSource" EnableSortingAndPagingCallbacks="True" OnRowCommand="usersGridView_RowCommand" DataKeyNames="ID" EmptyDataText="Не знайдено слухачів за вказаними параметрами." OnRowDataBound="usersGridView_RowDataBound">
        <Columns>
            <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/Student16-brown2.gif" />
            <asp:ButtonField DataTextField="FullName" HeaderText="П. І. Б." CommandName="SelectUser" SortExpression="FullName" />
            <asp:BoundField DataField="EMail" HeaderText="EMail" SortExpression="EMail" />
            <asp:BoundField DataField="JobPosition" HeaderText="Посада" SortExpression="JobPosition" />
            <asp:BoundField DataField="Comments" HeaderText="Примітки" SortExpression="Comments" />
            <asp:BoundField DataField="RegionName" HeaderText="Регіон" SortExpression="RegionName" />
            <asp:BoundField DataField="RoleName" HeaderText="Роль" SortExpression="RoleName" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="defaultAllUserListDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
        SelectCommand="SELECT ID, Login, FullName, EMail, JobPosition, Comments, CreateDate, LastModifyDate, RegionID, RegionName, RoleID, RoleName FROM dbo.dcetools_Fn_Access_FindUsersByAny(@homeRegion, @searchString) AS dcetools_Fn_Access_FindUsersByAny" CancelSelectOnNullParameter="false">
        <SelectParameters>
            <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
            <asp:ControlParameter ControlID="searchTextBox" Name="searchString" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
</td></tr>

<tr><td valign=bottom>
</td></tr></table>