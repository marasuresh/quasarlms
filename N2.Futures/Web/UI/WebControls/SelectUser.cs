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
	using System.Diagnostics;

	[DefaultProperty("Text")]
	[ToolboxData("<{0}:SelectUser runat=server></{0}:SelectUser>")]
	public class SelectUser : CompositeControl
	{
		public bool AllowMultipleSelection
		{
			get { this.EnsureChildControls(); return this.tree.AllowMultipleSelection; }
			set { this.EnsureChildControls(); this.tree.AllowMultipleSelection = value; }
		}

		public bool DisplayEmptyRoles
		{
			get { this.EnsureChildControls(); return this.tree.DisplayEmptyRoles; }
			set { this.EnsureChildControls(); this.tree.DisplayEmptyRoles = value; }
		}
		
		public bool ExpandRoles
		{
			get { this.EnsureChildControls(); return this.tree.ExpandRoles; }
			set { this.EnsureChildControls(); this.tree.ExpandRoles = value; }
		}

		public UserTree.DisplayModeEnum DisplayMode
		{
			get { this.EnsureChildControls(); return this.tree.DisplayMode; }
			set { this.EnsureChildControls(); this.tree.DisplayMode = value; }
		}

		public UserTree.DisplayModeEnum SelectionMode
		{
			get { this.EnsureChildControls(); return this.tree.SelectionMode; }
			set { this.EnsureChildControls(); this.tree.SelectionMode = value; }
		}

		#region Properties

		string m_delayedSelectedUserValue;

		public string SelectedUser
		{
			get
			{
				this.EnsureChildControls();
				Debug.WriteLine("SelectUser.SelctedUser.get: " + this.textBox.Text, "UserTree");

				return
					null != this.textBox
						? this.textBox.Text
						: this.tree.SelectedUser;
			}
			set
			{
				//Bug in N2?
				if (this.Page.IsPostBack) return;
				//postpone until InSelectionMode will be loaded from ViewState

				this.m_delayedSelectedUserValue = value;

				this.Load += (o, e) => {
					Debug.WriteLine(string.Format(
						@"SelectUser: SelectedUser_set/Load: ChildControlsCreated = {0}, InSelectedMode = {1}, {2}",
						this.ChildControlsCreated.ToString(),
						this.InSelectionMode.ToString(),
						this.m_delayedSelectedUserValue), "UserTree");

					if (!this.InSelectionMode) {
						this.EnsureChildControls();

						if (!string.Equals(this.textBox.Text, this.m_delayedSelectedUserValue, StringComparison.OrdinalIgnoreCase)) {
							this.textBox.Text = this.m_delayedSelectedUserValue;
						}
					}
				};
			}
		}

		protected bool InSelectionMode
		{
			get
			{
				return (bool?)this.ViewState["InSelectionMode"] ?? false;
			}
			set { this.ViewState["InSelectionMode"] = value; }
		}

		protected UserTree tree;
		protected TextBox textBox;
		protected Button selectButton;

		#endregion Properties

		#region Methods

		protected override void CreateChildControls()
		{
			Debug.WriteLine("SelectUser.CreateChildControls: IsPostBack=" + this.Page.IsPostBack.ToString(), "UserTree");

			base.CreateChildControls();

			this.tree = new UserTree {
				ID = "ut",
			};
			this.tree.Style.Add("float", "left");
			this.tree.SelectionChanged += UserTree_SelectionChanged;

			this.Controls.Add(this.tree);
			this.textBox = new TextBox { ID = "tb", };

			this.Controls.Add(this.textBox);

			this.selectButton = new Button {
				ID = "btn",
				Text = "…",
				CausesValidation = false
			};

			this.selectButton.Click += Button_Click;
			this.Controls.Add(this.selectButton);

			MembershipUserValidator _validator = new MembershipUserValidator {
				ID = "mvv",
				ControlToValidate = this.textBox.ID,
				Display = ValidatorDisplay.Dynamic,
				ErrorMessage = "User list contains unrecognised user(s)",
			};

			this.Controls.Add(_validator);

			this.ClearChildViewState();
		}

		protected void UserTree_SelectionChanged(object sender, EventArgs e)
		{
			string _selection = this.tree.SelectedUser;
			Debug.WriteLine("UserTree_SelectionChanged: " + _selection, "UserTree");

			this.InSelectionMode = false;
			this.ChildControlsCreated = false;
			//access to .SelectedUser will cause a recreation of child controls,
			// so TreeView will disappear..
			this.EnsureChildControls();
			this.textBox.Text = this.m_delayedSelectedUserValue = _selection;
		}

		protected void Button_Click(object sender, EventArgs e)
		{
			Debug.WriteLine("SelectUser.Button_Click", "UserTree");
			this.InSelectionMode = true;
			this.ChildControlsCreated = false;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.tree.Visible = this.InSelectionMode;
		}

		#endregion Methods
	}
}
