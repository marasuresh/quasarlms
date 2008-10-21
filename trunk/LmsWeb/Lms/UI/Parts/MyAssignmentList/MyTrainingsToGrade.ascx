<%@ Control
		Language="C#"
		AutoEventWireup="true"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.MyAssignmentList, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="N2.Lms.Items.Lms.RequestStates" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>
<%@ Import Namespace="N2.Lms.Items.TrainingWorkflow" %>

<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		this.rpt.ItemCommand += (_o, _e) => {
			
			var _request = N2.Context.Persister.Get<Request>(int.Parse((string)_e.CommandArgument));

			switch (_e.CommandName.ToLower()) {
				case "grade":
					TextBox _tb = ((Control)_e.CommandSource).NamingContainer.FindControl("tbGrade") as TextBox;

					int _grade = int.Parse(_tb.Text);

					_request.PerformAction(
						"Accept",
						Profile.UserName,
						string.Concat("Graded by ", Context.User.Identity.Name, " for ", _grade),
						new Dictionary<string, object>{{
							"Grade", _grade
						}});
					this.BindData(_request.Parent as RequestContainer);
					break;
				case "replay":
					_request.PerformAction(
						"Decline",
						Profile.UserName,
						string.Concat("Declined by ", Context.User.Identity.Name),
						null);
					this.BindData(_request.Parent as RequestContainer);
					break;
			}
		};
		
		if (!this.IsPostBack) {
			this.BindData(this.CurrentItem.RequestContainer);
		}
		
		base.OnInit(e);
	}

	protected override void OnLoad(EventArgs e)
	{
		
		base.OnLoad(e);
	}

	void BindData(RequestContainer rc)
	{
		this.rpt.DataSource = rc.MyFinishedAssignments;
		this.rpt.DataBind();
	}
</script>

<asp:Repeater runat="server" ID="rpt">
	<HeaderTemplate><table></HeaderTemplate>
	<FooterTemplate></table></FooterTemplate>
	<ItemTemplate>
	
	<tr><td><%# Eval("Course.Title") %></td>
		<td><asp:TextBox
				runat="server"
				ID="tbGrade"
				ValidationGroup='<%# "vg" + Eval("ID") %>' />
			<asp:RequiredFieldValidator
					runat="server"
					ControlToValidate="tbGrade"
					ErrorMessage="*"
					ValidationGroup='<%# "vg" + Eval("ID") %>'
					Display="Dynamic" />
			<asp:CompareValidator
					runat="server"
					ControlToValidate="tbGrade"
					ErrorMessage="*"
					ValidationGroup='<%# "vg" + Eval("ID") %>'
					Display="Dynamic"
					Operator="DataTypeCheck"
					Type="Integer" /></td>
		<td><asp:ImageButton
				runat="server"
				ImageUrl="~/Lms/UI/Img/accept.png"
				CommandName="Grade"
				ValidationGroup='<%# "vg" + Eval("ID") %>'
				CausesValidation="true"
				CommandArgument='<%# Eval("ID") %>' />
			<asp:ImageButton
				runat="server"
				ImageUrl="~/Lms/UI/Img/arrow_undo.png"
				CommandName="Replay"
				CommandArgument='<%# Eval("ID") %>' /></td></tr>
	</ItemTemplate>
</asp:Repeater>