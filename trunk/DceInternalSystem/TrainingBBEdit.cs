using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DCEInternalSystem
{
	/// <summary>
	/// Summary description for TrainingBBEdit.
	/// </summary>
	public class TrainingBBEdit : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Label label1;
      public System.Windows.Forms.DateTimePicker dateTimePicker1;
      private System.Windows.Forms.Label label2;
      public DCEAccessLib.MLEdit CComment;
      private DCEAccessLib.LangSwitcher langSwitcher1;
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.Button button2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TrainingBBEdit()
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
			this.label1 = new System.Windows.Forms.Label();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.CComment = new DCEAccessLib.MLEdit();
			this.langSwitcher1 = new DCEAccessLib.LangSwitcher();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "ƒата";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(116, 32);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(148, 20);
			this.dateTimePicker1.TabIndex = 2;
			this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "—ообщение";
			// 
			// CComment
			// 
			this.CComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.CComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CComment.CaptionLabel = null;
			this.CComment.eId = "";
			this.CComment.LanguageSwitcher = this.langSwitcher1;
			this.CComment.Location = new System.Drawing.Point(12, 92);
			this.CComment.MaxLength = 255;
			this.CComment.Multiline = true;
			this.CComment.Name = "CComment";
			this.CComment.Size = new System.Drawing.Size(468, 88);
			this.CComment.TabIndex = 3;
			// 
			// langSwitcher1
			// 
			this.langSwitcher1.Enabled = global::DCEInternalSystem.Properties.Settings.Default.LangSwitchEnabled;
			this.langSwitcher1.Location = new System.Drawing.Point(8, 4);
			this.langSwitcher1.Name = "langSwitcher1";
			this.langSwitcher1.Size = new System.Drawing.Size(188, 20);
			this.langSwitcher1.TabIndex = 1;
			this.langSwitcher1.TextLabel = "язык";
			this.langSwitcher1.Visible = global::DCEInternalSystem.Properties.Settings.Default.LangSwitchVisible;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(320, 192);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "OK";
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(404, 192);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "ќтменить";
			// 
			// TrainingBBEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(486, 223);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.langSwitcher1);
			this.Controls.Add(this.CComment);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dateTimePicker1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TrainingBBEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "–едактирование объ€влени€";
			this.VisibleChanged += new System.EventHandler(this.TrainingBBEdit_VisibleChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

      }
		#endregion

      private void TrainingBBEdit_VisibleChanged(object sender, System.EventArgs e)
      {
         if (this.Visible)
         {
            CComment.Select();
         }
      }

      public static TrainingBBEdit EditMessage(string msgId, DateTime date,bool rdonly)
      {
         TrainingBBEdit e  = new TrainingBBEdit();
         e.CComment.eId = msgId;
         e.dateTimePicker1.Value = date;
         if (rdonly)
         {
            e.button1.Enabled = false;
            e.CComment.Enabled = false;
            e.dateTimePicker1.Enabled=false;
         }
         if (e.ShowDialog() != DialogResult.Cancel)
         {
            e.CComment.AcceptInput();
            return e;
         }
         else
            return null;
      }

      private void dateTimePicker1_ValueChanged(object sender, System.EventArgs e)
      {
      
      }
	}
}
