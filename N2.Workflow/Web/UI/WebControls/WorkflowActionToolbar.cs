using System;
using System.Diagnostics;

namespace N2.Web.UI.WebControls
{
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using N2.Web.UI;
	using N2.Workflow;
	using N2.Workflow.Items;
	using System.Linq;
	using System.Collections.Generic;
	using N2.Definitions;
	
	/// <summary>
	/// 
	/// </summary>
	/// <see cref="N2.Web.UI.WebControls.ItemEditorList"/>
	public class WorkflowActionToolbar: CompositeControl, INamingContainer
	{
		#region Workflow Properties

		/// <summary>
		/// A wrapper around initial item state, guaranteed to be not null.
		/// </summary>
		public ItemState InitialState {
			get {
				return this.ParentItem.GetCurrentState();
			}
		}

		#endregion Workflow Properties

		#region Fields

		private DropDownList types;
		private PlaceHolder itemEditorsContainer;
		ContentItem m_parentItem;
		FieldSet fs;

		PlaceHolder actionSelectorContainer;
		#endregion

		#region Constructor

		public WorkflowActionToolbar()
		{
			CssClass = "itemListEditor";
		}

		#endregion

		#region Properties

		public ItemEditor ItemEditor { get; protected set; }

		/// <summary>Gets the parent item where to look for items.</summary>
		public int ParentItemID
		{
			get { return (int) (ViewState["CurrentItemID"] ?? 0); }
			set { ViewState["CurrentItemID"] = value; }
		}

		public bool WorkflowActionPerformed {
			get { return !string.IsNullOrEmpty(this.NewStateTypeName); }
		}

		protected string NewStateTypeName {
			get { return (string)this.ViewState["newStateTypeName"]; }
			set { this.ViewState["newStateTypeName"] = value; }
		}

		#endregion

/*		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			Trace.WriteLine("View State Loaded", "Workflow");
			this._CreateChildControls();
		}
*/
		ItemState GetNewStateStub()
		{
			if (!string.IsNullOrEmpty(this.NewStateTypeName)) {

				ItemState _itemState = (ItemState)this.CreateItem(Utility.TypeFromName(this.NewStateTypeName));

				int[] _itemStateProperties = this.ViewState["itemStateProperties"] as int[];

				if (_itemStateProperties != null && 3 == _itemStateProperties.Length) {
					_itemState.FromState = N2.Context.Persister.Get<StateDefinition>(_itemStateProperties[0]);
					_itemState.Action = N2.Context.Persister.Get<ActionDefinition>(_itemStateProperties[1]);
					_itemState.ToState = N2.Context.Persister.Get<StateDefinition>(_itemStateProperties[2]);
				}

				Trace.WriteLine("Created empty state stub: " + this.NewStateTypeName, "Workflow");

				return _itemState;
			} else {
				return null;
			}
		}

		void SaveNewStateStub(ItemState itemState)
		{
			this.SaveNewStateStub(
				itemState.GetType().AssemblyQualifiedName,
				itemState.FromState.ID,
				itemState.Action.ID,
				itemState.ToState.ID);
		}

		void SaveNewStateStub(string typeName, int fromStateId, int actionId, int toStateId)
		{
			this.NewStateTypeName = typeName;
			this.ViewState["itemStateProperties"] = new int[] {
				fromStateId,
				actionId,
				toStateId
			};
		}

		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			
			this.Controls.Clear();
			
			this.Controls.Add(this.itemEditorsContainer = new PlaceHolder());

			if (this.WorkflowActionPerformed) {
				this.InitStateEditor();
				this.UpdateItemEditor(this.GetNewStateStub());
				this.AddCancelActionButton();
			} else {
				Trace.WriteLine("Action Select", "Workflow");
				this.InitActionSelector();
			}
			
			this.ClearChildViewState();
		}

		private ContentItem CreateItem(Type itemType)
		{
			return N2.Context.Definitions.CreateInstance(itemType, ParentItem);
		}

		private void InitActionSelector()
		{
			var _actionQuery =
				from _action in this.InitialState.ToState.Actions
				select new ListItem(System.Web.HttpUtility.HtmlDecode(_action.Title), _action.Name);

			if (_actionQuery.Any()) {
				this.Controls.Add(this.actionSelectorContainer = new PlaceHolder());

				this.actionSelectorContainer.Controls.Add(types = new DropDownList());
				types.Items.AddRange(_actionQuery.ToArray());

				ImageButton b = new ImageButton();
				this.actionSelectorContainer.Controls.Add(b);
				b.ImageUrl = Page.ClientScript.GetWebResourceUrl(typeof(ItemEditorList), "N2.Resources.add.gif");
				b.ToolTip = "Perform action";
				b.CausesValidation = false;
				b.Click += NewActionClick;
			}
		}

		private void NewActionClick(object sender, ImageClickEventArgs e)
		{
			//Locate selected action by name, because DropDownList only supports items as strings
			ActionDefinition _selectedAction = this.InitialState
					.ToState
					.Actions
					.Single(_a => _a.Name == types.SelectedValue);

			Type _itemType = _selectedAction.StateType ?? typeof(ItemState);
			
			this.SaveNewStateStub(
				_itemType.AssemblyQualifiedName,
				this.InitialState.ToState.ID,
				_selectedAction.ID,
				_selectedAction.LeadsTo.ID);
			
			//force framework to regenerate the hierarchy before rendering
			this.ChildControlsCreated = false;
		}

		private void InitStateEditor()
		{
			itemEditorsContainer.Controls.Add(this.fs = new FieldSet());
			
			fs.Controls.Add(this.ItemEditor = new ItemEditor {
				ID = "ie"
			});

			Trace.WriteLine("Item editor created", "Workflow");
		}

		private void AddCancelActionButton()
		{
			ImageButton b = new ImageButton();
			itemEditorsContainer.Controls.Add(b);
			b.ID = "del";
			b.CssClass = " delete";
			b.ImageUrl = Page.ClientScript.GetWebResourceUrl(typeof (ItemEditorList), "N2.Resources.delete.gif");
			b.ToolTip = "Cancel Action";
			b.Click += CancelActionClick;
		}

		private void CancelActionClick(object sender, ImageClickEventArgs e)
		{
//			ImageButton b = (ImageButton)sender;
//			b.Visible = false;
			
			this.ClearChildState();
			this.NewStateTypeName = null;
			this.ChildControlsCreated = false;
		}

		private ItemEditor UpdateItemEditor(ContentItem item)
		{
			this.fs.Legend = N2.Context.Definitions.GetDefinition(item.GetType()).Title;
			this.ItemEditor.CurrentItem = item;
			this.ItemEditor.ParentPath = this.ParentItem.Path;
			return this.ItemEditor;
		}

		#region IItemContainer Members

		public ContentItem ParentItem {
			get {
				return this.m_parentItem 
					?? (this.m_parentItem = N2.Context.Persister.Get(this.ParentItemID));
			}
			set {
				if (value == null) {
					throw new ArgumentNullException("value");
				}
				this.m_parentItem = value;
				this.ParentItemID = value.ID;
			}
		}

		#endregion
	}
}
