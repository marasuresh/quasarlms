using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCEInternalSystem
{
	/// <summary>
	/// Создать заявку на курс
	/// </summary>
	public class CreateRequest : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.Button button2;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label StudentName;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.Label label2;
      public System.Windows.Forms.DateTimePicker StartDate;
      private System.Windows.Forms.Label label3;
      public System.Windows.Forms.TextBox Comments;

      public string aaa;
      public CoursesList list;
		public CreateRequest(string studentName)
		{
			//
			// Required for Windows Form Designer support
			//
         list = new CoursesList();
         list.Dock = DockStyle.Fill;
         this.Controls.Add(list);
			InitializeComponent();

         StudentName.Text = studentName;
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
         this.panel1 = new System.Windows.Forms.Panel();
         this.button2 = new System.Windows.Forms.Button();
         this.button1 = new System.Windows.Forms.Button();
         this.panel2 = new System.Windows.Forms.Panel();
         this.Comments = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.StartDate = new System.Windows.Forms.DateTimePicker();
         this.label2 = new System.Windows.Forms.Label();
         this.StudentName = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.button2,
                                                                             this.button1});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 289);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(536, 44);
         this.panel1.TabIndex = 0;
         // 
         // button2
         // 
         this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button2.Location = new System.Drawing.Point(420, 12);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(108, 24);
         this.button2.TabIndex = 1;
         this.button2.Text = "Отменить";
         // 
         // button1
         // 
         this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button1.Location = new System.Drawing.Point(296, 12);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(116, 24);
         this.button1.TabIndex = 0;
         this.button1.Text = "Создать";
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // panel2
         // 
         this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.Comments,
                                                                             this.label3,
                                                                             this.StartDate,
                                                                             this.label2,
                                                                             this.StudentName,
                                                                             this.label1});
         this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(536, 156);
         this.panel2.TabIndex = 1;
         // 
         // Comments
         // 
         this.Comments.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.Comments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.Comments.Location = new System.Drawing.Point(100, 72);
         this.Comments.Multiline = true;
         this.Comments.Name = "Comments";
         this.Comments.Size = new System.Drawing.Size(430, 76);
         this.Comments.TabIndex = 2;
         this.Comments.Text = "";
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(8, 72);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(80, 28);
         this.label3.TabIndex = 4;
         this.label3.Text = "Комментарии";
         // 
         // StartDate
         // 
         this.StartDate.Location = new System.Drawing.Point(208, 36);
         this.StartDate.Name = "StartDate";
         this.StartDate.Size = new System.Drawing.Size(160, 20);
         this.StartDate.TabIndex = 1;
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(8, 36);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(192, 28);
         this.label2.TabIndex = 2;
         this.label2.Text = "Желаемая дата начала обучения";
         // 
         // StudentName
         // 
         this.StudentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.StudentName.Location = new System.Drawing.Point(76, 8);
         this.StudentName.Name = "StudentName";
         this.StudentName.Size = new System.Drawing.Size(348, 20);
         this.StudentName.TabIndex = 1;
         this.StudentName.Text = "label2";
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(8, 8);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(56, 16);
         this.label1.TabIndex = 0;
         this.label1.Text = "Студент";
         // 
         // CreateRequest
         // 
         this.AcceptButton = this.button1;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.button2;
         this.ClientSize = new System.Drawing.Size(536, 333);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.panel2,
                                                                      this.panel1});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "CreateRequest";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Выберите курс для заявки";
         this.panel1.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void button1_Click(object sender, System.EventArgs e)
      {
         if (this.list.dataList.SelectedItems.Count ==0)
         {
            MessageBox.Show("Выберите курс для создания заявки","Ошибка");
         }
         else
         {
            this.DialogResult = DialogResult.OK;
            Close();
         }
      }
	}
}
