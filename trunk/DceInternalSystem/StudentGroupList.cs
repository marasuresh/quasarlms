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
   /// Класс описывающий ноду "Группы студентов"
   /// </summary>
   public class StudentGroupsControl : NodeControl
   {
      public StudentGroupsControl    (NodeControl parent)
            : base(parent)
      {
         this.rdonly = DCEUser.CurrentUser.Students != DCEUser.Access.Modify;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.StudentGroupList(this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Группы студентов";
      }

      public override bool HaveChildNodes()
      {
         return false;
      }
   }
   /// <summary>
	/// Список групп студентов
	/// </summary>
	public class StudentGroupList : System.Windows.Forms.UserControl
	{
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.ContextMenu contextMenu1;
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
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;

      StudentGroupsControl Node;
		public StudentGroupList(StudentGroupsControl node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
         this.toolBar1.ImageList = ToolbarImages.Images.images;

         Node = node;

         btnAdd.Enabled =
         btnDel.Enabled =
         menuItem3.Enabled =
         menuItem4.Enabled = this.Node.CanModify;

         RefreshData();
		}

      public void RefreshData()
      {
         // Показываем тока те группы, которые не принадлежат трекам или тренингам
		  string query;
		  if (DCEUser.CurrentUser.Students == DCEUser.Access.No)
			query = "select * from Groups where type ="+ ((int)EntityType.student).ToString()+
			   " and id not in (SELECT Students from dbo.Trainings UNION (SELECT Students from dbo.Tracks))"
			   +" and id in ( select DISTINCT g.id from Groups g,Rights r where g.type ="+ ((int)EntityType.student).ToString()+
              "and g.id=r.permid and r.eid='"+DCEUser.CurrentUser.id+"' and g.id not in (SELECT Students from dbo.Trainings UNION (SELECT Students from dbo.Tracks)) ) ";
		  else
			query = "select * from Groups where type ="+ ((int)EntityType.student).ToString()+
               " and id not in (SELECT Students from dbo.Trainings UNION (SELECT Students from dbo.Tracks))";

         this.dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
			 query, "Groups"  );
         this.dataView.Table = this.dataSet.Tables["Groups"];
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
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnAdd = new System.Windows.Forms.ToolBarButton();
         this.btnDel = new System.Windows.Forms.ToolBarButton();
         this.btnProps = new System.Windows.Forms.ToolBarButton();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // contextMenu1
         // 
         this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
         this.menuItem2.Text = "-";
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 2;
         this.menuItem3.Text = "Новая группа";
         this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 3;
         this.menuItem4.Text = "Удалить";
         this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
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
         this.toolBar1.Size = new System.Drawing.Size(448, 37);
         this.toolBar1.TabIndex = 42;
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
                                                                               this.dataColumnHeader1,
                                                                               this.dataColumnHeader2});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(448, 355);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 43;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItem1_Click);
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "Name";
         this.dataColumnHeader1.Text = "Название";
         this.dataColumnHeader1.Width = 300;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "Description";
         this.dataColumnHeader2.Text = "Описание";
         this.dataColumnHeader2.Width = 400;
         // 
         // StudentGroupList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "StudentGroupList";
         this.Size = new System.Drawing.Size(448, 392);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         GroupEditNode node = new GroupEditNode(this.Node ,"",EntityType.student,
            true,"Новая группа", "",true,true);
         node.Select();
      }

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;
            foreach (NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(GroupEditNode))
               {
                  if (((GroupEditNode)node).Id == row["id"].ToString())
                  {
                     node.Select();
                     return;
                  }
               }
            }

            GroupEditNode n = new GroupEditNode(this.Node ,row["id"].ToString()
               ,EntityType.student, true,row["Name"].ToString(), "",true,true);
            n.Select();
         }
      }

      private void menuItem4_Click(object sender, System.EventArgs e)
      {
         // delete group

         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;
            foreach (NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(GroupEditNode))
               {
                  if (((GroupEditNode)node).Id == row["id"].ToString())
                  {
                     MessageBox.Show("Выбранная группа сейчас редактируется. Закройте окно редактирования для ее удаления.","Удалить");
                     node.Select();
                     return;
                  }
               }
            }

            if( MessageBox.Show("Вы действительно хотите удалить выбранную группу?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               try
               {
                  DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from dbo.Groups where id='"+row["id"].ToString() + "'");
               }
               finally
               {
                  RefreshData();
               }
               
            }
         }

      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         ///
         if (e.Button == this.btnRefresh)
            this.RefreshData();
         if (e.Button == this.btnAdd)
            menuItem3_Click(null,null);
         if (e.Button == this.btnDel)
            menuItem4_Click(null,null);
         if (e.Button == this.btnProps)
            menuItem1_Click(null,null);
      }


	}
}
