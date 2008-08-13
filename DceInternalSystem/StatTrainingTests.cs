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
	/// Список тестов тренига для статистики
	/// </summary>
   public class StatTrainingTests : System.Windows.Forms.UserControl
   {
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnSingle;
      private System.Windows.Forms.ToolBarButton btnAll;
      private System.Windows.Forms.ImageList imageList1;
      private System.ComponentModel.IContainer components;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.Panel panel1;
      private DCEAccessLib.DataList dataList;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label StudentCnt;
      private DCEAccessLib.DataColumnHeader TestNameCol;

      public StatTrainingTestsNode Node;
      private DCEAccessLib.DataColumnHeader PTotal;
      private DCEAccessLib.DataColumnHeader PTries;
      private DCEAccessLib.DataColumnHeader PPoints;
      private DCEAccessLib.DataColumnHeader PAvgPoints;
      public TestType Type;
      public StatTrainingTests(StatTrainingTestsNode node, TestType type)
      {
         this.Node = node;
         this.Type = type;

         InitializeComponent();

         if (Type == TestType.practice)
         {
            PTotal.Text ="Сдавших работу";
            this.dataList.Columns.Remove(PTries);
            this.dataList.Columns.Remove(PPoints);
            this.dataList.Columns.Remove(PAvgPoints);
         }

         this.TestNameCol.OnParse += new DataColumnHeader.FieldParseHandler(TestNameParse);
         RefreshData();
      }

      protected void TestNameParse(string FieldName, DataRowView row, ref string text)
      {
         if (row["Parent"].ToString() == this.CourseId)
            text = "Курсовой тест";
      }

      protected string CourseId="";
      public void RefreshData()
      {
         CourseId = DCEAccessLib.DCEWebAccess.WebAccess.GetString("select Course from Trainings where id='"+this.Node.TrainingId+"'");
         if (this.Type == TestType.test)
         {
            this.dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               @"select t.id, dbo.GetThemeName(Tests.Parent,1) as TestName, isnull(pt.total,0) as ptotal, isnull(pt.avgpoints,0) as pavg, Tests.Points, Tests.Parent,
[dbo].[CountTestTries]('"+this.Node.TrainingId+@"',t.id) as ptries
from Tests, dbo.TrainingTests('"+this.Node.TrainingId+"') as t left join [dbo].[CountStudentsPassedTest]('"+this.Node.TrainingId
               +"') pt on (t.id=pt.id) where Tests.Type="+((int)DCEAccessLib.TestType.test).ToString()+" and Tests.id=t.id","stat");
         }
         else
         {
            // practices
            this.dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               @"select t.id, dbo.GetThemeName(Tests.Parent,1) as TestName, isnull(pt.total,0) as ptotal, Tests.Parent
from Tests, dbo.TrainingTests('"+this.Node.TrainingId+"') as t left join [dbo].[CountStudentsPassedTest]('"+this.Node.TrainingId
               +"') pt on (t.id=pt.id) where Tests.Type="+((int)DCEAccessLib.TestType.practice).ToString()+" and Tests.id=t.id","stat");
         }
         this.dataView.Table = this.dataSet.Tables["stat"];
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select count(*) from dbo.AllDistinctTrainingStudents('"+this.Node.TrainingId+"')" ,"q");
         this.StudentCnt.Text = ds.Tables["q"].Rows[0][0].ToString();
      }

      private class QStats
      {public int timesum;
         public int answercount;
         public int rightcount;
         public int min;
         public int max;

         public QStats(int time, string answer)
         {
            min = time;
            max = time;
            timesum = time;
            answercount = 1;
            if (answer.IndexOf("result=\"true\"")>0)
               rightcount++;
         }

         public void addTime(int time, string answer)
         {
            if (time<min) min = time;
            if (time>max) max = time;
            timesum+= time;
            answercount++;
            if (answer.IndexOf("result=\"true\"")>0)
               rightcount++;
         }
      }

      private string GeneratePracticeReport(string testid)
      {
         string xml="";
         // описание практики
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            "select t.*, dbo.GetThemeName(t.Parent,1) as ThemeName from Tests t where t.id='"+testid+"'","TestDetails");
         xml += ds.GetXml();

         // статистика по каждому студенту сдававшему практику 
         xml += DCEAccessLib.DAL.Student.GetPracticeResults(
				new Guid(testid),
				new Guid(this.Node.TrainingId)).GetXml();

         return xml;
      }

      private string GenerateTestReport(string testid)
      {
         string xml="";
         // описание теста
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
"select t.*, dbo.GetThemeName(t.Parent,1) as ThemeName from Tests t where t.id='"+testid+"'","TestDetails");
         xml += ds.GetXml();

         // инфа по вопросам теста
         ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
