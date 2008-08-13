using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DCEAccessLib;
using System.Data;

namespace DCEInternalSystem
{
	/// <summary>
	/// Выбор трека тренингов
	/// </summary>
	public class TrackSelect : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button btnOk;
      private System.Windows.Forms.Button btnCancel;
      private DCEAccessLib.DataList dataList;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TrackSelect()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
         this.dataSet = DCEWebAccess.GetdataSet(
"select id, dbo.GetStrContentAlt(Name,'RU','EN') as Name, dbo.GetStrContentAlt(Description,'RU','EN') as Descr, Students,Trainings from Tracks","Tracks");
         this.dataView.Table = this.dataSet.Tables["Tracks"];
      }

      public static DataRowView SelectTrack()
      {
         TrackSelect s = new TrackSelect();
         if (s.ShowDialog()==DialogResult.OK)
         {
            return (DataRowView)s.dataList.SelectedItems[0].Tag;
         }
         else
            return null;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.panel1 = new System.Windows.Forms.Panel();
         this.btnOk = new System.Windows.Forms.Button();
         this.btnCancel = new System.Windows.Forms.Button();
         this.dataList = new DCEAccessLib.DataList();
         this.dataSet = new System.Data.DataSet();
         this.dataView = new System.Data.DataView();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.panel1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.btnCancel,
                                                                             this.btnOk});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 335);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(606, 36);
         this.panel1.TabIndex = 0;
         // 
         // btnOk
         // 
         this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.btnOk.Location = new System.Drawing.Point(438, 8);
         this.btnOk.Name = "btnOk";
         this.btnOk.Size = new System.Drawing.Size(82, 23);
         this.btnOk.TabIndex = 0;
         this.btnOk.Text = "Ok";
         this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
         // 
         // btnCancel
         // 
         this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.btnCancel.Location = new System.Drawing.Point(526, 8);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.TabIndex = 1;
         this.btnCancel.Text = "Отменить";
         // 
         // dataList
         // 
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.dataColumnHeader1,
                                                                               this.dataColumnHeader2});
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.HideSelection = false;
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(606, 335);
         this.dataList.TabIndex = 1;
         this.dataList.View = System.Windows.Forms.View.Details;
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "Name";
         this.dataColumnHeader1.Text = "Название";
         this.dataColumnHeader1.Width = 250;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "Descr";
         this.dataColumnHeader2.Text = "Описание";
         this.dataColumnHeader2.Width = 500;
         // 
         // TrackSelect
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.btnCancel;
         this.ClientSize = new System.Drawing.Size(606, 371);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.panel1});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "TrackSelect";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Выберите трек тренингов";
         this.panel1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void btnOk_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            this.DialogResult = DialogResult.OK;
            this.Close();
         }
      }
	}
}
