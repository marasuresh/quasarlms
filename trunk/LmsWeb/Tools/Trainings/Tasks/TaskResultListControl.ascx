<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaskResultListControl.ascx.cs" Inherits="Tools_Trainings_Tasks_TaskResultListControl" %>
<asp:GridView ID="taskResultListGridView" runat="server" AutoGenerateColumns="False"
    DataKeyNames="ID" DataSourceID="TaskResultListDataSource" EmptyDataText="Ще ніхто не виконав завдання.">
    <Columns>
        <asp:BoundField DataField="SolutionDate" DataFormatString="{0:d}" HeaderText="Дата"
            SortExpression="SolutionDate" />
        <asp:BoundField DataField="StudentName" HeaderText="Слухач" ReadOnly="True" SortExpression="StudentName">
            <ItemStyle Width="40%" />
        </asp:BoundField>
        <asp:BoundField DataField="Complete" HeaderText="Виконано" SortExpression="Complete">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="SolutionText" HeaderText="Рішення" SortExpression="SolutionText">
            <ItemStyle Width="50%" />
        </asp:BoundField>
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="TaskResultListDataSource" runat="server" CancelSelectOnNullParameter="False"
    ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>" SelectCommand="dcetools_Trainings_Tasks_GetTaskSolutions"
    SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter Name="taskID" QueryStringField="task" />
    </SelectParameters>
</asp:SqlDataSource>
