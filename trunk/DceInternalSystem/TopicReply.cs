using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DCEInternalSystem
{
	/// <summary>
	/// Ответ в форум
	/// </summary>
	public class TopicReply : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Label MsgDate;
      private System.Windows.Forms.Label MsgAuthor;
      private System.Windows.Forms.TextBox MsgContent;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TopicReply(string date, string author, string msg)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.MsgDate.Text = date;
         this.MsgAuthor.Text = author;
         this.MsgContent.Text = msg;

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
         this.panel1 = new System.Windows.Forms.Panel();
         this.MsgDate = new System.Windows.Forms.Label();
         this.MsgAuthor = new System.Windows.Forms.Label();
         this.MsgContent = new System.Windows.Forms.TextBox();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.MsgAuthor,
                                                                             this.MsgDate});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(524, 28);
         this.panel1.TabIndex = 0;
         // 
         // MsgDate
         // 
         this.MsgDate.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.MsgDate.Location = new System.Drawing.Point(408, 8);
         this.MsgDate.Name = "MsgDate";
         this.MsgDate.Size = new System.Drawing.Size(112, 16);
         this.MsgDate.TabIndex = 0;
         this.MsgDate.Text = "label1";
         this.MsgDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // MsgAuthor
         // 
         this.MsgAuthor.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.MsgAuthor.Location = new System.Drawing.Point(4, 8);
         this.MsgAuthor.Name = "MsgAuthor";
         this.MsgAuthor.Size = new System.Drawing.Size(396, 16);
         this.MsgAuthor.TabIndex = 1;
         this.MsgAuthor.Text = "label2";
         this.MsgAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // MsgContent
         // 
         this.MsgContent.BackColor = System.Drawing.Color.White;
         this.MsgContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.MsgContent.Dock = System.Windows.Forms.DockStyle.Fill;
         this.MsgContent.Location = new System.Drawing.Point(0, 28);
         this.MsgContent.Multiline = true;
         this.MsgContent.Name = "MsgContent";
         this.MsgContent.ReadOnly = true;
         this.MsgContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.MsgContent.Size = new System.Drawing.Size(524, 120);
         this.MsgContent.TabIndex = 2;
         this.MsgContent.Text = "";
         // 
         // TopicReply
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.MsgContent,
                                                                      this.panel1});
         this.Name = "TopicReply";
         this.Size = new System.Drawing.Size(524, 148);
         this.panel1.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion
	}
}
