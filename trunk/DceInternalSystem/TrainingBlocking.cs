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
	/// Блокировка студентов тренинга
	/// </summary>
	public class TrainingBlocking : System.Windows.Forms.UserControl
	{
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem BlockmenuItem;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnChange;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader3;

      public TrainingBlockingNode Node;
		public TrainingBlocking(TrainingBlockingNode node)
		{
         this.Node = node;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
         this.toolBar1.ImageList = ToolbarImages.Images.images;

         if (Node.Readonly)
         {
            this.dataList.ContextMenu = null;
            this.btnChange.Enabled = false;
         }

         RefreshData();
		}

      protected void RefreshData()
      {
         dataSet =DCEAccessLib.DAL.Training.GetBlocking(new Guid(this.Node.Training));
         this.dataView.Table = dataSet.Tables["blocking"];
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
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.BlockmenuItem = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnChange = new System.Windows.Forms.ToolBarButton();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader3 = new DCEAccessLib.DataColumnHeader();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // contextMenu1
         // 
         this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.BlockmenuItem});
         // 
         // BlockmenuItem
         // 
         this.BlockmenuItem.Index = 0;
         this.BlockmenuItem.Text = "Блокировать";
         this.BlockmenuItem.Click += new System.EventHandler(this.BlockmenuItem_Click);
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // toolBar1
         // 
         this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
         this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                    this.btnRefresh,
                                                                                    this.btnChange});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(684, 37);
         this.toolBar1.TabIndex = 28;
         this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
         // 
         // btnRefresh
         // 
         this.btnRefresh.ImageIndex = 0;
         this.btnRefresh.Text = "Обновить";
         // 
         // btnChange
         // 
         this.btnChange.Text = "Блок./Разблок.";
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.dataColumnHeader2,
                                                                               this.dataColumnHeader1,
                                                                               this.dataColumnHeader3});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(684, 395);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 29;
         this.dataList.View = System.Windows.Forms.View.Details;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "FIO";
         this.dataColumnHeader2.Text = "Студент";
         this.dataColumnHeader2.Width = 300;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "isBlocked";
         this.dataColumnHeader1.Text = "Блокирован";
         this.dataColumnHeader1.Width = 120;
         // 
         // dataColumnHeader3
         // 
         this.dataColumnHeader3.FieldName = "LastLogin";
         this.dataColumnHeader3.Text = "Последний вход";
         this.dataColumnHeader3.Width = 120;
         // 
         // TrainingBlocking
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "TrainingBlocking";
         this.Size = new System.Drawing.Size(684, 432);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void dataList_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Right)
         {
            ListViewItem itm = this.dataList.GetItemAt(e.X,e.Y);
            if (itm !=null)
            {
               itm.Selected = true;
               if ( (bool)(((DataRowView)itm.Tag)["isBlocked"]))
                  BlockmenuItem.Text = "Разблокировать";
               else
                  BlockmenuItem.Text = "Блокировать";
            }
            BlockmenuItem.Enabled = (itm!=null);
         }
      }

      private void BlockmenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            try
            {
               DataRowView row = (DataRowView) this.dataList.SelectedItems[0].Tag;
               if ((bool)row["isBlocked"])
               {
                  DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from TrainingBlocking where id='"+row["id"].ToString()+"'");
               }
               else
               {
                  DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL(
"insert into TrainingBlocking (Student,Training) VALUES('"+row["StudentId"].ToString()+"' , '"+this.Node.Training+"')");
               }
            }
            finally
            {
               this.RefreshData();
            }
         }
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
            this.RefreshData();
         if (e.Button == this.btnChange)
            this.BlockmenuItem_Click(null,null);
      }
	}

   public class TrainingBlockingNode : NodeControl
   {
      public string Training;
      public TrainingBlockingNode (NodeControl parent, string training)
         : base(parent)
      {
         this.Training = training;
         this.rdonly = !(DCEUser.CurrentUser.Trainings == DCEUser.Access.Modify || DCEUser.CurrentUser.isCurator(Training));
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new TrainingBlocking(this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Блокировка студентов";
      }

      public override bool HaveChildNodes()
      {
         return false;
      }
   }


}
