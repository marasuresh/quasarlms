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
	
	[ToolboxData("<{0}:FindUsers runat=server></{0}:FindUsers>")]
	public class UserTree : fx.CompositeControl
	{
		#region Events

		public event EventHandler SelectionChanged;
		
		#endregion Events

		#region Properties

		public string SelectedUser {
			get {
				Debug.WriteLine("SelectedUser_get", "UserTree");
				Debug.WriteLine("SelectedUser_get: ChildControlsCreated = " + this.ChildControlsCreated.ToString(), "UserTree");
				this.EnsureChildControls();//should be true anyway, if caused by OnSelectionChanged
				Debug.WriteLine("SelectedUser_get: " + this.TreeView.SelectedValue, "UserTree");
				return this.TreeView.SelectedValue;
			}
			set {
				//this.EnsureChildControls();
				//this.TreeView..ViewState["SelectedUser"] = value;
			}
		}

		public bool AllowMultipleSelection {
			get {
				this.EnsureChildControls();
				return this.TreeView.ShowCheckBoxes == fx.TreeNodeTypes.All;
			}
			set { this.TreeView.ShowCheckBoxes = value
				? fx.TreeNodeTypes.All
				: fx.TreeNodeTypes.None; }
		}

		protected bool BindingPerformed {
			get { return (bool?)this.ViewState["BindingPerformed"] ?? false; }
			set { this.ViewState["BindingPerformed"] = value; }
		}

		#endregion Properties

		#region Fields

		protected fx.TreeView TreeView;

		#endregion Fields

		#region Methods

		protected override void CreateChildControls()
		{
			Debug.WriteLine("CreateChildControls", "UserTree");

			base.CreateChildControls();
			this.TreeView = new fx.TreeView {
				ID = "tv",
				ShowLines = true,
			};

			this.TreeView.SelectedNodeStyle.Font.Bold = true;
			this.TreeView.SelectedNodeChanged += this.TreeView_SelectedNodeChanged;
			//this.TreeView.Attributes.Add("style", "float:left");
			this.Controls.Add(this.TreeView);

			this.ClearChildViewState();
		}

		protected void TreeView_SelectedNodeChanged(object sender, EventArgs e)
		{
			this.SelectedUser = this.TreeView.SelectedValue;
			this.OnSelectionChanged(EventArgs.Empty);
		}

		protected void BindData()
		{
			string _roleImageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(UserTree), "N2.Futures.Images.group.png");
			string _userImageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(UserTree), "N2.Futures.Images.user_red.png");

			//Add roles
			foreach (var _role in Roles.GetAllRoles()) {
				fx.TreeNode _roleNode = new fx.TreeNode {
					Text = _role,
					Value = _role,
					ImageUrl = _roleImageUrl,
					SelectAction = fx.TreeNodeSelectAction.Expand,
				};

				foreach (var _user in Roles.GetUsersInRole(_role)) {
					_roleNode.ChildNodes.Add(new fx.TreeNode {
						Text = _user,
						Value = _user,
						SelectAction = fx.TreeNodeSelectAction.Select,
						ImageUrl = _userImageUrl,
					});
				}

				this.TreeView.Nodes.Add(_roleNode);
			}

			//Add users
			foreach (var _user in Membership.GetAllUsers().Cast<MembershipUser>()) {
				this.TreeView.Nodes.Add(new fx.TreeNode {
					Text = _user.UserName,
					Value = _user.UserName,
					SelectAction = fx.TreeNodeSelectAction.Select,
					ImageUrl = _userImageUrl,
				});
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			if (!this.Page.IsPostBack || !this.BindingPerformed) {
				
				Debug.WriteLine("Bind", "UserTree");
				
				this.EnsureChildControls();
				this.BindData();
				this.BindingPerformed = true;
			}

			base.OnLoad(e);
		}

		protected virtual void OnSelectionChanged(EventArgs args)
		{
			Debug.WriteLine("SelectionChanged", "UserTree");

			if(null != this.SelectionChanged) {
				this.SelectionChanged(this, args);
			}
		}

		#endregion Methods
	}
}
