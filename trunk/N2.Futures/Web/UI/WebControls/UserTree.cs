using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using fx = System.Web.UI.WebControls;

namespace N2.Web.UI.WebControls
{
	using System.Diagnostics;
	using System.Web.Security;

	[ToolboxData("<{0}:UserTree runat=\"server\"></{0}:UserTree>")]
	public class UserTree : fx.CompositeControl
	{
		#region Constructors

		public UserTree()
		{
			//Set non-automatic properties via storage variables !
			this.DisplayEmptyRoles = true;
			this.m_expandRoles = true;
			this.AllowMultipleSelection = false;
			this.m_displayMode = DisplayModeEnum.UsersAndRoles;
			this.SelectionMode = DisplayModeEnum.Users;
			this.UserFilterType = FilterTypeEnum.None;
			this.RoleFilterType = FilterTypeEnum.None;
		}
		
		#endregion Constructors

		#region Events

		public event EventHandler SelectionChanged;
		
		#endregion Events

		#region Properties

		[Browsable(false)]
		public string SelectedUser {
			get {
				Debug.WriteLine("SelectedUser_get", "UserTree");
				Debug.WriteLine("SelectedUser_get: ChildControlsCreated = " + this.ChildControlsCreated.ToString(), "UserTree");
				this.EnsureChildControls();//should be true anyway, if caused by OnSelectionChanged
				Debug.WriteLine("SelectedUser_get: " + this.TreeView.SelectedValue, "UserTree");
				return this.AllowMultipleSelection
					? GetMutltipleSelection()
					: this.TreeView.SelectedValue;
			}
			set {
				//this.EnsureChildControls();
				//this.TreeView..ViewState["SelectedUser"] = value;
			}
		}

		[Browsable(true)]
		[DefaultValue(true)]
		public bool DisplayEmptyRoles { get; set; }

		bool m_expandRoles;
		[Browsable(true)]
		[DefaultValue(true)]
		public bool ExpandRoles {
			get { return this.m_expandRoles; }
			set {
				if (this.m_expandRoles != value) {
					this.m_expandRoles = value;
					this.BindingPerformed = false;
				}
			}
		}

		[Browsable(true)]
		[DefaultValue(false)]
		public bool AllowMultipleSelection { get; set; }

		DisplayModeEnum m_displayMode;
		[Browsable(true)]
		[DefaultValue(typeof(DisplayModeEnum), "UsersAndRoles")]
		public DisplayModeEnum DisplayMode {
			get { return this.m_displayMode; }
			set {
				if (this.m_displayMode != value) {
					this.m_displayMode = value;
					this.BindingPerformed = false;
				}
			}
		}

		[Browsable(true)]
		[DefaultValue(typeof(DisplayModeEnum), "Users")]
		public DisplayModeEnum SelectionMode { get; set; }

		[Browsable(true)]
		public string UserFilter { get; set; }

		[Browsable(true)]
		[DefaultValue(typeof(FilterTypeEnum), "None")]
		public FilterTypeEnum UserFilterType { get; set; }

		[Browsable(true)]
		public string RoleFilter { get; set; }

		[Browsable(true)]
		[DefaultValue(typeof(FilterTypeEnum), "None")]
		public FilterTypeEnum RoleFilterType { get; set; }

		#endregion Properties

		#region Internal properties

		protected bool BindingPerformed {
			get { return (bool?)this.ViewState["!_bound"] ?? false; }
			set { this.ViewState["!_bound"] = value; }
		}
		
		#endregion Internal properties

		#region Control references

		protected fx.TreeView TreeView;
		protected fx.Button SelectButton;

		#endregion Control references

		#region Tree node references

		IEnumerable<fx.TreeNode> m_roleNodesRef;
		IEnumerable<fx.TreeNode> RoleNodesRef
		{
			get { return this.m_roleNodesRef
				?? (this.m_roleNodesRef = this.GetRoleNodeRef().ToArray());
			}
		}

		IEnumerable<fx.TreeNode> GetRoleNodeRef()
		{
			return 
				this.DisplayMode == DisplayModeEnum.Roles
					|| this.DisplayMode == DisplayModeEnum.UsersAndRoles
					?	from _node in this.TreeView.Nodes.Cast<fx.TreeNode>()
						where _node.ImageUrl == RoleImageUrl
						select _node
					: new fx.TreeNode[0];
		}

