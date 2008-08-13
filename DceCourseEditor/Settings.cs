using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCECourseEditor
{
	/// <summary>
	/// Настройки редактора курсов
	/// </summary>
	public class Settings : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label labelCoursesRoot;
      private System.Windows.Forms.Button buttonBrowse;
      private System.Windows.Forms.GroupBox groupBox1;
      private WinFormsExtras.FolderBrowser folderBrowser1;
      //private DCEAccessLib.WebServiceSettings webServiceSettings;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Settings()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.labelCoursesRoot.Text = DCEAccessLib.DCEUser.CourseRootPath;

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
         this.buttonCancel = new System.Windows.Forms.Button();
         this.buttonOk = new System.Windows.Forms.Button();
         this.label1 = new System.Windows.Forms.Label();
         this.labelCoursesRoot = new System.Windows.Forms.Label();
         this.buttonBrowse = new System.Windows.Forms.Button();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.folderBrowser1 = new WinFormsExtras.FolderBrowser();
         this.groupBox1.SuspendLayout();
         this.SuspendLayout();
         // 
         // buttonCancel
         // 
         this.buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonCancel.Location = new System.Drawing.Point(362, 341);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(96, 28);
         this.buttonCancel.TabIndex = 4;
         this.buttonCancel.Text = "Отмена";
         // 
         // buttonOk
         // 
         this.buttonOk.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonOk.Location = new System.Drawing.Point(250, 341);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.Size = new System.Drawing.Size(96, 28);
         this.buttonOk.TabIndex = 3;
         this.buttonOk.Text = "Ok";
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(8, 20);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(137, 26);
         this.label1.TabIndex = 6;
         this.label1.Text = "Путь :";
         // 
         // labelCoursesRoot
         // 
         this.labelCoursesRoot.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.labelCoursesRoot.Location = new System.Drawing.Point(8, 52);
         this.labelCoursesRoot.Name = "labelCoursesRoot";
         this.labelCoursesRoot.Size = new System.Drawing.Size(340, 25);
         this.labelCoursesRoot.TabIndex = 7;
         // 
         // buttonBrowse
         // 
         this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonBrowse.Location = new System.Drawing.Point(352, 52);
         this.buttonBrowse.Name = "buttonBrowse";
         this.buttonBrowse.Size = new System.Drawing.Size(92, 25);
         this.buttonBrowse.TabIndex = 2;
         this.buttonBrowse.Text = "Выбрать...";
         this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.buttonBrowse,
                                                                                this.labelCoursesRoot,
                                                                                this.label1});
         this.groupBox1.Location = new System.Drawing.Point(8, 244);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(452, 88);
         this.groupBox1.TabIndex = 9;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Редактор курсов";
         // 
         // folderBrowser1
         // 
         this.folderBrowser1.StartLocation = WinFormsExtras.FolderBrowser.FolderID.MyComputer;
         // 
         // Settings
         // 
         this.AcceptButton = this.buttonOk;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.buttonCancel;
         this.ClientSize = new System.Drawing.Size(466, 375);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.groupBox1,
                                                                      this.buttonCancel,
                                                                      this.buttonOk});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "Settings";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Настройки";
         this.groupBox1.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void buttonOk_Click(object sender, System.EventArgs e)
      {
         DCEAccessLib.DCEUser.CourseRootPath = this.labelCoursesRoot.Text;
      }

      private void buttonBrowse_Click(object sender, System.EventArgs e)
      {
         if ( DialogResult.OK == folderBrowser1.ShowDialog() )
         {
            string path = folderBrowser1.DirectoryPath;

            if (!path.EndsWith("\\")) 
               path = path + "\\";

            this.labelCoursesRoot.Text = path;
         }
      }

      private void webServiceSettings_Load(object sender, System.EventArgs e)
      {
      
      }
	}
}
