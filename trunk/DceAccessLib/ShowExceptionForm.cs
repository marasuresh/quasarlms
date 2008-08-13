using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DCEAccessLib
{
	/// <summary>
	/// Расширенное отображение исключения
	/// </summary>
	public class ShowExceptionForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Panel panel3;
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Panel panelStack;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.TextBox textBoxMessage;
      private System.Windows.Forms.TextBox textBoxStack;
      private System.Windows.Forms.Button button1;

      public string Message
      {
         get
         {
            return this.textBoxMessage.Text;
         }
         set
         {
            this.textBoxMessage.Text = value;
         }
      }
      public string Caption
      {
         get
         {
            return this.Text;
         }
         set
         {
            this.Text = value;
         }
      }
      public string Stack
      {
         get
         {
            return this.textBoxStack.Text;
         }
         set
         {
            this.textBoxStack.Text = value;
         }
      }

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ShowExceptionForm()
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
         this.panel3 = new System.Windows.Forms.Panel();
         this.buttonOk = new System.Windows.Forms.Button();
         this.button1 = new System.Windows.Forms.Button();
         this.panelStack = new System.Windows.Forms.Panel();
         this.textBoxStack = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.panel1 = new System.Windows.Forms.Panel();
         this.textBoxMessage = new System.Windows.Forms.TextBox();
         this.panel3.SuspendLayout();
         this.panelStack.SuspendLayout();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel3
         // 
         this.panel3.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.buttonOk,
                                                                             this.button1});
         this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel3.Location = new System.Drawing.Point(0, 289);
         this.panel3.Name = "panel3";
         this.panel3.Size = new System.Drawing.Size(545, 56);
         this.panel3.TabIndex = 2;
         // 
         // buttonOk
         // 
         this.buttonOk.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.buttonOk.Location = new System.Drawing.Point(408, 10);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.Size = new System.Drawing.Size(123, 37);
         this.buttonOk.TabIndex = 0;
         this.buttonOk.Text = "Ok";
         // 
         // button1
         // 
         this.button1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.button1.Location = new System.Drawing.Point(276, 10);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(123, 37);
         this.button1.TabIndex = 0;
         this.button1.Text = "ShowDetails";
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // panelStack
         // 
         this.panelStack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelStack.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.textBoxStack,
                                                                                 this.label1});
         this.panelStack.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panelStack.Location = new System.Drawing.Point(0, 139);
         this.panelStack.Name = "panelStack";
         this.panelStack.Size = new System.Drawing.Size(545, 150);
         this.panelStack.TabIndex = 3;
         this.panelStack.Visible = false;
         // 
         // textBoxStack
         // 
         this.textBoxStack.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.textBoxStack.Dock = System.Windows.Forms.DockStyle.Fill;
         this.textBoxStack.ForeColor = System.Drawing.SystemColors.WindowText;
         this.textBoxStack.Location = new System.Drawing.Point(0, 20);
         this.textBoxStack.Multiline = true;
         this.textBoxStack.Name = "textBoxStack";
         this.textBoxStack.ReadOnly = true;
         this.textBoxStack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBoxStack.Size = new System.Drawing.Size(541, 126);
         this.textBoxStack.TabIndex = 5;
         this.textBoxStack.Text = "";
         // 
         // label1
         // 
         this.label1.Dock = System.Windows.Forms.DockStyle.Top;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(541, 20);
         this.label1.TabIndex = 4;
         this.label1.Text = "Call Stack";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // panel1
         // 
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.textBoxMessage});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(545, 139);
         this.panel1.TabIndex = 4;
         // 
         // textBoxMessage
         // 
         this.textBoxMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.textBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
         this.textBoxMessage.ForeColor = System.Drawing.SystemColors.WindowText;
         this.textBoxMessage.Multiline = true;
         this.textBoxMessage.Name = "textBoxMessage";
         this.textBoxMessage.ReadOnly = true;
         this.textBoxMessage.Size = new System.Drawing.Size(545, 139);
         this.textBoxMessage.TabIndex = 3;
         this.textBoxMessage.Text = "";
         this.textBoxMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         // 
         // ShowExceptionForm
         // 
         this.AcceptButton = this.buttonOk;
         this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
         this.CancelButton = this.buttonOk;
         this.ClientSize = new System.Drawing.Size(545, 345);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.panel1,
                                                                      this.panelStack,
                                                                      this.panel3});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ShowExceptionForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Exception";
         this.panel3.ResumeLayout(false);
         this.panelStack.ResumeLayout(false);
         this.panel1.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void button1_Click(object sender, System.EventArgs e)
      {
         if (this.textBoxStack.Text != null && this.textBoxStack.Text != "")
         {
            this.panelStack.Visible = true;
         }
      }
	}
}
