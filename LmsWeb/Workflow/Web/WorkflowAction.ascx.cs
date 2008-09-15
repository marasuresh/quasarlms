using System.Diagnostics;
using System;
using System.Linq;
using N2.Web.UI;
using System.Web.UI.WebControls;
using N2.Web.UI.WebControls;
using N2.Workflow;
using N2.Resources;

public partial class WorkflowAction: ContentUserControl, IWorkflowActionControl
{
	#region Properties
	
	public string Comment { get {
		return this.tbComment.Text;
	}}

	public string SelectedAction { get {
		return this.ddlAction.SelectedValue;
	}}

	#endregion Properties

	protected void RegisterScripts()
	{
		Register.JQuery(this.Page);
		Register.JavaScript(this.Page, "~/Workflow/UI/js/jquery.jdrop.js");
		Register.JavaScript(
			this.Page,
			string.Format(@"$('#{0}').jdrop();
$('#{0} option').each(function(){{
	$(this).html($(this).html().replace(/&lt;/,'<').replace(/&gt;/,'>'));}});
",
				this.ddlAction.ClientID,
				this.tbComment.ClientID),
			ScriptOptions.DocumentReady);
		Register.StyleSheet(this.Page, "~/Workflow/UI/css/screen.css");
	}

	protected override void OnInit(System.EventArgs e)
	{
		base.OnInit(e);

		this.RegisterScripts();

		var _item = this.CurrentItem ?? N2.Web.UI.ItemUtility.FindCurrentItem(this.Parent);

		var _currentState = _item.GetCurrentState();

		//if (!this.IsPostBack) {
		this.ddlAction.DataSource =
			from _action in _currentState.ToState.Actions
			select new {
				Text = string.Format("<img src='{0}' /> {1}",
					this.ResolveClientUrl(_action.LeadsTo.Icon),
					_action.Title),
				Value = _action.Name,
			};
		this.ddlAction.DataBind();
		//}
	}

	protected void ddlAction_PreRender(object sender, EventArgs e)
	{
		foreach (var _item in ((DropDownList)sender).Items.Cast<ListItem>()) {
			_item.Text = System.Web.HttpUtility
				.HtmlDecode(_item.Text)
				.Replace("&lt;", "<")
				.Replace("&gt;", ">");
		}
	}
}
