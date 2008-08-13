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
	/// Список групп студентов для показа статистики
	/// </summary>
	public class StatsStudentGroups : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnSingle;
      private System.Windows.Forms.ImageList imageList1;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.ComponentModel.IContainer components;

      public StatsStudentGroupsNode Node;
		public StatsStudentGroups(StatsStudentGroupsNode node)
		{
         Node = node;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         RefreshData();			   
		}

      public void RefreshData()
      {
         string query="";
         if (DCEUser.CurrentUser.Students == DCEUser.Access.No
            && DCEUser.CurrentUser.Tests == DCEUser.Access.No)
         {
            query =
              "select DISTINCT g.* from Groups g,Rights r where g.type ="+ ((int)EntityType.student).ToString()+
              "and g.id=r.permid and r.eid='"+DCEUser.CurrentUser.id+"' and g.id not in (SELECT Students from dbo.Trainings UNION (SELECT Students from dbo.Tracks))";
         }
         else
            query ="select * from Groups where type ="+ ((int)EntityType.student).ToString()+
            " and id not in (SELECT Students from dbo.Trainings UNION (SELECT Students from dbo.Tracks))";

         this.dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            query, "Groups"  );
         this.dataView.Table = this.dataSet.Tables["Groups"];
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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatsStudentGroups));
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnSingle = new System.Windows.Forms.ToolBarButton();
         this.imageList1 = new System.Windows.Forms.ImageList(this.components);
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataSet = new System.Data.DataSet();
         this.dataView = new System.Data.DataView();
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.SuspendLayout();
         // 
         // toolBar1
         // 
         this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
         this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                    this.btnRefresh,
                                                                                    this.btnSingle});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.ImageList = this.imageList1;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(636, 39);
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
         this.btnSingle.Text = "Стат. по группе";
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
                                                                               this.dataColumnHeader2});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.Location = new System.Drawing.Point(0, 39);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(636, 297);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 26;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItem1_Click);
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "Name";
         this.dataColumnHeader1.Text = "Название";
         this.dataColumnHeader1.Width = 200;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "Description";
         this.dataColumnHeader2.Text = "Описание";
         this.dataColumnHeader2.Width = 400;
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // contextMenu1
         // 
         this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItem1});
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 0;
         this.menuItem1.Text = "Статистика по группе";
         this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
         // 
         // StatsStudentGroups
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList,
                                                                      this.toolBar1});
         this.Name = "StatsStudentGroups";
         this.Size = new System.Drawing.Size(636, 336);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         /// статистика по выбранной группе
         /// 
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = (DataRowView) this.dataList.SelectedItems[0].Tag;
            foreach (NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(StatGroupNode))
               {
                  if (((StatGroupNode)node).Id == row["id"].ToString())
                  {
                     node.Select();
                     return;
                  }
               }
            }
            StatGroupNode n = new StatGroupNode(this.Node,row["id"].ToString());
            n.Select();
         }
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)
         {
            this.RefreshData();
         }

         if (e.Button == this.btnSingle)
         {
            this.menuItem1_Click(null,null);
         }
      }
	}


   public class StatsStudentGroupsNode : NodeControl
   {
      public StatsStudentGroupsNode (NodeControl parent)
         :base(parent)
      {
      }

      public override String GetCaption()
      {
         return "Группы студентов";
      }

      public override bool CanClose()
      {
         return false;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCEInternalSystem.StatsStudentGroups(this);
         }
         if (needRefresh)
         {
            ((StatsStudentGroups)this.fControl).RefreshData();
            needRefresh = false;
         }
         return this.fControl;
      }


   }

}
 