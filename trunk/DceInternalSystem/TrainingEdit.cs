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
	/// Редактирование тренинга
	/// </summary>
	public class TrainingEdit : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button OkButton;
      private System.Windows.Forms.Button CancelButton;
      private System.Windows.Forms.Panel panel2;
      private DCEAccessLib.LangSwitcher langSwitcher1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.Panel panel3;
      private DCEAccessLib.MLEdit CComment;
      private DCEAccessLib.MLEdit CName;
      private System.Windows.Forms.DateTimePicker StartDate;
      private System.Windows.Forms.TextBox CCode;
      private System.Windows.Forms.ComboBox CourseSelect;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label StartDateLbl;
      private System.Windows.Forms.CheckBox CActive;
      private System.Windows.Forms.CheckBox TestOnly;

      TrainingEditControl Node;
      private class CourseItem : System.Object
      {
         public string Name;
         public string Id;
         public string Code;
         public int Lang;

         public override string ToString()
         {
            return Name;
         }

         public CourseItem(string name, string _id)
         {
            Name = name;
            Id = _id;
         }
         public CourseItem(string name, string _id, string _code, int _lang)
         {
            Name = name;
            Id = _id;
            Code = _code;
            Lang = _lang;
         }
      }

		public TrainingEdit(TrainingEditControl node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         Node = node;

         // databindings
         
         CName.SetParentNode(this.Node);

         CComment.SetParentNode(this.Node);

         RebindControls();
         this.Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(RebindControls);
      }

      protected bool rebinding;
      public void RebindControls()
      {
         rebinding = true;
         try
         {
            // filling course list
            CourseSelect.Items.Clear();      
            DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet
               ("select dbo.GetStrContentAlt(Name, 'RU','EN') as RName, id , Code, CourseLanguage from dbo.Courses","Courses");
            foreach (DataRow row in ds.Tables["Courses"].Rows )
            {
               CourseSelect.Items.Add(new CourseItem(row["RName"].ToString(),row["id"].ToString(),row["Code"].ToString(), (int)row["CourseLanguage"]));
            }
            CourseSelect.Sorted = true;
            if (!Node.IsNew)
            {
               foreach (CourseItem itm in CourseSelect.Items)
               {
                  if (itm.Id == Node.EditRow["Course"].ToString())
                  {
                     this.langSwitcher1.Language = itm.Lang;
                     CourseSelect.SelectedItem = itm;
                     CourseSelect.Enabled = false;
                     break;
                  }
               }
            }
            ds = null;

            CCode.DataBindings.Clear();
            CCode.DataBindings.Add("Text", Node.EditRow, "Code");
            CCode.MaxLength = 50;
//            CPublic.DataBindings.Clear();
//            CPublic.DataBindings.Add("Checked",Node.EditRow, "isPublic");
//            CTimeStrict.DataBindings.Clear();
//            CTimeStrict.DataBindings.Add("Checked",Node.EditRow,"TimeStrict");
            StartDate.DataBindings.Clear();
            StartDate.DataBindings.Add("Value",Node.EditRow,"StartDate");
            StartDate.Visible = this.Node.IsNew;
            StartDateLbl.Visible = this.Node.IsNew;
            //         EndDate.DataBindings.Clear();
            //         EndDate.DataBindings.Add("Value",Node.EditRow,"EndDate");
            CName.DataBindings.Clear();
            CName.DataBindings.Add("eId",Node.EditRow,"Name");
            CComment.DataBindings.Clear();
            CComment.DataBindings.Add("eId",Node.EditRow,"Comment");
            CActive.DataBindings.Clear();
            CActive.DataBindings.Add("Checked",Node.EditRow,"isActive");
            TestOnly.DataBindings.Clear();
            TestOnly.DataBindings.Add("Checked",Node.EditRow,"TestOnly");
            this.Node.CreateChilds();
            if (this.Node.Readonly)
            {
               OkButton.Enabled = false;
               CCode.Enabled = false;
               CName.Enabled  = false;
               CComment.Enabled = false;
//               CPublic.Enabled = false;
//               CTimeStrict.Enabled = false;
               CActive.Enabled = false;
               StartDate.Enabled = false;
            }
         }
         finally
         {
            rebinding = false;
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.langSwitcher1 = new DCEAccessLib.LangSwitcher();
			this.label9 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.TestOnly = new System.Windows.Forms.CheckBox();
			this.CActive = new System.Windows.Forms.CheckBox();
			this.CComment = new DCEAccessLib.MLEdit();
			this.CName = new DCEAccessLib.MLEdit();
			this.StartDate = new System.Windows.Forms.DateTimePicker();
			this.CCode = new System.Windows.Forms.TextBox();
			this.StartDateLbl = new System.Windows.Forms.Label();
			this.CourseSelect = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.OkButton);
			this.panel1.Controls.Add(this.CancelButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 384);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(676, 36);
			this.panel1.TabIndex = 20;
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.OkButton.Location = new System.Drawing.Point(468, 4);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(96, 24);
			this.OkButton.TabIndex = 10;
			this.OkButton.Text = "Ok";
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CancelButton.Location = new System.Drawing.Point(572, 4);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(96, 24);
			this.CancelButton.TabIndex = 11;
			this.CancelButton.Text = "Отменить";
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.langSwitcher1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(676, 28);
			this.panel2.TabIndex = 21;
			// 
			// langSwitcher1
			// 
			this.langSwitcher1.Enabled = global::DCEInternalSystem.Properties.Settings.Default.LangSwitchEnabled;
			this.langSwitcher1.Location = new System.Drawing.Point(4, 4);
			this.langSwitcher1.Name = "langSwitcher1";
			this.langSwitcher1.Size = new System.Drawing.Size(188, 20);
			this.langSwitcher1.TabIndex = 1;
			this.langSwitcher1.TextLabel = "Язык";
			this.langSwitcher1.Visible = global::DCEInternalSystem.Properties.Settings.Default.LangSwitchVisible;
			// 
			// label9
			// 
			this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label9.Dock = System.Windows.Forms.DockStyle.Top;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label9.ForeColor = System.Drawing.SystemColors.Info;
			this.label9.Location = new System.Drawing.Point(0, 28);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(676, 20);
			this.label9.TabIndex = 23;
			this.label9.Text = "Свойства";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel3
			// 
			this.panel3.AutoScroll = true;
			this.panel3.Controls.Add(this.TestOnly);
			this.panel3.Controls.Add(this.CActive);
			this.panel3.Controls.Add(this.CComment);
			this.panel3.Controls.Add(this.CName);
			this.panel3.Controls.Add(this.StartDate);
			this.panel3.Controls.Add(this.CCode);
			this.panel3.Controls.Add(this.StartDateLbl);
			this.panel3.Controls.Add(this.CourseSelect);
			this.panel3.Controls.Add(this.label5);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 48);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(676, 336);
			this.panel3.TabIndex = 24;
			// 
			// TestOnly
			// 
			this.TestOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TestOnly.Location = new System.Drawing.Point(16, 152);
			this.TestOnly.Name = "TestOnly";
			this.TestOnly.Size = new System.Drawing.Size(196, 44);
			this.TestOnly.TabIndex = 6;
			this.TestOnly.Text = "Только для тестирования (без учебных материалов)";
			// 
			// CActive
			// 
			this.CActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CActive.Location = new System.Drawing.Point(16, 224);
			this.CActive.Name = "CActive";
			this.CActive.Size = new System.Drawing.Size(212, 20);
			this.CActive.TabIndex = 8;
			this.CActive.Text = "Активный";
			// 
			// CComment
			// 
			this.CComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.CComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CComment.CaptionLabel = null;
			this.CComment.eId = "";
			this.CComment.LanguageSwitcher = this.langSwitcher1;
			this.CComment.Location = new System.Drawing.Point(168, 104);
			this.CComment.MaxLength = 255;
			this.CComment.Multiline = true;
			this.CComment.Name = "CComment";
			this.CComment.Size = new System.Drawing.Size(496, 32);
			this.CComment.TabIndex = 5;
			// 
			// CName
			// 
			this.CName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.CName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CName.CaptionLabel = null;
			this.CName.eId = "";
			this.CName.LanguageSwitcher = this.langSwitcher1;
			this.CName.Location = new System.Drawing.Point(168, 72);
			this.CName.MaxLength = 255;
			this.CName.Name = "CName";
			this.CName.Size = new System.Drawing.Size(496, 20);
			this.CName.TabIndex = 4;
			// 
			// StartDate
			// 
			this.StartDate.Location = new System.Drawing.Point(160, 248);
			this.StartDate.Name = "StartDate";
			this.StartDate.Size = new System.Drawing.Size(176, 20);
			this.StartDate.TabIndex = 9;
			this.StartDate.Value = new System.DateTime(2003, 9, 3, 0, 0, 0, 0);
			// 
			// CCode
			// 
			this.CCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CCode.Location = new System.Drawing.Point(168, 40);
			this.CCode.Name = "CCode";
			this.CCode.Size = new System.Drawing.Size(152, 20);
			this.CCode.TabIndex = 3;
			// 
			// StartDateLbl
			// 
			this.StartDateLbl.Location = new System.Drawing.Point(16, 256);
			this.StartDateLbl.Name = "StartDateLbl";
			this.StartDateLbl.Size = new System.Drawing.Size(140, 12);
			this.StartDateLbl.TabIndex = 202;
			this.StartDateLbl.Text = "Дата начала";
			this.StartDateLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CourseSelect
			// 
			this.CourseSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.CourseSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CourseSelect.Items.AddRange(new object[] {
            "Программирование на Си"});
			this.CourseSelect.Location = new System.Drawing.Point(168, 8);
			this.CourseSelect.Name = "CourseSelect";
			this.CourseSelect.Size = new System.Drawing.Size(496, 21);
			this.CourseSelect.TabIndex = 2;
			this.CourseSelect.SelectedIndexChanged += new System.EventHandler(this.CourseSelect_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 12);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(140, 12);
			this.label5.TabIndex = 199;
			this.label5.Text = "Курс";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(140, 16);
			this.label3.TabIndex = 197;
			this.label3.Text = "Комментарии";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(140, 12);
			this.label2.TabIndex = 196;
			this.label2.Text = "Название";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 12);
			this.label1.TabIndex = 195;
			this.label1.Text = "Код";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TrainingEdit
			// 
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "TrainingEdit";
			this.Size = new System.Drawing.Size(676, 420);
			this.Leave += new System.EventHandler(this.TrainingEdit_Leave);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

      private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
      {
      
      }

      private void tabPage1_Click(object sender, System.EventArgs e)
      {
      
      }


      private void OkButton_Click(object sender, System.EventArgs e)
      {
         if (this.CourseSelect.SelectedIndex>=0)
         {
            this.Node.EditRow["Course"] = ((CourseItem)CourseSelect.SelectedItem).Id;
            this.Node.EndEdit(false,false);
         }
         else
         {
            System.Windows.Forms.MessageBox.Show("Для добавления нового тренинга, выберите курс.","Внимание!");
         }
      }

      private void CancelButton_Click(object sender, System.EventArgs e)
      {
         if (Node.IsNew)
            this.Node.EndEdit(true, true);
         else
            this.Node.EndEdit(true, false);
      }


      private void CourseSelect_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (this.rebinding) return;
         if (CourseSelect.SelectedIndex>=0)
         {
            CourseItem itm = CourseSelect.Items[CourseSelect.SelectedIndex] as CourseItem;
//            DataSet cds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("Select c.* from dbo.Content c , Courses cr where cr.id='"+itm.Id+"' and c.eid=cr.Name","dbo.Content");
//            this.CName.ReplaceData(cds,"dbo.Content");
            this.langSwitcher1.Language = itm.Lang;
            this.Node.EditRow["Code"] = itm.Code;
            this.CCode.Text = itm.Code;
         }
      }

		private void TrainingEdit_Leave(object sender, System.EventArgs e)
		{
			//System.Windows.Forms.MessageBox.Show("Leave");
         //this.Node.EditRow.DataView.Table.
		}
	}

   public class TrainingEditControl : RecordEditNode 
   {

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new TrainingEdit(this);
         }
         return this.fControl;
      }

      // additional handling for updating training record

      public static DateTime SkipHolydays(DateTime date, int days)
      {
         if (date.DayOfWeek == DayOfWeek.Saturday)
            date = date.AddDays(2);
         if (date.DayOfWeek == DayOfWeek.Sunday)
            date = date.AddDays(1);

         while (days>0)
         {
            date = date.AddDays(1);
            days--;
            if (date.DayOfWeek == DayOfWeek.Saturday)
               date = date.AddDays(2);
            if (date.DayOfWeek == DayOfWeek.Sunday)
               date = date.AddDays(1);

         } while (days>0);
         return date;
      }

      public static bool CreateSchedule(string courseId, string trainingId, ref DCEAccessLib.DataSetBatchUpdate dataSet, System.DateTime startDate)
      {
         // creating a schedule
         DataSet Themes = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("Select * from dbo.Themes where Parent ='" +courseId+ "' and Type="+((int)ThemeType.theme).ToString() +" order by TOrder", "Themes");
         DataSet schedule = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("Select * from dbo.Schedule where Training = '"+ trainingId +"'","schedule");

         startDate = SkipHolydays( startDate,0);

         foreach (DataRow theme in Themes.Tables["Themes"].Rows)
         {
            DataRow shdrow = schedule.Tables["schedule"].NewRow();
            shdrow["id"] = System.Guid.NewGuid();
            shdrow["Training"] = trainingId;
            shdrow["Theme"] = theme["id"];
            shdrow["StartDate"] = startDate;
            startDate = SkipHolydays(startDate,((int)theme["Duration"])-1);
            shdrow["EndDate"] = startDate;
            startDate = SkipHolydays(startDate,1);
            shdrow["isOpen"] = false;
            if (theme["Mandatory"].GetType() != typeof(System.DBNull) )
               shdrow["Mandatory"] = theme["Mandatory"];
            else
               shdrow["Mandatory"] = true;
            schedule.Tables["schedule"].Rows.Add(shdrow);
         }
         dataSet.dataSet = schedule;
         dataSet.sql = "Select * from dbo.Schedule where Training = '"+ trainingId +"'";
         dataSet.tableName = "schedule";
         
         return Themes.Tables["Themes"].Rows.Count > 0;
      }

      bool UpdateDataSet(string TransactionId, ref DCEAccessLib.DataSetBatchUpdate dataSet)
      {
         string courseId = this.EditRow["Course"].ToString();
         string trainingId = this.EditRow["id"].ToString();
         if (this.IsNew)
         {
            bool res = CreateSchedule(courseId, trainingId, ref dataSet, (DateTime)this.EditRow["StartDate"]);
            if (res)
            {
               DateTime maxDate = (DateTime)dataSet.dataSet.Tables["schedule"].Rows[0]["EndDate"];
               foreach (DataRow row in dataSet.dataSet.Tables["schedule"].Rows)
               {
                  if (maxDate < (DateTime)row["EndDate"])
                     maxDate = (DateTime)row["EndDate"];
               }
               this.EditRow.BeginEdit();
               this.EditRow["EndDate"] = maxDate;
               this.EditRow.EndEdit();
            }
            return res;
         }
         return false;
      }

      public TrainingEditControl(NodeControl parent,string id)
            : base(parent, "select * from dbo.Trainings","Trainings", "id", id)
      {
         this.rdonly = DCEUser.CurrentUser.Trainings != DCEUser.Access.Modify;
         this.OnUpdateDataSet += new RecordEditNode.EnumDataSetsHandler(this.UpdateDataSet);
         this.Expand();
      }

      public override String GetCaption()
      {
         if (EditRow!=null)
         {
            if (IsNew)
               return "Тренинг: (Новый)";
            else
            {
               DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select dbo.GetStrContentAlt('"+this.EditRow["Name"].ToString()+"','RU','EN')","nm");
               string res;
               if (ds.Tables["nm"].Rows.Count>0)
                  res = "Тренинг: " + ds.Tables["nm"].Rows[0][0].ToString();
               else 
                  res = "Тренинг: " + EditRow["Code"].ToString();
               ds.Dispose();
               return res;
            }
         }
         return "";
      }

      /// <summary>
      /// Initialize new record for editing (EditRow)
      /// </summary>
      protected override void InitNewRecord()
      {
         // filling all non-nullable fields
         EditRow ["id"] = System.Guid.NewGuid().ToString();
         EditRow ["Name"] = System.Guid.NewGuid().ToString();
         EditRow ["Comment"]  = System.Guid.NewGuid().ToString();
         EditRow ["Instructors"]  = System.Guid.NewGuid().ToString();
         EditRow ["Curators"]  = System.Guid.NewGuid().ToString();
         EditRow ["Students"]  = System.Guid.NewGuid().ToString();
         EditRow ["TimeStrict"]  = 0;
         EditRow ["isPublic"]  = 0;
         EditRow ["isActive"]  = 0;
         EditRow ["StartDate"] = System.DateTime.Now;
         EditRow ["EndDate"] = System.DateTime.Now;
         EditRow ["TestOnly"] = 0;
      }

      public override bool HaveChildNodes()
      {
         return true;
      }
      public override bool CanClose()
      {
         return true;
      }

      public override void ReleaseControl()
      {
         // do nothing
      }

      public override void CreateChilds()
      {
         if (!this.IsNew)
         {
            bool create = true;
            foreach (NodeControl node in this.Nodes)
            {
               if (node.GetType() == typeof(GroupEditNode))
               {
                  create = false;
                  break;
               }
            }
            if (create)
            {
               new GroupEditNode(this,this.EditRow["Instructors"].ToString(),EntityType.user,true,"Инструкторы",this.Id,false,false);
               new GroupEditNode(this,this.EditRow["Curators"].ToString(),EntityType.user, true,"Кураторы",this.Id,false,false);
               new GroupEditNode(this,this.EditRow["Students"].ToString(),EntityType.student, false,"Студенты",this.Id,false,false);
               new TrainingScheduleControl (this,this.Id);
               new TrainingWorksNode(this,this.Id);
               new TrainingBBNode (this,this.Id);
               //new TrainingBlockingNode (this,this.Id);
            }
         }
      }
   }
}
