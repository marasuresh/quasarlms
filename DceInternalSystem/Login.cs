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
   /// Главная нода, появляется при запуске программы
   /// </summary>
   /// <summary>
	/// Summary description for Login.
	/// </summary>
	public class UserAuth : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.TextBox textBox2;
      private NodeControl fNode;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UserAuth(NodeControl Node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.fNode = Node;
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
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.button1 = new System.Windows.Forms.Button();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.textBox2 = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(40, 60);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(44, 23);
         this.label1.TabIndex = 0;
         this.label1.Text = "Логин";
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(40, 92);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(52, 23);
         this.label2.TabIndex = 1;
         this.label2.Text = "Пароль";
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(72, 128);
         this.button1.Name = "button1";
         this.button1.TabIndex = 2;
         this.button1.Text = "Войти";
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(100, 60);
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(104, 20);
         this.textBox1.TabIndex = 3;
         this.textBox1.Text = "textBox1";
         // 
         // textBox2
         // 
         this.textBox2.Location = new System.Drawing.Point(100, 92);
         this.textBox2.Name = "textBox2";
         this.textBox2.Size = new System.Drawing.Size(104, 20);
         this.textBox2.TabIndex = 4;
         this.textBox2.Text = "textBox2";
         // 
         // UserAuth
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.textBox2,
                                                                      this.textBox1,
                                                                      this.button1,
                                                                      this.label2,
                                                                      this.label1});
         this.Name = "UserAuth";
         this.Size = new System.Drawing.Size(420, 276);
         this.Enter += new System.EventHandler(this.UserAuth_Enter);
         this.ResumeLayout(false);

      }
		#endregion

      private void button1_Click(object sender, System.EventArgs e)
      {
         DCEUser.CurrentUser.Users = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Students = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Courses = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Questionnaire = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Requests = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Shedule = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Tests = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Trainings = DCEUser.Access.Modify;
         DCEUser.CurrentUser.Authorized = true;
         
         fNode.Expand();
      }

      private void UserAuth_Enter(object sender, System.EventArgs e)
      {

      }
	}
}
