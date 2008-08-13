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
	/// Доска объявлений
	/// </summary>
	public class TrainingBB : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ImageList imageList1;
      private System.ComponentModel.IContainer components;

      private System.Windows.Forms.Panel panel1;
      private DCEAccessLib.LangSwitcher langSwitcher1;
      public DCEAccessLib.DataList dataList;
      private System.Data.DataView dataView;
      private DCEAccessLib.DataColumnHeader dataColumnHeader6;
      private DCEAccessLib.DataColumnHeader dataColumnHeader7;
      private DCEAccessLib.DataColumnHeader dataColumnHeader8;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Data.DataSet dataSet;
      private System.Windows.Forms.ToolBarButton btnAdd;
      private System.Windows.Forms.ToolBarButton btnDel;
      private System.Windows.Forms.ToolBarButton btnProps;

      bool rdonly = false;
      TrainingBBNode Node;
		public TrainingBB(TrainingBBNode node)
		{
         Node = node;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         if (DCEUser.CurrentUser.Trainings != DCEUser.Access.Modify)
         {
         /// 
            if (!DCEUser.CurrentUser.isCuratorOrInstructor(this.Node.TrainingId))
            {
               this.dataList.ContextMenu = null;
               this.btnAdd.Enabled = false;
               this.btnDel.Enabled = false;
               this.rdonly = true;
            }
         }
         RefreshData();
		}

      public void RefreshData()
      {
         langSwitcher1_LanguageChanged(langSwitcher1.Language);
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingBB));
			this.dataSet = new System.Data.DataSet();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.btnRefresh = new System.Windows.Forms.ToolBarButton();
			this.btnAdd = new System.Windows.Forms.ToolBarButton();
			this.btnDel = new System.Windows.Forms.ToolBarButton();
			this.btnProps = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.langSwitcher1 = new DCEAccessLib.LangSwitcher();
			this.dataList = new DCEAccessLib.DataList();
			this.dataColumnHeader6 = new DCEAccessLib.DataColumnHeader();
			this.dataColumnHeader7 = new DCEAccessLib.DataColumnHeader();
			this.dataColumnHeader8 = new DCEAccessLib.DataColumnHeader();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.dataView = new System.Data.DataView();
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
			this.SuspendLayout();
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
            this.btnAdd,
            this.btnDel,
            this.btnProps});
			this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
			this.toolBar1.Divider = false;
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(632, 42);
			this.toolBar1.TabIndex = 27;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// btnRefresh
			// 
			this.btnRefresh.ImageIndex = 0;
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Text = "Обновить";
			// 
			// btnAdd
			// 
			this.btnAdd.ImageIndex = 1;
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Text = "Добавить";
			// 
			// btnDel
			// 
			this.btnDel.ImageIndex = 2;
			this.btnDel.Name = "btnDel";
			this.btnDel.Text = "Удалить";
			// 
			// btnProps
			// 
			this.btnProps.ImageIndex = 3;
			this.btnProps.Name = "btnProps";
			this.btnProps.Text = "Свойства";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.langSwitcher1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 42);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(632, 29);
			this.panel1.TabIndex = 29;
			// 
			// langSwitcher1
			// 
			this.langSwitcher1.Enabled = global::DCEInternalSystem.Properties.Settings.Default.LangSwitchEnabled;
			this.langSwitcher1.Location = new System.Drawing.Point(4, 4);
			this.langSwitcher1.Name = "langSwitcher1";
			this.langSwitcher1.Size = new System.Drawing.Size(180, 20);
			this.langSwitcher1.TabIndex = 29;
			this.langSwitcher1.TextLabel = "Язык";
			this.langSwitcher1.Visible = global::DCEInternalSystem.Properties.Settings.Default.LangSwitchVisible;
			this.langSwitcher1.LanguageChanged += new DCEAccessLib.LangSwitcher.SwitchLangHandler(this.langSwitcher1_LanguageChanged);
			// 
			// dataList
			// 
			this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
			this.dataList.AllowColumnReorder = true;
			this.dataList.AllowSorting = true;
			this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
            this.dataColumnHeader6,
            this.dataColumnHeader7,
            this.dataColumnHeader8});
			this.dataList.ContextMenu = this.contextMenu1;
			this.dataList.DataView = this.dataView;
			this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataList.FullRowSelect = true;
			this.dataList.GridLines = true;
			this.dataList.HideSelection = false;
			this.dataList.Location = new System.Drawing.Point(0, 71);
			this.dataList.MultiSelect = false;
			this.dataList.Name = "dataList";
			this.dataList.Size = new System.Drawing.Size(632, 353);
			this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.dataList.TabIndex = 30;
			this.dataList.UseCompatibleStateImageBehavior = false;
			this.dataList.View = System.Windows.Forms.View.Details;
			this.dataList.DoubleClick += new System.EventHandler(this.menuItem1_Click);
			// 
			// dataColumnHeader6
			// 
			this.dataColumnHeader6.FieldName = "PostDate";
			this.dataColumnHeader6.Text = "Дата";
			this.dataColumnHeader6.Width = 100;
			// 
			// dataColumnHeader7
			// 
			this.dataColumnHeader7.FieldName = "UserName";
			this.dataColumnHeader7.Text = "Автор";
			this.dataColumnHeader7.Width = 150;
			// 
			// dataColumnHeader8
			// 
			this.dataColumnHeader8.FieldName = "RMsg";
			this.dataColumnHeader8.Text = "Сообщение";
			this.dataColumnHeader8.Width = 400;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem4,
            this.menuItem2,
            this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Свойства";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.Text = "-";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.Text = "Добавить";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Text = "Удалить";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// TrainingBB
			// 
			this.Controls.Add(this.dataList);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.toolBar1);
			this.Name = "TrainingBB";
			this.Size = new System.Drawing.Size(632, 424);
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

      }
		#endregion

      private void langSwitcher1_LanguageChanged(int langid)
      {
///
         this.dataSet = DCEWebAccess.WebAccess.GetDataSet("select *, dbo.UserName(Author,0) as UserName, dbo.GetStrContent(Message,"+langid+") as RMsg from BulletinBoard where Training='"+this.Node.TrainingId+"'","Messages");
         this.dataView.Table = this.dataSet.Tables["Messages"];
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
         {
            this.RefreshData();
         }
         if (e.Button == this.btnAdd)
         {
            menuItem2_Click(null,null);
         }
         if (e.Button == this.btnDel)
         {
            menuItem3_Click(null,null);
         }
         if (e.Button == this.btnProps)
         {
            menuItem1_Click(null,null);
         }
      }

      private void menuItem2_Click(object sender, System.EventArgs e)
      {
         DateTime tm = DateTime.Now;
         string guid = System.Guid.NewGuid().ToString();
         TrainingBBEdit ed = TrainingBBEdit.EditMessage(guid,tm,this.rdonly);
         if (ed != null)
         {
            this.dataList.SuspendListChange = true;
            try
            {
               DataRowView row = this.dataView.AddNew();
               row["id"] = guid;
               row["Author"] = DCEUser.CurrentUser.id;
               row["PostDate"] = ed.dateTimePicker1.Value;
               row["Message"] = guid;
               row["Training"] = this.Node.TrainingId;
               row.EndEdit();
               DCEAccessLib.DCEWebAccess.WebAccess.UpdateDataSets(
                  new string[] {
                     "select * from BulletinBoard where Training='"+this.Node.TrainingId+"'",
                     "select * from dbo.Content where eid='" + ed.CComment.eId + "'"
                  }
                  , new string[] {"Messages","dbo.Content"}
                  , new DataSet[] { this.dataSet,ed.CComment.dataSet }
                  );
            }
            finally
            {
               this.dataList.SuspendListChange = false;
               this.RefreshData();
               this.dataList.UpdateList();
            }
         }
      }

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;

            TrainingBBEdit ed = TrainingBBEdit.EditMessage(row["Message"].ToString(),(DateTime)row["PostDate"],this.rdonly);
            if (ed != null)
            {
               this.dataList.SuspendListChange = true;
               try
               {
                  row.BeginEdit();
                  row["PostDate"] = ed.dateTimePicker1.Value;
                  row.EndEdit();
                  DCEAccessLib.DCEWebAccess.WebAccess.UpdateDataSets(
                     new string[] {
                                     "select * from BulletinBoard where Training='"+this.Node.TrainingId+"'",
                                     "select * from dbo.Content where eid='" + ed.CComment.eId + "'"
                                  }
                     , new string[] {"Messages","dbo.Content"}
                     , new DataSet[] { this.dataSet,ed.CComment.dataSet }
                     );
               }
               finally
               {
                  this.dataList.SuspendListChange = false;
                  this.RefreshData();
                  this.dataList.UpdateList();
               }
            }
         }
      }

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            if ( MessageBox.Show("Вы действительно хотите удалить выбранное объявление?","Удалить",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
               DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;
               try
               {
                  DCEWebAccess.WebAccess.ExecSQL("Delete from BulletinBoard where id='"+row["id"].ToString()+"'");
               }
               finally
               {
                  this.RefreshData();            
               }
            }
         }      
      }
	}

   public class TrainingBBNode : NodeControl
   {
      public string TrainingId;

      public TrainingBBNode(NodeControl parent, string trainingid)
         : base (parent)
      {
         this.TrainingId = trainingid;
      }

      public override String GetCaption()
      {
         return "Объявления";
      }

      public override bool CanClose()
      {
         return false;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.TrainingBB(this);
         }
         if (needRefresh)
         {
            ((TrainingBB)this.fControl).RefreshData();
            needRefresh = false;
         }
         return this.fControl;
      }

   } 
}
