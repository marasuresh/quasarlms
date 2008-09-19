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
	public class WorkflowActionToolbar: WebControl
	{
		#region Workflow Properties

		/// <summary>
		/// Item's state. It can be either persisted value OR new value just assigned by the user.
		/// </summary>
		public ItemState CurrentState {
			get { return this.parentItem.GetCurrentState(); }
		}

		/// <summary>
		/// The state in which current item remains before user starts using this control.
		/// Because item state is supposed to change after user interection,
		/// we need to hold initial value in the view state.
		/// </summary>
		protected ItemState InitialStateStore
		{
			get { return ViewState["InitialState"] as ItemState; }
			set { this.ViewState["InitialState"] = value; }
		}

		/// <summary>
		/// A wrapper around initial item state, guaranteed to be not null.
		/// </summary>
		public ItemState InitialState {
			get {
				return this.InitialStateStore
					?? (this.InitialStateStore = this.CurrentState);
			}
		}

		public bool IsNewAction {
			get { return this.InitialState != this.CurrentState; }
		}

		#endregion Workflow Properties

		#region Fields

		private DropDownList types;
		private PlaceHolder itemEditorsContainer;
		private ContentItem parentItem;
		private List<string> addedTypes = new List<string>();
		private List<int> deletedIndexes = new List<int>();
		FieldSet fs;

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

		/// <summary>Gets the parent item where to look for items.</summary>
		public string ParentItemType
		{
			get { return (string) (ViewState["CurrentItemType"] ?? string.Empty); }
			set { ViewState["CurrentItemType"] = value; }
		}

		public IList<string> AddedTypes
		{
			get { return addedTypes; }
		}

		public IList<int> DeletedIndexes
		{
			get { return deletedIndexes; }
		}

		protected IDefinitionManager Definitions {
			get { return N2.Context.Definitions; }
		}

		public ItemDefinition CurrentItemDefinition {
			get { return Definitions.GetDefinition(Type.GetType(ParentItemType)); }
		}

		#endregion

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			Controls.Add(this.itemEditorsContainer = new PlaceHolder());

			this.InitActionDropDown();
			this.InitStateEditor();
			//this.AddCancelActionButton();

			this.ItemEditor.Visible = this.IsNewAction;
		}

		protected override void LoadViewState(object savedState)
		{
			Triplet p = (Triplet)savedState;
			base.LoadViewState(p.First);
			addedTypes = (List<string>)p.Second;
			deletedIndexes = (List<int>)p.Third;
			EnsureChildControls();

			Console.WriteLine("addedTypes: " + addedTypes.Count + ", deletedIndexes: " + deletedIndexes.Count);
		}

		protected override void CreateChildControls()
		{
			types.Items.AddRange((
				from _action in this.InitialState.ToState.Actions
				select new ListItem(_action.Title, _action.Name)
			).ToArray());

			base.CreateChildControls();
		}

		protected override object SaveViewState()
		{
			return new Triplet(base.SaveViewState(), addedTypes, deletedIndexes);
		}

		private ContentItem CreateItem(Type itemType)
		{
			return N2.Context.Definitions.CreateInstance(itemType, ParentItem);
		}

		private void InitActionDropDown()
		{
			types = new DropDownList();
			Controls.Add(types);

			ImageButton b = new ImageButton();
			Controls.Add(b);
			b.ImageUrl = Page.ClientScript.GetWebResourceUrl(typeof (ItemEditorList), "N2.Resources.add.gif");
			b.ToolTip = "Perform action";
			b.CausesValidation = false;
			b.Click += NewActionClick;
		}

		private void InitStateEditor()
		{
			itemEditorsContainer.Controls.Add(this.fs = new FieldSet());
			fs.Controls.Add(this.ItemEditor = new ItemEditor {
				ID = ID + "_ie",
				Visible = false,
			});
		}

		private void NewActionClick(object sender, ImageClickEventArgs e)
		{
			ActionDefinition _selectedAction = this.InitialState
					.ToState
					.Actions
					.Single(_a => _a.Name == types.SelectedValue);

			ItemState item = CreateItem(_selectedAction.StateType ?? typeof(ItemState)) as ItemState;
			item.ToState = _selectedAction.LeadsTo;

			//not required
			item.Action = _selectedAction;
			item.FromState = this.InitialState.ToState;

			//this.ParentItem.AssignCurrentState(item);
			UpdateItemEditor(item);
		}

		private void AddCancelActionButton()
		{
			ImageButton b = new ImageButton();
			itemEditorsContainer.Controls.Add(b);
			b.ID = ID + "_d";
			b.CssClass = " delete";
			b.ImageUrl = Page.ClientScript.GetWebResourceUrl(typeof (ItemEditorList), "N2.Resources.delete.gif");
			b.ToolTip = "Cancel Action";
			b.Click += CancelActionClick;
		}

		private void CancelActionClick(object sender, ImageClickEventArgs e)
		{
			ImageButton b = (ImageButton)sender;
			b.Enabled = false;
			b.CssClass += " disabled";
			
			this.ParentItem.AssignCurrentState(this.InitialState);
			
			this.ItemEditor.Visible = false;
			this.ItemEditor.CssClass += " disabled";
		}

		private ItemEditor UpdateItemEditor(ContentItem item)
		{
			this.fs.Legend = N2.Context.Definitions.GetDefinition(item.GetType()).Title;
			this.ItemEditor.CurrentItem = item;
			this.ItemEditor.ParentPath = this.ParentItem.Path;
			this.ItemEditor.Visible = true;
			//private void AddCancelActionButton()
			return this.ItemEditor;
		}

		#region IItemContainer Members

		public ContentItem ParentItem
		{
			get
			{
				return parentItem 
					?? (parentItem = N2.Context.Persister.Get(ParentItemID));
			}
			set
			{
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
