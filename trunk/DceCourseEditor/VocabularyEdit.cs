using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCECourseEditor
{
   public class VocabularyEditNode : NodeControl
   {
      public VocabularyEditNode(NodeControl parent, string courseid)
      : base (parent)
      {
         this.courseid = courseid;
      }
      
      private string courseid;
      public string CourseId
      {
         get { return courseid; }
      }

      public override UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new VocabularyEdit(this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         return "—ловарь";
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
   /// »зменение словар€
   /// </summary>
   public class VocabularyEdit : System.Windows.Forms.UserControl
   {
      #region Forms variables Definition
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;
      private DCEAccessLib.DataList SelectedTermsDataList;
      private DCEAccessLib.DataList AvaibleDataList;
      private System.Windows.Forms.Panel SelectedTermsPanel;
      private System.Windows.Forms.Panel AvaibleTermsPanel;
      private System.Windows.Forms.Label ContentLabel;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.MenuItem ACreateMenuItem;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem AEditMenuItem;
      private System.Windows.Forms.MenuItem ARemoveMenuItem;
      private System.Data.DataView SelectedDataView;
      private System.Data.DataView AvaibleDataView;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private System.Data.DataSet TermsDataSet;
      private System.Data.DataSet VocabularyDataSet;
      private System.Data.DataView TermsDataView;
      private System.Windows.Forms.MenuItem SRemoveMenuItem;
      private System.Windows.Forms.ContextMenu SelectedContextMenu;
      private System.Windows.Forms.MenuItem SEditMenuItem;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem AddMenuItem;
      private System.Windows.Forms.MenuItem AddAllMenuItem;
      private System.Windows.Forms.MenuItem SAllRemoveMenuItem;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.ContextMenu AvaibleContextMenu;
      #endregion

      private VocabularyEditNode Node;

      private DataSet AvaibleDataSet;
      
      public VocabularyEdit(VocabularyEditNode node)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         Node = node;

         if (DCEUser.CurrentUser.ReadOnlyCourses)
         {
            this.SRemoveMenuItem.Enabled = false;
            this.SAllRemoveMenuItem.Enabled = false;
            this.ACreateMenuItem.Enabled = false;
            this.ARemoveMenuItem.Enabled = false;
            this.AddMenuItem.Enabled = false;
            this.AddAllMenuItem.Enabled = false;
            this.SEditMenuItem.DefaultItem = true;
            this.AEditMenuItem.DefaultItem = true;

            this.SelectedTermsDataList.DoubleClick -= new System.EventHandler(this.RemoveButton_Click);
            this.SelectedTermsDataList.DoubleClick += new System.EventHandler(this.SEditMenuItem_Click);
            this.AvaibleDataList.DoubleClick -= new System.EventHandler(this.AddMenuItem_Click);
            this.AvaibleDataList.DoubleClick += new System.EventHandler(this.AEditMenuItem_Click);
         }

         RefreshData();
      }

      #region Disposing control code
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
      #endregion

      private void RefreshVocabulary()
      {
         // выбор всех терминов с их названи€ми
         VocabularyDataSet = DCEWebAccess.WebAccess.GetDataSet(

            "select v.id, v.Course, v.Term, t.id as id, t.Name as Name, t.Text as Text, " + 

            "IsNull(dbo.GetStrContentAlt(Name, 'RU', 'EN'), 'нет контента') " + 
            
            "as RName from dbo.Vocabulary v, dbo.VTerms t where v.Term = t.id",

            "Termins");

         SelectedDataView.Table = VocabularyDataSet.Tables["Termins"];
         
         // отображать только термины вход€щие в текущий курс
         SelectedDataView.RowFilter = "Course = '" + Node.CourseId + "'";
         
         // выбор всех терминов которых нет в текущем курсе
         AvaibleDataSet = DCEWebAccess.WebAccess.GetDataSet(

            "select *, IsNull(dbo.GetStrContentAlt(Name, 'RU', 'EN'), 'нет контента') " + 
            
            "as RName from dbo.VTerms where id not in (select Term from Vocabulary where " + 
            
            "Course = '" + Node.CourseId + "')",

            "Termins");

         AvaibleDataView.Table = AvaibleDataSet.Tables["Termins"];

         // обновление списков
         this.AvaibleDataList.UpdateList();
         this.SelectedTermsDataList.UpdateList();
      }

      private void UpdateVocabulary()
      {
         DCEAccessLib.DCEWebAccess.WebAccess.UpdateDataSet("select * from Vocabulary", "Termins", ref VocabularyDataSet);

         RefreshVocabulary();
      }

      private void RefreshTerms()
      {
         // выбор всех терминов
         TermsDataSet = DCEWebAccess.WebAccess.GetDataSet(

            "select * from dbo.VTerms",

            "Terms");
         
         TermsDataView.Table = TermsDataSet.Tables["Terms"];
         
         RefreshVocabulary();
      }

      private void UpdateTerms()
      {
         DCEAccessLib.DCEWebAccess.WebAccess.UpdateDataSet("select * from dbo.VTerms", "Terms", ref TermsDataSet);

         RefreshTerms();
      }

      public void RefreshData()
      {
         RefreshTerms();
      }

		#region Component Designer generated code
      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.SEditMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.SRemoveMenuItem = new System.Windows.Forms.MenuItem();
         this.SAllRemoveMenuItem = new System.Windows.Forms.MenuItem();
         this.ACreateMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.AEditMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.ARemoveMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.AddMenuItem = new System.Windows.Forms.MenuItem();
         this.AddAllMenuItem = new System.Windows.Forms.MenuItem();
         this.SelectedTermsDataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.SelectedContextMenu = new System.Windows.Forms.ContextMenu();
         this.SelectedDataView = new System.Data.DataView();
         this.AvaibleDataView = new System.Data.DataView();
         this.AvaibleDataList = new DCEAccessLib.DataList();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.AvaibleContextMenu = new System.Windows.Forms.ContextMenu();
         this.SelectedTermsPanel = new System.Windows.Forms.Panel();
         this.ContentLabel = new System.Windows.Forms.Label();
         this.AvaibleTermsPanel = new System.Windows.Forms.Panel();
         this.label1 = new System.Windows.Forms.Label();
         this.VocabularyDataSet = new System.Data.DataSet();
         this.TermsDataSet = new System.Data.DataSet();
         this.TermsDataView = new System.Data.DataView();
         ((System.ComponentModel.ISupportInitialize)(this.SelectedDataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.AvaibleDataView)).BeginInit();
         this.SelectedTermsPanel.SuspendLayout();
         this.AvaibleTermsPanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.VocabularyDataSet)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.TermsDataSet)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.TermsDataView)).BeginInit();
         this.SuspendLayout();
         // 
         // SelectedTermsDataList
         // 
         this.SelectedTermsDataList.AllowSorting = true;
         this.SelectedTermsDataList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.SelectedTermsDataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                                            this.dataColumnHeader1});
         this.SelectedTermsDataList.ContextMenu = this.SelectedContextMenu;
         this.SelectedTermsDataList.DataView = this.SelectedDataView;
         this.SelectedTermsDataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.SelectedTermsDataList.FullRowSelect = true;
         this.SelectedTermsDataList.GridLines = true;
         this.SelectedTermsDataList.HideSelection = false;
         this.SelectedTermsDataList.Location = new System.Drawing.Point(0, 20);
         this.SelectedTermsDataList.Name = "SelectedTermsDataList";
         this.SelectedTermsDataList.Size = new System.Drawing.Size(312, 376);
         this.SelectedTermsDataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.SelectedTermsDataList.TabIndex = 0;
         this.SelectedTermsDataList.View = System.Windows.Forms.View.Details;
         this.SelectedTermsDataList.DoubleClick += new System.EventHandler(this.RemoveButton_Click);
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "RName";
         this.dataColumnHeader1.Text = "“ермин";
         this.dataColumnHeader1.Width = 263;
         // 
         // SelectedContextMenu
         // 
         this.SelectedContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                            this.SEditMenuItem,
                                                                                            this.menuItem3,
                                                                                            this.SRemoveMenuItem,
                                                                                            this.SAllRemoveMenuItem});
         // 
         // SEditMenuItem
         // 
         this.SEditMenuItem.Index = 0;
         this.SEditMenuItem.Text = "–едактировать";
         this.SEditMenuItem.Click += new System.EventHandler(this.SEditMenuItem_Click);
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 1;
         this.menuItem3.Text = "-";
         // 
         // SRemoveMenuItem
         // 
         this.SRemoveMenuItem.DefaultItem = true;
         this.SRemoveMenuItem.Index = 2;
         this.SRemoveMenuItem.Text = "”далить из словар€";
         this.SRemoveMenuItem.Click += new System.EventHandler(this.RemoveButton_Click);
         // 
         // SAllRemoveMenuItem
         // 
         this.SAllRemoveMenuItem.Index = 3;
         this.SAllRemoveMenuItem.Text = "”далить все из словар€";
         this.SAllRemoveMenuItem.Click += new System.EventHandler(this.RemoveAllButton_Click);
         // 
         // AvaibleDataList
         // 
         this.AvaibleDataList.AllowSorting = true;
         this.AvaibleDataList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.AvaibleDataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                                      this.dataColumnHeader2});
         this.AvaibleDataList.ContextMenu = this.AvaibleContextMenu;
         this.AvaibleDataList.DataView = this.AvaibleDataView;
         this.AvaibleDataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.AvaibleDataList.FullRowSelect = true;
         this.AvaibleDataList.GridLines = true;
         this.AvaibleDataList.HideSelection = false;
         this.AvaibleDataList.Location = new System.Drawing.Point(0, 20);
         this.AvaibleDataList.Name = "AvaibleDataList";
         this.AvaibleDataList.Size = new System.Drawing.Size(328, 376);
         this.AvaibleDataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.AvaibleDataList.TabIndex = 1;
         this.AvaibleDataList.View = System.Windows.Forms.View.Details;
         this.AvaibleDataList.DoubleClick += new System.EventHandler(this.AddMenuItem_Click);
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "RName";
         this.dataColumnHeader2.Text = "“ермин";
         this.dataColumnHeader2.Width = 263;
         // 
         // AvaibleContextMenu
         // 
         this.AvaibleContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                           this.ACreateMenuItem,
                                                                                           this.menuItem2,
                                                                                           this.AEditMenuItem,
                                                                                           this.menuItem4,
                                                                                           this.ARemoveMenuItem,
                                                                                           this.menuItem1,
                                                                                           this.AddMenuItem,
                                                                                           this.AddAllMenuItem});
         // 
         // ACreateMenuItem
         // 
         this.ACreateMenuItem.Index = 0;
         this.ACreateMenuItem.Text = "—оздать";
         this.ACreateMenuItem.Click += new System.EventHandler(this.ACreateMenuItem_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 1;
         this.menuItem2.Text = "-";
         // 
         // AEditMenuItem
         // 
         this.AEditMenuItem.Index = 2;
         this.AEditMenuItem.Text = "–едактировать";
         this.AEditMenuItem.Click += new System.EventHandler(this.AEditMenuItem_Click);
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 3;
         this.menuItem4.Text = "-";
         // 
         // ARemoveMenuItem
         // 
         this.ARemoveMenuItem.Index = 4;
         this.ARemoveMenuItem.Text = "”далить из базы";
         this.ARemoveMenuItem.Click += new System.EventHandler(this.ARemoveMenuItem_Click);
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 5;
         this.menuItem1.Text = "-";
         // 
         // AddMenuItem
         // 
         this.AddMenuItem.DefaultItem = true;
         this.AddMenuItem.Index = 6;
         this.AddMenuItem.Text = "ƒобавить в словарь";
         this.AddMenuItem.Click += new System.EventHandler(this.AddMenuItem_Click);
         // 
         // AddAllMenuItem
         // 
         this.AddAllMenuItem.Index = 7;
         this.AddAllMenuItem.Text = "ƒобавить все в словарь";
         this.AddAllMenuItem.Click += new System.EventHandler(this.AddAllButton_Click);
         // 
         // SelectedTermsPanel
         // 
         this.SelectedTermsPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                         this.SelectedTermsDataList,
                                                                                         this.ContentLabel});
         this.SelectedTermsPanel.Dock = System.Windows.Forms.DockStyle.Left;
         this.SelectedTermsPanel.Name = "SelectedTermsPanel";
         this.SelectedTermsPanel.Size = new System.Drawing.Size(312, 396);
         this.SelectedTermsPanel.TabIndex = 2;
         // 
         // ContentLabel
         // 
         this.ContentLabel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.ContentLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ContentLabel.Dock = System.Windows.Forms.DockStyle.Top;
         this.ContentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.ContentLabel.ForeColor = System.Drawing.SystemColors.Info;
         this.ContentLabel.Name = "ContentLabel";
         this.ContentLabel.Size = new System.Drawing.Size(312, 20);
         this.ContentLabel.TabIndex = 168;
         this.ContentLabel.Text = "¬ыбранные термины";
         this.ContentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // AvaibleTermsPanel
         // 
         this.AvaibleTermsPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.AvaibleDataList,
                                                                                        this.label1});
         this.AvaibleTermsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.AvaibleTermsPanel.Location = new System.Drawing.Point(312, 0);
         this.AvaibleTermsPanel.Name = "AvaibleTermsPanel";
         this.AvaibleTermsPanel.Size = new System.Drawing.Size(328, 396);
         this.AvaibleTermsPanel.TabIndex = 3;
         // 
         // label1
         // 
         this.label1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label1.Dock = System.Windows.Forms.DockStyle.Top;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label1.ForeColor = System.Drawing.SystemColors.Info;
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(328, 20);
         this.label1.TabIndex = 168;
         this.label1.Text = "ƒоступные термины";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // VocabularyDataSet
         // 
         this.VocabularyDataSet.DataSetName = "NewDataSet";
         this.VocabularyDataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
         // 
         // TermsDataSet
         // 
         this.TermsDataSet.DataSetName = "NewDataSet";
         this.TermsDataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
         // 
         // VocabularyEdit
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.AvaibleTermsPanel,
                                                                      this.SelectedTermsPanel});
         this.Name = "VocabularyEdit";
         this.Size = new System.Drawing.Size(640, 396);
         this.Layout += new System.Windows.Forms.LayoutEventHandler(this.VocabularyEdit_Layout);
         ((System.ComponentModel.ISupportInitialize)(this.SelectedDataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.AvaibleDataView)).EndInit();
         this.SelectedTermsPanel.ResumeLayout(false);
         this.AvaibleTermsPanel.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.VocabularyDataSet)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.TermsDataSet)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.TermsDataView)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void AddTermin(string termid)
      {
         // добавить термин в словарь курса

         DataRowView row = this.SelectedDataView.AddNew();
         row["id"] = System.Guid.NewGuid();
         row["Course"] = Node.CourseId;
         row["Term"] = termid;
         row.EndEdit();
      }

      private void SuspendPaint(bool suspend)
      {
         AvaibleDataList.SuspendListChange = suspend;
         SelectedTermsDataList.SuspendListChange = suspend;
      }

      private void AddMenuItem_Click(object sender, System.EventArgs e)
      {
         // добавить выделенные термины в словарь курса
         
         if (AvaibleDataList.SelectedItems.Count != 0)
         {
            SuspendPaint(true);

            foreach (ListViewItem item in AvaibleDataList.SelectedItems)
            {
               if (item.Tag != null)
                  AddTermin(((DataRowView)item.Tag)["id"].ToString());
            }
            
            SuspendPaint(false);

            UpdateVocabulary();
         }
         
      }

      private void RemoveTermin(DataRowView row)
      {
         // удалить термин из словар€ курса

         row.Delete();
      }

      private void RemoveButton_Click(object sender, System.EventArgs e)
      {
         // удалить выделенные термины из словар€ курса

         if (SelectedTermsDataList.SelectedItems.Count != 0)
         {
            SuspendPaint(true);

            int count = SelectedTermsDataList.SelectedItems.Count;

            string[] idents = new string[count];

            for(int i=0; i<count; i++)
            {
               ListViewItem item = SelectedTermsDataList.SelectedItems[i];
               idents[i] = (string)((DataRowView)item.Tag)["id"].ToString();
            }

            SelectedDataView.Sort = "id";

            for (int i=0; i<idents.Length;i++)
            {
               DataRowView[] deletedrows = SelectedDataView.FindRows(idents[i]);

               deletedrows[0].Delete();
            }

            SuspendPaint(false);

            UpdateVocabulary();
         }
      }

      private void AddAllButton_Click(object sender, System.EventArgs e)
      {
         // добавить все термины в словарь курса

         if (AvaibleDataList.Items.Count != 0)
         {
            SuspendPaint(true);

            foreach (ListViewItem item in AvaibleDataList.Items)
            {
               if (item.Tag != null)
                  AddTermin(((DataRowView)item.Tag)["id"].ToString());
            }
            
            SuspendPaint(false);

            UpdateVocabulary();
         }
      }

      private void RemoveAllButton_Click(object sender, System.EventArgs e)
      {
         // удалить все термины из словар€ курса

         SuspendPaint(true);

         int count = SelectedDataView.Count;
         for (int i=0; i<count; i++)
         {
            SelectedDataView.Delete(0);
         }
         
         SuspendPaint(false);

         UpdateVocabulary();
      }

      private void ACreateMenuItem_Click(object sender, System.EventArgs e)
      {
         // *создание нового термина*
      
         DataRowView newrow = TermsDataView.AddNew();
         newrow["id"] = System.Guid.NewGuid();
         newrow["Name"] = System.Guid.NewGuid();
         newrow["Text"] = System.Guid.NewGuid();

         // редактирование модальным диалогом
         TermForm form = new TermForm(newrow);

         if (form.ShowDialog() != DialogResult.OK)
         {
            newrow.CancelEdit();
         }
         else
         {
            newrow.EndEdit();

            BatchUpdateProc(form, true);
            
            UpdateVocabulary();

            RefreshTerms();
         }
      }

      private void BatchUpdateProc(TermForm form, bool all)
      {
         System.Collections.ArrayList  updates = new  System.Collections.ArrayList();

         DCEAccessLib.DataSetBatchUpdate dataSet = new DCEAccessLib.DataSetBatchUpdate();

         if (all)
         {
            dataSet.sql = "select * from dbo.VTerms";
            dataSet.tableName = "Terms";
            dataSet.dataSet = this.TermsDataSet;
            updates.Add(dataSet);
         }
         updates.AddRange(form.GetDialogDataSets());               
            
         string[] sqls = new string[updates.Count];
         string[] tables = new string[updates.Count];
         DataSet[] datasets = new DataSet[updates.Count];

         for (int i=0; i<updates.Count;i++)
         {
            sqls[i] = ((DCEAccessLib.DataSetBatchUpdate)updates[i]).sql;
            tables[i] = ((DCEAccessLib.DataSetBatchUpdate)updates[i]).tableName;
            datasets[i] = ((DCEAccessLib.DataSetBatchUpdate)updates[i]).dataSet;
         }

         DCEAccessLib.DCEWebAccess.WebAccess.UpdateDataSets(sqls,tables,datasets);
      }

      private void AEditMenuItem_Click(object sender, System.EventArgs e)
      {
         // *редактирование термина*

         if (AvaibleDataList.SelectedItems.Count != 0 && AvaibleDataList.SelectedItems[0].Tag != null)
         {
            DataRowView row = AvaibleDataList.SelectedItems[0].Tag as DataRowView;

            // редактирование модальным диалогом
            TermForm form = new TermForm(row);

            if (form.ShowDialog() == DialogResult.OK)
            {
               BatchUpdateProc(form, false);

               UpdateVocabulary();

               RefreshTerms();
            }
         }
      }

      private void ARemoveMenuItem_Click(object sender, System.EventArgs e)
      {
         // *удаление термина*

         if (AvaibleDataList.SelectedItems.Count > 0) 
         {
            // messagebox ...         
            if (MessageBox.Show("”далить выбранные термины?", "—ообщение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               foreach (ListViewItem item in AvaibleDataList.SelectedItems)
               {
                  if (item.Tag != null)
                  {
                     DataRowView itemrow = (DataRowView)item.Tag;

                     TermsDataView.RowFilter = "id = '" + itemrow["id"].ToString() + "'";
               
                     foreach(DataRowView row in TermsDataView)
                        row.Delete();
                  }
               }

               UpdateTerms();
            }
         }
      }
      
      private void SEditMenuItem_Click(object sender, System.EventArgs e)
      {
         // *редактирование термина*

         if (SelectedTermsDataList.SelectedItems.Count != 0 && SelectedTermsDataList.SelectedItems[0].Tag != null)
         {
            DataRowView row = SelectedTermsDataList.SelectedItems[0].Tag as DataRowView;

            // редактирование модальным диалогом
            TermForm form = new TermForm(row);

            if (form.ShowDialog() == DialogResult.OK)
            {
               BatchUpdateProc(form, false);

               UpdateVocabulary();

               RefreshTerms();
            }
         }      
      }

      private void VocabularyEdit_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
      {
         this.SelectedTermsPanel.Width = this.Size.Width/2;
         this.SelectedTermsDataList.Columns[0].Width = this.SelectedTermsDataList.Width - 15;
         this.AvaibleDataList.Columns[0].Width = this.AvaibleDataList.Width - 15;
      }
   }
}
