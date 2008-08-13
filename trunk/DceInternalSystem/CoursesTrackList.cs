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
   /// Класс описывающий ноду "Треки курсов"
   /// </summary>
   public class CourseTracksControl : NodeControl
   {
      public  CourseTracksControl (NodeControl parent)
         : base(parent)
      {
         this.rdonly = DCEUser.CurrentUser.Trainings != DCEUser.Access.Modify;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.CoursesTrackList(this);
         }
         return this.fControl;
      }
   
      public override String GetCaption()
      {
         return "Треки тренингов";
      }

      public override bool HaveChildNodes()
      {
         return false;
      }
   }
   /// <summary>
	/// Список треков тренингов
	/// </summary>
	public class CoursesTrackList : System.Windows.Forms.UserControl
	{
      private System.Data.DataView dataView;
      private System.Data.DataSet dataSet;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnAdd;
      private System.Windows.Forms.ToolBarButton btnDel;
      private System.Windows.Forms.ToolBarButton btnProps;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private System.Windows.Forms.MenuItem menuItemFrom;

      protected CourseTracksControl Node;
      public CoursesTrackList(CourseTracksControl node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.toolBar1.ImageList = ToolbarImages.Images.images;
         this.Node = node;

         this.btnDel.Enabled =
         this.btnAdd.Enabled =
         this.menuItem3.Enabled = 
         this.menuItem4.Enabled = this.Node.CanModify;

         RefreshData();
		}

      private class tagMenuItem : System.Windows.Forms.MenuItem
      {
         public tagMenuItem(string trackId)
         {
            TrackId = trackId;
         }
         public tagMenuItem(string trackId, string caption, System.EventHandler onclick):
            base(caption, onclick)
         {
            TrackId = trackId;
         }
         public string TrackId;
      }

      public void RefreshData()
      {
         dataSet =  DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            "select id, dbo.GetStrContentAlt(Name,'RU','EN') as Name, dbo.GetStrContentAlt(Description,'RU','EN') as Description  from dbo.Tracks","Tracks");
         dataView.Table = dataSet.Tables["Tracks"];

         System.Data.DataSet dsFrom = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            "select id, dbo.GetStrContentAlt(Name,'RU','EN') as Name, dbo.GetStrContentAlt(Description,'RU','EN') as Description  from dbo.CTracks","CTracks");
         System.Data.DataTable tableFrom = dsFrom.Tables["CTracks"];
         if (tableFrom != null && tableFrom.Rows.Count > 0)
         {
            this.menuItemFrom.Enabled = true;
            this.menuItemFrom.MenuItems.Clear();
            foreach (System.Data.DataRow row in tableFrom.Rows)
            {
               tagMenuItem item = new tagMenuItem(row["id"].ToString(), row["Name"].ToString(), new System.EventHandler(this.menuItemFromI_Click));
               this.menuItemFrom.MenuItems.Add(item);
            }
         }
      }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         this.Node = null;
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
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnAdd = new System.Windows.Forms.ToolBarButton();
         this.btnDel = new System.Windows.Forms.ToolBarButton();
         this.btnProps = new System.Windows.Forms.ToolBarButton();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.menuItemFrom = new System.Windows.Forms.MenuItem();
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
                                                                                     this.menuItemFrom,
                                                                                     this.menuItem4});
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 0;
         this.menuItem1.Text = "Свойства";
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
         this.menuItem3.Text = "Добавить";
         this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 4;
         this.menuItem4.Text = "Удалить";
         this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
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
                                                                                    this.btnAdd,
                                                                                    this.btnDel,
                                                                                    this.btnProps});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(392, 40);
         this.toolBar1.TabIndex = 41;
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
         this.btnAdd.Text = "Добавить";
         // 
         // btnDel
         // 
         this.btnDel.ImageIndex = 2;
         this.btnDel.Text = "Удалить";
         // 
         // btnProps
         // 
         this.btnProps.ImageIndex = 3;
         this.btnProps.Text = "Свойства";
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.dataColumnHeader1,
                                                                               this.dataColumnHeader2});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.HideSelection = false;
         this.dataList.Location = new System.Drawing.Point(0, 40);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(392, 288);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 42;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItem1_Click);
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "Name";
         this.dataColumnHeader1.Text = "Название";
         this.dataColumnHeader1.Width = 303;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "Description";
         this.dataColumnHeader2.Text = "Описание";
         this.dataColumnHeader2.Width = 400;
         // 
         // menuItemFrom
         // 
         this.menuItemFrom.Enabled = false;
         this.menuItemFrom.Index = 3;
         this.menuItemFrom.Text = "Из трека курсов...";
         // 
         // CoursesTrackList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "CoursesTrackList";
         this.Size = new System.Drawing.Size(392, 328);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0 && this.dataList.SelectedItems[0].Tag!=null)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;

            bool neednew = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(TrackEditControl) )
               {
                  if ( ((TrackEditControl)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     node.Select();
                     neednew = false;
                     break;
                  }
               }
            }
            if (neednew)
            {
               TrackEditControl node = new TrackEditControl(Node,row["id"].ToString());
               node.Select();
            }
         }
      }

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         // new track
         TrackEditControl node = new TrackEditControl(Node,"");
         node.Select();
      }

      private void menuItem4_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0 && this.dataList.SelectedItems[0].Tag!=null)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;
            foreach (NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(TrackEditControl))
               {
                  if ( ((TrackEditControl)node).Id == row["id"].ToString())
                  {
                     System.Windows.Forms.MessageBox.Show("Перед удалением, закройте окно редактирования.","Удалить");
                     node.Select();
                     return;
                  }
               }
            }
            
            if (MessageBox.Show("Вы действительно хотите удалить выбранный трек?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
     
               try
               {
                  DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("Delete from dbo.Tracks where id='"+ row["id"].ToString()+"'");
               }
               finally
               {
                  dataSet =  DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
                     "select id, dbo.GetStrContentAlt(Name,'RU','EN') as Name, dbo.GetStrContentAlt(Description,'RU','EN') as Description  from dbo.Tracks","Tracks");
                  dataView.Table = dataSet.Tables["Tracks"];
               }
            }
         }
      
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
            this.RefreshData();
         if (e.Button == this.btnAdd)
            this.menuItem3_Click(null,null);
         if (e.Button == this.btnDel)
            this.menuItem4_Click(null,null);
         if (e.Button == this.btnProps)
            this.menuItem1_Click(null,null);
      }

      private void menuItemFromI_Click(object sender, System.EventArgs e)
      {
         tagMenuItem item = (tagMenuItem) sender;
         // new track
         string cTrackId = item.TrackId;
         System.Data.DataSet dsCourses = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(@"
                  select c.id, c.Code, c.Name
                  from CTracks ctr, GroupMembers gm, Courses c 
                  where ctr.id ='"+cTrackId+"' and gm.id=c.id and gm.MGroup=ctr.Courses", "Courses");
         System.Data.DataTable tableCourses = dsCourses.Tables["Courses"];

         string trackId = System.Guid.NewGuid().ToString();
         string trainingsId = System.Guid.NewGuid().ToString();

                  
         if (tableCourses != null && tableCourses.Rows.Count > 0)
         {
            int baseIndex = 2;
            int interval = 2;
            int count = baseIndex+tableCourses.Rows.Count*interval;
            DataSet[] datasets = new DataSet[count];
            string[] sqls = new string[count];
            string[] tables = new string[count];

            string trackName = System.Guid.NewGuid().ToString();
            sqls[0] = @"insert into Tracks (id, Trainings, Students, Name) 
                  values ('"+trackId+"', '"+trainingsId+"','"+System.Guid.NewGuid().ToString()+"', '"+trackName+@"')
                  insert into Content (Lang, eid, Type, DataStr) 
                  values (1, '"+trackName+"', "+((int)ContentType._string).ToString()+", 'Новый трек на основе "+item.Text+"')";

            sqls[1] = "select * from Trainings";
            tables[1] = "Trainings";
            System.Data.DataSet dsTrainings 
               = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               "select * from Trainings", "Trainings");
            datasets[1] = dsTrainings;

            for (int i=0; i < tableCourses.Rows.Count; i++)
            {
               System.Data.DataRow crow = tableCourses.Rows[i];
               string triningId = System.Guid.NewGuid().ToString();
               string trainingName = System.Guid.NewGuid().ToString();
               System.Data.DataRow trow = dsTrainings.Tables["Trainings"].NewRow();
               trow["id"] = triningId;
               trow["Course"] = crow["id"];
               trow["Name"] = trainingName;
               trow["Code"] = crow["Code"].ToString();
               trow["isPublic"] = 0;
               trow["isActive"] = 0;
               trow["TimeStrict"] = 0;
               dsTrainings.Tables["Trainings"].Rows.Add(trow);

               sqls[baseIndex+i*interval] = @"
                  insert into GroupMembers (MGroup, id)
                  values ('"+trainingsId+"', '"+triningId+@"')
                  exec dbo.MakeContentCopy '"+trainingName+"', '"+crow["Name"].ToString()+"'"; 

               DCEAccessLib.DataSetBatchUpdate scheduleSet 
                  = new DCEAccessLib.DataSetBatchUpdate();
               TrainingEditControl.CreateSchedule(crow["id"].ToString(), 
                  triningId, ref scheduleSet, System.DateTime.Now);
               datasets[baseIndex+i*interval+1] = scheduleSet.dataSet;
               sqls[baseIndex+i*interval+1] = scheduleSet.sql;
               tables[baseIndex+i*interval+1] = scheduleSet.tableName;
            }
            DCEAccessLib.DCEWebAccess.WebAccess.UpdateDataSets(sqls, tables, datasets);

            this.RefreshData();
         }
      }
   }
}
