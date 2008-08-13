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
	/// Кураторы курсов
	/// </summary>
	public class GroupCurators : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Label label5;
      public DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public RecordEditNode GroupEditor;
		public GroupCurators(RecordEditNode groupEditor)
		{
         GroupEditor = groupEditor;
         GroupEditor.OnReloadContent += new RecordEditNode.ReloadContentHandler(RefreshData);
         GroupEditor.OnUpdateDataSet += new RecordEditNode.EnumDataSetsHandler(PostUpdates);

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
         this.dataColumnHeader1.OnParse += new DataColumnHeader.FieldParseHandler(NameParse); 
         if (DCEUser.CurrentUser.Students != DCEUser.Access.Modify)
         {
            this.dataList.ContextMenu = null;
         }
         RefreshData();
		}

      public void NameParse(string FieldName, DataRowView row, ref string text)
      {
         text = row["LastName"].ToString() + " " + row["FirstName"].ToString() + " " + row["Patronymic"].ToString();
      }

      bool PostUpdates(string TransactionId, ref DCEAccessLib.DataSetBatchUpdate dataSet)
      {
         this.dataList.SuspendListChange=true;
         this.dataSet.Tables[0].Columns.Remove("FirstName");
         this.dataSet.Tables[0].Columns.Remove("LastName");
         this.dataSet.Tables[0].Columns.Remove("Patronymic");

         dataSet.sql = "select id,eid,permid from Rights where permid='"+this.GroupEditor.Id+"'";
         dataSet.dataSet = this.dataSet;
         dataSet.tableName = "Curators";
         return true;
      }
         
      public void RefreshData()
      {
         this.dataList.SuspendListChange=false;
         this.dataSet = DCEWebAccess.WebAccess.GetDataSet(
@"select DISTINCT u.FirstName, u.LastName, u.Patronymic, r.id, r.eid, r.permid from Users u, Rights r 
where
   r.eid = u.id and r.permid = '"+this.GroupEditor.Id+"'","Curators");
         this.dataView.Table = this.dataSet.Tables["Curators"];
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
         this.label5 = new System.Windows.Forms.Label();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.dataSet = new System.Data.DataSet();
         this.dataView = new System.Data.DataView();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.SuspendLayout();
         // 
         // label5
         // 
         this.label5.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label5.Dock = System.Windows.Forms.DockStyle.Top;
         this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label5.ForeColor = System.Drawing.SystemColors.Info;
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(564, 20);
         this.label5.TabIndex = 33;
         this.label5.Text = "Кураторы";
         this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.dataColumnHeader1});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 20);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(564, 128);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 34;
         this.dataList.View = System.Windows.Forms.View.Details;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "FirstName";
         this.dataColumnHeader1.Text = "Имя";
         this.dataColumnHeader1.Width = 300;
         // 
         // contextMenu1
         // 
         this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItem1,
                                                                                     this.menuItem2,
                                                                                     this.menuItem3});
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 0;
         this.menuItem1.Text = "Добавить";
         this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 1;
         this.menuItem2.Text = "-";
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 2;
         this.menuItem3.Text = "Удалить";
         this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // GroupCurators
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.label5});
         this.Name = "GroupCurators";
         this.Size = new System.Drawing.Size(564, 148);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         DataRowView row = UserSelect.SelectUser(this.dataView, "eid");

         if (row !=null)
         {
            DataRowView newrow = this.dataView.AddNew();
            newrow["id"] = System.Guid.NewGuid();
            newrow["eid"] = row["id"];
            newrow["permid"] = this.GroupEditor.Id;
            newrow["FirstName"] = row["FirstName"];
            newrow["LastName"] = row["LastName"];
            newrow["Patronymic"] = row["Patronymic"];
            newrow.EndEdit();
            this.GroupEditor.ChangeNotify();
         }
      }

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить выбранного пользователя?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;
               row.Delete();
               this.GroupEditor.ChangeNotify();
            }
         }
      }
	}
}
