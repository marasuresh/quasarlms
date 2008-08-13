using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCEInternalSystem
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	public class SettingsDialog : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Button buttonOk;
      private DCEAccessLib.WebServiceSettings webServiceSettings1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SettingsDialog()
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
         this.buttonCancel = new System.Windows.Forms.Button();
         this.buttonOk = new System.Windows.Forms.Button();
         this.webServiceSettings1 = new DCEAccessLib.WebServiceSettings();
         this.SuspendLayout();
         // 
         // buttonCancel
         // 
         this.buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.buttonCancel.Location = new System.Drawing.Point(362, 252);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(81, 26);
         this.buttonCancel.TabIndex = 5;
         this.buttonCancel.Text = "Cancel";
         // 
         // buttonOk
         // 
         this.buttonOk.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.buttonOk.Location = new System.Drawing.Point(266, 252);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.Size = new System.Drawing.Size(81, 26);
         this.buttonOk.TabIndex = 5;
         this.buttonOk.Text = "Ok";
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // webServiceSettings1
         // 
         this.webServiceSettings1.Dock = System.Windows.Forms.DockStyle.Top;
         this.webServiceSettings1.Name = "webServiceSettings1";
         this.webServiceSettings1.Size = new System.Drawing.Size(454, 240);
         this.webServiceSettings1.TabIndex = 6;
         // 
         // SettingsDialog
         // 
         this.AcceptButton = this.buttonOk;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(454, 286);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.webServiceSettings1,
                                                                      this.buttonCancel,
                                                                      this.buttonOk});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.Name = "SettingsDialog";
         this.Text = "Settings";
         this.ResumeLayout(false);

      }
		#endregion

      private void buttonOk_Click(object sender, System.EventArgs e)
      {
         this.webServiceSettings1.OnOk();
      }

      private void buttonBrowse_Click(object sender, System.EventArgs e)
      {
      }
	}
}
