<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Create.ascx.cs" Inherits="Trainings_Create" %>
<%@ Register Src="../RegionEditControl.ascx" TagName="RegionEditControl" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<table width=100%>
<tr><td valign=top>
            <asp:Label runat=server id=courseHead Text="Course" meta:resourcekey="courseHeadResource1"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="coursDropDownList" runat="server" DataSourceID="CourseTitlesDataSource" DataTextField="Name" DataValueField="ID" meta:resourcekey="coursDropDownListResource1">
            </asp:DropDownList><asp:SqlDataSource ID="CourseTitlesDataSource" runat="server"
                ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>" SelectCommand="dcetools_Courses_GetTitles"
                SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
                <SelectParameters>
                    <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
</tr>
<tr><td valign=top>
            <asp:Label runat=server id=dateHead meta:resourcekey="dateHeadResource1" >Date</asp:Label> 
        </td>
        <td>
                <table><tr><td valign=top>
                <asp:Label runat=server id=startDateHeadSpanEdit meta:resourcekey="startDateHeadSpanEditResource1">From:</asp:Label>
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
				<asp:CalendarExtender
						ID="CalendarExtender1"
						runat="server"
						TargetControlID="tbStartDate" />
               </td> <td  valign=top>
                <asp:Label runat=server id=endDateHeadSpanEdit meta:resourcekey="endDateHeadSpanEditResource1">To:</asp:Label> 
               </td> <td valign=top>
				<asp:TextBox
						ID="tbEndDate"
						runat="server"
						Text='<%# Bind("EndDate") %>'
						meta:resourcekey="endDateCalendarResource1" />
				<asp:CompareValidator
						runat="server"
						ID="CompareValidator1"
						ControlToValidate="tbEndDate"
						Operator="DataTypeCheck"
						Type="Date"
						Display="Dynamic"
						SetFocusOnError="true" />
				<asp:CalendarExtender
						ID="CalendarExtender2"
						runat="server"
						TargetControlID="tbEndDate" />
               </td></tr></table> 
        </td>
</tr>
<tr><td valign=top>
            <asp:Label runat=server id=codeHead meta:resourcekey="codeHeadResource1">Code</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="codeTextBox" runat="server" Width="100%" meta:resourcekey="codeTextBoxResource1"></asp:TextBox>
        </td>
</tr>
<tr><td valign=top>
            <asp:Label runat=server id=nameHead meta:resourcekey="nameHeadResource1" >Name</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="nameTextBox" runat="server" Width="100%" meta:resourcekey="nameTextBoxResource1"></asp:TextBox>
        </td>
</tr>
<tr><td valign=top>
            <asp:Label runat=server id=regionHead meta:resourcekey="regionHeadResource1"> Region</asp:Label>
        </td>
        <td>
            <uc1:RegionEditControl ID="RegionEditControl1" runat="server" />
        </td>
</tr>
<tr><td valign=top>
            <asp:Label runat=server id=isPublicHead meta:resourcekey="isPublicHeadResource1">Is Public</asp:Label>            
        </td>
        <td>
            <asp:CheckBox ID="isPublicCheckBox" runat="server" Width="100%" meta:resourcekey="isPublicCheckBoxResource1"></asp:CheckBox>
        </td>
</tr>
<tr><td valign=top>
            <asp:Label runat=server id=isActiveHead meta:resourcekey="isActiveHeadResource1">Is Active</asp:Label>
            
        </td>
        <td>
            <asp:CheckBox ID="isActiveCheckBox" runat="server" Width="100%" meta:resourcekey="isActiveCheckBoxResource1"></asp:CheckBox>
        </td>
</tr>
<tr><td valign=top>
            <asp:Label runat=server id=testOnlyHead meta:resourcekey="testOnlyHeadResource1">Test Only</asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="testOnlyCheckBox" runat="server" Width="100%" meta:resourcekey="testOnlyCheckBoxResource1"></asp:CheckBox>
        </td>
</tr>
<tr><td valign=top>
            <asp:Label runat=server id=expiresHead meta:resourcekey="expiresHeadResource1">Expires</asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="expiresCheckBox" runat="server" Width="100%" meta:resourcekey="expiresCheckBoxResource1"></asp:CheckBox>
        </td>
</tr>
</table>
<br />
<asp:Button ID="createTrainingButton" runat="server" Text="Create" OnClick="createTrainingButton_Click" meta:resourcekey="createTrainingButtonResource1" />
<asp:Button ID="cancelButton" runat="server" OnClick="cancelButton_Click" Text="Відмінити" />