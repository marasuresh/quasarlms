using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DCEAccessLib;

namespace DCEInternalSystem
{
	/// <summary>
	/// Редактирование свойств пользователя
	/// </summary>
	public class UserEdit : System.Windows.Forms.UserControl
	{
      private System.Data.DataSet photos = null;
      private System.Data.DataTable photoTable = null;

      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button CancelButton;
      private System.Windows.Forms.Button OkButton;
      private System.ComponentModel.IContainer components;
      private System.Windows.Forms.TabControl tabControl1;
      private System.Windows.Forms.TabPage tabPage1;
      private System.Windows.Forms.TabPage tabPage2;
      private System.Windows.Forms.Label label19;
      private System.Windows.Forms.Label label20;
      private System.Windows.Forms.Label label21;
      private System.Windows.Forms.Label label22;
      private System.Windows.Forms.Label label23;
      private System.Windows.Forms.Label label24;
      private System.Windows.Forms.Label label25;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.MenuItem menuItem5;
      private System.Windows.Forms.ComboBox RUsers;
      private System.Windows.Forms.ComboBox RCourses;
      private System.Windows.Forms.ComboBox RQuestionnaire;
      private System.Windows.Forms.ComboBox RTrainings;
      private System.Windows.Forms.ComboBox RShedule;
      private System.Windows.Forms.ComboBox RRequests;
      private System.Windows.Forms.ComboBox RTests;
      private System.Windows.Forms.ToolTip toolTip1;

      #region class UserRights
      public class UserRights
      {
         private DataRowView brow;
         public string Rights
         {
            get 
            {
               return brow["Rights"].ToString();
            }
            set
            {
               brow["Rights"] = snapr(value);
            }
         }

         public string snapr(string r)
         {
            string t="";
            for (int i=0; i<50-r.Length; i++)
               t+="-";
            r+=t;
            return r;
         }

         public UserRights(DataRowView row)
         {
            brow = row;
            string r = brow["Rights"].ToString();
            if (r.Length <50)
               brow["Rights"] = snapr(r);
            //bindings.Add(new Binding("Rights",row,"Rights"));
         }

         public int RtoI(int pos)
         {
            char c = Rights[pos];
            if (c == 'W')
               return 2;
            if (c== 'R')
               return 1;
            return 0;
         }


         public void ItoR(int pos,int r)
         {
            char c = '-';
            if (r==1)
               c = 'R';
            if (r==2)
               c = 'W';
            string rights ="";
            for (int i=0; i<50; i++)
            {
               if (i==pos)
                  rights += c;
               else
                  rights += Rights[i];
            }
            Rights = rights;
         }

         public int RUsers
         {
            get 
            {
               return RtoI(0);
            }
            set
            {
               ItoR(0,value);
            }
         }

         public int RCourses
         {
            get 
            {
               return RtoI(1);
            }
            set
            {
               ItoR(1,value);
            }
         }

         public int RTrainings
         {
            get 
            {
               return RtoI(2);
            }
            set
            {
               ItoR(2,value);
            }
         }
         public int RRequests
         {
            get 
            {
               return RtoI(3);
            }
            set
            {
               ItoR(3,value);
            }
         }
         public int RShedule
         {
            get 
            {
               return RtoI(4);
            }
            set
            {
               ItoR(4,value);
            }
         }
         public int RTests
         {
            get 
            {
               return RtoI(5);
            }
            set
            {
               ItoR(5,value);
            }
         }
         public int RQuestionnaire
         {
            get 
            {
               return RtoI(6);
            }
            set
            {
               ItoR(6,value);
            }
         }
         public int RStudents
         {
            get 
            {
               return RtoI(7);
            }
            set
            {
               ItoR(7,value);
            }
         }
         public int RNews
         {
            get 
            {
               return RtoI(8);
            }
            set
            {
               ItoR(8,value);
            }
         }

      }
      #endregion

      private UserRights rights;
      private UserEditNode Node;
      private System.Windows.Forms.Label TitleLabel;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.ComboBox Sex;
      private System.Windows.Forms.PictureBox pictureBox1;
      private System.Windows.Forms.Button ChangePwd;
      private System.Windows.Forms.TextBox Login;
      private System.Windows.Forms.TextBox FirstNameEng;
      private System.Windows.Forms.TextBox FirstName;
      private System.Windows.Forms.Label label18;
      private System.Windows.Forms.Label label16;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label28;
      private System.Windows.Forms.Panel panel3;
      private System.Windows.Forms.TextBox Email;
      private System.Windows.Forms.TextBox Phone;
      private System.Windows.Forms.TextBox Address;
      private System.Windows.Forms.Label label11;
      private System.Windows.Forms.Label label10;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.Label label17;
      private System.Windows.Forms.Panel panel4;
      private System.Windows.Forms.TextBox Courses;
      private System.Windows.Forms.TextBox Education;
      private System.Windows.Forms.TextBox Certificates;
      private System.Windows.Forms.Label label14;
      private System.Windows.Forms.Label label13;
      private System.Windows.Forms.Label label12;
      private System.Windows.Forms.Label label27;
      private System.Windows.Forms.TextBox LastNameEng;
      private System.Windows.Forms.TextBox Patronymic;
      private System.Windows.Forms.TextBox LastName;
      private System.Windows.Forms.ComboBox RStudents;
      private System.Windows.Forms.Label label15;
      private System.Windows.Forms.OpenFileDialog openFileDialog1;
      private System.Windows.Forms.DateTimePicker Birthday;
      private System.Windows.Forms.ComboBox RNews;
      private System.Windows.Forms.Label label26;
      private System.Windows.Forms.ComboBox JobPosition;
      private System.Windows.Forms.Panel panel5;
      private System.Windows.Forms.TextBox Comments;
      private System.Windows.Forms.Panel panel6;
      private System.Windows.Forms.Panel panel7;
      private System.Windows.Forms.Button stdRightsBtn;
      


