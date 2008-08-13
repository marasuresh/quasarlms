using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;
using System.Xml;
using System.Diagnostics;

namespace DCEInternalSystem
{
	/// <summary>
	/// Статистика тренинга
	/// </summary>
   public class StatsTraining : System.Windows.Forms.UserControl
   {
      private System.ComponentModel.IContainer components;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.ImageList imageList1;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnSingle;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader StudentCol;
      private DCEAccessLib.DataColumnHeader dataColumnHeader8;
      private DCEAccessLib.DataColumnHeader dataColumnHeader9;
      private DCEAccessLib.DataColumnHeader dataColumnHeader10;
      private DCEAccessLib.DataColumnHeader SolutionsCol;
      private DCEAccessLib.DataColumnHeader CourseCompleteCol;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.ToolBarButton btnAll;

      StatsTrainingNode Node;
      System.Collections.ArrayList files;

      public StatsTraining(StatsTrainingNode node)
      {
         Node = node;
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
         files = new  System.Collections.ArrayList();

         this.StudentCol.OnParse += new DCEAccessLib.DataColumnHeader.FieldParseHandler(StudentNameParse);
         this.CourseCompleteCol.OnParse += new DCEAccessLib.DataColumnHeader.FieldParseHandler(StudentNameParse);
         this.SolutionsCol.OnParse += new DCEAccessLib.DataColumnHeader.FieldParseHandler(StudentNameParse);
         RefreshData();
      }

      void StudentNameParse(string FieldName, DataRowView row, ref string text)
      {
         if (FieldName == "CourseComplete")
         {
            if ((int)row["CourseComplete"] >0)
               text = "Сдан";
            else
               text = "Не сдан";
         }
         else
            if (FieldName == "Solutions")
         {
            text = row["Solutions"].ToString() +" из " + row["TotalSolutions"].ToString();
         }
         else
            text = row["LastName"].ToString() + " " + row["FirstName"].ToString() + " " + row["Patronymic"].ToString();
      }

