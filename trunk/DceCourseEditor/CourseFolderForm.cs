using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DCECourseEditor
{
	/// <summary>
	/// Форма выбора папки курса
	/// </summary>
	public class CourseFolderForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Button ButtonOk;
      private System.Windows.Forms.Button ButtonCancel;
      private DCECourseEditor.CourseFolderChooser courseFolder;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CourseFolderForm()
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
         this.courseFolder = new DCECourseEditor.CourseFolderChooser();
         this.ButtonOk = new System.Windows.Forms.Button();
         this.ButtonCancel = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // courseFolder
         // 
         this.courseFolder.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.courseFolder.Location = new System.Drawing.Point(8, 12);
         this.courseFolder.Name = "courseFolder";
         this.courseFolder.Size = new System.Drawing.Size(402, 60);
         this.courseFolder.TabIndex = 0;
         this.courseFolder.PathChoosed += new System.EventHandler(this.courseFolder_PathChoosed);
         // 
         // ButtonOk
         // 
         this.ButtonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
         this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.ButtonOk.Enabled = false;
         this.ButtonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ButtonOk.Location = new System.Drawing.Point(108, 80);
         this.ButtonOk.Name = "ButtonOk";
         this.ButtonOk.Size = new System.Drawing.Size(96, 28);
         this.ButtonOk.TabIndex = 1;
         this.ButtonOk.Text = "OK";
         this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
         // 
         // ButtonCancel
         // 
         this.ButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
         this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ButtonCancel.Location = new System.Drawing.Point(212, 80);
         this.ButtonCancel.Name = "ButtonCancel";
         this.ButtonCancel.Size = new System.Drawing.Size(96, 28);
         this.ButtonCancel.TabIndex = 2;
         this.ButtonCancel.Text = "Отмена";
         this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
         // 
         // CourseFolderForm
         // 
         this.AcceptButton = this.ButtonOk;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.ButtonCancel;
         this.ClientSize = new System.Drawing.Size(418, 116);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.ButtonCancel,
                                                                      this.ButtonOk,
                                                                      this.courseFolder});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "CourseFolderForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Создание курса (определение папки)";
         this.ResumeLayout(false);

      }
		#endregion

      private string path = "";
      public string Path
      {
         get { return path; }
      }

      private void ButtonOk_Click(object sender, System.EventArgs e)
      {
         path = courseFolder.DiskFolder.Text;
         Close();
      }

      private void ButtonCancel_Click(object sender, System.EventArgs e)
      {
         Close();
      }

      private void courseFolder_PathChoosed(object sender, System.EventArgs e)
      {
         this.ButtonOk.Enabled = courseFolder.IsChoosed;
      }
	}
}
