using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N2.Web.UI.WebControls.Test
{
	/// <summary>
	/// Demo control for a UserTree
	/// </summary>
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:UserTreeTestBed runat=server></{0}:UserTreeTest>")]
	public class UserTreeTestBed : CompositeControl
	{
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CreateControlHierarchy();
			this.ClearChildViewState();
		}

		public UserTree UserTree { get; set; }

		TableRow Tr(Func<Control> controlProvider)
		{
			return Tr(null, controlProvider);
		}

		TableRow Tr(string label, Func<Control> controlProvider)
		{
			TableRow _tr = new TableRow();
			TableCell _td;

			_tr.Cells.Add(_td = new TableCell());
			Control _ctl = controlProvider();
			_td.Controls.Add(_ctl);
			
			_tr.Cells.AddAt(0, _td = new TableCell());
			if (!string.IsNullOrEmpty(label)) {
				_td.Controls.AddAt(0, new Label { AssociatedControlID = _ctl.ID, Text = label });
			}

			return _tr;
		}

		protected void CreateControlHierarchy()
		{
			
			TextBox _tbResult = new TextBox { ID = "tbResult", ReadOnly = true };

			UserTree _ut = this.UserTree = new UserTree();
			_ut.SelectionChanged += (o, e) => _tbResult.Text = ((UserTree)o).SelectedUser;
			
			Table _tbl;
			this.Controls.Add(_tbl = new Table());

			_tbl.Rows.AddRange(this.GetRowsWithControls());
			_tbl.Rows.AddAt(0, Tr("Selection", () => _tbResult));
			_tbl.Rows.AddAt(1, Tr(() => _ut));
			var _tr = _tbl.Rows[1];
			_tr.Cells.RemoveAt(0);
			var _td = _tr.Cells[0];
			_td.ColumnSpan = 2;
			_td.Style.Add("border", "1px solid Blue;");
		}
		
		TableRow[] GetRowsWithControls()
		{
			UserTree _ut = this.UserTree;

			//drop down list with Display Mode options
			var _displayModeListItemsQuery =
				from _name in new[] {
					UserTree.DisplayModeEnum.Roles,
					UserTree.DisplayModeEnum.Users,
					UserTree.DisplayModeEnum.UsersAndRoles
				}.Select(_item => _item.ToString())
				select new ListItem {
					Text = _name, Value = _name,
				};

			//prepare drop down list with Filter Type options
			var _filterTypeListItemsQuery =
				from _name in new[] {
					UserTree.FilterTypeEnum.None,
					UserTree.FilterTypeEnum.Include,
					UserTree.FilterTypeEnum.Exclude,
				}.Select(_item => _item.ToString())
				select new ListItem {
					Text = _name, Value = _name,
				};

			return
			new[] {
				Tr(() => {
					var _cb = new CheckBox { AutoPostBack = true, Text = "Display Empty Roles" };
					_cb.CheckedChanged += (o, e) => _ut.DisplayEmptyRoles = ((CheckBox)o).Checked;
					_cb.Checked = _ut.DisplayEmptyRoles;
					return _cb;
				}),
				
				Tr(() => {
					var _cb = new CheckBox { AutoPostBack = true, Text = "Allow Multiple Selection" };
					_cb.CheckedChanged += (o, e) => _ut.AllowMultipleSelection = ((CheckBox)o).Checked;
					_cb.Checked = _ut.AllowMultipleSelection;
					return _cb;
				}),

				Tr(() => {
					var _cb = new CheckBox { AutoPostBack = true, Text = "Expand Roles" };
					_cb.CheckedChanged += (o, e) => _ut.ExpandRoles = ((CheckBox)o).Checked;
					_cb.Checked = _ut.ExpandRoles;
					return _cb;
				}),
				
				Tr("Display Mode", () => {
					var _ddl = new DropDownList { ID = "ddlDisplayMode", AutoPostBack = true };
					_ddl.Items.AddRange(_displayModeListItemsQuery.ToArray());
					_ddl.SelectedIndexChanged +=
						(o, e) => _ut.DisplayMode = (UserTree.DisplayModeEnum)Enum.Parse(
							typeof(UserTree.DisplayModeEnum),
							((DropDownList)o).SelectedValue);
					_ddl.SelectedValue = _ut.DisplayMode.ToString();
					return _ddl;
					}),
				
				Tr("Selection Mode", () => {
					var _ddl = new DropDownList { ID = "ddlSelectionMode", AutoPostBack = true };
					_ddl.Items.AddRange(_displayModeListItemsQuery.ToArray());
					_ddl.SelectedIndexChanged +=
						(o, e) => _ut.SelectionMode = (UserTree.DisplayModeEnum)Enum.Parse(
							typeof(UserTree.DisplayModeEnum),
							((DropDownList)o).SelectedValue);
					_ddl.SelectedValue = _ut.SelectionMode.ToString();
					return _ddl;
				}),
				
				Tr("User Filter", () => {
					var _tb = new TextBox { ID = "tbUserFilter", AutoPostBack = true };
					_tb.TextChanged += (o, e) => _ut.UserFilter = ((TextBox)o).Text;
					_tb.Text = _ut.UserFilter;
					return _tb;
				}),
				
				Tr("User Filter Type", () => {
					var _ddl = new DropDownList { ID = "ddlUserFilterType", AutoPostBack = true };
					_ddl.Items.AddRange(_filterTypeListItemsQuery.ToArray());
					_ddl.SelectedIndexChanged +=
						(o, e) => _ut.UserFilterType = (UserTree.FilterTypeEnum)Enum.Parse(
							typeof(UserTree.FilterTypeEnum),
							((DropDownList)o).SelectedValue);
					_ddl.SelectedValue = _ut.UserFilterType.ToString();
					return _ddl;
				}),

				Tr("Role Filter", () => {
					var _tb = new TextBox { ID = "tbRoleFilter", AutoPostBack = true };
					_tb.TextChanged += (o, e) => _ut.RoleFilter = ((TextBox)o).Text;
					_tb.Text = _ut.RoleFilter;
					return _tb;
				}),

				Tr("Role Filter Type", () => {
					var _ddl = new DropDownList { ID = "ddlRoleFilterType", AutoPostBack = true };
					_ddl.Items.AddRange(_filterTypeListItemsQuery.ToArray());
					_ddl.SelectedIndexChanged +=
						(o, e) => _ut.RoleFilterType = (UserTree.FilterTypeEnum)Enum.Parse(
							typeof(UserTree.FilterTypeEnum),
							((DropDownList)o).SelectedValue);
					_ddl.SelectedValue = _ut.RoleFilterType.ToString();
					return _ddl;
				}),
			};
		}
	}
}