		IEnumerable<fx.TreeNode> m_userNodeRef;
		IEnumerable<fx.TreeNode> UserNodesRef
		{
			get {
				return this.m_userNodeRef
					?? (this.m_userNodeRef = this.GetUserNodesRef().ToArray());
			}
		}

		IEnumerable<fx.TreeNode> GetUserNodesRef()
		{
			if (this.DisplayMode == DisplayModeEnum.Users
					|| this.DisplayMode == DisplayModeEnum.UsersAndRoles) {
				
				var _result =
					from _node in this.TreeView.Nodes.Cast<fx.TreeNode>()
					where _node.ImageUrl == UserImageUrl
					select _node;

				if (this.ExpandRoles) {
					_result = _result.Concat(
						from _role in this.RoleNodesRef
						from _user in _role.ChildNodes.Cast<fx.TreeNode>()
						select _user);
				}

				return _result;
			} else {
				return new fx.TreeNode[0];
			}
		}

		#endregion Tree node references

		#region ViewState Management

		protected override void LoadViewState(object savedState)
		{
			Pair _pair = savedState as Pair;

			if (null != _pair) {
				base.LoadViewState(_pair.First);
				object[] _values = _pair.Second as object[];
				int i = 0;
				//Set non-automatic properties via storage variables !
				this.DisplayEmptyRoles = (bool)_values[i++];
				this.m_expandRoles = (bool)_values[i++];
				this.AllowMultipleSelection = (bool)_values[i++];
				this.m_displayMode = (DisplayModeEnum)_values[i++];
				this.SelectionMode = (DisplayModeEnum)_values[i++];
				this.UserFilter = (string)_values[i++];
				this.UserFilterType = (FilterTypeEnum)_values[i++];
				this.RoleFilter = (string)_values[i++];
				this.RoleFilterType = (FilterTypeEnum)_values[i++];

			} else {
				base.LoadViewState(savedState);
			}
		}

		protected override object SaveViewState()
		{
			return new Pair(
				base.SaveViewState(),
				new object[] {
					this.DisplayEmptyRoles,
					this.ExpandRoles,
					this.AllowMultipleSelection,
					this.DisplayMode,
					this.SelectionMode,
					this.UserFilter,
					this.UserFilterType,
					this.RoleFilter,
					this.RoleFilterType
				});
		}

		#endregion ViewState Management

		#region Methods

		void SetChildProperties()
		{
			this.EnsureChildControls();

			fx.TreeView _control = this.TreeView;

			_control.ShowCheckBoxes =
				this.AllowMultipleSelection
					? fx.TreeNodeTypes.All
					: fx.TreeNodeTypes.None;

			_control.SelectedNodeStyle.Font.Bold = true;

			_control.CopyBaseAttributes(this);
			_control.ApplyStyle(base.ControlStyle);
			_control.Visible = true;

			var _selectionMode = this.SelectionMode;
			
			var _roleNodeSelectAction =
				(_selectionMode == DisplayModeEnum.Roles
					|| _selectionMode == DisplayModeEnum.UsersAndRoles)
				&& !this.AllowMultipleSelection
					? fx.TreeNodeSelectAction.Select
					: fx.TreeNodeSelectAction.Expand;

			var _roleNodeCheckBoxes =
				(_selectionMode == DisplayModeEnum.Roles
					|| _selectionMode == DisplayModeEnum.UsersAndRoles)
				&& this.AllowMultipleSelection;
			
			foreach (var _roleNode in this.RoleNodesRef) {
				_roleNode.SelectAction = _roleNodeSelectAction;
				_roleNode.ShowCheckBox = _roleNodeCheckBoxes;
			}

			var _userNodeCheckBoxes =
				(_selectionMode == DisplayModeEnum.Users
					|| _selectionMode == DisplayModeEnum.UsersAndRoles)
				&& this.AllowMultipleSelection;

			var _userNodeSelectAction =
				(_selectionMode == DisplayModeEnum.Users
					|| _selectionMode == DisplayModeEnum.UsersAndRoles)
				&& !this.AllowMultipleSelection
					? fx.TreeNodeSelectAction.Select
					: fx.TreeNodeSelectAction.None;

			foreach (var _userNode in this.UserNodesRef) {
				_userNode.SelectAction = _userNodeSelectAction;
				_userNode.ShowCheckBox = _userNodeCheckBoxes;
			}

			this.SelectButton.Visible = this.AllowMultipleSelection;
		}

