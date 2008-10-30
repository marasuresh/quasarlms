﻿<%@ Import Namespace="System.ComponentModel"%>
<%@ Control
		Language="C#"
		AutoEventWireup="true"
		ClassName="MyCourses"
		Inherits="N2.Lms.Web.UI.MyAssignmentListControl`1[[N2.Lms.MyCoursesDAO, LmsWeb]], N2.Lms" %>
<%@ Register Assembly="N2.Futures" Namespace="N2.Web.UI.WebControls" TagPrefix="n2" %>

<script runat="server">
	protected void lv_ItemUpdating(object sender, ListViewUpdateEventArgs e)
	{
		var _lv = sender as ListView;
		
		//e.NewValues.Add("courseId", _lv.DataKeys[_lv.EditIndex]);
		e.NewValues.Add("begin", ((DatePicker)_lv.EditItem.FindControl("dtBegin")).SelectedDate);
		e.NewValues.Add("end", ((DatePicker)_lv.EditItem.FindControl("dtEnd")).SelectedDate);
		e.NewValues.Add("comments", ((TextBox)_lv.EditItem.FindControl("tbComment")).Text);
	}
</script>

<asp:ObjectDataSource 
	ID="dsCourses"
	runat="server"
	SelectMethod="FindAll"
	UpdateMethod="InsertRequest"
	TypeName="N2.Lms.MyCoursesDAO"
	onobjectcreating="ds_ObjectCreating" 
    OldValuesParameterFormatString="original_{0}" >
	<UpdateParameters>
		<%--<asp:Parameter Name="courseId" Type="Int32" ConvertEmptyStringToNull="true" />--%>
		<asp:Parameter Name="begin" Type="DateTime" ConvertEmptyStringToNull="true" />
		<asp:Parameter Name="end" Type="DateTime" ConvertEmptyStringToNull="true" />
		<asp:Parameter Name="comments" Type="String" />
	</UpdateParameters>
</asp:ObjectDataSource>

<n2:ChromeBox runat="Server">
<asp:ListView
		ID="lv"
		DataKeyNames="ID"
		runat="server"
		DataSourceID="dsCourses" 
		onitemupdating="lv_ItemUpdating">
	
	<LayoutTemplate>
		<table class="gridview" cellpadding="0" cellspacing="0">
			<tr class="header">
				<th></th>
				<th>Title</th>
			<tr id="itemPlaceholder" runat="server" />
		</table>
	</LayoutTemplate>
	
	<ItemTemplate>
		<tr class='<%# Container.DataItemIndex % 2 == 0 ? "row" : "altrow" %>'>
			<td class="command"><asp:LinkButton ID="btnEdit" runat="server" Text="View" CommandName="Edit" /></td>
			<td><%# Eval("Title") %></td>
		</tr>
	</ItemTemplate>
	
	<EditItemTemplate>
		<tr class='edit-info <%# Container.DataItemIndex == 0 ? "first" : string.Empty %>'>
			<td class="command"><asp:LinkButton ID="btnEdit" runat="server" Text="Cancel" CommandName="Cancel" /></td>
			<td><%# Eval("Title") %></td>
		</tr>
		<tr><td class="edit" colspan="2">
				<div class="details">
					<div class="header">Edit details for '<%# Eval("Title")%>'</div>
					<table class="detailview" cellpadding="0" cellspacing="0">
						<tr><th>Begin</th>
							<td><n2:DatePicker ID="dtBegin" runat="server" /></td></tr>
						<tr><th>End</th>
							<td><n2:DatePicker ID="dtEnd" runat="server" /></td>
						</tr>
						<tr><th>Comments</th>
							<td><asp:TextBox ID="tbComment" TextMode="MultiLine" runat="server" /></td>
						</tr> 
					</table>
					<div class="footer command">
						<asp:LinkButton ID="btnSave" runat="server" Text="Subscribe" CommandName="Update" />
						<asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" />
					</div>
				</div>
			</td>
		</tr>
	</EditItemTemplate>
</asp:ListView>
</n2:ChromeBox>
