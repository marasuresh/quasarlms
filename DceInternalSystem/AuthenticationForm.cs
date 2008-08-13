using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DCEAccessLib;
using System.Threading;
using System.Data;

namespace DCEInternalSystem
{
	/// <summary>
	/// Summary description for AuthenticationForm.
	/// </summary>
	public class AuthenticationForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Button button2;
      private System.Windows.Forms.TextBox LoginE;
      private System.Windows.Forms.TextBox PwdE;
      private System.Windows.Forms.Button button3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AuthenticationForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
         this.button1 = new System.Windows.Forms.Button();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.LoginE = new System.Windows.Forms.TextBox();
         this.PwdE = new System.Windows.Forms.TextBox();
         this.button2 = new System.Windows.Forms.Button();
         this.button3 = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(124, 80);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(102, 25);
         this.button1.TabIndex = 0;
         this.button1.Text = "Ok";
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(12, 16);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(76, 16);
         this.label1.TabIndex = 1;
         this.label1.Text = "Имя";
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(12, 44);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(72, 16);
         this.label2.TabIndex = 2;
         this.label2.Text = "Пароль";
         // 
         // LoginE
         // 
         this.LoginE.Location = new System.Drawing.Point(92, 16);
         this.LoginE.Name = "LoginE";
         this.LoginE.Size = new System.Drawing.Size(240, 20);
         this.LoginE.TabIndex = 3;
         this.LoginE.Text = "";
         // 
         // PwdE
         // 
         this.PwdE.Location = new System.Drawing.Point(92, 40);
         this.PwdE.Name = "PwdE";
         this.PwdE.Size = new System.Drawing.Size(240, 20);
         this.PwdE.TabIndex = 4;
         this.PwdE.Text = "";
         // 
         // button2
         // 
         this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.button2.Location = new System.Drawing.Point(232, 80);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(102, 25);
         this.button2.TabIndex = 5;
         this.button2.Text = "Выход";
         // 
         // button3
         // 
         this.button3.Location = new System.Drawing.Point(8, 80);
         this.button3.Name = "button3";
         this.button3.Size = new System.Drawing.Size(102, 25);
         this.button3.TabIndex = 6;
         this.button3.Text = "Настройки";
         this.button3.Click += new System.EventHandler(this.button3_Click);
         // 
         // AuthenticationForm
         // 
         this.AcceptButton = this.button1;
         this.AutoScale = false;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(342, 111);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.button3,
                                                                      this.button2,
                                                                      this.PwdE,
                                                                      this.LoginE,
                                                                      this.label2,
                                                                      this.label1,
                                                                      this.button1});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Name = "AuthenticationForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Вход в систему";
         this.VisibleChanged += new System.EventHandler(this.AuthenticationForm_VisibleChanged);
         this.ResumeLayout(false);

      }
		#endregion

      private void button1_Click(object sender, System.EventArgs e)
      {
         if (AuthenticationForm.Authentification(this.LoginE.Text,this.PwdE.Text) )
            this.DialogResult=DialogResult.OK;
         else
            MessageBox.Show("Вход в систему невозможен. Проверьте правильность ввода имени и пароля.","Вход");
      }

      public static bool Authentification(string login, string password)
      {
         DCEUser.CurrentUser.Users = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Students = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Courses = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Questionnaire = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Requests = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Shedule = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Tests = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Trainings = DCEUser.Access.Modify;
         DCEUser.CurrentUser.News = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Authorized = false;

         try
         {
            string query = "select id, Login, Rights from dbo.Users where Login='"+login+"' and  ( Password ";
            if (password == "")
               query += "is NULL or Password = '' )";
            else
               query += "= '"+password+"')";


            DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(query,"u");
            DataTable t = ds.Tables["u"];
            if (t.Rows.Count>0)
            {
               DCEUser.CurrentUser.id = t.Rows[0][0].ToString();
               DCEUser.CurrentUser.Login =t.Rows[0][1].ToString();
               DCEUser.CurrentUser.FillAccessRights(t.Rows[0][2].ToString());
               DCEUser.CurrentUser.Authorized = true;
            }
         }
         catch 
         {
            MessageBox.Show("Ошибка при обращении к базе данных. Вход в систему невозможен.","Ошибка");
         }
         
         return DCEUser.CurrentUser.Authorized;

      }
      [STAThread]
      static void Main() 
      {
 //         Application.ThreadException += new ThreadExceptionEventHandler(DCEException.OnThreadException);
         AuthenticationForm f = new AuthenticationForm();

         if (f.ShowDialog() == DialogResult.OK)
         {
         //   AuthenticationForm.Authentification("", "");
            DCEAccessLib.Settings.LoadSettings();
            Application.Run(new MainForm());
         }
      }

      private void button3_Click(object sender, System.EventArgs e)
      {
         DCEInternalSystem.SettingsDialog sDialog = new DCEInternalSystem.SettingsDialog();
         sDialog.ShowDialog(this);
      }

      private void AuthenticationForm_VisibleChanged(object sender, System.EventArgs e)
      {
         this.LoginE.Select();
      }

	}
}