      public void RebindControls()
      {

         this.FirstName.DataBindings.Clear();
         this.FirstName.DataBindings.Add("Text",this.Node.EditRow,"FirstName");
         this.FirstName.MaxLength = 255;
         this.LastName.DataBindings.Clear();
         this.LastName.DataBindings.Add("Text",this.Node.EditRow,"LastName");
         this.LastName.MaxLength = 255;
         this.Patronymic.DataBindings.Clear();
         this.Patronymic.DataBindings.Add("Text",this.Node.EditRow,"Patronymic");
         this.Patronymic.MaxLength = 255;

         this.Birthday.DataBindings.Clear();
         if (Node.EditRow["Birthday"] == System.DBNull.Value )
         {
            Node.EditRow["Birthday"]= DateTime.Now;
         }
         else
            if ( ((DateTime)Node.EditRow["Birthday"]) > DateTime.Now)
               Node.EditRow["Birthday"]= DateTime.Now;

         this.Birthday.DataBindings.Add("Value",this.Node.EditRow,"Birthday");

         this.FirstNameEng.DataBindings.Clear();
         this.FirstNameEng.DataBindings.Add("Text",this.Node.EditRow,"FirstNameEng");
         this.FirstNameEng.MaxLength = 255;
         this.LastNameEng.DataBindings.Clear();
         this.LastNameEng.DataBindings.Add("Text",this.Node.EditRow,"LastNameEng");
         this.LastNameEng.MaxLength = 255;
         this.Sex.DataBindings.Clear();
         this.Sex.DataBindings.Add("SelectedIndex",this.Node.EditRow,"Sex");
         this.JobPosition.DataBindings.Clear();
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            "select distinct JobPosition from Users order by JobPosition", "P");
         JobPosition.Items.Clear();
         foreach(DataRow row in ds.Tables["P"].Rows)
         {
            JobPosition.Items.Add(row[0].ToString());
         }
         this.JobPosition.DataBindings.Add("Text",this.Node.EditRow,"JobPosition");
         this.JobPosition.MaxLength = 255;
         this.Address.DataBindings.Clear();
         this.Address.DataBindings.Add("Text",this.Node.EditRow,"Address");
         this.Address.MaxLength = 255;
         this.Phone.DataBindings.Clear();
         this.Phone.DataBindings.Add("Text",this.Node.EditRow,"Phone");
         this.Phone.MaxLength = 100;
         this.Email.DataBindings.Clear();
         this.Email.DataBindings.Add("Text",this.Node.EditRow,"Email");
         this.Email.MaxLength = 100;
         this.Education.DataBindings.Clear();
         this.Education.DataBindings.Add("Text",this.Node.EditRow,"Education");
         this.Education.MaxLength = 255;
         this.Courses.DataBindings.Clear();
         this.Courses.DataBindings.Add("Text",this.Node.EditRow,"Courses");
         this.Courses.MaxLength = 255;
         this.Certificates.DataBindings.Clear();
         this.Certificates.DataBindings.Add("Text",this.Node.EditRow,"Certificates");
         this.Certificates.MaxLength = 255;
         this.Comments.DataBindings.Clear();
         this.Comments.DataBindings.Add("Text",this.Node.EditRow,"Comments");
         this.Comments.MaxLength = 255;
         this.Login.DataBindings.Clear();
         this.Login.DataBindings.Add("Text",this.Node.EditRow,"Login");
         this.Login.MaxLength = 20;

         rights = new UserRights(this.Node.EditRow);
         this.RUsers.DataBindings.Clear();
         this.RUsers.DataBindings.Add(new Binding("SelectedIndex",rights,"RUsers"));
         this.RCourses.DataBindings.Clear();
         this.RCourses.DataBindings.Add(new Binding("SelectedIndex",rights,"RCourses"));
         this.RTrainings.DataBindings.Clear();
         this.RTrainings.DataBindings.Add(new Binding("SelectedIndex",rights,"RTrainings"));
         this.RRequests.DataBindings.Clear();
         this.RRequests.DataBindings.Add(new Binding("SelectedIndex",rights,"RRequests"));
         this.RShedule.DataBindings.Clear();
         this.RShedule.DataBindings.Add(new Binding("SelectedIndex",rights,"RShedule"));
         this.RTests.DataBindings.Clear();
         this.RTests.DataBindings.Add(new Binding("SelectedIndex",rights,"RTests"));
         this.RQuestionnaire.DataBindings.Clear();
         this.RQuestionnaire.DataBindings.Add(new Binding("SelectedIndex",rights,"RQuestionnaire"));
         this.RStudents.DataBindings.Clear();
         this.RStudents.DataBindings.Add(new Binding("SelectedIndex",rights,"RStudents"));
         this.RNews.DataBindings.Clear();
         this.RNews.DataBindings.Add(new Binding("SelectedIndex",rights,"RNews"));

