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
   /// Класс описывающий ноду "Курсы"
   /// </summary>
   public class NewsListControl : NodeControl
   {
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.NewsList(this);
         }
         return this.fControl;
      }

      public NewsListControl (NodeControl parent)
         : base(parent) 
      {
            this.rdonly = DCEUser.CurrentUser.News != DCEUser.Access.Modify;
      }

      public override String GetCaption()
      {
         return "Новости";
      }
      
      //public override bool HaveChildNodes() { return true; }
   }
   /// <summary>
	/// Список новостей
	/// </summary>
   public class NewsList : System.Windows.Forms.UserControl
	{
      private System.Data.DataView dataView;
      private System.Data.DataSet dataSet;
      private System.Windows.Forms.ContextMenu contextMenuNews;
      private System.Windows.Forms.MenuItem menuItemCreate;
      private System.Windows.Forms.MenuItem menuItemDelete;
      private System.Windows.Forms.MenuItem menuItemEdit;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnAdd;
      private System.Windows.Forms.ToolBarButton btnDel;
      private System.Windows.Forms.ToolBarButton btnProps;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader5;
      private DCEAccessLib.DataColumnHeader dataColumnHeader4;
      private System.Windows.Forms.MenuItem menuItem1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NewsList(NewsListControl node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
         this.toolBar1.ImageList = ToolbarImages.Images.images;

         Node = node;

         this.menuItemCreate.Enabled = Node.CanModify;
         this.menuItemDelete.Enabled = Node.CanModify;
         this.btnAdd.Enabled = Node.CanModify;
         this.btnDel.Enabled = Node.CanModify;

         this.RefreshData();
      }
      private NewsListControl Node = null;
         
      protected void RefreshData()
      {
         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select *, dbo.GetStrContentAlt(Head, 'RU','EN') as RHead from dbo.News", "News");
         dataView.Table = dataSet.Tables["News"];
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
      this.dataView = new System.Data.DataView();
      this.dataSet = new System.Data.DataSet();
      this.contextMenuNews = new System.Windows.Forms.ContextMenu();
      this.menuItemEdit = new System.Windows.Forms.MenuItem();
      this.menuItemCreate = new System.Windows.Forms.MenuItem();
      this.menuItemDelete = new System.Windows.Forms.MenuItem();
      this.toolBar1 = new System.Windows.Forms.ToolBar();
      this.btnRefresh = new System.Windows.Forms.ToolBarButton();
      this.btnAdd = new System.Windows.Forms.ToolBarButton();
      this.btnDel = new System.Windows.Forms.ToolBarButton();
      this.btnProps = new System.Windows.Forms.ToolBarButton();
      this.dataList = new DCEAccessLib.DataList();
      this.dataColumnHeader5 = new DCEAccessLib.DataColumnHeader();
      this.dataColumnHeader4 = new DCEAccessLib.DataColumnHeader();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
      this.SuspendLayout();
      // 
      // dataSet
      // 
      this.dataSet.DataSetName = "NewDataSet";
      this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
      // 
      // contextMenuNews
      // 
      this.contextMenuNews.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItemEdit,
                                                                                     this.menuItem1,
                                                                                     this.menuItemCreate,
                                                                                     this.menuItemDelete});
      // 
      // menuItemEdit
      // 
      this.menuItemEdit.Index = 0;
      this.menuItemEdit.Text = "Свойства";
      this.menuItemEdit.Click += new System.EventHandler(this.menuItemEdit_Click);
      // 
      // menuItemCreate
      // 
      this.menuItemCreate.Index = 2;
      this.menuItemCreate.Text = "Добавить";
      this.menuItemCreate.Click += new System.EventHandler(this.menuItemCreate_Click);
      // 
      // menuItemDelete
      // 
      this.menuItemDelete.Index = 3;
      this.menuItemDelete.Text = "Удалить";
      this.menuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
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
      this.toolBar1.Name = "toolBar1";
      this.toolBar1.ShowToolTips = true;
      this.toolBar1.Size = new System.Drawing.Size(560, 37);
      this.toolBar1.TabIndex = 40;
      this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
      // 
      // btnRefresh
      // 
      this.btnRefresh.ImageIndex = 0;
      this.btnRefresh.Text = "Обновить";
      // 
      // btnAdd
      // 
      this.btnAdd.ImageIndex = 1;
      this.btnAdd.Text = "Добавить";
      // 
      // btnDel
      // 
      this.btnDel.ImageIndex = 2;
      this.btnDel.Text = "Удалить";
      // 
      // btnProps
      // 
      this.btnProps.ImageIndex = 3;
      this.btnProps.Text = "Свойства";
      // 
      // dataList
      // 
      this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
      this.dataList.AllowColumnReorder = true;
      this.dataList.AllowSorting = true;
      this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                            this.dataColumnHeader5,
                                                                            this.dataColumnHeader4});
      this.dataList.ContextMenu = this.contextMenuNews;
      this.dataList.DataView = this.dataView;
      this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataList.FullRowSelect = true;
      this.dataList.GridLines = true;
      this.dataList.Location = new System.Drawing.Point(0, 37);
      this.dataList.MultiSelect = false;
      this.dataList.Name = "dataList";
      this.dataList.Size = new System.Drawing.Size(560, 427);
      this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.dataList.TabIndex = 41;
      this.dataList.View = System.Windows.Forms.View.Details;
      this.dataList.DoubleClick += new System.EventHandler(this.menuItemEdit_Click);
      // 
      // dataColumnHeader5
      // 
      this.dataColumnHeader5.FieldName = "NewsDate";
      this.dataColumnHeader5.Text = "Дата";
      this.dataColumnHeader5.Width = 100;
      // 
      // dataColumnHeader4
      // 
      this.dataColumnHeader4.FieldName = "RHead";
      this.dataColumnHeader4.Text = "Заголовок";
      this.dataColumnHeader4.Width = 450;
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 1;
      this.menuItem1.Text = "-";
      // 
      // NewsList
      // 
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                   this.dataList,
                                                                   this.toolBar1});
      this.Name = "NewsList";
      this.Size = new System.Drawing.Size(560, 464);
      ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
      this.ResumeLayout(false);

   }
   #endregion

      private void menuItemCreate_Click(object sender, System.EventArgs e)
      {
         NewsEditNode node = new NewsEditNode(Node, "");
         node.Select();
      }

      private void menuItemDelete_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0 && this.dataList.SelectedItems[0].Tag!=null)      
         {
            DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;
            bool candelete = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(NewsEditNode) )
               {
                  if ( ((NewsEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     System.Windows.Forms.MessageBox.Show("Перед удалением, закройте окно редактирования.","Удалить");
                     node.Select();
                     candelete = false;
                     break;
                  }
               }
            }
            if (candelete)
            {
               if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данную запись?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
               {
                  try
                  {
                     DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("Delete from dbo.News where id='" + row["id"].ToString() + "'");
                  }
                  finally
                  {
                     RefreshData();
                  }
               }
            }
         }
      }

      private void menuItemEdit_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0 && this.dataList.SelectedItems[0].Tag!=null)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;

            bool neednew = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(NewsEditNode) )
               {
                  if ( ((NewsEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     node.Select();
                     neednew = false;
                     break;
                  }
               }
            }
            if (neednew)
            {
               NewsEditNode node = new NewsEditNode(Node,row["id"].ToString());
               node.Select();
            }
         }
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
            this.RefreshData();
         if (e.Button == this.btnAdd)
            this.menuItemCreate_Click(null,null);
         if (e.Button == this.btnDel)
            this.menuItemDelete_Click(null,null);
         if (e.Button == this.btnProps)
            this.menuItemEdit_Click(null,null);
      }
	}
}
