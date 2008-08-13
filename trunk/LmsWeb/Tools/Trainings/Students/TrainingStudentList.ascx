<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TrainingStudentList.ascx.cs" Inherits="Trainings_Students_TrainingStudentList" %>
<asp:TextBox ID="searchTextBox" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="searchButton"
    runat="server" Text="Пошук" />
<br />
<br />
<asp:GridView ID="StudentsGridView" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" CssClass="dataList" DataKeyNames="ID" DataSourceID="TrainingStudentsDataSource"
    BackColor=White Width="100%" OnRowCommand="StudentsGridView_RowCommand" >
    <Columns>
        <asp:TemplateField >
            <ItemStyle Width="1px" />
            <ItemTemplate>
                <asp:Image ID="studentIconImage" runat="server" ImageUrl="~/Images/Student16-brown2.gif" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="FullName" HeaderText="Слухач" ReadOnly="True" SortExpression="FullName" >
            <ItemStyle Width="90%" />
        </asp:BoundField>
        <asp:BoundField DataField="EMail" HeaderText="EMail" SortExpression="EMail" />
        <asp:BoundField DataField="JobPosition" HeaderText="Посада" SortExpression="JobPosition" />
        <asp:BoundField DataField="RegionName" HeaderText="Регіон" ReadOnly="True" SortExpression="RegionName" />
        <asp:ButtonField Text="x" CommandName="Unsubscribe" HeaderText=Вилучити >
            <ItemStyle Font-Bold="True" ForeColor="Red" HorizontalAlign="center" Width="50px" />
        </asp:ButtonField>
    </Columns>
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
</asp:GridView>
<asp:SqlDataSource ID="TrainingStudentsDataSource" runat="server" CancelSelectOnNullParameter="False"
    ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>" SelectCommand="dcetools_Trainings_Students_FindTrainingStudents"
    SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="" Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter DefaultValue="" Name="trainingID" QueryStringField="id" />
        <asp:ControlParameter ControlID="searchTextBox" Name="searchString" PropertyName="Text"
            Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
