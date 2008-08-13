using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Net;

namespace DCEAccessLib
{
	/// <summary>
	/// Форма аутентификации пользователя
	/// </summary>
	public class AuthenticationForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Button button2;
      private System.Windows.Forms.TextBox LoginE;
	  private System.Windows.Forms.TextBox PwdE;
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
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(145, 78);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(119, 29);
			this.button1.TabIndex = 3;
			this.button1.Text = "Ok";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(14, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Имя";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 19);
			this.label2.TabIndex = 2;
			this.label2.Text = "Пароль";
			// 
			// LoginE
			// 
			this.LoginE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.LoginE.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DCEAccessLib.Properties.Settings.Default, "DCEAccessLibAuthenticationFormLoginText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.LoginE.Location = new System.Drawing.Point(108, 11);
			this.LoginE.Name = "LoginE";
			this.LoginE.Size = new System.Drawing.Size(280, 20);
			this.LoginE.TabIndex = 1;
			this.LoginE.Text = global::DCEAccessLib.Properties.Settings.Default.DCEAccessLibAuthenticationFormLoginText;
			// 
			// PwdE
			// 
			this.PwdE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PwdE.Location = new System.Drawing.Point(108, 43);
			this.PwdE.Name = "PwdE";
			this.PwdE.PasswordChar = '*';
			this.PwdE.Size = new System.Drawing.Size(280, 20);
			this.PwdE.TabIndex = 2;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(271, 78);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(119, 29);
			this.button2.TabIndex = 4;
			this.button2.Text = "Выход";
			// 
			// AuthenticationForm
			// 
			this.AcceptButton = this.button1;
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(399, 112);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.PwdE);
			this.Controls.Add(this.LoginE);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "AuthenticationForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Вход в систему";
			this.VisibleChanged += new System.EventHandler(this.AuthenticationForm_VisibleChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

      }
		#endregion

      private void button1_Click(object sender, System.EventArgs e)
      {
         if (AuthenticationForm.Authentification(this.LoginE.Text,this.PwdE.Text) )
            this.DialogResult=DialogResult.OK;
         else
            MessageBox.Show("Вход в систему невозможен. Проверьте правильность ввода имени и пароля.","Вход",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }

		static void TryAuthenticate(string login, string password)
		{
			SecurityServices.UserManager _userManager = new DCEAccessLib.SecurityServices.UserManager();
			
			if (_userManager.Authenticate(login, password)) {
				DCEUser.CurrentUser.id = _userManager.GetId(login).ToString();
				DCEUser.CurrentUser.Login = login;
				DCEUser.CurrentUser.FillAccessRights("RRRRRRRRR");
				DCEUser.CurrentUser.Authorized = true;
			}
		}
      /// <summary>
      /// Аутентификация пользователя и вычитываение прав пользователя в системе
      /// </summary>
      /// <param name="login"></param>
      /// <param name="password"></param>
      /// <returns></returns>
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
			
			login = login.Replace("'","").Replace("\"","");
			TryAuthenticate(login, password);
			return DCEUser.CurrentUser.Authorized;
		}
		
      private void AuthenticationForm_VisibleChanged(object sender, System.EventArgs e)
      {
         this.LoginE.Select();
      }

	}
}
