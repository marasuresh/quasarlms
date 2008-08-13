<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubscribeAvailableTrainingList.ascx.cs" Inherits="Orders_SubscribeAvailableTrainingList" %>
<asp:GridView ID="availableTrainingsGridView" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" CssClass="dataList" DataSourceID="AvailableTrainingSqlDataSource"
    Width="100%" meta:resourcekey="availableTrainingsGridViewResource1" OnRowCommand="availableTrainingsGridView_RowCommand" DataKeyNames="ID">
    <Columns>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:Image ID="trainingIconImage" runat="server" ImageUrl="~/Images/calendar.gif" meta:resourcekey="trainingIconImageResource1" />
            </ItemTemplate>
            <ItemStyle Width="1px" />
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name" meta:resourcekey="BoundFieldResource1">
            <ItemStyle Width="90%" />
        </asp:BoundField>
        <asp:BoundField DataField="StartDate" DataFormatString="{0:d}" HeaderText="StartDate"
            HtmlEncode="False" SortExpression="StartDate" meta:resourcekey="BoundFieldResource2" />
        <asp:BoundField DataField="EndDate" DataFormatString="{0:d}" HeaderText="EndDate"
            HtmlEncode="False" SortExpression="EndDate" meta:resourcekey="BoundFieldResource3" />
        <asp:ButtonField Text="Subscribe" CommandName="Subscribe" meta:resourcekey="ButtonFieldResource1" />
    </Columns>
    <RowStyle BackColor="White" />
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
</asp:GridView>
<asp:SqlDataSource ID="AvailableTrainingSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_Orders_GetOrderAvailableTrainingList" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="" Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter Name="orderID" QueryStringField="id" />
    </SelectParameters>
</asp:SqlDataSource>
