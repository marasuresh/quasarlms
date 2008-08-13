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
	/// Практическая работа
	/// </summary>
	public class WorkEdit : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Panel LangPanel;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private DCEAccessLib.LangSwitcher TopLangSwitcher;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.ContextMenu WorksContextMenu;
      private System.Windows.Forms.MenuItem CreateMenuItem;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem EditMenuItem;
      private System.Windows.Forms.MenuItem RemoveMenuItem;
      private System.Windows.Forms.Panel WorkTasksPanel;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Panel MainPanel;
      private DCEAccessLib.MLEdit NameTextBox;
      private System.Windows.Forms.Label NameLabel;
      private System.Windows.Forms.Label TitleLabel;
      private System.Windows.Forms.Panel ButtonsPanel;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.GroupBox ShowHintsGroupBox;
      private System.Windows.Forms.RadioButton BothRadioButton;
      private System.Windows.Forms.RadioButton OneRadioButton;
      private System.Windows.Forms.RadioButton NoRadioButton;
      private DCECourseEditor.QuestionList questionList;

      private WorkEditNode Node;

		public WorkEdit(WorkEditNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         Node = node;

         this.SaveButton.Enabled = DCEUser.CurrentUser.EditableCourses;

         CourseEditNode coursenode = (CourseEditNode)Node.GetSpecifiedParentNode("DCECourseEditor.CourseEditNode");
         if ( coursenode != null)
         {
            if (coursenode.IsCourseActive)
            {
               NameTextBox.Enabled = false;
               ShowHintsGroupBox.Enabled = false;
            }
            
            this.TopLangSwitcher.Language = coursenode.DefLang;
         }

         this.NameTextBox.SetParentNode(Node, "Themes");

         questionList.SetParentNode(Node, "Tests");

         this.RebindControls();
         
         this.Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(this.RebindControls);
		}
      
      public void RefreshData()
      {
         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(

            "select *, " + 
            
            "dbo.GetStrContentAlt(Content, 'RU', 'EN') as RContent " + 

            "from TestQuestions " + 
         
            "where Test = '" + Node.GetEditContent("Tests").editRow["id"].ToString() + "'", "dbo.TestQuestions");

         dataView.Table = dataSet.Tables["dbo.TestQuestions"];

         questionList.RefreshData();
      }
      
      public void RebindControls()
      {
         this.NameTextBox.DataBindings.Clear();
         this.NameTextBox.DataBindings.Add("eid", Node.GetEditContent("Themes").editRow, "Name");

         NoRadioButton.Checked = false;
         OneRadioButton.Checked = false;
         BothRadioButton.Checked = false;

         string hints = this.Node.GetEditContent("Tests").editRow["Hints"].ToString();
         
         if (hints == ((int)HintType.none).ToString())
            NoRadioButton.Checked = true;
         
         else if (hints == ((int)HintType.one).ToString())
            OneRadioButton.Checked = true;
         
         else if (hints == ((int)HintType.both).ToString())
            BothRadioButton.Checked = true;
         
         else
            NoRadioButton.Checked = true;

         RefreshData();
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
         this.TopLangSwitcher = new DCEAccessLib.LangSwitcher();
         this.dataSet = new System.Data.DataSet();
         this.dataView = new System.Data.DataView();
         this.WorksContextMenu = new System.Windows.Forms.ContextMenu();
         this.CreateMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.EditMenuItem = new System.Windows.Forms.MenuItem();
         this.RemoveMenuItem = new System.Windows.Forms.MenuItem();
         this.ButtonsPanel = new System.Windows.Forms.Panel();
         this.SaveButton = new System.Windows.Forms.Button();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.WorkTasksPanel = new System.Windows.Forms.Panel();
         this.label1 = new System.Windows.Forms.Label();
         this.MainPanel = new System.Windows.Forms.Panel();
         this.ShowHintsGroupBox = new System.Windows.Forms.GroupBox();
         this.BothRadioButton = new System.Windows.Forms.RadioButton();
         this.OneRadioButton = new System.Windows.Forms.RadioButton();
         this.NoRadioButton = new System.Windows.Forms.RadioButton();
         this.NameTextBox = new DCEAccessLib.MLEdit();
         this.NameLabel = new System.Windows.Forms.Label();
         this.TitleLabel = new System.Windows.Forms.Label();
         this.questionList = new DCECourseEditor.QuestionList();
         this.LangPanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.ButtonsPanel.SuspendLayout();
         this.WorkTasksPanel.SuspendLayout();
         this.MainPanel.SuspendLayout();
         this.ShowHintsGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // LangPanel
         // 
         this.LangPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.LangPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.TopLangSwitcher});
         this.LangPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.LangPanel.Name = "LangPanel";
         this.LangPanel.Size = new System.Drawing.Size(640, 40);
         this.LangPanel.TabIndex = 1;
         // 
         // TopLangSwitcher
         // 
         this.TopLangSwitcher.Location = new System.Drawing.Point(8, 8);
         this.TopLangSwitcher.Name = "TopLangSwitcher";
         this.TopLangSwitcher.Size = new System.Drawing.Size(188, 24);
         this.TopLangSwitcher.TabIndex = 1;
         this.TopLangSwitcher.TextLabel = "Язык";
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
         // 
         // WorksContextMenu
         // 
         this.WorksContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                         this.CreateMenuItem,
                                                                                         this.menuItem2,
                                                                                         this.EditMenuItem,
                                                                                         this.RemoveMenuItem});
         // 
         // CreateMenuItem
         // 
         this.CreateMenuItem.Index = 0;
         this.CreateMenuItem.Text = "Создать";
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 1;
         this.menuItem2.Text = "-";
         // 
         // EditMenuItem
         // 
         this.EditMenuItem.Index = 2;
         this.EditMenuItem.Text = "Редактировать";
         // 
         // RemoveMenuItem
         // 
         this.RemoveMenuItem.Index = 3;
         this.RemoveMenuItem.Text = "Удалить";
         // 
         // ButtonsPanel
         // 
         this.ButtonsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ButtonsPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.SaveButton,
                                                                                   this.buttonCancel});
         this.ButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.ButtonsPanel.Location = new System.Drawing.Point(0, 452);
         this.ButtonsPanel.Name = "ButtonsPanel";
         this.ButtonsPanel.Size = new System.Drawing.Size(640, 44);
         this.ButtonsPanel.TabIndex = 2;
         // 
         // SaveButton
         // 
         this.SaveButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SaveButton.Location = new System.Drawing.Point(430, 6);
         this.SaveButton.Name = "SaveButton";
         this.SaveButton.Size = new System.Drawing.Size(96, 28);
         this.SaveButton.TabIndex = 6;
         this.SaveButton.Text = "Сохранить";
         this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
         // 
         // buttonCancel
         // 
         this.buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.buttonCancel.Location = new System.Drawing.Point(534, 6);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(96, 28);
         this.buttonCancel.TabIndex = 5;
         this.buttonCancel.Text = "Отменить";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // WorkTasksPanel
         // 
         this.WorkTasksPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                     this.questionList,
                                                                                     this.label1});
         this.WorkTasksPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.WorkTasksPanel.Location = new System.Drawing.Point(0, 148);
         this.WorkTasksPanel.Name = "WorkTasksPanel";
         this.WorkTasksPanel.Size = new System.Drawing.Size(640, 304);
         this.WorkTasksPanel.TabIndex = 5;
         // 
         // label1
         // 
         this.label1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label1.Dock = System.Windows.Forms.DockStyle.Top;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label1.ForeColor = System.Drawing.SystemColors.Info;
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(640, 20);
         this.label1.TabIndex = 172;
         this.label1.Text = "Список вопросов";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // MainPanel
         // 
         this.MainPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.ShowHintsGroupBox,
                                                                                this.NameTextBox,
                                                                                this.NameLabel,
                                                                                this.TitleLabel});
         this.MainPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.MainPanel.Location = new System.Drawing.Point(0, 40);
         this.MainPanel.Name = "MainPanel";
         this.MainPanel.Size = new System.Drawing.Size(640, 108);
         this.MainPanel.TabIndex = 4;
         // 
         // ShowHintsGroupBox
         // 
         this.ShowHintsGroupBox.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.BothRadioButton,
                                                                                        this.OneRadioButton,
                                                                                        this.NoRadioButton});
         this.ShowHintsGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ShowHintsGroupBox.Location = new System.Drawing.Point(4, 56);
         this.ShowHintsGroupBox.Name = "ShowHintsGroupBox";
         this.ShowHintsGroupBox.Size = new System.Drawing.Size(460, 48);
         this.ShowHintsGroupBox.TabIndex = 205;
         this.ShowHintsGroupBox.TabStop = false;
         this.ShowHintsGroupBox.Text = "Использование подсказок";
         // 
         // BothRadioButton
         // 
         this.BothRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.BothRadioButton.Location = new System.Drawing.Point(312, 12);
         this.BothRadioButton.Name = "BothRadioButton";
         this.BothRadioButton.Size = new System.Drawing.Size(144, 28);
         this.BothRadioButton.TabIndex = 2;
         this.BothRadioButton.Text = "Показывать обе";
         this.BothRadioButton.CheckedChanged += new System.EventHandler(this.BothRadioButton_CheckedChanged);
         // 
         // OneRadioButton
         // 
         this.OneRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.OneRadioButton.Location = new System.Drawing.Point(140, 12);
         this.OneRadioButton.Name = "OneRadioButton";
         this.OneRadioButton.Size = new System.Drawing.Size(172, 28);
         this.OneRadioButton.TabIndex = 1;
         this.OneRadioButton.Text = "Показывать краткую";
         this.OneRadioButton.CheckedChanged += new System.EventHandler(this.OneRadioButton_CheckedChanged);
         // 
         // NoRadioButton
         // 
         this.NoRadioButton.Checked = true;
         this.NoRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.NoRadioButton.Location = new System.Drawing.Point(4, 12);
         this.NoRadioButton.Name = "NoRadioButton";
         this.NoRadioButton.Size = new System.Drawing.Size(136, 28);
         this.NoRadioButton.TabIndex = 0;
         this.NoRadioButton.TabStop = true;
         this.NoRadioButton.Text = "Не показывать";
         this.NoRadioButton.CheckedChanged += new System.EventHandler(this.NoRadioButton_CheckedChanged);
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
         this.NameTextBox.LanguageSwitcher = this.TopLangSwitcher;
         this.NameTextBox.Location = new System.Drawing.Point(120, 28);
         this.NameTextBox.MaxLength = 255;
         this.NameTextBox.Name = "NameTextBox";
         this.NameTextBox.Size = new System.Drawing.Size(512, 20);
         this.NameTextBox.TabIndex = 180;
         this.NameTextBox.Text = "";
         // 
         // NameLabel
         // 
         this.NameLabel.Location = new System.Drawing.Point(8, 28);
         this.NameLabel.Name = "NameLabel";
         this.NameLabel.TabIndex = 179;
         this.NameLabel.Text = "Название";
         // 
         // TitleLabel
         // 
         this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
         this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
         this.TitleLabel.Name = "TitleLabel";
         this.TitleLabel.Size = new System.Drawing.Size(640, 20);
         this.TitleLabel.TabIndex = 172;
         this.TitleLabel.Text = "Основные";
         this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // questionList
         // 
         this.questionList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.questionList.Location = new System.Drawing.Point(0, 20);
         this.questionList.Name = "questionList";
         this.questionList.QuestionType = DCECourseEditor.QuestionListType.qltWork;
         this.questionList.Size = new System.Drawing.Size(640, 284);
         this.questionList.TabIndex = 173;
         // 
         // WorkEdit
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.WorkTasksPanel,
                                                                      this.MainPanel,
                                                                      this.ButtonsPanel,
                                                                      this.LangPanel});
         this.Name = "WorkEdit";
         this.Size = new System.Drawing.Size(640, 496);
         this.LangPanel.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.ButtonsPanel.ResumeLayout(false);
         this.WorkTasksPanel.ResumeLayout(false);
         this.MainPanel.ResumeLayout(false);
         this.ShowHintsGroupBox.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         HintType type = HintType.none;
         
         if (OneRadioButton.Checked)
            type = HintType.one;
         else if (BothRadioButton.Checked)
            type = HintType.both;

         this.Node.GetEditContent("Tests").editRow["Hints"] = (int)type;

         this.Node.EndEdit(false, false);
      }

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         this.Node.EndEdit(true, Node.IsNew);
      }

      private void NoRadioButton_CheckedChanged(object sender, System.EventArgs e)
      {
         if (NoRadioButton.Checked)
         {
            OneRadioButton.Checked = false;
            BothRadioButton.Checked = false;
         }
      }

      private void OneRadioButton_CheckedChanged(object sender, System.EventArgs e)
      {
         if (OneRadioButton.Checked)
         {
            NoRadioButton.Checked = false;
            BothRadioButton.Checked = false;
         }
      }

      private void BothRadioButton_CheckedChanged(object sender, System.EventArgs e)
      {
         if (BothRadioButton.Checked)
         {
            NoRadioButton.Checked = false;
            OneRadioButton.Checked = false;
         }
      }
	}
	public class WorkEditNode : RecordEditNode
	{
		public WorkEditNode(NodeControl parent, string workid, string parentThemeId, string workname)
			: base(parent,
			// WARNING : Порядок не менять
		   new EditRowContent<string>[] {
                                 new EditRowContent<string>(
                                    "select * from dbo.Themes", 
                                    "Themes", 
                                    "id", 
                                    workid, 
                                    "Themes"),
                                 new EditRowContent<string>(
                                    "select * from dbo.Tests", 
                                    "Tests", 
                                    "id", 
                                    RecordEditNode.GetFieldValue(
                                       "select * from dbo.Themes",
                                       "Themes",
                                       "id", 
                                       workid, 
                                       "Practice"), 
                                    "Tests")})
		{
			if (this.GetEditContent("Themes").IsNew)
				this.GetEditContent("Themes").editRow["Parent"] = parentThemeId;

			WorkName = workname;
		}

		private string workName = "";
		public string WorkName
		{
			get
			{
				return workName;
			}
			set
			{
				this.workName = value;
				this.CaptionChanged();
			}
		}

		public override bool HaveChildNodes() { return false; }

		public override System.Windows.Forms.UserControl GetControl()
		{
			if (this.fControl == null) {
				fControl = new WorkEdit(this);
			}

			if (needRefresh) {
				((WorkEdit)this.fControl).RefreshData();
				needRefresh = false;
			}

			return this.fControl;
		}

		public override String GetCaption()
		{
			if (this.GetEditContent("Themes") != null) {
				if (this.GetEditContent("Themes").IsNew)
					return "[Новая Практическая Работа]";
				else
					return "Практическая Работа : " + this.WorkName;
			} else
				return "Практическая Работа";
		}

		public override void ReleaseControl()
		{
			// do nothing
		}

		public override bool CanClose()
		{
			return false;
		}

		string workidguid = System.Guid.NewGuid().ToString();
		string themeguid = System.Guid.NewGuid().ToString();

		protected override void InitNewRecord(string qualif)
		{
			switch (qualif) {
				case "Themes": {
						GetEditContent("Themes").editRow["id"] = themeguid;
						GetEditContent("Themes").editRow["Name"] = System.Guid.NewGuid().ToString();
						GetEditContent("Themes").editRow["Practice"] = workidguid;
						GetEditContent("Themes").editRow["TOrder"] = 0;
						GetEditContent("Themes").editRow["Content"] = System.Guid.NewGuid().ToString();
						GetEditContent("Themes").editRow["Duration"] = 1;
						GetEditContent("Themes").editRow["Mandatory"] = true;
					}
					break;
				case "Tests": {
						GetEditContent("Tests").editRow["id"] = workidguid;
						GetEditContent("Tests").editRow["Parent"] = themeguid;
						GetEditContent("Tests").editRow["Type"] = (int)TestType.practice;
						GetEditContent("Tests").editRow["Duration"] = 0;
						GetEditContent("Tests").editRow["Points"] = 0;
						GetEditContent("Tests").editRow["Show"] = 1;
						GetEditContent("Tests").editRow["Split"] = 0;
						GetEditContent("Tests").editRow["AutoFinish"] = 0;
						GetEditContent("Tests").editRow["DefLanguage"] = 1;
						GetEditContent("Tests").editRow["CanSwitchLang"] = 0;
						GetEditContent("Tests").editRow["Hints"] = (int)HintType.none;
					}
					break;
			}
		}
	}
}
