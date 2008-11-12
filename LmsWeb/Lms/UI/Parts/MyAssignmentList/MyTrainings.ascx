<%@ Import Namespace="N2.Lms.Items.Lms.RequestStates"%>
<%@ Import Namespace="N2.Workflow"%>
<%@ Import Namespace="System.ComponentModel"%>
<%@ Control
		Language="C#"
		AutoEventWireup="true"
		ClassName="MyTrainings"
		Inherits="N2.Lms.Web.UI.MyAssignmentListControl" %>
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
	SelectMethod="FindApprovedRequests"
	UpdateMethod="GoRequest"
	TypeName="N2.Lms.Items.MyAssignmentList"
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
			<td class="command">
				<asp:ImageButton
						ID="ImageButton1"
						runat="server"
						ImageUrl="~/Lms/UI/Img/clear.gif"
						CssClass="LibC_c"
						AlternateText="детали..."
						CommandName="Edit" /></td>
			<td><a target="_blank" href='<%# this.ResolveClientUrl("~/Lms/UI/Player.aspx?id=" + ((ApprovedState)((Request)Container.DataItem).GetCurrentState()).Ticket.ID).ToString()%>'><%# ((ApprovedState)((Request)Container.DataItem).GetCurrentState()).Training.Title%></a></td>
		</tr>
	</ItemTemplate>
	
	<EditItemTemplate>
		<tr class='edit-info <%# Container.DataItemIndex == 0 ? "first" : string.Empty %>'>
			<td class="command">
				<asp:ImageButton
						ID="btnCancel"
						runat="server"
						ImageUrl="~/Lms/UI/Img/clear.gif"
						CssClass="LibC_o"
						CommandName="Cancel" /></td>
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
						<asp:Image runat="server" ImageUrl="~/Edit/Img/ico/accept.gif" />
						<asp:LinkButton ID="btnSave" runat="server" Text="Закончить курс" CommandName="Update" />
					</div>
				</div>
			</td>
		</tr>
	</EditItemTemplate>
</asp:ListView>
</n2:ChromeBox>
