<%@ Import Namespace="N2.Lms.Items.Lms.RequestStates"%>
<%@ Import Namespace="N2.Workflow"%>
<%@ Import Namespace="System.ComponentModel"%>
<%@ Control
		Language="C#"
		AutoEventWireup="true"
		ClassName="MyTrainings"
		Inherits="N2.Lms.Web.UI.MyAssignmentListControl`1[[N2.Lms.MyTrainingsDAO, LmsWeb]], N2.Lms" %>
<%@ Register Assembly="N2.Futures" Namespace="N2.Web.UI.WebControls" TagPrefix="n2" %>

<script runat="server">
    
    protected void lv_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        var _lv = sender as ListView;

        e.NewValues.Add("comments", ((TextBox)_lv.EditItem.FindControl("tbComment")).Text);
    }
</script>

<asp:ObjectDataSource 
	ID="dsRequests"
	runat="server"
	SelectMethod="FindAll"
	UpdateMethod="GoRequest"
	TypeName="N2.Lms.MyTrainingsDAO"
	onobjectcreating="ds_ObjectCreating">
	<UpdateParameters>
        <asp:Parameter Name="comments" Type="String" />
    </UpdateParameters>
</asp:ObjectDataSource>

<n2:ChromeBox runat="Server">
<asp:ListView
		ID="lv"
		DataKeyNames="ID"
		runat="server"
		DataSourceID="dsRequests"  OnItemUpdating="lv_ItemUpdating">
	
	<LayoutTemplate>
		<table class="gridview" cellpadding="0" cellspacing="0">
			<tr class="header">
				<th></th>
				<th>Тренинг</th>
			<tr id="itemPlaceholder" runat="server" />
		</table>
	</LayoutTemplate>
	
	<ItemTemplate>
		<tr class='<%# Container.DataItemIndex % 2 == 0 ? "row" : "altrow" %>'>
			<td class="command"><asp:LinkButton ID="btnEdit" runat="server" Text="View" CommandName="Edit" /></td>
			<td><%# N2.Web.Link.To(((ApprovedState)((Request)Container.DataItem).GetCurrentState()).Training).ToString()%></td>
		</tr>
	</ItemTemplate>
	
	<EditItemTemplate>
		<tr class='edit-info <%# Container.DataItemIndex == 0 ? "first" : string.Empty %>'>
			<td class="command"><asp:LinkButton ID="btnEdit" runat="server" Text="Отмена" CommandName="Cancel" /></td>
			<td><%# ((ApprovedState)((Request)Container.DataItem).GetCurrentState()).Training.Title %></td>
		</tr>
		<tr><td class="edit" colspan="2">
				<div class="details">
					<div class="header">Edit details for '<%# Eval("Title")%>'</div>
					<table class="detailview" cellpadding="0" cellspacing="0">
						<tr><th>Комментарий:</th>
							<td><asp:TextBox ID="tbComment" TextMode="MultiLine" runat="server" /></td>
						</tr> 
					</table>
					<div class="footer command">
						<asp:LinkButton ID="btnSave" runat="server" Text="Закончить курс" CommandName="Update" />
					</div>
				</div>
			</td>
		</tr>
	</EditItemTemplate>
</asp:ListView>
</n2:ChromeBox>
