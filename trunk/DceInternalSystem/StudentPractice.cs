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
	/// Практические работы студента
	/// </summary>
	public class StudentPractice : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnProps;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader5;
      private DCEAccessLib.DataColumnHeader dataColumnHeader6;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
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

      public StudentPracticeNode Node;
		public StudentPractice(StudentPracticeNode node)
		{
         Node = node;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.toolBar1.ImageList = ToolbarImages.Images.images;
         RefreshData();

		}

      public void RefreshData()
      {
         this.dataSet = DCEWebAccess.WebAccess.GetDataSet(
            @"select (select dbo.GetStrContentAlt(Name,'RU','EN') from Courses where id=dbo.GetTestCourse(tst.id)) as CourseName,
            dbo.GetThemeName(tst.Parent,1) as TestName, tr.Complete,
            tr.CompletionDate, tr.Test
            from Tests tst, TestResults tr
            where tr.Test = tst.id and tst.Type = "+((int)TestType.practice).ToString()+@"
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
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnProps = new System.Windows.Forms.ToolBarButton();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader5 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader6 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
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
         this.toolBar1.Size = new System.Drawing.Size(640, 37);
         this.toolBar1.TabIndex = 41;
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
                                                                               this.dataColumnHeader5,
                                                                               this.dataColumnHeader6,
                                                                               this.dataColumnHeader2});
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(640, 435);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 42;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItem1_Click);
         // 
         // dataColumnHeader5
         // 
         this.dataColumnHeader5.FieldName = "CourseName";
         this.dataColumnHeader5.Text = "Курс";
         this.dataColumnHeader5.Width = 200;
         // 
         // dataColumnHeader6
         // 
         this.dataColumnHeader6.FieldName = "TestName";
         this.dataColumnHeader6.Text = "Тема";
         this.dataColumnHeader6.Width = 200;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "CompletionDate";
         this.dataColumnHeader2.Text = "Дата выполнения";
         this.dataColumnHeader2.Width = 130;
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
                                                                                     this.menuItem3});
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 0;
         this.menuItem1.Text = "Просмотр результатов выполенения";
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
         this.menuItem3.Text = "Обновить";
         this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
         // 
         // StudentPractice
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "StudentPractice";
         this.Size = new System.Drawing.Size(640, 472);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         ///
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

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         this.RefreshData();
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
            this.RefreshData();
         if (e.Button == this.btnProps)
            this.menuItem1_Click(null,null);
      }
	}

   public class StudentPracticeNode : NodeControl
   {
      public string StudentId;
      public StudentPracticeNode (NodeControl parent, string studentid)
         : base(parent)
      {
         this.StudentId = studentid;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new StudentPractice(this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Практические работы";
      }

      public override bool HaveChildNodes()
      {
         return false;
      }
   }
}

