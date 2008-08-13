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
   /// Класс описывающий ноду "Студенты и группы"
   /// </summary>
   public class StudentsAndGroupsControl : NodeControl
   {
	  protected bool fGroupsOnly = false;
      public override void CreateChilds()
      {
		 if (!fGroupsOnly)
		   new DCEInternalSystem.StudentsControl(this);
         new DCEInternalSystem.StudentGroupsControl(this);
      }

      public StudentsAndGroupsControl (NodeControl parent, bool groupsonly)
         : base(parent)
      { fGroupsOnly = groupsonly; }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.StudentAndGroupsList(this.Nodes);
         }
         if (!this.treeNode.IsExpanded)
            this.ExpandTreeNode();
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Студенты и группы";
      }
      
      public override bool HaveChildNodes() { return true; }
   }

   /// <summary>
	/// Список студентов и групп студентов
	/// </summary>
	public class StudentAndGroupsList : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.ListView listView1;
      private System.Windows.Forms.ColumnHeader columnHeader1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StudentAndGroupsList(System.Collections.ArrayList nodes)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         foreach (NodeControl node in nodes)
         {
            node.GetCaption();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
                                                                                                                                                            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Студенты", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204))))}, -1);
         System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
                                                                                                                                                            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Группы студентов", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204))))}, -1);
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
         this.listView1.Size = new System.Drawing.Size(384, 336);
         this.listView1.TabIndex = 6;
         this.listView1.View = System.Windows.Forms.View.Details;
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "Имя";
         this.columnHeader1.Width = 117;
         // 
         // StudentAndGroupsList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.listView1});
         this.Name = "StudentAndGroupsList";
         this.Size = new System.Drawing.Size(384, 336);
         this.ResumeLayout(false);

      }
		#endregion
	}
}
