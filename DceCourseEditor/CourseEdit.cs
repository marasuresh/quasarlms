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
	/// Форма свойств курса
	/// </summary>
	public class CourseEdit : System.Windows.Forms.UserControl
	{
      #region Form variables
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private DCEAccessLib.LangSwitcher langSwitcher2;
      private System.Windows.Forms.Panel TopPanel;
      private System.Windows.Forms.Panel MainPanel;
      private System.Windows.Forms.Panel panel4;
      private System.Windows.Forms.Panel panel5;
      private DCEAccessLib.MLEdit CAddition4;
      private DCEAccessLib.MLEdit CAddition3;
      private DCEAccessLib.MLEdit CAddition2;
      private DCEAccessLib.MLEdit CAddition1;
      private System.Windows.Forms.Label label18;
      private System.Windows.Forms.Label label14;
      private System.Windows.Forms.Label label13;
      private System.Windows.Forms.Label label12;
      private System.Windows.Forms.Label label17;
      private System.Windows.Forms.Panel panel6;
      private DCEAccessLib.MLEdit CKeyWords;
      private DCEAccessLib.MLEdit CPriorRequests;
      private DCEAccessLib.MLEdit CFullDescription;
      private System.Windows.Forms.Label label11;
      private System.Windows.Forms.Label label10;
      private System.Windows.Forms.Label label15;
      private System.Windows.Forms.Label label8;
      private DCEAccessLib.MLEdit CBriefDescription;
      private System.Windows.Forms.Label label28;
      private System.Windows.Forms.Panel panel7;
      private DCEAccessLib.MLEdit CAuthor;
      private DCEAccessLib.MLEdit CName;
      private System.Windows.Forms.Label label27;
      private System.Windows.Forms.ComboBox CType;
      private System.Windows.Forms.Label label26;
      private System.Windows.Forms.TextBox CVersion;
      private System.Windows.Forms.TextBox CCode;
      private System.Windows.Forms.Label label16;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label TitleLabel;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button ExitButton;
      private System.Windows.Forms.Panel ButtonPanel;
      private System.Windows.Forms.Button MenuButton;
      private System.Windows.Forms.Button BrowseButton;
      private System.Windows.Forms.ContextMenu Create_Edit_ContextMenu;
      private System.Windows.Forms.MenuItem FinalTestMenuItem;
      private System.Windows.Forms.MenuItem VocabluraryMenuItem;
      private System.Windows.Forms.MenuItem LibraryMenuItem;
      private System.Windows.Forms.Button BrowseButton1;
      private System.Windows.Forms.Label label20;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Label label21;
      private System.Windows.Forms.Label label22;
      private System.Windows.Forms.Label CDuration;
      private System.Windows.Forms.ComboBox comboBoxFinishQuestionnaire;
      private System.Windows.Forms.ComboBox comboBoxStartQuestionnaire;
      private System.Windows.Forms.Label CActive;
      private System.Windows.Forms.CheckBox CPublic;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label label9;
      private SpinEdit Cost1;
      private SpinEdit Cost2;
      private System.Windows.Forms.Label label19;
      private System.Windows.Forms.Label label23;
      private System.Windows.Forms.CheckBox checkBoxReady;
      private System.Windows.Forms.Label label24;
      private System.Windows.Forms.ComboBox LangCB;
      private DCECourseEditor.CourseFolderChooser courseFolder;
      #endregion

      private CourseEditNode Node;
		
      public CourseEdit(CourseEditNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         Node = node;

         this.SaveButton.Enabled = DCEUser.CurrentUser.EditableCourses;

         if (DCEUser.CurrentUser.ReadOnlyCourses)
         {
            this.FinalTestMenuItem.Enabled = false;
            this.LibraryMenuItem.Enabled = false;
            this.VocabluraryMenuItem.Enabled = false;
         }

         this.langSwitcher2.Language = (int)this.Node.EditRow["CourseLanguage"];

         this.CName.SetParentNode(Node);
         this.CAuthor.SetParentNode(Node);
         this.CBriefDescription.SetParentNode(Node);
         this.CFullDescription.SetParentNode(Node);
         this.CPriorRequests.SetParentNode(Node);
         this.CKeyWords.SetParentNode(Node);

         this.CAddition1.SetParentNode(Node);
         this.CAddition2.SetParentNode(Node);
         this.CAddition3.SetParentNode(Node);
         this.CAddition4.SetParentNode(Node);
         
         this.RebindControls();
         this.Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(this.RebindControls);
         
         RefreshData();
		}

      public bool IsCourseActive()
      {
         DataSet ds1 = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            
            "select SUM(Duration) as sDuration, dbo.CourseIsActive('"
            
            + Node.EditRow["id"].ToString() + "') as isActive from Themes where Parent = '"
            
            + Node.EditRow["id"].ToString() + "'", "Info");

         DataTable table1 = ds1.Tables["Info"];

         if (table1 != null)
         {
            return (bool)table1.Rows[0]["isActive"];
         }
         
         return false;
      }

      public void RebindControls()
      {
         this.CName.DataBindings.Clear();
         this.CName.DataBindings.Add("eId", Node.EditRow, "Name");

         this.CType.DataBindings.Clear();
         this.CType.DataBindings.Add("SelectedValue", Node.EditRow, "Type");

         this.CAuthor.DataBindings.Clear();
         this.CAuthor.DataBindings.Add("eId", Node.EditRow, "Author");

         this.CBriefDescription.DataBindings.Clear();
         this.CBriefDescription.DataBindings.Add("eId", Node.EditRow, "DescriptionShort");

         this.CFullDescription.DataBindings.Clear();
         this.CFullDescription.DataBindings.Add("eId", Node.EditRow, "DescriptionLong");

         this.CPriorRequests.DataBindings.Clear();
         this.CPriorRequests.DataBindings.Add("eId", Node.EditRow, "Requirements");

         this.CKeyWords.DataBindings.Clear();
         this.CKeyWords.DataBindings.Add("eId", Node.EditRow, "Keywords");

         this.CAddition1.DataBindings.Clear();
         this.CAddition1.DataBindings.Add("eId", Node.EditRow, "Additions");

         this.CAddition2.DataBindings.Clear();
         this.CAddition2.DataBindings.Add("eId", Node.EditRow, "Additions");

         this.CAddition3.DataBindings.Clear();
         this.CAddition3.DataBindings.Add("eId", Node.EditRow, "Additions");
         
         this.CAddition4.DataBindings.Clear();
         this.CAddition4.DataBindings.Add("eId", Node.EditRow, "Additions");
         
         this.CCode.DataBindings.Clear();
         this.CCode.DataBindings.Add("Text", Node.EditRow, "Code");

         this.CVersion.DataBindings.Clear();
         this.CVersion.DataBindings.Add("Text", Node.EditRow, "Version");

         this.CPublic.DataBindings.Clear();
         this.CPublic.DataBindings.Add("Checked", Node.EditRow, "CPublic");

         Cost1.DataBindings.Clear();
         Cost1.DataBindings.Add("Value", Node.EditRow, "Cost1");

         Cost2.DataBindings.Clear();
         Cost2.DataBindings.Add("Value", Node.EditRow, "Cost2");

         courseFolder.DiskFolder.DataBindings.Clear();
         courseFolder.DiskFolder.DataBindings.Add("Text", Node.EditRow, "DiskFolder");

         DataSet ds1 = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            
            "select SUM(Duration) as sDuration, dbo.CourseIsActive('"
            
            + Node.EditRow["id"].ToString() + "') as isActive from Themes where Parent = '"
            
            + Node.EditRow["id"].ToString() + "'", "Info");

         DataTable table1 = ds1.Tables["Info"];

         if (table1 != null)
         {
            this.CDuration.Text = table1.Rows[0]["sDuration"].ToString();
            if ((bool)table1.Rows[0]["isActive"])
            {
               this.CActive.Text = "Курс  АКТИВНЫЙ";
               this.CType.Enabled = false;
               this.CName.Enabled = false;

               this.CCode.Enabled = false;
               this.CVersion.Enabled = false;
               this.CKeyWords.Enabled = false;
               this.CFullDescription.Enabled = false;
               this.BrowseButton.Enabled = false;
               this.CAuthor.Enabled = false;
               this.courseFolder.Enabled = false;
               this.CPriorRequests.Enabled = false;
               this.BrowseButton1.Enabled = false;
               this.CAddition1.Enabled = false;
               this.CAddition2.Enabled = false;
               this.CAddition3.Enabled = false;
               this.CAddition4.Enabled = false;
               this.comboBoxFinishQuestionnaire.Enabled = false;
               this.comboBoxStartQuestionnaire.Enabled = false;
               this.CBriefDescription.Enabled = false;
               this.LangCB.Enabled = false;
            }
            else
            {
               this.CActive.Text = "Курс не активный";
               this.CType.Enabled = true;
               this.CName.Enabled = true;

               this.CCode.Enabled = true;
               this.CVersion.Enabled = true;
               this.CKeyWords.Enabled = true;
               this.CFullDescription.Enabled = true;
               this.BrowseButton.Enabled = true;
               this.CAuthor.Enabled = true;
               this.courseFolder.Enabled = true;
               this.CPriorRequests.Enabled = true;
               this.BrowseButton1.Enabled = true;
               this.CAddition1.Enabled = true;
               this.CAddition2.Enabled = true;
               this.CAddition3.Enabled = true;
               this.CAddition4.Enabled = true;
               this.comboBoxFinishQuestionnaire.Enabled = true;
               this.comboBoxStartQuestionnaire.Enabled = true;
               this.CBriefDescription.Enabled = true;
               this.LangCB.Enabled = true;
            }
         }

         comboBoxStartQuestionnaire.DataBindings.Clear();
         comboBoxStartQuestionnaire.DataBindings.Add("SelectedValue", Node.EditRow, "StartQuestionnaire");

         comboBoxFinishQuestionnaire.DataBindings.Clear();
         comboBoxFinishQuestionnaire.DataBindings.Add("SelectedValue", Node.EditRow, "FinishQuestionnaire");

         checkBoxReady.DataBindings.Clear();
         checkBoxReady.DataBindings.Add("Checked", Node.EditRow, "isReady");

         LangCB.DataBindings.Clear();
         LangCB.DataBindings.Add("SelectedValue", Node.EditRow, "CourseLanguage");

         Node.DefLang = (int)Node.EditRow["CourseLanguage"];
      }

      private void FillComboBoxNotNull(ComboBox c, string query, string tablename, string displaymember, string valuemember, string fieldname)
      {
         CustomFillComboBox(c, query, tablename, displaymember, valuemember, fieldname, false);
      }

      private void FillComboBox(ComboBox c, string query, string tablename, string displaymember, string valuemember, string fieldname)
      {
         CustomFillComboBox(c, query, tablename, displaymember, valuemember, fieldname, true);
      }

      private void CustomFillComboBox(ComboBox c, string query, string tablename, string displaymember, string valuemember, string fieldname, bool diplaynull)
      {
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            query,
            tablename);

         if (diplaynull)
            ds.Tables[tablename].Rows.Add(new object[]{System.DBNull.Value, "<Пусто>"});
         
         c.DisplayMember = displaymember;
         c.ValueMember = valuemember;
         c.DataSource = ds.Tables[tablename];

         bool neednull = true;
         foreach(DataRow row in ds.Tables[tablename].Rows)
         {
            if (row[valuemember].ToString() == Node.EditRow[fieldname].ToString())
            {
               c.SelectedValue = row[valuemember];
               neednull = false;
               break;
            }
         }
         
         if (neednull)
         {
            c.SelectedValue = System.DBNull.Value;
         }
      }

      public void RefreshData()
      {
         FillComboBox(
            comboBoxStartQuestionnaire, 
            "select id, InternalName from Tests where type = " + (int)TestType.questionnaire, 
            "Questionnaires", 
            "InternalName", 
            "id",
            "StartQuestionnaire");
         
         FillComboBox(
            comboBoxFinishQuestionnaire, 
            "select id, InternalName from Tests where type = " + (int)TestType.questionnaire, 
            "Questionnaires", 
            "InternalName", 
            "id",
            "FinishQuestionnaire");

         FillComboBox(
            CType, 
            "select id, dbo.GetStrContentAlt2(Name, " + Node.DefLang + ", 'RU') as RName from CourseType", 
            "CourseType", 
            "RName", 
            "id",
            "Type");

         FillComboBoxNotNull(
            LangCB, 
            "select id, NameNative from Languages", 
            "Languages", 
            "NameNative", 
            "id",
            "CourseLanguage");
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
         this.langSwitcher2 = new DCEAccessLib.LangSwitcher();
         this.TopPanel = new System.Windows.Forms.Panel();
         this.ButtonPanel = new System.Windows.Forms.Panel();
         this.MenuButton = new System.Windows.Forms.Button();
         this.ExitButton = new System.Windows.Forms.Button();
         this.SaveButton = new System.Windows.Forms.Button();
         this.MainPanel = new System.Windows.Forms.Panel();
         this.panel4 = new System.Windows.Forms.Panel();
         this.panel5 = new System.Windows.Forms.Panel();
         this.CAddition4 = new DCEAccessLib.MLEdit();
         this.CAddition3 = new DCEAccessLib.MLEdit();
         this.CAddition2 = new DCEAccessLib.MLEdit();
         this.CAddition1 = new DCEAccessLib.MLEdit();
         this.label18 = new System.Windows.Forms.Label();
         this.label14 = new System.Windows.Forms.Label();
         this.label13 = new System.Windows.Forms.Label();
         this.label12 = new System.Windows.Forms.Label();
         this.label17 = new System.Windows.Forms.Label();
         this.panel2 = new System.Windows.Forms.Panel();
         this.label23 = new System.Windows.Forms.Label();
         this.label19 = new System.Windows.Forms.Label();
         this.Cost2 = new SpinEdit();
         this.Cost1 = new SpinEdit();
         this.label9 = new System.Windows.Forms.Label();
         this.label7 = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.panel1 = new System.Windows.Forms.Panel();
         this.label22 = new System.Windows.Forms.Label();
         this.label21 = new System.Windows.Forms.Label();
         this.comboBoxStartQuestionnaire = new System.Windows.Forms.ComboBox();
         this.comboBoxFinishQuestionnaire = new System.Windows.Forms.ComboBox();
         this.label20 = new System.Windows.Forms.Label();
         this.panel6 = new System.Windows.Forms.Panel();
         this.BrowseButton1 = new System.Windows.Forms.Button();
         this.BrowseButton = new System.Windows.Forms.Button();
         this.CKeyWords = new DCEAccessLib.MLEdit();
         this.CPriorRequests = new DCEAccessLib.MLEdit();
         this.CFullDescription = new DCEAccessLib.MLEdit();
         this.label11 = new System.Windows.Forms.Label();
         this.label10 = new System.Windows.Forms.Label();
         this.label15 = new System.Windows.Forms.Label();
         this.label8 = new System.Windows.Forms.Label();
         this.CBriefDescription = new DCEAccessLib.MLEdit();
         this.label28 = new System.Windows.Forms.Label();
         this.panel7 = new System.Windows.Forms.Panel();
         this.courseFolder = new DCECourseEditor.CourseFolderChooser();
         this.LangCB = new System.Windows.Forms.ComboBox();
         this.label24 = new System.Windows.Forms.Label();
         this.checkBoxReady = new System.Windows.Forms.CheckBox();
         this.CPublic = new System.Windows.Forms.CheckBox();
         this.CActive = new System.Windows.Forms.Label();
         this.CDuration = new System.Windows.Forms.Label();
         this.CAuthor = new DCEAccessLib.MLEdit();
         this.CName = new DCEAccessLib.MLEdit();
         this.label27 = new System.Windows.Forms.Label();
         this.CType = new System.Windows.Forms.ComboBox();
         this.label26 = new System.Windows.Forms.Label();
         this.CVersion = new System.Windows.Forms.TextBox();
         this.CCode = new System.Windows.Forms.TextBox();
         this.label16 = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.TitleLabel = new System.Windows.Forms.Label();
         this.Create_Edit_ContextMenu = new System.Windows.Forms.ContextMenu();
         this.FinalTestMenuItem = new System.Windows.Forms.MenuItem();
         this.VocabluraryMenuItem = new System.Windows.Forms.MenuItem();
         this.LibraryMenuItem = new System.Windows.Forms.MenuItem();
         this.TopPanel.SuspendLayout();
         this.ButtonPanel.SuspendLayout();
         this.MainPanel.SuspendLayout();
         this.panel4.SuspendLayout();
         this.panel5.SuspendLayout();
         this.panel2.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.Cost2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.Cost1)).BeginInit();
         this.panel1.SuspendLayout();
         this.panel6.SuspendLayout();
         this.panel7.SuspendLayout();
         this.SuspendLayout();
         // 
         // langSwitcher2
         // 
         this.langSwitcher2.Location = new System.Drawing.Point(0, 4);
         this.langSwitcher2.Name = "langSwitcher2";
         this.langSwitcher2.Size = new System.Drawing.Size(176, 24);
         this.langSwitcher2.TabIndex = 0;
         this.langSwitcher2.TextLabel = "Язык";
         // 
         // TopPanel
         // 
         this.TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TopPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                               this.langSwitcher2});
         this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.TopPanel.Name = "TopPanel";
         this.TopPanel.Size = new System.Drawing.Size(632, 30);
         this.TopPanel.TabIndex = 1;
         // 
         // ButtonPanel
         // 
         this.ButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ButtonPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.MenuButton,
                                                                                  this.ExitButton,
                                                                                  this.SaveButton});
         this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.ButtonPanel.Location = new System.Drawing.Point(0, 456);
         this.ButtonPanel.Name = "ButtonPanel";
         this.ButtonPanel.Size = new System.Drawing.Size(632, 44);
         this.ButtonPanel.TabIndex = 160;
         // 
         // MenuButton
         // 
         this.MenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.MenuButton.Location = new System.Drawing.Point(8, 8);
         this.MenuButton.Name = "MenuButton";
         this.MenuButton.Size = new System.Drawing.Size(148, 28);
         this.MenuButton.TabIndex = 21;
         this.MenuButton.Text = "Создать / Редактировать";
         this.MenuButton.Click += new System.EventHandler(this.MenuButton_Click);
         // 
         // ExitButton
         // 
         this.ExitButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ExitButton.Location = new System.Drawing.Point(526, 8);
         this.ExitButton.Name = "ExitButton";
         this.ExitButton.Size = new System.Drawing.Size(96, 28);
         this.ExitButton.TabIndex = 23;
         this.ExitButton.Text = "Отмена";
         this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
         // 
         // SaveButton
         // 
         this.SaveButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SaveButton.Location = new System.Drawing.Point(422, 8);
         this.SaveButton.Name = "SaveButton";
         this.SaveButton.Size = new System.Drawing.Size(96, 28);
         this.SaveButton.TabIndex = 22;
         this.SaveButton.Text = "Сохранить";
         this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
         // 
         // MainPanel
         // 
         this.MainPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.panel4});
         this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.MainPanel.Location = new System.Drawing.Point(0, 30);
         this.MainPanel.Name = "MainPanel";
         this.MainPanel.Size = new System.Drawing.Size(632, 426);
         this.MainPanel.TabIndex = 27;
         // 
         // panel4
         // 
         this.panel4.AutoScroll = true;
         this.panel4.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.panel5,
                                                                             this.label17,
                                                                             this.panel2,
                                                                             this.label4,
                                                                             this.panel1,
                                                                             this.label20,
                                                                             this.panel6,
                                                                             this.label28,
                                                                             this.panel7,
                                                                             this.TitleLabel});
         this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel4.Name = "panel4";
         this.panel4.Size = new System.Drawing.Size(632, 426);
         this.panel4.TabIndex = 2;
         // 
         // panel5
         // 
         this.panel5.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.CAddition4,
                                                                             this.CAddition3,
                                                                             this.CAddition2,
                                                                             this.CAddition1,
                                                                             this.label18,
                                                                             this.label14,
                                                                             this.label13,
                                                                             this.label12});
         this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel5.Location = new System.Drawing.Point(0, 816);
         this.panel5.Name = "panel5";
         this.panel5.Size = new System.Drawing.Size(614, 280);
         this.panel5.TabIndex = 158;
         // 
         // CAddition4
         // 
         this.CAddition4.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.CAddition4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.CAddition4.CaptionLabel = null;
         this.CAddition4.COrder = 3;
         this.CAddition4.eId = "";
         this.CAddition4.LanguageSwitcher = this.langSwitcher2;
         this.CAddition4.Location = new System.Drawing.Point(152, 212);
         this.CAddition4.MaxLength = 255;
         this.CAddition4.Multiline = true;
         this.CAddition4.Name = "CAddition4";
         this.CAddition4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.CAddition4.Size = new System.Drawing.Size(456, 60);
         this.CAddition4.TabIndex = 20;
         this.CAddition4.Text = "";
         this.CAddition4.UseCOrder = true;
         // 
         // CAddition3
         // 
         this.CAddition3.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.CAddition3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.CAddition3.CaptionLabel = null;
         this.CAddition3.COrder = 2;
         this.CAddition3.eId = "";
         this.CAddition3.LanguageSwitcher = this.langSwitcher2;
         this.CAddition3.Location = new System.Drawing.Point(152, 144);
         this.CAddition3.MaxLength = 255;
         this.CAddition3.Multiline = true;
         this.CAddition3.Name = "CAddition3";
         this.CAddition3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.CAddition3.Size = new System.Drawing.Size(456, 60);
         this.CAddition3.TabIndex = 19;
         this.CAddition3.Text = "";
         this.CAddition3.UseCOrder = true;
         // 
         // CAddition2
         // 
         this.CAddition2.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.CAddition2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.CAddition2.CaptionLabel = null;
         this.CAddition2.COrder = 1;
         this.CAddition2.eId = "";
         this.CAddition2.LanguageSwitcher = this.langSwitcher2;
         this.CAddition2.Location = new System.Drawing.Point(152, 76);
         this.CAddition2.MaxLength = 255;
         this.CAddition2.Multiline = true;
         this.CAddition2.Name = "CAddition2";
         this.CAddition2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.CAddition2.Size = new System.Drawing.Size(456, 60);
         this.CAddition2.TabIndex = 18;
         this.CAddition2.Text = "";
         this.CAddition2.UseCOrder = true;
         // 
         // CAddition1
         // 
         this.CAddition1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.CAddition1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.CAddition1.CaptionLabel = null;
         this.CAddition1.eId = "";
         this.CAddition1.LanguageSwitcher = this.langSwitcher2;
         this.CAddition1.Location = new System.Drawing.Point(152, 8);
         this.CAddition1.MaxLength = 255;
         this.CAddition1.Multiline = true;
         this.CAddition1.Name = "CAddition1";
         this.CAddition1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.CAddition1.Size = new System.Drawing.Size(456, 60);
         this.CAddition1.TabIndex = 17;
         this.CAddition1.Text = "";
         this.CAddition1.UseCOrder = true;
         // 
         // label18
         // 
         this.label18.Location = new System.Drawing.Point(8, 234);
         this.label18.Name = "label18";
         this.label18.Size = new System.Drawing.Size(112, 16);
         this.label18.TabIndex = 94;
         this.label18.Text = "Дополнение 4";
         this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label14
         // 
         this.label14.Location = new System.Drawing.Point(8, 166);
         this.label14.Name = "label14";
         this.label14.Size = new System.Drawing.Size(112, 16);
         this.label14.TabIndex = 92;
         this.label14.Text = "Дополнение 3";
         this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label13
         // 
         this.label13.Location = new System.Drawing.Point(8, 98);
         this.label13.Name = "label13";
         this.label13.Size = new System.Drawing.Size(112, 16);
         this.label13.TabIndex = 91;
         this.label13.Text = "Дополнение 2";
         this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label12
         // 
         this.label12.Location = new System.Drawing.Point(8, 30);
         this.label12.Name = "label12";
         this.label12.Size = new System.Drawing.Size(112, 16);
         this.label12.TabIndex = 90;
         this.label12.Text = "Дополнение 1";
         this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label17
         // 
         this.label17.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label17.Dock = System.Windows.Forms.DockStyle.Top;
         this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label17.ForeColor = System.Drawing.SystemColors.Info;
         this.label17.Location = new System.Drawing.Point(0, 796);
         this.label17.Name = "label17";
         this.label17.Size = new System.Drawing.Size(614, 20);
         this.label17.TabIndex = 156;
         this.label17.Text = "Дополнения";
         this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // panel2
         // 
         this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.label23,
                                                                             this.label19,
                                                                             this.Cost2,
                                                                             this.Cost1,
                                                                             this.label9,
                                                                             this.label7});
         this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel2.Location = new System.Drawing.Point(0, 732);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(614, 64);
         this.panel2.TabIndex = 157;
         // 
         // label23
         // 
         this.label23.Location = new System.Drawing.Point(280, 36);
         this.label23.Name = "label23";
         this.label23.Size = new System.Drawing.Size(36, 20);
         this.label23.TabIndex = 166;
         this.label23.Text = "(у.е.)";
         // 
         // label19
         // 
         this.label19.Location = new System.Drawing.Point(280, 12);
         this.label19.Name = "label19";
         this.label19.Size = new System.Drawing.Size(36, 20);
         this.label19.TabIndex = 165;
         this.label19.Text = "(грн)";
         // 
         // Cost2
         // 
         this.Cost2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.Cost2.Location = new System.Drawing.Point(152, 36);
         this.Cost2.Maximum = new System.Decimal(new int[] {
                                                              -727379969,
                                                              232,
                                                              0,
                                                              0});
         this.Cost2.Name = "Cost2";
         this.Cost2.Size = new System.Drawing.Size(116, 22);
         this.Cost2.TabIndex = 16;
         // 
         // Cost1
         // 
         this.Cost1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.Cost1.Location = new System.Drawing.Point(152, 8);
         this.Cost1.Maximum = new System.Decimal(new int[] {
                                                              -727379969,
                                                              232,
                                                              0,
                                                              0});
         this.Cost1.Name = "Cost1";
         this.Cost1.Size = new System.Drawing.Size(116, 22);
         this.Cost1.TabIndex = 15;
         // 
         // label9
         // 
         this.label9.Location = new System.Drawing.Point(4, 36);
         this.label9.Name = "label9";
         this.label9.Size = new System.Drawing.Size(112, 16);
         this.label9.TabIndex = 160;
         this.label9.Text = "Валюта 2";
         this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label7
         // 
         this.label7.Location = new System.Drawing.Point(4, 8);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(112, 16);
         this.label7.TabIndex = 158;
         this.label7.Text = "Валюта 1";
         // 
         // label4
         // 
         this.label4.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label4.Dock = System.Windows.Forms.DockStyle.Top;
         this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label4.ForeColor = System.Drawing.SystemColors.Info;
         this.label4.Location = new System.Drawing.Point(0, 712);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(614, 20);
         this.label4.TabIndex = 161;
         this.label4.Text = "Стоимость";
         this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // panel1
         // 
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.label22,
                                                                             this.label21,
                                                                             this.comboBoxStartQuestionnaire,
                                                                             this.comboBoxFinishQuestionnaire});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Location = new System.Drawing.Point(0, 640);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(614, 72);
         this.panel1.TabIndex = 156;
         // 
         // label22
         // 
         this.label22.Location = new System.Drawing.Point(8, 40);
         this.label22.Name = "label22";
         this.label22.Size = new System.Drawing.Size(164, 24);
         this.label22.TabIndex = 0;
         this.label22.Text = "Заключительная анкета";
         // 
         // label21
         // 
         this.label21.Location = new System.Drawing.Point(8, 8);
         this.label21.Name = "label21";
         this.label21.Size = new System.Drawing.Size(164, 24);
         this.label21.TabIndex = 0;
         this.label21.Text = "Вступительная анкета";
         // 
         // comboBoxStartQuestionnaire
         // 
         this.comboBoxStartQuestionnaire.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.comboBoxStartQuestionnaire.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxStartQuestionnaire.Location = new System.Drawing.Point(172, 8);
         this.comboBoxStartQuestionnaire.Name = "comboBoxStartQuestionnaire";
         this.comboBoxStartQuestionnaire.Size = new System.Drawing.Size(435, 24);
         this.comboBoxStartQuestionnaire.TabIndex = 13;
         // 
         // comboBoxFinishQuestionnaire
         // 
         this.comboBoxFinishQuestionnaire.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.comboBoxFinishQuestionnaire.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxFinishQuestionnaire.Location = new System.Drawing.Point(172, 40);
         this.comboBoxFinishQuestionnaire.Name = "comboBoxFinishQuestionnaire";
         this.comboBoxFinishQuestionnaire.Size = new System.Drawing.Size(435, 24);
         this.comboBoxFinishQuestionnaire.TabIndex = 14;
         // 
         // label20
         // 
         this.label20.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label20.Dock = System.Windows.Forms.DockStyle.Top;
         this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label20.ForeColor = System.Drawing.SystemColors.Info;
         this.label20.Location = new System.Drawing.Point(0, 620);
         this.label20.Name = "label20";
         this.label20.Size = new System.Drawing.Size(614, 20);
         this.label20.TabIndex = 58;
         this.label20.Text = "Анкетирование";
         this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // panel6
         // 
         this.panel6.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.BrowseButton1,
                                                                             this.BrowseButton,
                                                                             this.CKeyWords,
                                                                             this.CPriorRequests,
                                                                             this.CFullDescription,
                                                                             this.label11,
                                                                             this.label10,
                                                                             this.label15,
                                                                             this.label8,
                                                                             this.CBriefDescription});
         this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel6.Location = new System.Drawing.Point(0, 404);
         this.panel6.Name = "panel6";
         this.panel6.Size = new System.Drawing.Size(614, 216);
         this.panel6.TabIndex = 155;
         // 
         // BrowseButton1
         // 
         this.BrowseButton1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.BrowseButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.BrowseButton1.Location = new System.Drawing.Point(519, 112);
         this.BrowseButton1.Name = "BrowseButton1";
         this.BrowseButton1.Size = new System.Drawing.Size(88, 20);
         this.BrowseButton1.TabIndex = 12;
         this.BrowseButton1.Text = "Выбор...";
         this.BrowseButton1.Click += new System.EventHandler(this.BrowseButton1_Click);
         // 
         // BrowseButton
         // 
         this.BrowseButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.BrowseButton.Location = new System.Drawing.Point(520, 76);
         this.BrowseButton.Name = "BrowseButton";
         this.BrowseButton.Size = new System.Drawing.Size(88, 20);
         this.BrowseButton.TabIndex = 10;
         this.BrowseButton.Text = "Выбор...";
         this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
         // 
         // CKeyWords
         // 
         this.CKeyWords.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.CKeyWords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.CKeyWords.CaptionLabel = null;
         this.CKeyWords.eId = "";
         this.CKeyWords.LanguageSwitcher = this.langSwitcher2;
         this.CKeyWords.Location = new System.Drawing.Point(152, 148);
         this.CKeyWords.MaxLength = 255;
         this.CKeyWords.Multiline = true;
         this.CKeyWords.Name = "CKeyWords";
         this.CKeyWords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.CKeyWords.Size = new System.Drawing.Size(456, 60);
         this.CKeyWords.TabIndex = 12;
         this.CKeyWords.Text = "";
         // 
         // CPriorRequests
         // 
         this.CPriorRequests.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.CPriorRequests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.CPriorRequests.CaptionLabel = null;
         this.CPriorRequests.eId = "";
         this.CPriorRequests.LanguageSwitcher = this.langSwitcher2;
         this.CPriorRequests.Location = new System.Drawing.Point(152, 112);
         this.CPriorRequests.MaxLength = 255;
         this.CPriorRequests.Name = "CPriorRequests";
         this.CPriorRequests.Size = new System.Drawing.Size(359, 22);
         this.CPriorRequests.TabIndex = 11;
         this.CPriorRequests.Text = "";
         // 
         // CFullDescription
         // 
         this.CFullDescription.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.CFullDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.CFullDescription.CaptionLabel = null;
         this.CFullDescription.eId = "";
         this.CFullDescription.LanguageSwitcher = this.langSwitcher2;
         this.CFullDescription.Location = new System.Drawing.Point(152, 76);
         this.CFullDescription.MaxLength = 255;
         this.CFullDescription.Multiline = true;
         this.CFullDescription.Name = "CFullDescription";
         this.CFullDescription.Size = new System.Drawing.Size(360, 20);
         this.CFullDescription.TabIndex = 10;
         this.CFullDescription.Text = "";
         // 
         // label11
         // 
         this.label11.Location = new System.Drawing.Point(8, 148);
         this.label11.Name = "label11";
         this.label11.Size = new System.Drawing.Size(136, 36);
         this.label11.TabIndex = 187;
         this.label11.Text = "Ключевые слова для поиска";
         this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label10
         // 
         this.label10.Location = new System.Drawing.Point(8, 108);
         this.label10.Name = "label10";
         this.label10.Size = new System.Drawing.Size(136, 32);
         this.label10.TabIndex = 185;
         this.label10.Text = "Предварительные требования";
         this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label15
         // 
         this.label15.Location = new System.Drawing.Point(8, 80);
         this.label15.Name = "label15";
         this.label15.Size = new System.Drawing.Size(136, 20);
         this.label15.TabIndex = 182;
         this.label15.Text = "Полное описание";
         this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label8
         // 
         this.label8.Location = new System.Drawing.Point(8, 16);
         this.label8.Name = "label8";
         this.label8.Size = new System.Drawing.Size(136, 20);
         this.label8.TabIndex = 180;
         this.label8.Text = "Краткое описание";
         this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // CBriefDescription
         // 
         this.CBriefDescription.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.CBriefDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.CBriefDescription.CaptionLabel = this.label8;
         this.CBriefDescription.ContentType = DCEAccessLib.ContentType._html;
         this.CBriefDescription.DataType = DCEAccessLib.dataFieldType.ntext;
         this.CBriefDescription.eId = "";
         this.CBriefDescription.EntType = DCEAccessLib.ContentType._html;
         this.CBriefDescription.LanguageSwitcher = this.langSwitcher2;
         this.CBriefDescription.Location = new System.Drawing.Point(152, 8);
         this.CBriefDescription.MaxLength = 255;
         this.CBriefDescription.Multiline = true;
         this.CBriefDescription.Name = "CBriefDescription";
         this.CBriefDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.CBriefDescription.Size = new System.Drawing.Size(456, 60);
         this.CBriefDescription.TabIndex = 9;
         this.CBriefDescription.Text = "";
         // 
         // label28
         // 
         this.label28.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label28.Dock = System.Windows.Forms.DockStyle.Top;
         this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label28.ForeColor = System.Drawing.SystemColors.Info;
         this.label28.Location = new System.Drawing.Point(0, 384);
         this.label28.Name = "label28";
         this.label28.Size = new System.Drawing.Size(614, 20);
         this.label28.TabIndex = 54;
         this.label28.Text = "Описание";
         this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // panel7
         // 
         this.panel7.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.courseFolder,
                                                                             this.LangCB,
                                                                             this.label24,
                                                                             this.checkBoxReady,
                                                                             this.CPublic,
                                                                             this.CActive,
                                                                             this.CDuration,
                                                                             this.CAuthor,
                                                                             this.CName,
                                                                             this.label27,
                                                                             this.CType,
                                                                             this.label26,
                                                                             this.CVersion,
                                                                             this.CCode,
                                                                             this.label16,
                                                                             this.label6,
                                                                             this.label5,
                                                                             this.label3,
                                                                             this.label2,
                                                                             this.label1});
         this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel7.Location = new System.Drawing.Point(0, 20);
         this.panel7.Name = "panel7";
         this.panel7.Size = new System.Drawing.Size(614, 364);
         this.panel7.TabIndex = 154;
         // 
         // courseFolder
         // 
         this.courseFolder.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.courseFolder.Location = new System.Drawing.Point(8, 308);
         this.courseFolder.Name = "courseFolder";
         this.courseFolder.Size = new System.Drawing.Size(603, 52);
         this.courseFolder.TabIndex = 184;
         // 
         // LangCB
         // 
         this.LangCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.LangCB.Location = new System.Drawing.Point(152, 276);
         this.LangCB.Name = "LangCB";
         this.LangCB.Size = new System.Drawing.Size(140, 24);
         this.LangCB.TabIndex = 183;
         // 
         // label24
         // 
         this.label24.Location = new System.Drawing.Point(8, 280);
         this.label24.Name = "label24";
         this.label24.Size = new System.Drawing.Size(140, 16);
         this.label24.TabIndex = 182;
         this.label24.Text = "Родной язык курса";
         // 
         // checkBoxReady
         // 
         this.checkBoxReady.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.checkBoxReady.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.checkBoxReady.Location = new System.Drawing.Point(8, 248);
         this.checkBoxReady.Name = "checkBoxReady";
         this.checkBoxReady.Size = new System.Drawing.Size(156, 16);
         this.checkBoxReady.TabIndex = 7;
         this.checkBoxReady.Text = "Готовность";
         // 
         // CPublic
         // 
         this.CPublic.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.CPublic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.CPublic.Location = new System.Drawing.Point(8, 168);
         this.CPublic.Name = "CPublic";
         this.CPublic.Size = new System.Drawing.Size(156, 16);
         this.CPublic.TabIndex = 5;
         this.CPublic.Text = "Публичность";
         // 
         // CActive
         // 
         this.CActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.CActive.Location = new System.Drawing.Point(152, 140);
         this.CActive.Name = "CActive";
         this.CActive.Size = new System.Drawing.Size(232, 16);
         this.CActive.TabIndex = 0;
         this.CActive.Text = "Курс не активный";
         // 
         // CDuration
         // 
         this.CDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.CDuration.Location = new System.Drawing.Point(152, 80);
         this.CDuration.Name = "CDuration";
         this.CDuration.Size = new System.Drawing.Size(32, 16);
         this.CDuration.TabIndex = 181;
         this.CDuration.Text = "0";
         this.CDuration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // CAuthor
         // 
         this.CAuthor.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.CAuthor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.CAuthor.CaptionLabel = null;
         this.CAuthor.eId = "";
         this.CAuthor.LanguageSwitcher = this.langSwitcher2;
         this.CAuthor.Location = new System.Drawing.Point(152, 196);
         this.CAuthor.MaxLength = 255;
         this.CAuthor.Multiline = true;
         this.CAuthor.Name = "CAuthor";
         this.CAuthor.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.CAuthor.Size = new System.Drawing.Size(456, 40);
         this.CAuthor.TabIndex = 6;
         this.CAuthor.Text = "";
         // 
         // CName
         // 
         this.CName.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.CName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.CName.CaptionLabel = null;
         this.CName.eId = "";
         this.CName.LanguageSwitcher = this.langSwitcher2;
         this.CName.Location = new System.Drawing.Point(152, 8);
         this.CName.MaxLength = 150;
         this.CName.Name = "CName";
         this.CName.Size = new System.Drawing.Size(456, 22);
         this.CName.TabIndex = 1;
         this.CName.Text = "";
         // 
         // label27
         // 
         this.label27.Location = new System.Drawing.Point(188, 78);
         this.label27.Name = "label27";
         this.label27.Size = new System.Drawing.Size(92, 16);
         this.label27.TabIndex = 175;
         this.label27.Text = " (раб. дни)";
         this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // CType
         // 
         this.CType.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.CType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.CType.DropDownWidth = 436;
         this.CType.ItemHeight = 16;
         this.CType.Location = new System.Drawing.Point(152, 108);
         this.CType.Name = "CType";
         this.CType.Size = new System.Drawing.Size(455, 24);
         this.CType.TabIndex = 4;
         // 
         // label26
         // 
         this.label26.Location = new System.Drawing.Point(8, 108);
         this.label26.Name = "label26";
         this.label26.Size = new System.Drawing.Size(112, 16);
         this.label26.TabIndex = 170;
         this.label26.Text = "Тип";
         this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // CVersion
         // 
         this.CVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.CVersion.Location = new System.Drawing.Point(360, 48);
         this.CVersion.MaxLength = 2;
         this.CVersion.Name = "CVersion";
         this.CVersion.Size = new System.Drawing.Size(20, 22);
         this.CVersion.TabIndex = 3;
         this.CVersion.Text = "";
         // 
         // CCode
         // 
         this.CCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.CCode.Location = new System.Drawing.Point(152, 48);
         this.CCode.MaxLength = 15;
         this.CCode.Name = "CCode";
         this.CCode.Size = new System.Drawing.Size(105, 22);
         this.CCode.TabIndex = 2;
         this.CCode.Text = "";
         // 
         // label16
         // 
         this.label16.Location = new System.Drawing.Point(8, 16);
         this.label16.Name = "label16";
         this.label16.Size = new System.Drawing.Size(112, 16);
         this.label16.TabIndex = 0;
         this.label16.Text = "Название";
         // 
         // label6
         // 
         this.label6.Location = new System.Drawing.Point(8, 140);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(112, 16);
         this.label6.TabIndex = 0;
         this.label6.Text = "Активность";
         this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label5
         // 
         this.label5.Location = new System.Drawing.Point(8, 200);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(112, 16);
         this.label5.TabIndex = 0;
         this.label5.Text = "Автор";
         this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(8, 78);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(112, 16);
         this.label3.TabIndex = 158;
         this.label3.Text = "Длительность";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(296, 50);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(60, 16);
         this.label2.TabIndex = 157;
         this.label2.Text = "Версия";
         this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(8, 50);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(112, 16);
         this.label1.TabIndex = 0;
         this.label1.Text = "Код";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // TitleLabel
         // 
         this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
         this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
         this.TitleLabel.Name = "TitleLabel";
         this.TitleLabel.Size = new System.Drawing.Size(614, 20);
         this.TitleLabel.TabIndex = 152;
         this.TitleLabel.Text = "Основные";
         this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // Create_Edit_ContextMenu
         // 
         this.Create_Edit_ContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                                this.FinalTestMenuItem,
                                                                                                this.VocabluraryMenuItem,
                                                                                                this.LibraryMenuItem});
         // 
         // FinalTestMenuItem
         // 
         this.FinalTestMenuItem.Index = 0;
         this.FinalTestMenuItem.Text = "Создать/Редактировать Заключительный тест";
         this.FinalTestMenuItem.Click += new System.EventHandler(this.FinalTestMenuItem_Click);
         // 
         // VocabluraryMenuItem
         // 
         this.VocabluraryMenuItem.Index = 1;
         this.VocabluraryMenuItem.Text = "Создать/Редактировать Словарь";
         this.VocabluraryMenuItem.Click += new System.EventHandler(this.VocabluraryMenuItem_Click);
         // 
         // LibraryMenuItem
         // 
         this.LibraryMenuItem.Index = 2;
         this.LibraryMenuItem.Text = "Создать/Редактировать Библиотеку";
         this.LibraryMenuItem.Click += new System.EventHandler(this.LibraryMenuItem_Click);
         // 
         // CourseEdit
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.MainPanel,
                                                                      this.TopPanel,
                                                                      this.ButtonPanel});
         this.Name = "CourseEdit";
         this.Size = new System.Drawing.Size(632, 500);
         this.TopPanel.ResumeLayout(false);
         this.ButtonPanel.ResumeLayout(false);
         this.MainPanel.ResumeLayout(false);
         this.panel4.ResumeLayout(false);
         this.panel5.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.Cost2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.Cost1)).EndInit();
         this.panel1.ResumeLayout(false);
         this.panel6.ResumeLayout(false);
         this.panel7.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         Node.EditRow["DiskFolder"] = courseFolder.DiskFolder.Text;
         Node.EditRow["Cost1"] = this.Cost1.Value;
         Node.EditRow["Cost2"] = this.Cost2.Value;
         
         this.Node.EndEdit(false, false);
      }

      private void ExitButton_Click(object sender, System.EventArgs e)
      {
         this.Node.EndEdit(true, Node.IsNew);
      }

      private void MenuButton_Click(object sender, System.EventArgs e)
      {
         // Создание или Редактирование Словаря, Библиотеки, Заключительного Теста
         this.Create_Edit_ContextMenu.Show(this.MenuButton, new Point(0,0));
      }

      private void BrowseButton_Click(object sender, System.EventArgs e)
      {
         MaterialsForm Matform = new MaterialsForm(this.CFullDescription.Text, 
            Node.EditRow["DiskFolder"].ToString(),
            "Выбор контента для полного описания", 
            ("Htm files|*.html;*.htm|All files|*.*"));

         if (Matform.ShowDialog() == DialogResult.OK)
         {
            this.CFullDescription.Text = Matform.Path;   
         }
      }

      private void BrowseButton1_Click(object sender, System.EventArgs e)
      {
         MaterialsForm Matform = new MaterialsForm(this.CPriorRequests.Text, 
            Node.EditRow["DiskFolder"].ToString(),
            "Выбор содержания предварительных требований", 
            ("Htm files|*.html;*.htm|All files|*.*"));

         if (Matform.ShowDialog() == DialogResult.OK)
         {
            this.CPriorRequests.Text = Matform.Path;   
         }
      }

      private void FinalTestMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.Node.IsNew)
            MessageBox.Show("Для создания теста необходимо вначале сохранить курс!","Сообщение",MessageBoxButtons.OK);
         else
         {
            // флаг, показывающий есть ли уже словарь для этого курса
            bool neednew = true;
         
            // Проверка нет ли уже открытой ноды "Словарь"
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(TestEditNode) )
               {
                  node.Select();
                  neednew = false;
                  break;
               }
            }
            
            if (neednew)
            {
               TestEditNode node = new TestEditNode(Node, "", 
                  Node.EditRow["id"].ToString(), true, "");
               node.Select();
            }
         }
      }

      private void VocabluraryMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.Node.IsNew)
            MessageBox.Show("Для создания словаря необходимо вначале сохранить курс!","Сообщение",MessageBoxButtons.OK);
         else
         {
            // флаг, показывающий есть ли уже словарь для этого курса
            bool neednew = true;
         
            // Проверка нет ли уже открытой ноды "Словарь"
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(VocabularyEditNode) )
               {
                  node.Select();
                  neednew = false;
                  break;
               }
            }
            
            if (neednew)
            {
               VocabularyEditNode node = new VocabularyEditNode(Node, Node.Id);
               node.Select();
            }
         }      
      }

      private void LibraryMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.Node.IsNew)
            MessageBox.Show("Для создания библиотеки необходимо вначале сохранить курс!","Сообщение",MessageBoxButtons.OK);
         else
         {
            // Открыть окно редактирования библиотеки
         
            // флаг, показывающий есть ли уже библиотека для этого курса
            bool neednew = true;
         
            // Проверка нет ли уже открытой ноды "Библиотека"
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(LibraryEditNode) )
               {
                  if ( ((LibraryEditNode)node).EditRow["Parent"].ToString() == Node.EditRow["id"].ToString())
                  {
                     node.Select();
                     neednew = false;
                     break;
                  }
               }
            }
            if (neednew)
            {
               LibraryEditNode node = new LibraryEditNode(Node, "", 
                  Node.EditRow["id"].ToString(), Node.CourseName, true);
               node.Select();
            }
         }
      }
	}

   /// <summary>
   /// Нода отвечающая за редактирование свойств курса
   /// </summary>
   public class CourseEditNode : HighlightedRecordEditNode 
   {
      public CourseEditNode(NodeControl parent, string id, string domainid, string diskfolder)
         : base(parent, "select * from dbo.Courses", "Courses", "id", id, DCEUser.CurrentUser.ReadOnlyCourses)
      {
         domainId = domainid;
         
         defLang = (int)EditRow["CourseLanguage"];

         this.diskfolder = diskfolder;
         
         if (EditRow.IsNew && diskfolder == null)
         {
            throw new DCEException("При создании курса не был определен путь курса!", DCEException.ExceptionLevel.ErrorContinue);
         }

         if (EditRow.IsNew)
         {
            if (this.domainId != null && this.domainId != "") 
               EditRow ["Area"] = this.domainId;
            EditRow["DiskFolder"] = diskfolder;
         }
         else
         {
            courseName = DCEAccessLib.DCEWebAccess.WebAccess.GetString(
               "select dbo.GetStrContentAlt2(Name, " + defLang + ", 'RU') as RName from Courses where id = '" + id + "'");
         }

         this.Expand();
      }

      private int defLang;
      public int DefLang
      {
         get 
         { 
            return defLang; 
         }
         set 
         { 
            defLang = value; 
            
            CourseName = DCEAccessLib.DCEWebAccess.WebAccess.GetString(
               "select dbo.GetStrContentAlt2(Name, " + defLang + ", 'RU') as RName from Courses where id = '" + this.Id + "'");
         }
      }

      private string courseName;
      public string CourseName
      {
         get 
         {
            return courseName;
         }
         set
         {
            courseName = value;
            this.CaptionChanged();
         }
      }
      
      private string diskfolder;
      
      private string domainId;
      public string DomainId
      {
         get { return domainId; }
      }

      public override bool HaveChildNodes() { return true; }
      
      public override void CreateChilds()
      {
         // создание ноды "Темы"

         new ThemeListNode(this, this.Id, ThemeListNodeTypes.tlnMain, false);

         // cоздание ноды "Тест"
            
         DataSet dstest = DCEWebAccess.WebAccess.GetDataSet(
               
            "select * from dbo.Tests where " + 
               
            "Parent = '" + this.EditRow["id"].ToString() + "' and " +  
               
            "Type = " + (int)TestType.test,         
               
            "Tests");

         if (dstest.Tables["Tests"].Rows.Count > 0)
         {
            DataRow row = dstest.Tables["Tests"].Rows[0];
               
            new TestEditNode(this, row["id"].ToString(), this.Id, true, "");
         }

         // создание ноды "Библиотека"

         System.Data.DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
         
            "select *, dbo.GetStrContentAlt2(Name, " + DefLang + ", 'RU') as RName, Parent from Themes " + 
         
            "where Parent = '" + Id + "' and " + 
            
            "Type = " + ((int)ThemeType.library).ToString()  +
   
            " ORDER BY Type", "Themes");

         System.Data.DataView dv = new System.Data.DataView(ds.Tables["Themes"]);
         
         foreach (DataRowView row in dv)
         {
            if (row["Type"].ToString() == ((int)ThemeType.library).ToString())
            {
               new LibraryEditNode(this, row["id"].ToString(), EditRow["id"].ToString(), row["RName"].ToString(), true);
            }
         }

         // создание ноды "Словарь"
         
         new VocabularyEditNode(this, this.Id);
      }

      public override UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new CourseEdit(this);
         }
         
         if (needRefresh)
         {
            ((CourseEdit)this.fControl).RefreshData();
            needRefresh = false;
         }

         return this.fControl;
      }

      public override String GetCaption()
      {
         if (EditRow == null)
            return "";
         if (IsNew)
            return "Курс: (Новый)";
         else
            return "Курс: " + CourseName;
      }

      public override void ReleaseControl()
      {
         // do nothing
      }
      
      public override bool CanClose()
      {
         return true;
      }

      /// <summary>
      /// Initialize new record for editing (EditRow)
      /// </summary>
      protected override void InitNewRecord()
      {
         // filling all non-nullable fields
         EditRow ["id"] = System.Guid.NewGuid().ToString();
         EditRow ["Name"] = System.Guid.NewGuid().ToString();
         EditRow ["Version"]  = "";
         EditRow ["Code"]  = "";
         EditRow ["Author"]  = System.Guid.NewGuid().ToString();
         EditRow ["DescriptionShort"]  = System.Guid.NewGuid().ToString();
         EditRow ["DescriptionLong"]  = System.Guid.NewGuid().ToString();
         EditRow ["Requirements"]  = System.Guid.NewGuid().ToString();
         EditRow ["Keywords"]  = System.Guid.NewGuid().ToString();
         EditRow ["Additions"]  = System.Guid.NewGuid().ToString();
         EditRow ["Instructors"]  = System.Guid.NewGuid().ToString();
         EditRow ["CPublic"]  = false.ToString();
         EditRow ["DiskFolder"]  = diskfolder;
         EditRow ["Cost1"]  = 0;
         EditRow ["Cost2"]  = 0;
         EditRow ["isReady"] = 0;
         EditRow ["CourseLanguage"] = 1;
      }
      
      public bool IsCourseActive
      {
         get { return ((CourseEdit)this.fControl).IsCourseActive(); }
      }
   }


}
