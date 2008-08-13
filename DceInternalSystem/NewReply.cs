using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DCEInternalSystem
{
	/// <summary>
	/// Создать твет в форум
	/// </summary>
	public class NewReply : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Button OkButtonz;
      private System.Windows.Forms.Button CancelButtonz;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.Button DelBtn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      static public bool NewReplyDialog(ref string reply)
      {
         NewReply r = new NewReply();
         r.DelBtn.Visible = false;
         if (r.ShowDialog() == DialogResult.OK)
         {
            reply = r.textBox1.Text;
            return true;
         }
         return false;
      }

      static public DialogResult EditReplyDialog(ref string reply)
      {
         NewReply r = new NewReply();
         r.Text = "Изменить ответ";
         r.DelBtn.Visible = true;
         r.textBox1.Text = reply;

         DialogResult res = r.ShowDialog();
         reply = r.textBox1.Text;
         return res;
      }

      public NewReply()
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
         this.panel2 = new System.Windows.Forms.Panel();
         this.DelBtn = new System.Windows.Forms.Button();
         this.OkButtonz = new System.Windows.Forms.Button();
         this.CancelButtonz = new System.Windows.Forms.Button();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.panel2.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel2
         // 
         this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.DelBtn,
                                                                             this.OkButtonz,
                                                                             this.CancelButtonz});
         this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel2.Location = new System.Drawing.Point(0, 149);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(536, 36);
         this.panel2.TabIndex = 2;
         // 
         // DelBtn
         // 
         this.DelBtn.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.DelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.DelBtn.Location = new System.Drawing.Point(8, 8);
         this.DelBtn.Name = "DelBtn";
         this.DelBtn.Size = new System.Drawing.Size(96, 24);
         this.DelBtn.TabIndex = 2;
         this.DelBtn.Text = "Удалить";
         this.DelBtn.Click += new System.EventHandler(this.DelBtn_Click);
         // 
         // OkButtonz
         // 
         this.OkButtonz.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.OkButtonz.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OkButtonz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.OkButtonz.Location = new System.Drawing.Point(336, 8);
         this.OkButtonz.Name = "OkButtonz";
         this.OkButtonz.Size = new System.Drawing.Size(96, 24);
         this.OkButtonz.TabIndex = 3;
         this.OkButtonz.Text = "OK";
         // 
         // CancelButtonz
         // 
         this.CancelButtonz.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.CancelButtonz.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelButtonz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.CancelButtonz.Location = new System.Drawing.Point(436, 8);
         this.CancelButtonz.Name = "CancelButtonz";
         this.CancelButtonz.Size = new System.Drawing.Size(96, 24);
         this.CancelButtonz.TabIndex = 4;
         this.CancelButtonz.Text = "Отменить";
         // 
         // textBox1
         // 
         this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.textBox1.Multiline = true;
         this.textBox1.Name = "textBox1";
         this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBox1.Size = new System.Drawing.Size(536, 149);
         this.textBox1.TabIndex = 1;
         this.textBox1.Text = "";
         // 
         // NewReply
         // 
         this.AcceptButton = this.OkButtonz;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.CancelButtonz;
         this.ClientSize = new System.Drawing.Size(536, 185);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.textBox1,
                                                                      this.panel2});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "NewReply";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Ответить";
         this.panel2.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void DelBtn_Click(object sender, System.EventArgs e)
      {
         if (MessageBox.Show("Вы действительно хотите удалить данный ответ?","Удалить",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
         {
            this.DialogResult = DialogResult.Abort;
            this.Close();
         }
         else
            this.DelBtn.DialogResult = DialogResult.None;
      }

	}
}