		protected override void CreateChildControls()
		{
			Debug.WriteLine("CreateChildControls", "UserTree");

			base.CreateChildControls();
			this.TreeView = new fx.TreeView {
				ID = "tv",
				ShowLines = true
			};
			
			this.TreeView.SelectedNodeChanged += this.TreeView_SelectedNodeChanged;
			this.Controls.Add(this.TreeView);

			this.SelectButton = new fx.Button {
				ID = "btnSelect",
				Visible = false,
				Text = "Select",
				EnableViewState = false,
			};

			this.SelectButton.Click += this.SelectButton_Click;
			this.Controls.Add(this.SelectButton);

			this.ClearChildViewState();
		}

		string GetMutltipleSelection()
		{
			var _selection = ((
				from _user in this.UserNodesRef
				where _user.Checked
				select _user.Value).Concat(
					from _role in this.RoleNodesRef
					where _role.Checked
					select _role.Value
				)).Distinct().ToArray();

			return string.Join(";", _selection);
		}
		
		void AddRoles()
		{
			foreach (var _role in Roles.GetAllRoles()) {
				
				var _usersInRole = Roles.GetUsersInRole(_role);

				if (_usersInRole.Any() || this.DisplayEmptyRoles) {

					fx.TreeNode _roleNode = new fx.TreeNode {
						Text = _role,
						Value = _role,
						ImageUrl = RoleImageUrl,
					};

					if (this.ExpandRoles
						&& (this.DisplayMode == DisplayModeEnum.Users
							|| this.DisplayMode == DisplayModeEnum.UsersAndRoles)) {
						this.AppendUserNodes(_roleNode.ChildNodes, _usersInRole);
					}

					this.TreeView.Nodes.Add(_roleNode);
				}
			}
		}

		void AddUsers()
		{
			this.AppendUserNodes(
				this.TreeView.Nodes,
				Membership.GetAllUsers().Cast<MembershipUser>().Select(_user => _user.UserName));
		}

		void AppendUserNodes(fx.TreeNodeCollection collection, IEnumerable<string> userNames)
		{
			foreach (string _user in userNames) {
				fx.TreeNode _userNode = new fx.TreeNode {
					Text = _user,
					Value = _user,
					ImageUrl = UserImageUrl,
				};
				
				collection.Add(_userNode);
			}
		}

		protected void BindData()
		{
			this.TreeView.Nodes.Clear();

			if (this.DisplayMode == DisplayModeEnum.Roles || this.DisplayMode == DisplayModeEnum.UsersAndRoles) {
				this.AddRoles();
			}

			if (this.DisplayMode == DisplayModeEnum.Users || this.DisplayMode == DisplayModeEnum.UsersAndRoles) {
				this.AddUsers();
			}
		}

		#endregion Methods

		#region Event handlers

		protected override void OnPreRender(EventArgs e)
		{
			if (!this.BindingPerformed) {

				Debug.WriteLine("Bind", "UserTree");

				this.EnsureChildControls();
				this.BindData();
				this.BindingPerformed = true;
			}
			
			this.SetChildProperties();
			base.OnPreRender(e);
		}

		protected virtual void OnSelectionChanged(EventArgs args)
		{
			Debug.WriteLine("SelectionChanged", "UserTree");

			if(null != this.SelectionChanged) {
				this.SelectionChanged(this, args);
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			if (string.IsNullOrEmpty(RoleImageUrl)) {
				RoleImageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(UserTree), "N2.Futures.Images.group.png");
			}

			if (string.IsNullOrEmpty(UserImageUrl)) {
				UserImageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(UserTree), "N2.Futures.Images.user_red.png");
			}

		}

		#endregion Event handlers

		#region Child controls event handlers

		protected void SelectButton_Click(object sender, EventArgs e)
		{
			this.OnSelectionChanged(EventArgs.Empty);
		}

		protected void TreeView_SelectedNodeChanged(object sender, EventArgs e)
		{
			this.SelectedUser = this.TreeView.SelectedValue;
			this.OnSelectionChanged(EventArgs.Empty);
		}
		
		#endregion Child controls event handlers

		#region Types

		public enum DisplayModeEnum
		{
			Users,
			Roles,
			UsersAndRoles
		}

		public enum FilterTypeEnum
		{
			None,
			Include,
			Exclude
		}

		#endregion Types
		
		static string RoleImageUrl;
		static string UserImageUrl;
	}
}
