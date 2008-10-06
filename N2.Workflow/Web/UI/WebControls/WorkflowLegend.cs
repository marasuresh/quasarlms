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
	
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:WorkflowLegend runat=server></{0}:WorkflowLegend>")]
	public class WorkflowLegend : CompositeControl
	{
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			
			if (null != this.ParentItem) {
				Workflow _workflow =
					this.ParentItem is IWorkflowItemContainer
						? ((IWorkflowItemContainer)this.parentItem).Workflow
						: this.ParentItem.GetWorkflow();

				this.Controls.Add(new LiteralControl(this.ParentItem.Title));

				if (null != _workflow) {
					
					this.Controls.Add(N2.Web.Tree
						.From(new LegendNode(_workflow))
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

			LegendNode(StateDefinition item)
				: this((ContentItem)item)
			{
				this.Children = (
					from _action in item.Actions
					select new LegendNode(_action)
				).Cast<ContentItem>().ToList();
			}

			LegendNode(ActionDefinition action)
				: this((ContentItem)action)
			{
				//Prevent eternal cycle
				if (action.LeadsTo != ((Workflow)action.LeadsTo.Parent).InitialState) {
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

			public override string Title
			{
				get
				{
					return
						this.m_item is StateDefinition
					  ? string.Format("<img src='{0}' /> {1}",
							  this.m_item.IconUrl,
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
