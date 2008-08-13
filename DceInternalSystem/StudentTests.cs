using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;
using System.Diagnostics;

namespace DCEInternalSystem
{
	/// <summary>
	/// Тесты студента
	/// </summary>
	public class StudentTests : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.MenuItem menuItem5;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnProps;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader7;
      private DCEAccessLib.DataColumnHeader dataColumnHeader11;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader8;
      private DCEAccessLib.DataColumnHeader dataColumnHeader9;
      private DCEAccessLib.DataColumnHeader dataColumnHeader10;
      private DCEAccessLib.DataColumnHeader CourseNameCol;
      private DCEAccessLib.DataColumnHeader TestNameCol;

      StudentTestsNode Node;
		public StudentTests(StudentTestsNode node)
		{
		   this.Node = node;
         InitializeComponent();
         this.toolBar1.ImageList = ToolbarImages.Images.images;
         this.CourseNameCol.OnParse += new DataColumnHeader.FieldParseHandler(OnCourseNameParse);
         this.TestNameCol.OnParse += new DataColumnHeader.FieldParseHandler(OnCourseNameParse);
         RefreshData();
		}

      public void OnCourseNameParse(string fieldName,DataRowView row, ref string text)
      {
         if (fieldName =="CourseName")
            text = row["CourseName"].ToString() + " ("+row["Version"].ToString()+")";
         else
         {
            if (row["Parent"].ToString() == row["CourseId"].ToString())
               text = "Курсовой тест";
         }
      }

      public void RefreshData()
      {
         this.dataSet = DCEWebAccess.WebAccess.GetDataSet(
            @"select dbo.GetStrContentAlt(c.Name,'RU','EN') as CourseName,  c.Version, c.id as CourseId, tst.Parent,
            dbo.GetThemeName(tst.Parent,1) as TestName, tr.Complete,
            dbo.TestResultPoints(tr.id) as resPoints,tst.Points,
            tr.CompletionDate,tr.Tries,tr.AllowTries,tr.Test
            from Tests tst, TestResults tr, Courses c 
            where  c.id = dbo.GetTestCourse(tst.id) and tr.Test = tst.id and tst.Type = "+((int)TestType.test).ToString()+@"
               and tr.Student='"+Node.StudentId+"'",
            "da"
            );
         this.dataView.Table = this.dataSet.Tables["da"];

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
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.menuItem5 = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnProps = new System.Windows.Forms.ToolBarButton();
         this.dataList = new DCEAccessLib.DataList();
         this.CourseNameCol = new DCEAccessLib.DataColumnHeader();
         this.TestNameCol = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader7 = new DCEAccessLib.DataColumnHeader();
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
                                                                                     this.menuItem4,
                                                                                     this.menuItem5,
                                                                                     this.menuItem3});
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 0;
         this.menuItem4.Text = "Посмотреть результаты теста";
         this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
         // 
         // menuItem5
         // 
         this.menuItem5.Index = 1;
         this.menuItem5.Text = "-";
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 2;
         this.menuItem3.Text = "Обновить";
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
                                                                                    this.btnProps});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(704, 37);
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
         this.btnProps.ImageIndex = 5;
         this.btnProps.Text = "Просмотр";
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.CourseNameCol,
                                                                               this.TestNameCol,
                                                                               this.dataColumnHeader7,
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
         this.dataList.Size = new System.Drawing.Size(704, 419);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 41;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItem4_Click);
         // 
         // CourseNameCol
         // 
         this.CourseNameCol.FieldName = "CourseName";
         this.CourseNameCol.Text = "Курс (<версия>)";
         this.CourseNameCol.Width = 150;
         // 
         // TestNameCol
         // 
         this.TestNameCol.FieldName = "TestName";
         this.TestNameCol.Text = "Тема";
         this.TestNameCol.Width = 150;
         // 
         // dataColumnHeader7
         // 
         this.dataColumnHeader7.FieldName = "Complete";
         this.dataColumnHeader7.Text = "Сдан";
         this.dataColumnHeader7.Width = 50;
         // 
         // dataColumnHeader11
         // 
         this.dataColumnHeader11.FieldName = "resPoints";
         this.dataColumnHeader11.Text = "Баллы";
         this.dataColumnHeader11.Width = 50;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "Points";
         this.dataColumnHeader1.Text = "Требуемые баллы";
         this.dataColumnHeader1.Width = 150;
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
         // StudentTests
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "StudentTests";
         this.Size = new System.Drawing.Size(704, 456);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         this.RefreshData();
      }

      private void menuItem4_Click(object sender, System.EventArgs e)
      {
//         System.p
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row  = (DataRowView) this.dataList.SelectedItems[0].Tag;
            Process p = new Process();
            p.StartInfo.FileName = DCEAccessLib.Settings.DCEWebAddr 
               + "/Statistics.aspx?studentId="+this.Node.StudentId+"&testId="+row["Test"].ToString();
               p.StartInfo.UseShellExecute = true;
            p.Start();
         }
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
            this.RefreshData();
         if (e.Button == this.btnProps)
            this.menuItem4_Click(null,null);
      }

	}

   public class StudentTestsNode : NodeControl
   {
      public string StudentId;
      public StudentTestsNode (NodeControl parent,string studentid)
         : base(parent)
      {
         StudentId =studentid;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new StudentTests(this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Тесты";
      }

      public override bool HaveChildNodes()
      {
         return false;
      }
   }
}