         if ( Node.GetType() == typeof(PersonalProfileNode) 
            && DCEUser.CurrentUser.Users != DCEUser.Access.Modify )
         {
            Login.ReadOnly = true;
            RUsers.Enabled = false;
            RCourses.Enabled = false;
            RTrainings.Enabled = false;
            RRequests.Enabled = false;
            RShedule.Enabled = false;
            RTests.Enabled = false;
            RQuestionnaire.Enabled = false;
            RStudents.Enabled = false;
            RNews.Enabled = false;
            stdRightsBtn.Enabled = false;

         }
         else
            if (DCEUser.CurrentUser.Users != DCEUser.Access.Modify )
         {
            Login.ReadOnly = true;
            RUsers.Enabled = false;
            RCourses.Enabled = false;
            RTrainings.Enabled = false;
            RRequests.Enabled = false;
            RShedule.Enabled = false;
            RTests.Enabled = false;
            RQuestionnaire.Enabled = false;
            RStudents.Enabled = false;
            RNews.Enabled = false;
            stdRightsBtn.Enabled = false;
            OkButton.Enabled = false;
            ChangePwd.Enabled = false;
            pictureBox1.Enabled = false;

            this.FirstName.Enabled = false;
            this.LastName.Enabled = false;
            this.Patronymic.Enabled = false;
            this.Birthday.Enabled = false;
            this.FirstNameEng.Enabled = false;
            this.LastNameEng.Enabled = false;
            this.Sex.Enabled = false;
            this.JobPosition.Enabled = false;
            this.Address.Enabled = false;
            this.Phone.Enabled = false;
            this.Email.Enabled = false;
            this.Education.Enabled = false;
            this.Courses.Enabled = false;
            this.Certificates.Enabled = false;
            this.Comments.Enabled = false;
         }

         this.photos = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            "select * from Content where eid='"+this.Node.EditRow["Photo"].ToString()
            +"'", "Photos");
         photoTable = photos.Tables["Photos"];
         System.Data.DataRow pRow = null;
         if (photoTable != null && photoTable.Rows.Count == 0)
         {
            pRow = photoTable.NewRow();
            pRow["eid"] = this.Node.EditRow["Photo"];
            pRow["Type"] = DCEAccessLib.ContentType._image;
            photoTable.Rows.Add(pRow);
         }
         else
            pRow = photoTable.Rows[0];

