<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TrainingTestList.ascx.cs" Inherits="Trainings_Tests_TrainingTestList" %>
<asp:GridView ID="testResultsGridView" runat="server" GridLines=None AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="dataList" DataKeyNames="ID" DataSourceID="TestResultsDataSource" OnRowCommand="testResultsGridView_RowCommand" PageSize="30" Width="100%"
EmptyDataText="Тестів до курсу не додано."
    >
    <RowStyle BackColor="White" />
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
    <Columns>
        <asp:ButtonField CommandName="ShowDetails" DataTextField="StudentName" HeaderText="Слухач"
            SortExpression="StudentName" />
        <asp:BoundField DataField="ThemeName" HeaderText="Тема" ReadOnly="True" SortExpression="ThemeName" />
        <asp:CheckBoxField DataField="IsCompleted" HeaderText="Пройдено" SortExpression="IsCompleted" />
        <asp:BoundField DataField="PointsCollected" HeaderText="Бали" ReadOnly="True" SortExpression="PointsCollected" />
        <asp:BoundField DataField="PointsRequired" HeaderText="Необхідно" SortExpression="PointsRequired" />
        <asp:BoundField DataField="CompletionDate" DataFormatString="{0:d}" HeaderText="Дата"
            HtmlEncode="False" SortExpression="CompletionDate" />
        <asp:BoundField DataField="TryCount" HeaderText="Кількість спроб" SortExpression="TryCount" />
        <asp:BoundField DataField="RestTryCount" HeaderText="Залишилося спроб" SortExpression="RestTryCount" />
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="TestResultsDataSource" runat="server" CancelSelectOnNullParameter="False"
    ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>" SelectCommand="dcetools_Trainings_Tests_GetTestResults"
    SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter DefaultValue="" Name="trainingID" QueryStringField="id" />
    </SelectParameters>
</asp:SqlDataSource>
