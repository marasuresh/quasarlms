using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;
using System.Xml;

namespace DCECourseEditor
{
   /* QuestionEdit */
   
   /// <summary>
   /// Редактирование вопроса теста
   /// </summary>
   public class QuestionEdit : System.Windows.Forms.UserControl
   {
      #region Form definitions
      private System.Windows.Forms.Panel LangPanel;
      private System.Windows.Forms.Panel ButtonPanel;
      private System.Windows.Forms.Panel MainPanel;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label TitleLabel;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button buttonCancel;
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;
      private System.Windows.Forms.Button BrowseButton;
      private SpinEdit PointsNumericUpDown;
      private DCEAccessLib.MLEdit LongHintMLEdit;
      private DCEAccessLib.MLEdit ShortHintMLEdit;
      private DCEAccessLib.MLEdit QuestionMLEdit;
      private DCEAccessLib.LangSwitcher TopLangSwitcher;
      private System.Windows.Forms.ComboBox TypeComboBox;
      private System.Windows.Forms.Panel TestOnlyPanel;
      private System.Windows.Forms.Panel AnswerPanel;
      private System.Windows.Forms.Label label9;
      private static XmlDocument doc = new XmlDocument();
      private DCECourseEditor.AnswerList AnswerList;
      private System.Windows.Forms.Panel HintPanel;
      private System.Windows.Forms.Panel ThemePanel;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.ComboBox ThemeCB;
      #endregion

      private QuestionEditNode Node;

      private string ParentThemeId = null;

      public QuestionEdit(QuestionEditNode node, QuestionListType enumparam)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
         
         switch (enumparam)
         {
            case QuestionListType.qltQuestionnaire :
               TestOnlyPanel.Visible = false;
               HintPanel.Visible = false;
               TypeComboBox.Visible = false;
               BrowseButton.Visible = false;
               ThemePanel.Visible = false;
               break;
            case QuestionListType.qltTest :
               TestOnlyPanel.Visible = true;
               HintPanel.Visible = true;
               ThemePanel.Visible = true;
               break;
            case QuestionListType.qltWork :
               TestOnlyPanel.Visible = false;
               HintPanel.Visible = true;
               ThemePanel.Visible = true;
               break;
         }

         Node = node;

         this.SaveButton.Enabled = DCEUser.CurrentUser.EditableCourses;

         this.AnswerList.AnswerListType = enumparam;

         this.QuestionMLEdit.SetParentNode(Node);
         this.ShortHintMLEdit.SetParentNode(Node);
         this.LongHintMLEdit.SetParentNode(Node);
         this.AnswerList.SetParentNode(Node);

         NodeControl pnode = Node.GetSpecifiedParentNode("DCECourseEditor.CourseEditNode");
         if (pnode != null && ((CourseEditNode)pnode).IsCourseActive)
         {
            this.QuestionMLEdit.Enabled = false;
            this.ShortHintMLEdit.Enabled = false;
            this.LongHintMLEdit.Enabled = false;
            this.AnswerList.Enabled = false;
            this.TypeComboBox.Enabled = false;
            this.ThemeCB.Enabled = false;
            this.PointsNumericUpDown.Enabled = false;
         }
         
         this.Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(this.RebindControls);

         this.TypeComboBox.Items.Add(new QuestionsType(QuestionsType.QuestionsEnum.text));
         
         CourseEditNode coursenode = (CourseEditNode)Node.GetSpecifiedParentNode("DCECourseEditor.CourseEditNode");
         if ( coursenode != null)
            this.TopLangSwitcher.Language = coursenode.DefLang;
         
         if (enumparam != QuestionListType.qltQuestionnaire)
         {
            this.TypeComboBox.Items.Add(new QuestionsType(QuestionsType.QuestionsEnum.url));
            this.TypeComboBox.Items.Add(new QuestionsType(QuestionsType.QuestionsEnum.obj));
            
            pnode = Node.GetSpecifiedParentNode("DCECourseEditor.ThemeEditNode");
         
            if (pnode == null)
            {
               // родитель - курс
               pnode = Node.GetSpecifiedParentNode("DCECourseEditor.CourseEditNode");
               if (pnode != null)
               {
                  ParentThemeId = ((CourseEditNode)pnode).Id;
               }
            }
            else
            {
               // родитель - тема
               ParentThemeId = ((ThemeEditNode)pnode).Id;
            }
         }
         
