<%@ Import Namespace="System.ComponentModel"%>
<%@ Control
		Language="C#"
		AutoEventWireup="true"
		ClassName="MyTrainings"
		Inherits="N2.Lms.Web.UI.MyAssignmentListControl`1[[N2.Lms.MyTrainingsDAO, LmsWeb]], N2.Lms" %>
<%@ Register Assembly="N2.Futures" Namespace="N2.Web.UI.WebControls" TagPrefix="n2" %>

<script runat="server">
	protected override void OnInit(EventArgs e)
	{/*
		this.rpt.ItemCommand += (_o, _e) => {
			if (string.Equals(_e.CommandName, "MarkComplete", StringComparison.InvariantCultureIgnoreCase)) {
				var _state = N2.Context.Persister.Get<ApprovedState>(int.Parse((string)_e.CommandArgument));
				var _request = _state.Parent;
				_request.PerformAction(
					"Finish",
					Profile.UserName,
					"Finished by " + Context.User.Identity.Name,
					new Dictionary<string, object>{{ "Grade", 5 }});
				this.BindData(_request.Parent as RequestContainer);
			}
		};
		
		//if (!this.IsPostBack) {
			this.BindData(this.CurrentItem.RequestContainer);
		//}
		*/
		base.OnInit(e);
	}
</script>

<asp:ObjectDataSource 
	ID="ds"
	runat="server"
	SelectMethod="FindAll"
	TypeName="N2.Lms.MyTrainingsDAO"
	onobjectcreating="ds_ObjectCreating">
</asp:ObjectDataSource>

<asp:Repeater runat="server" ID="rpt">
	<HeaderTemplate><table></HeaderTemplate>
	<FooterTemplate></table></FooterTemplate>
	<ItemTemplate>
	<tr><td><a href='<%# this.ResolveClientUrl(string.Concat("~/Lms/UI/Player.aspx?id=", Eval("Ticket.ID").ToString())) %>'><%# Eval("Ticket.Training") != null ? Eval("Ticket.Training.Title") : "Error: Training is NULL" %></a></td>
		<td><asp:ImageButton
					runat="server"
					ID="btnMarkComplete"
					ImageUrl="~/Lms/UI/Img/cancel.png"
					AlternateText="Закончить"
					CommandName="MarkComplete"
					CommandArgument='<%# Eval("ID") %>' /></td></tr>
	</ItemTemplate>
</asp:Repeater>

<n2:ChromeBox runat="Server">
<asp:ListView
		ID="lv"
		DataKeyNames="ID"
		runat="server"
		DataSourceID="ds">
	
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
