using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCECourseEditor
{
   public class QuestionnaireEditNode : RecordEditNode
   {
      public QuestionnaireEditNode(NodeControl aParent, string questionnaireId, string questionnaireName)
         : base(aParent, "select * from dbo.Tests", "Questionaire", "id", questionnaireId)
      {
         this.questionnaireId = questionnaireId;
         this.questionnaireName = questionnaireName;
         
         this.CaptionChanged();
      }
      
      private string questionnaireId = "";
      private string questionnaireName = "";
      public string QuestionnaireName 
      {
         get 
         {
            return questionnaireName;
         }
         set
         {
            this.questionnaireName = value;
            this.CaptionChanged();
         }
      }


      public override bool HaveChildNodes() 
      { 
         // Пользователь может открывать форму свойств анкеты 
         // только через контекстное меню на форме списка анкет

         return false; 
      }
      
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new QuestionnaireEdit(this);
         }
         if (needRefresh)
         {
            ((QuestionnaireEdit)this.fControl).RefreshData();
            needRefresh = false;
         }

         return this.fControl;
      }

      public override string GetCaption()
      {
         if (this.IsNew)
         {
            return "[Новая анкета]";
         }
         else
         {
            return questionnaireName;
         }
      }

      public override void ReleaseControl()
      {
         // do nothing
      }
      
      public override bool CanClose()
      {
         // Нода "Анкета" относится к динамическим нодам
         
         return true;
      }

      /// <summary>
      /// Initialize new record for editing (EditRow)
      /// </summary>
      protected override void InitNewRecord()
      {
         // filling all non-nullable fields
         EditRow ["id"] = System.Guid.NewGuid().ToString();
         
         EditRow ["Type"] = (int)TestType.questionnaire;
         
         EditRow ["Duration"] = 20;
         EditRow ["Points"] = 0;
         EditRow ["Show"] = 1;
         EditRow ["Split"] = 0;
         EditRow ["AutoFinish"] = 0;
         EditRow ["DefLanguage"] = 1;
         EditRow ["CanSwitchLang"] = 0;
         EditRow ["Hints"] = (int)HintType.none;
      }
   }

   /// <summary>
	/// Редактирование анкеты
	/// </summary>
	public class QuestionnaireEdit : System.Windows.Forms.UserControl
	{
      #region Form variables
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.Panel QuestionPanel;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Panel MainPanel;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label4;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.TextBox InternalNameTextBox;
      private System.Windows.Forms.ComboBox TypeComboBox;
      private System.Windows.Forms.ContextMenu contextMenuQuestions;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.Panel ButtonPanel;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button buttonCancel;
      private DCECourseEditor.QuestionList questionList;
      #endregion

		private QuestionnaireEditNode Node;
      
      class QuestionnaireType 
      {
         public QuestionnaireType(TestType type)
         {
            this.type = type;
         }

         private TestType type;
         public TestType Type
         {
            get { return type; }
         }

         public override string ToString()
         {
            switch (type)
            {
               case TestType.questionnaire :
                  return "Курсовая анкета";
               case TestType.globalquestionnaire :
                  return "Общая анкета";
               default : 
                  return "Анкета";
            }
         }
      }
      
      public QuestionnaireEdit(QuestionnaireEditNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			Node = node;

         this.SaveButton.Enabled = DCEUser.CurrentUser.EditableCourses;

         if (DCEUser.CurrentUser.ReadOnlyCourses)
         {
            this.menuItem1.Enabled = false;
            this.menuItem4.Enabled = false;
         }


         TypeComboBox.Items.Add(new QuestionnaireType(TestType.questionnaire));
         TypeComboBox.Items.Add(new QuestionnaireType(TestType.globalquestionnaire));

         questionList.SetParentNode(Node, "");
         
         this.RebindControls();

         this.Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(RebindControls);
      }

      public void RefreshData()
      {
         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(

            "select *, " + 
            
            "dbo.GetStrContentAlt(Content, 'RU', 'EN') as RContent " + 

            "from TestQuestions " + 
         
            "where Test = '" + Node.Id + "'", "dbo.TestQuestions");

         dataView.Table = dataSet.Tables["dbo.TestQuestions"];

         questionList.RefreshData();
      }

      public void RebindControls()
      {
         InternalNameTextBox.DataBindings.Clear();
         InternalNameTextBox.DataBindings.Add("Text", Node.EditRow, "InternalName");
         
         for( int i=0; i<TypeComboBox.Items.Count; i++)
         {
            QuestionnaireType item = TypeComboBox.Items[i] as QuestionnaireType;
            
            if ((int)item.Type == (int)Node.EditRow["Type"])
            {
               TypeComboBox.SelectedIndex = i;
               break;
            }
         }

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
         this.QuestionPanel = new System.Windows.Forms.Panel();
         this.questionList = new DCECourseEditor.QuestionList();
         this.label1 = new System.Windows.Forms.Label();
         this.dataView = new System.Data.DataView();
         this.MainPanel = new System.Windows.Forms.Panel();
         this.TypeComboBox = new System.Windows.Forms.ComboBox();
         this.label4 = new System.Windows.Forms.Label();
         this.InternalNameTextBox = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.dataSet = new System.Data.DataSet();
         this.contextMenuQuestions = new System.Windows.Forms.ContextMenu();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.ButtonPanel = new System.Windows.Forms.Panel();
         this.SaveButton = new System.Windows.Forms.Button();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.QuestionPanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.MainPanel.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.ButtonPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // QuestionPanel
         // 
         this.QuestionPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.questionList,
                                                                                    this.label1});
         this.QuestionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.QuestionPanel.Location = new System.Drawing.Point(0, 104);
         this.QuestionPanel.Name = "QuestionPanel";
         this.QuestionPanel.Size = new System.Drawing.Size(520, 296);
         this.QuestionPanel.TabIndex = 7;
         // 
         // questionList
         // 
         this.questionList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.questionList.Location = new System.Drawing.Point(0, 20);
         this.questionList.Name = "questionList";
         this.questionList.QuestionType = DCECourseEditor.QuestionListType.qltQuestionnaire;
         this.questionList.Size = new System.Drawing.Size(520, 276);
         this.questionList.TabIndex = 173;
         // 
         // label1
         // 
         this.label1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label1.Dock = System.Windows.Forms.DockStyle.Top;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label1.ForeColor = System.Drawing.SystemColors.Info;
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(520, 20);
         this.label1.TabIndex = 172;
         this.label1.Text = "Список вопросов";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // MainPanel
         // 
         this.MainPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.TypeComboBox,
                                                                                this.label4,
                                                                                this.InternalNameTextBox,
                                                                                this.label3,
                                                                                this.label2});
         this.MainPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.MainPanel.Name = "MainPanel";
         this.MainPanel.Size = new System.Drawing.Size(520, 104);
         this.MainPanel.TabIndex = 8;
         // 
         // TypeComboBox
         // 
         this.TypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.TypeComboBox.Location = new System.Drawing.Point(140, 70);
         this.TypeComboBox.Name = "TypeComboBox";
         this.TypeComboBox.Size = new System.Drawing.Size(176, 21);
         this.TypeComboBox.TabIndex = 177;
         // 
         // label4
         // 
         this.label4.Location = new System.Drawing.Point(8, 64);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(120, 32);
         this.label4.TabIndex = 176;
         this.label4.Text = "Тип анкеты";
         this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // InternalNameTextBox
         // 
         this.InternalNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.InternalNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.InternalNameTextBox.Location = new System.Drawing.Point(140, 32);
         this.InternalNameTextBox.Name = "InternalNameTextBox";
         this.InternalNameTextBox.Size = new System.Drawing.Size(368, 20);
         this.InternalNameTextBox.TabIndex = 175;
         this.InternalNameTextBox.Text = "";
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(8, 28);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(120, 28);
         this.label3.TabIndex = 174;
         this.label3.Text = "Внутреннее название";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label2
         // 
         this.label2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label2.Dock = System.Windows.Forms.DockStyle.Top;
         this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label2.ForeColor = System.Drawing.SystemColors.Info;
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(520, 20);
         this.label2.TabIndex = 173;
         this.label2.Text = "Основные";
         this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
         // 
         // contextMenuQuestions
         // 
         this.contextMenuQuestions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                             this.menuItem1,
                                                                                             this.menuItem2,
                                                                                             this.menuItem3,
                                                                                             this.menuItem4});
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 0;
         this.menuItem1.Text = "Создать";
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 1;
         this.menuItem2.Text = "-";
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 2;
         this.menuItem3.Text = "Редактировать";
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 3;
         this.menuItem4.Text = "Удалить";
         // 
         // ButtonPanel
         // 
         this.ButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ButtonPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.SaveButton,
                                                                                  this.buttonCancel});
         this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.ButtonPanel.Location = new System.Drawing.Point(0, 356);
         this.ButtonPanel.Name = "ButtonPanel";
         this.ButtonPanel.Size = new System.Drawing.Size(520, 44);
         this.ButtonPanel.TabIndex = 9;
         // 
         // SaveButton
         // 
         this.SaveButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SaveButton.Location = new System.Drawing.Point(310, 6);
         this.SaveButton.Name = "SaveButton";
         this.SaveButton.Size = new System.Drawing.Size(96, 28);
         this.SaveButton.TabIndex = 4;
         this.SaveButton.Text = "Сохранить";
         this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
         // 
         // buttonCancel
         // 
         this.buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.buttonCancel.Location = new System.Drawing.Point(414, 6);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(96, 28);
         this.buttonCancel.TabIndex = 3;
         this.buttonCancel.Text = "Отменить";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // QuestionnaireEdit
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.ButtonPanel,
                                                                      this.QuestionPanel,
                                                                      this.MainPanel});
         this.Name = "QuestionnaireEdit";
         this.Size = new System.Drawing.Size(520, 400);
         this.QuestionPanel.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.MainPanel.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ButtonPanel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         // сохранить изменения и не закрывать окно

         Node.QuestionnaireName = this.InternalNameTextBox.Text;

         QuestionnaireType type = TypeComboBox.SelectedItem as QuestionnaireType;
         Node.EditRow["Type"] = (int)type.Type;
         Node.EndEdit(false, false);
      }

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         // не сохранять изменения и не закрывать окно

         Node.EndEdit(true, Node.EditRow.IsNew);
      }
   }
}

