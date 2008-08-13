using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCEInternalSystem
{
   /// <summary>
   /// Класс описывающий ноду "Тренинги и треки курсов"
   /// </summary>
   public class TrainingAndTrackControl : NodeControl
   {
      public TrainingAndTrackControl (NodeControl parent)
         : base(parent)
   {
   }

      public override void CreateChilds()
      {
         new DCEInternalSystem.TrainingControl(this);
         new DCEInternalSystem.TrainingExpiresControl(this);
         if (DCEUser.CurrentUser.Trainings != DCEUser.Access.No)
            new DCEInternalSystem.CourseTracksControl(this);
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.TrainingAndTracksList();
         }
         if (!this.treeNode.IsExpanded)
            this.ExpandTreeNode();
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Тренинги и треки тренингов";
      }

      public override bool HaveChildNodes()
      {
         return true;
      }
   }
   /// <summary>
	/// Тренинги и треки тренингов
	/// </summary>
	public class TrainingAndTracksList : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.ListView listView1;
      private System.Windows.Forms.ColumnHeader columnHeader1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TrainingAndTracksList()
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
         System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
                                                                                                                                                            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Тренинги", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204))))}, -1);
         System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
                                                                                                                                                            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Треки тренингов", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204))))}, -1);
         this.listView1 = new System.Windows.Forms.ListView();
         this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
         this.SuspendLayout();
         // 
         // listView1
         // 
         this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                    this.columnHeader1});
         this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.listView1.GridLines = true;
         this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
                                                                                  listViewItem1,
                                                                                  listViewItem2});
         this.listView1.Name = "listView1";
         this.listView1.Size = new System.Drawing.Size(384, 320);
         this.listView1.TabIndex = 7;
         this.listView1.View = System.Windows.Forms.View.Details;
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "Имя";
         this.columnHeader1.Width = 117;
         // 
         // TrainingAndTracksList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.listView1});
         this.Name = "TrainingAndTracksList";
         this.Size = new System.Drawing.Size(384, 320);
         this.ResumeLayout(false);

      }
		#endregion
	}
}
