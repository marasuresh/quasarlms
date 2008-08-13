<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TrainingSchedule.ascx.cs" Inherits="Trainings_TrainingSchedule" %>
<asp:GridView ID="ScheduleGridView" runat="server" AllowPaging="True"
    AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ScheduleDataSource" Width="100%" meta:resourcekey="ScheduleGridViewResource1">
    <Columns>
        <asp:TemplateField HeaderText="Початок" SortExpression="StartDate">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StartDate", "{0:d}") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                    Display="Dynamic" ErrorMessage="RequiredFieldValidator">Вкажiть дату</asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TextBox1"
                    Display="Dynamic" ErrorMessage="RangeValidator" MaximumValue="10.10.2030" MinimumValue="10.10.2003"
                    Type="Date">Дата невiдповiдна</asp:RangeValidator>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("StartDate", "{0:d}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Закінчення" SortExpression="EndDate">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("EndDate", "{0:d}") %>'></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("EndDate", "{0:d}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:HyperLinkField DataNavigateUrlFields="ContentHref" DataTextField="Name" HeaderText="Theme"
            SortExpression="Name" meta:resourcekey="HyperLinkFieldResource1" >
            <ItemStyle Width="90%" />
        </asp:HyperLinkField>
        <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration" ReadOnly="True" meta:resourcekey="BoundFieldResource3" />
        <asp:CheckBoxField DataField="IsOpen" HeaderText="IsOpen" SortExpression="IsOpen" meta:resourcekey="CheckBoxFieldResource1" />
        <asp:CheckBoxField DataField="Mandatory" HeaderText="Mandatory" SortExpression="Mandatory" meta:resourcekey="CheckBoxFieldResource2" />
        <asp:CommandField ShowEditButton="True" meta:resourcekey="CommandFieldResource1" />
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="ScheduleDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_Trainings_Schedule_GetSchedule" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnUpdating="ScheduleDataSource_Updating" UpdateCommand="dcetools_Trainings_Schedule_UpdateThemeSchedule" UpdateCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter Name="trainingID" QueryStringField="id" />
    </SelectParameters>
    <UpdateParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter Name="trainingID" QueryStringField="id" />
        <asp:Parameter Name="id" />
        <asp:Parameter Name="startDate" Type="DateTime" />
        <asp:Parameter Name="endDate" Type="DateTime" />
        <asp:Parameter Name="isOpen" Type="Boolean" />
        <asp:Parameter Name="mandatory" Type="Boolean" />
    </UpdateParameters>
</asp:SqlDataSource>