         this.RebindControls();

         SelectType((int)Node.EditRow["Type"]);
         
         RefreshData();
      }

      private void CustomFillComboBox(ComboBox c, string query, string tablename, string displaymember, string valuemember, string fieldname)
      {
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            query,
            tablename);

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

      public void RebindControls()
      {
         this.QuestionMLEdit.DataBindings.Clear();
         this.QuestionMLEdit.DataBindings.Add("eId", Node.EditRow, "Content");

         this.ShortHintMLEdit.DataBindings.Clear();
         this.ShortHintMLEdit.DataBindings.Add("eId", Node.EditRow, "ShortHint");
         
         this.LongHintMLEdit.DataBindings.Clear();
         this.LongHintMLEdit.DataBindings.Add("eId", Node.EditRow, "LongHint");
         
         this.PointsNumericUpDown.DataBindings.Clear();
         this.PointsNumericUpDown.DataBindings.Add("Value", Node.EditRow, "Points");
         
         if (ParentThemeId != null)
         {
            this.ThemeCB.DataBindings.Clear();
            this.ThemeCB.DataBindings.Add("SelectedValue", Node.EditRow, "Theme");
         }

         QuestionsType q = this.TypeComboBox.SelectedItem as QuestionsType;

         this.AnswerList.DataBindings.Clear();
         this.AnswerList.ClearParentNode(this.Node);
         
         this.AnswerList.DataBindings.Add("eId", Node.EditRow, "Answer");
         this.AnswerList.SetParentNode(this.Node);
      }

