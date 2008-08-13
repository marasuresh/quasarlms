<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubscribeStudentList.ascx.cs" Inherits="Trainings_Students_SubscribeStudentList" %>
<%@ Register Src="~/Security/UserListWithSearch/SearchableUserList.ascx" TagName="SearchableUserList"
    TagPrefix="uc1" %>
<table width=100%><tr><td valign=top width=10% style="height: 133px">
    <asp:TextBox ID="searchTextBox" runat="server"></asp:TextBox><br />
    <div align=right>
    <asp:Button ID="searchButton" runat="server" Text="Пошук  >" />
    </div>
</td><td valign=top style="height: 133px">
    <asp:GridView ID="usersGridView" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" DataSourceID="SubscribeStudentsDataSource" EnableSortingAndPagingCallbacks="True" OnRowCommand="usersGridView_RowCommand" DataKeyNames="ID" EmptyDataText="Не знайдено слухачів за вказаними параметрами.">
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
</td></tr></table>
<asp:SqlDataSource ID="SubscribeStudentsDataSource" runat="server" CancelSelectOnNullParameter="False"
    ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>" SelectCommand="dcetools_Trainings_Students_FindNonTrainingStudents"
    SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="" Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter DefaultValue="" Name="trainingID" QueryStringField="id" />
        <asp:ControlParameter ControlID="searchTextBox" Name="searchString" PropertyName="Text" />
    </SelectParameters>
</asp:SqlDataSource>
