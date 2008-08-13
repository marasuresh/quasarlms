using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCECourseEditor
{
   public enum QuestionListType
   {
      qltTest,
      qltWork,
      qltQuestionnaire
   }

   /// <summary>
	/// Список вопросов теста
	/// </summary>
	public class QuestionList : System.Windows.Forms.UserControl
	{
      #region Form variables

      private System.Windows.Forms.ContextMenu contextMenuQuestions;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private DCEAccessLib.DataList dataList;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.MenuItem menuItemRefresh;
      private System.Windows.Forms.MenuItem menuItemCreate;
      private System.Windows.Forms.MenuItem menuItemEdit;
      private System.Windows.Forms.MenuItem menuItemRemove;
      private System.Windows.Forms.MenuItem menuItemSeparator1;
      private System.Windows.Forms.MenuItem menuItemSeparator2;
      private System.Windows.Forms.Panel PanelList;
      private System.Windows.Forms.Panel PanelLeft;
      private System.Windows.Forms.Panel ButtonPanel;
      private System.Windows.Forms.Button DownButton;
      private System.Windows.Forms.Button UpButton;
      private System.Windows.Forms.MenuItem menuItemMoveUp;
      private System.Windows.Forms.MenuItem menuItemMoveDown;
      private System.Windows.Forms.MenuItem menuItemSeparator3;
      #endregion

      private RecordEditNode Node;
      private QuestionListType type;
      private System.Windows.Forms.MenuItem menuItemImport;
      private string qualif = "";

      [BrowsableAttribute(true)]
      [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)] 
      public QuestionListType QuestionType
      {
         get 
         { 
            return type; 
         }
         set 
         { 
            type = value; 
            UpdateQuestionListLayout();
         }
      }
      
      private void UpdateQuestionListLayout()
      {
         this.dataList.Columns.Clear();

         switch (type)
         {
            case QuestionListType.qltTest:
               #region Создание столбца Вопрос и столбца Балл
               
            {
               DCEAccessLib.DataColumnHeader Col1 = new DCEAccessLib.DataColumnHeader();
               DCEAccessLib.DataColumnHeader Col2 = new DCEAccessLib.DataColumnHeader();
               
               Col1.FieldName = "RContent";
               Col1.Text = "Вопрос";
               Col1.Width = 360;
               Col2.FieldName = "Points";
               Col2.Text = "Балл";
               Col2.Width = 360;
               
               dataList.Columns.AddRange(
                  new DCEAccessLib.DataColumnHeader[] {Col1, Col2}
                  );
            }
               #endregion
               break;

            case QuestionListType.qltWork:
               #region Создание столбца Вопрос
            {  
               DCEAccessLib.DataColumnHeader Col1 = new DCEAccessLib.DataColumnHeader();
               Col1.FieldName = "RContent";
               Col1.Text = "Вопрос";
               Col1.Width = 360;
               dataList.Columns.AddRange(
                  new DCEAccessLib.DataColumnHeader[] {Col1}
                  );
            }
               #endregion
               break;

            case QuestionListType.qltQuestionnaire:
               #region Создание столбца Вопрос
            {
               DCEAccessLib.DataColumnHeader Col1 = new DCEAccessLib.DataColumnHeader();
               Col1.FieldName = "RContent";
               Col1.Text = "Вопрос";
               Col1.Width = 360;
               dataList.Columns.AddRange(
                  new DCEAccessLib.DataColumnHeader[] {Col1}
                  );
            }
               #endregion
               break;
         }
      }

      private bool haveinit = false;
      public void SetParentNode(RecordEditNode node, string qualif)
      {
         haveinit = true;
         this.qualif = qualif;
         Node = node;
         
         if (!(Node is TestEditNode))
            this.menuItemImport.Visible = false;

         CourseEditNode coursenode = (CourseEditNode)Node.GetSpecifiedParentNode("DCECourseEditor.CourseEditNode");
         if ( coursenode != null)
         {
            if (coursenode.IsCourseActive)
            {
               this.menuItemCreate.Enabled = false;
               this.menuItemImport.Enabled = false;
               this.menuItemMoveDown.Enabled = false;
               this.menuItemMoveUp.Enabled = false;
               this.menuItemRemove.Enabled = false;

               this.DownButton.Enabled = false;
               this.UpButton.Enabled = false;
            }
         }
      }

      public QuestionList()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         if (DCEUser.CurrentUser.ReadOnlyCourses)
         {
            this.menuItemCreate.Enabled = false;
            this.menuItemMoveDown.Enabled = false;
            this.menuItemMoveUp.Enabled = false;
            this.menuItemRemove.Enabled = false;
            this.menuItemImport.Enabled = false;
            
            this.DownButton.Enabled = false;
            this.UpButton.Enabled = false;
         }

         this.DownButton.Text = "\u25bc";
         this.UpButton.Text = "\u25b2";
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
      
      private EditRowContent<string> GetRowContent()
      {
         return GetRowContent(Node);
      }

      private EditRowContent<string> GetRowContent(RecordEditNode node)
      {
         return node.GetEditContent(qualif);
      }

      public void RefreshData()
      {
         // Предполагается что вопрос хранится в поле DataStr, т.е. 
         // dataType MLEdit установлен как nvarchar255
         
         if (haveinit)
         {
            dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(

               "select *, " + 
            
               "dbo.GetContentAlt(Content, 'RU', 'EN') as RContent " + 

               "from TestQuestions " + 
         
               "where Test = '" + GetRowContent().editRow["id"] + "' " +
               
               "order by QOrder", "dbo.TestQuestions");

            dataView.Table = dataSet.Tables["dbo.TestQuestions"];
         }
      }

		#region Component Designer generated code
      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.contextMenuQuestions = new System.Windows.Forms.ContextMenu();
         this.menuItemCreate = new System.Windows.Forms.MenuItem();
         this.menuItemImport = new System.Windows.Forms.MenuItem();
         this.menuItemSeparator1 = new System.Windows.Forms.MenuItem();
         this.menuItemEdit = new System.Windows.Forms.MenuItem();
         this.menuItemRemove = new System.Windows.Forms.MenuItem();
         this.menuItemSeparator2 = new System.Windows.Forms.MenuItem();
         this.menuItemMoveUp = new System.Windows.Forms.MenuItem();
         this.menuItemMoveDown = new System.Windows.Forms.MenuItem();
         this.menuItemSeparator3 = new System.Windows.Forms.MenuItem();
         this.menuItemRefresh = new System.Windows.Forms.MenuItem();
         this.dataList = new DCEAccessLib.DataList();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.PanelLeft = new System.Windows.Forms.Panel();
         this.ButtonPanel = new System.Windows.Forms.Panel();
         this.DownButton = new System.Windows.Forms.Button();
         this.UpButton = new System.Windows.Forms.Button();
         this.PanelList = new System.Windows.Forms.Panel();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.PanelLeft.SuspendLayout();
         this.ButtonPanel.SuspendLayout();
         this.PanelList.SuspendLayout();
         this.SuspendLayout();
         // 
         // contextMenuQuestions
         // 
         this.contextMenuQuestions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                             this.menuItemCreate,
                                                                                             this.menuItemImport,
                                                                                             this.menuItemSeparator1,
                                                                                             this.menuItemEdit,
                                                                                             this.menuItemRemove,
                                                                                             this.menuItemSeparator2,
                                                                                             this.menuItemMoveUp,
                                                                                             this.menuItemMoveDown,
                                                                                             this.menuItemSeparator3,
                                                                                             this.menuItemRefresh});
         // 
         // menuItemCreate
         // 
         this.menuItemCreate.Index = 0;
         this.menuItemCreate.Text = "Создать";
         this.menuItemCreate.Click += new System.EventHandler(this.menuItemCreate_Click);
         // 
         // menuItemImport
         // 
         this.menuItemImport.Index = 1;
         this.menuItemImport.Text = "Импортировать список вопросов";
         this.menuItemImport.Click += new System.EventHandler(this.menuItemImport_Click);
         // 
         // menuItemSeparator1
         // 
         this.menuItemSeparator1.Index = 2;
         this.menuItemSeparator1.Text = "-";
         // 
         // menuItemEdit
         // 
         this.menuItemEdit.DefaultItem = true;
         this.menuItemEdit.Index = 3;
         this.menuItemEdit.Text = "Редактировать";
         this.menuItemEdit.Click += new System.EventHandler(this.menuItemEdit_Click);
         // 
         // menuItemRemove
         // 
         this.menuItemRemove.Index = 4;
         this.menuItemRemove.Text = "Удалить";
         this.menuItemRemove.Click += new System.EventHandler(this.menuItemRemove_Click);
         // 
         // menuItemSeparator2
         // 
         this.menuItemSeparator2.Index = 5;
         this.menuItemSeparator2.Text = "-";
         // 
         // menuItemMoveUp
         // 
         this.menuItemMoveUp.Index = 6;
         this.menuItemMoveUp.Text = "Переместить вверх";
         this.menuItemMoveUp.Click += new System.EventHandler(this.menuItemMoveUp_Click);
         // 
         // menuItemMoveDown
         // 
         this.menuItemMoveDown.Index = 7;
         this.menuItemMoveDown.Text = "Переместить вниз";
         this.menuItemMoveDown.Click += new System.EventHandler(this.menuItemMoveDown_Click);
         // 
         // menuItemSeparator3
         // 
         this.menuItemSeparator3.Index = 8;
         this.menuItemSeparator3.Text = "-";
         // 
         // menuItemRefresh
         // 
         this.menuItemRefresh.Index = 9;
         this.menuItemRefresh.Text = "Обновить";
         this.menuItemRefresh.Click += new System.EventHandler(this.menuItemRefresh_Click);
         // 
         // dataList
         // 
         this.dataList.AllowSorting = false;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.ContextMenu = this.contextMenuQuestions;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.HideSelection = false;
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(380, 448);
         this.dataList.TabIndex = 0;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.menuItemEdit_Click);
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
         // 
         // PanelLeft
         // 
         this.PanelLeft.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.ButtonPanel});
         this.PanelLeft.Dock = System.Windows.Forms.DockStyle.Left;
         this.PanelLeft.Name = "PanelLeft";
         this.PanelLeft.Size = new System.Drawing.Size(36, 448);
         this.PanelLeft.TabIndex = 1;
         // 
         // ButtonPanel
         // 
         this.ButtonPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
         this.ButtonPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.DownButton,
                                                                                  this.UpButton});
         this.ButtonPanel.Location = new System.Drawing.Point(6, 188);
         this.ButtonPanel.Name = "ButtonPanel";
         this.ButtonPanel.Size = new System.Drawing.Size(24, 72);
         this.ButtonPanel.TabIndex = 1;
         // 
         // DownButton
         // 
         this.DownButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
         this.DownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.DownButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.DownButton.Location = new System.Drawing.Point(0, 38);
         this.DownButton.Name = "DownButton";
         this.DownButton.Size = new System.Drawing.Size(24, 23);
         this.DownButton.TabIndex = 3;
         this.DownButton.Click += new System.EventHandler(this.menuItemMoveDown_Click);
         // 
         // UpButton
         // 
         this.UpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.UpButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.UpButton.Location = new System.Drawing.Point(0, 11);
         this.UpButton.Name = "UpButton";
         this.UpButton.Size = new System.Drawing.Size(24, 23);
         this.UpButton.TabIndex = 2;
         this.UpButton.Click += new System.EventHandler(this.menuItemMoveUp_Click);
         // 
         // PanelList
         // 
         this.PanelList.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.dataList});
         this.PanelList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.PanelList.Location = new System.Drawing.Point(36, 0);
         this.PanelList.Name = "PanelList";
         this.PanelList.Size = new System.Drawing.Size(380, 448);
         this.PanelList.TabIndex = 2;
         // 
         // QuestionList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.PanelList,
                                                                      this.PanelLeft});
         this.Name = "QuestionList";
         this.Size = new System.Drawing.Size(416, 448);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.PanelLeft.ResumeLayout(false);
         this.ButtonPanel.ResumeLayout(false);
         this.PanelList.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemCreate_Click(object sender, System.EventArgs e)
      {
         // создание нового вопроса
         
         if (haveinit)
         {
            if (!GetRowContent().IsNew)
            {
               QuestionEditNode node = new QuestionEditNode(Node, "", GetRowContent().editRow["id"].ToString(), type);
               node.Select();
            }
            else
            {
               string item = "Контент";
               switch (type)
               {
                  case QuestionListType.qltQuestionnaire :
                     item = "Анкету";
                     break;
                  case QuestionListType.qltTest :
                     item = "Тест";
                     break;
                  case QuestionListType.qltWork :
                     item = "Практическую работу";
                     break;
               }
               MessageBox.Show("Для создания вопроса необходимо сохранить " + item, "Сообщение");
            }
         }
      }

      private void menuItemEdit_Click(object sender, System.EventArgs e)
      {
         // редактирование вопроса

         if (haveinit)
         {
            if (this.dataList.SelectedItems.Count>0 && 
               this.dataList.SelectedItems[0].Tag!=null)
            {
               DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;

               bool neednew = true;
               foreach( NodeControl node in this.Node.Nodes)
               {
                  if (node.GetType() == typeof(QuestionEditNode) )
                  {
                     if (((RecordEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                     {
                        node.Select();
                        neednew = false;
                        break;
                     }
                  }
               }
               if (neednew)
               {
                  QuestionEditNode node = new QuestionEditNode(Node, 
                     row["id"].ToString(), 
                     row["Test"].ToString(),
                     type);

                  node.Select();
               }
            }
         }
      }

      private void menuItemRemove_Click(object sender, System.EventArgs e)
      {
         // удаление вопроса
         
         if (haveinit)
         {
            if (this.dataList.SelectedItems.Count>0 && 
               this.dataList.SelectedItems[0].Tag!=null)      
            {
               DataRowView row = (DataRowView)this.dataList.SelectedItems[0].Tag;
               if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данную запись?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
               {
                  DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from TestQuestions where id = '" + row["id"].ToString() + "'");
                  foreach (NodeControl node in Node.Nodes)
                  {
                     if (node.GetType() == typeof(QuestionEditNode))
                     {
                        if (((RecordEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
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
      }

      private void menuItemRefresh_Click(object sender, System.EventArgs e)
      {
         this.RefreshData();
      }

      private void SwapItems(int index1, int index2)
      {
         string strid = ((DataRowView)this.dataList.SelectedItems[0].Tag).Row["id"].ToString();

         DataRowView row1 = this.dataList.Items[index1].Tag as DataRowView;
         string id1 = row1["id"].ToString();
         int order1 = (int)(row1["QOrder"]);
         DataRowView row2 = this.dataList.Items[index2].Tag as DataRowView; 
         string id2 = row2["id"].ToString();
         int order2 = (int)(row2["QOrder"]);

         string query = 
            "UPDATE dbo.TestQuestions SET QOrder = " + order2 + " where id = '" + id1 + "' "+
            "UPDATE dbo.TestQuestions SET QOrder = " + order1 + " where id = '" + id2 + "' ";
               
         DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL(query);

         RefreshData();

         foreach (ListViewItem item in this.dataList.Items)
         {
            if (((DataRowView)item.Tag).Row["id"].ToString() == strid)
            {
               item.Selected = true;
               break;
            }
         }
      }
      
      private void menuItemMoveUp_Click(object sender, System.EventArgs e)
      {
         // перемещение вопроса вперед по порядку
         
         if (this.dataList.SelectedItems.Count>0 && this.dataList.SelectedItems[0].Tag!=null)
         {
            int curindex = this.dataList.SelectedItems[0].Index;

            // если вопрос можно переместить вверх на одну позицию
            if (curindex > 0)
               SwapItems(curindex, curindex-1);
         }
      }

      private void menuItemMoveDown_Click(object sender, System.EventArgs e)
      {
         // перемещение вопроса вниз по порядку
         if (this.dataList.SelectedItems.Count>0 && this.dataList.SelectedItems[0].Tag!=null)
         {
            int curindex = this.dataList.SelectedItems[0].Index;
            
            // если вопрос можно переместить вверх на одну позицию
            if (curindex < this.dataList.Items.Count-1)
               SwapItems(curindex, curindex+1);               
         }
      }

      private void menuItemImport_Click(object sender, System.EventArgs e)
      {
         if (haveinit)
         {
            if (!GetRowContent().IsNew)
            {
               ExportImportService service = new ExportImportService();
               service.ImportQuestions(Node.Id);
               RefreshData();
            }
            else
            {
               string item = "Контент";
               switch (type)
               {
                  case QuestionListType.qltQuestionnaire :
                     item = "Анкету";
                     break;
                  case QuestionListType.qltTest :
                     item = "Тест";
                     break;
                  case QuestionListType.qltWork :
                     item = "Практическую работу";
                     break;
               }
               MessageBox.Show("Для импорта вопросов необходимо сохранить " + item, "Сообщение");
            }
         }
      }
	}
}
