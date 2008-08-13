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
	/// Список тестов тренинга
	/// </summary>
	public class TrainingTests : System.Windows.Forms.UserControl
	{
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.MenuItem SkipTest;
      private System.Windows.Forms.MenuItem AllowRetry;
      private System.Windows.Forms.MenuItem ViewTest;

      TrainingTestsNode Node;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnProps;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader StudentNameCol;
      private DCEAccessLib.DataColumnHeader TestCompleteColumn;
      private DCEAccessLib.DataColumnHeader dataColumnHeader11;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader8;
      private DCEAccessLib.DataColumnHeader dataColumnHeader9;
      private System.Windows.Forms.ToolBarButton btnRetry;
      private System.Windows.Forms.ToolBarButton btnSkip;
      private DCEAccessLib.DataColumnHeader ThemeNameCol;
      private DCEAccessLib.DataColumnHeader dataColumnHeader10;

      public TrainingTests(TrainingTestsNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.StudentNameCol.OnParse += new DataColumnHeader.FieldParseHandler(OnNameParse);
         this.TestCompleteColumn.OnParse += new DataColumnHeader.FieldParseHandler(OnCompleteParse);
         this.ThemeNameCol.OnParse += new DataColumnHeader.FieldParseHandler(OnThemeParse);
         this.Node = node;

         this.toolBar1.ImageList = ToolbarImages.Images.images;

         this.contextMenu1.Popup += new System.EventHandler(PopupMenuHandler);
         RefreshData();
		}

      private void PopupMenuHandler( object sender, EventArgs e)
      {
         AllowRetry.Enabled &= this.Node.CanModify;
         SkipTest.Enabled &= this.Node.CanModify;

      }

      private string CourseId ="";
      public void RefreshData()
      {
         CourseId = DCEAccessLib.DCEWebAccess.WebAccess.GetString("select Course from Trainings where id='"+this.Node.TrainingId+"'");
         this.dataSet = DCEAccessLib.DAL.Training.GetTests(new Guid(this.Node.TrainingId));
         this.dataView.Table = this.dataSet.Tables["Tests"];

      }

      protected void OnThemeParse(string FieldName, DataRowView row, ref string text)
      {
         if (row["Parent"].ToString() == CourseId)
            text = "Курсовой тест";
      }

      protected void OnNameParse(string FieldName, DataRowView row, ref string text)
      {
         text = row["LastName"].ToString()+" "+row["StudentName"].ToString()+" "+row["Patronymic"].ToString();
      }

      protected void OnCompleteParse(string FieldName, DataRowView row, ref string text)
      {
         if ((bool)row["Skipped"])
         {
            text = "Пропущен";
         }
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
         this.ViewTest = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.AllowRetry = new System.Windows.Forms.MenuItem();
         this.SkipTest = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnProps = new System.Windows.Forms.ToolBarButton();
         this.btnRetry = new System.Windows.Forms.ToolBarButton();
         this.btnSkip = new System.Windows.Forms.ToolBarButton();
         this.dataList = new DCEAccessLib.DataList();
         this.StudentNameCol = new DCEAccessLib.DataColumnHeader();
         this.ThemeNameCol = new DCEAccessLib.DataColumnHeader();
         this.TestCompleteColumn = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader11 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader8 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader9 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader10 = new DCEAccessLib.DataColumnHeader();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // contextMenu1
         // 
         this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.ViewTest,
                                                                                     this.menuItem4,
                                                                                     this.AllowRetry,
                                                                                     this.SkipTest});
         // 
         // ViewTest
         // 
         this.ViewTest.Index = 0;
         this.ViewTest.Text = "Просмотр ответов";
         this.ViewTest.Click += new System.EventHandler(this.ViewTest_Click);
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 1;
         this.menuItem4.Text = "-";
         // 
         // AllowRetry
         // 
         this.AllowRetry.Index = 2;
         this.AllowRetry.Text = "Разрешить перездачу";
         this.AllowRetry.Click += new System.EventHandler(this.menuItem1_Click);
         // 
         // SkipTest
         // 
         this.SkipTest.Index = 3;
         this.SkipTest.Text = "Пропустить тест";
         this.SkipTest.Click += new System.EventHandler(this.menuItem2_Click);
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
                                                                                    this.btnProps,
                                                                                    this.btnRetry,
                                                                                    this.btnSkip});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(680, 37);
         this.toolBar1.TabIndex = 40;
         this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
         // 
         // btnRefresh
         // 
         this.btnRefresh.ImageIndex = 0;
         this.btnRefresh.Text = "Обновить";
         // 
         // btnProps
         // 
         this.btnProps.ImageIndex = 3;
         this.btnProps.Text = "Просмотр";
         // 
         // btnRetry
         // 
         this.btnRetry.Text = "Разр. перездачу";
         // 
         // btnSkip
         // 
         this.btnSkip.Text = "Пропустить";
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.StudentNameCol,
                                                                               this.ThemeNameCol,
                                                                               this.TestCompleteColumn,
                                                                               this.dataColumnHeader11,
                                                                               this.dataColumnHeader1,
                                                                               this.dataColumnHeader8,
                                                                               this.dataColumnHeader9,
                                                                               this.dataColumnHeader10});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(680, 423);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 41;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.ViewTest_Click);
         this.dataList.SelectedIndexChanged += new System.EventHandler(this.dataList_SelectedIndexChanged);
         // 
         // StudentNameCol
         // 
         this.StudentNameCol.FieldName = "StudentName";
         this.StudentNameCol.Text = "Студент";
         this.StudentNameCol.Width = 150;
         // 
         // ThemeNameCol
         // 
         this.ThemeNameCol.FieldName = "ThemeName";
         this.ThemeNameCol.Text = "Тема/курс";
         this.ThemeNameCol.Width = 150;
         // 
         // TestCompleteColumn
         // 
         this.TestCompleteColumn.FieldName = "Complete";
         this.TestCompleteColumn.Text = "Сдан";
         this.TestCompleteColumn.Width = 120;
         // 
         // dataColumnHeader11
         // 
         this.dataColumnHeader11.FieldName = "Points";
         this.dataColumnHeader11.Text = "Баллы";
         this.dataColumnHeader11.Width = 50;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "reqPoints";
         this.dataColumnHeader1.Text = "Требуемые баллы";
         this.dataColumnHeader1.Width = 120;
         // 
         // dataColumnHeader8
         // 
         this.dataColumnHeader8.FieldName = "CompletionDate";
         this.dataColumnHeader8.Text = "Дата сдачи";
         this.dataColumnHeader8.Width = 100;
         // 
         // dataColumnHeader9
         // 
         this.dataColumnHeader9.FieldName = "Tries";
         this.dataColumnHeader9.Text = "Попыток";
         // 
         // dataColumnHeader10
         // 
         this.dataColumnHeader10.FieldName = "AllowTries";
         this.dataColumnHeader10.Text = "Осталось попыток";
         this.dataColumnHeader10.Width = 120;
         // 
         // TrainingTests
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "TrainingTests";
         this.Size = new System.Drawing.Size(680, 460);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void dataList_RowParsed(System.Data.DataRowView row, System.Windows.Forms.ListViewItem item)
      {
      ///
      }

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         if (dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView) dataList.SelectedItems[0].Tag;
            if ((int)row["AllowTries"] <=0)
            {
               try
               {
                  DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("Update TestResults set AllowTries=1 where id = '"+row["id"].ToString()+"'");
               }
               finally
               {
                  this.RefreshData();
               }
            }
         }
      }

      private void menuItem2_Click(object sender, System.EventArgs e)
      {
         if (dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView) dataList.SelectedItems[0].Tag;
            try
            {
               string skip = ((bool)row["Skipped"])?"0":"1";
               DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("Update TestResults set Skipped="+skip+" where id = '"+row["id"].ToString()+"'");
            }
            finally
            {
               this.RefreshData();
            }
         }
      }

      private void dataList_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Right)
         {
            ListViewItem itm = this.dataList.GetItemAt(e.X,e.Y);
            if (itm !=null)
            {
               itm.Selected = true;
//               if ( (bool)(((DataRowView)itm.Tag)["Skipped"]))
//               {
//                  this.btnRetry.Enabled = AllowRetry.Enabled = false;
//                  this.btnSkip.Enabled = SkipTest.Enabled = this.Node.CanModify;
//                  SkipTest.Text = "Убрать пропуск теста";
//                  
//               }
//               else
//               {
//                  this.btnRetry.Enabled =  AllowRetry.Enabled = !( (bool)(((DataRowView)itm.Tag)["Complete"])) && this.Node.CanModify;
//                  this.btnSkip.Enabled = SkipTest.Enabled = this.Node.CanModify;
//                  SkipTest.Text = "Пропустить тест";
//               }
            }
         }
      }

      private void ViewTest_Click(object sender, System.EventArgs e)
      {
         if (dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView) dataList.SelectedItems[0].Tag;
            foreach(NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(TestResultsNode))
               {
                  if (((TestResultsNode)node).ResultId == row["id"].ToString())
                  {
                     node.Select();
                     return;
                  }
               }
            }
            TestResultsNode n =new TestResultsNode(this.Node,row["id"].ToString(),row["ThemeName"].ToString(),
               row["LastName"].ToString()+" "+row["StudentName"].ToString()+" "+row["Patronymic"].ToString());
            n.Select();
         }
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
            this.RefreshData();
         if (e.Button == this.btnProps)
            this.ViewTest_Click(null,null);
         if (e.Button == this.btnRetry)
            this.menuItem1_Click(null,null);
         if (e.Button == this.btnSkip)
            this.menuItem2_Click(null,null);
      }

      private void dataList_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView) dataList.SelectedItems[0].Tag;
            if ( (bool)row["Skipped"])
            {
               this.btnRetry.Enabled = AllowRetry.Enabled = false;
               this.btnSkip.Enabled = SkipTest.Enabled = this.Node.CanModify;
               SkipTest.Text = "Убрать пропуск теста";
            }
            else
            {
               this.btnRetry.Enabled =  AllowRetry.Enabled = !((bool)row["Complete"]) && this.Node.CanModify;
               this.btnSkip.Enabled = SkipTest.Enabled = this.Node.CanModify;
               SkipTest.Text = "Пропустить тест";
            }
         }
         
      }
	}


   public class TrainingTestsNode : NodeControl
   {
      public string TrainingId;
      public TrainingTestsNode(NodeControl parent, string trainingId)
         : base(parent)
      {
         this.TrainingId = trainingId;
         this.rdonly = (!DCEUser.CurrentUser.isCurator(TrainingId) 
            && DCEUser.CurrentUser.Trainings != DCEUser.Access.Modify )
            || DCEUser.CurrentUser.Tests != DCEUser.Access.Modify;
      }

      public override string GetCaption()
      {
         return "Тесты / Практика";
      }
      public override UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new TrainingTests(this);
         }
         return fControl;
      }
   }
}
