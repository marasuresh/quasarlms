<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TrainingList.ascx.cs" Inherits="Trainings_TrainingList" %>
<asp:GridView ID="TrainingsGridView" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="TrainingTitlesDataSource"
    Width="100%" EmptyDataText="No trainings." CssClass="dataList" GridLines="None" meta:resourcekey="TrainingsGridViewResource1">
    <Columns>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:Image ID="trainingIconImage" runat="server" ImageUrl="~/Images/calendar.gif" meta:resourcekey="trainingIconImageResource1" />
            </ItemTemplate>
            <ItemStyle Width="1px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="StartDate" SortExpression="StartDate" meta:resourcekey="TemplateFieldResource2">
            <ItemTemplate>
                <asp:Label ID="startDateLabel" runat="server" Text='<%# Bind("StartDate", "{0:d}") %>'
                    ToolTip='<%# Eval("EndDate", "— {0:d}") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="1px" />
        </asp:TemplateField>
        <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Tools/Trainings/Details.aspx?id={0}"
            DataTextField="Code" HeaderText="Code" SortExpression="Code" meta:resourcekey="HyperLinkFieldResource1" />
        <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Tools/Trainings/Details.aspx?id={0}"
            DataTextField="Name" HeaderText="Name" SortExpression="Name" meta:resourcekey="HyperLinkFieldResource2" />
        <asp:BoundField DataField="RegionName" HeaderText="Region" ReadOnly="True" SortExpression="RegionName" meta:resourcekey="BoundFieldResource1" />
    </Columns>
    <RowStyle BackColor="White" />
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
</asp:GridView>
<asp:SqlDataSource ID="TrainingTitlesDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_Trainings_GetTitles" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnSelecting="TrainingTitlesDataSource_Selecting">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
    </SelectParameters>
</asp:SqlDataSource>
