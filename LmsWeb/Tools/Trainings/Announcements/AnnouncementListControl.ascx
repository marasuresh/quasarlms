<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AnnouncementListControl.ascx.cs" Inherits="Trainings_Announcements_AnnouncementListControl" %>
<asp:GridView ID="announcementsGridView" runat="server"
    GridLines=None
    PageSize="30"
    Width="100%"
    CssClass="dataList"
    AllowPaging="True"
    AllowSorting="True"
    AutoGenerateColumns="False"
    DataKeyNames="ID"
    DataSourceID="announcementsDataSource"
    OnRowCommand="announcementsGridView_RowCommand"
    EmptyDataText="По цьому тренінгу немає оголошень." >
    <RowStyle BackColor="White" />
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
    <Columns>
        <asp:TemplateField >
            <ItemTemplate>
                <asp:Image ID="trainingIconImage" runat="server" ImageUrl="~/Images/Announcements16.gif" meta:resourcekey="trainingIconImageResource1" />
            </ItemTemplate>
            <ItemStyle Width="1px" VerticalAlign="Top" />
        </asp:TemplateField>
        <asp:BoundField
				DataField="PostDate"
				SortExpression="PostDate"
				meta:resourcekey="boundFieldPostDate">
            <ItemStyle VerticalAlign="Top" />
        </asp:BoundField>
        <asp:BoundField
				DataField="AuthorName"
				ReadOnly="True"
				SortExpression="AuthorName"
				meta:resourcekey="boundFieldAuthorName">
            <ItemStyle VerticalAlign="Top" />
        </asp:BoundField>
        <asp:ButtonField
				CommandName="ShowMessage"
				DataTextField="MessageText"
				SortExpression="MessageText"
				meta:resourcekey="buttonFieldMessage">
            <ItemStyle VerticalAlign="Top" />
        </asp:ButtonField>
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="announcementsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_Trainings_Announcements_GetAnnouncementList" SelectCommandType="StoredProcedure"
    UpdateCommand="CreateNewsItem" UpdateCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
    <UpdateParameters>
        <asp:Parameter Name="newsID" Type="Object" />
        <asp:Parameter Name="newsRegion" Type="Object" />
        <asp:Parameter Name="newsDate" Type="DateTime" />
        <asp:Parameter Name="head" Type="String" />
        <asp:Parameter Name="short" Type="String" />
        <asp:Parameter Name="textData" Type="String" />
        <asp:Parameter Name="moreText" Type="String" />
        <asp:Parameter Name="imageData" Type="Object" />
        <asp:Parameter Name="moreHRef" Type="String" />
    </UpdateParameters>
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter Name="trainingID" QueryStringField="id" />
    </SelectParameters>
</asp:SqlDataSource>