      public void RefreshData()
      {
         this.Node.RefreshQuestionName();
         
         if (ParentThemeId != null)
         {
            CustomFillComboBox(
               ThemeCB, 
               "select id, dbo.GetStrContentAlt(Name, 'RU', 'EN') as RName from Themes where Parent = '" + 
               ParentThemeId + "' and Type = " + (int)ThemeType.theme + " and Practice is Null order by TOrder", 
               "Themes", 
               "RName", 
               "id",
               "Theme");
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
         this.LangPanel = new System.Windows.Forms.Panel();
         this.TopLangSwitcher = new DCEAccessLib.LangSwitcher();
         this.ButtonPanel = new System.Windows.Forms.Panel();
         this.SaveButton = new System.Windows.Forms.Button();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.MainPanel = new System.Windows.Forms.Panel();
         this.BrowseButton = new System.Windows.Forms.Button();
         this.TypeComboBox = new System.Windows.Forms.ComboBox();
         this.label1 = new System.Windows.Forms.Label();
         this.QuestionMLEdit = new DCEAccessLib.MLEdit();
         this.TitleLabel = new System.Windows.Forms.Label();
         this.LongHintMLEdit = new DCEAccessLib.MLEdit();
         this.label2 = new System.Windows.Forms.Label();
         this.ShortHintMLEdit = new DCEAccessLib.MLEdit();
         this.label3 = new System.Windows.Forms.Label();
         this.PointsNumericUpDown = new SpinEdit();
         this.label4 = new System.Windows.Forms.Label();
         this.TestOnlyPanel = new System.Windows.Forms.Panel();
         this.AnswerPanel = new System.Windows.Forms.Panel();
         this.AnswerList = new DCECourseEditor.AnswerList();
         this.HintPanel = new System.Windows.Forms.Panel();
         this.label9 = new System.Windows.Forms.Label();
         this.ThemePanel = new System.Windows.Forms.Panel();
         this.ThemeCB = new System.Windows.Forms.ComboBox();
         this.label5 = new System.Windows.Forms.Label();
         this.LangPanel.SuspendLayout();
         this.ButtonPanel.SuspendLayout();
         this.MainPanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.PointsNumericUpDown)).BeginInit();
         this.TestOnlyPanel.SuspendLayout();
         this.AnswerPanel.SuspendLayout();
         this.HintPanel.SuspendLayout();
         this.ThemePanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // LangPanel
         // 
         this.LangPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.LangPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.TopLangSwitcher});
         this.LangPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.LangPanel.Name = "LangPanel";
         this.LangPanel.Size = new System.Drawing.Size(647, 40);
         this.LangPanel.TabIndex = 3;
         // 
         // TopLangSwitcher
         // 
         this.TopLangSwitcher.Location = new System.Drawing.Point(8, 8);
         this.TopLangSwitcher.Name = "TopLangSwitcher";
         this.TopLangSwitcher.Size = new System.Drawing.Size(184, 24);
         this.TopLangSwitcher.TabIndex = 1;
         this.TopLangSwitcher.TextLabel = "Язык";
         // 
         // ButtonPanel
         // 
         this.ButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ButtonPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.SaveButton,
                                                                                  this.buttonCancel});
         this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.ButtonPanel.Location = new System.Drawing.Point(0, 560);
         this.ButtonPanel.Name = "ButtonPanel";
         this.ButtonPanel.Size = new System.Drawing.Size(647, 44);
         this.ButtonPanel.TabIndex = 4;
         // 
         // SaveButton
         // 
         this.SaveButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SaveButton.Location = new System.Drawing.Point(438, 6);
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
         this.buttonCancel.Location = new System.Drawing.Point(542, 6);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(96, 28);
         this.buttonCancel.TabIndex = 5;
         this.buttonCancel.Text = "Отменить";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // MainPanel
         // 
         this.MainPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.BrowseButton,
                                                                                this.TypeComboBox,
                                                                                this.label1,
                                                                                this.QuestionMLEdit,
                                                                                this.TitleLabel});
         this.MainPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.MainPanel.Location = new System.Drawing.Point(0, 40);
         this.MainPanel.Name = "MainPanel";
         this.MainPanel.Size = new System.Drawing.Size(647, 92);
         this.MainPanel.TabIndex = 6;
         // 
         // BrowseButton
         // 
         this.BrowseButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.BrowseButton.Location = new System.Drawing.Point(552, 65);
         this.BrowseButton.Name = "BrowseButton";
         this.BrowseButton.Size = new System.Drawing.Size(84, 23);
         this.BrowseButton.TabIndex = 205;
         this.BrowseButton.Text = "Выбор...";
         this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
         // 
         // TypeComboBox
         // 
         this.TypeComboBox.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.TypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.TypeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.TypeComboBox.Location = new System.Drawing.Point(552, 28);
         this.TypeComboBox.Name = "TypeComboBox";
         this.TypeComboBox.Size = new System.Drawing.Size(84, 21);
         this.TypeComboBox.TabIndex = 204;
         this.TypeComboBox.SelectedIndexChanged += new System.EventHandler(this.TypeComboBox_SelectedIndexChanged);
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(8, 28);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(124, 24);
         this.label1.TabIndex = 196;
         this.label1.Text = "Вопрос";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // QuestionMLEdit
         // 
         this.QuestionMLEdit.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.QuestionMLEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.QuestionMLEdit.CaptionLabel = this.label1;
         this.QuestionMLEdit.ContentType = DCEAccessLib.ContentType._html;
         this.QuestionMLEdit.DataType = DCEAccessLib.dataFieldType.ntext;
         this.QuestionMLEdit.eId = null;
         this.QuestionMLEdit.EntType = DCEAccessLib.ContentType._html;
         this.QuestionMLEdit.LanguageSwitcher = this.TopLangSwitcher;
         this.QuestionMLEdit.Location = new System.Drawing.Point(136, 28);
         this.QuestionMLEdit.MaxLength = 4000;
         this.QuestionMLEdit.Multiline = true;
         this.QuestionMLEdit.Name = "QuestionMLEdit";
         this.QuestionMLEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.QuestionMLEdit.Size = new System.Drawing.Size(408, 60);
         this.QuestionMLEdit.TabIndex = 195;
         this.QuestionMLEdit.Text = "";
         // 
         // TitleLabel
         // 
         this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
         this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
         this.TitleLabel.Name = "TitleLabel";
         this.TitleLabel.Size = new System.Drawing.Size(647, 20);
         this.TitleLabel.TabIndex = 172;
         this.TitleLabel.Text = "Основные";
         this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // LongHintMLEdit
         // 
         this.LongHintMLEdit.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.LongHintMLEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.LongHintMLEdit.CaptionLabel = this.label2;
         this.LongHintMLEdit.ContentType = DCEAccessLib.ContentType._html;
         this.LongHintMLEdit.DataType = DCEAccessLib.dataFieldType.ntext;
         this.LongHintMLEdit.eId = null;
         this.LongHintMLEdit.EntType = DCEAccessLib.ContentType._html;
         this.LongHintMLEdit.LanguageSwitcher = this.TopLangSwitcher;
         this.LongHintMLEdit.Location = new System.Drawing.Point(136, 36);
         this.LongHintMLEdit.MaxLength = 4000;
         this.LongHintMLEdit.Multiline = true;
         this.LongHintMLEdit.Name = "LongHintMLEdit";
         this.LongHintMLEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.LongHintMLEdit.Size = new System.Drawing.Size(500, 60);
         this.LongHintMLEdit.TabIndex = 200;
         this.LongHintMLEdit.Text = "";
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(8, 36);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(124, 23);
         this.label2.TabIndex = 197;
         this.label2.Text = "Полная подсказка";
         this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // ShortHintMLEdit
         // 
         this.ShortHintMLEdit.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.ShortHintMLEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ShortHintMLEdit.CaptionLabel = this.label3;
         this.ShortHintMLEdit.eId = null;
         this.ShortHintMLEdit.LanguageSwitcher = this.TopLangSwitcher;
         this.ShortHintMLEdit.Location = new System.Drawing.Point(136, 8);
         this.ShortHintMLEdit.MaxLength = 255;
         this.ShortHintMLEdit.Name = "ShortHintMLEdit";
         this.ShortHintMLEdit.Size = new System.Drawing.Size(500, 20);
         this.ShortHintMLEdit.TabIndex = 199;
         this.ShortHintMLEdit.Text = "";
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(8, 8);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(124, 23);
         this.label3.TabIndex = 198;
         this.label3.Text = "Краткая подсказка";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // PointsNumericUpDown
         // 
         this.PointsNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.PointsNumericUpDown.Location = new System.Drawing.Point(136, 4);
         this.PointsNumericUpDown.Maximum = new System.Decimal(new int[] {
                                                                            9999,
                                                                            0,
                                                                            0,
                                                                            0});
         this.PointsNumericUpDown.Name = "PointsNumericUpDown";
         this.PointsNumericUpDown.Size = new System.Drawing.Size(64, 20);
         this.PointsNumericUpDown.TabIndex = 202;
         // 
         // label4
         // 
         this.label4.Location = new System.Drawing.Point(8, 0);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(124, 23);
         this.label4.TabIndex = 201;
         this.label4.Text = "Количество баллов";
         this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // TestOnlyPanel
         // 
         this.TestOnlyPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.PointsNumericUpDown,
                                                                                    this.label4});
         this.TestOnlyPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.TestOnlyPanel.Location = new System.Drawing.Point(0, 160);
         this.TestOnlyPanel.Name = "TestOnlyPanel";
         this.TestOnlyPanel.Size = new System.Drawing.Size(647, 32);
         this.TestOnlyPanel.TabIndex = 7;
         // 
         // AnswerPanel
         // 
         this.AnswerPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.AnswerList,
                                                                                  this.TestOnlyPanel,
                                                                                  this.HintPanel,
                                                                                  this.label9,
                                                                                  this.ThemePanel});
         this.AnswerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.AnswerPanel.Location = new System.Drawing.Point(0, 132);
         this.AnswerPanel.Name = "AnswerPanel";
         this.AnswerPanel.Size = new System.Drawing.Size(647, 428);
         this.AnswerPanel.TabIndex = 8;
         // 
         // AnswerList
         // 
         this.AnswerList.AnswerListType = DCECourseEditor.QuestionListType.qltTest;
         this.AnswerList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.AnswerList.eId = null;
         this.AnswerList.LanguageSwitcher = this.TopLangSwitcher;
         this.AnswerList.Location = new System.Drawing.Point(0, 192);
         this.AnswerList.Name = "AnswerList";
         this.AnswerList.Size = new System.Drawing.Size(647, 236);
         this.AnswerList.TabIndex = 173;
         this.AnswerList.Type = DCECourseEditor.AnswerType.atSingle;
         // 
         // HintPanel
         // 
         this.HintPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.label2,
                                                                                this.ShortHintMLEdit,
                                                                                this.LongHintMLEdit,
                                                                                this.label3});
         this.HintPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.HintPanel.Location = new System.Drawing.Point(0, 56);
         this.HintPanel.Name = "HintPanel";
         this.HintPanel.Size = new System.Drawing.Size(647, 104);
         this.HintPanel.TabIndex = 9;
         // 
         // label9
         // 
         this.label9.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label9.Dock = System.Windows.Forms.DockStyle.Top;
         this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label9.ForeColor = System.Drawing.SystemColors.Info;
         this.label9.Location = new System.Drawing.Point(0, 36);
         this.label9.Name = "label9";
         this.label9.Size = new System.Drawing.Size(647, 20);
         this.label9.TabIndex = 172;
         this.label9.Text = "Ответы";
         this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // ThemePanel
         // 
         this.ThemePanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.ThemeCB,
                                                                                 this.label5});
         this.ThemePanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.ThemePanel.Name = "ThemePanel";
         this.ThemePanel.Size = new System.Drawing.Size(647, 36);
         this.ThemePanel.TabIndex = 174;
         // 
         // ThemeCB
         // 
         this.ThemeCB.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.ThemeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.ThemeCB.Location = new System.Drawing.Point(136, 8);
         this.ThemeCB.Name = "ThemeCB";
         this.ThemeCB.Size = new System.Drawing.Size(412, 21);
         this.ThemeCB.TabIndex = 1;
         // 
         // label5
         // 
         this.label5.Location = new System.Drawing.Point(8, 8);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(120, 24);
         this.label5.TabIndex = 0;
         this.label5.Text = "Тема вопроса";
         this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // QuestionEdit
         // 
         this.AutoScroll = true;
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.AnswerPanel,
                                                                      this.MainPanel,
                                                                      this.ButtonPanel,
                                                                      this.LangPanel});
         this.Name = "QuestionEdit";
         this.Size = new System.Drawing.Size(647, 604);
         this.LangPanel.ResumeLayout(false);
         this.ButtonPanel.ResumeLayout(false);
         this.MainPanel.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.PointsNumericUpDown)).EndInit();
         this.TestOnlyPanel.ResumeLayout(false);
         this.AnswerPanel.ResumeLayout(false);
         this.HintPanel.ResumeLayout(false);
         this.ThemePanel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      #endregion

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         LongHintMLEdit.EntType = ContentType._html;

         if (this.PointsNumericUpDown.Value > 9999)
            this.PointsNumericUpDown.Value = 9999;
         
         if (this.PointsNumericUpDown.Value < 0)
            this.PointsNumericUpDown.Value = 0;
         
         Node.EditRow["Points"] = this.PointsNumericUpDown.Value;

         Node.EditRow["Type"] = ((QuestionsType)TypeComboBox.SelectedItem).Type;

         if (ParentThemeId != null)
            Node.EditRow["Theme"] = ThemeCB.SelectedValue;
         
         Node.EndEdit(false, false);
         
         RefreshData();
      }

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         // не сохранять изменения и не закрывать окно
         Node.EndEdit(true, Node.EditRow.IsNew);
         
         SelectType((int)Node.EditRow["Type"]);
      }

      private void BrowseButton_Click(object sender, System.EventArgs e)
      {
         NodeControl node = this.Node;
         while ( ! (node is CourseEditNode) && node != null )
            node = node.NodeParent;
         
         if (node is CourseEditNode)
         {
            RecordEditNode rnode = (RecordEditNode)node;

            MaterialsForm Matform = new MaterialsForm(this.QuestionMLEdit.Text, 
               rnode.EditRow["DiskFolder"].ToString(),
               "Выбор контента для вопроса", 
               (this.QuestionMLEdit.EntType == DCEAccessLib.ContentType._object) ? 
               ("Swf files|*.swf|All files|*.*") :
               ("Htm files|*.html;*.htm|All files|*.*"));

            if (Matform.ShowDialog() == DialogResult.OK)
            {
               this.QuestionMLEdit.Text = Matform.Path;   
            }
         }
      }

      private void TypeComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         QuestionsType q = this.TypeComboBox.SelectedItem as QuestionsType;
         switch (q.Type)
         {
            case QuestionsType.QuestionsEnum.text :
               this.BrowseButton.Enabled = false;
               this.QuestionMLEdit.Multiline = true;
               AnswerList.Visible = true;
               break;
            case QuestionsType.QuestionsEnum.url :
               this.BrowseButton.Enabled = true;
               this.QuestionMLEdit.Multiline = false;
               AnswerList.Visible = true;
               break;
            case QuestionsType.QuestionsEnum.obj :
               AnswerList.Visible = false;
               this.BrowseButton.Enabled = true;
               this.QuestionMLEdit.Multiline = false;
               break;
         }
         
         this.QuestionMLEdit.EntType = (ContentType)(int)(this.TypeComboBox.SelectedItem as QuestionsType);

         RebindControls();
      }

      private void SelectType(int type)
      {
         this.QuestionMLEdit.eId = Node.EditRow["Content"].ToString();

         bool needdefault = true;
         foreach (QuestionsType q in this.TypeComboBox.Items)
         {
            if ((int)q.Type == type)
            {
               this.TypeComboBox.SelectedItem = q;
               needdefault = false;
               break;
            }
         }
         if (needdefault)
         {
            this.TypeComboBox.SelectedItem = TypeComboBox.Items[0];
         }
      }
   }

   /* QuestionEditNode */
   
   public class QuestionEditNode : RecordEditNode
   {
      public QuestionEditNode(NodeControl aParent, string aQuestionId, string aParentId, QuestionListType enumparam)
         : base(aParent, "select * from dbo.TestQuestions", "TestQuestions", "id", aQuestionId)
      {
         fParentId = aParentId;
         
         this.enumparam = enumparam;
         
         if (EditRow.IsNew)
         {
            EditRow ["Test"]  = fParentId;
         }
         
         this.QuestionName = GetQuestionName();
         
         //this.Expand();
      }
      
      private QuestionListType enumparam;
      /// <summary>
      /// Идентификатор Теста
      /// </summary>
      private string fParentId = "";
      
      private string questionName;
      
      private string GetQuestionName()
      {
         return DCEAccessLib.DCEWebAccess.WebAccess.GetString(
            "select dbo.GetContentAlt('" + EditRow["Content"].ToString() + "', 'RU', 'EN')");
      }
      
      public void RefreshQuestionName()
      {
         this.QuestionName = GetQuestionName();
      }
      
      public string QuestionName
      {
         get { return questionName; }

         set
         {
            questionName = value;
            this.CaptionChanged();
         }
      }

      public override bool HaveChildNodes() 
      { 
         // Пользователь может открывать форму свойств вопроса 
         // только через контекстное меню на форме свойств теста

         return false; 
      }
      
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new QuestionEdit(this, enumparam);
         }
         if (needRefresh)
         {
            ((QuestionEdit)this.fControl).RefreshData();
            needRefresh = false;
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         if (this.IsNew)
            return "Вопрос[Новый]";
         else
            return "Вопрос : " + QuestionName;
      }

      public override void ReleaseControl()
      {
         // do nothing
      }
      
      public override bool CanClose()
      {
         // Нода "Вопрос" относится к динамическим нодам
         
         return true;
      }

      /// <summary>
      /// Initialize new record for editing (EditRow)
      /// </summary>
      protected override void InitNewRecord()
      {
         EditRow ["id"] = System.Guid.NewGuid();
         EditRow ["Test"] = string.IsNullOrEmpty(this.fParentId) ? Guid.Empty : new Guid(this.fParentId);
         EditRow ["Type"] = QuestionsType.QuestionsEnum.text;
         EditRow ["Content"] = System.Guid.NewGuid().ToString();
         EditRow ["ShortHint"] = System.Guid.NewGuid().ToString();
         EditRow ["LongHint"] = System.Guid.NewGuid().ToString();
         EditRow ["Points"] = 0;
         EditRow ["Answer"] = System.Guid.NewGuid().ToString();
      }
   }
   
   public class QuestionsType
   {
      public enum QuestionsEnum
      {
         text = ContentType._html,
         url = ContentType._url,
         obj = ContentType._object,
      }

      private QuestionsEnum _type;
      public QuestionsEnum Type
      {
         get
         { 
            return _type; 
         }
      }
      public QuestionsType(QuestionsEnum type)
      {
         _type = type;
      }

      public override string ToString()
      {
         switch (_type)
         {
            case QuestionsEnum.url:
               return "Url";

            case QuestionsEnum.text:
               return "Text";

            case QuestionsEnum.obj:
               return "Object";
      
            default:
               return "Unknown Item";
         }
      }
      public static implicit operator int ( QuestionsType type )
      {
         return (int)type.Type;
      }
   }
}
