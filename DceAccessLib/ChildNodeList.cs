using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DCEAccessLib
{
	/// <summary>
	/// Список подчиненных нод
	/// </summary>
	public class ChildNodeList : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.ColumnHeader columnHeader1;
      private System.Windows.Forms.ListView listView;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ChildNodeList(System.Collections.ArrayList nodes)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         foreach (NodeControl node in nodes)
         {
            ListViewItem itm = listView.Items.Add (node.GetCaption());
            itm.Tag = node;
         }
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

      public void SetView(View v)
      {
         this.listView.View = v;
      }

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.listView = new System.Windows.Forms.ListView();
         this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
         this.SuspendLayout();
         // 
         // listView
         // 
         this.listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                   this.columnHeader1});
         this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.listView.FullRowSelect = true;
         this.listView.GridLines = true;
         this.listView.Name = "listView";
         this.listView.Size = new System.Drawing.Size(600, 444);
         this.listView.TabIndex = 8;
         this.listView.View = System.Windows.Forms.View.Details;
         this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "Имя";
         this.columnHeader1.Width = 300;
         // 
         // ChildNodeList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.listView});
         this.Name = "ChildNodeList";
         this.Size = new System.Drawing.Size(600, 444);
         this.ResumeLayout(false);

      }
		#endregion

      private void listView_DoubleClick(object sender, System.EventArgs e)
      {
         if (this.listView.SelectedItems.Count>0 && this.listView.SelectedItems[0].Tag!=null)
         {
            NodeControl node =(NodeControl) this.listView.SelectedItems[0].Tag;
            node.Select();
         }
      }
	}

   public class ChildNodeListControl : NodeControl
   {
      public ChildNodeListControl(NodeControl parent)
         : base(parent)
      {
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new ChildNodeList(this.Nodes);
         }
         if (!this.treeNode.IsExpanded)
            this.ExpandTreeNode();
         return this.fControl;
      }

   }
}
