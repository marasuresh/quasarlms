<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StudentResultsControl.ascx.cs" Inherits="Trainings_Tests_StudentResultsControl" %>
<asp:GridView ID="answersGridView" runat="server" PageSize="30" Width="100%" CssClass="dataList" GridLines=None AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="AnswerID" DataSourceID="answersDataSource" OnRowCommand="answersGridView_RowCommand" >
    <RowStyle BackColor="White" />
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
    <Columns>
        <asp:TemplateField HeaderText="Питання" SortExpression="QuestionXml">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("QuestionText") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="AnswerTime" HeaderText="Час" SortExpression="AnswerTime" />
        <asp:BoundField DataField="AnswerPoints" HeaderText="Бали" SortExpression="AnswerPoints" />
        <asp:BoundField DataField="StudentName" HeaderText="Слухач" SortExpression="StudentName" />
        
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="answersDataSource" runat="server" CancelSelectOnNullParameter="False"
    ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>" OnSelecting="answersDataSource_Selecting"
    SelectCommand="dcetools_Trainings_Tests_GetTestResultDetails" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter DefaultValue="" Name="testID" QueryStringField="test" />
    </SelectParameters>
</asp:SqlDataSource>
