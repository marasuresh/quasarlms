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
	/// Список нитей форума
	/// </summary>
	public class TrainingForum : System.Windows.Forms.UserControl
	{
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnAdd;
      private System.Windows.Forms.ToolBarButton btnDel;
      private System.Windows.Forms.ToolBarButton btnProps;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader3;
      private DCEAccessLib.DataColumnHeader AuthorColumn;
      private DCEAccessLib.DataColumnHeader dataColumnHeader4;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader StundentNameCol;

      TrainingForumNode Node;
		public TrainingForum(TrainingForumNode node)
		{
         this.Node=node;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.btnAdd.Enabled = this.Node.CanModify;
         this.btnDel.Enabled = this.Node.CanModify;
         this.menuItem3.Enabled = this.Node.CanModify;
         this.menuItem4.Enabled = this.Node.CanModify;

         this.toolBar1.ImageList = ToolbarImages.Images.images;
         AuthorColumn.OnParse += new DataColumnHeader.FieldParseHandler(ParseAuthor);
         this.StundentNameCol.OnParse += new DataColumnHeader.FieldParseHandler(ParseStudent);
         RefreshData();
		}

      void ParseStudent(string field, DataRowView row,ref string text)
      {
         if ( row["StudentName"].GetType() == typeof(System.DBNull) /*|| row["StudentName"].ToString() == ""*/)
            text = "[Публичная тема]";
  //       text = row["stid"].ToString();
      }
      void ParseAuthor(string field, DataRowView row,ref string text)
      {
         if ( (int) row["Type"] == (int)EntityType.student)
            text += " (студент)";
      }

      public void RefreshData()
      {
         this.dataSet = DCEAccessLib.DAL.Training.GetForumTopics(new Guid(this.Node.TrainingId));
         this.dataView.Table = dataSet.Tables["topics"];
      }
		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         this.Node=null;
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
         this.dataSet = new System.Data.DataSet();
         this.dataView = new System.Data.DataView();
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
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
         this.dataColumnHeader3 = new DCEAccessLib.DataColumnHeader();
         this.AuthorColumn = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader4 = new DCEAccessLib.DataColumnHeader();
         this.StundentNameCol = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.SuspendLayout();
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
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
         this.menuItem3.Text = "Новая тема";
         this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 3;
         this.menuItem4.Text = "Удалить";
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
         this.toolBar1.Size = new System.Drawing.Size(736, 37);
         this.toolBar1.TabIndex = 28;
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
                                                                               this.dataColumnHeader3,
                                                                               this.AuthorColumn,
                                                                               this.dataColumnHeader4,
                                                                               this.StundentNameCol,
                                                                               this.dataColumnHeader1});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.HideSelection = false;
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(736, 291);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 29;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItem1_Click);
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "PostDate";
         this.dataColumnHeader2.Text = "Дата";
         this.dataColumnHeader2.Width = 100;
         // 
         // dataColumnHeader3
         // 
         this.dataColumnHeader3.FieldName = "Topic";
         this.dataColumnHeader3.Text = "Тема";
         this.dataColumnHeader3.Width = 300;
         // 
         // AuthorColumn
         // 
         this.AuthorColumn.FieldName = "Author";
         this.AuthorColumn.Text = "Автор";
         this.AuthorColumn.Width = 140;
         // 
         // dataColumnHeader4
         // 
         this.dataColumnHeader4.FieldName = "NumAnswers";
         this.dataColumnHeader4.Text = "Ответов";
         // 
         // StundentNameCol
         // 
         this.StundentNameCol.FieldName = "StudentName";
         this.StundentNameCol.Text = "Для студента";
         this.StundentNameCol.Width = 150;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "Blocked";
         this.dataColumnHeader1.Text = "Заблокирована";
         this.dataColumnHeader1.Width = 120;
         // 
         // TrainingForum
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "TrainingForum";
         this.Size = new System.Drawing.Size(736, 328);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;
            foreach (NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(TopicViewNode))
               {
                  if (((TopicViewNode)node).Id == row["id"].ToString())
                  {
                     node.Select();
                     return;
                  }
               }
            }
            TopicViewNode n = new TopicViewNode(this.Node,row["id"].ToString(),this.Node.TrainingId);
            n.Select();
         }
      }

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         TopicViewNode n = new TopicViewNode(this.Node,"",this.Node.TrainingId);
         n.Select();
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         //
         if (e.Button == this.btnAdd)
            this.menuItem3_Click(null,null);
         if (e.Button == this.btnDel)
            this.menuItem4_Click(null,null);
         if (e.Button == this.btnProps)
            this.menuItem1_Click(null,null);
         if (e.Button == this.btnRefresh)
            this.RefreshData();

      }

      private void menuItem4_Click(object sender, System.EventArgs e)
      {
         // удалить тему
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row =(DataRowView) this.dataList.SelectedItems[0].Tag;
            foreach (NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(TopicViewNode))
               {
                  if (((TopicViewNode)node).Id == row["id"].ToString())
                  {
                     System.Windows.Forms.MessageBox.Show("Перед удалением выбранной темы, закройте окно ее просмотра.","Удалить");
                     node.Select();
                  }
               }
            }
            if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данную тему?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               try
               {
                  DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from ForumTopics where id='"+row["id"].ToString()+"'");
               }
               finally
               {
                  this.RefreshData();
               }
            }
         }
      }
	}

   public class TrainingForumNode : NodeControl
   {
      public string TrainingId;

      public TrainingForumNode (NodeControl parent,string trainingId)
         : base(parent)
      {
         this.TrainingId=trainingId;
         this.rdonly = DCEUser.CurrentUser.Trainings != DCEUser.Access.Modify && 
            !DCEUser.CurrentUser.isCuratorOrInstructor(TrainingId);
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new TrainingForum(this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Форум";
      }

   }


}
