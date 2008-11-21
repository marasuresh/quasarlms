using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N2.Web.UI.WebControls
{
	using System.Linq;
	using N2.Workflow;
	using N2.Workflow.Items;
	using N2.Collections;
	using N2.Resources;

	[DefaultProperty("Text")]
	[ToolboxData("<{0}:AuditTrail runat=server></{0}:AuditTrail>")]
	public class AuditTrail : WebControl
	{
		#region Fields

		private ContentItem parentItem;
		
		#endregion Fields

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Register.StyleSheet(
				this.Page,
				this.Page.ClientScript.GetWebResourceUrl(
					typeof(Workflow),
					"N2.Workflow.Workflow.css"));
		}

		protected override void RenderContents(HtmlTextWriter output)
		{
			var _history = this.ParentItem.GetWorkflowHistory();

			if (_history.Any()) {
				var _rows =
					from _state in _history
					select new {
						Date = _state.Created,
						User = _state.SavedBy,
						From = _state.FromState.Name,
						FromIcon = _state.FromState.IconUrl,
						To = _state.ToState.Name,
						ToIcon = _state.ToState.IconUrl,
						ByAction = _state.Action.Name,
						Reason = _state.Comment,
						ReassignedTo = _state.AssignedTo,
					} into _trailData
					select new[] {
					_trailData.Date.ToLongDateString(),
					_trailData.User,
					string.Format(@"<img src='{0}' />&nbsp;{1}",
						this.ResolveClientUrl(_trailData.FromIcon),
						_trailData.From),
					string.Format(@"<img src='{0}' />&nbsp;{1}",
						this.ResolveClientUrl(_trailData.ToIcon),
						_trailData.To),
					_trailData.ByAction,
					_trailData.Reason,
					_trailData.ReassignedTo,
				} into _stringData

					let _row = "<tr>" + string.Join(string.Empty, (
						from _s in _stringData
						select "<td>" + _s + "</td>"
					).ToArray()) + "</tr>"

					select _row;


				output.Write(string.Format(@"
<table class='Workflow AuditTrail' cellpadding='0' cellspacing='0'>
	<tr><th>Date</th>
		<th>User</th>
		<th>From</th>
		<th>To</th>
		<th>By Action</th>
		<th>Reason</th>
		<th>Reassigned To</th></tr>
	{0}
</table>
", string.Join(System.Environment.NewLine, _rows.ToArray())));
			}
		}

		#region Properties
		
		/// <summary>Gets the parent item where to look for items.</summary>
		public int ParentItemID {
			get { return (int)(ViewState["CurrentItemID"] ?? 0); }
			set { ViewState["CurrentItemID"] = value; }
		}
		
		#endregion Properties
		
		#region IItemContainer Members

		public ContentItem ParentItem {
			get {
				return parentItem 
					?? (parentItem = N2.Context.Persister.Get(ParentItemID))
					?? (ParentItem = N2.Context.UrlParser.CurrentPage);
			}
			set {
				if (value == null)
					throw new ArgumentNullException("value");

				parentItem = value;
				ParentItemID = value.ID;
			}
		}

		#endregion
	}
}
