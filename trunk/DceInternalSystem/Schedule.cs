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
   /// Класс описывающий ноду "Расписание"
   /// </summary>
   public class ScheduleControl : NodeControl
   {

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.Schedule(this);
         }
         return this.fControl;
      }

      public ScheduleControl (NodeControl parent)
         : base(parent)
      {
      }


      public override String GetCaption()
      {
         return "Расписание";
      }

      public override bool HaveChildNodes()
      {
         return true;
      }
   }
   /// <summary>
	/// Summary description for Schedule.
	/// </summary>
	public class Schedule : System.Windows.Forms.UserControl
	{
      private DataList dataList;
      private DataColumnHeader dataColumnHeader1;
      private DataColumnHeader dataColumnHeader2;
      private DataColumnHeader dataColumnHeader3;
      private DataColumnHeader dataColumnHeader4;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button OkButton;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      private ScheduleControl Node;
      public Schedule(ScheduleControl node)
      {

        // This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
         Node = node;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         Node = null;
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
                                                                                                                                                            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "УН-333", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)))),
                                                                                                                                                            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Программирование на Си"),
                                                                                                                                                            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "25.10.2003"),
                                                                                                                                                            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "25.11.2003")}, -1);
         this.dataList = new DataList();
         this.dataColumnHeader1 = new DataColumnHeader();
         this.dataColumnHeader2 = new DataColumnHeader();
         this.dataColumnHeader3 = new DataColumnHeader();
         this.dataColumnHeader4 = new DataColumnHeader();
         this.panel1 = new System.Windows.Forms.Panel();
         this.OkButton = new System.Windows.Forms.Button();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DataColumnHeader[] {
                                                                                    this.dataColumnHeader1,
                                                                                    this.dataColumnHeader2,
                                                                                    this.dataColumnHeader3,
                                                                                    this.dataColumnHeader4});
         this.dataList.DataView = null;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
                                                                                 listViewItem1});
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(584, 448);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 12;
         this.dataList.View = System.Windows.Forms.View.Details;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "dataColumnHeader1";
         this.dataColumnHeader1.Text = "Код";
         this.dataColumnHeader1.Width = 100;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "dataColumnHeader2";
         this.dataColumnHeader2.Text = "Название";
         this.dataColumnHeader2.Width = 200;
         // 
         // dataColumnHeader3
         // 
         this.dataColumnHeader3.FieldName = "dataColumnHeader3";
         this.dataColumnHeader3.Text = "Дата начала";
         this.dataColumnHeader3.Width = 100;
         // 
         // dataColumnHeader4
         // 
         this.dataColumnHeader4.FieldName = "dataColumnHeader4";
         this.dataColumnHeader4.Text = "Дата окончания";
         this.dataColumnHeader4.Width = 100;
         // 
         // panel1
         // 
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.OkButton});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 408);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(584, 40);
         this.panel1.TabIndex = 20;
         // 
         // OkButton
         // 
         this.OkButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.OkButton.Location = new System.Drawing.Point(476, 8);
         this.OkButton.Name = "OkButton";
         this.OkButton.Size = new System.Drawing.Size(96, 24);
         this.OkButton.TabIndex = 200;
         this.OkButton.Text = "Расписание";
         this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
         // 
         // Schedule
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.panel1,
                                                                      this.dataList});
         this.Name = "Schedule";
         this.Size = new System.Drawing.Size(584, 448);
         this.panel1.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void OkButton_Click(object sender, System.EventArgs e)
      {
//         TrainingScheduleControl c = new TrainingScheduleControl(this.Node,
//            
//            );
//         c.Select();
      }
	}
}
