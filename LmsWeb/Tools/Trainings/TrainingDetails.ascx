<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TrainingDetails.ascx.cs" Inherits="Trainings_TrainingDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../RegionEditControl.ascx" TagName="RegionEditControl" TagPrefix="uc1" %>
<asp:DetailsView ID="TrainingDetailsView" runat="server" AutoGenerateRows="False"
    DataSourceID="TrainingDetailsDataSource" Width="100%" EmptyDataText="No training selected." meta:resourcekey="TrainingDetailsViewResource1">
    <Fields>
        <asp:BoundField DataField="Code" HeaderText="Код" SortExpression="Code" meta:resourcekey="BoundFieldResource1" >
            <ControlStyle Width="100%" />
        </asp:BoundField>
        <asp:BoundField DataField="Name" HeaderText="Назва" SortExpression="Name" meta:resourcekey="BoundFieldResource2" >
            <ControlStyle Width="100%" />
        </asp:BoundField>
        <asp:BoundField DataField="CourseName" HeaderText="Курс" ReadOnly="True" SortExpression="CourseName" meta:resourcekey="BoundFieldResource3" />
        <asp:TemplateField HeaderText="Примітки" SortExpression="Comments" meta:resourcekey="TemplateFieldResource1">
            <EditItemTemplate>
                <asp:TextBox ID="commentTextBox" runat="server" Height="10em" Text='<%# Bind("Comments") %>'
                    Width="100%" meta:resourcekey="commentTextBoxResource1"></asp:TextBox>
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Comments") %>' meta:resourcekey="TextBox1Resource1"></asp:TextBox>
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:Label ID="commentLabel" runat="server" Text='<%# Bind("Comments") %>' meta:resourcekey="commentLabelResource1"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Дата" SortExpression="StartDate">
            <EditItemTemplate>
                <table><tr><td valign=top>
                <span runat=server id=startDateHeadSpanEdit>Початок:</span>
               </td> <td  valign=top>
				<asp:TextBox
						ID="tbStartDate"
						runat="server"
						Text='<%# Bind("StartDate") %>'
						meta:resourcekey="startDateCalendarResource1" />
				<asp:CompareValidator
						runat="server"
						ID="cmpvalStartDate"
						ControlToValidate="tbStartDate"
						Operator="DataTypeCheck"
						Type="Date"
						Display="Dynamic"
						SetFocusOnError="true" />
				<cc1:CalendarExtender
						ID="CalendarExtender1"
						runat="server"
						TargetControlID="tbStartDate" />
               </td> <td  valign=top>
                <span runat=server id=endDateHeadSpanEdit>Закінчення:<br />
                </span> 
               </td> <td valign=top> 
					<asp:TextBox
							runat="server"
							ID="tbEndDate"
							Text='<%# Bind("EndDate") %>'
							meta:resourcekey="endDateCalendarResource1" />
					<asp:CompareValidator
						runat="server"
						ID="cmpvalEndDate"
						ControlToValidate="tbEndDate"
						Operator="DataTypeCheck"
						Type="Date"
						Display="Dynamic"
						SetFocusOnError="true" />
					<cc1:CalendarExtender
						ID="CalendarExtender2"
						runat="server"
						TargetControlID="tbEndDate" />
               </td></tr></table> 
               
                
            </EditItemTemplate>
            <ItemTemplate>
                <span runat=server id=startDateHeadSpan>Початок:</span>
                <asp:Label ID="startDateLabel" runat="server" Text='<%# Bind("StartDate", "{0:d}") %>' meta:resourcekey="startDateLabelResource1"></asp:Label>
               <span runat=server id=endDateHeadSpan>Закінчення:</span> 
               <asp:Label ID="endDateLabel" runat="server" Text='<%# Bind("EndDate", "{0:d}") %>' meta:resourcekey="endDateLabelResource1"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Регіон" SortExpression="RegionName" meta:resourcekey="TemplateFieldResource3">
            <EditItemTemplate>
                
                <uc1:RegionEditControl ID="RegionEditControl1" runat="server" RegionID='<%# Bind("RegionID") %>' />

            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("RegionName") %>' meta:resourcekey="TextBox3Resource1"></asp:TextBox>
            </InsertItemTemplate>
            <ItemTemplate>
                <asp:Label ID="regionNameLabel" runat="server" Text='<%# Bind("RegionName") %>' meta:resourcekey="regionNameLabelResource1"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CheckBoxField DataField="IsPublic" HeaderText="IsPublic" SortExpression="IsPublic" meta:resourcekey="CheckBoxFieldResource1" />
        <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" meta:resourcekey="CheckBoxFieldResource2" />
        <asp:CheckBoxField DataField="TestOnly" HeaderText="TestOnly" SortExpression="TestOnly" meta:resourcekey="CheckBoxFieldResource3" />
        <asp:CheckBoxField DataField="Expires" HeaderText="Expires" SortExpression="Expires" meta:resourcekey="CheckBoxFieldResource4" />
        <asp:CommandField ButtonType="Button" ShowEditButton="True" meta:resourcekey="CommandFieldResource1" />
    </Fields>
</asp:DetailsView>
<asp:SqlDataSource ID="TrainingDetailsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_Trainings_GetDetails" SelectCommandType="StoredProcedure"
    UpdateCommand="dcetools_Trainings_UpdateDetails" UpdateCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnUpdating="TrainingDetailsDataSource_Updating">
    <UpdateParameters>
        <asp:Parameter Name="homeRegion" />
        <asp:QueryStringParameter Name="trainingID" QueryStringField="id" />
        <asp:Parameter Name="code" Type="String" />
        <asp:Parameter Name="name" Type="String" />
        <asp:Parameter Name="comments" Type="String" />
        <asp:Parameter Name="startDate" Type="DateTime" />
        <asp:Parameter Name="endDate" Type="DateTime" />
        <asp:Parameter Name="isPublic" Type="Boolean" />
        <asp:Parameter Name="isActive" Type="Boolean" />
        <asp:Parameter Name="testOnly" Type="Boolean" />
        <asp:Parameter Name="expires" Type="Boolean" />
        <asp:Parameter Name="regionID" />
    </UpdateParameters>
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter Name="trainingID" QueryStringField="id" />
    </SelectParameters>
</asp:SqlDataSource>
