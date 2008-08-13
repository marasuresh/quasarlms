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
   /// Класс описывающий ноду "Студенты"
   /// </summary>
   public class StudentsControl : NodeControl
   {
     public StudentsControl (NodeControl parent)
         : base(parent)
      {
      }
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.StudentList(this);
         }
         return this.fControl;
      }

      public override bool HaveChildNodes() { return false; }
      public override String GetCaption()
      {
         return "Студенты";
      }
      
   }
   /// <summary>
	/// Список студентов
	/// </summary>
	public class StudentList : System.Windows.Forms.UserControl
	{
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem5;
      private System.ComponentModel.IContainer components;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnAdd;
      private System.Windows.Forms.ToolBarButton btnDel;
      private System.Windows.Forms.ToolBarButton btnProps;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader3;
      private DCEAccessLib.DataColumnHeader dataColumnHeader4;
      private DCEAccessLib.DataColumnHeader dataColumnHeader5;

      private StudentsControl Node;
		public StudentList(StudentsControl node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.toolBar1.ImageList = ToolbarImages.Images.images;

         Node = node;

         if (DCEUser.CurrentUser.Students != DCEUser.Access.Modify)
         {
            menuItem5.Enabled = false;
            menuItem3.Enabled = false;
            btnAdd.Enabled = false;
            btnDel.Enabled = false;
         }

         RefreshData();
		}

      protected void RefreshData()
      {
         dataSet = DCEAccessLib.DAL.Student.GetDataSet();
         dataView.Table = dataSet.Tables["Students"];
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
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItem5 = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnAdd = new System.Windows.Forms.ToolBarButton();
         this.btnDel = new System.Windows.Forms.ToolBarButton();
         this.btnProps = new System.Windows.Forms.ToolBarButton();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader3 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader4 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader5 = new DCEAccessLib.DataColumnHeader();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // contextMenu1
         // 
         this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItem1,
                                                                                     this.menuItem5,
                                                                                     this.menuItem2,
                                                                                     this.menuItem3});
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 0;
         this.menuItem1.Text = "Свойства...";
         this.menuItem1.Click += new System.EventHandler(this.dataList_DoubleClick);
         // 
         // menuItem5
         // 
         this.menuItem5.Index = 1;
         this.menuItem5.Text = "Удалить...";
         this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 2;
         this.menuItem2.Text = "-";
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 3;
         this.menuItem3.Text = "Новый студент...";
         this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
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
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(444, 40);
         this.toolBar1.TabIndex = 38;
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
                                                                               this.dataColumnHeader4,
                                                                               this.dataColumnHeader5});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.HideSelection = false;
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(444, 363);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 39;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.dataList_DoubleClick);
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "LastName";
         this.dataColumnHeader2.Text = "Фамилия";
         this.dataColumnHeader2.Width = 120;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "FirstName";
         this.dataColumnHeader1.Text = "Имя";
         this.dataColumnHeader1.Width = 120;
         // 
         // dataColumnHeader3
         // 
         this.dataColumnHeader3.FieldName = "Patronymic";
         this.dataColumnHeader3.Text = "Отчество";
         this.dataColumnHeader3.Width = 120;
         // 
         // dataColumnHeader4
         // 
         this.dataColumnHeader4.FieldName = "Email";
         this.dataColumnHeader4.Text = "Email";
         this.dataColumnHeader4.Width = 120;
         // 
         // dataColumnHeader5
         // 
         this.dataColumnHeader5.FieldName = "LastLogin";
         this.dataColumnHeader5.Text = "Последний вход";
         this.dataColumnHeader5.Width = 100;
         // 
         // StudentList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "StudentList";
         this.Size = new System.Drawing.Size(444, 400);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void dataList_DoubleClick(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0 && this.dataList.SelectedItems[0].Tag!=null)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;

            bool neednew = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(StudentEditNode) )
               {
                  if ( ((StudentEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     node.Select();
                     neednew = false;
                     break;
                  }
               }
            }
            if (neednew)
            {
               StudentEditNode node = new StudentEditNode(Node,row["id"].ToString());
               node.Select();
            }
         }
      }

      private void menuItem5_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0 && this.dataList.SelectedItems[0].Tag!=null)      
         {
            DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;
            bool candelete = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(StudentEditNode) )
               {
                  if ( ((StudentEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     System.Windows.Forms.MessageBox.Show("Перед удалением, закройте окно редактирования.","Удалить");
                     node.Select();
                     candelete = false;
                     break;
                  }
               }
            }
            if (candelete)
               if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данную запись?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
               {
                  try
                  {
					  DCEAccessLib.DAL.Student.Delete((Guid)row["id"]);
                  }
                  finally
                  {
                     RefreshData();
                  }
               }
         }
      }

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         StudentEditNode node = new StudentEditNode(Node,"");
         node.Select();
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)      
         {
            this.RefreshData();
         }
         if (e.Button == this.btnAdd)      
         {
            menuItem3_Click(null,null);
         }
         if (e.Button == this.btnDel)      
         {
            menuItem5_Click(null,null);
         }
         if (e.Button == this.btnProps)      
         {
            this.dataList_DoubleClick(null,null);
         }
      }
	}
}
