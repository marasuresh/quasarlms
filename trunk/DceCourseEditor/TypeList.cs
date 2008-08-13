using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCECourseEditor
{
   public class TypeListNode : NodeControl
   {
      public TypeListNode(NodeControl node)
         : base (node)
      {
      }
      
      public override UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new TypeList(this);
         }
         if (needRefresh)
         {
            ((TypeList)this.fControl).RefreshData();
            needRefresh = false;
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Типы курсов";
      }

      public override void ReleaseControl()
      {
         // do nothing
      }
      
      public override bool CanClose()
      {
         return false;
      }
   }

   /// <summary>
	/// Список типов курсов
	/// </summary>
	public class TypeList : System.Windows.Forms.UserControl
	{
      #region Form variables
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private DCEAccessLib.DataList dataList;
      private System.Windows.Forms.ContextMenu TypeContextMenu;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private System.Windows.Forms.MenuItem menuItemCreate;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItemEdit;
      private System.Windows.Forms.MenuItem menuItemRemove;
      private System.Windows.Forms.MenuItem menuItem5;
      private System.Windows.Forms.MenuItem menuItemRefresh;
      #endregion

		private TypeListNode Node;

      public TypeList(TypeListNode node)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         Node = node;

         if (DCEUser.CurrentUser.ReadOnlyCourses)
         {
            this.menuItemCreate.Enabled = false;
            this.menuItemRemove.Enabled = false;
         }

         RefreshData();
      }
      
      public void RefreshData()
      {
         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            "select *, dbo.GetStrContentAlt(Name, 'RU', 'EN') as RName from dbo.CourseType", 
            "CourseType");

         dataView.Table = dataSet.Tables["CourseType"];
      }

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
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.TypeContextMenu = new System.Windows.Forms.ContextMenu();
         this.menuItemCreate = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItemEdit = new System.Windows.Forms.MenuItem();
         this.menuItemRemove = new System.Windows.Forms.MenuItem();
         this.menuItem5 = new System.Windows.Forms.MenuItem();
         this.menuItemRefresh = new System.Windows.Forms.MenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // dataList
         // 
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.dataColumnHeader1});
         this.dataList.ContextMenu = this.TypeContextMenu;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.HideSelection = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(488, 352);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 0;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItemEdit_Click);
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "RName";
         this.dataColumnHeader1.Text = "Название типа";
         this.dataColumnHeader1.Width = 450;
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
         // 
         // TypeContextMenu
         // 
         this.TypeContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                        this.menuItemCreate,
                                                                                        this.menuItem2,
                                                                                        this.menuItemEdit,
                                                                                        this.menuItemRemove,
                                                                                        this.menuItem5,
                                                                                        this.menuItemRefresh});
         // 
         // menuItemCreate
         // 
         this.menuItemCreate.Index = 0;
         this.menuItemCreate.Text = "Создать";
         this.menuItemCreate.Click += new System.EventHandler(this.menuItemCreate_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 1;
         this.menuItem2.Text = "-";
         // 
         // menuItemEdit
         // 
         this.menuItemEdit.DefaultItem = true;
         this.menuItemEdit.Index = 2;
         this.menuItemEdit.Text = "Редактировать";
         this.menuItemEdit.Click += new System.EventHandler(this.menuItemEdit_Click);
         // 
         // menuItemRemove
         // 
         this.menuItemRemove.Index = 3;
         this.menuItemRemove.Text = "Удалить";
         this.menuItemRemove.Click += new System.EventHandler(this.menuItemRemove_Click);
         // 
         // menuItem5
         // 
         this.menuItem5.Index = 4;
         this.menuItem5.Text = "-";
         // 
         // menuItemRefresh
         // 
         this.menuItemRefresh.Index = 5;
         this.menuItemRefresh.Text = "Обновить";
         this.menuItemRefresh.Click += new System.EventHandler(this.menuItemRefresh_Click);
         // 
         // TypeList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList});
         this.Name = "TypeList";
         this.Size = new System.Drawing.Size(488, 352);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemCreate_Click(object sender, System.EventArgs e)
      {
         TypeEditNode node = new TypeEditNode(Node, "", "");
         node.Select();
      }

      private void menuItemEdit_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count > 0 && 
            this.dataList.SelectedItems[0].Tag != null)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;

            bool neednew = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(TypeListNode) )
               {
                  if ( ((TypeEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     node.Select();
                     neednew = false;
                     break;
                  }
               }
            }
            if (neednew)
            {
               TypeEditNode node = new TypeEditNode(Node, row["id"].ToString(), row["RName"].ToString());
               node.Select();
            }
         }
      }

      private void menuItemRemove_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0 && 
            this.dataList.SelectedItems[0].Tag!=null)      
         {
            DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;
            if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данную запись?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from CourseType where id = '" + row["id"].ToString() + "'");
               foreach (NodeControl node in Node.Nodes)
               {
                  if (node is TypeEditNode)
                  {
                     if (((TypeEditNode)node).Id == row["id"].ToString())
                     {
                        node.Dispose();
                        break;
                     }
                  }
               }
               this.RefreshData();
            }
         }
      }

      private void menuItemRefresh_Click(object sender, System.EventArgs e)
      {
         RefreshData();
      }
	}
}
