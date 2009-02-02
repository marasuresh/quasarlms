<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="N2.Lms.Items.Lms.RequestStates" %>
<%@ Import Namespace="N2.Workflow" %>
<%@ Import Namespace="N2.Workflow.Items" %>
<%@ Import Namespace="N2.Lms.Items.TrainingWorkflow" %>
<%@ Control Language="C#" Inherits="N2.Lms.Web.UI.MyAssignmentListControl" %>
<%@ Register Assembly="N2.Futures" Namespace="N2.Web.UI.WebControls" TagPrefix="n2" %>

<script runat="server">
    
    private string _command;

    protected override void OnInit(EventArgs e)
    {
        this.lv.ItemCommand += (_o, _e) =>
        {
            if (string.Equals(_e.CommandName, "Accept", StringComparison.InvariantCultureIgnoreCase))
            {
                _command = "Accept";
                lv.UpdateItem(int.Parse((string)_e.CommandArgument), true);
            }
            else if (string.Equals(_e.CommandName, "Decline", StringComparison.InvariantCultureIgnoreCase))
            {
                _command = "Decline";
                lv.UpdateItem(int.Parse((string)_e.CommandArgument), true);
            }


        };

        base.OnInit(e);
    }
    
    protected void lv_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        var _lv = sender as ListView;

        e.NewValues.Add("command", _command);
        e.NewValues.Add("comments", ((TextBox)_lv.EditItem.FindControl("tbComment")).Text);
        e.NewValues.Add("grade", ((TextBox)_lv.EditItem.FindControl("tbGrade")).Text);
        e.NewValues.Add("trainingID", ((DropDownList)_lv.EditItem.FindControl("ddlTrainings")).SelectedValue);
    }

	protected IEnumerable<ApprovedState> ListApprovedRequests(Request request)
	{
		return this.WorkflowProvider.GetHistory(request)
			.OfType<ApprovedState>();
	}
</script>

<asp:ObjectDataSource
		ID="dsRequests"
		runat="server"
		SelectMethod="FindRequestsToGrade"
		UpdateMethod="GoRequest"
		TypeName="N2.Lms.Items.MyAssignmentList"
		OnObjectCreating="ds_ObjectCreating">
	
	<UpdateParameters>
		<asp:Parameter Name="command" Type="String" />
		<asp:Parameter Name="comments" Type="String" />
		<asp:Parameter Name="grade" Type="String" />
		<asp:Parameter Name="trainingID" Type="String" />
	</UpdateParameters>

</asp:ObjectDataSource>
<asp:ListView
		ID="lv"
		DataKeyNames="ID"
		runat="server"
		DataSourceID="dsRequests"
		OnItemUpdating="lv_ItemUpdating">
	
	<LayoutTemplate>
	<table class="gridview" cellpadding="0" cellspacing="0">
		<tr class="header">
			<th></th>
			<th><asp:Localize runat="server" meta:resourcekey="TrainingColumn" /></th>
			<th><asp:Localize runat="server" meta:resourcekey="StudentColumn" /></th></tr>
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
				<td><%# ((Request)Container.DataItem).Course.Title %></td>
				<td><%# ((Request)Container.DataItem).User %></td>
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
				<td><%# ((Request)Container.DataItem).Course.Title %></td>
				<td><%# ((Request)Container.DataItem).User %></td>
            </tr>
			<tr><td class="edit" colspan="3">
					<div class="details">
						<asp:Localize runat="server" meta:resourcekey="History" />
						<n2:ChromeBox runat="server">
					<asp:Repeater runat="server" DataSource='<%# this.ListApprovedRequests((Request)Container.DataItem) %>'>
						<HeaderTemplate><table></HeaderTemplate>
						
						<FooterTemplate></table></FooterTemplate>
						
						<ItemTemplate>
							<tr><td><%# this.GetPlayerUrl(((ApprovedState)Container.DataItem).Ticket) %>
							</td></tr></ItemTemplate>
					</asp:Repeater>
						</n2:ChromeBox>
					</div>
					
					<div class="details">
						<div class="header">Edit details for '<%# Eval("Title")%>'</div>
						<table class="detailview" cellpadding="0" cellspacing="0">
							<tr><th><asp:Localize
											runat="server"
											meta:resourcekey="Comment" /></th>
								<td><%# this.GetRequestState((Request)Container.DataItem).Comment %></td></tr>
							<tr><th><asp:Label
											runat="server"
											meta:resourcekey="Grade"
											AssociatedControlID="tbGrade" /></th>
								<td><asp:TextBox
											runat="server"
											ID="tbGrade"
											ValidationGroup='Accept' />
									<asp:RequiredFieldValidator
											ID="rfv"
											runat="server"
											ControlToValidate="tbGrade"
											ErrorMessage="*"
											ValidationGroup='Accept'
											Display="Dynamic" />
									<asp:CompareValidator
											ID="CompareValidator1"
											runat="server"
											ControlToValidate="tbGrade"
											ErrorMessage="*"
											ValidationGroup='Accept'
											Display="Dynamic"
											Operator="DataTypeCheck"
											Type="Integer" /></td>
							</tr>
							<tr><th><asp:Label
											runat="server"
											meta:resourcekey="Remark"
											AssociatedControlID="tbComment" /></th>
								<td><asp:TextBox
											ID="tbComment"
											TextMode="MultiLine"
											runat="server" /></td></tr>
						</table>
						<ul class="buttons">
							<li><asp:LinkButton
									ID="btnSave"
									meta:resourcekey="btnSave"
									runat="server"
									CommandName="Accept"
									CommandArgument='<%# Container.DataItemIndex %>'
									ValidationGroup='Accept' /></li>
						</ul>
                        <br />
						<table	class="detailview"
								cellpadding="0"
								cellspacing="0">
							<tr><th><asp:Label
											runat="server"
											meta:resourcekey="lTraining"
											AssociatedControlID="ddlTrainings" /></th>
								<td><asp:DropDownList
											ID="ddlTrainings"
											runat="server"
											DataSource='<%# ((Request)Container.DataItem).Course.Trainings %>'
											DataValueField="ID"
											DataTextField="Title" /></td>
							</tr>
						</table>
						<ul class="buttons">
							<li><asp:LinkButton
									runat="server"
									meta:resourcekey="btnDecline"
									CommandName="Decline"
									CommandArgument='<%# Container.DataItemIndex %>' /></li>
                        </ul>
                    </div>
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>
