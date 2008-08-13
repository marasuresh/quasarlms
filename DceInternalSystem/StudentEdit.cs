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
	/// Изменение свойств студента
	/// </summary>
	public class StudentEdit : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button OkButton;
      private System.Windows.Forms.Button CancelButton;
      private System.ComponentModel.IContainer components;

      private StudentEditNode Node;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.Panel panel6;
      private System.Windows.Forms.TextBox Comments;
      private System.Windows.Forms.Label label27;
      private System.Windows.Forms.Panel panel5;
      private System.Windows.Forms.TextBox Courses;
      private System.Windows.Forms.TextBox Education;
      private System.Windows.Forms.TextBox Certificates;
      private System.Windows.Forms.Label label14;
      private System.Windows.Forms.Label label13;
      private System.Windows.Forms.Label label12;
      private System.Windows.Forms.Label label11;
      private System.Windows.Forms.Panel panel4;
      private System.Windows.Forms.TextBox Fax;
      private System.Windows.Forms.Label label15;
      private System.Windows.Forms.TextBox ZIP;
      private System.Windows.Forms.Label label26;
      private System.Windows.Forms.TextBox City;
      private System.Windows.Forms.Label label25;
      private System.Windows.Forms.Label label24;
      private System.Windows.Forms.TextBox Phone;
      private System.Windows.Forms.TextBox Address;
      private System.Windows.Forms.Label label10;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.Label label28;
      private System.Windows.Forms.Panel panel3;
      private System.Windows.Forms.TextBox ChiefPhone;
      private System.Windows.Forms.Label label23;
      private System.Windows.Forms.TextBox ChiefPosition;
      private System.Windows.Forms.Label label22;
      private System.Windows.Forms.TextBox Chief;
      private System.Windows.Forms.Label label21;
      private System.Windows.Forms.Label label20;
      private System.Windows.Forms.Label label19;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label label17;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.PictureBox pictureBox1;
      private System.Windows.Forms.Label label18;
      private System.Windows.Forms.Button ChangePwd;
      private System.Windows.Forms.ComboBox Sex;
      private System.Windows.Forms.TextBox LastNameEng;
      private System.Windows.Forms.TextBox FirstNameEng;
      private System.Windows.Forms.TextBox Patronymic;
      private System.Windows.Forms.TextBox LastName;
      private System.Windows.Forms.TextBox FirstName;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label TitleLabel;
      private System.Windows.Forms.DateTimePicker Birthday;
      private System.Windows.Forms.ComboBox Organization;
      private System.Windows.Forms.ComboBox OrgType;
      private System.Windows.Forms.ComboBox Country;
      private System.Windows.Forms.TextBox Email;
      private System.Windows.Forms.Label label29;
      private System.Windows.Forms.ComboBox JobPosition;

      public StudentEdit(DataRowView editRow, StudentEditNode node)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         Country.Items.AddRange(ToolbarImages.Images.countries);

         Node = node;

         Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(RebindControls);
         RebindControls();
      }

      public int CountryBinding
      {
         get
         {
            string ua = "UA";
            int uaindex = 0;
            if (this.Node.EditRow != null)
            {
               string country = this.Node.EditRow["Country"].ToString();
               for (int i=0; i<ToolbarImages.Images.countries.Length;i++)
               {
                  if (ToolbarImages.Images.countries[i].Value == country)
                  {
                     return i;
                  }
                  if (ToolbarImages.Images.countries[i].Value == ua)
                  {
                     uaindex = i;
                  }
               }
            }
            return uaindex;
         }
         set
         {
            if (this.Node.EditRow != null)
            {
               this.Node.EditRow["Country"] = ToolbarImages.Images.countries[value].Value;
            }
         }
      }
      public void RebindControls()
      {
//         this.Login.DataBindings.Clear();
//         this.Login.DataBindings.Add("Text",this.Node.EditRow,"Login");
//         this.Login.MaxLength = 20;

         this.Email.DataBindings.Clear();
         this.Email.DataBindings.Add("Text",this.Node.EditRow,"Email");
         this.Email.MaxLength = 100;

         this.FirstName.DataBindings.Clear();
         this.FirstName.DataBindings.Add("Text",this.Node.EditRow,"FirstName");
         this.FirstName.MaxLength = 155;

         this.LastName.DataBindings.Clear();
         this.LastName.DataBindings.Add("Text",this.Node.EditRow,"LastName");
         this.LastName.MaxLength = 155;

         this.Patronymic.DataBindings.Clear();
         this.Patronymic.DataBindings.Add("Text",this.Node.EditRow,"Patronymic");
         this.Patronymic.MaxLength = 155;

         this.Birthday.DataBindings.Clear();
         if (Node.EditRow["Birthday"] == System.DBNull.Value )
         {
            Node.EditRow["Birthday"]= DateTime.Now;
         }
         else
         if ( ((DateTime)Node.EditRow["Birthday"]) > DateTime.Now)
            Node.EditRow["Birthday"]= DateTime.Now;

         this.Birthday.DataBindings.Add("Value",this.Node.EditRow,"Birthday");

         this.Sex.DataBindings.Clear();
         this.Sex.DataBindings.Add("SelectedIndex",this.Node.EditRow,"Sex");

         this.FirstNameEng.DataBindings.Clear();
         this.FirstNameEng.DataBindings.Add("Text",this.Node.EditRow,"FirstNameEng");
         this.FirstNameEng.MaxLength = 155;

         this.LastNameEng.DataBindings.Clear();
         this.LastNameEng.DataBindings.Add("Text",this.Node.EditRow,"LastNameEng");
         this.LastNameEng.MaxLength = 155;

         this.Organization.DataBindings.Clear();
         DataSet ds = DCEWebAccess.WebAccess.GetDataSet("select distinct Organization from Students order by Organization","t");
         this.Organization.Items.Clear();
         foreach (DataRow row in ds.Tables["t"].Rows)
         {
            this.Organization.Items.Add(row[0].ToString());
         }
         this.Organization.DataBindings.Add("Text",this.Node.EditRow,"Organization");
         this.Organization.MaxLength = 155;

         this.OrgType.DataBindings.Clear();
         ds = DCEWebAccess.WebAccess.GetDataSet("select distinct OrgType from Students order by OrgType","t");
         this.OrgType.Items.Clear();
         foreach (DataRow row in ds.Tables["t"].Rows)
         {
            this.OrgType.Items.Add(row[0].ToString());
         }
         this.OrgType.DataBindings.Add("Text",this.Node.EditRow,"OrgType");
         this.OrgType.MaxLength = 50;

         this.JobPosition.DataBindings.Clear();
         ds = DCEWebAccess.WebAccess.GetDataSet("select distinct JobPosition from Students order by JobPosition","t");
         this.JobPosition.Items.Clear();
         foreach (DataRow row in ds.Tables["t"].Rows)
         {
            this.JobPosition.Items.Add(row[0].ToString());
         }
         this.JobPosition.DataBindings.Add("Text",this.Node.EditRow,"JobPosition");
         this.JobPosition.MaxLength = 155;

         this.Chief.DataBindings.Clear();
         this.Chief.DataBindings.Add("Text",this.Node.EditRow,"Chief");
         this.Chief.MaxLength = 155;

         this.ChiefPosition.DataBindings.Clear();
         this.ChiefPosition.DataBindings.Add("Text",this.Node.EditRow,"ChiefPosition");
         this.ChiefPosition.MaxLength = 155;

         this.ChiefPhone.DataBindings.Clear();
         this.ChiefPhone.DataBindings.Add("Text",this.Node.EditRow,"ChiefPhone");
         this.ChiefPhone.MaxLength = 100;

         this.Country.DataBindings.Clear();
         this.Country.DataBindings.Add("SelectedIndex",this,"CountryBinding");

         this.City.DataBindings.Clear();
         this.City.DataBindings.Add("Text",this.Node.EditRow,"City");
         this.City.MaxLength = 155;

         this.ZIP.DataBindings.Clear();
         this.ZIP.DataBindings.Add("Text",this.Node.EditRow,"ZIP");
         this.ZIP.MaxLength = 50;

         this.Address.DataBindings.Clear();
         this.Address.DataBindings.Add("Text",this.Node.EditRow,"Address");
         this.Address.MaxLength = 255;

         this.Phone.DataBindings.Clear();
         this.Phone.DataBindings.Add("Text",this.Node.EditRow,"Phone");
         this.Phone.MaxLength = 100;

         this.Fax.DataBindings.Clear();
         this.Fax.DataBindings.Add("Text",this.Node.EditRow,"Fax");
         this.Fax.MaxLength = 100;

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

         bool rdonly = DCEUser.CurrentUser.Students == DCEUser.Access.Modify;
         this.Email.Enabled = rdonly;
         this.FirstName.Enabled = rdonly;
         this.LastName.Enabled = rdonly;
         this.Patronymic.Enabled = rdonly;
         this.Birthday.Enabled = rdonly;
         this.Sex.Enabled = rdonly;
         this.FirstNameEng.Enabled = rdonly;
         this.LastNameEng.Enabled = rdonly;
         this.Organization.Enabled = rdonly;
         this.OrgType.Enabled = rdonly;
         this.JobPosition.Enabled = rdonly;
         this.Chief.Enabled = rdonly;
         this.ChiefPosition.Enabled = rdonly;
         this.ChiefPhone.Enabled = rdonly;
         this.Country.Enabled = rdonly;
         this.City.Enabled = rdonly;
         this.ZIP.Enabled = rdonly;
         this.Address.Enabled = rdonly;
         this.Phone.Enabled = rdonly;
         this.Fax.Enabled = rdonly;
         this.Education.Enabled = rdonly;
         this.Courses.Enabled = rdonly;
         this.Certificates.Enabled = rdonly;
         this.Comments.Enabled = rdonly;
         this.OkButton.Enabled = rdonly;
         this.ChangePwd.Enabled = rdonly;


         DataSet photos = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            "select * from Content where eid='"+this.Node.EditRow["Photo"].ToString()
            +"'", "Photos");
         DataTable photoTable = photos.Tables["Photos"];
         System.Data.DataRow pRow = null;
         if (photoTable != null && photoTable.Rows.Count != 0)
            pRow = photoTable.Rows[0];

         if (pRow != null && pRow["Data"] != System.DBNull.Value)
            this.pictureBox1.Image = new Bitmap(
               new System.IO.MemoryStream((byte[]) pRow["Data"]));

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.dataView = new System.Data.DataView();
			this.dataSet = new System.Data.DataSet();
			this.panel6 = new System.Windows.Forms.Panel();
			this.Comments = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.Courses = new System.Windows.Forms.TextBox();
			this.Education = new System.Windows.Forms.TextBox();
			this.Certificates = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.Country = new System.Windows.Forms.ComboBox();
			this.Fax = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.ZIP = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.City = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.Phone = new System.Windows.Forms.TextBox();
			this.Address = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.JobPosition = new System.Windows.Forms.ComboBox();
			this.OrgType = new System.Windows.Forms.ComboBox();
			this.Organization = new System.Windows.Forms.ComboBox();
			this.ChiefPhone = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.ChiefPosition = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.Chief = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.Email = new System.Windows.Forms.TextBox();
			this.label29 = new System.Windows.Forms.Label();
			this.Birthday = new System.Windows.Forms.DateTimePicker();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label18 = new System.Windows.Forms.Label();
			this.ChangePwd = new System.Windows.Forms.Button();
			this.Sex = new System.Windows.Forms.ComboBox();
			this.LastNameEng = new System.Windows.Forms.TextBox();
			this.FirstNameEng = new System.Windows.Forms.TextBox();
			this.Patronymic = new System.Windows.Forms.TextBox();
			this.LastName = new System.Windows.Forms.TextBox();
			this.FirstName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
			this.panel6.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.OkButton);
			this.panel1.Controls.Add(this.CancelButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 376);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(568, 40);
			this.panel1.TabIndex = 191;
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.OkButton.Location = new System.Drawing.Point(332, 8);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(96, 24);
			this.OkButton.TabIndex = 27;
			this.OkButton.Text = "Ok";
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CancelButton.Location = new System.Drawing.Point(436, 8);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(96, 24);
			this.CancelButton.TabIndex = 28;
			this.CancelButton.Text = "Отменить";
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// dataSet
			// 
			this.dataSet.DataSetName = "NewDataSet";
			this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
			// 
			// panel6
			// 
			this.panel6.AutoScroll = true;
			this.panel6.Controls.Add(this.Comments);
			this.panel6.Controls.Add(this.label27);
			this.panel6.Controls.Add(this.panel5);
			this.panel6.Controls.Add(this.label11);
			this.panel6.Controls.Add(this.panel4);
			this.panel6.Controls.Add(this.label28);
			this.panel6.Controls.Add(this.panel3);
			this.panel6.Controls.Add(this.label17);
			this.panel6.Controls.Add(this.panel2);
			this.panel6.Controls.Add(this.TitleLabel);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel6.Location = new System.Drawing.Point(0, 0);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(568, 376);
			this.panel6.TabIndex = 151;
			// 
			// Comments
			// 
			this.Comments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Comments.Dock = System.Windows.Forms.DockStyle.Top;
			this.Comments.Location = new System.Drawing.Point(0, 908);
			this.Comments.Multiline = true;
			this.Comments.Name = "Comments";
			this.Comments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Comments.Size = new System.Drawing.Size(552, 84);
			this.Comments.TabIndex = 26;
			// 
			// label27
			// 
			this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label27.Dock = System.Windows.Forms.DockStyle.Top;
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label27.ForeColor = System.Drawing.SystemColors.Info;
			this.label27.Location = new System.Drawing.Point(0, 888);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(552, 20);
			this.label27.TabIndex = 146;
			this.label27.Text = "Дополнительные сведения";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.Courses);
			this.panel5.Controls.Add(this.Education);
			this.panel5.Controls.Add(this.Certificates);
			this.panel5.Controls.Add(this.label14);
			this.panel5.Controls.Add(this.label13);
			this.panel5.Controls.Add(this.label12);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(0, 684);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(552, 204);
			this.panel5.TabIndex = 144;
			// 
			// Courses
			// 
			this.Courses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Courses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Courses.Location = new System.Drawing.Point(128, 52);
			this.Courses.Multiline = true;
			this.Courses.Name = "Courses";
			this.Courses.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Courses.Size = new System.Drawing.Size(418, 72);
			this.Courses.TabIndex = 24;
			this.Courses.Text = "textBox16";
			// 
			// Education
			// 
			this.Education.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Education.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Education.Location = new System.Drawing.Point(128, 4);
			this.Education.Multiline = true;
			this.Education.Name = "Education";
			this.Education.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Education.Size = new System.Drawing.Size(418, 42);
			this.Education.TabIndex = 23;
			this.Education.Text = "textBox13";
			// 
			// Certificates
			// 
			this.Certificates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Certificates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Certificates.Location = new System.Drawing.Point(128, 128);
			this.Certificates.Multiline = true;
			this.Certificates.Name = "Certificates";
			this.Certificates.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Certificates.Size = new System.Drawing.Size(418, 72);
			this.Certificates.TabIndex = 25;
			this.Certificates.Text = "textBox12";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(4, 156);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(116, 16);
			this.label14.TabIndex = 92;
			this.label14.Text = "Сертификаты";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(4, 68);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(116, 42);
			this.label13.TabIndex = 91;
			this.label13.Text = "Курсы, тренинги";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(4, 8);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(124, 34);
			this.label12.TabIndex = 90;
			this.label12.Text = "ВУЗ, специальность";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label11
			// 
			this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label11.Dock = System.Windows.Forms.DockStyle.Top;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label11.ForeColor = System.Drawing.SystemColors.Info;
			this.label11.Location = new System.Drawing.Point(0, 664);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(552, 20);
			this.label11.TabIndex = 143;
			this.label11.Text = "Образование";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.Country);
			this.panel4.Controls.Add(this.Fax);
			this.panel4.Controls.Add(this.label15);
			this.panel4.Controls.Add(this.ZIP);
			this.panel4.Controls.Add(this.label26);
			this.panel4.Controls.Add(this.City);
			this.panel4.Controls.Add(this.label25);
			this.panel4.Controls.Add(this.label24);
			this.panel4.Controls.Add(this.Phone);
			this.panel4.Controls.Add(this.Address);
			this.panel4.Controls.Add(this.label10);
			this.panel4.Controls.Add(this.label9);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 480);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(552, 184);
			this.panel4.TabIndex = 142;
			// 
			// Country
			// 
			this.Country.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Country.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Country.Location = new System.Drawing.Point(128, 4);
			this.Country.Name = "Country";
			this.Country.Size = new System.Drawing.Size(416, 21);
			this.Country.TabIndex = 16;
			// 
			// Fax
			// 
			this.Fax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Fax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Fax.Location = new System.Drawing.Point(128, 152);
			this.Fax.Name = "Fax";
			this.Fax.Size = new System.Drawing.Size(418, 20);
			this.Fax.TabIndex = 21;
			this.Fax.Text = "textBox9";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(4, 156);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(124, 16);
			this.label15.TabIndex = 143;
			this.label15.Text = "Факс";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ZIP
			// 
			this.ZIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ZIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ZIP.Location = new System.Drawing.Point(128, 52);
			this.ZIP.Name = "ZIP";
			this.ZIP.Size = new System.Drawing.Size(418, 20);
			this.ZIP.TabIndex = 18;
			this.ZIP.Text = "textBox10";
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(4, 48);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(124, 28);
			this.label26.TabIndex = 141;
			this.label26.Text = "Почтовый индекс";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// City
			// 
			this.City.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.City.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.City.Location = new System.Drawing.Point(128, 28);
			this.City.Name = "City";
			this.City.Size = new System.Drawing.Size(418, 20);
			this.City.TabIndex = 17;
			this.City.Text = "textBox10";
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(4, 30);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(124, 16);
			this.label25.TabIndex = 139;
			this.label25.Text = "Город";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(4, 6);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(124, 16);
			this.label24.TabIndex = 137;
			this.label24.Text = "Страна";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Phone
			// 
			this.Phone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Phone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Phone.Location = new System.Drawing.Point(128, 128);
			this.Phone.Name = "Phone";
			this.Phone.Size = new System.Drawing.Size(418, 20);
			this.Phone.TabIndex = 20;
			this.Phone.Text = "textBox9";
			// 
			// Address
			// 
			this.Address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Address.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Address.Location = new System.Drawing.Point(128, 76);
			this.Address.Multiline = true;
			this.Address.Name = "Address";
			this.Address.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Address.Size = new System.Drawing.Size(418, 48);
			this.Address.TabIndex = 19;
			this.Address.Text = "textBox8";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(4, 130);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(124, 16);
			this.label10.TabIndex = 135;
			this.label10.Text = "Телефон(ы)";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(4, 92);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(124, 16);
			this.label9.TabIndex = 134;
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
			this.label28.Location = new System.Drawing.Point(0, 460);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(552, 20);
			this.label28.TabIndex = 141;
			this.label28.Text = "Контактные данные";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.JobPosition);
			this.panel3.Controls.Add(this.OrgType);
			this.panel3.Controls.Add(this.Organization);
			this.panel3.Controls.Add(this.ChiefPhone);
			this.panel3.Controls.Add(this.label23);
			this.panel3.Controls.Add(this.ChiefPosition);
			this.panel3.Controls.Add(this.label22);
			this.panel3.Controls.Add(this.Chief);
			this.panel3.Controls.Add(this.label21);
			this.panel3.Controls.Add(this.label20);
			this.panel3.Controls.Add(this.label19);
			this.panel3.Controls.Add(this.label8);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 272);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(552, 188);
			this.panel3.TabIndex = 140;
			// 
			// JobPosition
			// 
			this.JobPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.JobPosition.Location = new System.Drawing.Point(128, 64);
			this.JobPosition.Name = "JobPosition";
			this.JobPosition.Size = new System.Drawing.Size(416, 21);
			this.JobPosition.TabIndex = 12;
			this.JobPosition.Text = "comboBox3";
			// 
			// OrgType
			// 
			this.OrgType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.OrgType.Location = new System.Drawing.Point(128, 36);
			this.OrgType.Name = "OrgType";
			this.OrgType.Size = new System.Drawing.Size(416, 21);
			this.OrgType.TabIndex = 11;
			this.OrgType.Text = "comboBox2";
			// 
			// Organization
			// 
			this.Organization.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Organization.Location = new System.Drawing.Point(128, 8);
			this.Organization.Name = "Organization";
			this.Organization.Size = new System.Drawing.Size(416, 21);
			this.Organization.TabIndex = 10;
			this.Organization.Text = "comboBox1";
			this.Organization.SelectedIndexChanged += new System.EventHandler(this.Organization_SelectedIndexChanged);
			// 
			// ChiefPhone
			// 
			this.ChiefPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ChiefPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ChiefPhone.Location = new System.Drawing.Point(128, 156);
			this.ChiefPhone.Name = "ChiefPhone";
			this.ChiefPhone.Size = new System.Drawing.Size(418, 20);
			this.ChiefPhone.TabIndex = 15;
			this.ChiefPhone.Text = "textBox7";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(4, 152);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(116, 28);
			this.label23.TabIndex = 161;
			this.label23.Text = "Телефон руководителя";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ChiefPosition
			// 
			this.ChiefPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ChiefPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ChiefPosition.Location = new System.Drawing.Point(128, 124);
			this.ChiefPosition.Name = "ChiefPosition";
			this.ChiefPosition.Size = new System.Drawing.Size(418, 20);
			this.ChiefPosition.TabIndex = 14;
			this.ChiefPosition.Text = "textBox7";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(4, 120);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(116, 28);
			this.label22.TabIndex = 159;
			this.label22.Text = "Должность руководителя";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Chief
			// 
			this.Chief.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Chief.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Chief.Location = new System.Drawing.Point(128, 96);
			this.Chief.Name = "Chief";
			this.Chief.Size = new System.Drawing.Size(418, 20);
			this.Chief.TabIndex = 13;
			this.Chief.Text = "textBox7";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(4, 96);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(116, 16);
			this.label21.TabIndex = 157;
			this.label21.Text = "Руководитель";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(4, 32);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(116, 32);
			this.label20.TabIndex = 155;
			this.label20.Text = "Форма собственности";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(4, 10);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(116, 16);
			this.label19.TabIndex = 153;
			this.label19.Text = "Организация";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(4, 68);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(116, 16);
			this.label8.TabIndex = 151;
			this.label8.Text = "Должность";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label17
			// 
			this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label17.Dock = System.Windows.Forms.DockStyle.Top;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label17.ForeColor = System.Drawing.SystemColors.Info;
			this.label17.Location = new System.Drawing.Point(0, 252);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(552, 20);
			this.label17.TabIndex = 139;
			this.label17.Text = "Место работы";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.Email);
			this.panel2.Controls.Add(this.label29);
			this.panel2.Controls.Add(this.Birthday);
			this.panel2.Controls.Add(this.pictureBox1);
			this.panel2.Controls.Add(this.label18);
			this.panel2.Controls.Add(this.ChangePwd);
			this.panel2.Controls.Add(this.Sex);
			this.panel2.Controls.Add(this.LastNameEng);
			this.panel2.Controls.Add(this.FirstNameEng);
			this.panel2.Controls.Add(this.Patronymic);
			this.panel2.Controls.Add(this.LastName);
			this.panel2.Controls.Add(this.FirstName);
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
			this.panel2.Size = new System.Drawing.Size(552, 232);
			this.panel2.TabIndex = 138;
			// 
			// Email
			// 
			this.Email.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Email.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Email.Location = new System.Drawing.Point(128, 16);
			this.Email.Name = "Email";
			this.Email.Size = new System.Drawing.Size(266, 20);
			this.Email.TabIndex = 1;
			this.Email.Text = "textBox9";
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(8, 16);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(112, 16);
			this.label29.TabIndex = 147;
			this.label29.Text = "Учетная запись";
			this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Birthday
			// 
			this.Birthday.Location = new System.Drawing.Point(128, 120);
			this.Birthday.Name = "Birthday";
			this.Birthday.Size = new System.Drawing.Size(152, 20);
			this.Birthday.TabIndex = 6;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new System.Drawing.Point(450, 80);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(90, 120);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 134;
			this.pictureBox1.TabStop = false;
			// 
			// label18
			// 
			this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label18.Location = new System.Drawing.Point(458, 64);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(83, 16);
			this.label18.TabIndex = 133;
			this.label18.Text = "Фото";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ChangePwd
			// 
			this.ChangePwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ChangePwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ChangePwd.Location = new System.Drawing.Point(410, 16);
			this.ChangePwd.Name = "ChangePwd";
			this.ChangePwd.Size = new System.Drawing.Size(128, 24);
			this.ChangePwd.TabIndex = 2;
			this.ChangePwd.Text = "Сменить пароль";
			this.ChangePwd.Click += new System.EventHandler(this.ChangePwd_Click);
			// 
			// Sex
			// 
			this.Sex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Sex.Items.AddRange(new object[] {
            "Жен.",
            "Муж."});
			this.Sex.Location = new System.Drawing.Point(128, 144);
			this.Sex.Name = "Sex";
			this.Sex.Size = new System.Drawing.Size(96, 21);
			this.Sex.TabIndex = 7;
			// 
			// LastNameEng
			// 
			this.LastNameEng.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.LastNameEng.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.LastNameEng.Location = new System.Drawing.Point(128, 200);
			this.LastNameEng.Name = "LastNameEng";
			this.LastNameEng.Size = new System.Drawing.Size(306, 20);
			this.LastNameEng.TabIndex = 9;
			this.LastNameEng.Text = "textBox6";
			// 
			// FirstNameEng
			// 
			this.FirstNameEng.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.FirstNameEng.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FirstNameEng.Location = new System.Drawing.Point(128, 176);
			this.FirstNameEng.Name = "FirstNameEng";
			this.FirstNameEng.Size = new System.Drawing.Size(306, 20);
			this.FirstNameEng.TabIndex = 8;
			this.FirstNameEng.Text = "textBox5";
			// 
			// Patronymic
			// 
			this.Patronymic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Patronymic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Patronymic.Location = new System.Drawing.Point(128, 96);
			this.Patronymic.Name = "Patronymic";
			this.Patronymic.Size = new System.Drawing.Size(306, 20);
			this.Patronymic.TabIndex = 5;
			this.Patronymic.Text = "textBox3";
			// 
			// LastName
			// 
			this.LastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.LastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.LastName.Location = new System.Drawing.Point(128, 44);
			this.LastName.Name = "LastName";
			this.LastName.Size = new System.Drawing.Size(306, 20);
			this.LastName.TabIndex = 3;
			this.LastName.Text = "textBox2";
			// 
			// FirstName
			// 
			this.FirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.FirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FirstName.Location = new System.Drawing.Point(128, 72);
			this.FirstName.Name = "FirstName";
			this.FirstName.Size = new System.Drawing.Size(306, 20);
			this.FirstName.TabIndex = 4;
			this.FirstName.Text = "textBox1";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(4, 208);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(116, 16);
			this.label7.TabIndex = 129;
			this.label7.Text = "Фамилия (англ.)";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4, 184);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(116, 16);
			this.label6.TabIndex = 128;
			this.label6.Text = "Имя (англ.)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(116, 16);
			this.label5.TabIndex = 127;
			this.label5.Text = "Пол";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(4, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(116, 16);
			this.label4.TabIndex = 126;
			this.label4.Text = "Дата рождения";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 16);
			this.label3.TabIndex = 125;
			this.label3.Text = "Отчество";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 16);
			this.label2.TabIndex = 124;
			this.label2.Text = "Фамилия";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 74);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 16);
			this.label1.TabIndex = 123;
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
			this.TitleLabel.Size = new System.Drawing.Size(552, 20);
			this.TitleLabel.TabIndex = 137;
			this.TitleLabel.Text = "Основные";
			this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// StudentEdit
			// 
			this.Controls.Add(this.panel6);
			this.Controls.Add(this.panel1);
			this.Name = "StudentEdit";
			this.Size = new System.Drawing.Size(568, 416);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

      }
		#endregion

      private void OkButton_Click(object sender, System.EventArgs e)
      {
         /// Проверка полей.
         /// 
         string msg ="";
         if (this.FirstName.Text.Length==0 || this.LastName.Text.Length==0 || this.Patronymic.Text.Length==0)
            msg = "Имя, фамилия, отчество не могут быть пустыми.\n";
		if(string.IsNullOrEmpty(this.Email.Text)) {
			msg += "Неправильно указана учетная запись.\n";
		}

         if (this.Birthday.Value > DateTime.Now || this.Birthday.Value < new DateTime(DateTime.Now.Year-100,1,1))
            msg += "Неправильно указана дата рождения.\n";
//         if (this.Login.Text.Length==0 || this.Login.Text.IndexOf(" ")>=0)
//            msg += "Неправильно указан логин.\n";

         if (msg!="")
         {
            MessageBox.Show(msg,"Ошибка");
            return;
         }
         this.Node.EditRow["Login"]=this.Node.EditRow["EMail"].ToString();

         this.Node.EndEdit(false,false);
      }

      private void CancelButton_Click(object sender, System.EventArgs e)
      {
         this.Node.EndEdit(true,false);
      }

      private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
      {
      
      }

      private void ChangePwd_Click(object sender, System.EventArgs e)
      {
         string pwd = "";
         if (EditPassword.Edit(ref pwd, 20))
         {
            this.Node.EditRow["Password"] = pwd;
         }
      }

      private void Organization_SelectedIndexChanged(object sender, System.EventArgs e)
      {
      
      }
	}

   public class StudentEditNode : RecordEditNode 
   {
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new StudentEdit(EditRow, this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         if (EditRow!=null)
         {
            if (IsNew)
               return "Студент: (Новый)";
            else
               return "Студент: " + EditRow["Login"].ToString();
         }
         return "";
      }

      public override void ReleaseControl()
      {
         // do nothing
      }
      public override bool CanClose()
      {
         return true;
      }
      public override bool HaveChildNodes()
      {
         return true;
      }

      public override void CreateChilds()
      {
         if (!this.IsNew)
         {
            new StudentTestsNode(this,this.Id);   
            new StudentPracticeNode(this,this.Id);
            new StudentQuestionnaireNode(this, this.Id);
            new StudentTasksNode(this,this.Id);
            new StudentTrainingsNode(this,this.Id);
         }
      }

      /// <summary>
      /// Initialize new record for editing (EditRow)
      /// </summary>
      protected override void InitNewRecord()
      {
         EditRow ["id"] = System.Guid.NewGuid().ToString();
         EditRow ["Sex"] = 0;
         EditRow ["Photo"] = System.Guid.NewGuid();
      }

      public StudentEditNode(NodeControl parent,string id)
         : base(parent, "select * from dbo.Students","Students", new Guid(id))
      {

      }
   }

}
