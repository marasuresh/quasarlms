using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCEInternalSystem
{
	/// <summary>
	/// Редактор заданий
	/// </summary>
	public class TaskEdit : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Button OkButtonz;
      private System.Windows.Forms.Button CancelButtonz;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label17;
      private DCEAccessLib.MLEdit DescEdit;
      private DCEAccessLib.MLEdit NameEdit;
      private System.Windows.Forms.DateTimePicker DateEdit;
      private System.Windows.Forms.Panel panel3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Panel panel4;
      private System.Windows.Forms.TextBox SolutionText;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.ComboBox TaskCompleteCB;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader CompleteColumn;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private System.Windows.Forms.Panel panel5;
      private DCEAccessLib.LangSwitcher langSwitcher1;

      private TaskEditNode Node;
      public TaskEdit(TaskEditNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.Node = node;
         this.CompleteColumn.OnParse += new DataColumnHeader.FieldParseHandler(OnCompleteParse);
         

         this.NameEdit.SetParentNode(this.Node);
         this.DescEdit.SetParentNode(this.Node);
         this.Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(RebindControls);
         RebindControls();
		}

      public void RebindControls()
      {
         this.NameEdit.DataBindings.Clear();
         this.NameEdit.DataBindings.Add("eId",Node.EditRow,"Name");
         this.DescEdit.DataBindings.Clear();
         this.DescEdit.DataBindings.Add("eId",Node.EditRow,"Description");
         this.DateEdit.DataBindings.Clear();
         this.DateEdit.DataBindings.Add("Value",Node.EditRow,"TaskTime");
         this.dataList.DataView = this.Node.SolutionsDV;
         this.dataList.UpdateList();
      }

      public void OnCompleteParse(string FieldName, DataRowView row, ref string text)
      {
         
         switch ((int)row["Complete"])
         {
            case TaskComplete.NotChecked:
               if (row["Solution"].GetType() == typeof(System.DBNull))
               {
                  text = "Отсутствует";
               }
               else
                  text = "Не проверено";
               break;
            case TaskComplete.Incomplete:
               text = "Не выполнено";
               break;
            case TaskComplete.Incorrect:
               text = "Неправильно";
               break;
            case TaskComplete.Partially:
               text = "Частично";
               break;
            case TaskComplete.Almost:
               text = "В основном правильно";
               break;
            case TaskComplete.Right:
               text = "Правильно";
               break;
         }
      }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         Node = null;
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
			this.panel2 = new System.Windows.Forms.Panel();
			this.OkButtonz = new System.Windows.Forms.Button();
			this.CancelButtonz = new System.Windows.Forms.Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.DescEdit = new DCEAccessLib.MLEdit();
			this.langSwitcher1 = new DCEAccessLib.LangSwitcher();
			this.NameEdit = new DCEAccessLib.MLEdit();
			this.DateEdit = new System.Windows.Forms.DateTimePicker();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.SolutionText = new System.Windows.Forms.TextBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.TaskCompleteCB = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dataList = new DCEAccessLib.DataList();
			this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
			this.CompleteColumn = new DCEAccessLib.DataColumnHeader();
			this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.OkButtonz);
			this.panel2.Controls.Add(this.CancelButtonz);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 360);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(624, 32);
			this.panel2.TabIndex = 142;
			// 
			// OkButtonz
			// 
			this.OkButtonz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButtonz.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButtonz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.OkButtonz.Location = new System.Drawing.Point(422, 2);
			this.OkButtonz.Name = "OkButtonz";
			this.OkButtonz.Size = new System.Drawing.Size(96, 24);
			this.OkButtonz.TabIndex = 8;
			this.OkButtonz.Text = "Применить";
			this.OkButtonz.Click += new System.EventHandler(this.OkButtonz_Click);
			// 
			// CancelButtonz
			// 
			this.CancelButtonz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButtonz.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButtonz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CancelButtonz.Location = new System.Drawing.Point(522, 2);
			this.CancelButtonz.Name = "CancelButtonz";
			this.CancelButtonz.Size = new System.Drawing.Size(96, 24);
			this.CancelButtonz.TabIndex = 9;
			this.CancelButtonz.Text = "Отменить";
			this.CancelButtonz.Click += new System.EventHandler(this.CancelButtonz_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Добавить студента";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Удалить";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.SystemColors.Info;
			this.label1.Location = new System.Drawing.Point(0, 172);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(624, 20);
			this.label1.TabIndex = 151;
			this.label1.Text = "Студенты";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.DescEdit);
			this.panel1.Controls.Add(this.NameEdit);
			this.panel1.Controls.Add(this.DateEdit);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 48);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(624, 124);
			this.panel1.TabIndex = 150;
			// 
			// DescEdit
			// 
			this.DescEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.DescEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DescEdit.CaptionLabel = null;
			this.DescEdit.eId = null;
			this.DescEdit.LanguageSwitcher = this.langSwitcher1;
			this.DescEdit.Location = new System.Drawing.Point(160, 28);
			this.DescEdit.MaxLength = 255;
			this.DescEdit.Multiline = true;
			this.DescEdit.Name = "DescEdit";
			this.DescEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.DescEdit.Size = new System.Drawing.Size(456, 68);
			this.DescEdit.TabIndex = 3;
			this.DescEdit.Text = "mlEdit2";
			// 
			// langSwitcher1
			// 
			this.langSwitcher1.Enabled = global::DCEInternalSystem.Properties.Settings.Default.LangSwitchEnabled;
			this.langSwitcher1.Location = new System.Drawing.Point(4, 2);
			this.langSwitcher1.Name = "langSwitcher1";
			this.langSwitcher1.Size = new System.Drawing.Size(176, 24);
			this.langSwitcher1.TabIndex = 2;
			this.langSwitcher1.TextLabel = "Язык";
			this.langSwitcher1.Visible = global::DCEInternalSystem.Properties.Settings.Default.LangSwitchVisible;
			// 
			// NameEdit
			// 
			this.NameEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.NameEdit.CaptionLabel = null;
			this.NameEdit.eId = null;
			this.NameEdit.LanguageSwitcher = this.langSwitcher1;
			this.NameEdit.Location = new System.Drawing.Point(160, 4);
			this.NameEdit.MaxLength = 255;
			this.NameEdit.Name = "NameEdit";
			this.NameEdit.Size = new System.Drawing.Size(456, 20);
			this.NameEdit.TabIndex = 2;
			this.NameEdit.Text = "mlEdit1";
			// 
			// DateEdit
			// 
			this.DateEdit.Location = new System.Drawing.Point(160, 100);
			this.DateEdit.Name = "DateEdit";
			this.DateEdit.Size = new System.Drawing.Size(168, 20);
			this.DateEdit.TabIndex = 4;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4, 104);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(132, 16);
			this.label6.TabIndex = 4;
			this.label6.Text = "Дата к выполнению";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 32);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(68, 16);
			this.label5.TabIndex = 2;
			this.label5.Text = "Описание";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Название";
			// 
			// label17
			// 
			this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label17.Dock = System.Windows.Forms.DockStyle.Top;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label17.ForeColor = System.Drawing.SystemColors.Info;
			this.label17.Location = new System.Drawing.Point(0, 28);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(624, 20);
			this.label17.TabIndex = 149;
			this.label17.Text = "Свойства";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.SolutionText);
			this.panel3.Controls.Add(this.panel4);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 244);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(624, 116);
			this.panel3.TabIndex = 154;
			// 
			// SolutionText
			// 
			this.SolutionText.BackColor = System.Drawing.Color.White;
			this.SolutionText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.SolutionText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SolutionText.Location = new System.Drawing.Point(0, 20);
			this.SolutionText.Multiline = true;
			this.SolutionText.Name = "SolutionText";
			this.SolutionText.ReadOnly = true;
			this.SolutionText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.SolutionText.Size = new System.Drawing.Size(468, 96);
			this.SolutionText.TabIndex = 6;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.TaskCompleteCB);
			this.panel4.Controls.Add(this.label3);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel4.Location = new System.Drawing.Point(468, 20);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(156, 96);
			this.panel4.TabIndex = 158;
			// 
			// TaskCompleteCB
			// 
			this.TaskCompleteCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TaskCompleteCB.Items.AddRange(new object[] {
            "Не проверено",
            "Не выполнено",
            "Неправильно",
            "Частично",
            "В основном правильно",
            "Правильно"});
			this.TaskCompleteCB.Location = new System.Drawing.Point(8, 36);
			this.TaskCompleteCB.Name = "TaskCompleteCB";
			this.TaskCompleteCB.Size = new System.Drawing.Size(140, 21);
			this.TaskCompleteCB.TabIndex = 7;
			this.TaskCompleteCB.SelectedIndexChanged += new System.EventHandler(this.TaskCompleteCB_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4, 12);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(136, 20);
			this.label3.TabIndex = 1;
			this.label3.Text = "Оценка решения";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.SystemColors.Info;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(624, 20);
			this.label2.TabIndex = 154;
			this.label2.Text = "Решение";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dataList
			// 
			this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
			this.dataList.AllowColumnReorder = true;
			this.dataList.AllowSorting = true;
			this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
            this.dataColumnHeader1,
            this.CompleteColumn,
            this.dataColumnHeader2});
			this.dataList.ContextMenu = this.contextMenu1;
			this.dataList.DataView = null;
			this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataList.FullRowSelect = true;
			this.dataList.GridLines = true;
			this.dataList.HideSelection = false;
			this.dataList.Location = new System.Drawing.Point(0, 192);
			this.dataList.MultiSelect = false;
			this.dataList.Name = "dataList";
			this.dataList.Size = new System.Drawing.Size(624, 52);
			this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.dataList.TabIndex = 5;
			this.dataList.UseCompatibleStateImageBehavior = false;
			this.dataList.View = System.Windows.Forms.View.Details;
			this.dataList.SelectedIndexChanged += new System.EventHandler(this.dataList_SelectedIndexChanged);
			// 
			// dataColumnHeader1
			// 
			this.dataColumnHeader1.FieldName = "StudentName";
			this.dataColumnHeader1.Text = "Имя";
			this.dataColumnHeader1.Width = 250;
			// 
			// CompleteColumn
			// 
			this.CompleteColumn.FieldName = "Complete";
			this.CompleteColumn.Text = "Решение";
			this.CompleteColumn.Width = 130;
			// 
			// dataColumnHeader2
			// 
			this.dataColumnHeader2.FieldName = "SDate";
			this.dataColumnHeader2.Text = "Когда решено";
			this.dataColumnHeader2.Width = 120;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.langSwitcher1);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(0, 0);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(624, 28);
			this.panel5.TabIndex = 156;
			// 
			// TaskEdit
			// 
			this.Controls.Add(this.dataList);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel5);
			this.Name = "TaskEdit";
			this.Size = new System.Drawing.Size(624, 392);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.ResumeLayout(false);

      }
		#endregion

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         DataRowView row = UserSelect.SelectTrainingStudent(this.Node.SolutionsDV,"Student", this.Node.TrainingId);
         if (row !=null)
         {
			//Prevents DataList.ListChanged from firing
			this.dataList.SuspendListChange = true;
			DataRowView newrow = this.Node.SolutionsDV.AddNew();
			this.dataList.SuspendListChange = false;
            newrow["StudentName"] = row["LastName"].ToString()+" "+row["FirstName"].ToString()+" "+row["Patronymic"].ToString();
            newrow["id"] = System.Guid.NewGuid();
            newrow["Task"] = this.Node.Id;
            newrow["Student"] = row["id"];
            newrow["Complete"] = 0;
            newrow["Solution"] = System.DBNull.Value;
            newrow.EndEdit();
            this.Node.Changed = true;
         }
      }

      private void OkButtonz_Click(object sender, System.EventArgs e)
      {
         try
         {
            this.dataList.SuspendListChange = true;
            this.Node.EndEdit(false,false);
         }
         finally
         {
            this.dataList.SuspendListChange = false;
            this.dataList.UpdateList();
         }

      }

      private void CancelButtonz_Click(object sender, System.EventArgs e)
      {
         this.Node.EndEdit(true,this.Node.IsNew);
      }

      private void dataList_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;
            this.SolutionText.Text = row["Solution"].ToString();
            this.updating = true;
            this.TaskCompleteCB.SelectedIndex = (int)row["Complete"];
            this.updating = false;
         }
      }

      private void menuItem2_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            if (MessageBox.Show("Удалить поставленную задачу для выбранного студента?","Удалить",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
               DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;
               row.Delete();
               this.Node.Changed = true;
            }
         }
      }

      private bool updating = true;
      private void TaskCompleteCB_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ///
         if (!updating)
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;
            row.BeginEdit();
            row["Complete"] = TaskCompleteCB.SelectedIndex;
            row.EndEdit();
            this.Node.Changed = true;
         }
      }

	}

   public class TaskEditNode : RecordEditNode
   {
      public string TrainingId;
      // public string TaskId;
      public DataSet SolutionsDS;
      public DataView SolutionsDV;

      public TaskEditNode (NodeControl parent,string trainingId, string taskId)
         : base(parent,"select * from Tasks where Training ='"+trainingId+"'","Tasks","id",taskId)
      {
         this.TrainingId=trainingId;

         if (this.IsNew)
            this.EditRow["Training"] = this.TrainingId;

         this.SolutionsDV = new DataView();
         RefreshData();
         this.OnReloadContent += new RecordEditNode.ReloadContentHandler(RefreshData);
         this.OnUpdateDataSet += new RecordEditNode.EnumDataSetsHandler(UpdateSolutions);
      }

      protected bool UpdateSolutions(string TransactionId, ref DCEAccessLib.DataSetBatchUpdate dataSet)
      {
         this.SolutionsDS.Tables["sol"].Columns.Remove("StudentName");
         dataSet.dataSet = this.SolutionsDS;
         dataSet.sql = "select * from dbo.TaskSolutions where id='"+this.Id+"'";
         dataSet.tableName = "sol";
         return true;
      }

      public void RefreshData()
      {
		  this.SolutionsDS = DCEAccessLib.DAL.Task.GetSolutions(new Guid(this.Id));
         this.SolutionsDV.Table = this.SolutionsDS.Tables["sol"];
      }

      protected override void InitNewRecord()
      {
         this.EditRow["id"] = System.Guid.NewGuid();
         this.EditRow["Creator"] = DCEUser.CurrentUser.id; // TODO: change to uid of logged-in user
         this.EditRow["Name"] = System.Guid.NewGuid();
         this.EditRow["Description"] = System.Guid.NewGuid();
         this.EditRow["TaskTime"] = System.DateTime.Now;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new TaskEdit(this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         if (this.EditRow == null)
            return "";
         if (this.IsNew)
            return "[Новое задание]";
         return DCEAccessLib.DCEWebAccess.WebAccess.GetString("select dbo.GetStrContentAlt(Name,'RU','EN') from Tasks where id ='"+this.Id+"'");
      }

      public override bool CanClose()
      {
         return true;
      }
      public override void ReleaseControl() 
      {
      }
   }

}
