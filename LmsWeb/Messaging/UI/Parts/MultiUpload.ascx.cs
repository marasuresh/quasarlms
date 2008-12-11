using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace N2.Messaging.Messaging.UI.Parts
{
	using N2.Web.UI.WebControls;
	
	public partial class MultiUpload : UserControl
	{
        #region Event handlers

		protected void btnAddFile_Click(object sender, EventArgs e)
		{
			this.Items = this.Items.Concat(new[] { string.Empty }).ToArray();
		}

		protected override void OnLoad(EventArgs e)
		{
			if (!this.IsPostBack) {
				this.DataBind();
			}
			base.OnLoad(e);
		}

		protected void rptFiles_ItemCommand(object sender, RepeaterCommandEventArgs e)
		{
			if (string.Equals("Delete", e.CommandName, StringComparison.OrdinalIgnoreCase)) {
				var _itemToDelete = this.PhysicalItems.ToArray()[e.Item.ItemIndex];
				this.Items = this.PhysicalItems.Except(new[] { _itemToDelete}).ToArray();
			}
		}

		#endregion Event handlers

		#region Properties

		protected IEnumerable<string> PhysicalItems {
			get {
				return this.rptFiles.Items
						.OfType<RepeaterItem>()
						.Where(_item =>
							_item.ItemType == ListItemType.Item
							|| _item.ItemType == ListItemType.AlternatingItem)
						.Select(_item => _item.FindControl("fs"))
						.Cast<FileSelector>()
						.Select(_fs => _fs.Url);
			}
		}

		public string[] Items {
			get {
				return
					this.PhysicalItems
						.Where(_url => !string.IsNullOrEmpty(_url))
						.Distinct()
						.ToArray();
			}
			set {
				this.rptFiles.DataSource = value;
				this.rptFiles.DataBind();
			}
		}

		#endregion Properties
	}
}