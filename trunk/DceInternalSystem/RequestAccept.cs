using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;
using DCEAccessLib;
using System.Data;
using System.Xml;
using System.Net.Mail;

namespace DCEInternalSystem
{
	/// <summary>
	/// Удовлетворение заявки
	/// </summary>
	public class RequestAccept : Form
	{
      private Panel panel2;
      private Label label2;
      private Label label3;
      private Label label4;
      private Panel panel3;
      private Panel panel1;
      private Button button3;
      private Label label1;
      private Label TitleLabel;
		private WebBrowser WebBrowser;
      private TextBox server;
      private TextBox from;
      private TextBox to;
      private Label label6;
      private TextBox StudentName;
      private Button btnCancel;
      private Button btnOk;
      private Button btnSend;
      private System.Data.DataSet dataSet;
      private ComboBox templatelist;
      private Button btnChangeTemplate;
      private Button btnNewTemplate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

      DataRow SRow;
      string CourseName;
      string StartDate;
		public RequestAccept(string studentid, string courseName, string startDate)
		{
			//
			// Required for Windows Form Designer support
			//
         this.CourseName = courseName;
         this.StartDate = startDate;

			InitializeComponent();

         DataSet ds = DCEAccessLib.DCEWebAccess.GetdataSet("select * from Students where id='"+studentid+"'","s");
         if (ds.Tables["s"].Rows.Count>0)
         {
            SRow = ds.Tables["s"].Rows[0];
            this.StudentName.Text = SRow["LastName"].ToString()+" "+SRow["FirstName"].ToString()+" "+SRow["Patronymic"].ToString();
            this.to.Text = SRow["Email"].ToString();
         }
         else
            throw new DCEException("Студент с указанным id не найден",DCEException.ExceptionLevel.ErrorContinue);

         this.ReloadTemplateList();
		}

      public class ETemplate
      {
         public DataRow row;

         public ETemplate()
         {
         }
         public ETemplate(DataRow r)
         {
            row = r;
         }
         public override string ToString()
         {
            return row["DataStr"].ToString();
         }
      }

