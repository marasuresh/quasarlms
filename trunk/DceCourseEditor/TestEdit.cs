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
	/// Редактирование теста
	/// </summary>
	public class TestEdit : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Panel LangPanel;
      private System.Windows.Forms.Panel ButtonPanel;
      private System.Windows.Forms.Panel MainPanel;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label TitleLabel;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button buttonCancel;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.CheckBox AutoFinishCheckBox;
      private System.Windows.Forms.CheckBox ShowResultCheckBox;
      private SpinEdit PointsNumericUpDown;
      private SpinEdit DurationNumericUpDown;
      private System.Windows.Forms.Label TotalScoreLabel;
      private int totalPoints = 0;
      private System.Windows.Forms.GroupBox ShowHintsGroupBox;
      private System.Windows.Forms.RadioButton NoRadioButton;
      private System.Windows.Forms.RadioButton OneRadioButton;
      private System.Windows.Forms.RadioButton BothRadioButton;
      private System.Windows.Forms.Button DeleteButton;
      private System.Windows.Forms.CheckBox ShowThemesCB;
      private System.Windows.Forms.Panel QuestionPanel;
      private DCECourseEditor.QuestionList questionList;
      private System.Windows.Forms.Label label1;

		private TestEditNode Node;

      public TestEdit(TestEditNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			Node = node;

         this.SaveButton.Enabled = DCEUser.CurrentUser.EditableCourses;
         if (DCEUser.CurrentUser.ReadOnlyCourses)
         {
            this.DeleteButton.Enabled = false;
         }
         
         CourseEditNode coursenode = (CourseEditNode)Node.GetSpecifiedParentNode("DCECourseEditor.CourseEditNode");
         if ( coursenode != null)
         {
            if (this.Node.NodeParent != (NodeControl)coursenode)
            {
               ShowThemesCB.Text = "Показывать разбивку вопросов  по подтемам";
            }
            if (coursenode.IsCourseActive)
            {
               DurationNumericUpDown.Enabled = false;
               PointsNumericUpDown.Enabled = false;
               ShowResultCheckBox.Enabled = false;
               AutoFinishCheckBox.Enabled = false;
               ShowHintsGroupBox.Enabled = false;
               ShowThemesCB.Enabled = false;
               this.DeleteButton.Enabled = false;
            }
         }

         questionList.SetParentNode(Node, "");

         this.RebindControls();
         
         this.Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(RebindControls);
         
         if (this.Node.NodeParent is RecordEditNode)
         {
            RecordEditNode r = this.Node.NodeParent as RecordEditNode;
            r.OnReloadContent += new RecordEditNode.ReloadContentHandler(ParentSaved);
         }
		}
      
      public void ParentSaved()
      {
         // Если родительская нода - нода редактирования темы
         if (Node.NodeParent is ThemeEditNode)
         {
            ThemeEditNode n = Node.NodeParent as ThemeEditNode;
            
            // Проверка не изменилось ли имя у темы
            if (n.ThemeName != Node.ParentName)
               Node.ParentName = n.ThemeName;
         }
      }

      public void RebindControls()
      {
         if (DCEUser.CurrentUser.EditableCourses)
            this.DeleteButton.Enabled = !Node.IsNew;            

         DurationNumericUpDown.DataBindings.Clear();
         DurationNumericUpDown.DataBindings.Add("Value", Node.EditRow, "Duration");
         
         PointsNumericUpDown.DataBindings.Clear();
         PointsNumericUpDown.DataBindings.Add("Value", Node.EditRow, "Points");

         NoRadioButton.Checked = false;
         OneRadioButton.Checked = false;
         BothRadioButton.Checked = false;

         if (Node.EditRow["Hints"].ToString() == ((int)HintType.none).ToString())
            NoRadioButton.Checked = true;
         else if (Node.EditRow["Hints"].ToString() == ((int)HintType.one).ToString())
            OneRadioButton.Checked = true;
         else if (Node.EditRow["Hints"].ToString() == ((int)HintType.both).ToString())
            BothRadioButton.Checked = true;
         else
            NoRadioButton.Checked = true;
         
         AutoFinishCheckBox.DataBindings.Clear();
         AutoFinishCheckBox.DataBindings.Add("Checked", Node.EditRow, "AutoFinish");

         ShowResultCheckBox.DataBindings.Clear();
         ShowResultCheckBox.DataBindings.Add("Checked", Node.EditRow, "Show");

         ShowThemesCB.DataBindings.Clear();
         ShowThemesCB.DataBindings.Add("Checked", Node.EditRow, "ShowThemes");

         RefreshData();
      }

      public void RefreshData()
      {
         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(

            "select * " + 
            
            //", dbo.GetStrContentAlt(Content, 'RU', 'EN') as RContent " + 

            "from TestQuestions " + 
         
            "where Test = '" + Node.Id + "'", "dbo.TestQuestions");

         dataView.Table = dataSet.Tables["dbo.TestQuestions"];

         this.totalPoints = 0;

         foreach (DataRow row in dataSet.Tables["dbo.TestQuestions"].Rows)
         {
           this.totalPoints += (int)row["Points"];
         }
         TotalScoreLabel.Text = this.totalPoints.ToString();
       
         if (PointsNumericUpDown.Value > this.totalPoints) 
            PointsNumericUpDown.Value = this.totalPoints;
         
         questionList.RefreshData();
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
         this.ButtonPanel = new System.Windows.Forms.Panel();
         this.DeleteButton = new System.Windows.Forms.Button();
         this.SaveButton = new System.Windows.Forms.Button();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.dataView = new System.Data.DataView();
         this.MainPanel = new System.Windows.Forms.Panel();
         this.ShowThemesCB = new System.Windows.Forms.CheckBox();
         this.ShowHintsGroupBox = new System.Windows.Forms.GroupBox();
         this.BothRadioButton = new System.Windows.Forms.RadioButton();
         this.OneRadioButton = new System.Windows.Forms.RadioButton();
         this.NoRadioButton = new System.Windows.Forms.RadioButton();
         this.TotalScoreLabel = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.AutoFinishCheckBox = new System.Windows.Forms.CheckBox();
         this.ShowResultCheckBox = new System.Windows.Forms.CheckBox();
         this.label5 = new System.Windows.Forms.Label();
         this.PointsNumericUpDown = new DCEAccessLib.SpinEdit();
         this.label3 = new System.Windows.Forms.Label();
         this.DurationNumericUpDown = new DCEAccessLib.SpinEdit();
         this.label2 = new System.Windows.Forms.Label();
         this.TitleLabel = new System.Windows.Forms.Label();
         this.dataSet = new System.Data.DataSet();
         this.QuestionPanel = new System.Windows.Forms.Panel();
         this.questionList = new DCECourseEditor.QuestionList();
         this.label1 = new System.Windows.Forms.Label();
         this.ButtonPanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.MainPanel.SuspendLayout();
         this.ShowHintsGroupBox.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.PointsNumericUpDown)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.DurationNumericUpDown)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.QuestionPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // LangPanel
         // 
         this.LangPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.LangPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.LangPanel.Location = new System.Drawing.Point(0, 236);
         this.LangPanel.Name = "LangPanel";
         this.LangPanel.Size = new System.Drawing.Size(672, 0);
         this.LangPanel.TabIndex = 2;
         // 
         // ButtonPanel
         // 
         this.ButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ButtonPanel.Controls.Add(this.DeleteButton);
         this.ButtonPanel.Controls.Add(this.SaveButton);
         this.ButtonPanel.Controls.Add(this.buttonCancel);
         this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.ButtonPanel.Location = new System.Drawing.Point(0, 412);
         this.ButtonPanel.Name = "ButtonPanel";
         this.ButtonPanel.Size = new System.Drawing.Size(672, 44);
         this.ButtonPanel.TabIndex = 3;
         // 
         // DeleteButton
         // 
         this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.DeleteButton.Location = new System.Drawing.Point(8, 6);
         this.DeleteButton.Name = "DeleteButton";
         this.DeleteButton.Size = new System.Drawing.Size(108, 28);
         this.DeleteButton.TabIndex = 5;
         this.DeleteButton.Text = "Удалить тест";
         this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
         // 
         // SaveButton
         // 
         this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SaveButton.Location = new System.Drawing.Point(462, 6);
         this.SaveButton.Name = "SaveButton";
         this.SaveButton.Size = new System.Drawing.Size(96, 28);
         this.SaveButton.TabIndex = 4;
         this.SaveButton.Text = "Сохранить";
         this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
         // 
         // buttonCancel
         // 
         this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.buttonCancel.Location = new System.Drawing.Point(566, 6);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(96, 28);
         this.buttonCancel.TabIndex = 3;
         this.buttonCancel.Text = "Отменить";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // MainPanel
         // 
         this.MainPanel.Controls.Add(this.ShowThemesCB);
         this.MainPanel.Controls.Add(this.ShowHintsGroupBox);
         this.MainPanel.Controls.Add(this.TotalScoreLabel);
         this.MainPanel.Controls.Add(this.label6);
         this.MainPanel.Controls.Add(this.AutoFinishCheckBox);
         this.MainPanel.Controls.Add(this.ShowResultCheckBox);
         this.MainPanel.Controls.Add(this.label5);
         this.MainPanel.Controls.Add(this.PointsNumericUpDown);
         this.MainPanel.Controls.Add(this.label3);
         this.MainPanel.Controls.Add(this.DurationNumericUpDown);
         this.MainPanel.Controls.Add(this.label2);
         this.MainPanel.Controls.Add(this.TitleLabel);
         this.MainPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.MainPanel.Location = new System.Drawing.Point(0, 0);
         this.MainPanel.Name = "MainPanel";
         this.MainPanel.Size = new System.Drawing.Size(672, 236);
         this.MainPanel.TabIndex = 5;
         // 
         // ShowThemesCB
         // 
         this.ShowThemesCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.ShowThemesCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ShowThemesCB.Location = new System.Drawing.Point(8, 156);
         this.ShowThemesCB.Name = "ShowThemesCB";
         this.ShowThemesCB.Size = new System.Drawing.Size(332, 28);
         this.ShowThemesCB.TabIndex = 198;
         this.ShowThemesCB.Text = "Показывать разбивку вопросов  по темам";
         // 
         // ShowHintsGroupBox
         // 
         this.ShowHintsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.ShowHintsGroupBox.Controls.Add(this.BothRadioButton);
         this.ShowHintsGroupBox.Controls.Add(this.OneRadioButton);
         this.ShowHintsGroupBox.Controls.Add(this.NoRadioButton);
         this.ShowHintsGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ShowHintsGroupBox.Location = new System.Drawing.Point(8, 188);
         this.ShowHintsGroupBox.Name = "ShowHintsGroupBox";
         this.ShowHintsGroupBox.Size = new System.Drawing.Size(476, 44);
         this.ShowHintsGroupBox.TabIndex = 197;
         this.ShowHintsGroupBox.TabStop = false;
         this.ShowHintsGroupBox.Text = "Использование подсказок";
         // 
         // BothRadioButton
         // 
         this.BothRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.BothRadioButton.Location = new System.Drawing.Point(312, 16);
         this.BothRadioButton.Name = "BothRadioButton";
         this.BothRadioButton.Size = new System.Drawing.Size(144, 24);
         this.BothRadioButton.TabIndex = 2;
         this.BothRadioButton.Text = "Показывать обе";
         this.BothRadioButton.CheckedChanged += new System.EventHandler(this.BothRadioButton_CheckedChanged);
         // 
         // OneRadioButton
         // 
         this.OneRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.OneRadioButton.Location = new System.Drawing.Point(140, 16);
         this.OneRadioButton.Name = "OneRadioButton";
         this.OneRadioButton.Size = new System.Drawing.Size(172, 24);
         this.OneRadioButton.TabIndex = 1;
         this.OneRadioButton.Text = "Показывать краткую";
         this.OneRadioButton.CheckedChanged += new System.EventHandler(this.OneRadioButton_CheckedChanged);
         // 
         // NoRadioButton
         // 
         this.NoRadioButton.Checked = true;
         this.NoRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.NoRadioButton.Location = new System.Drawing.Point(4, 16);
         this.NoRadioButton.Name = "NoRadioButton";
         this.NoRadioButton.Size = new System.Drawing.Size(136, 24);
         this.NoRadioButton.TabIndex = 0;
         this.NoRadioButton.TabStop = true;
         this.NoRadioButton.Text = "Не показывать";
         this.NoRadioButton.CheckedChanged += new System.EventHandler(this.NoRadioButton_CheckedChanged);
         // 
         // TotalScoreLabel
         // 
         this.TotalScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.TotalScoreLabel.Location = new System.Drawing.Point(424, 62);
         this.TotalScoreLabel.Name = "TotalScoreLabel";
         this.TotalScoreLabel.Size = new System.Drawing.Size(48, 16);
         this.TotalScoreLabel.TabIndex = 195;
         this.TotalScoreLabel.Text = "0";
         this.TotalScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label6
         // 
         this.label6.Location = new System.Drawing.Point(300, 62);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(120, 16);
         this.label6.TabIndex = 194;
         this.label6.Text = "Суммарный балл : ";
         this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // AutoFinishCheckBox
         // 
         this.AutoFinishCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.AutoFinishCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.AutoFinishCheckBox.Location = new System.Drawing.Point(8, 124);
         this.AutoFinishCheckBox.Name = "AutoFinishCheckBox";
         this.AutoFinishCheckBox.Size = new System.Drawing.Size(332, 28);
         this.AutoFinishCheckBox.TabIndex = 189;
         this.AutoFinishCheckBox.Text = "Завершение теста при наборе проходного балла";
         // 
         // ShowResultCheckBox
         // 
         this.ShowResultCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.ShowResultCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ShowResultCheckBox.Location = new System.Drawing.Point(8, 92);
         this.ShowResultCheckBox.Name = "ShowResultCheckBox";
         this.ShowResultCheckBox.Size = new System.Drawing.Size(332, 28);
         this.ShowResultCheckBox.TabIndex = 187;
         this.ShowResultCheckBox.Text = "Показывать результат студенту";
         // 
         // label5
         // 
         this.label5.Location = new System.Drawing.Point(8, 60);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(220, 23);
         this.label5.TabIndex = 186;
         this.label5.Text = "Проходной балл";
         // 
         // PointsNumericUpDown
         // 
         this.PointsNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.PointsNumericUpDown.Location = new System.Drawing.Point(236, 60);
         this.PointsNumericUpDown.Maximum = new System.Decimal(new int[] {
                                                                            32768,
                                                                            0,
                                                                            0,
                                                                            0});
         this.PointsNumericUpDown.Name = "PointsNumericUpDown";
         this.PointsNumericUpDown.Size = new System.Drawing.Size(64, 20);
         this.PointsNumericUpDown.TabIndex = 184;
         this.PointsNumericUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.PointsNumericUpDown_Validating);
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(300, 32);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(44, 16);
         this.label3.TabIndex = 183;
         this.label3.Text = "мин";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // DurationNumericUpDown
         // 
         this.DurationNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.DurationNumericUpDown.Location = new System.Drawing.Point(236, 28);
         this.DurationNumericUpDown.Maximum = new System.Decimal(new int[] {
                                                                              600,
                                                                              0,
                                                                              0,
                                                                              0});
         this.DurationNumericUpDown.Name = "DurationNumericUpDown";
         this.DurationNumericUpDown.Size = new System.Drawing.Size(64, 20);
         this.DurationNumericUpDown.TabIndex = 182;
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(8, 28);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(220, 23);
         this.label2.TabIndex = 181;
         this.label2.Text = "Время сдачи теста";
         // 
         // TitleLabel
         // 
         this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
         this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
         this.TitleLabel.Location = new System.Drawing.Point(0, 0);
         this.TitleLabel.Name = "TitleLabel";
         this.TitleLabel.Size = new System.Drawing.Size(672, 20);
         this.TitleLabel.TabIndex = 172;
         this.TitleLabel.Text = "Основные";
         this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
         // 
         // QuestionPanel
         // 
         this.QuestionPanel.Controls.Add(this.questionList);
         this.QuestionPanel.Controls.Add(this.label1);
         this.QuestionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.QuestionPanel.Location = new System.Drawing.Point(0, 236);
         this.QuestionPanel.Name = "QuestionPanel";
         this.QuestionPanel.Size = new System.Drawing.Size(672, 176);
         this.QuestionPanel.TabIndex = 7;
         // 
         // questionList
         // 
         this.questionList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.questionList.Location = new System.Drawing.Point(0, 20);
         this.questionList.Name = "questionList";
         this.questionList.QuestionType = DCECourseEditor.QuestionListType.qltTest;
         this.questionList.Size = new System.Drawing.Size(672, 156);
         this.questionList.TabIndex = 173;
         // 
         // label1
         // 
         this.label1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label1.Dock = System.Windows.Forms.DockStyle.Top;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label1.ForeColor = System.Drawing.SystemColors.Info;
         this.label1.Location = new System.Drawing.Point(0, 0);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(672, 20);
         this.label1.TabIndex = 172;
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // TestEdit
         // 
         this.Controls.Add(this.QuestionPanel);
         this.Controls.Add(this.ButtonPanel);
         this.Controls.Add(this.LangPanel);
         this.Controls.Add(this.MainPanel);
         this.Name = "TestEdit";
         this.Size = new System.Drawing.Size(672, 456);
         this.ButtonPanel.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.MainPanel.ResumeLayout(false);
         this.ShowHintsGroupBox.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.PointsNumericUpDown)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.DurationNumericUpDown)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.QuestionPanel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         // сохранить изменения и не закрывать окно

         if (PointsNumericUpDown.Value > this.totalPoints)
            PointsNumericUpDown.Value = this.totalPoints;

         HintType type = HintType.none;
         
         if (OneRadioButton.Checked)
            type = HintType.one;
         else if (BothRadioButton.Checked)
            type = HintType.both;

         int res = (int)type;
         this.Node.EditRow["Hints"] = (int)type;
         this.Node.EditRow["Points"] = this.PointsNumericUpDown.Value;
         
         Node.EndEdit(false, false);
      }

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         // не сохранять изменения и не закрывать окно

         Node.EndEdit(true, Node.EditRow.IsNew);
      }

      private bool CheckValidity()
      {
         if (Node.IsNew)
         {
            MessageBox.Show("Необходимо вначале сохранить тест", "Сообщение");
            return false;
         }
         return true;
      }
      
      private void PointsNumericUpDown_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (this.PointsNumericUpDown.Value > this.totalPoints)
         {
            e.Cancel = false;
            this.PointsNumericUpDown.Value = this.totalPoints;
         }

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

      private void DeleteButton_Click(object sender, System.EventArgs e)
      {
         if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить тест?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from Tests where id = '" + this.Node.Id + "'");

            this.Node.Dispose();
         }
      }
	}

   public class TestEditNode : RecordEditNode
   {
      public TestEditNode(NodeControl aParent, string aTestId, string aParentId, bool aFinalTest, string aParentName)
         : base(aParent, "select * from dbo.Tests", "Tests", "id", aTestId)
      {
         fParentId = aParentId;
         
         if (EditRow.IsNew)
         {
            EditRow ["Parent"]  = fParentId;
         }
         
         fParentName = aParentName;

         fFinalTest = aFinalTest;
         
         this.CaptionChanged();
         
         //this.Expand();
      }
      
      /// <summary>
      /// Идентификатор Темы или Курса
      /// </summary>
      private string fParentId = "";

      /// <summary>
      /// Название Темы или Курса
      /// </summary>
      private string fParentName = "";

      /// <summary>
      // Признак Заключительного Теста
      /// </summary>
      private bool fFinalTest;

      public string ParentName 
      {
         get 
         {
            return fParentName;
         }
         set
         {
            this.fParentName = value;
            this.CaptionChanged();
         }
      }
      
      public bool FinalTest
      {
         get 
         {
            return fFinalTest;
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
            fControl = new TestEdit(this);
         }
         if (needRefresh)
         {
            ((TestEdit)this.fControl).RefreshData();
            needRefresh = false;
         }

         return this.fControl;
      }

      public override String GetCaption()
      {
         if (this.IsNew)
         {
            return "[Новый тест]";
         }
         else
         {
            if (this.fFinalTest)
               return "Заключительный Тест";
            else
               return "Тест по теме : " + this.fParentName;
         }
      }

      public override void ReleaseControl()
      {
         // do nothing
      }
      
      public override void ChildChanged(NodeControl child)
      {
      }

      public override bool CanClose()
      {
         // Нода "Тест" относится к стационарным нодам
         
         return false;
      }

      /// <summary>
      /// Initialize new record for editing (EditRow)
      /// </summary>
      protected override void InitNewRecord()
      {
         // filling all non-nullable fields
         EditRow ["id"] = System.Guid.NewGuid().ToString();
         
         EditRow ["Type"] = (int)TestType.test;
         
         EditRow ["Duration"] = 20;
         EditRow ["Points"] = 0;
         EditRow ["Show"] = 1;
         EditRow ["ShowThemes"] = 1;
         EditRow ["Split"] = 0;
         EditRow ["AutoFinish"] = 0;
         EditRow ["DefLanguage"] = 1;
         EditRow ["CanSwitchLang"] = 0;
         EditRow ["Hints"] = (int)HintType.none;
      }
   }
}
