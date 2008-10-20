using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N2.Web.UI.WebControls
{
	using N2.Workflow;
	using N2.Workflow.Items;
	
	[ToolboxData("<{0}:WorkflowLegend runat=server></{0}:WorkflowLegend>")]
	public class WorkflowLegend : WebControl
	{
		protected override void  CreateChildControls()
		{
 			base.CreateChildControls();
			this.Controls.Clear();
			
			if (null != this.ParentItem) {
				Workflow _workflow =
					this.ParentItem is IWorkflowItemContainer
						? ((IWorkflowItemContainer)this.parentItem).Workflow
						: this.ParentItem.GetWorkflow();

				this.Controls.Add(new LiteralControl(this.ParentItem.Title));

				if (null != _workflow && null != _workflow.InitialState) {
				
					this.Controls.Add(N2.Web.Tree
						.From(new LegendNode((Workflow)_workflow.Clone(true)))
						.ToControl());
				}
			}
		}

		private ContentItem parentItem;
		
		/// <summary>Gets the parent item where to look for items.</summary>
		public int ParentItemID
		{
			get { return (int)(ViewState["CurrentItemID"] ?? 0); }
			set { ViewState["CurrentItemID"] = value; }
		}

		public ContentItem ParentItem {
			get {
				return parentItem
					?? (parentItem = N2.Context.Persister.Get(ParentItemID));
			}
			set {
				if (value == null)
					throw new ArgumentNullException("value");

				parentItem = value;
				ParentItemID = value.ID;
			}
		}

		class LegendNode : ContentItem
		{
			LegendNode() { }

			readonly ContentItem m_item;
			LegendNode(ContentItem item)
			{
				this.m_item = item;
			}
			
			LegendNode(StateDefinition item): this((ContentItem)item)
			{
				item["mark"] = true;//remember to skip on next loop
				this.Children = (
					from _action in item.Actions
					select new LegendNode(_action)
				).Cast<ContentItem>().ToList();
			}

			LegendNode(ActionDefinition action)
				: this((ContentItem)action)
			{
				//Prevent eternal cycle by skipping marked states
				if ((bool?)action.LeadsTo["mark"] ?? false) {
					this.Children.Add(new LegendNode(action.LeadsTo));
				} else {
					this.Children.Add(new LegendNode((ContentItem)action.LeadsTo));
				}
			}

			public LegendNode(Workflow workflow)
				: this(workflow.InitialState)
			{
			}

			public override string IconUrl { get { return this.m_item.IconUrl; } }

			public override string Name
			{
				get { return this.m_item.Name; }
				set { throw new NotImplementedException(); }
			}

			public override string Title {
				get {
					return
						this.m_item is StateDefinition
					  ? string.Concat(
							"<img src='",
							(HttpContext.Current.CurrentHandler as Page).ResolveClientUrl(this.m_item.IconUrl),
							"' /> ",
							this.m_item.Name)
					  : this.m_item.Name;
				}
				set { throw new NotImplementedException(); }
			}

			public override string Url
			{
				get { return this.m_item.Url; }
			}
		}
	}
}
