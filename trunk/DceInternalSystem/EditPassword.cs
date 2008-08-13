using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DCEInternalSystem
{
	/// <summary>
	/// Изменить пароль
	/// </summary>
	public class EditPassword : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.Button button2;
      public System.Windows.Forms.TextBox pwd1;
      public System.Windows.Forms.TextBox pwd2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public static  bool Edit(ref string pwd, int maxlen)
      {
         EditPassword ed = new EditPassword();
         ed.pwd1.MaxLength = maxlen;
         ed.pwd2.MaxLength = maxlen;
         if (ed.ShowDialog() == DialogResult.OK)
         {
            pwd = ed.pwd1.Text;
            return true;
         }
         return false;
      }

		public EditPassword()
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
         this.pwd1 = new System.Windows.Forms.TextBox();
         this.pwd2 = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.button1 = new System.Windows.Forms.Button();
         this.button2 = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // pwd1
         // 
         this.pwd1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.pwd1.Location = new System.Drawing.Point(184, 10);
         this.pwd1.Name = "pwd1";
         this.pwd1.PasswordChar = '*';
         this.pwd1.Size = new System.Drawing.Size(256, 22);
         this.pwd1.TabIndex = 0;
         this.pwd1.Text = "";
         // 
         // pwd2
         // 
         this.pwd2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.pwd2.Location = new System.Drawing.Point(184, 40);
         this.pwd2.Name = "pwd2";
         this.pwd2.PasswordChar = '*';
         this.pwd2.Size = new System.Drawing.Size(256, 22);
         this.pwd2.TabIndex = 1;
         this.pwd2.Text = "";
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(8, 15);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(168, 17);
         this.label1.TabIndex = 2;
         this.label1.Text = "Пароль";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(8, 40);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(168, 16);
         this.label2.TabIndex = 3;
         this.label2.Text = "Подтверждение пароля ";
         this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // button1
         // 
         this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button1.Location = new System.Drawing.Point(232, 80);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(96, 28);
         this.button1.TabIndex = 4;
         this.button1.Text = "OK";
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // button2
         // 
         this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button2.Location = new System.Drawing.Point(344, 80);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(96, 28);
         this.button2.TabIndex = 5;
         this.button2.Text = "Отмена";
         // 
         // EditPassword
         // 
         this.AcceptButton = this.button1;
         this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
         this.CancelButton = this.button2;
         this.ClientSize = new System.Drawing.Size(450, 118);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.button2,
                                                                      this.button1,
                                                                      this.label2,
                                                                      this.label1,
                                                                      this.pwd2,
                                                                      this.pwd1});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.Name = "EditPassword";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Задать пароль";
         this.ResumeLayout(false);

      }
		#endregion

      private void button1_Click(object sender, System.EventArgs e)
      {
         if (pwd1.Text != pwd2.Text)
         {
            System.Windows.Forms.MessageBox.Show("Пароль и подтверждение не совпадают.","Ошибка");
            return;
         }
         if (pwd1.Text.Length < 4)
         {
            System.Windows.Forms.MessageBox.Show("Пароль должен содержать минимум 4 символа.","Ошибка");
            return;
         }
         this.DialogResult = DialogResult.OK;
      }
	}
}
