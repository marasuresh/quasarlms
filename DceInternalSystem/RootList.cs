using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCEInternalSystem
{
   ///<summary>
	/// Корневой список нод
	/// </summary>
	public class RootList : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.ListView listView1;
      private System.Windows.Forms.ColumnHeader columnHeader1;
      private System.Windows.Forms.ColumnHeader columnHeader2;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RootList()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.listView1 = new System.Windows.Forms.ListView();
         this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
         this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
         this.SuspendLayout();
         // 
         // listView1
         // 
         this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                    this.columnHeader1,
                                                                                    this.columnHeader2});
         this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.listView1.GridLines = true;
         this.listView1.Name = "listView1";
         this.listView1.Size = new System.Drawing.Size(476, 368);
         this.listView1.TabIndex = 0;
         this.listView1.View = System.Windows.Forms.View.Details;
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "Название";
         this.columnHeader1.Width = 164;
         // 
         // columnHeader2
         // 
         this.columnHeader2.Text = "Описание";
         this.columnHeader2.Width = 224;
         // 
         // RootList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.listView1});
         this.Name = "RootList";
         this.Size = new System.Drawing.Size(476, 368);
         this.ResumeLayout(false);

      }
		#endregion
	}
}
