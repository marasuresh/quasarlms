using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DCEAccessLib;

namespace DCEInternalSystem
{
	/// <summary>
	/// Форма выбора группы
	/// </summary>
	public class GroupSelect : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.Button CancelBtn;
      public DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader6;
      private DCEAccessLib.DataColumnHeader dataColumnHeader7;
      public System.Data.DataSet dataSet;
      public System.Data.DataView dataView;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GroupSelect()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

      /// <summary>
      /// Вызывает диалог выбора группы и возвращает выбранную группу
      /// </summary>
      /// <param name="excludes">Какие группы исключать из списка выбора</param>
      /// <param name="mainGroupIdExclude">Какой id исключить из списка</param>
      /// <param name="groupType">тип элементов группы</param>
      /// <returns></returns>
      public static DataRowView SelectGroup(DataView excludes, 
         string mainGroupIdExclude, EntityType groupType)
      {
         GroupSelect sel = new GroupSelect();
         sel.dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select * from dbo.Groups where Type=" + ((int)groupType).ToString()+" and id not in (select Students from Trainings union select Students from Tracks)","Groups");
         sel.dataView.Table = sel.dataSet.Tables["Groups"];

         foreach (DataRowView row in excludes)
         {
            for (int i=0; i<sel.dataView.Count; i++)
            {
               if (row["id"].ToString() == sel.dataView[i]["id"].ToString())
               {
                  sel.dataView.Delete(i);
                  break;
               }
            }
         }
         for (int i=0; i<sel.dataView.Count; i++)
         {
            if (mainGroupIdExclude == sel.dataView[i]["id"].ToString())
            {
               sel.dataView.Delete(i);
               break;
            }
         }
         if ( sel.ShowDialog() == DialogResult.OK )
         {
            if (sel.dataList.SelectedItems.Count>0)
            {
               return (DataRowView)sel.dataList.SelectedItems[0].Tag;
            }
         }
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
         this.button1 = new System.Windows.Forms.Button();
         this.CancelBtn = new System.Windows.Forms.Button();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader6 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader7 = new DCEAccessLib.DataColumnHeader();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.panel1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.button1,
                                                                             this.CancelBtn});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 377);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(564, 28);
         this.panel1.TabIndex = 1;
         // 
         // button1
         // 
         this.button1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button1.Location = new System.Drawing.Point(316, 4);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(120, 20);
         this.button1.TabIndex = 2;
         this.button1.Text = "Выбрать";
         // 
         // CancelBtn
         // 
         this.CancelBtn.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.CancelBtn.Location = new System.Drawing.Point(440, 4);
         this.CancelBtn.Name = "CancelBtn";
         this.CancelBtn.Size = new System.Drawing.Size(120, 20);
         this.CancelBtn.TabIndex = 3;
         this.CancelBtn.Text = "Отменить";
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.dataColumnHeader6,
                                                                               this.dataColumnHeader7});
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(564, 377);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 1;
         this.dataList.View = System.Windows.Forms.View.Details;
         // 
         // dataColumnHeader6
         // 
         this.dataColumnHeader6.FieldName = "Name";
         this.dataColumnHeader6.Text = "Название";
         this.dataColumnHeader6.Width = 250;
         // 
         // dataColumnHeader7
         // 
         this.dataColumnHeader7.FieldName = "Description";
         this.dataColumnHeader7.Text = "Описание";
         this.dataColumnHeader7.Width = 350;
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // GroupSelect
         // 
         this.AcceptButton = this.button1;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.CancelBtn;
         this.ClientSize = new System.Drawing.Size(564, 405);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.panel1});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "GroupSelect";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Добавить группу";
         this.panel1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion
	}
}
