using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCECourseEditor
{
   public class QuestionnaireListNode : NodeControl
   {
      public QuestionnaireListNode(NodeControl node)
         : base (node)
      {
      }
      
      public override UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new QuestionnaireList(this);
         }
         if (needRefresh)
         {
            ((QuestionnaireList)this.fControl).RefreshData();
            needRefresh = false;
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "Анкеты";
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
	/// Список анкет
	/// </summary>
	public class QuestionnaireList : System.Windows.Forms.UserControl
	{
      #region Form variables
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.ContextMenu contextMenuQuestionnaire;
      private System.Windows.Forms.MenuItem menuItemCreate;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItemEdit;
      private System.Windows.Forms.MenuItem menuItemRemove;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItemRefresh;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Label label1;
      private DCEAccessLib.DataList dataListQuestionnaire;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private System.Windows.Forms.ComboBox GlobalQCombo;
      #endregion

      private QuestionnaireListNode Node;
		
      public QuestionnaireList(QuestionnaireListNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         Node = node;

         if (DCEUser.CurrentUser.ReadOnlyCourses)
         {
            this.menuItemCreate.Enabled = false;
            this.menuItemRemove.Enabled = false;
            GlobalQCombo.Enabled = false;
         }

         this.dataColumnHeader2.OnParse += new DataColumnHeader.FieldParseHandler(OnParse);

         RefreshData();
		}
      
      public bool refreshing = false;
      public void RefreshData()
      {
         try
         {
            refreshing = true;
            this.GlobalQCombo.Items.Clear();
            dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               "select t.*, e.Type as EType from dbo.Tests t,Entities e where t.Type % 3 = 0 and e.id=t.id", 
               "Questionnaires");

            dataView.Table = dataSet.Tables["Questionnaires"];
         }
         finally
         {
            refreshing = false;
         }
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
         this.contextMenuQuestionnaire = new System.Windows.Forms.ContextMenu();
         this.menuItemCreate = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItemEdit = new System.Windows.Forms.MenuItem();
         this.menuItemRemove = new System.Windows.Forms.MenuItem();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItemRefresh = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.panel1 = new System.Windows.Forms.Panel();
         this.GlobalQCombo = new System.Windows.Forms.ComboBox();
         this.label1 = new System.Windows.Forms.Label();
         this.dataListQuestionnaire = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // contextMenuQuestionnaire
         // 
         this.contextMenuQuestionnaire.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                                 this.menuItemCreate,
                                                                                                 this.menuItem2,
                                                                                                 this.menuItemEdit,
                                                                                                 this.menuItemRemove,
                                                                                                 this.menuItem1,
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
         // menuItem1
         // 
         this.menuItem1.Index = 4;
         this.menuItem1.Text = "-";
         // 
         // menuItemRefresh
         // 
         this.menuItemRefresh.Index = 5;
         this.menuItemRefresh.Text = "Обновить";
         this.menuItemRefresh.Click += new System.EventHandler(this.menuItemRefresh_Click);
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
         // 
         // panel1
         // 
         this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.GlobalQCombo,
                                                                             this.label1});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 388);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(604, 68);
         this.panel1.TabIndex = 1;
         // 
         // GlobalQCombo
         // 
         this.GlobalQCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.GlobalQCombo.Location = new System.Drawing.Point(8, 36);
         this.GlobalQCombo.Name = "GlobalQCombo";
         this.GlobalQCombo.Size = new System.Drawing.Size(328, 21);
         this.GlobalQCombo.Sorted = true;
         this.GlobalQCombo.TabIndex = 1;
         this.GlobalQCombo.SelectedIndexChanged += new System.EventHandler(this.GlobalQCombo_SelectedIndexChanged);
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(8, 12);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(200, 20);
         this.label1.TabIndex = 0;
         this.label1.Text = "Анкета для заполнения на сайте:";
         // 
         // dataListQuestionnaire
         // 
         this.dataListQuestionnaire.AllowSorting = true;
         this.dataListQuestionnaire.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataListQuestionnaire.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                                            this.dataColumnHeader1,
                                                                                            this.dataColumnHeader2});
         this.dataListQuestionnaire.ContextMenu = this.contextMenuQuestionnaire;
         this.dataListQuestionnaire.DataView = this.dataView;
         this.dataListQuestionnaire.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataListQuestionnaire.FullRowSelect = true;
         this.dataListQuestionnaire.GridLines = true;
         this.dataListQuestionnaire.HideSelection = false;
         this.dataListQuestionnaire.MultiSelect = false;
         this.dataListQuestionnaire.Name = "dataListQuestionnaire";
         this.dataListQuestionnaire.Size = new System.Drawing.Size(604, 388);
         this.dataListQuestionnaire.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataListQuestionnaire.TabIndex = 2;
         this.dataListQuestionnaire.View = System.Windows.Forms.View.Details;
         this.dataListQuestionnaire.DoubleClick += new System.EventHandler(this.menuItemEdit_Click);
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "InternalName";
         this.dataColumnHeader1.Text = "Название";
         this.dataColumnHeader1.Width = 353;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "Type";
         this.dataColumnHeader2.Text = "Тип";
         this.dataColumnHeader2.Width = 100;
         // 
         // QuestionnaireList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataListQuestionnaire,
                                                                      this.panel1});
         this.Name = "QuestionnaireList";
         this.Size = new System.Drawing.Size(604, 456);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.panel1.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemCreate_Click(object sender, System.EventArgs e)
      {
         QuestionnaireEditNode node = new QuestionnaireEditNode(Node, "", "");
         node.Select();
      }

      private void menuItemEdit_Click(object sender, System.EventArgs e)
      {
         if (this.dataListQuestionnaire.SelectedItems.Count > 0 && 
            this.dataListQuestionnaire.SelectedItems[0].Tag != null)
         {
            DataRowView row = this.dataListQuestionnaire.SelectedItems[0].Tag as DataRowView;

            bool neednew = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(QuestionnaireEditNode) )
               {
                  if ( ((QuestionnaireEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     node.Select();
                     neednew = false;
                     break;
                  }
               }
            }
            if (neednew)
            {
               QuestionnaireEditNode node = new QuestionnaireEditNode(Node, row["id"].ToString(), row["InternalName"].ToString());
               node.Select();
            }
         }
      }

      private void menuItemRemove_Click(object sender, System.EventArgs e)
      {
         if (this.dataListQuestionnaire.SelectedItems.Count>0 && 
            this.dataListQuestionnaire.SelectedItems[0].Tag!=null)      
         {
            DataRowView row = (DataRowView)this.dataListQuestionnaire.SelectedItems[0].Tag;
            if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данную запись?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from Tests where id = '" + row["id"].ToString() + "'");
               foreach (NodeControl node in Node.Nodes)
               {
                  if (node.GetType() == typeof(QuestionnaireEditNode))
                  {
                     if (((QuestionnaireEditNode)node).Id == row["id"].ToString())
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

      public class GlobalQItem
      {
         public DataRowView row;
         public GlobalQItem(DataRowView r)
         {
            row = r;
         }
         public override string ToString()
         {
            return row["InternalName"].ToString();
         }
      }
      
      void OnParse(string FieldName, DataRowView row, ref string text)
      {
         if ((int)row["Type"] == (int)TestType.questionnaire)
         {
            text = "Курсовая анкета";
         }
         else if ((int)row["Type"] == (int)TestType.globalquestionnaire)
         {
            text = "Общая анкета";

            // adding to combo
            bool add = true;
            foreach (GlobalQItem i in this.GlobalQCombo.Items)
            {
               if (i.row["id"].ToString() == row["id"].ToString())
               {
                  add =false;
                  break;
               }
            }
            if (add)
            {
               GlobalQItem itm = new GlobalQItem(row);
               this.GlobalQCombo.Items.Add(itm);
               if ((int) row["EType"] == (int)EntityType.globalquestionnaire)
               {
                  this.GlobalQCombo.SelectedItem = itm;
               }
            }
         }
      }

      private void GlobalQCombo_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ///
         if (!refreshing)
         {
            GlobalQItem itm  = (GlobalQItem) this.GlobalQCombo.SelectedItem;
            if (itm != null)
            {
               try
               {
                  DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("update dbo.Entities set Type="
                     +((int)EntityType.test).ToString()+ " where Type="+((int)EntityType.globalquestionnaire).ToString()
                   +  "update dbo.Entities set Type="+((int)EntityType.globalquestionnaire).ToString()+" where id='"+itm.row["id"].ToString()+"'"
                     );
               }
               finally
               {
                  this.RefreshData();
               }
            }
         }
      }
	}
}
