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
   /// Класс описывающий ноду "Тренинги"
   /// </summary>
   public class TrainingExpiresControl : NodeControl
   {
      public TrainingExpiresControl   (NodeControl parent)
            : base(parent)
      {
         this.rdonly = DCEUser.CurrentUser.Trainings != DCEUser.Access.Modify;

      }
      
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.TrainingExpiresList(this);
         }
         if (needRefresh)
         {
            ((TrainingExpiresList)this.fControl).RefreshData();
            needRefresh = false;
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Завершенные тренинги";
      }

      public override bool HaveChildNodes()
      {
         return false;
      }
   }
   /// <summary>
	/// Список завершенных тренинов
	/// </summary>
	public class TrainingExpiresList : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.MenuItem RemoveTraining;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnDel;
      private System.Windows.Forms.ToolBarButton btnProps;
      public DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader5;
      private DCEAccessLib.DataColumnHeader dataColumnHeader3;
      private DCEAccessLib.DataColumnHeader dataColumnHeader4;

      private TrainingExpiresControl Node;
		public TrainingExpiresList(TrainingExpiresControl node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
         Node = node;
         this.toolBar1.ImageList = ToolbarImages.Images.images;
         RefreshData();
         if (this.Node.Readonly)
         {
            RemoveTraining.Enabled = false;
            this.btnDel.Enabled = false;
         }
		}

      public TrainingExpiresList()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
         this.Controls.Remove(this.toolBar1);
         this.toolBar1 = null;
         Node = null;
         dataList.ContextMenu = null;
      }

      /// <summary>
      /// получает список тренингов за исключением тех, которые присутствуют в Excludes
      /// </summary>
      /// <param name="Excludes"></param>
      public void GenList (DataView Excludes)
      {
         this.dataList.SuspendListChange = true;
         try
         {
            dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
@"select dbo.GetStrContentAlt(t.Name, 'RU', 'EN') as RName, 
dbo.GetStrContentAlt(c.Name, 'RU', 'EN') as RCourse , t.* from Trainings t, Courses c
where t.Expires=1 and t.Course = c.id","Trainings");

            dataView.Table = dataSet.Tables["Trainings"];
            if (Excludes !=null)
            {
               for (int i=0; i<Excludes.Count; i++)
               {
                  for(int j=0; j<dataView.Count;j++)
                  {
                     if (dataView[j]["id"].ToString()== Excludes[i]["id"].ToString())
                     {
                        dataView.Delete(j);
                        break;
                     }
                  }
               }
            }
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
         System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
                                                                                                                                                            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "УН-333", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)))),
                                                                                                                                                            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Программирование на Си"),
                                                                                                                                                            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "25.10.2003"),
                                                                                                                                                            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "25.11.2003")}, -1);
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.RemoveTraining = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnDel = new System.Windows.Forms.ToolBarButton();
         this.btnProps = new System.Windows.Forms.ToolBarButton();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader5 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader3 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader4 = new DCEAccessLib.DataColumnHeader();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // contextMenu1
         // 
         this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItem3,
                                                                                     this.menuItem2,
                                                                                     this.RemoveTraining});
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 0;
         this.menuItem3.Text = "Свойства";
         this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 1;
         this.menuItem2.Text = "-";
         // 
         // RemoveTraining
         // 
         this.RemoveTraining.Index = 2;
         this.RemoveTraining.Text = "Удалить";
         this.RemoveTraining.Click += new System.EventHandler(this.menuItem4_Click);
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
                                                                                    this.btnDel,
                                                                                    this.btnProps});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(572, 37);
         this.toolBar1.TabIndex = 40;
         this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
         // 
         // btnRefresh
         // 
         this.btnRefresh.ImageIndex = 0;
         this.btnRefresh.Text = "Обновить";
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
                                                                               this.dataColumnHeader2,
                                                                               this.dataColumnHeader5,
                                                                               this.dataColumnHeader3,
                                                                               this.dataColumnHeader4});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.HideSelection = false;
         this.dataList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
                                                                                 listViewItem1});
         this.dataList.Location = new System.Drawing.Point(0, 37);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(572, 283);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 41;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItem3_Click);
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
         this.dataColumnHeader5.Width = 359;
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
         // TrainingExpiresList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "TrainingExpiresList";
         this.Size = new System.Drawing.Size(572, 320);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
      }

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0 && this.dataList.SelectedItems[0].Tag!=null)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;

            if (this.Node == null)
            {
               // select mode
//               this.FindForm().DialogResult = DialogResult.OK;
//               this.FindForm().Close();
               return;
            }

            bool neednew = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(TrainingEditControl) )
               {
                  if ( ((TrainingEditControl)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     node.Select();
                     neednew = false;
                     break;
                  }
               }
            }
            if (neednew)
            {
               TrainingEditControl node = new TrainingEditControl(Node,row["id"].ToString());
               node.Select();
            }
         }
      }

      public void RefreshData()
      {
         if (DCEUser.CurrentUser.Trainings == DCEUser.Access.No)
            dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               @"select dbo.GetStrContentAlt(t.Name, 'RU', 'EN') as RName, 
dbo.GetStrContentAlt(c.Name, 'RU', 'EN') as RCourse , t.* from Trainings t, Courses c
where t.Course = c.id and t.id in (SELECT tr.id from Trainings tr, GroupMembers gm where tr.Expires=1 and gm.id='"+DCEUser.CurrentUser.id+"' and (gm.MGroup = tr.Instructors or gm.MGroup = tr.Curators))","Trainings");
         else
         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            @"select dbo.GetStrContentAlt(t.Name, 'RU', 'EN') as RName, 
dbo.GetStrContentAlt(c.Name, 'RU', 'EN') as RCourse , t.* from Trainings t, Courses c
where t.Expires=1 and t.Course = c.id","Trainings");
         dataView.Table = dataSet.Tables["Trainings"];
      }


      private void menuItem4_Click(object sender, System.EventArgs e)
      {
         // удаление тренинга
         if (this.dataList.SelectedItems.Count>0 && this.dataList.SelectedItems[0].Tag!=null)      
         {
            DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;
            bool candelete = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(TrainingEditControl) )
               {
                  if ( ((TrainingEditControl)node).Id == row["id"].ToString())
                  {
                     System.Windows.Forms.MessageBox.Show("Перед удалением редактируемого тренинга, закройте окно редактирования.","Удалить");
                     node.Select();
                     candelete = false;
                     break;
                  }
               }
               if (node.GetType() == typeof(TrainingScheduleControl) )
                  if ( ((TrainingScheduleControl)node).TrainingId == row["id"].ToString())
                  {
                     System.Windows.Forms.MessageBox.Show("Перед удалением, закройте окно свойств тренинга.","Удалить");
                     node.Select();
                     candelete = false;
                     break;
                  }

            }
            if (candelete)
               if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данный тренинг?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
               {
                  DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("Delete from Trainings where id='"+row["id"].ToString()+"'");
                  RefreshData();
               }
         }

      }

      private void dataList_RowParsed(System.Data.DataRowView row, System.Windows.Forms.ListViewItem item)
      {
         if ((bool)row["isActive"])
         {
            item.BackColor = System.Drawing.Color.FromArgb(230,255,255);
         }
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnDel)
         {
            this.menuItem4_Click(null,null);
         }
         if (e.Button == this.btnProps)
         {
            this.menuItem3_Click(null,null);
         }
         if (e.Button == this.btnRefresh)
         {
            this.RefreshData();
         }

      }
	}
}
