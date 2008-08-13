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
	/// Список тренгов для показа статистики
	/// </summary>
	public class StatsTrainingList : System.Windows.Forms.UserControl
	{
      private System.ComponentModel.IContainer components;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.ImageList imageList1;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnSingle;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader3;
      private DCEAccessLib.DataColumnHeader dataColumnHeader4;
      private DCEAccessLib.DataColumnHeader dataColumnHeader5;
      private System.Windows.Forms.ToolBarButton btnTests;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.ToolBarButton btnPractices;
      private System.Windows.Forms.ToolBarButton btnForum;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem4;

      StatsTrainingListNode Node;
		public StatsTrainingList(StatsTrainingListNode node)
		{

         Node = node;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         RefreshData();
		}

      public void RefreshData()
      {
         if (DCEUser.CurrentUser.Trainings == DCEUser.Access.No 
            && DCEUser.CurrentUser.Tests == DCEUser.Access.No)
            dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               @"select dbo.GetStrContentAlt(t.Name,'RU','EN') as RName, 
dbo.GetStrContentAlt(c.Name, 'RU','EN') as RCourse , t.* from Trainings t, Courses c
where t.Course = c.id and t.id in (SELECT tr.id from Trainings tr, GroupMembers gm where gm.id='"+DCEUser.CurrentUser.id+"' and (gm.MGroup = tr.Instructors or gm.MGroup = tr.Curators))","Trainings");
         else
            dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               @"select dbo.GetStrContentAlt(t.Name,'RU','EN') as RName, 
dbo.GetStrContentAlt(c.Name, 'RU','EN') as RCourse , t.* from Trainings t, Courses c
where t.Course = c.id","Trainings");
         dataView.Table = dataSet.Tables["Trainings"];

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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatsTrainingList));
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.imageList1 = new System.Windows.Forms.ImageList(this.components);
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnSingle = new System.Windows.Forms.ToolBarButton();
         this.btnTests = new System.Windows.Forms.ToolBarButton();
         this.btnPractices = new System.Windows.Forms.ToolBarButton();
         this.btnForum = new System.Windows.Forms.ToolBarButton();
         this.dataSet = new System.Data.DataSet();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader5 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader3 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader4 = new DCEAccessLib.DataColumnHeader();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // contextMenu1
         // 
         this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItem1,
                                                                                     this.menuItem2,
                                                                                     this.menuItem3,
                                                                                     this.menuItem4});
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 0;
         this.menuItem1.Text = "Статистика по студентам тренинга";
         this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 1;
         this.menuItem2.Text = "Статистика по тестам тренинга";
         this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
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
                                                                                    this.btnTests,
                                                                                    this.btnPractices,
                                                                                    this.btnForum});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.ImageList = this.imageList1;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(676, 39);
         this.toolBar1.TabIndex = 22;
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
         this.btnSingle.Text = "Стат. студентов";
         // 
         // btnTests
         // 
         this.btnTests.ImageIndex = 1;
         this.btnTests.Text = "Тесты";
         // 
         // btnPractices
         // 
         this.btnPractices.ImageIndex = 1;
         this.btnPractices.Text = "Практ. работы";
         // 
         // btnForum
         // 
         this.btnForum.ImageIndex = 2;
         this.btnForum.Text = "Форум";
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.dataColumnHeader1,
                                                                               this.dataColumnHeader2,
                                                                               this.dataColumnHeader5,
                                                                               this.dataColumnHeader3,
                                                                               this.dataColumnHeader4});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 39);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(676, 549);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 23;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItem1_Click);
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "Code";
         this.dataColumnHeader1.Text = "Код";
         this.dataColumnHeader1.Width = 100;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "RName";
         this.dataColumnHeader2.Text = "Название";
         this.dataColumnHeader2.Width = 200;
         // 
         // dataColumnHeader5
         // 
         this.dataColumnHeader5.FieldName = "RCourse";
         this.dataColumnHeader5.Text = "Курс";
         this.dataColumnHeader5.Width = 200;
         // 
         // dataColumnHeader3
         // 
         this.dataColumnHeader3.FieldName = "StartDate";
         this.dataColumnHeader3.Text = "Дата начала";
         this.dataColumnHeader3.Width = 100;
         // 
         // dataColumnHeader4
         // 
         this.dataColumnHeader4.FieldName = "EndDate";
         this.dataColumnHeader4.Text = "Дата окончания";
         this.dataColumnHeader4.Width = 100;
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 2;
         this.menuItem3.Text = "Статистика по практическим работам тренинга";
         this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 3;
         this.menuItem4.Text = "Статистика по форуму тренинга";
         this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
         // 
         // StatsTrainingList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "StatsTrainingList";
         this.Size = new System.Drawing.Size(676, 588);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
         {
            this.RefreshData();
         }
         if (e.Button == this.btnSingle)
         {
            menuItem1_Click(null,null);
         }
         if (e.Button == this.btnTests)
         {
            ShowTrainingTestStats(TestType.test);
         }
         if (e.Button == this.btnPractices)
         {
            ShowTrainingTestStats(TestType.practice);
         }
         if (e.Button == this.btnForum)
         {
            menuItem4_Click(null,null);
         }
      }

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         /// выдаем статистику тренинга по студентам
         ///
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView) this.dataList.SelectedItems[0].Tag;
            foreach (NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(StatsTrainingNode))
               {
                  if ( ((StatsTrainingNode) node).TrainingId == row["id"].ToString())
                  {
                     node.Select();
                     return;
                  }
               }
            }
            StatsTrainingNode newnode = new StatsTrainingNode(this.Node,row["id"].ToString());
            newnode.Select();
         }
      }

      private void ShowTrainingTestStats(TestType type)
      {
         /// выдаем статистику тренинга по тестам
         ///
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView) this.dataList.SelectedItems[0].Tag;
            foreach (NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(StatTrainingTestsNode))
               {
                  if ( ((StatTrainingTestsNode) node).TrainingId == row["id"].ToString() && ((StatTrainingTestsNode) node).Type == type)
                  {
                     node.Select();
                     return;
                  }
               }
            }
            StatTrainingTestsNode newnode = new StatTrainingTestsNode(this.Node,row["id"].ToString(),type);
            newnode.Select();
         }
      }

      private void menuItem2_Click(object sender, System.EventArgs e)
      {
         ShowTrainingTestStats(TestType.test);
      }

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         // practices stats
         ShowTrainingTestStats(TestType.practice);
      }

      private void menuItem4_Click(object sender, System.EventArgs e)
      {
         // forum stats
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;
            GenerateForumReport(row["id"].ToString());
         }
      }

      public void GenerateForumReport(string trainingid)
      {
         string xml="";
         DataSet ds = DCEWebAccess.GetdataSet(
@"select s.id, dbo.StudentName(s.id,0) as StudentName,count(f.PostDate) as StartedTopics , 
(select count(*) from ForumReplies fr,ForumTopics ft where fr.Author=s.id 
and fr.Topic=ft.id and ft.Training='"+trainingid+@"') as ReplyCount 
from 
dbo.AllDistinctTrainingStudents('"+trainingid+@"') s
left join ForumTopics f on f.Author=s.id and f.Training='"+trainingid+@"' ,
Students ss where ss.id = s.id group by s.id "
,"StudentStats");
         xml = ds.GetXml();
         DCEAccessLib.XmlReports.ProduceReport(StatsTraining.GenerateTrainingInfo(trainingid)+ 
            "<ForumReport>" + xml + "</ForumReport>","DCEInternalSystem.Res.TrainingForumStats.xsl");
      }
	}

   

   public class StatsTrainingListNode : NodeControl
   {
      public StatsTrainingListNode (NodeControl parent)
         : base(parent)
      {
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.StatsTrainingList(this);
         }
         if (needRefresh)
         {
            ((StatsTrainingList)this.fControl).RefreshData();
            needRefresh = false;
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
