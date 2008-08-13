using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCECourseEditor
{
   /// <summary>
   /// Редктирование библиотеки
   /// </summary>
   public class LibraryEdit : System.Windows.Forms.UserControl
   {
      #region Forms definitions
      private System.Windows.Forms.Panel LangPanel;
      private DCEAccessLib.LangSwitcher LangSwitcher;
      private System.Windows.Forms.Label TitleLabel;
      private DCEAccessLib.MLEdit NameTextBox;
      private System.Windows.Forms.Label NameLabel;
      private System.Windows.Forms.Panel TotalPanel;
      private System.Windows.Forms.Panel NamePanel;
      private System.Windows.Forms.Panel LinksPanel;
      private System.Windows.Forms.Label ContentLabel;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.ContextMenu LinksContextMenu;
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;
      private DCEAccessLib.DataList LinksDataList;
      private System.Windows.Forms.MenuItem CreateMenuItem;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem EditMenuItem;
      private System.Windows.Forms.MenuItem RemoveMenuItem;
      private System.Windows.Forms.Panel ButtonPanel;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Button buttonRemove;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      #endregion

      private LibraryEditNode Node;
      private int MaxIndex = 1;

      public LibraryEdit(LibraryEditNode node)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         Node = node;

         this.SaveButton.Enabled = DCEUser.CurrentUser.EditableCourses;

         CourseEditNode coursenode = (CourseEditNode)Node.GetSpecifiedParentNode("DCECourseEditor.CourseEditNode");
         if ( coursenode != null)
            this.LangSwitcher.Language = coursenode.DefLang;

         if (Node.RootLibrary)
            this.buttonRemove.Visible = true;  
         
         if (Node.RootLibrary && !Node.IsNew)
            this.buttonRemove.Enabled = true;  

         if (DCEUser.CurrentUser.ReadOnlyCourses)
         {
            this.CreateMenuItem.Enabled = false;
            this.RemoveMenuItem.Enabled = false;
            this.buttonRemove.Enabled = false;
         }

         this.NameTextBox.SetParentNode(Node);
         
         this.Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(this.RebindControls);
         this.Node.OnUpdateDataSet += new RecordEditNode.EnumDataSetsHandler(this.UpdateMembers);

         this.LangSwitcher.LanguageChanged += new LangSwitcher.SwitchLangHandler(this.OnSwitchLang);

         this.RebindControls();
      }
      
      private void OnSwitchLang(int NewLang)
      {
         this.dataView.RowFilter = "Lang = " + NewLang;
      }

      private string GetSqlQuery()
      {
         return "select * from dbo.Content where eid = '" + 
            
            Node.EditRow["Content"].ToString() + "'" + 
            
            " order by COrder";
      }

      public void RefreshData()
      {
         this.dataSet = DCEWebAccess.WebAccess.GetDataSet(GetSqlQuery(), "dbo.Content");
         this.dataView.Table = this.dataSet.Tables["dbo.Content"];
         this.dataView.RowFilter = "Lang = " + this.LangSwitcher.Language;
      }

      void UpdateMaterials()
      {
         DCEAccessLib.DCEWebAccess.WebAccess.UpdateDataSet(GetSqlQuery(), "dbo.Content", ref this.dataSet);
      }
      
      bool UpdateMembers(string TransactionId, ref DCEAccessLib.DataSetBatchUpdate BatchDataSet)
      {
         BatchDataSet.sql = GetSqlQuery();
         
         BatchDataSet.tableName = "dbo.Content";
         
         BatchDataSet.dataSet = this.dataSet;

         return true;
      }
      
      public void RebindControls()
      {
         this.NameTextBox.DataBindings.Clear();
         this.NameTextBox.DataBindings.Add("eId", Node.EditRow, "Name");
         
         this.RefreshData();
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
         this.LangPanel = new System.Windows.Forms.Panel();
         this.LangSwitcher = new DCEAccessLib.LangSwitcher();
         this.TotalPanel = new System.Windows.Forms.Panel();
         this.LinksPanel = new System.Windows.Forms.Panel();
         this.LinksDataList = new DCEAccessLib.DataList();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.LinksContextMenu = new System.Windows.Forms.ContextMenu();
         this.CreateMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.EditMenuItem = new System.Windows.Forms.MenuItem();
         this.RemoveMenuItem = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.ContentLabel = new System.Windows.Forms.Label();
         this.NamePanel = new System.Windows.Forms.Panel();
         this.NameLabel = new System.Windows.Forms.Label();
         this.NameTextBox = new DCEAccessLib.MLEdit();
         this.TitleLabel = new System.Windows.Forms.Label();
         this.dataSet = new System.Data.DataSet();
         this.ButtonPanel = new System.Windows.Forms.Panel();
         this.buttonRemove = new System.Windows.Forms.Button();
         this.SaveButton = new System.Windows.Forms.Button();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.LangPanel.SuspendLayout();
         this.TotalPanel.SuspendLayout();
         this.LinksPanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.NamePanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.ButtonPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // LangPanel
         // 
         this.LangPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.LangPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.LangSwitcher});
         this.LangPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.LangPanel.Name = "LangPanel";
         this.LangPanel.Size = new System.Drawing.Size(560, 40);
         this.LangPanel.TabIndex = 1;
         // 
         // LangSwitcher
         // 
         this.LangSwitcher.Location = new System.Drawing.Point(8, 8);
         this.LangSwitcher.Name = "LangSwitcher";
         this.LangSwitcher.Size = new System.Drawing.Size(172, 24);
         this.LangSwitcher.TabIndex = 1;
         this.LangSwitcher.TextLabel = "Язык";
         // 
         // TotalPanel
         // 
         this.TotalPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.LinksPanel,
                                                                                 this.NamePanel,
                                                                                 this.TitleLabel});
         this.TotalPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.TotalPanel.Location = new System.Drawing.Point(0, 40);
         this.TotalPanel.Name = "TotalPanel";
         this.TotalPanel.Size = new System.Drawing.Size(560, 288);
         this.TotalPanel.TabIndex = 2;
         // 
         // LinksPanel
         // 
         this.LinksPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.LinksDataList,
                                                                                 this.ContentLabel});
         this.LinksPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.LinksPanel.Location = new System.Drawing.Point(0, 64);
         this.LinksPanel.Name = "LinksPanel";
         this.LinksPanel.Size = new System.Drawing.Size(560, 224);
         this.LinksPanel.TabIndex = 182;
         // 
         // LinksDataList
         // 
         this.LinksDataList.AllowSorting = true;
         this.LinksDataList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.LinksDataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                                    this.dataColumnHeader2});
         this.LinksDataList.ContextMenu = this.LinksContextMenu;
         this.LinksDataList.DataView = this.dataView;
         this.LinksDataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.LinksDataList.FullRowSelect = true;
         this.LinksDataList.GridLines = true;
         this.LinksDataList.HideSelection = false;
         this.LinksDataList.Location = new System.Drawing.Point(0, 20);
         this.LinksDataList.MultiSelect = false;
         this.LinksDataList.Name = "LinksDataList";
         this.LinksDataList.Size = new System.Drawing.Size(560, 204);
         this.LinksDataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.LinksDataList.TabIndex = 169;
         this.LinksDataList.View = System.Windows.Forms.View.Details;
         this.LinksDataList.DoubleClick += new System.EventHandler(this.EditMenuItem_Click);
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "dataStr";
         this.dataColumnHeader2.Text = "Ссылка";
         this.dataColumnHeader2.Width = 350;
         // 
         // LinksContextMenu
         // 
         this.LinksContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                         this.CreateMenuItem,
                                                                                         this.menuItem2,
                                                                                         this.EditMenuItem,
                                                                                         this.RemoveMenuItem});
         // 
         // CreateMenuItem
         // 
         this.CreateMenuItem.Index = 0;
         this.CreateMenuItem.Text = "Создать";
         this.CreateMenuItem.Click += new System.EventHandler(this.CreateMenuItem_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 1;
         this.menuItem2.Text = "-";
         // 
         // EditMenuItem
         // 
         this.EditMenuItem.DefaultItem = true;
         this.EditMenuItem.Index = 2;
         this.EditMenuItem.Text = "Редактировать";
         this.EditMenuItem.Click += new System.EventHandler(this.EditMenuItem_Click);
         // 
         // RemoveMenuItem
         // 
         this.RemoveMenuItem.Index = 3;
         this.RemoveMenuItem.Text = "Удалить";
         this.RemoveMenuItem.Click += new System.EventHandler(this.RemoveMenuItem_Click);
         // 
         // ContentLabel
         // 
         this.ContentLabel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.ContentLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ContentLabel.Dock = System.Windows.Forms.DockStyle.Top;
         this.ContentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.ContentLabel.ForeColor = System.Drawing.SystemColors.Info;
         this.ContentLabel.Name = "ContentLabel";
         this.ContentLabel.Size = new System.Drawing.Size(560, 20);
         this.ContentLabel.TabIndex = 168;
         this.ContentLabel.Text = "Ссылки";
         this.ContentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // NamePanel
         // 
         this.NamePanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.NameLabel,
                                                                                this.NameTextBox});
         this.NamePanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.NamePanel.Location = new System.Drawing.Point(0, 20);
         this.NamePanel.Name = "NamePanel";
         this.NamePanel.Size = new System.Drawing.Size(560, 44);
         this.NamePanel.TabIndex = 181;
         // 
         // NameLabel
         // 
         this.NameLabel.Location = new System.Drawing.Point(8, 12);
         this.NameLabel.Name = "NameLabel";
         this.NameLabel.Size = new System.Drawing.Size(100, 20);
         this.NameLabel.TabIndex = 179;
         this.NameLabel.Text = "Название";
         // 
         // NameTextBox
         // 
         this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.NameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.NameTextBox.CaptionLabel = null;
         this.NameTextBox.DataType = DCEAccessLib.dataFieldType.nvarchar255;
         this.NameTextBox.eId = "";
         this.NameTextBox.EntType = DCEAccessLib.ContentType._string;
         this.NameTextBox.LanguageSwitcher = this.LangSwitcher;
         this.NameTextBox.Location = new System.Drawing.Point(128, 8);
         this.NameTextBox.MaxLength = 100;
         this.NameTextBox.Name = "NameTextBox";
         this.NameTextBox.Size = new System.Drawing.Size(424, 20);
         this.NameTextBox.TabIndex = 180;
         this.NameTextBox.Text = "";
         // 
         // TitleLabel
         // 
         this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
         this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
         this.TitleLabel.Name = "TitleLabel";
         this.TitleLabel.Size = new System.Drawing.Size(560, 20);
         this.TitleLabel.TabIndex = 172;
         this.TitleLabel.Text = "Основные";
         this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
         // 
         // ButtonPanel
         // 
         this.ButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ButtonPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.buttonRemove,
                                                                                  this.SaveButton,
                                                                                  this.buttonCancel});
         this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.ButtonPanel.Location = new System.Drawing.Point(0, 284);
         this.ButtonPanel.Name = "ButtonPanel";
         this.ButtonPanel.Size = new System.Drawing.Size(560, 44);
         this.ButtonPanel.TabIndex = 3;
         // 
         // buttonRemove
         // 
         this.buttonRemove.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
         this.buttonRemove.Enabled = false;
         this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonRemove.Location = new System.Drawing.Point(8, 6);
         this.buttonRemove.Name = "buttonRemove";
         this.buttonRemove.Size = new System.Drawing.Size(132, 28);
         this.buttonRemove.TabIndex = 3;
         this.buttonRemove.Text = "Удалить библиотеку";
         this.buttonRemove.Visible = false;
         this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
         // 
         // SaveButton
         // 
         this.SaveButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SaveButton.Location = new System.Drawing.Point(350, 6);
         this.SaveButton.Name = "SaveButton";
         this.SaveButton.Size = new System.Drawing.Size(96, 28);
         this.SaveButton.TabIndex = 2;
         this.SaveButton.Text = "Сохранить";
         this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
         // 
         // buttonCancel
         // 
         this.buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonCancel.Location = new System.Drawing.Point(454, 6);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(96, 28);
         this.buttonCancel.TabIndex = 2;
         this.buttonCancel.Text = "Отменить";
         this.buttonCancel.Click += new System.EventHandler(this.ExitButton_Click);
         // 
         // LibraryEdit
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.ButtonPanel,
                                                                      this.TotalPanel,
                                                                      this.LangPanel});
         this.Name = "LibraryEdit";
         this.Size = new System.Drawing.Size(560, 328);
         this.LangPanel.ResumeLayout(false);
         this.TotalPanel.ResumeLayout(false);
         this.LinksPanel.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.NamePanel.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ButtonPanel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         // сохранить изменения
         this.Node.EndEdit(false, false);

         if (Node.RootLibrary)
         {
            string text = DCEWebAccess.WebAccess.GetString(
               
               "select dbo.GetStrContentAlt(Name, 'RU', 'EN') " + 
               
               "as RName from dbo.Themes where id = '" + this.Node.Id + "'");

            if (text != null)
               Node.ThemeName = text;
         }

         this.Node.CreateChilds();
         this.Node.IsNodeDirty = false;

         if (Node.RootLibrary && !Node.IsNew && !DCEUser.CurrentUser.ReadOnlyCourses)
            this.buttonRemove.Enabled = true;  
      }

      private void ExitButton_Click(object sender, System.EventArgs e)
      {
         // закрыть окно
         if (this.Node.IsNew)
            this.Node.Dispose();
         else
            this.Node.EndEdit(true, false);
         
         this.Node.IsNodeDirty = false;
      }

      private void CreateMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.Node.IsNew)
            MessageBox.Show("Для добавления ссылки необходимо вначале сохранить тему библиотеки!","Сообщение",MessageBoxButtons.OK);
         else
         {
            LinksForm MatForm = new LinksForm("", "Выбор ссылки");
            
            if (MatForm.ShowDialog() == DialogResult.OK)
            {
               DCEWebAccess.WebAccess.ExecSQL(
                     
                  "declare @maxcorder int " + 

                  "set @maxcorder = (select 1+isnull(max(COrder), 0) from Content) " +
                     
                  "INSERT INTO dbo.Content " + 

                  " (id, eid, Type, Lang, DataStr, COrder) " + 

                  " VALUES ('" + System.Guid.NewGuid().ToString() + "', '" + 

                  this.Node.EditRow["Content"].ToString() + "', " + 

                  (int)ContentType._url + ", " + this.LangSwitcher.Language + ",'" + 

                  MatForm.Url + "', @maxcorder )");
               
               RefreshData();

               this.LinksDataList.UpdateList();
            }
         }
      }

      private void EditMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.LinksDataList.SelectedItems.Count != 0 
            && this.LinksDataList.SelectedItems[0].Tag != null)
         {
            DataRowView row = (DataRowView)this.LinksDataList.SelectedItems[0].Tag;
            
            string path = row["DataStr"].ToString();
            LinksForm MatForm = new LinksForm(path, "Выбор ссылки");

            if (MatForm.ShowDialog() == DialogResult.OK)
            {
               this.Node.IsNodeDirty = true;
               
               // WARNING
               row["DataStr"] = MatForm.Url;
            }
            
            this.LinksDataList.UpdateList();
         }
      }

      private void RemoveMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.LinksDataList.SelectedItems.Count>0 && this.LinksDataList.SelectedItems[0].Tag!=null)      
         {
            DataRowView row = (DataRowView)this.LinksDataList.SelectedItems[0].Tag;
            if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данную запись?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from dbo.Content where id = '" + row["id"].ToString() + "'");
               
               this.RefreshData();
               
               this.LinksDataList.UpdateList();

            }
         }
      }

      private void buttonRemove_Click(object sender, System.EventArgs e)
      {
         if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить библиотеку?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from Themes where id = '" + this.Node.Id + "'");

            this.Node.Dispose();
         }
      }
   }

   /// <summary>
   /// Нода "Библиотека"
   /// </summary>
   public class LibraryEditNode : ThemeEditNode
   {
      public LibraryEditNode(NodeControl parent, string libraryId, string parentCourseId, string libraryName, bool rootlibrary)
         : base(parent, libraryId, parentCourseId, ThemeEditNodeTypes.tenMain, libraryName, true)
      {
         this.rootlibrary = rootlibrary;
      }
      
      private bool rootlibrary;
      public bool RootLibrary
      { 
         get { return rootlibrary; } 
      }
      public override String GetCaption()
      {
         string caption;
         
         caption = "Библиотека : " + this.ThemeName;
         
         if (this.IsNodeDirty)
            caption += "*";
         
         return caption;
      }

      public override UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new LibraryEdit(this);
         }
         
         if (needRefresh)
         {
            ((LibraryEdit)this.fControl).RefreshData();
            needRefresh = false;
         }

         return this.fControl;
      }
      
      /// <summary>
      /// Initialize new record for editing (EditRow)
      /// </summary>
      protected override void InitNewRecord()
      {
         // filling all non-nullable fields
         EditRow ["id"] = System.Guid.NewGuid().ToString();
         EditRow ["Type"] = (int)ThemeType.library;
         EditRow ["Name"] = System.Guid.NewGuid().ToString();
         EditRow ["TOrder"]  = 0; // вычисляется в триггере
         EditRow ["Content"]  = System.Guid.NewGuid().ToString();
         EditRow ["Duration"]  = 1; // неиспользуемое поле 
         EditRow ["Mandatory"] = true; // неиспользуемое поле 
      }
   }
}