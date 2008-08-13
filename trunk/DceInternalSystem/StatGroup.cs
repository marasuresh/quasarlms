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
	/// Статистика по группе
	/// </summary>
	public class StatGroup : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnSingle;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private System.Data.DataSet dataSet;
      private System.Windows.Forms.ImageList imageList1;
      private System.Data.DataView dataView;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.ComponentModel.IContainer components;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.MenuItem menuItem5;
      private System.Windows.Forms.ToolBarButton StatAll;

      public StatGroupNode Node;
		public StatGroup(StatGroupNode node)
		{
         Node = node;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
         dataColumnHeader1.OnParse += new DataColumnHeader.FieldParseHandler(StudentNameParse);
         RefreshData();
		}

      void StudentNameParse(string FieldName, DataRowView row, ref string text)
      {
         text = row["LastName"].ToString() + " " + row["FirstName"].ToString() + " " + row["Patronymic"].ToString();
      }


      public void RefreshData()
      {
         this.dataSet = 
            DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               "select s.id, s.FirstName,s.LastName,s.Patronymic from GroupMembers gm, Students s where "   
               + " s.id = gm.id and gm.MGroup= '"+this.Node.Id+"'","Students");
         this.dataView.Table = this.dataSet.Tables["Students"];
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
         this.components = new System.ComponentModel.Container();
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatGroup));
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnSingle = new System.Windows.Forms.ToolBarButton();
         this.imageList1 = new System.Windows.Forms.ImageList(this.components);
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.menuItem5 = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.StatAll = new System.Windows.Forms.ToolBarButton();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // menuItem1
         // 
         this.menuItem1.Index = -1;
         this.menuItem1.Text = "Статистика по группе";
         // 
         // toolBar1
         // 
         this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
         this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                    this.btnRefresh,
                                                                                    this.btnSingle,
                                                                                    this.StatAll});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.ImageList = this.imageList1;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(656, 39);
         this.toolBar1.TabIndex = 26;
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
         this.btnSingle.Text = "Стат. студента";
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
                                                                               this.dataColumnHeader1});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 39);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(656, 253);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 27;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItem3_Click);
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "FirstName";
         this.dataColumnHeader1.Text = "Студент";
         this.dataColumnHeader1.Width = 250;
         // 
         // contextMenu1
         // 
         this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItem3,
                                                                                     this.menuItem4,
                                                                                     this.menuItem5});
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 0;
         this.menuItem3.Text = "Статистика студента";
         this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 1;
         this.menuItem4.Text = "-";
         // 
         // menuItem5
         // 
         this.menuItem5.Index = 2;
         this.menuItem5.Text = "Статистика по всем студентам";
         this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // menuItem2
         // 
         this.menuItem2.Index = -1;
         this.menuItem2.Text = "Статистика по группе";
         // 
         // StatAll
         // 
         this.StatAll.ImageIndex = 2;
         this.StatAll.Text = "Стат. всех студентов";
         // 
         // StatGroup
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "StatGroup";
         this.Size = new System.Drawing.Size(656, 292);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private string GroupInfo()
      {
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("Select Name, Description from Groups where id='"+this.Node.Id+"'","GroupInfo");
         return ds.GetXml();
      }
      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         /// статистика одного студента
         /// 

         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView) this.dataList.SelectedItems[0].Tag;

            string xml = GroupInfo()+"<StudentStats>"+ GenerateStudentReport(row["id"].ToString())+ "</StudentStats>";
            DCEAccessLib.XmlReports.ProduceReport(xml,"DCEInternalSystem.Res.StudentGroupStats.xsl");
         }
      }

      private void menuItem5_Click(object sender, System.EventArgs e)
      {
         ///
         string xml = GroupInfo();
         foreach (DataRowView row in this.dataView)
         {
            xml += "<StudentStats>"+ GenerateStudentReport(row["id"].ToString())+ "</StudentStats>";
         }
         DCEAccessLib.XmlReports.ProduceReport(xml,"DCEInternalSystem.Res.StudentGroupStats.xsl");
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
         {
            this.RefreshData();
         }
         if (e.Button == this.btnSingle)
         {
            menuItem3_Click(null,null);
         }
         if (e.Button == this.StatAll)
         {
            menuItem5_Click(null,null);
         }
      }

      private string GenerateStudentReport(string studentid)
      {

         string xml = "";
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select * from Students where id='"+studentid+"'","StudentInfo");
         xml += ds.GetXml();

         ds = DCEWebAccess.WebAccess.GetDataSet("Select t.id, t.Course, t.Code, dbo.GetStrContentAlt(t.Name,'RU','EN') as TrName from Trainings t , dbo.AllStudentTrainings('"+studentid+"') st where st.id=t.id","t");
         foreach (DataRow row in ds.Tables["t"].Rows)
         {
            xml +="\n<TrainingStats>\n";
            xml +="<Course>"+row["Course"].ToString() +"</Course>"+"<Code>" 
               + row["Code"].ToString() +"</Code>\n<Name>"+row["TrName"].ToString()+"</Name>\n";

            // тесты и пр. работы
            DataSet ds2 = DCEWebAccess.WebAccess.GetDataSet(
@"select tr.*, dbo.TestResultPoints(tr.id) as Points,t.Parent, dbo.GetThemeName(t.Parent,1) as TestName, t.Type from TestResults tr, Tests t 
where t.id = tr.Test and tr.Student = '"+studentid+ "' and tr.Test in (Select id from dbo.TrainingTests('"+row["id"].ToString()+"')) order by tr.TryStart","Tests");
            xml += ds2.GetXml();

            // задачи
            ds2 = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               @"select dbo.GetStrContentAlt(t.Name,'RU','EN') as TaskName, t.TaskTime , ts.Complete, ts.SDate from
Tasks t, TaskSolutions ts where t.Training = '"+row["id"].ToString()+@"'
and ts.Task = t.id and ts.Student = '"+studentid+"' order by t.TaskTime","Tasks");
            xml += ds2.GetXml();

            xml += "<ForumTopicsStarted>"+
               DCEWebAccess.WebAccess.GetString("select count(*) from ForumTopics where Training='"+row["id"].ToString()+"' and Author='"+studentid+"'")
               +"</ForumTopicsStarted>";
            xml += "<ForumReplies>"+
               DCEWebAccess.WebAccess.GetString("select count(*) from ForumReplies r , ForumTopics t where t.Training='"+row["id"].ToString()+"' and r.Topic = t.id and r.Author='"+studentid+"'")
               +"</ForumReplies>";
            
            xml +="\n</TrainingStats>\n";
         }
         return xml;
      }
	}

   public class StatGroupNode : NodeControl
   {
      public string Id = "";

      public StatGroupNode (NodeControl parent, string id)
         : base(parent)
      {
         this.Id = id;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.StatGroup(this);
         }
         if (needRefresh)
         {
            ((StatGroup)this.fControl).RefreshData();
            needRefresh = false;
         }
         return this.fControl;
      }

      public override bool CanClose()
      {
         return true;
      }

      public override String GetCaption()
      {
         if (Id != "")
            return "Группа:" + DCEAccessLib.DCEWebAccess.WebAccess.GetString("Select Name from Groups where id='"+this.Id+"'");
         return "";
      }

   }
}
