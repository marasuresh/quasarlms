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
	/// Анкеты студента
	/// </summary>
	public class StudentQuestionnaire : System.Windows.Forms.UserControl
	{
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private System.Windows.Forms.ToolBarButton btnView;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;

      private string Student;
		public StudentQuestionnaire(string student)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         Student = student;
         this.toolBar1.ImageList = ToolbarImages.Images.images;
         RefreshData();
		}
      public void RefreshData()
      {
         this.dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
@"select tr.Test, tr.id, t.InternalName, tr.CompletionDate from Tests t , TestResults tr where t.id=tr.Test and (t.Type="
            +((int)TestType.questionnaire).ToString()+" or t.Type="
            +((int)TestType.globalquestionnaire).ToString()+ ") "
         +" and tr.Student='"+this.Student+"'"   
            ,"q");
         this.dataView.Table = dataSet.Tables["q"];
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
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.btnView = new System.Windows.Forms.ToolBarButton();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
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
         this.menuItem1.Text = "Просмотр";
         this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
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
                                                                                    this.btnView});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(528, 37);
         this.toolBar1.TabIndex = 46;
         this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
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
                                                                               this.dataColumnHeader2,
                                                                               this.dataColumnHeader1});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(528, 355);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 47;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItem1_Click);
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "InternalName";
         this.dataColumnHeader2.Text = "Название";
         this.dataColumnHeader2.Width = 300;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "CompletionDate";
         this.dataColumnHeader1.Text = "Дата заполнения";
         this.dataColumnHeader1.Width = 120;
         // 
         // btnView
         // 
         this.btnView.ImageIndex = 5;
         this.btnView.Text = "Просмотр";
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
         // StudentQuestionnaire
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "StudentQuestionnaire";
         this.Size = new System.Drawing.Size(528, 392);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         // просмотр анкеты
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row  = (DataRowView) this.dataList.SelectedItems[0].Tag;
            Process p = new Process();
            p.StartInfo.FileName = DCEAccessLib.Settings.DCEWebAddr 
               + "/Statistics.aspx?studentId="+this.Student+"&testId="+row["Test"].ToString();
            p.StartInfo.UseShellExecute = true;
            p.Start();
         }
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
            this.RefreshData();
         if (e.Button == this.btnView)
            menuItem1_Click(null,null);
      }

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         this.RefreshData();
      }
	}

   public class StudentQuestionnaireNode : NodeControl
   {
      public string Student;
      public StudentQuestionnaireNode (NodeControl parent, string student)
         : base(parent)
      {
         this.Student = student;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new StudentQuestionnaire(Student);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Анкеты";
      }

      public override bool HaveChildNodes()
      {
         return false;
      }
   }


}
