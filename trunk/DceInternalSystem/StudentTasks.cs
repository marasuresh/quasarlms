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
	/// Задания студента
	/// </summary>
	public class StudentTasks : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader5;
      private DCEAccessLib.DataColumnHeader dataColumnHeader6;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private DCEAccessLib.DataColumnHeader CompleteCol;
      private System.Windows.Forms.TextBox SolutionView;

      public StudentTasksNode Node;
		public StudentTasks(StudentTasksNode node)
		{
         this.Node = node;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
         this.toolBar1.ImageList = ToolbarImages.Images.images;
         this.CompleteCol.OnParse += new DataColumnHeader.FieldParseHandler(OnCompleteParse);
         RefreshData();
		}

      public void RefreshData()
      {
         this.dataSet = DCEWebAccess.WebAccess.GetDataSet(
@"select dbo.GetStrContentAlt(tr.Name,'RU','EN') as TName, dbo.GetStrContentAlt(t.Name,'RU','EN') as Name,t.TaskTime, ts.Complete ,ts.Solution from Trainings tr, Tasks t, TaskSolutions ts where
tr.id =t.Training and ts.Task = t.id and ts.Student='"+this.Node.StudentId+"'","t");
         this.dataView.Table = this.dataSet.Tables["t"];
      }

      public void OnCompleteParse(string FieldName, DataRowView row, ref string text)
      {
         switch ((int)row["Complete"])
         {
            case 0:
               if (row["Solution"].GetType() == typeof(System.DBNull))
               {
                  text = "Отсутствует";
               }
               else
                  text = "Не проверено";
               break;
            case 1:
               text = "Правильно";
               break;
            case 2:
               text = "Неправильно";
               break;
         }
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
         this.panel1 = new System.Windows.Forms.Panel();
         this.SolutionView = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader5 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader6 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.CompleteCol = new DCEAccessLib.DataColumnHeader();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.panel1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.SolutionView,
                                                                             this.label2});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 316);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(672, 100);
         this.panel1.TabIndex = 43;
         // 
         // SolutionView
         // 
         this.SolutionView.BackColor = System.Drawing.Color.White;
         this.SolutionView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.SolutionView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.SolutionView.ForeColor = System.Drawing.SystemColors.WindowText;
         this.SolutionView.Location = new System.Drawing.Point(0, 20);
         this.SolutionView.Multiline = true;
         this.SolutionView.Name = "SolutionView";
         this.SolutionView.ReadOnly = true;
         this.SolutionView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.SolutionView.Size = new System.Drawing.Size(672, 80);
         this.SolutionView.TabIndex = 2;
         this.SolutionView.Text = "";
         this.SolutionView.TextChanged += new System.EventHandler(this.SolutionView_TextChanged);
         // 
         // label2
         // 
         this.label2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label2.Dock = System.Windows.Forms.DockStyle.Top;
         this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label2.ForeColor = System.Drawing.SystemColors.Info;
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(672, 20);
         this.label2.TabIndex = 155;
         this.label2.Text = "Решение";
         this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
         this.toolBar1.Size = new System.Drawing.Size(672, 37);
         this.toolBar1.TabIndex = 45;
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
                                                                               this.dataColumnHeader5,
                                                                               this.dataColumnHeader6,
                                                                               this.dataColumnHeader2,
                                                                               this.CompleteCol});
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.HideSelection = false;
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(672, 279);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 1;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.SelectedIndexChanged += new System.EventHandler(this.dataList_SelectedIndexChanged);
         // 
         // dataColumnHeader5
         // 
         this.dataColumnHeader5.FieldName = "TName";
         this.dataColumnHeader5.Text = "Тренинг";
         this.dataColumnHeader5.Width = 200;
         // 
         // dataColumnHeader6
         // 
         this.dataColumnHeader6.FieldName = "Name";
         this.dataColumnHeader6.Text = "Задание";
         this.dataColumnHeader6.Width = 200;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "TaskTime";
         this.dataColumnHeader2.Text = "Выполнить до";
         this.dataColumnHeader2.Width = 130;
         // 
         // CompleteCol
         // 
         this.CompleteCol.FieldName = "Complete";
         this.CompleteCol.Text = "Выполнено";
         this.CompleteCol.Width = 120;
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // StudentTasks
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1,
                                                                      this.panel1});
         this.Name = "StudentTasks";
         this.Size = new System.Drawing.Size(672, 416);
         this.panel1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         this.RefreshData();
      }

      private void dataList_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ///
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;
            this.SolutionView.Text = row["Solution"].ToString();
         }
      }

      private void SolutionView_TextChanged(object sender, System.EventArgs e)
      {
      
      }
	}

   public class StudentTasksNode : NodeControl
   {
      public string StudentId;
      public StudentTasksNode  (NodeControl parent, string studentid)
         : base(parent)
      {
         this.StudentId = studentid;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new StudentTasks(this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Задания";
      }

      public override bool HaveChildNodes()
      {
         return false;
      }
   }

}
