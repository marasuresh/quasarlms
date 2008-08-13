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
	/// Тренинги студента
	/// </summary>
	public class StudentTrainings : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader CourseNameCol;
      private System.Windows.Forms.MenuItem menuItem4;
      private DCEAccessLib.DataColumnHeader TrNameCol;
      private System.Data.DataView dataView;
      private System.Data.DataSet dataSet;
      private System.Windows.Forms.ContextMenu contextMenu1;

      public StudentTrainingsNode Node;
      public StudentTrainings(StudentTrainingsNode node)
		{
         this.Node = node;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
         this.toolBar1.ImageList = ToolbarImages.Images.images;

         DataColumnHeader.FieldParseHandler h = new DataColumnHeader.FieldParseHandler(FldParse);
         this.CourseNameCol.OnParse += h;
         this.TrNameCol.OnParse += h;

         RefreshData();
		}

      public void FldParse(string FieldName, DataRowView row, ref string text)
      {
         if (FieldName == "CName")
            text = row["CName"].ToString() + " ("+row["Version"].ToString() +")";
         else
            text = row["TName"].ToString() + " ("+row["Code"].ToString() +")";
   }

      public void RefreshData()
      {
         this.dataSet = DCEWebAccess.GetdataSet(
@"select dbo.GetStrContentAlt(t.Name,'RU','EN') as TName , t.Code , 
 dbo.GetStrContentAlt(c.Name,'RU','EN') as CName, c.Version
 from dbo.AllStudentTrainings('"+this.Node.StudentId+@"') al , Trainings t,
Courses c
where
  t.id = al.id and c.id = t.Course","Tr");
         this.dataView.Table = this.dataSet.Tables["Tr"];
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
         this.dataList = new DCEAccessLib.DataList();
         this.CourseNameCol = new DCEAccessLib.DataColumnHeader();
         this.TrNameCol = new DCEAccessLib.DataColumnHeader();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // toolBar1
         // 
         this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
         this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                    this.btnRefresh});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(568, 37);
         this.toolBar1.TabIndex = 41;
         // 
         // btnRefresh
         // 
         this.btnRefresh.ImageIndex = 0;
         this.btnRefresh.Text = "Обновить";
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.CourseNameCol,
                                                                               this.TrNameCol});
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(568, 295);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 42;
         this.dataList.View = System.Windows.Forms.View.Details;
         // 
         // CourseNameCol
         // 
         this.CourseNameCol.FieldName = "CName";
         this.CourseNameCol.Text = "Курс (<версия>)";
         this.CourseNameCol.Width = 250;
         // 
         // TrNameCol
         // 
         this.TrNameCol.FieldName = "TName";
         this.TrNameCol.Text = "Тренинг (<код>)";
         this.TrNameCol.Width = 250;
         // 
         // menuItem4
         // 
         this.menuItem4.Index = -1;
         this.menuItem4.Text = "Посмотреть результаты теста";
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // StudentTrainings
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "StudentTrainings";
         this.Size = new System.Drawing.Size(568, 332);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion
	}

   public class StudentTrainingsNode : NodeControl
   {
      public string StudentId;
      public StudentTrainingsNode(NodeControl parent,string studentid)
         : base(parent)
      {
         StudentId =studentid;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new StudentTrainings(this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Тренинги";
      }

      public override bool HaveChildNodes()
      {
         return false;
      }
   }

}
