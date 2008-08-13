using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DCEAccessLib
{
	/// <summary>
	/// Форма WEB авторизации
	/// </summary>
	public class WebAuthorize : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Button buttonCancel;
      public System.Windows.Forms.TextBox textBoxLogin;
      public System.Windows.Forms.TextBox textBoxPassword;
      public System.Windows.Forms.TextBox textBoxDomain;
      private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WebAuthorize()
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
         this.textBoxLogin = new System.Windows.Forms.TextBox();
         this.textBoxPassword = new System.Windows.Forms.TextBox();
         this.buttonOk = new System.Windows.Forms.Button();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.textBoxDomain = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // textBoxLogin
         // 
         this.textBoxLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.textBoxLogin.Location = new System.Drawing.Point(120, 24);
         this.textBoxLogin.Name = "textBoxLogin";
         this.textBoxLogin.Size = new System.Drawing.Size(208, 22);
         this.textBoxLogin.TabIndex = 0;
         this.textBoxLogin.Text = "";
         // 
         // textBoxPassword
         // 
         this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.textBoxPassword.Location = new System.Drawing.Point(120, 56);
         this.textBoxPassword.Name = "textBoxPassword";
         this.textBoxPassword.PasswordChar = '*';
         this.textBoxPassword.Size = new System.Drawing.Size(208, 22);
         this.textBoxPassword.TabIndex = 1;
         this.textBoxPassword.Text = "";
         // 
         // buttonOk
         // 
         this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonOk.Location = new System.Drawing.Point(256, 120);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.TabIndex = 3;
         this.buttonOk.Text = "Ok";
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(16, 24);
         this.label2.Name = "label2";
         this.label2.TabIndex = 4;
         this.label2.Text = "Имя:";
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(16, 56);
         this.label3.Name = "label3";
         this.label3.TabIndex = 5;
         this.label3.Text = "Пароль:";
         // 
         // buttonCancel
         // 
         this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonCancel.Location = new System.Drawing.Point(168, 120);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.TabIndex = 4;
         this.buttonCancel.Text = "Отмена";
         // 
         // textBoxDomain
         // 
         this.textBoxDomain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.textBoxDomain.Location = new System.Drawing.Point(120, 88);
         this.textBoxDomain.Name = "textBoxDomain";
         this.textBoxDomain.Size = new System.Drawing.Size(208, 22);
         this.textBoxDomain.TabIndex = 2;
         this.textBoxDomain.Text = "";
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(16, 88);
         this.label1.Name = "label1";
         this.label1.TabIndex = 5;
         this.label1.Text = "Домен:";
         // 
         // WebAuthorize
         // 
         this.AcceptButton = this.buttonOk;
         this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
         this.CancelButton = this.buttonCancel;
         this.ClientSize = new System.Drawing.Size(344, 158);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.label3,
                                                                      this.label2,
                                                                      this.buttonOk,
                                                                      this.textBoxPassword,
                                                                      this.textBoxLogin,
                                                                      this.buttonCancel,
                                                                      this.textBoxDomain,
                                                                      this.label1});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Name = "WebAuthorize";
         this.Text = "Прокси аутентификация";
         this.ResumeLayout(false);

      }
		#endregion

      private void buttonOk_Click(object sender, System.EventArgs e)
      {
      }
	}
}
