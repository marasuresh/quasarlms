using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DCECourseEditor
{
	/// <summary>
	/// Выбор папки курса
	/// </summary>
	public class CourseFolderChooser : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Button BrowseButton;
      private System.Windows.Forms.GroupBox groupBox1;
      private WinFormsExtras.FolderBrowser folderBrowser;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.TextBox FolderTextBox;

      
      public TextBox DiskFolder
      {
         get { return this.FolderTextBox; }
      }
      
      private bool isChoosed = false;
      public bool IsChoosed
      {
         get { return isChoosed; }
      }

      public event EventHandler PathChoosed;
      
      public CourseFolderChooser()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.groupBox1.Text = "Выбор папки курса (корень редактора курсов : " + 
            DCEAccessLib.DCEUser.CourseRootPath + ")";

         this.FolderTextBox.Text = "<Путь не выбран>";
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
         this.FolderTextBox = new System.Windows.Forms.TextBox();
         this.BrowseButton = new System.Windows.Forms.Button();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.folderBrowser = new WinFormsExtras.FolderBrowser();
         this.groupBox1.SuspendLayout();
         this.SuspendLayout();
         // 
         // FolderTextBox
         // 
         this.FolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.FolderTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.FolderTextBox.Enabled = false;
         this.FolderTextBox.Location = new System.Drawing.Point(8, 24);
         this.FolderTextBox.Name = "FolderTextBox";
         this.FolderTextBox.Size = new System.Drawing.Size(372, 20);
         this.FolderTextBox.TabIndex = 0;
         this.FolderTextBox.Text = "";
         // 
         // BrowseButton
         // 
         this.BrowseButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.BrowseButton.Location = new System.Drawing.Point(384, 24);
         this.BrowseButton.Name = "BrowseButton";
         this.BrowseButton.Size = new System.Drawing.Size(84, 20);
         this.BrowseButton.TabIndex = 1;
         this.BrowseButton.Text = "Выбрать ...";
         this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.FolderTextBox,
                                                                                this.BrowseButton});
         this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(480, 52);
         this.groupBox1.TabIndex = 2;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Выбор корневой папки курса";
         // 
         // folderBrowser
         // 
         this.folderBrowser.OnlySubfolders = true;
         // 
         // CourseFolderChooser
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.groupBox1});
         this.Name = "CourseFolderChooser";
         this.Size = new System.Drawing.Size(480, 52);
         this.groupBox1.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void BrowseButton_Click(object sender, System.EventArgs e)
      {
         if (this.folderBrowser.ShowDialog() == DialogResult.OK)
         {
            string wholepath = this.folderBrowser.DirectoryPath + "\\";
            string userroot = DCEAccessLib.DCEUser.CourseRootPath;

            if (wholepath.IndexOf(userroot) != -1)
            {
               isChoosed = true;
               this.FolderTextBox.Text = wholepath.Substring(userroot.Length);
               
               if (PathChoosed != null)
                  PathChoosed(this, new EventArgs());
            }
            else
               MessageBox.Show("Выбранный путь находится вне пути редактора курсов (" + 
                  DCEAccessLib.DCEUser.CourseRootPath + ")" ,"Сообщение");
         }
      }
	}
}