         if (pRow["Data"] != System.DBNull.Value)
            this.pictureBox1.Image = new Bitmap(
               new System.IO.MemoryStream((byte[]) pRow["Data"]));
      }

		public UserEdit(UserEditNode node)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
         Node = node;

         Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(RebindControls);
         Node.OnUpdateDataSet += new RecordEditNode.EnumDataSetsHandler(this.OnUpdateDataSet);

         RebindControls();
      }

      public bool OnUpdateDataSet(string TransactionId, ref DCEAccessLib.DataSetBatchUpdate dataSet)
      {
            dataSet.sql = "select * from Content where eid='"
               +this.Node.EditRow["Photo"].ToString()+"'";
            dataSet.tableName ="Photos";
            dataSet.dataSet = this.photos;
            return this.photoTable.Rows[0]["Data"].GetType() != typeof(System.DBNull);
      }

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserEdit));
			this.panel1 = new System.Windows.Forms.Panel();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.panel5 = new System.Windows.Forms.Panel();
			this.Comments = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.Courses = new System.Windows.Forms.TextBox();
			this.Education = new System.Windows.Forms.TextBox();
			this.Certificates = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.Email = new System.Windows.Forms.TextBox();
			this.Phone = new System.Windows.Forms.TextBox();
			this.Address = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.JobPosition = new System.Windows.Forms.ComboBox();
			this.Birthday = new System.Windows.Forms.DateTimePicker();
			this.Sex = new System.Windows.Forms.ComboBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.ChangePwd = new System.Windows.Forms.Button();
			this.Login = new System.Windows.Forms.TextBox();
			this.LastNameEng = new System.Windows.Forms.TextBox();
			this.FirstNameEng = new System.Windows.Forms.TextBox();
			this.Patronymic = new System.Windows.Forms.TextBox();
			this.LastName = new System.Windows.Forms.TextBox();
			this.FirstName = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.panel6 = new System.Windows.Forms.Panel();
			this.RTests = new System.Windows.Forms.ComboBox();
			this.RRequests = new System.Windows.Forms.ComboBox();
			this.RShedule = new System.Windows.Forms.ComboBox();
			this.RTrainings = new System.Windows.Forms.ComboBox();
			this.RQuestionnaire = new System.Windows.Forms.ComboBox();
			this.label25 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.RCourses = new System.Windows.Forms.ComboBox();
			this.RNews = new System.Windows.Forms.ComboBox();
			this.label26 = new System.Windows.Forms.Label();
			this.RStudents = new System.Windows.Forms.ComboBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.RUsers = new System.Windows.Forms.ComboBox();
			this.panel7 = new System.Windows.Forms.Panel();
			this.stdRightsBtn = new System.Windows.Forms.Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.panel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tabPage2.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel7.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.OkButton);
			this.panel1.Controls.Add(this.CancelButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 384);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(448, 40);
			this.panel1.TabIndex = 18;
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.OkButton.Location = new System.Drawing.Point(238, 8);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(96, 24);
			this.OkButton.TabIndex = 18;
			this.OkButton.Text = "Применить";
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CancelButton.Location = new System.Drawing.Point(342, 8);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(96, 24);
			this.CancelButton.TabIndex = 19;
			this.CancelButton.Text = "Отменить";
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(448, 384);
			this.tabControl1.TabIndex = 19;
			// 
			// tabPage1
			// 
			this.tabPage1.AutoScroll = true;
			this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tabPage1.Controls.Add(this.panel5);
			this.tabPage1.Controls.Add(this.label27);
			this.tabPage1.Controls.Add(this.panel4);
			this.tabPage1.Controls.Add(this.label17);
			this.tabPage1.Controls.Add(this.panel3);
			this.tabPage1.Controls.Add(this.label28);
			this.tabPage1.Controls.Add(this.panel2);
			this.tabPage1.Controls.Add(this.TitleLabel);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(440, 355);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Свойства";
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.Comments);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(0, 620);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(422, 90);
			this.panel5.TabIndex = 153;
			// 
			// Comments
			// 
			this.Comments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Comments.Dock = System.Windows.Forms.DockStyle.Top;
			this.Comments.Location = new System.Drawing.Point(0, 0);
			this.Comments.Multiline = true;
			this.Comments.Name = "Comments";
			this.Comments.Size = new System.Drawing.Size(422, 72);
			this.Comments.TabIndex = 17;
			this.Comments.Text = "textBox11";
			// 
			// label27
			// 
			this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label27.Dock = System.Windows.Forms.DockStyle.Top;
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label27.ForeColor = System.Drawing.SystemColors.Info;
			this.label27.Location = new System.Drawing.Point(0, 600);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(422, 20);
			this.label27.TabIndex = 152;
			this.label27.Text = "Дополнительные сведения";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.Courses);
			this.panel4.Controls.Add(this.Education);
			this.panel4.Controls.Add(this.Certificates);
			this.panel4.Controls.Add(this.label14);
			this.panel4.Controls.Add(this.label13);
			this.panel4.Controls.Add(this.label12);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 392);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(422, 208);
			this.panel4.TabIndex = 151;
			// 
			// Courses
			// 
			this.Courses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Courses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Courses.Location = new System.Drawing.Point(120, 56);
			this.Courses.Multiline = true;
			this.Courses.Name = "Courses";
			this.Courses.Size = new System.Drawing.Size(292, 72);
			this.Courses.TabIndex = 15;
			this.Courses.Text = "textBox16";
			// 
			// Education
			// 
			this.Education.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Education.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Education.Location = new System.Drawing.Point(120, 4);
			this.Education.Multiline = true;
			this.Education.Name = "Education";
			this.Education.Size = new System.Drawing.Size(292, 48);
			this.Education.TabIndex = 14;
			this.Education.Text = "textBox13";
			// 
			// Certificates
			// 
			this.Certificates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Certificates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Certificates.Location = new System.Drawing.Point(120, 132);
			this.Certificates.Multiline = true;
			this.Certificates.Name = "Certificates";
			this.Certificates.Size = new System.Drawing.Size(292, 72);
			this.Certificates.TabIndex = 16;
			this.Certificates.Text = "textBox12";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(4, 160);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(116, 16);
			this.label14.TabIndex = 92;
			this.label14.Text = "Сертификаты";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(4, 84);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(116, 16);
			this.label13.TabIndex = 91;
			this.label13.Text = "Курсы, тренинги";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(4, 8);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(112, 40);
			this.label12.TabIndex = 90;
			this.label12.Text = "ВУЗ, специальность";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label17
			// 
			this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label17.Dock = System.Windows.Forms.DockStyle.Top;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label17.ForeColor = System.Drawing.SystemColors.Info;
			this.label17.Location = new System.Drawing.Point(0, 372);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(422, 20);
			this.label17.TabIndex = 150;
			this.label17.Text = "Образование";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.Email);
			this.panel3.Controls.Add(this.Phone);
			this.panel3.Controls.Add(this.Address);
			this.panel3.Controls.Add(this.label11);
			this.panel3.Controls.Add(this.label10);
			this.panel3.Controls.Add(this.label9);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 268);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(422, 104);
			this.panel3.TabIndex = 149;
			// 
			// Email
			// 
			this.Email.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Email.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Email.Location = new System.Drawing.Point(120, 80);
			this.Email.Name = "Email";
			this.Email.Size = new System.Drawing.Size(292, 20);
			this.Email.TabIndex = 13;
			this.Email.Text = "12";
			// 
			// Phone
			// 
			this.Phone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Phone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Phone.Location = new System.Drawing.Point(120, 56);
			this.Phone.Name = "Phone";
			this.Phone.Size = new System.Drawing.Size(292, 20);
			this.Phone.TabIndex = 12;
			this.Phone.Text = "11";
			// 
			// Address
			// 
			this.Address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Address.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Address.Location = new System.Drawing.Point(120, 4);
			this.Address.Multiline = true;
			this.Address.Name = "Address";
			this.Address.Size = new System.Drawing.Size(292, 48);
			this.Address.TabIndex = 11;
			this.Address.Text = "10";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(4, 82);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(108, 16);
			this.label11.TabIndex = 148;
			this.label11.Text = "Email";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(4, 58);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(108, 16);
			this.label10.TabIndex = 147;
			this.label10.Text = "Телефон(ы)";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(4, 20);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(108, 16);
			this.label9.TabIndex = 146;
			this.label9.Text = "Адрес";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label28
			// 
			this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label28.Dock = System.Windows.Forms.DockStyle.Top;
			this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label28.ForeColor = System.Drawing.SystemColors.Info;
			this.label28.Location = new System.Drawing.Point(0, 248);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(422, 20);
			this.label28.TabIndex = 148;
			this.label28.Text = "Контактные данные";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.JobPosition);
			this.panel2.Controls.Add(this.Birthday);
			this.panel2.Controls.Add(this.Sex);
			this.panel2.Controls.Add(this.pictureBox1);
			this.panel2.Controls.Add(this.ChangePwd);
			this.panel2.Controls.Add(this.Login);
			this.panel2.Controls.Add(this.LastNameEng);
			this.panel2.Controls.Add(this.FirstNameEng);
			this.panel2.Controls.Add(this.Patronymic);
			this.panel2.Controls.Add(this.LastName);
			this.panel2.Controls.Add(this.FirstName);
			this.panel2.Controls.Add(this.label18);
			this.panel2.Controls.Add(this.label16);
			this.panel2.Controls.Add(this.label8);
			this.panel2.Controls.Add(this.label7);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 20);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(422, 228);
			this.panel2.TabIndex = 147;
			// 
			// JobPosition
			// 
			this.JobPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.JobPosition.Location = new System.Drawing.Point(124, 204);
			this.JobPosition.Name = "JobPosition";
			this.JobPosition.Size = new System.Drawing.Size(184, 21);
			this.JobPosition.TabIndex = 10;
			// 
			// Birthday
			// 
			this.Birthday.Location = new System.Drawing.Point(124, 104);
			this.Birthday.Name = "Birthday";
			this.Birthday.Size = new System.Drawing.Size(152, 20);
			this.Birthday.TabIndex = 6;
			this.Birthday.ValueChanged += new System.EventHandler(this.Birthday_ValueChanged);
			// 
			// Sex
			// 
			this.Sex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Sex.Items.AddRange(new object[] {
            "Жен.",
            "Муж."});
			this.Sex.Location = new System.Drawing.Point(124, 128);
			this.Sex.Name = "Sex";
			this.Sex.Size = new System.Drawing.Size(96, 21);
			this.Sex.TabIndex = 7;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox1.Location = new System.Drawing.Point(316, 64);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(96, 128);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 166;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// ChangePwd
			// 
			this.ChangePwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ChangePwd.Location = new System.Drawing.Point(252, 4);
			this.ChangePwd.Name = "ChangePwd";
			this.ChangePwd.Size = new System.Drawing.Size(132, 24);
			this.ChangePwd.TabIndex = 2;
			this.ChangePwd.Text = "Сменить пароль";
			this.ChangePwd.Visible = false;
			this.ChangePwd.Click += new System.EventHandler(this.ChangePwd_Click);
			// 
			// Login
			// 
			this.Login.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Login.Enabled = false;
			this.Login.Location = new System.Drawing.Point(124, 8);
			this.Login.Name = "Login";
			this.Login.Size = new System.Drawing.Size(122, 20);
			this.Login.TabIndex = 1;
			this.Login.Text = "textBox15";
			// 
			// LastNameEng
			// 
			this.LastNameEng.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.LastNameEng.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.LastNameEng.Location = new System.Drawing.Point(124, 180);
			this.LastNameEng.Name = "LastNameEng";
			this.LastNameEng.Size = new System.Drawing.Size(184, 20);
			this.LastNameEng.TabIndex = 9;
			this.LastNameEng.Text = "8";
			// 
			// FirstNameEng
			// 
			this.FirstNameEng.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.FirstNameEng.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FirstNameEng.Location = new System.Drawing.Point(124, 156);
			this.FirstNameEng.Name = "FirstNameEng";
			this.FirstNameEng.Size = new System.Drawing.Size(184, 20);
			this.FirstNameEng.TabIndex = 8;
			this.FirstNameEng.Text = "7";
			// 
			// Patronymic
			// 
			this.Patronymic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Patronymic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Patronymic.Location = new System.Drawing.Point(124, 80);
			this.Patronymic.Name = "Patronymic";
			this.Patronymic.Size = new System.Drawing.Size(184, 20);
			this.Patronymic.TabIndex = 5;
			this.Patronymic.Text = "4";
			// 
			// LastName
			// 
			this.LastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.LastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.LastName.Location = new System.Drawing.Point(124, 32);
			this.LastName.Name = "LastName";
			this.LastName.Size = new System.Drawing.Size(184, 20);
			this.LastName.TabIndex = 3;
			this.LastName.Text = "2";
			// 
			// FirstName
			// 
			this.FirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.FirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FirstName.Location = new System.Drawing.Point(124, 56);
			this.FirstName.Name = "FirstName";
			this.FirstName.Size = new System.Drawing.Size(184, 20);
			this.FirstName.TabIndex = 4;
			this.FirstName.Text = "3";
			// 
			// label18
			// 
			this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label18.Location = new System.Drawing.Point(324, 44);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(83, 16);
			this.label18.TabIndex = 165;
			this.label18.Text = "Фото";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(4, 10);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(116, 16);
			this.label16.TabIndex = 164;
			this.label16.Text = "Логин";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(4, 206);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(116, 16);
			this.label8.TabIndex = 163;
			this.label8.Text = "Должность";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(4, 182);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(116, 16);
			this.label7.TabIndex = 162;
			this.label7.Text = "Last Name";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4, 158);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(116, 16);
			this.label6.TabIndex = 161;
			this.label6.Text = "First Name";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4, 130);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(116, 16);
			this.label5.TabIndex = 160;
			this.label5.Text = "Пол";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(4, 106);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(116, 16);
			this.label4.TabIndex = 159;
			this.label4.Text = "Дата рождения";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4, 82);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 16);
			this.label3.TabIndex = 158;
			this.label3.Text = "Отчество";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 16);
			this.label2.TabIndex = 157;
			this.label2.Text = "Фамилия";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 16);
			this.label1.TabIndex = 156;
			this.label1.Text = "Имя";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.TitleLabel.Size = new System.Drawing.Size(422, 20);
			this.TitleLabel.TabIndex = 146;
			this.TitleLabel.Text = "Основные";
			this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabPage2
			// 
			this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tabPage2.Controls.Add(this.panel6);
			this.tabPage2.Controls.Add(this.panel7);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(440, 355);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Права";
			// 
			// panel6
			// 
			this.panel6.AutoScroll = true;
			this.panel6.Controls.Add(this.RTests);
			this.panel6.Controls.Add(this.RRequests);
			this.panel6.Controls.Add(this.RShedule);
			this.panel6.Controls.Add(this.RTrainings);
			this.panel6.Controls.Add(this.RQuestionnaire);
			this.panel6.Controls.Add(this.label25);
			this.panel6.Controls.Add(this.label24);
			this.panel6.Controls.Add(this.label23);
			this.panel6.Controls.Add(this.label22);
			this.panel6.Controls.Add(this.label21);
			this.panel6.Controls.Add(this.RCourses);
			this.panel6.Controls.Add(this.RNews);
			this.panel6.Controls.Add(this.label26);
			this.panel6.Controls.Add(this.RStudents);
			this.panel6.Controls.Add(this.label15);
			this.panel6.Controls.Add(this.label20);
			this.panel6.Controls.Add(this.label19);
			this.panel6.Controls.Add(this.RUsers);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel6.Location = new System.Drawing.Point(0, 0);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(438, 313);
			this.panel6.TabIndex = 23;
			// 
			// RTests
			// 
			this.RTests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RTests.Items.AddRange(new object[] {
            "Нет",
            "Просмотр",
            "Просмотр и изменение"});
			this.RTests.Location = new System.Drawing.Point(256, 184);
			this.RTests.Name = "RTests";
			this.RTests.Size = new System.Drawing.Size(144, 21);
			this.RTests.TabIndex = 5;
			this.toolTip1.SetToolTip(this.RTests, resources.GetString("RTests.ToolTip"));
			// 
			// RRequests
			// 
			this.RRequests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RRequests.Items.AddRange(new object[] {
            "Нет",
            "Просмотр",
            "Просмотр и изменение"});
			this.RRequests.Location = new System.Drawing.Point(256, 120);
			this.RRequests.Name = "RRequests";
			this.RRequests.Size = new System.Drawing.Size(144, 21);
			this.RRequests.TabIndex = 3;
			this.toolTip1.SetToolTip(this.RRequests, resources.GetString("RRequests.ToolTip"));
			// 
			// RShedule
			// 
			this.RShedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RShedule.Items.AddRange(new object[] {
            "Нет",
            "Просмотр",
            "Просмотр и изменение"});
			this.RShedule.Location = new System.Drawing.Point(256, 152);
			this.RShedule.Name = "RShedule";
			this.RShedule.Size = new System.Drawing.Size(144, 21);
			this.RShedule.TabIndex = 4;
			this.toolTip1.SetToolTip(this.RShedule, "Нет - пользователь не может просматривать расписание\r\nПросмотр - пользователь мож" +
					"ет просматривать расписание\r\nПросмотр и изменение - пользователь может редактиро" +
					"вать расписание.");
			// 
			// RTrainings
			// 
			this.RTrainings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RTrainings.Items.AddRange(new object[] {
            "Нет",
            "Просмотр",
            "Просмотр и изменение"});
			this.RTrainings.Location = new System.Drawing.Point(256, 88);
			this.RTrainings.Name = "RTrainings";
			this.RTrainings.Size = new System.Drawing.Size(144, 21);
			this.RTrainings.TabIndex = 2;
			this.toolTip1.SetToolTip(this.RTrainings, resources.GetString("RTrainings.ToolTip"));
			// 
			// RQuestionnaire
			// 
			this.RQuestionnaire.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RQuestionnaire.Items.AddRange(new object[] {
            "Нет",
            "Просмотр",
            "Просмотр и изменение"});
			this.RQuestionnaire.Location = new System.Drawing.Point(256, 216);
			this.RQuestionnaire.Name = "RQuestionnaire";
			this.RQuestionnaire.Size = new System.Drawing.Size(144, 21);
			this.RQuestionnaire.TabIndex = 6;
			this.toolTip1.SetToolTip(this.RQuestionnaire, resources.GetString("RQuestionnaire.ToolTip"));
			// 
			// label25
			// 
			this.label25.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label25.Location = new System.Drawing.Point(8, 184);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(232, 16);
			this.label25.TabIndex = 18;
			this.label25.Text = "Тесты, статистика";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label24
			// 
			this.label24.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label24.Location = new System.Drawing.Point(8, 120);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(232, 16);
			this.label24.TabIndex = 17;
			this.label24.Text = "Заявки на курс";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label23
			// 
			this.label23.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label23.Location = new System.Drawing.Point(8, 152);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(232, 16);
			this.label23.TabIndex = 16;
			this.label23.Text = "Расписание";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label22
			// 
			this.label22.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label22.Location = new System.Drawing.Point(8, 88);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(232, 16);
			this.label22.TabIndex = 15;
			this.label22.Text = "Тренинги";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label21
			// 
			this.label21.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label21.Location = new System.Drawing.Point(8, 216);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(232, 16);
			this.label21.TabIndex = 14;
			this.label21.Text = "Анкеты";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// RCourses
			// 
			this.RCourses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RCourses.Items.AddRange(new object[] {
            "Нет",
            "Просмотр",
            "Просмотр и изменение"});
			this.RCourses.Location = new System.Drawing.Point(256, 56);
			this.RCourses.Name = "RCourses";
			this.RCourses.Size = new System.Drawing.Size(144, 21);
			this.RCourses.TabIndex = 1;
			this.toolTip1.SetToolTip(this.RCourses, resources.GetString("RCourses.ToolTip"));
			// 
			// RNews
			// 
			this.RNews.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RNews.Items.AddRange(new object[] {
            "Нет",
            "Просмотр",
            "Просмотр и изменение"});
			this.RNews.Location = new System.Drawing.Point(256, 280);
			this.RNews.Name = "RNews";
			this.RNews.Size = new System.Drawing.Size(144, 21);
			this.RNews.TabIndex = 21;
			this.toolTip1.SetToolTip(this.RNews, resources.GetString("RNews.ToolTip"));
			// 
			// label26
			// 
			this.label26.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label26.Location = new System.Drawing.Point(8, 288);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(232, 16);
			this.label26.TabIndex = 22;
			this.label26.Text = "Новости";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// RStudents
			// 
			this.RStudents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RStudents.Items.AddRange(new object[] {
            "Нет",
            "Просмотр",
            "Просмотр и изменение"});
			this.RStudents.Location = new System.Drawing.Point(256, 248);
			this.RStudents.Name = "RStudents";
			this.RStudents.Size = new System.Drawing.Size(144, 21);
			this.RStudents.TabIndex = 19;
			this.toolTip1.SetToolTip(this.RStudents, resources.GetString("RStudents.ToolTip"));
			// 
			// label15
			// 
			this.label15.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label15.Location = new System.Drawing.Point(8, 256);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(232, 16);
			this.label15.TabIndex = 20;
			this.label15.Text = "Студенты";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label20
			// 
			this.label20.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label20.Location = new System.Drawing.Point(8, 56);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(232, 16);
			this.label20.TabIndex = 12;
			this.label20.Text = "Курсы";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label19
			// 
			this.label19.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label19.Location = new System.Drawing.Point(8, 8);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(232, 46);
			this.label19.TabIndex = 11;
			this.label19.Text = "Редактирование пользователей и назначение прав";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// RUsers
			// 
			this.RUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RUsers.Items.AddRange(new object[] {
            "Нет",
            "Просмотр",
            "Просмотр и изменение"});
			this.RUsers.Location = new System.Drawing.Point(256, 16);
			this.RUsers.Name = "RUsers";
			this.RUsers.Size = new System.Drawing.Size(144, 21);
			this.RUsers.TabIndex = 0;
			this.toolTip1.SetToolTip(this.RUsers, resources.GetString("RUsers.ToolTip"));
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.stdRightsBtn);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel7.Location = new System.Drawing.Point(0, 313);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(438, 40);
			this.panel7.TabIndex = 24;
			// 
			// stdRightsBtn
			// 
			this.stdRightsBtn.AccessibleDescription = "";
			this.stdRightsBtn.ContextMenu = this.contextMenu1;
			this.stdRightsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.stdRightsBtn.Location = new System.Drawing.Point(8, 8);
			this.stdRightsBtn.Name = "stdRightsBtn";
			this.stdRightsBtn.Size = new System.Drawing.Size(216, 24);
			this.stdRightsBtn.TabIndex = 7;
			this.stdRightsBtn.Text = "Стандартный набор прав...";
			this.toolTip1.SetToolTip(this.stdRightsBtn, "Задать предопределенный набор прав");
			this.stdRightsBtn.Click += new System.EventHandler(this.button1_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem5,
            this.menuItem2,
            this.menuItem3,
            this.menuItem4});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Администратор";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.Text = "-";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.Text = "Методист";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Text = "Куратор";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 4;
			this.menuItem4.Text = "Инструктор";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 5000;
			this.toolTip1.AutoPopDelay = 60000;
			this.toolTip1.InitialDelay = 500;
			this.toolTip1.ReshowDelay = 100;
			this.toolTip1.ShowAlways = true;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "Файлы изображений|*.jpg; *.gif; *.jpeg";
			// 
			// UserEdit
			// 
			this.AutoScroll = true;
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.panel1);
			this.Name = "UserEdit";
			this.Size = new System.Drawing.Size(448, 424);
			this.panel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.panel7.ResumeLayout(false);
			this.ResumeLayout(false);

      }
		#endregion

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

      private void ChangePwd_Click(object sender, System.EventArgs e)
      {
         
         string pwd = "";
         if (EditPassword.Edit(ref pwd, 20))
         {
            this.Node.EditRow["Password"] = pwd;
         }
      }

      private void button1_Click(object sender, System.EventArgs e)
      {
         this.contextMenu1.Show(this.stdRightsBtn,new Point(0,this.stdRightsBtn.Height));

      }

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         BindingContext[rights].SuspendBinding();
         this.rights.Rights = "WWWWWWWWW";
         BindingContext[rights].ResumeBinding();
      }

      private void menuItem2_Click(object sender, System.EventArgs e)
      {
         BindingContext[rights].SuspendBinding();
         this.rights.Rights = "RWWWWWWWR";
         BindingContext[rights].ResumeBinding();
      }

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         BindingContext[rights].SuspendBinding();
         this.rights.Rights = "RRRRWWRRR";
         BindingContext[rights].ResumeBinding();
      }

      private void menuItem4_Click(object sender, System.EventArgs e)
      {
         BindingContext[rights].SuspendBinding();
         this.rights.Rights = "RRRRRRRRR";
         BindingContext[rights].ResumeBinding();
      }

      private void OkButton_Click(object sender, System.EventArgs e)
      {
         /// Проверка полей.
         /// 
         string msg ="";
         if (this.FirstName.Text.Length==0 || this.LastName.Text.Length==0 || this.Patronymic.Text.Length==0)
            msg = "Имя, фамилия, отчество не могут быть пустыми.\n";
         if (!Settings.validateEmail(this.Email.Text))
            msg += "Неправильно указан Email.\n";

         if (this.Birthday.Value > DateTime.Now || this.Birthday.Value < new DateTime(DateTime.Now.Year-100,1,1))
            msg += "Неправильно указана дата рождения.\n";
         if (this.Login.Text.Length==0 || this.Login.Text.IndexOf(" ")>=0)
            msg += "Неправильно указан логин.\n";

         if (msg!="")
         {
            MessageBox.Show(msg,"Ошибка");
            return;
         }

         if (this.Node.EditRow["id"].ToString() == DCEUser.CurrentUser.id )
         {
            // is changes made?
            if (this.Node.EditRow.Row.HasVersion(DataRowVersion.Proposed))
               if (this.Node.EditRow.Row["Rights", DataRowVersion.Original].ToString()
                  != this.Node.EditRow.Row["Rights", DataRowVersion.Proposed].ToString()
                  )
               {
                  if (MessageBox.Show(@"Вы поменяли права доступа к системе под своим именем.
Если вы продолжите, все текущие ветви системы будут закрыты и перечитаны с учетом ваших новых прав.
ВСЕ несохраненне данные будут ПОТЕРЯНЫ.
Вы хотите продолжить?","Внимание!",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                  {
                     this.Node.EndEdit(false,false);
                     DCEUser.CurrentUser.FillAccessRights(this.Node.EditRow["Rights"].ToString());
                     RootNode.Root.RecreateNodes();
                  }
                  
                  return;
               }
         }
         this.Node.EndEdit(false,false);
      }

      private void CancelButton_Click(object sender, System.EventArgs e)
      {
         this.Node.EndEdit(true,false);
      }

      private void pictureBox1_Click(object sender, System.EventArgs e)
      {
         if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
         {
            System.IO.Stream pic = this.openFileDialog1.OpenFile();
            try
            {
               if (pic.Length > 0)
               {
                  byte[] Array = new byte[pic.Length];
                  pic.Read(Array,0,(int)pic.Length);
                  this.photoTable.Rows[0]["Data"] = Array;
                  this.pictureBox1.Image = new Bitmap(new System.IO.MemoryStream(Array));
               }
            }
            finally
            {
               pic.Close();
            }
         }
      }

      private void Birthday_ValueChanged(object sender, System.EventArgs e)
      {
      
      }

	}

   public class UserEditNode : RecordEditNode 
   {
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new UserEdit(this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         if (EditRow == null)
            return "";
         if (IsNew)
            return "Пользователь: (Новый)";
         else
            return "Пользователь: " + EditRow["Login"].ToString();
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
         EditRow ["id"] = System.Guid.NewGuid().ToString();
         EditRow ["sex"] = 0;
         EditRow ["Rights"] = "--------------------------------------------------";
         EditRow ["Photo"] = System.Guid.NewGuid();
         EditRow ["Birthday"] = DateTime.Now;
      }

      public UserEditNode(NodeControl parent,string id)
         : base(parent, "select * from dbo.Users","Users", "id", id)
      {
      }
   }

   public class PersonalProfileNode : UserEditNode
   {
      public PersonalProfileNode (NodeControl parent)
         : base(parent,DCEUser.CurrentUser.id)
      {

      }

      public override String GetCaption()
      {
         return "Мой профиль";
      }

      public override bool CanClose()
      {
         return false;
      }
   }
}
