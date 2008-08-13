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
	/// Редактирование темы
	/// </summary>
	public class ThemeEdit : System.Windows.Forms.UserControl
	{
      #region Form variables
      private System.Windows.Forms.Panel LangPanel;
      private DCEAccessLib.LangSwitcher LangSwitcher;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.Panel PropPanel;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Panel GeneralPanel;
      private DCEAccessLib.MLEdit NameTextBox;
      private System.Windows.Forms.Label label;
      private SpinEdit DurationNumericUpDown;
      private System.Windows.Forms.Label DurationLabel;
      private System.Windows.Forms.Label NameLabel;
      private System.Windows.Forms.Label TitleLabel;
      private System.Windows.Forms.Panel TemporaryPanel;
      private System.Windows.Forms.Panel MaterialPanel;
      private System.Windows.Forms.Label ContentLabel;
      private System.Windows.Forms.CheckBox MandatoryCheckBox;
      private System.Windows.Forms.Panel ButtonPanel;
      private System.Windows.Forms.Button TestButton;
      private DCEAccessLib.DataList MaterialsDataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeaderName;
      private System.Windows.Forms.Panel LeftPanel;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button DownButton;
      private System.Windows.Forms.Button UpButton;
      private System.Windows.Forms.Panel BrowserPanel;
      private System.Windows.Forms.Label label1;
      private WebBrowser WebBrowser;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.ContextMenu MaterialContextMenu;
      private System.Windows.Forms.MenuItem CreateMenuItem;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem EditMenuItem;
      private System.Windows.Forms.MenuItem RemoveMenuItem;
      private System.Windows.Forms.MenuItem menuItem5;
      private System.Windows.Forms.MenuItem MoveUpMenuItem;
      private System.Windows.Forms.MenuItem MoveDownMenuItem;
      #endregion

		private ThemeEditNode Node;
      private System.Windows.Forms.CheckBox MakePreviewCheckBox;
      private bool activecourse = false;

      public ThemeEdit(ThemeEditNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.DownButton.Text = "\u25bc";
         this.UpButton.Text = "\u25b2";

         Node = node;

         this.SaveButton.Enabled = DCEUser.CurrentUser.EditableCourses;

         if (DCEUser.CurrentUser.ReadOnlyCourses)
         {
            this.CreateMenuItem.Enabled = false;
            this.RemoveMenuItem.Enabled = false;
            this.MoveDownMenuItem.Enabled = false;
            this.MoveUpMenuItem.Enabled = false;
            this.TestButton.Enabled = false;

            this.DownButton.Enabled = false;
            this.UpButton.Enabled = false;
         }

         CourseEditNode coursenode = (CourseEditNode)Node.GetSpecifiedParentNode("DCECourseEditor.CourseEditNode");
         if ( coursenode != null)
         {
            if (coursenode.IsCourseActive)
            {
               NameTextBox.Enabled = false;
               DurationNumericUpDown.Enabled = false;
               MandatoryCheckBox.Enabled = false;

               this.CreateMenuItem.Enabled = false;
               this.RemoveMenuItem.Enabled = false;
               this.EditMenuItem.Enabled = false;
               this.MoveDownMenuItem.Enabled = false;
               this.MoveUpMenuItem.Enabled = false;

               this.DownButton.Enabled = false;
               this.UpButton.Enabled = false;
               this.TestButton.Enabled = false;

               activecourse = true;
            }
            this.LangSwitcher.Language = coursenode.DefLang;
         }
         
         if (Node.NodeType == ThemeEditNodeTypes.tenMain && !Node.Restricted)
         {
            this.TemporaryPanel.Visible = true;
            this.TestButton.Visible = true;
         }
         else
         {
            this.TemporaryPanel.Visible = false;
            this.TestButton.Visible = false;
         }

         this.NameTextBox.SetParentNode(Node);
         this.RebindControls();
         
         this.Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(this.RebindControls);
         this.Node.OnUpdateDataSet += new RecordEditNode.EnumDataSetsHandler(this.UpdateMembers);

         this.LangSwitcher.LanguageChanged += new LangSwitcher.SwitchLangHandler(this.OnSwitchLang);

         // TODO: Необходимо более надежное решение
         this.NameTextBox.KeyPress += new KeyPressEventHandler(this.OnDataChange);

         //this.DurationNumericUpDown.ValueChanged += new EventHandler(this.OnDataChange);
         this.DurationNumericUpDown.TextChanged += new EventHandler(this.OnDataChange);
         this.MandatoryCheckBox.CheckedChanged += new EventHandler(this.OnDataChange);

         RefreshData(true);
      }

      private void OnDataChange( object sender, KeyPressEventArgs e )
      {
         this.Node.IsNodeDirty = true;
      }

      private void OnDataChange( object sender, EventArgs e )
      {
         this.Node.IsNodeDirty = true;
      }

      private void OnSwitchLang(int NewLang)
      {
         this.dataView.RowFilter = "Lang = " + this.LangSwitcher.Language;
      }

      private string GetSqlQuery()
      {
         return "select * from dbo.Content where eid = '" + 
            
            Node.EditRow["Content"].ToString() + "'" + 
            
            " order by COrder";
      }
      
      public void RefreshData()
      {
         RefreshData(true);
      }

      private void RefreshData(bool refresh)
      {
         this.dataSet = DCEWebAccess.WebAccess.GetDataSet(GetSqlQuery(), "dbo.Content");
         this.dataView.Table = this.dataSet.Tables["dbo.Content"];
         this.dataView.RowFilter = "Lang = " + this.LangSwitcher.Language;

         this.MaterialsDataList.UpdateList();
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
         
         this.DurationNumericUpDown.DataBindings.Clear();
         this.MandatoryCheckBox.DataBindings.Clear();
         
         if (Node.NodeType == ThemeEditNodeTypes.tenMain && !Node.Restricted)
         {
            this.DurationNumericUpDown.DataBindings.Add("Value", Node.EditRow, "Duration");
            this.MandatoryCheckBox.DataBindings.Add("Checked", Node.EditRow, "Mandatory");
         }

         this.RefreshData(true);
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
			this.ButtonPanel = new System.Windows.Forms.Panel();
			this.TestButton = new System.Windows.Forms.Button();
			this.SaveButton = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.PropPanel = new System.Windows.Forms.Panel();
			this.BrowserPanel = new System.Windows.Forms.Panel();
			this.MakePreviewCheckBox = new System.Windows.Forms.CheckBox();
			this.WebBrowser = new System.Windows.Forms.WebBrowser();
			this.label1 = new System.Windows.Forms.Label();
			this.MaterialPanel = new System.Windows.Forms.Panel();
			this.MaterialsDataList = new DCEAccessLib.DataList();
			this.dataColumnHeaderName = new DCEAccessLib.DataColumnHeader();
			this.MaterialContextMenu = new System.Windows.Forms.ContextMenu();
			this.CreateMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.EditMenuItem = new System.Windows.Forms.MenuItem();
			this.RemoveMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.MoveUpMenuItem = new System.Windows.Forms.MenuItem();
			this.MoveDownMenuItem = new System.Windows.Forms.MenuItem();
			this.dataView = new System.Data.DataView();
			this.LeftPanel = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.DownButton = new System.Windows.Forms.Button();
			this.UpButton = new System.Windows.Forms.Button();
			this.ContentLabel = new System.Windows.Forms.Label();
			this.TemporaryPanel = new System.Windows.Forms.Panel();
			this.DurationNumericUpDown = new DCEAccessLib.SpinEdit();
			this.DurationLabel = new System.Windows.Forms.Label();
			this.label = new System.Windows.Forms.Label();
			this.MandatoryCheckBox = new System.Windows.Forms.CheckBox();
			this.GeneralPanel = new System.Windows.Forms.Panel();
			this.NameTextBox = new DCEAccessLib.MLEdit();
			this.NameLabel = new System.Windows.Forms.Label();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.dataSet = new System.Data.DataSet();
			this.LangPanel.SuspendLayout();
			this.ButtonPanel.SuspendLayout();
			this.PropPanel.SuspendLayout();
			this.BrowserPanel.SuspendLayout();
			this.MaterialPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
			this.LeftPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.TemporaryPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DurationNumericUpDown)).BeginInit();
			this.GeneralPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
			this.SuspendLayout();
			// 
			// LangPanel
			// 
			this.LangPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.LangPanel.Controls.Add(this.LangSwitcher);
			this.LangPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.LangPanel.Location = new System.Drawing.Point(0, 0);
			this.LangPanel.Name = "LangPanel";
			this.LangPanel.Size = new System.Drawing.Size(612, 40);
			this.LangPanel.TabIndex = 0;
			// 
			// LangSwitcher
			// 
			this.LangSwitcher.Location = new System.Drawing.Point(8, 8);
			this.LangSwitcher.Name = "LangSwitcher";
			this.LangSwitcher.Size = new System.Drawing.Size(172, 24);
			this.LangSwitcher.TabIndex = 1;
			this.LangSwitcher.TextLabel = "Язык";
			// 
			// ButtonPanel
			// 
			this.ButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ButtonPanel.Controls.Add(this.TestButton);
			this.ButtonPanel.Controls.Add(this.SaveButton);
			this.ButtonPanel.Controls.Add(this.buttonCancel);
			this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ButtonPanel.Location = new System.Drawing.Point(0, 444);
			this.ButtonPanel.Name = "ButtonPanel";
			this.ButtonPanel.Size = new System.Drawing.Size(612, 44);
			this.ButtonPanel.TabIndex = 1;
			// 
			// TestButton
			// 
			this.TestButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TestButton.Location = new System.Drawing.Point(8, 8);
			this.TestButton.Name = "TestButton";
			this.TestButton.Size = new System.Drawing.Size(92, 28);
			this.TestButton.TabIndex = 181;
			this.TestButton.Text = "Тест";
			this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
			// 
			// SaveButton
			// 
			this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SaveButton.Location = new System.Drawing.Point(402, 6);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(96, 28);
			this.SaveButton.TabIndex = 2;
			this.SaveButton.Text = "Сохранить";
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonCancel.Location = new System.Drawing.Point(506, 6);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(96, 28);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Отменить";
			this.buttonCancel.Click += new System.EventHandler(this.ExitButton_Click);
			// 
			// PropPanel
			// 
			this.PropPanel.AutoScroll = true;
			this.PropPanel.Controls.Add(this.BrowserPanel);
			this.PropPanel.Controls.Add(this.MaterialPanel);
			this.PropPanel.Controls.Add(this.TemporaryPanel);
			this.PropPanel.Controls.Add(this.GeneralPanel);
			this.PropPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropPanel.Location = new System.Drawing.Point(0, 40);
			this.PropPanel.Name = "PropPanel";
			this.PropPanel.Size = new System.Drawing.Size(612, 404);
			this.PropPanel.TabIndex = 3;
			// 
			// BrowserPanel
			// 
			this.BrowserPanel.Controls.Add(this.MakePreviewCheckBox);
			this.BrowserPanel.Controls.Add(this.WebBrowser);
			this.BrowserPanel.Controls.Add(this.label1);
			this.BrowserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BrowserPanel.Location = new System.Drawing.Point(0, 272);
			this.BrowserPanel.Name = "BrowserPanel";
			this.BrowserPanel.Size = new System.Drawing.Size(612, 132);
			this.BrowserPanel.TabIndex = 167;
			// 
			// MakePreviewCheckBox
			// 
			this.MakePreviewCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.MakePreviewCheckBox.Checked = true;
			this.MakePreviewCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.MakePreviewCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.MakePreviewCheckBox.Location = new System.Drawing.Point(96, 3);
			this.MakePreviewCheckBox.Name = "MakePreviewCheckBox";
			this.MakePreviewCheckBox.Size = new System.Drawing.Size(13, 16);
			this.MakePreviewCheckBox.TabIndex = 170;
			this.MakePreviewCheckBox.UseVisualStyleBackColor = false;
			this.MakePreviewCheckBox.CheckedChanged += new System.EventHandler(this.MaterialsDataList_SelectedIndexChanged);
			// 
			// WebBrowser
			// 
			this.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WebBrowser.Location = new System.Drawing.Point(0, 20);
			this.WebBrowser.Name = "WebBrowser";
			this.WebBrowser.Size = new System.Drawing.Size(612, 112);
			this.WebBrowser.TabIndex = 169;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.SystemColors.Info;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(612, 20);
			this.label1.TabIndex = 168;
			this.label1.Text = "Просмотр";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MaterialPanel
			// 
			this.MaterialPanel.Controls.Add(this.MaterialsDataList);
			this.MaterialPanel.Controls.Add(this.LeftPanel);
			this.MaterialPanel.Controls.Add(this.ContentLabel);
			this.MaterialPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.MaterialPanel.Location = new System.Drawing.Point(0, 128);
			this.MaterialPanel.Name = "MaterialPanel";
			this.MaterialPanel.Size = new System.Drawing.Size(612, 144);
			this.MaterialPanel.TabIndex = 166;
			// 
			// MaterialsDataList
			// 
			this.MaterialsDataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
			this.MaterialsDataList.AllowColumnReorder = true;
			this.MaterialsDataList.AllowSorting = false;
			this.MaterialsDataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.MaterialsDataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
            this.dataColumnHeaderName});
			this.MaterialsDataList.ContextMenu = this.MaterialContextMenu;
			this.MaterialsDataList.DataView = this.dataView;
			this.MaterialsDataList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MaterialsDataList.FullRowSelect = true;
			this.MaterialsDataList.GridLines = true;
			this.MaterialsDataList.HideSelection = false;
			this.MaterialsDataList.Location = new System.Drawing.Point(40, 20);
			this.MaterialsDataList.MultiSelect = false;
			this.MaterialsDataList.Name = "MaterialsDataList";
			this.MaterialsDataList.Size = new System.Drawing.Size(572, 124);
			this.MaterialsDataList.TabIndex = 172;
			this.MaterialsDataList.UseCompatibleStateImageBehavior = false;
			this.MaterialsDataList.View = System.Windows.Forms.View.Details;
			this.MaterialsDataList.DoubleClick += new System.EventHandler(this.EditMenuItem_Click);
			this.MaterialsDataList.SelectedIndexChanged += new System.EventHandler(this.MaterialsDataList_SelectedIndexChanged);
			// 
			// dataColumnHeaderName
			// 
			this.dataColumnHeaderName.FieldName = "DataStr";
			this.dataColumnHeaderName.Text = "URL";
			this.dataColumnHeaderName.Width = 450;
			// 
			// MaterialContextMenu
			// 
			this.MaterialContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.CreateMenuItem,
            this.menuItem2,
            this.EditMenuItem,
            this.RemoveMenuItem,
            this.menuItem5,
            this.MoveUpMenuItem,
            this.MoveDownMenuItem});
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
			// menuItem5
			// 
			this.menuItem5.Index = 4;
			this.menuItem5.Text = "-";
			// 
			// MoveUpMenuItem
			// 
			this.MoveUpMenuItem.Index = 5;
			this.MoveUpMenuItem.Text = "Переместить вверх";
			this.MoveUpMenuItem.Click += new System.EventHandler(this.MoveUpMenuItem_Click);
			// 
			// MoveDownMenuItem
			// 
			this.MoveDownMenuItem.Index = 6;
			this.MoveDownMenuItem.Text = "Переместить вниз";
			this.MoveDownMenuItem.Click += new System.EventHandler(this.MoveDownMenuItem_Click);
			// 
			// LeftPanel
			// 
			this.LeftPanel.Controls.Add(this.panel1);
			this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.LeftPanel.Location = new System.Drawing.Point(0, 20);
			this.LeftPanel.Name = "LeftPanel";
			this.LeftPanel.Size = new System.Drawing.Size(40, 124);
			this.LeftPanel.TabIndex = 171;
			// 
			// panel1
			// 
			this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.panel1.Controls.Add(this.DownButton);
			this.panel1.Controls.Add(this.UpButton);
			this.panel1.Location = new System.Drawing.Point(8, 26);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(24, 72);
			this.panel1.TabIndex = 0;
			// 
			// DownButton
			// 
			this.DownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.DownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DownButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.DownButton.Location = new System.Drawing.Point(0, 38);
			this.DownButton.Name = "DownButton";
			this.DownButton.Size = new System.Drawing.Size(24, 23);
			this.DownButton.TabIndex = 3;
			this.DownButton.Click += new System.EventHandler(this.MoveDownMenuItem_Click);
			// 
			// UpButton
			// 
			this.UpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.UpButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.UpButton.Location = new System.Drawing.Point(0, 11);
			this.UpButton.Name = "UpButton";
			this.UpButton.Size = new System.Drawing.Size(24, 23);
			this.UpButton.TabIndex = 2;
			this.UpButton.Click += new System.EventHandler(this.MoveUpMenuItem_Click);
			// 
			// ContentLabel
			// 
			this.ContentLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.ContentLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ContentLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.ContentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ContentLabel.ForeColor = System.Drawing.SystemColors.Info;
			this.ContentLabel.Location = new System.Drawing.Point(0, 0);
			this.ContentLabel.Name = "ContentLabel";
			this.ContentLabel.Size = new System.Drawing.Size(612, 20);
			this.ContentLabel.TabIndex = 167;
			this.ContentLabel.Text = "Материалы";
			this.ContentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TemporaryPanel
			// 
			this.TemporaryPanel.Controls.Add(this.DurationNumericUpDown);
			this.TemporaryPanel.Controls.Add(this.DurationLabel);
			this.TemporaryPanel.Controls.Add(this.label);
			this.TemporaryPanel.Controls.Add(this.MandatoryCheckBox);
			this.TemporaryPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TemporaryPanel.Location = new System.Drawing.Point(0, 56);
			this.TemporaryPanel.Name = "TemporaryPanel";
			this.TemporaryPanel.Size = new System.Drawing.Size(612, 72);
			this.TemporaryPanel.TabIndex = 165;
			// 
			// DurationNumericUpDown
			// 
			this.DurationNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DurationNumericUpDown.Location = new System.Drawing.Point(260, 8);
			this.DurationNumericUpDown.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
			this.DurationNumericUpDown.Name = "DurationNumericUpDown";
			this.DurationNumericUpDown.Size = new System.Drawing.Size(56, 20);
			this.DurationNumericUpDown.TabIndex = 176;
			// 
			// DurationLabel
			// 
			this.DurationLabel.Location = new System.Drawing.Point(8, 8);
			this.DurationLabel.Name = "DurationLabel";
			this.DurationLabel.Size = new System.Drawing.Size(240, 23);
			this.DurationLabel.TabIndex = 174;
			this.DurationLabel.Text = "Длительность темы";
			this.DurationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label
			// 
			this.label.Location = new System.Drawing.Point(320, 10);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(56, 16);
			this.label.TabIndex = 177;
			this.label.Text = "(раб. дни)";
			// 
			// MandatoryCheckBox
			// 
			this.MandatoryCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.MandatoryCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.MandatoryCheckBox.Location = new System.Drawing.Point(8, 40);
			this.MandatoryCheckBox.Name = "MandatoryCheckBox";
			this.MandatoryCheckBox.Size = new System.Drawing.Size(308, 24);
			this.MandatoryCheckBox.TabIndex = 179;
			this.MandatoryCheckBox.Text = "Обязательность прохождения";
			// 
			// GeneralPanel
			// 
			this.GeneralPanel.Controls.Add(this.NameTextBox);
			this.GeneralPanel.Controls.Add(this.NameLabel);
			this.GeneralPanel.Controls.Add(this.TitleLabel);
			this.GeneralPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.GeneralPanel.Location = new System.Drawing.Point(0, 0);
			this.GeneralPanel.Name = "GeneralPanel";
			this.GeneralPanel.Size = new System.Drawing.Size(612, 56);
			this.GeneralPanel.TabIndex = 164;
			// 
			// NameTextBox
			// 
			this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.NameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.NameTextBox.CaptionLabel = null;
			this.NameTextBox.eId = "";
			this.NameTextBox.LanguageSwitcher = this.LangSwitcher;
			this.NameTextBox.Location = new System.Drawing.Point(120, 27);
			this.NameTextBox.MaxLength = 100;
			this.NameTextBox.Name = "NameTextBox";
			this.NameTextBox.Size = new System.Drawing.Size(484, 20);
			this.NameTextBox.TabIndex = 178;
			// 
			// NameLabel
			// 
			this.NameLabel.Location = new System.Drawing.Point(8, 32);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(100, 16);
			this.NameLabel.TabIndex = 173;
			this.NameLabel.Text = "Название";
			// 
			// TitleLabel
			// 
			this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
			this.TitleLabel.Location = new System.Drawing.Point(0, 0);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(612, 20);
			this.TitleLabel.TabIndex = 171;
			this.TitleLabel.Text = "Основные";
			this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dataSet
			// 
			this.dataSet.DataSetName = "NewDataSet";
			this.dataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
			// 
			// ThemeEdit
			// 
			this.Controls.Add(this.PropPanel);
			this.Controls.Add(this.ButtonPanel);
			this.Controls.Add(this.LangPanel);
			this.Name = "ThemeEdit";
			this.Size = new System.Drawing.Size(612, 488);
			this.LangPanel.ResumeLayout(false);
			this.ButtonPanel.ResumeLayout(false);
			this.PropPanel.ResumeLayout(false);
			this.BrowserPanel.ResumeLayout(false);
			this.MaterialPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
			this.LeftPanel.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.TemporaryPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DurationNumericUpDown)).EndInit();
			this.GeneralPanel.ResumeLayout(false);
			this.GeneralPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
			this.ResumeLayout(false);

      }
		#endregion

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         // сохранить изменения
         this.Node.EndEdit(false, false);
         this.Node.CreateChilds();
         this.Node.IsNodeDirty = false;
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

      private void TestButton_Click(object sender, System.EventArgs e)
      {
         if (this.Node.IsNew)
            MessageBox.Show("Для создания теста необходимо вначале сохранить тему!","Сообщение",MessageBoxButtons.OK);
         else
         {
            Node.CreateChilds();
         
            // флаг, показывающий есть ли уже тест для этой темы
            bool neednew = true;
         
            // Проверка нет ли уже открытой ноды "Тест"
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(TestEditNode) )
               {
                  if ( ((TestEditNode)node).EditRow["Parent"].ToString() == Node.EditRow["id"].ToString())
                  {
                     node.Select();
                     neednew = false;
                     break;
                  }
               }
            }
            if (neednew)
            {
               TestEditNode node = new TestEditNode(Node, "", 
                  Node.EditRow["id"].ToString(), false, Node.ThemeName);
               node.Select();
            }
         }
      }

      private bool GetDiskFolder(out string diskfolder)
      {
         NodeControl node = this.Node;
         while ( ! (node is CourseEditNode) && node != null )
            node = node.NodeParent;
         
         if (node is CourseEditNode)
         {
            RecordEditNode rnode = (RecordEditNode)node;
            diskfolder = rnode.EditRow["DiskFolder"].ToString();
            return true;
         }
         else
         {
            diskfolder = "";
            return false;
         }
      }
      
      private void CreateMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.Node.IsNew)
            MessageBox.Show("Для добавления материала необходимо вначале сохранить тему!","Сообщение",MessageBoxButtons.OK);
         else
         {
            string diskfolder;
            if (GetDiskFolder(out diskfolder))
            {
               MaterialsForm MatForm = new MaterialsForm(
                  "", 
                  diskfolder,
                  "Выбор материала", 
                  "Htm files|*.html;*.htm|All files|*.*");
            
               if (MatForm.ShowDialog() == DialogResult.OK)
               {
                  DCEWebAccess.WebAccess.ExecSQL(
                     
                     "declare @maxcorder int " + 

                     "set @maxcorder = (select 1+isnull(max(COrder), 0) from Content) " +
                     
                     "INSERT INTO dbo.Content " + 

                     //" (id, eid, Type, Lang, DataStr, COrder) " + 

                     " VALUES ('" + System.Guid.NewGuid().ToString() + "', '" + 

                     this.Node.EditRow["Content"].ToString() + "', " + 

                     (int)ContentType._url + ", " + this.LangSwitcher.Language + ",'" + 

                     MatForm.Path + "', NULL, NULL, NULL, @maxcorder )");
               
                  RefreshData(true);

                  if (this.MaterialsDataList.Items.Count != 0)
                  {
                     ListViewItem item = this.MaterialsDataList.Items[
                        this.MaterialsDataList.Items.Count-1];
                     item.Selected = true;
                     item.Focused = true;
                  }
               }
            }
         }
      }

      private void EditMenuItem_Click(object sender, System.EventArgs e)
      {
         if (activecourse) return;

         if (this.MaterialsDataList.SelectedItems.Count != 0 
            && this.MaterialsDataList.SelectedItems[0].Tag != null)
         {
            DataRowView row = (DataRowView)this.MaterialsDataList.SelectedItems[0].Tag;
            
            string path = row["DataStr"].ToString();
            
            string diskfolder;
            if (GetDiskFolder(out diskfolder))
            {
               MaterialsForm MatForm = new MaterialsForm(
                  path, 
                  diskfolder, 
                  "Выбор материала", 
                  "Htm files|*.html;*.htm|All files|*.*");

               if (MatForm.ShowDialog() == DialogResult.OK)
               {
                  DCEWebAccess.WebAccess.ExecSQL("UPDATE Content SET dataStr = '" + 
                     MatForm.Path + "' WHERE id = '" + row["id"].ToString() + "'" );
                  
                  this.RefreshData(true);
               }
            }
         }
      }

      private void RemoveMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.MaterialsDataList.SelectedItems.Count>0 && this.MaterialsDataList.SelectedItems[0].Tag!=null)      
         {
            DataRowView row = (DataRowView)this.MaterialsDataList.SelectedItems[0].Tag;
            if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данную запись?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from dbo.Content where id = '" + row["id"].ToString() + "'");
               
               this.RefreshData(true);
            }
         }
      }

      private void SwapMaterials(int index1, int index2)
      {
         string strid = ((DataRowView)this.MaterialsDataList.SelectedItems[0].Tag).Row["id"].ToString();

         DataRowView row1 = this.MaterialsDataList.Items[index1].Tag as DataRowView;
         string id1 = row1["id"].ToString();
         int order1 = (int)(row1["COrder"]);
         DataRowView row2 = this.MaterialsDataList.Items[index2].Tag as DataRowView; 
         string id2 = row2["id"].ToString();
         int order2 = (int)(row2["COrder"]);

         string query = 
            "UPDATE dbo.Content SET COrder = " + order2 + " where id = '" + id1 + "' "+
            "UPDATE dbo.Content SET COrder = " + order1 + " where id = '" + id2 + "' ";
               
         DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL(query);

         RefreshData(true);

         foreach (ListViewItem item in this.MaterialsDataList.Items)
         {
            if (((DataRowView)item.Tag).Row["id"].ToString() == strid)
            {
               item.Selected = true;
               break;
            }
         }
      }
      
      private void MoveUpMenuItem_Click(object sender, System.EventArgs e)
      {
         // перемещение контента вперед по порядку
         
         UpdateMaterials();
         
         if (this.MaterialsDataList.SelectedItems.Count>0 && this.MaterialsDataList.SelectedItems[0].Tag!=null)
         {
            int curindex = this.MaterialsDataList.SelectedItems[0].Index;

            // если контент можно переместить вверх на одну позицию
            if (curindex > 0)
               SwapMaterials(curindex, curindex-1);
         }
      }

      private void MoveDownMenuItem_Click(object sender, System.EventArgs e)
      {
         // перемещение контента вниз по порядку
         
         UpdateMaterials();

         if (this.MaterialsDataList.SelectedItems.Count>0 && this.MaterialsDataList.SelectedItems[0].Tag!=null)
         {
            int curindex = this.MaterialsDataList.SelectedItems[0].Index;
            
            // если контент можно переместить вверх на одну позицию
            if (curindex < this.MaterialsDataList.Items.Count-1)
               SwapMaterials(curindex, curindex+1);               
         }
      }

      private void MaterialsDataList_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (this.MaterialsDataList.SelectedItems.Count != 0 
            && this.MaterialsDataList.SelectedItems[0].Tag != null &&
            this.MakePreviewCheckBox.Checked)
         {
            DataRowView row = (DataRowView)this.MaterialsDataList.SelectedItems[0].Tag;

            try
            {
               string diskfolder;
               if (GetDiskFolder(out diskfolder))
               {
                  string url = DCEUser.CourseRootPath + diskfolder + row["DataStr"].ToString();
                  Object refmissing = System.Reflection.Missing.Value;
                  WebBrowser.Navigate(url);
               }
            }
            finally
            {
            }
         }
      }
	}
    public enum ThemeEditNodeTypes { tenMain, tenOther };
    public class ThemeEditNode : HighlightedRecordEditNode
    {
        public ThemeEditNode(NodeControl parent, string themeid, string parentThemeId, ThemeEditNodeTypes nodetype, string themename, bool restricted)
            : base(parent, "select * from dbo.Themes", "dbo.Themes", "id", themeid, DCEUser.CurrentUser.ReadOnlyCourses)
        {
            nodeType = nodetype;
            ParentThemeId = parentThemeId;

            this.restricted = restricted;

            if (EditRow.IsNew)
                EditRow["Parent"] = ParentThemeId;

            ThemeName = themename;
        }
        /// <summary>
        /// Флаг, указывающий на ограниченны возможности темы. Флаг 
        /// устанавливается когда нода входит в библиотеку или в словарь
        /// </summary>
        bool restricted;
        public bool Restricted
        {
            get { return restricted; }
        }

        /// <summary>
        /// Идентификатор группы или курса
        /// </summary>
        private string ParentThemeId = "";

        private string themeName = "";

        private ThemeEditNodeTypes nodeType;

        public string ThemeName
        {
            get
            {
                return themeName;
            }
            set
            {
                this.themeName = value;
                this.CaptionChanged();
            }
        }

        public ThemeEditNodeTypes NodeType
        {
            get { return nodeType; }
        }

        public override bool HaveChildNodes() { return true; }

        public override void CreateChilds()
        {
            //if (!this.IsNew)
            //{
            // создание ноды "Подтемы"

            bool needcreate = true;

            foreach (NodeControl node in this.Nodes)
            {
                if (node.GetType() == typeof(ThemeListNode))
                {
                    needcreate = false;
                    break;
                }
            }

            if (needcreate)
            {
                ThemeListNodeTypes childsType = ThemeListNodeTypes.tlnOther;

                switch (this.nodeType)
                {
                    case ThemeEditNodeTypes.tenMain:
                        childsType = restricted ? ThemeListNodeTypes.tlnOther : ThemeListNodeTypes.tlnWithWork;
                        break;
                    case ThemeEditNodeTypes.tenOther:
                        childsType = ThemeListNodeTypes.tlnOther;
                        break;
                }

                new ThemeListNode(this, this.Id, childsType, restricted);
            }

            if (restricted == false)
            {
                // Создание ноды "Тест"

                needcreate = true;

                foreach (NodeControl node in this.Nodes)
                {
                    if (node.GetType() == typeof(TestEditNode))
                    {
                        needcreate = false;
                        break;
                    }
                }

                if (needcreate)
                {
                    // поиск идентификатора теста относящегося к данной теме
                    DataSet ds = DCEWebAccess.WebAccess.GetDataSet(

                       "select * from dbo.Tests where " +

                       "Parent = '" + this.EditRow["id"].ToString() + "' and " +

                       "Type = " + (int)TestType.test,

                       "Tests");

                    if (ds.Tables["Tests"].Rows.Count > 0)
                    {
                        DataRow row = ds.Tables["Tests"].Rows[0];

                        new TestEditNode(this, row["id"].ToString(), this.Id, false, this.ThemeName);
                    }
                }
                //}
            }
        }

        public override System.Windows.Forms.UserControl GetControl()
        {
            if (this.fControl == null)
            {
                fControl = new ThemeEdit(this);
            }
            if (needRefresh)
            {
                ((ThemeEdit)this.fControl).RefreshData();
                needRefresh = false;
            }

            return this.fControl;
        }

        public override String GetCaption()
        {
            string caption = "";
            string name = "Подтема";

            if (this.NodeType == ThemeEditNodeTypes.tenMain)
                name = "Тема";

            if (this.IsNew)
                caption = "[Новая " + name + "]";
            else
                caption = name + " : " + this.ThemeName;

            if (this.IsNodeDirty)
                caption += "*";

            return caption;
        }

        public override void ReleaseControl()
        {
            // do nothing
        }

        public override bool CanClose()
        {
            return false;
        }

        /// <summary>
        /// Initialize new record for editing (EditRow)
        /// </summary>
        protected override void InitNewRecord()
        {
            // filling all non-nullable fields
            EditRow["id"] = System.Guid.NewGuid().ToString();
            EditRow["Type"] = (int)ThemeType.theme;
            EditRow["Name"] = System.Guid.NewGuid().ToString();
            EditRow["TOrder"] = 0;
            EditRow["Content"] = System.Guid.NewGuid().ToString();
            EditRow["Duration"] = 1;
            EditRow["Mandatory"] = true;
        }
    }
}
