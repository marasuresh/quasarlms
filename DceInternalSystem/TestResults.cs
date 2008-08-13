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
	/// Результаты по тестам тренинга
	/// </summary>
	public class TestResults : System.Windows.Forms.UserControl
	{
      private System.ComponentModel.IContainer components;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.ToolBar toolBar1;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;

      private DCEAccessLib.DataColumnHeader AnswerTimeCol;
      private System.Windows.Forms.ImageList imageList1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private DCEAccessLib.DataColumnHeader AnswerCol;
      private TestResultsNode Node;
		public TestResults(TestResultsNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.Node=node;
         this.AnswerTimeCol.OnParse += new DCEAccessLib.DataColumnHeader.FieldParseHandler(OnAnswerTimeParse);
         this.AnswerCol.OnParse += new DCEAccessLib.DataColumnHeader.FieldParseHandler(OnAnswerParse);
         RefreshData();
		}

      private int answerTime;
      private void OnAnswerTimeParse(string FieldName, DataRowView row, ref string text)
      {
         text = (((int)row["AnswerTime"])).ToString();
         answerTime = (int)row["AnswerTime"];
      }

      private void OnAnswerParse(string FieldName, DataRowView row, ref string text)
      {
         System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
         doc.LoadXml(text);
         System.Xml.XmlAttribute attr = doc.DocumentElement.Attributes["result"];
         bool right = false;
         if (attr != null)
         {
            right = attr.Value == "true";
         }
         if (right)
            text ="Правильно";
         else
            text ="Неправильно";
      }


      public void RefreshData()
      {
         answerTime = 0;
         this.dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
"Select ta.*, dbo.GetContentAlt(t.Content,'RU','EN') as QContent from TestAnswers ta, TestQuestions t , TestResults tr where"
+" ta.Question = t.id and ta.TestResults = tr.id and tr.Test=t.Test and tr.id='"+this.Node.ResultId+"' order by ta.AnswerTime asc" ,"tr");
         try
         {
            this.dataList.SuspendListChange = true;
            this.dataView.Table = this.dataSet.Tables["tr"];
         }
         finally
         {
            this.dataList.SuspendListChange = false;
            this.dataList.UpdateList();
         }
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
         this.components = new System.ComponentModel.Container();
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TestResults));
         this.dataSet = new System.Data.DataSet();
         this.dataView = new System.Data.DataView();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.imageList1 = new System.Windows.Forms.ImageList(this.components);
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.AnswerCol = new DCEAccessLib.DataColumnHeader();
         this.AnswerTimeCol = new DCEAccessLib.DataColumnHeader();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.SuspendLayout();
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
                                                                                    this.btnRefresh});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.ImageList = this.imageList1;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(644, 25);
         this.toolBar1.TabIndex = 0;
         this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
         // 
         // btnRefresh
         // 
         this.btnRefresh.ImageIndex = 0;
         // 
         // imageList1
         // 
         this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
         this.imageList1.ImageSize = new System.Drawing.Size(18, 18);
         this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
         this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.dataColumnHeader1,
                                                                               this.AnswerCol,
                                                                               this.AnswerTimeCol});
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 25);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(644, 427);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 13;
         this.dataList.View = System.Windows.Forms.View.Details;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "QContent";
         this.dataColumnHeader1.Text = "Вопрос";
         this.dataColumnHeader1.Width = 250;
         // 
         // AnswerCol
         // 
         this.AnswerCol.FieldName = "Answer";
         this.AnswerCol.Text = "Результат";
         this.AnswerCol.Width = 150;
         // 
         // AnswerTimeCol
         // 
         this.AnswerTimeCol.FieldName = "AnswerTime";
         this.AnswerTimeCol.Text = "Время ответа (сек.)";
         this.AnswerTimeCol.Width = 140;
         // 
         // TestResults
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "TestResults";
         this.Size = new System.Drawing.Size(644, 452);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
         {
            this.RefreshData();
         }
      }

	}



   public class TestResultsNode: NodeControl
   {
      public string ResultId;
      public string TestName;
      public string StudentName;

      public TestResultsNode(NodeControl parent, string resultId, string testName, string studentName)
         : base(parent)
      {
         this.TestName = testName;
         this.StudentName = studentName;
         this.ResultId = resultId;
      }

      public override string GetCaption()
      {
         return "Студент: " + StudentName + " , тест: " + TestName;
      }

      public override bool CanClose()
      {
         return true;
      }

      public override UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new TestResults (this);
         }
         return fControl;
      }
   }

}
