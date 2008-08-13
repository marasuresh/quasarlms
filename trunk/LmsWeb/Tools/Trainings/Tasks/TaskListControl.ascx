<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaskListControl.ascx.cs" Inherits="Tools_Trainings_Tasks_TaskList" %>
<asp:GridView ID="tasksGridView" runat="server" DataSourceID="TaskListDataSource" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="tasksGridView_RowCommand" EmptyDataText="Завдань до тренінгу немає.">
    <Columns>
        <asp:BoundField DataField="TaskTime" DataFormatString="{0:d}" HeaderText="Дата" HtmlEncode="False"
            SortExpression="TaskTime" />
        <asp:ButtonField CommandName="ShowTask" DataTextField="Name" HeaderText="Завдання">
            <ItemStyle Width="90%" />
        </asp:ButtonField>
    </Columns>
</asp:GridView>
<asp:SqlDataSource
		ID="TaskListDataSource"
		runat="server"
		ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
		SelectCommand="dcetools_Trainings_Tasks_GetTaskList"
		SelectCommandType="StoredProcedure"
		CancelSelectOnNullParameter="False">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter Name="trainingID" QueryStringField="id" />
    </SelectParameters>
</asp:SqlDataSource>
