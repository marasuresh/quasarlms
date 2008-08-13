using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DCECourseEditor
{
	/// <summary>
	/// Создание новой версии курса
	/// </summary>
	public class VersionForm : System.Windows.Forms.Form
	{
      #region Form variables
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.TextBox textBox2;
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.Button button2;
      private System.Windows.Forms.Label label4;
      private DCECourseEditor.CourseFolderChooser courseFolderChooser1;
      private System.Windows.Forms.GroupBox groupBox1;
      #endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public string CourseName
      {
         set { this.label1.Text = value; }
      }
      
      public string OldVersion
      {
         get { return this.textBox1.Text; }
         set { this.textBox1.Text = value; }
      }

      public string NewVersion
      {
         get { return this.textBox2.Text; }
         set { this.textBox2.Text = value; }
      }

      public string CourseDiskFolder
      {
         get { return this.courseFolderChooser1.DiskFolder.Text; }
      }
      
      public VersionForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.textBox2 = new System.Windows.Forms.TextBox();
         this.button1 = new System.Windows.Forms.Button();
         this.button2 = new System.Windows.Forms.Button();
         this.label4 = new System.Windows.Forms.Label();
         this.courseFolderChooser1 = new DCECourseEditor.CourseFolderChooser();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.groupBox1.SuspendLayout();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label1.Location = new System.Drawing.Point(132, 12);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(256, 60);
         this.label1.TabIndex = 0;
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(8, 20);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(112, 20);
         this.label2.TabIndex = 1;
         this.label2.Text = "Старая версия";
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(8, 52);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(112, 20);
         this.label3.TabIndex = 2;
         this.label3.Text = "Новая версия";
         // 
         // textBox1
         // 
         this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.textBox1.Enabled = false;
         this.textBox1.Location = new System.Drawing.Point(128, 20);
         this.textBox1.MaxLength = 2;
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(28, 20);
         this.textBox1.TabIndex = 3;
         this.textBox1.Text = "";
         // 
         // textBox2
         // 
         this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.textBox2.Location = new System.Drawing.Point(128, 52);
         this.textBox2.MaxLength = 2;
         this.textBox2.Name = "textBox2";
         this.textBox2.Size = new System.Drawing.Size(28, 20);
         this.textBox2.TabIndex = 4;
         this.textBox2.Text = "";
         // 
         // button1
         // 
         this.button1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
         this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.button1.Enabled = false;
         this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button1.Location = new System.Drawing.Point(77, 240);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(96, 28);
         this.button1.TabIndex = 5;
         this.button1.Text = "OK";
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // button2
         // 
         this.button2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
         this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button2.Location = new System.Drawing.Point(221, 240);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(96, 28);
         this.button2.TabIndex = 6;
         this.button2.Text = "Отмена";
         this.button2.Click += new System.EventHandler(this.button2_Click);
         // 
         // label4
         // 
         this.label4.Location = new System.Drawing.Point(12, 12);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(112, 20);
         this.label4.TabIndex = 7;
         this.label4.Text = "Название курса";
         // 
         // courseFolderChooser1
         // 
         this.courseFolderChooser1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
         this.courseFolderChooser1.Location = new System.Drawing.Point(8, 176);
         this.courseFolderChooser1.Name = "courseFolderChooser1";
         this.courseFolderChooser1.Size = new System.Drawing.Size(380, 56);
         this.courseFolderChooser1.TabIndex = 8;
         this.courseFolderChooser1.PathChoosed += new System.EventHandler(this.courseFolderChooser1_PathChoosed);
         // 
         // groupBox1
         // 
         this.groupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
         this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.label3,
                                                                                this.textBox2,
                                                                                this.label2,
                                                                                this.textBox1});
         this.groupBox1.Location = new System.Drawing.Point(8, 84);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(380, 88);
         this.groupBox1.TabIndex = 9;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Выбор следующей версии курса";
         // 
         // VersionForm
         // 
         this.AcceptButton = this.button1;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.button2;
         this.ClientSize = new System.Drawing.Size(398, 276);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.groupBox1,
                                                                      this.courseFolderChooser1,
                                                                      this.label4,
                                                                      this.button2,
                                                                      this.button1,
                                                                      this.label1});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "VersionForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Создание новой версии";
         this.groupBox1.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void button1_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void button2_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void courseFolderChooser1_PathChoosed(object sender, System.EventArgs e)
      {
         if (this.courseFolderChooser1.IsChoosed)
            this.button1.Enabled = true;
      }
	}
}
