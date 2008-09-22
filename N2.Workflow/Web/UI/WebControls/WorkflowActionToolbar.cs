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

		public bool IsNewAction {
			get { return !string.IsNullOrEmpty(this.AddedTypeName); }
		}

		#endregion Workflow Properties

		#region Fields

		private DropDownList types;
		private PlaceHolder itemEditorsContainer;
		private ContentItem parentItem;
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

		protected string AddedTypeName {
			get { return (string)this.ViewState["AddedTypeName"]; }
			set { this.ViewState["AddedTypeName"] = value; }
		}

		/// <summary>Gets the parent item where to look for items.</summary>
		public int ParentItemID
		{
			get { return (int) (ViewState["CurrentItemID"] ?? 0); }
			set { ViewState["CurrentItemID"] = value; }
		}

		/// <summary>Gets the parent item where to look for items.</summary>
		public string ParentItemType
		{
			get { return (string) (ViewState["CurrentItemType"] ?? string.Empty); }
			set { ViewState["CurrentItemType"] = value; }
		}

		protected IDefinitionManager Definitions {
			get { return N2.Context.Definitions; }
		}

		public ItemDefinition CurrentItemDefinition {
			get { return Definitions.GetDefinition(Type.GetType(ParentItemType)); }
		}

		#endregion

		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			Trace.WriteLine("View State Loaded", "Workflow");
			this._CreateChildControls();
		}

		protected override void OnLoad(EventArgs e)
		{
			if (!this.Page.IsPostBack) {
				this._CreateChildControls();
			}
			base.OnLoad(e);
		}

		protected void _CreateChildControls()
		{
			this.Controls.Clear();
			
			this.Controls.Add(this.itemEditorsContainer = new PlaceHolder());
			
			if (this.IsNewAction) {
				Trace.WriteLine("Initializing empty Item Editor with type: " + this.AddedTypeName, "Workflow");
				this.InitStateEditor();
				this.UpdateItemEditor(
					this.CreateItem(
						Utility.TypeFromName(this.AddedTypeName)));
				this.AddCancelActionButton();
			} else {
				Trace.WriteLine("Action Select", "Workflow");
				this.InitActionDropDown();
			}
			
			this.ClearChildViewState();
		}

		private ContentItem CreateItem(Type itemType)
		{
			return N2.Context.Definitions.CreateInstance(itemType, ParentItem);
		}

		private void InitActionDropDown()
		{
			this.Controls.Add(this.actionSelectorContainer = new PlaceHolder());
			
			this.actionSelectorContainer.Controls.Add(types = new DropDownList());
			types.Items.AddRange((
					from _action in this.InitialState.ToState.Actions
					select new ListItem(_action.Title, _action.Name)
				).ToArray());

			ImageButton b = new ImageButton();
			this.actionSelectorContainer.Controls.Add(b);
			b.ImageUrl = Page.ClientScript.GetWebResourceUrl(typeof (ItemEditorList), "N2.Resources.add.gif");
			b.ToolTip = "Perform action";
			b.CausesValidation = false;
			b.Click += NewActionClick;
		}

		private void InitStateEditor()
		{
			itemEditorsContainer.Controls.Add(this.fs = new FieldSet());
			
			fs.Controls.Add(this.ItemEditor = new ItemEditor {
				ID = "ie"
			});

			Trace.WriteLine("Item editor created", "Workflow");
		}

		private void NewActionClick(object sender, ImageClickEventArgs e)
		{
			//Locate selected action by name, because DropDownList only supports items as strings
			ActionDefinition _selectedAction = this.InitialState
					.ToState
					.Actions
					.Single(_a => _a.Name == types.SelectedValue);

			Type _itemType = _selectedAction.StateType ?? typeof(ItemState);
			ItemState item = CreateItem(_itemType) as ItemState;
			item.ToState = _selectedAction.LeadsTo;

			//not required
			item.Action = _selectedAction;
			item.FromState = this.InitialState.ToState;
			
			this.AddedTypeName = string.Format("{0}", _itemType.AssemblyQualifiedName);
			this.InitStateEditor();
			UpdateItemEditor(item);
			this.actionSelectorContainer.Visible = false;
			this.AddCancelActionButton();
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
			ImageButton b = (ImageButton)sender;
			b.Visible = false;
			
			//this.ParentItem.AssignCurrentState(this.InitialState);
			this.ClearChildState();
			this.itemEditorsContainer.Visible = false;
			this.AddedTypeName = null;
			this.InitActionDropDown();
			this.actionSelectorContainer.Visible = true;
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
				return parentItem 
					?? (parentItem = N2.Context.Persister.Get(ParentItemID));
			}
			set {
				if (value == null)
					throw new ArgumentNullException("value");

				parentItem = value;
				ParentItemID = value.ID;
				ParentItemType = value.GetType().AssemblyQualifiedName;
			}
		}

		#endregion
	}
}
