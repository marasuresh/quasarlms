<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageEditControl.ascx.cs" Inherits="Trainings_Announcements_MessageEditControl" %>
    <asp:DetailsView ID="announcementDetailsView" runat="server" Width="100%" AutoGenerateRows="False" DataKeyNames="ID" DataSourceID="AnnouncementDetailsDataSource">
        <Fields>
            <asp:BoundField DataField="PostDate" HeaderText="Дата" SortExpression="PostDate" ReadOnly="True" />
            <asp:BoundField DataField="AuthorName" HeaderText="Автор" ReadOnly="True" SortExpression="AuthorName" />
            <asp:BoundField DataField="MessageText" HeaderText="Оголошення"
                SortExpression="MessageText">
                <ItemStyle VerticalAlign="Top" />
                <HeaderStyle VerticalAlign="Top" />
                <ControlStyle Height="20em" Width="100%" />
            </asp:BoundField>
            <asp:CommandField ButtonType="Button" CancelText="Відмінити" EditText="Редагувати"
                ShowEditButton="True" UpdateText="Змінити" />
        </Fields>
    </asp:DetailsView>
    <asp:SqlDataSource ID="AnnouncementDetailsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"    
        SelectCommand="dcetools_Trainings_Announcements_GetDetails" SelectCommandType="StoredProcedure"
        UpdateCommand="dcetools_Trainings_Announcements_UPDATE" UpdateCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnUpdating="AnnouncementDetailsDataSource_Updating">
        <UpdateParameters>
            <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
            <asp:QueryStringParameter Name="trainingID" QueryStringField="id" />
            <asp:Parameter Name="id" />
            <asp:Parameter Name="messageText" Type="String" />
            <asp:SessionParameter Name="authorID" SessionField="userID" />
        </UpdateParameters>
        <SelectParameters>
            <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
            <asp:QueryStringParameter Name="trainingID" QueryStringField="id" />
            <asp:QueryStringParameter Name="announcementID" QueryStringField="msg" />
        </SelectParameters>
    </asp:SqlDataSource>

