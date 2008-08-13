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
   /// Класс описывающий ноду "Просмотр и удовлетворение заявок"
   /// </summary>
   public class RequestControl : NodeControl
   {

      public RequestControl (NodeControl parent)
         : base(parent)
      {
         this.rdonly = DCEUser.CurrentUser.Requests != DCEUser.Access.Modify;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.RequestList(this);
         }
         return this.fControl;
      }
      public override String GetCaption()
      {
         return "Заявки";
      }
      
      public override bool HaveChildNodes()
      {
         return false;
      }
   }
   /// <summary>
	/// Список заявок
	/// </summary>
	public class RequestList : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button OkButton;
      private System.Windows.Forms.Button CancelButton;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader3;
      private DCEAccessLib.DataColumnHeader dataColumnHeader4;
      private DCEAccessLib.DataColumnHeader dataColumnHeader5;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.Button CreateRequest;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.MenuItem menuItem5;
      private DCEAccessLib.DataColumnHeader dataColumnHeader6;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnAdd;
      private System.Windows.Forms.ToolBarButton btnDel;
      private System.Windows.Forms.ToolBarButton btnApply;

      RequestControl Node;
		public RequestList(RequestControl node)
		{
         this.Node = node;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.toolBar1.ImageList = ToolbarImages.Images.images;
         RefreshData();
         CreateRequest.Enabled = Node.CanModify;
         OkButton.Enabled = Node.CanModify;
         CancelButton.Enabled = Node.CanModify;
         menuItem3.Enabled = Node.CanModify;
         menuItem4.Enabled = Node.CanModify;
         menuItem5.Enabled = Node.CanModify;
         this.btnAdd.Enabled = Node.CanModify;
         this.btnDel.Enabled = Node.CanModify;
         this.btnApply.Enabled = Node.CanModify;
      }

      protected void RefreshData()
      {
         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
@"select s.LastName +' '+s.FirstName + ' ' + s.Patronymic as StudentName, dbo.GetStrContentAlt(c.Name,'RU','EN') as CourseName, c.Version, 
r.Course, r.RequestDate, r.StartDate, r.Comments, s.id, r.id as rid, 0 as isTrack
from Students s, Courses c, CourseRequest r where r.Student = s.id and r.Course = c.id
union
select s.LastName +' '+s.FirstName + ' ' + s.Patronymic as StudentName, dbo.GetStrContentAlt(ct.Name,'RU','EN') as CourseName, 'Track' as Version, 
ct.Courses as Course, r.RequestDate, r.StartDate, r.Comments, s.id, r.id as rid, 1 as isTrack
from Students s, CTracks ct, CTrackRequest r where r.Student = s.id and r.CTrack = ct.id
","req");
         dataView.Table = dataSet.Tables["req"];
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
         this.dataView = new System.Data.DataView();
         this.panel1 = new System.Windows.Forms.Panel();
         this.CreateRequest = new System.Windows.Forms.Button();
         this.OkButton = new System.Windows.Forms.Button();
         this.CancelButton = new System.Windows.Forms.Button();
         this.dataSet = new System.Data.DataSet();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader6 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader3 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader4 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader5 = new DCEAccessLib.DataColumnHeader();
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.menuItem5 = new System.Windows.Forms.MenuItem();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnAdd = new System.Windows.Forms.ToolBarButton();
         this.btnDel = new System.Windows.Forms.ToolBarButton();
         this.btnApply = new System.Windows.Forms.ToolBarButton();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.panel1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.CreateRequest,
                                                                             this.OkButton,
                                                                             this.CancelButton});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 344);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(660, 40);
         this.panel1.TabIndex = 19;
         // 
         // CreateRequest
         // 
         this.CreateRequest.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.CreateRequest.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.CreateRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.CreateRequest.Location = new System.Drawing.Point(249, 8);
         this.CreateRequest.Name = "CreateRequest";
         this.CreateRequest.Size = new System.Drawing.Size(128, 24);
         this.CreateRequest.TabIndex = 4;
         this.CreateRequest.Text = "Создать";
         this.CreateRequest.Click += new System.EventHandler(this.button1_Click);
         // 
         // OkButton
         // 
         this.OkButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.OkButton.Location = new System.Drawing.Point(384, 8);
         this.OkButton.Name = "OkButton";
         this.OkButton.Size = new System.Drawing.Size(128, 24);
         this.OkButton.TabIndex = 2;
         this.OkButton.Text = "Удовлетворить ";
         this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
         // 
         // CancelButton
         // 
         this.CancelButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.CancelButton.Location = new System.Drawing.Point(520, 8);
         this.CancelButton.Name = "CancelButton";
         this.CancelButton.Size = new System.Drawing.Size(128, 24);
         this.CancelButton.TabIndex = 3;
         this.CancelButton.Text = "Удалить";
         this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
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
                                                                               this.dataColumnHeader6,
                                                                               this.dataColumnHeader3,
                                                                               this.dataColumnHeader4,
                                                                               this.dataColumnHeader5});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.HideSelection = false;
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(660, 307);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 1;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.OkButton_Click);
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "StudentName";
         this.dataColumnHeader1.Text = "Студент/Абитуриент";
         this.dataColumnHeader1.Width = 200;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "CourseName";
         this.dataColumnHeader2.Text = "Курс / Трек";
         this.dataColumnHeader2.Width = 290;
         // 
         // dataColumnHeader6
         // 
         this.dataColumnHeader6.FieldName = "Version";
         this.dataColumnHeader6.Text = "Версия";
         // 
         // dataColumnHeader3
         // 
         this.dataColumnHeader3.FieldName = "RequestDate";
         this.dataColumnHeader3.Text = "Дата заявки";
         this.dataColumnHeader3.Width = 100;
         // 
         // dataColumnHeader4
         // 
         this.dataColumnHeader4.FieldName = "StartDate";
         this.dataColumnHeader4.Text = "Желаемое начало обучения";
         this.dataColumnHeader4.Width = 150;
         // 
         // dataColumnHeader5
         // 
         this.dataColumnHeader5.FieldName = "Comments";
         this.dataColumnHeader5.Text = "Коментарии";
         this.dataColumnHeader5.Width = 300;
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
         this.menuItem3.Text = "Создать заявку";
         this.menuItem3.Click += new System.EventHandler(this.button1_Click);
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 1;
         this.menuItem4.Text = "Удовлетворить заявку";
         this.menuItem4.Click += new System.EventHandler(this.OkButton_Click);
         // 
         // menuItem5
         // 
         this.menuItem5.Index = 2;
         this.menuItem5.Text = "Удалить заявку";
         this.menuItem5.Click += new System.EventHandler(this.CancelButton_Click);
         // 
         // menuItem1
         // 
         this.menuItem1.Index = -1;
         this.menuItem1.Text = "Обновить список";
         this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
         // 
         // toolBar1
         // 
         this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
         this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                    this.btnRefresh,
                                                                                    this.btnAdd,
                                                                                    this.btnDel,
                                                                                    this.btnApply});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(660, 40);
         this.toolBar1.TabIndex = 37;
         this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
         // 
         // btnRefresh
         // 
         this.btnRefresh.ImageIndex = 0;
         this.btnRefresh.Text = "Обновить";
         // 
         // btnAdd
         // 
         this.btnAdd.ImageIndex = 1;
         this.btnAdd.Text = "Создать";
         // 
         // btnDel
         // 
         this.btnDel.ImageIndex = 2;
         this.btnDel.Text = "Удалить";
         // 
         // btnApply
         // 
         this.btnApply.ImageIndex = 6;
         this.btnApply.Text = "Удовлетворить";
         // 
         // RequestList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.panel1,
                                                                      this.toolBar1});
         this.Name = "RequestList";
         this.Size = new System.Drawing.Size(660, 384);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.panel1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void OkButton_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView srow = (DataRowView)this.dataList.SelectedItems[0].Tag;
            
            if ((int)srow["isTrack"] == 1)
            {
               DataRowView track = TrackSelect.SelectTrack();
               if (track != null)
               {
                  RequestAccept a = new RequestAccept(srow["id"].ToString(),srow["CourseName"].ToString(),srow["StartDate"].ToString());
                  if (a.ShowDialog() == DialogResult.OK)
                  {
                     try
                     {
                        DCEWebAccess.execSQL("insert into GroupMembers (id,MGroup) VALUES ('"+srow["id"].ToString()+"','"+track["Students"].ToString()+"')"
                           + " delete from dbo.CTrackRequest where id='"+srow["rid"].ToString()+"'");
                  
                     }
                     finally
                     {
                        this.RefreshData();
                     }
                  }
               }
               return;
            }

            DataSet dsCourses = null;
            dsCourses = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(@"
                  select c.id, dbo.GetStrContentAlt(c.Name,'RU','EN') as CourseName
                  from Courses c, GroupMembers gm
                  where gm.MGroup = '"+srow["Course"].ToString()+@"'
                     and gm.id=c.id"
               ,"Courses");
            System.Data.DataTable tableCourses = dsCourses.Tables["Courses"];
            if (tableCourses.Rows.Count == 0)
            {
               System.Data.DataRow trow = tableCourses.NewRow();
               tableCourses.Rows.Add(trow);
               trow["id"] = srow["Course"];
               trow["CourseName"] = srow["CourseName"].ToString();
            }
            bool hasComplete = false;
            bool hasIncomplete = false;
            foreach (System.Data.DataRow crow in tableCourses.Rows)
            {

               DataSet exds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select id from dbo.Trainings where Course <>'"+crow["id"].ToString()+"'","tr");
               DataView exdv = new DataView(exds.Tables["tr"]);


               DataRowView row = TrainingSelect.SelectTraining(exdv);

               exdv.Dispose();
               exds.Dispose();

               if (row != null)
               {
                  // we need to check if the student already in this training
                  DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("Select count(*) from dbo.AllTrainingStudents('"+row["id"].ToString()+"') where id='"+ srow["id"].ToString() +"'","cnt");
                  if ( (int)(ds.Tables["cnt"].Rows[0][0]) > 0 )
                  {
                     MessageBox.Show("Указанный студент уже является учащимся выбраного тренинга. Повторное добавление невозможно.",
                        "Удовлетворить заявку");
                     hasComplete = true;
                  }
                  else
                  {
                  
                     RequestAccept ac = new RequestAccept(srow["id"].ToString(),crow["CourseName"].ToString(),row["StartDate"].ToString());

                     bool complete = ac.ShowDialog() == DialogResult.OK;
                     if (complete)
                     {
                        DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL(@"
                              insert into dbo.GroupMembers (id,MGroup) 
                              values('"+srow["id"].ToString()+"' , '"+ row["Students"].ToString()+"')");
                     }
                     hasComplete = 
                        hasComplete || complete;
                     hasIncomplete = 
                        hasIncomplete || !complete;
                  }
               }
               else
               {
                  hasIncomplete = true;
               }
            }
            if (hasComplete && !hasIncomplete)
            {
               try
               {
                  if ((int)srow["isTrack"] == 0)
                  {
                     DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL(@"
                        delete from dbo.CourseRequest 
                        where id='"+srow["rid"].ToString()+"'");
                  }
                  else
                  {
                     DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL(@"
                        delete from dbo.CTrackRequest 
                        where id='"+srow["rid"].ToString()+"'");
                  }
               }
               finally
               {
                  this.RefreshData();
               }
            }
         }
      }

      private void CancelButton_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            if ( MessageBox.Show("Вы действительно хотите удалить выбранную заявку?",
               "Удалить заявку", MessageBoxButtons.YesNo) == DialogResult.Yes )
            {
               DataRowView srow =(DataRowView) this.dataList.SelectedItems[0].Tag;

               try
               {
                  if ((int)srow["isTrack"] == 0)
                  {
                     DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL(
                        "delete from dbo.CourseRequest where id='"+srow["rid"].ToString()+"'"
                        );
                  }
                  else
                  {
                     DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL(
                        "delete from dbo.CTrackRequest where id='"+srow["rid"].ToString()+"'"
                        );
                  }
               }
               finally
               {
                  this.RefreshData();
               }
            }
         }
      }

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         this.RefreshData();
      }

      private void button1_Click(object sender, System.EventArgs e)
      {
         CreateRequest req;
         DataRowView row = UserSelect.SelectStudent(null,"");
         if (row !=null)
         {
            try
            {
               req = new CreateRequest(row["LastName"].ToString() + " " +row["FirstName"].ToString() +" " +row["Patronymic"].ToString() );
               if (req.ShowDialog() == DialogResult.OK)
               {
                  if (req.list.dataList.SelectedItems.Count>0)
                  {
                     DataRowView cRow = (DataRowView)req.list.dataList.SelectedItems[0].Tag;
                     string comm=req.Comments.Text.Replace("'","\\'");
                  
                     DCEWebAccess.execSQL("insert into CourseRequest (Student,Course,RequestDate,StartDate,Comments) values('"+row["id"].ToString()
                        +"','"+cRow["id"].ToString()+"','"+
                        Settings.ToSQLDate(DateTime.Now)+"','"+
                        Settings.ToSQLDate(req.StartDate.Value)+"','"+
                        comm+"')");
                  }
                  else
                     MessageBox.Show("Не выбран курс.");
               }
            }
            finally
            {
               this.RefreshData();
            }
         }
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
            this.RefreshData();
         if (e.Button == this.btnAdd)
            button1_Click(null,null);
         if (e.Button == this.btnDel)
            this.CancelButton_Click(null,null);
         if (e.Button == this.btnApply)
            this.OkButton_Click(null,null);
      }
	}
}