      public void ReloadTemplateList()
      {
         string id = "";
         if (templatelist.SelectedItem != null)
         {
            id = ((ETemplate)templatelist.SelectedItem).row["id"].ToString();
         }
         string query = "select c.id, c.DataStr, c.TData, c.eid from Content c where c.Type="+((int)ContentType._emailTemplate).ToString();
         this.dataSet = DCEWebAccess.GetdataSet(query,"t");
         this.templatelist.Items.Clear();
         foreach (DataRow row in dataSet.Tables["t"].Rows)
         {
            ETemplate t = new ETemplate(row);
            this.templatelist.Items.Add(t);
            if (row["id"].ToString() == id)
               this.templatelist.SelectedItem = t;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel2 = new System.Windows.Forms.Panel();
			this.StudentName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.to = new System.Windows.Forms.TextBox();
			this.from = new System.Windows.Forms.TextBox();
			this.server = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnSend = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button3 = new System.Windows.Forms.Button();
			this.btnChangeTemplate = new System.Windows.Forms.Button();
			this.btnNewTemplate = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.templatelist = new System.Windows.Forms.ComboBox();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.WebBrowser = new System.Windows.Forms.WebBrowser();
			this.dataSet = new System.Data.DataSet();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.StudentName);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.to);
			this.panel2.Controls.Add(this.from);
			this.panel2.Controls.Add(this.server);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(652, 108);
			this.panel2.TabIndex = 47;
			// 
			// StudentName
			// 
			this.StudentName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.StudentName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.StudentName.Location = new System.Drawing.Point(172, 4);
			this.StudentName.Name = "StudentName";
			this.StudentName.ReadOnly = true;
			this.StudentName.Size = new System.Drawing.Size(470, 20);
			this.StudentName.TabIndex = 9;
			this.StudentName.TabStop = false;
			this.StudentName.Text = "textBox5";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(12, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 8;
			this.label6.Text = "Студент";
			// 
			// to
			// 
			this.to.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.to.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.to.Location = new System.Drawing.Point(172, 52);
			this.to.Name = "to";
			this.to.Size = new System.Drawing.Size(470, 20);
			this.to.TabIndex = 2;
			this.to.Text = "textBox3";
			// 
			// from
			// 
			this.from.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.from.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.from.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DCEInternalSystem.Properties.Settings.Default, "SenderEmail", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.from.Location = new System.Drawing.Point(172, 28);
			this.from.Name = "from";
			this.from.Size = new System.Drawing.Size(470, 20);
			this.from.TabIndex = 1;
			this.from.Text = global::DCEInternalSystem.Properties.Settings.Default.SenderEmail;
			// 
			// server
			// 
			this.server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.server.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.server.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DCEInternalSystem.Properties.Settings.Default, "MailServer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.server.Location = new System.Drawing.Point(172, 76);
			this.server.Name = "server";
			this.server.Size = new System.Drawing.Size(470, 20);
			this.server.TabIndex = 3;
			this.server.Text = global::DCEInternalSystem.Properties.Settings.Default.MailServer;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 76);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(156, 20);
			this.label4.TabIndex = 2;
			this.label4.Text = "Адрес почтового сервера";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 1;
			this.label3.Text = "Кому";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "От кого";
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.btnCancel);
			this.panel3.Controls.Add(this.btnOk);
			this.panel3.Controls.Add(this.btnSend);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 345);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(652, 36);
			this.panel3.TabIndex = 48;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Location = new System.Drawing.Point(510, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(136, 28);
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "Отменить";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOk.Location = new System.Drawing.Point(370, 4);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(136, 28);
			this.btnOk.TabIndex = 10;
			this.btnOk.Text = "Удовлетворить";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnSend
			// 
			this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSend.Location = new System.Drawing.Point(226, 4);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(140, 28);
			this.btnSend.TabIndex = 9;
			this.btnSend.Text = "Отправить письмо";
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.btnChangeTemplate);
			this.panel1.Controls.Add(this.btnNewTemplate);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.templatelist);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 108);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(652, 72);
			this.panel1.TabIndex = 49;
			// 
			// button3
			// 
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Location = new System.Drawing.Point(296, 36);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(136, 28);
			this.button3.TabIndex = 7;
			this.button3.Text = "Удалить";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// btnChangeTemplate
			// 
			this.btnChangeTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnChangeTemplate.Location = new System.Drawing.Point(156, 36);
			this.btnChangeTemplate.Name = "btnChangeTemplate";
			this.btnChangeTemplate.Size = new System.Drawing.Size(136, 28);
			this.btnChangeTemplate.TabIndex = 6;
			this.btnChangeTemplate.Text = "Изменить";
			this.btnChangeTemplate.Click += new System.EventHandler(this.btnChangeTemplate_Click);
			// 
			// btnNewTemplate
			// 
			this.btnNewTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNewTemplate.Location = new System.Drawing.Point(12, 36);
			this.btnNewTemplate.Name = "btnNewTemplate";
			this.btnNewTemplate.Size = new System.Drawing.Size(140, 28);
			this.btnNewTemplate.TabIndex = 5;
			this.btnNewTemplate.Text = "Новый";
			this.btnNewTemplate.Click += new System.EventHandler(this.btnNewTemplate_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 16);
			this.label1.TabIndex = 46;
			this.label1.Text = "Шаблон письма";
			// 
			// templatelist
			// 
			this.templatelist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.templatelist.Location = new System.Drawing.Point(164, 8);
			this.templatelist.Name = "templatelist";
			this.templatelist.Size = new System.Drawing.Size(236, 21);
			this.templatelist.TabIndex = 4;
			this.templatelist.SelectedIndexChanged += new System.EventHandler(this.templatelist_SelectedIndexChanged);
			// 
			// TitleLabel
			// 
			this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
			this.TitleLabel.Location = new System.Drawing.Point(0, 180);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(652, 20);
			this.TitleLabel.TabIndex = 147;
			this.TitleLabel.Text = "Просмотр письма";
			this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// WebBrowser
			// 
			this.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WebBrowser.Location = new System.Drawing.Point(0, 200);
			this.WebBrowser.Name = "WebBrowser";
			this.WebBrowser.Size = new System.Drawing.Size(652, 145);
			this.WebBrowser.TabIndex = 8;
			this.WebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.ProcessTemplate);
			// 
			// dataSet
			// 
			this.dataSet.DataSetName = "NewDataSet";
			this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
			// 
			// RequestAccept
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(652, 381);
			this.Controls.Add(this.WebBrowser);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Name = "RequestAccept";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Удовлетворение заявки";
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
			this.ResumeLayout(false);

      }
		#endregion

      private void panel1_Paint(object sender, PaintEventArgs e)
      {
      
      }

      private void button3_Click(object sender, System.EventArgs e)
      {
         if (templatelist.SelectedItem != null)
         {
            if (MessageBox.Show("Удалить выбранный шаблон?","Удалить",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
               DataRow row = ((ETemplate)templatelist.SelectedItem).row;
               try
               {
                  DCEAccessLib.DCEWebAccess.execSQL("delete from entities where id='"+row["eid"].ToString()+"' "
                     + "delete from Content where eid='"+row["eid"].ToString()+"'");
               }
               finally
               {
                  this.ReloadTemplateList();
               }
            }
         }
      }

      private void btnNewTemplate_Click(object sender, System.EventArgs e)
      {
         string eid = System.Guid.NewGuid().ToString();
         string id = System.Guid.NewGuid().ToString();

         string name="";
         string body="";
         if (EditEmailTemplate.EditTemplate("",ref name,ref body))
         {
            try
            {
               DCEAccessLib.DCEWebAccess.execSQL(@"insert into Entities values('"+eid+@"',NULL,3)
                  insert into Content values('"+id+"','"+eid+"',"+((int)ContentType._emailTemplate).ToString()+", NULL,'"+name+"', NULL, '"+body+"',NULL,NULL)"
               );                  
            }
            finally
            {
               this.ReloadTemplateList();
            }
         }
      }

      private void btnChangeTemplate_Click(object sender, System.EventArgs e)
      {
         if (templatelist.SelectedItem != null)
         {
            DataRow row = ((ETemplate)templatelist.SelectedItem).row;
            string name = row["DataStr"].ToString();
            string body = row["TData"].ToString();
            if (EditEmailTemplate.EditTemplate(row["id"].ToString(),ref name,ref body))
            {
               try
               {
                  DCEAccessLib.DCEWebAccess.execSQL("update Content set DataStr='"+name+"', TData='"+body+"' where id='"+row["id"].ToString()+"'");
               }
               finally
               {
                  this.ReloadTemplateList();
               }
            }      
         }
      }

		private void ProcessTemplate(object sender, WebBrowserDocumentCompletedEventArgs e)
      {
         if (this.templatelist.SelectedItem !=null)
         {
            ///
            Object refmissing = System.Reflection.Missing.Value;
            HtmlDocument doc = WebBrowser.Document;
            /// generating xml
            /// 
            string xml=@"<?xml version=""1.0"" encoding=""windows-1251""?>
<Request>";
            xml+="<FirstName>"+SRow["FirstName"].ToString()+"</FirstName>";
            xml+="<LastName>"+SRow["LastName"].ToString()+"</LastName>";
            xml+="<Patronymic>"+SRow["Patronymic"].ToString()+"</Patronymic>";
            xml+="<FirstNameEng>"+SRow["FirstNameEng"].ToString()+"</FirstNameEng>";
            xml+="<LastNameEng>"+SRow["LastNameEng"].ToString()+"</LastNameEng>";
            xml+="<Login>"+SRow["Login"].ToString()+"</Login>";
            xml+="<Password>"+SRow["Password"].ToString()+"</Password>";
            xml+="<CourseName>"+this.CourseName+"</CourseName>";
            xml+="<StartDate>"+this.StartDate+"</StartDate>";
            xml+="</Request>";

            string xsl = ((ETemplate)this.templatelist.SelectedItem).row["TData"].ToString();
            xml = StatsTraining.XmlTransform(xml,xsl);

            doc.Write(xml);
         }
      }

      private void templatelist_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         Object refmissing = System.Reflection.Missing.Value;
         WebBrowser.Navigate("about:blank");
      }

      bool wassent = false;
      private void btnSend_Click(object sender, System.EventArgs e)
      {
         /// посылаем наше письмо
         /// 
         if (this.templatelist.SelectedItem ==null)
         {
            MessageBox.Show("Выберите шаблон письма.");
            return;
         }

         string xml=@"<?xml version=""1.0"" encoding=""windows-1251""?><Request>";
         xml+="<FirstName>"+SRow["FirstName"].ToString()+"</FirstName>";
         xml+="<LastName>"+SRow["LastName"].ToString()+"</LastName>";
         xml+="<Patronymic>"+SRow["Patronymic"].ToString()+"</Patronymic>";
         xml+="<FirstNameEng>"+SRow["FirstNameEng"].ToString()+"</FirstNameEng>";
         xml+="<LastNameEng>"+SRow["LastNameEng"].ToString()+"</LastNameEng>";
         xml+="<Login>"+SRow["Login"].ToString()+"</Login>";
         xml+="<Password>"+SRow["Password"].ToString()+"</Password>";
         xml+="<CourseName>"+this.CourseName+"</CourseName>";
         xml+="<StartDate>"+this.StartDate+"</StartDate>";
         xml+="</Request>";

         string xsl = ((ETemplate)this.templatelist.SelectedItem).row["TData"].ToString();
         xml = StatsTraining.XmlTransform(xml,xsl);

         MailMessage msg = new MailMessage();
         msg.IsBodyHtml = true;
         msg.From = new MailAddress(this.from.Text);
         msg.To.Add(new MailAddress(this.to.Text));
         msg.Subject = "Заявка на курс";
         msg.Body = xml;
		SmtpClient _smtpClient = new SmtpClient(server.Text);
		
		try {
			_smtpClient.Send(msg);
			global::DCEInternalSystem.Properties.Settings.Default.Save();
			MessageBox.Show("Отправка письма завершена успешно.");
			wassent = true;
		} catch (SmtpException) {
			MessageBox.Show("There was a problem sending email");
			wassent = false;
		}
         
      }

      private void btnOk_Click(object sender, System.EventArgs e)
      {
         if (!wassent)
         {
            if (MessageBox.Show("Письмо об удовлетворении заявки не было отправлено. Вы хотите удовлетворить заявку без отправки письма?","Внимание!",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
               this.DialogResult = DialogResult.OK;
               this.Close();
            }
            else
               return;
         }
         this.DialogResult = DialogResult.OK;
         this.Close();
      }
	}
}
