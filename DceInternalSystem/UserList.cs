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
	/// Список пользователей
	/// </summary>
	public class UserList : System.Windows.Forms.UserControl
	{
      private System.Data.DataView dataView;
      private System.Data.DataSet dataSet;
      private System.Windows.Forms.ContextMenu ListMenu;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem4;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnAdd;
      private System.Windows.Forms.ToolBarButton btnDel;
      private System.Windows.Forms.ToolBarButton btnProps;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader3;
      private DCEAccessLib.DataColumnHeader dataColumnHeader5;
      private DCEAccessLib.DataColumnHeader dataColumnHeader4;
      
      UsersControl Node=null;
		public UserList(UsersControl node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.toolBar1.ImageList = ToolbarImages.Images.images;

         if (DCEUser.CurrentUser.Users != DCEUser.Access.Modify)
         {
            menuItem2.Enabled = false;
            menuItem4.Enabled = false;
            this.btnAdd.Enabled =false;
            this.btnDel.Enabled = false;
         }
         Node = node;
         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select * from dbo.Users","Users");
         dataView.Table = dataSet.Tables["Users"];
		}

      public UserList()
      {
         InitializeComponent();
         this.Controls.Remove(this.toolBar1);
         this.toolBar1 = null;
         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select * from dbo.Users","Users");
         dataView.Table = dataSet.Tables["Users"];
         this.dataList.ContextMenu = null;
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
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.ListMenu = new System.Windows.Forms.ContextMenu();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnAdd = new System.Windows.Forms.ToolBarButton();
         this.btnDel = new System.Windows.Forms.ToolBarButton();
         this.btnProps = new System.Windows.Forms.ToolBarButton();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader3 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader5 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader4 = new DCEAccessLib.DataColumnHeader();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // ListMenu
         // 
         this.ListMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuItem1,
                                                                                 this.menuItem2,
                                                                                 this.menuItem3,
                                                                                 this.menuItem4});
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 0;
         this.menuItem1.Text = "Свойства";
         this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 1;
         this.menuItem2.Text = "Удалить";
         this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 2;
         this.menuItem3.Text = "-";
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 3;
         this.menuItem4.Text = "Новый пользователь";
         this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
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
         this.toolBar1.Size = new System.Drawing.Size(448, 37);
         this.toolBar1.TabIndex = 39;
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
                                                                               this.dataColumnHeader2,
                                                                               this.dataColumnHeader1,
                                                                               this.dataColumnHeader3,
                                                                               this.dataColumnHeader5,
                                                                               this.dataColumnHeader4});
         this.dataList.ContextMenu = this.ListMenu;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(448, 219);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 40;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItem1_Click);
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "LastName";
         this.dataColumnHeader2.Text = "Фамилия";
         this.dataColumnHeader2.Width = 100;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "FirstName";
         this.dataColumnHeader1.Text = "Имя";
         this.dataColumnHeader1.Width = 100;
         // 
         // dataColumnHeader3
         // 
         this.dataColumnHeader3.FieldName = "Patronymic";
         this.dataColumnHeader3.Text = "Отчество";
         this.dataColumnHeader3.Width = 100;
         // 
         // dataColumnHeader5
         // 
         this.dataColumnHeader5.FieldName = "JobPosition";
         this.dataColumnHeader5.Text = "Должность";
         this.dataColumnHeader5.Width = 100;
         // 
         // dataColumnHeader4
         // 
         this.dataColumnHeader4.FieldName = "Email";
         this.dataColumnHeader4.Text = "Email";
         this.dataColumnHeader4.Width = 100;
         // 
         // UserList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "UserList";
         this.Size = new System.Drawing.Size(448, 256);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion


      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0 && this.dataList.SelectedItems[0].Tag!=null)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;

            bool neednew = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(UserEditNode) )
               {
                  if ( ((UserEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     node.Select();
                     neednew = false;
                     break;
                  }
               }
            }
            if (neednew)
            {
               UserEditNode node = new UserEditNode(Node,row["id"].ToString());
               node.Select();
            }
         }
      }

      private void menuItem4_Click(object sender, System.EventArgs e)
      {
         UserEditNode node = new UserEditNode(Node,"");
         node.Select();
      }

      private void menuItem2_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0 && this.dataList.SelectedItems[0].Tag!=null)      
         {
            DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;
            bool candelete = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(UserEditNode) )
               {
                  if ( ((UserEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     System.Windows.Forms.MessageBox.Show("Перед удалением редактируемого пользователя, закройте окно редактирования.","Удалить");
                     node.Select();
                     candelete = false;
                     break;
                  }
               }
            }
            if (candelete)
            if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данную запись?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from Users where id='"+row["id"].ToString()+"'");
               this.RefreshData();
            }
         }
      }

      public void RefreshData()
      {
         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select * from dbo.Users","Users");
         dataView.Table = dataSet.Tables["Users"];
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         //
         if (e.Button == this.btnRefresh)
            this.RefreshData();
         if (e.Button == this.btnAdd)
            menuItem4_Click(null,null);
         if (e.Button == this.btnDel)
            menuItem2_Click(null,null);
         if (e.Button == this.btnProps)
            menuItem1_Click(null,null);
      }
	}

   /// <summary>
   /// Класс описывающий ноду "Пользователи"
   /// </summary>
   public class UsersControl : NodeControl
   {
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new UserList(this);
            needRefresh = false;
         }
         if (needRefresh)
         {
            ((UserList)this.fControl).RefreshData();
            needRefresh = false;
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Пользователи";
      }
      
      public UsersControl (NodeControl parent)
         : base(parent) 
      {
      }
      
      public override bool HaveChildNodes() { return false; }
      
      public override  void ReleaseControl()
      {
      }
   }
}