      public void RefreshData()
      {
         string sql = 
            @" select s.id ,st.LastLogin, st.FirstName, st.LastName, st.Patronymic, dbo.CountCompleteTests('"+this.Node.TrainingId+@"',s.id) as CntTests,
dbo.CountCompletePractice('"+this.Node.TrainingId+@"',s.id) as CntPractice, 
dbo.CountCompleteSolutions('"+this.Node.TrainingId+@"',s.id) as Solutions,
dbo.CountStudentSolutions('"+this.Node.TrainingId+@"',s.id) as TotalSolutions,
dbo.IsCourseTestComplete('"+this.Node.TrainingId+@"',s.id) as CourseComplete
from dbo.AllDistinctTrainingStudents('"+this.Node.TrainingId+"') s, Students st where st.id = s.id";
         this.dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(sql,"t");
         this.dataView.Table = this.dataSet.Tables["t"];

         //         
         //         select = @"select t.id, (ISNULL(tr.Skipped,0) | ISNULL(tr.Complete, 0)) as AllowNext, '"
         //            +test+"' as text from dbo.Tests t left join dbo.TestResults tr"
         //            +"on (tr.Test=t.id and tr.Student='"+studentId+"') where Type="
         //            +((int)DCEAccessLib.TestType.test).ToString()+" and t.Parent='"
         //            + parentId + "'";
      }
      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose( bool disposing )
      {
         try
         {
            System.IO.File.Delete("output.html");
         }
         catch
         {

         }

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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatsTraining));
         this.dataSet = new System.Data.DataSet();
         this.dataView = new System.Data.DataView();
         this.imageList1 = new System.Windows.Forms.ImageList(this.components);
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnSingle = new System.Windows.Forms.ToolBarButton();
         this.btnAll = new System.Windows.Forms.ToolBarButton();
         this.dataList = new DCEAccessLib.DataList();
         this.StudentCol = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader8 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader9 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader10 = new DCEAccessLib.DataColumnHeader();
         this.SolutionsCol = new DCEAccessLib.DataColumnHeader();
         this.CourseCompleteCol = new DCEAccessLib.DataColumnHeader();
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.SuspendLayout();
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // imageList1
         // 
         this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
         this.imageList1.ImageSize = new System.Drawing.Size(18, 18);
         this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
         this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
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
         this.toolBar1.Size = new System.Drawing.Size(664, 39);
         this.toolBar1.TabIndex = 24;
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
         this.btnSingle.Text = "Стат. по студенту";
         // 
         // btnAll
         // 
         this.btnAll.ImageIndex = 2;
         this.btnAll.Text = "Стат. по всем";
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.StudentCol,
                                                                               this.dataColumnHeader8,
                                                                               this.dataColumnHeader9,
                                                                               this.dataColumnHeader10,
                                                                               this.SolutionsCol,
                                                                               this.CourseCompleteCol});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 39);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(664, 325);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 25;
         this.dataList.View = System.Windows.Forms.View.Details;
         // 
         // StudentCol
         // 
         this.StudentCol.FieldName = "FirstName";
         this.StudentCol.Text = "Студент";
         this.StudentCol.Width = 150;
         // 
         // dataColumnHeader8
         // 
         this.dataColumnHeader8.FieldName = "LastLogin";
         this.dataColumnHeader8.Text = "Последний вход";
         this.dataColumnHeader8.Width = 130;
         // 
         // dataColumnHeader9
         // 
         this.dataColumnHeader9.FieldName = "CntTests";
         this.dataColumnHeader9.Text = "Сдано тестов";
         this.dataColumnHeader9.Width = 100;
         // 
         // dataColumnHeader10
         // 
         this.dataColumnHeader10.FieldName = "CntPractice";
         this.dataColumnHeader10.Text = "Практ. работы";
         this.dataColumnHeader10.Width = 120;
         // 
         // SolutionsCol
         // 
         this.SolutionsCol.FieldName = "Solutions";
         this.SolutionsCol.Text = "Вып. Задания";
         this.SolutionsCol.Width = 100;
         // 
         // CourseCompleteCol
         // 
         this.CourseCompleteCol.FieldName = "CourseComplete";
         this.CourseCompleteCol.Text = "Курсовой тест";
         this.CourseCompleteCol.Width = 150;
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
         this.menuItem1.Text = "Статистика по выбранному студенту";
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
         this.menuItem3.Text = "Статистика по всем студентам тренинга";
         this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
         // 
         // StatsTraining
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "StatsTraining";
         this.Size = new System.Drawing.Size(664, 364);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         /// генерим статистику для одного студента
         /// 
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView) this.dataList.SelectedItems[0].Tag;

            DCEAccessLib.XmlReports.ProduceReport(GenerateTrainingInfo(this.Node.TrainingId) + 
               "<StudentReport>"
                  + GenerateStudentReport(row["id"].ToString()) 
               + "</StudentReport>"
              ,"DCEInternalSystem.Res.TrainingStudentsStats.xsl", "output.html");
         }
      }

      private string GenerateStudentReport(string studentid)
      {
         string xml="";
         // инфа по студенту
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select * from Students where id='"+studentid+"'","Student");
         xml += ds.GetXml();

         // тесты и пр. работы
         ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
@"select tr.*, dbo.TestResultPoints(tr.id) as Points, dbo.GetThemeName(t.Parent,1) as TestName, t.Type , t.Parent from TestResults tr, Tests t 
where t.id = tr.Test and tr.Student = '"+studentid+ "' and tr.Test in (Select id from dbo.TrainingTests('"+this.Node.TrainingId+"')) order by tr.TryStart","Tests");
         xml += ds.GetXml();

         // задачи
         ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
@"select dbo.GetStrContentAlt(t.Name,'RU','EN') as TaskName, t.TaskTime , ts.Complete, ts.SDate from
Tasks t, TaskSolutions ts where t.Training = '"+this.Node.TrainingId+@"'
and ts.Task = t.id and ts.Student = '"+studentid+"' order by t.TaskTime","Tasks");
         xml += ds.GetXml();

         xml += "<ForumTopicsStarted>"+
            DCEWebAccess.WebAccess.GetString("select count(*) from ForumTopics where Training='"+this.Node.TrainingId+"' and Author='"+studentid+"'")
            +"</ForumTopicsStarted>";
         xml += "<ForumReplies>"+
            DCEWebAccess.WebAccess.GetString("select count(*) from ForumReplies r , ForumTopics t where t.Training='"+this.Node.TrainingId+"' and r.Topic = t.id and r.Author='"+studentid+"'")
            +"</ForumReplies>";
         return xml;
      }

      public static string GenerateTrainingInfo(string TrainingId)
      {
         // инфа по тренингу
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select Course,Code, StartDate,EndDate, dbo.GetStrContentAlt(Name,'RU','EN') as TName from Trainings where id='"+TrainingId+"'","Training");
         return ds.GetXml();
      }

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         /// генерим статистику для всех студентов
         /// 
         string xml = GenerateTrainingInfo(this.Node.TrainingId);
         foreach (DataRowView row in this.dataView )
         {
            xml += "<StudentReport>" + GenerateStudentReport(row["id"].ToString()) + "</StudentReport>";
         }
         DCEAccessLib.XmlReports.ProduceReport(xml,"DCEInternalSystem.Res.TrainingStudentsStats.xsl", "output.html");
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnAll)
         {
            menuItem3_Click(null,null);
         }

         if (e.Button == this.btnSingle)
         {
            menuItem1_Click(null,null);
         }

         if (e.Button == this.btnRefresh)
         {  
            this.RefreshData();
         }
      }

      public static string XmlTransform(string xml, string xsl)
      {
         System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
         doc.LoadXml(xml);

         System.Xml.Xsl.XslTransform tr = new System.Xml.Xsl.XslTransform();
         System.IO.StringWriter writer = new System.IO.StringWriter();
         
         XmlDocument xsldoc = new XmlDocument();
         xsldoc.LoadXml(xsl);
         tr.Load(xsldoc);
         tr.Transform(doc,null,writer);

         return writer.ToString();
      }
	}

   public class StatsTrainingNode : NodeControl
   {
      public string TrainingId="";

      public StatsTrainingNode (NodeControl parent, string trainingId)
         : base(parent)
      {
         this.TrainingId = trainingId;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.StatsTraining(this);
         }
         if (needRefresh)
         {
            ((StatsTraining)this.fControl).RefreshData();
            needRefresh = false;
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         if (this.TrainingId!="")
            return "Статистика успеваемости студентов тренинга: "+DCEAccessLib.DCEWebAccess.WebAccess.GetString("select dbo.GetStrContentAlt(Name,'RU','EN') from dbo.Trainings t where id='"+this.TrainingId+"'");
         return "";
      }

      public override bool CanClose()
      {
         return true;
      }
   }
}
