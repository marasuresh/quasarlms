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
	/// Список заполненных общих анкет
	/// </summary>
	public class GlobalQuestionnaire : System.Windows.Forms.UserControl
	{
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnView;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.MenuItem menuItem6;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.MenuItem menuItem7;
      private System.Windows.Forms.MenuItem menuItem8;
      private System.Windows.Forms.ToolBarButton btnViewResp;

      GlobalQuestionnaireNode Node;
		public GlobalQuestionnaire(GlobalQuestionnaireNode node)
		{
         Node = node;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
         this.toolBar1.ImageList = ToolbarImages.Images.images;

         RefreshData();
		}

      public void RefreshData()
      {
        this.dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
@"select t.id, t.InternalName, dbo.CountTestResults(t.id) as ResCount from Tests t 
 where t.Type="+((int)TestType.globalquestionnaire).ToString() ,"q");
         this.dataView.Table = dataSet.Tables["q"];
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
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataView = new System.Data.DataView();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnView = new System.Windows.Forms.ToolBarButton();
         this.btnViewResp = new System.Windows.Forms.ToolBarButton();
         this.dataSet = new System.Data.DataSet();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItem7 = new System.Windows.Forms.MenuItem();
         this.menuItem8 = new System.Windows.Forms.MenuItem();
         this.menuItem6 = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
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
         this.dataList.HideSelection = false;
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(660, 291);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 49;
         this.dataList.View = System.Windows.Forms.View.Details;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "InternalName";
         this.dataColumnHeader2.Text = "Название";
         this.dataColumnHeader2.Width = 300;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "ResCount";
         this.dataColumnHeader1.Text = "Кол-во заполнений";
         this.dataColumnHeader1.Width = 120;
         // 
         // toolBar1
         // 
         this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
         this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                    this.btnRefresh,
                                                                                    this.btnView,
                                                                                    this.btnViewResp});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(660, 37);
         this.toolBar1.TabIndex = 48;
         this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
         // 
         // btnRefresh
         // 
         this.btnRefresh.ImageIndex = 0;
         this.btnRefresh.Text = "Обновить";
         // 
         // btnView
         // 
         this.btnView.ImageIndex = 5;
         this.btnView.Text = "Просм. по отв.";
         // 
         // btnViewResp
         // 
         this.btnViewResp.ImageIndex = 5;
         this.btnViewResp.Text = "Просм. по респ.";
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // menuItem1
         // 
         this.menuItem1.Index = -1;
         this.menuItem1.Text = "Просмотр";
         // 
         // contextMenu1
         // 
         this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItem2,
                                                                                     this.menuItem6,
                                                                                     this.menuItem4});
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 0;
         this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                  this.menuItem7,
                                                                                  this.menuItem8});
         this.menuItem2.Text = "Просмотр";
         // 
         // menuItem7
         // 
         this.menuItem7.Index = 0;
         this.menuItem7.Text = "Группировать по вопросам";
         this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
         // 
         // menuItem8
         // 
         this.menuItem8.Index = 1;
         this.menuItem8.Text = "Группировать по респондентам";
         this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
         // 
         // menuItem6
         // 
         this.menuItem6.Index = 1;
         this.menuItem6.Text = "-";
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 2;
         this.menuItem4.Text = "Обновить";
         this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
         // 
         // GlobalQuestionnaire
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "GlobalQuestionnaire";
         this.Size = new System.Drawing.Size(660, 328);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
            this.RefreshData();
         if (e.Button == this.btnView)
            this.ProduceReport(true);
         if (e.Button == this.btnViewResp)
            this.ProduceReport(false);
      }

      private void ProduceReport(bool groupByQuestions)
      {
         // просмотр анкеты
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row  = (DataRowView) this.dataList.SelectedItems[0].Tag;
            string xml = "<GroupByQuestions>"+groupByQuestions.ToString()+"</GroupByQuestions>";

            // retrieving questionnaire info
            DataSet ds = DCEWebAccess.GetdataSet(
               @"select t.InternalName, dbo.CountTestResults(t.id) as ResCount from Tests t 
 where t.id = '"+row["id"].ToString()+"'", "Questionnaire");
            xml += ds.GetXml();
            // retrieving questions
            ds = DCEWebAccess.GetdataSet("select id, dbo.GetContentAlt(Content,'RU','EN') as QContent from TestQuestions where Test='"+ row["id"].ToString()+"'", "Questions");

            xml += ds.GetXml();
            // retrieving answers
            ds = DCEWebAccess.GetdataSet(
"select ta.TestResults, ta.Question, ta.Answer from TestAnswers ta, TestQuestions q where q.id=ta.Question and q.Test='"
   + row["id"].ToString()+"' order by ta.TestResults,ta.Question","Answers");
            string pxml = ds.GetXml();
            pxml = pxml.Replace("&lt;","<");
            pxml = pxml.Replace("&gt;",">");
            xml += pxml;

            DCEAccessLib.XmlReports.ProduceReport(xml, "DCEInternalSystem.Res.QuestionnaireStats.xsl", "output.html");
         }
      }

      private void menuItem4_Click(object sender, System.EventArgs e)
      {
         this.RefreshData();
      }

      private void menuItem7_Click(object sender, System.EventArgs e)
      {
         ///
         ProduceReport(true);
      }

      private void menuItem8_Click(object sender, System.EventArgs e)
      {
         ///
         ProduceReport(false);
      }

      private void menuItem5_Click(object sender, System.EventArgs e)
      {
         ///
      }
	}


   public class GlobalQuestionnaireNode : NodeControl
   {
      public GlobalQuestionnaireNode  (NodeControl parent)
         : base(parent)
      {
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new GlobalQuestionnaire (this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Общие анкеты";
      }

      public override bool HaveChildNodes()
      {
         return false;
      }
   }

}