@"select tr.Student, a.Question, a.AnswerTime , a.Answer
from TestResults tr, TestAnswers a 
where a.TestResults = tr.id and tr.Test='"+testid+@"' 
and tr.Student in (select distinct id from dbo.AllTrainingStudents('"+this.Node.TrainingId+@"')) 
order by tr.Student, a.AnswerTime asc","tQ");

         System.Collections.Hashtable hash = new System.Collections.Hashtable();
         string currStudentId ="";
         int timecount = 0;

         foreach (DataRow row in ds.Tables["tQ"].Rows)
         {
            if (row["Student"].ToString() != currStudentId)
            {
               currStudentId = row["Student"].ToString();
               timecount = 0;
            }
            if (hash.Contains(row["Question"].ToString()))
            {
               ((QStats)hash[row["Question"].ToString()]).addTime((int)row["AnswerTime"] - timecount, row["Answer"].ToString());
            }
            else
            {
               hash[row["Question"].ToString()] = new QStats((int)row["AnswerTime"] - timecount, row["Answer"].ToString());
            }
            timecount = (int)row["AnswerTime"];
         }

         // контент вопросов
         ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            "select id, dbo.GetStrContentAlt(Content,'RU','EN') as QContent, Points from TestQuestions where Test='"+testid+"'","Questions" );

         xml += "<Questions>";
         foreach (DataRow row in ds.Tables["Questions"].Rows)
         {
               xml += "\n<Question><Content>"+row["QContent"].ToString()+"</Content>"
                  +"<Points>"+row["Points"].ToString()+"</Points>";

               if (hash.Contains(row["id"].ToString()))
               {
                  QStats stats = (QStats)hash[row["id"].ToString()];
                  xml += "<RightCount>"+stats.rightcount.ToString()+"</RightCount>";
                  xml += "<WrongCount>"+(stats.answercount-stats.rightcount).ToString()+"</WrongCount>";
                  xml += "<Min>"+stats.min.ToString()+"</Min>";
                  xml += "<Max>"+stats.max.ToString()+"</Max>";
                  xml += "<Average>"+(stats.timesum/stats.answercount).ToString()+"</Average>";
               }
               xml += "</Question>";
         }
         xml += "</Questions>";
         // статистика по каждому студенту сдававшему тест
         xml += DCEAccessLib.DAL.Student.GetTestResults(
				new Guid(testid),
				new Guid(this.Node.TrainingId)).GetXml();

         return xml;
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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatTrainingTests));
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnSingle = new System.Windows.Forms.ToolBarButton();
         this.btnAll = new System.Windows.Forms.ToolBarButton();
         this.imageList1 = new System.Windows.Forms.ImageList(this.components);
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.panel1 = new System.Windows.Forms.Panel();
         this.StudentCnt = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.dataList = new DCEAccessLib.DataList();
         this.TestNameCol = new DCEAccessLib.DataColumnHeader();
         this.PTotal = new DCEAccessLib.DataColumnHeader();
         this.PTries = new DCEAccessLib.DataColumnHeader();
         this.PPoints = new DCEAccessLib.DataColumnHeader();
         this.PAvgPoints = new DCEAccessLib.DataColumnHeader();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolBar1
         // 
         this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
         this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                    this.btnRefresh,
                                                                                    this.btnSingle,
                                                                                    this.btnAll});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.ImageList = this.imageList1;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(556, 39);
         this.toolBar1.TabIndex = 25;
         this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
         // 
         // btnRefresh
         // 
         this.btnRefresh.ImageIndex = 0;
         this.btnRefresh.Text = "Обновить";
         // 
         // btnSingle
         // 
         this.btnSingle.ImageIndex = 1;
         this.btnSingle.Text = "Стат. по выбр.";
         // 
         // btnAll
         // 
         this.btnAll.ImageIndex = 2;
         this.btnAll.Text = "Стат. по всем";
         // 
         // imageList1
         // 
         this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
         this.imageList1.ImageSize = new System.Drawing.Size(18, 18);
         this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
         this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
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
         this.menuItem1.Text = "Статистика по тесту";
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
         this.menuItem3.Text = "Статистика по всем тестам";
         this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // panel1
         // 
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.StudentCnt,
                                                                             this.label1});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 372);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(556, 32);
         this.panel1.TabIndex = 27;
         // 
         // StudentCnt
         // 
         this.StudentCnt.Location = new System.Drawing.Point(156, 8);
         this.StudentCnt.Name = "StudentCnt";
         this.StudentCnt.Size = new System.Drawing.Size(96, 16);
         this.StudentCnt.TabIndex = 1;
         this.StudentCnt.Text = "label2";
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(8, 8);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(144, 20);
         this.label1.TabIndex = 0;
         this.label1.Text = "Всего студентов тренинга:";
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = false;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.TestNameCol,
                                                                               this.PTotal,
                                                                               this.PTries,
                                                                               this.PPoints,
                                                                               this.PAvgPoints});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 39);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(556, 333);
         this.dataList.TabIndex = 28;
         this.dataList.View = System.Windows.Forms.View.Details;
         // 
         // TestNameCol
         // 
         this.TestNameCol.FieldName = "TestName";
         this.TestNameCol.Text = "Тема";
         this.TestNameCol.Width = 200;
         // 
         // PTotal
         // 
         this.PTotal.FieldName = "ptotal";
         this.PTotal.Text = "Сдавших тест";
         this.PTotal.Width = 120;
         // 
         // PTries
         // 
         this.PTries.FieldName = "ptries";
         this.PTries.Text = "Кол-во попыток";
         this.PTries.Width = 120;
         // 
         // PPoints
         // 
         this.PPoints.FieldName = "Points";
         this.PPoints.Text = "Проходной балл";
         this.PPoints.Width = 120;
         // 
         // PAvgPoints
         // 
         this.PAvgPoints.FieldName = "pavg";
         this.PAvgPoints.Text = "Средний балл";
         this.PAvgPoints.Width = 120;
         // 
         // StatTrainingTests
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.panel1,
                                                                      this.toolBar1});
         this.Name = "StatTrainingTests";
         this.Size = new System.Drawing.Size(556, 404);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.panel1.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         /// single test stats
         /// 
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;
            if (this.Type == TestType.test)
            DCEAccessLib.XmlReports.ProduceReport(StatsTraining.GenerateTrainingInfo(this.Node.TrainingId)+ 
"<TestReport>" + GenerateTestReport(row["id"].ToString()) + "</TestReport>","DCEInternalSystem.Res.TrainingTestStats.xsl");
            else
               DCEAccessLib.XmlReports.ProduceReport(StatsTraining.GenerateTrainingInfo(this.Node.TrainingId)+ 
"<TestReport>" + GeneratePracticeReport(row["id"].ToString()) + "</TestReport>","DCEInternalSystem.Res.TrainingTestStats.xsl");         
         }
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
         {
            this.Refresh();
         }
         if (e.Button == this.btnSingle)
         {
            menuItem1_Click(null,null);
         }
         if (e.Button == this.btnAll)
         {
            menuItem3_Click(null,null);
         }
      }

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         string xml=StatsTraining.GenerateTrainingInfo(this.Node.TrainingId);
         foreach (DataRowView row in this.dataView)
         {
            if (this.Type == TestType.test)
               xml+= "<TestReport>" + GenerateTestReport(row["id"].ToString())+"</TestReport>";
            else
               xml+= "<TestReport>" + GeneratePracticeReport(row["id"].ToString())+"</TestReport>";
         }
         DCEAccessLib.XmlReports.ProduceReport(xml,"DCEInternalSystem.Res.TrainingTestStats.xsl");
      }
	}

   public class StatTrainingTestsNode : NodeControl
   {
      public string TrainingId="";
      public TestType Type;
      public StatTrainingTestsNode (NodeControl parent, string trainingid,TestType type)
         : base (parent)
      {
         this.Type = type;
         this.TrainingId = trainingid;
      }
      public override String GetCaption()
      {
         if (this.TrainingId!="")
         {
            string trainingname =DCEAccessLib.DCEWebAccess.WebAccess.GetString("select dbo.GetStrContentAlt(Name,'RU','EN') from dbo.Trainings t where id='"+this.TrainingId+"'");
            if (Type == TestType.test)
               return "Статистика тестов тренинга: "+trainingname;
            else
               return "Статистика практических работ тренинга: "+trainingname;
         }
         return "";
      }
      public override bool CanClose()
      {
         return true;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.StatTrainingTests(this,Type);
         }
         if (needRefresh)
         {
            ((StatTrainingTests)this.fControl).RefreshData();
            needRefresh = false;
         }
         return this.fControl;
      }

   }
}
