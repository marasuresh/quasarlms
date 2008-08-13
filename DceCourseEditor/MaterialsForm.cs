using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DCEAccessLib;

namespace DCECourseEditor
{
	/// <summary>
	/// Выбор материала наполнения темы
	/// </summary>
	public class MaterialsForm : System.Windows.Forms.Form
	{
      #region Form variables
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button ExitButton;
      private System.Windows.Forms.OpenFileDialog OpenFileDialog;
      private System.Windows.Forms.Button BrowseButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.TextBox RelativleTextBox;
      private System.Windows.Forms.TextBox AbsoluteTextBox;
      private System.Windows.Forms.Panel PreviewPanel;
      private System.Windows.Forms.Button PreviewButton;
      private WebBrowser WebBrowser;
      private System.Windows.Forms.Button RefreshButton;
      #endregion

      public string Path;
      public MaterialsForm(string path, string coursediskfolder, string Title, string filter)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
            
         this.SaveButton.Enabled = DCEUser.CurrentUser.EditableCourses;
         
         AbsoluteTextBox.Text = DCEUser.CourseRootPath + coursediskfolder;
         this.Text = Title;
         this.OpenFileDialog.Filter = filter;

         RelativleTextBox.Text = path;

         bool b = RelativleTextBox.Focus();
         
         ChangeLayout();
      }
      
      private void MakePreview(string a, string r)
      {
         if (r != "")
         {
            string url = a + r;
            Object refmissing = System.Reflection.Missing.Value;
            WebBrowser.Navigate(url);
         }
      }

      private void ChangeLayout()
      {
         if (preview)
         {
            this.PreviewButton.Text = "<< Просмотр";
            this.PreviewPanel.Visible = true;
            
            // y = 120 + this.PreviewPanel.Height
            this.Height = this.SaveButton.Bottom + 40 + this.PreviewPanel.Height;

            MakePreview(AbsoluteTextBox.Text, RelativleTextBox.Text);
         }
         else
         {
            // y = 120
            this.Height = this.SaveButton.Bottom + 40;

            this.PreviewButton.Text = "Просмотр >>";
            this.PreviewPanel.Visible = false;
         }
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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MaterialsForm));
         this.label1 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.SaveButton = new System.Windows.Forms.Button();
         this.ExitButton = new System.Windows.Forms.Button();
         this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
         this.BrowseButton = new System.Windows.Forms.Button();
         this.RelativleTextBox = new System.Windows.Forms.TextBox();
         this.AbsoluteTextBox = new System.Windows.Forms.TextBox();
         this.PreviewPanel = new System.Windows.Forms.Panel();
         this.WebBrowser = new WebBrowser();
         this.PreviewButton = new System.Windows.Forms.Button();
         this.RefreshButton = new System.Windows.Forms.Button();
         this.PreviewPanel.SuspendLayout();
         this.WebBrowser.SuspendLayout();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(9, 19);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(136, 20);
         this.label1.TabIndex = 0;
         this.label1.Text = "Корневой путь";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(9, 52);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(136, 20);
         this.label3.TabIndex = 0;
         this.label3.Text = "Относительный путь";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // SaveButton
         // 
         this.SaveButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SaveButton.Location = new System.Drawing.Point(492, 88);
         this.SaveButton.Name = "SaveButton";
         this.SaveButton.Size = new System.Drawing.Size(103, 27);
         this.SaveButton.TabIndex = 3;
         this.SaveButton.Text = "OK";
         this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
         // 
         // ExitButton
         // 
         this.ExitButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ExitButton.Location = new System.Drawing.Point(608, 88);
         this.ExitButton.Name = "ExitButton";
         this.ExitButton.Size = new System.Drawing.Size(103, 27);
         this.ExitButton.TabIndex = 4;
         this.ExitButton.Text = "Отмена";
         this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
         // 
         // OpenFileDialog
         // 
         this.OpenFileDialog.Filter = "Htm files|*.htm|Html files|*.html;*.htm|All files|*.*";
         // 
         // BrowseButton
         // 
         this.BrowseButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.BrowseButton.Location = new System.Drawing.Point(608, 50);
         this.BrowseButton.Name = "BrowseButton";
         this.BrowseButton.Size = new System.Drawing.Size(103, 24);
         this.BrowseButton.TabIndex = 2;
         this.BrowseButton.Text = "Выбрать...";
         this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
         // 
         // RelativleTextBox
         // 
         this.RelativleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.RelativleTextBox.Location = new System.Drawing.Point(145, 52);
         this.RelativleTextBox.Name = "RelativleTextBox";
         this.RelativleTextBox.Size = new System.Drawing.Size(447, 20);
         this.RelativleTextBox.TabIndex = 1;
         this.RelativleTextBox.Text = "";
         // 
         // AbsoluteTextBox
         // 
         this.AbsoluteTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.AbsoluteTextBox.Enabled = false;
         this.AbsoluteTextBox.Location = new System.Drawing.Point(145, 19);
         this.AbsoluteTextBox.Name = "AbsoluteTextBox";
         this.AbsoluteTextBox.Size = new System.Drawing.Size(449, 20);
         this.AbsoluteTextBox.TabIndex = 8;
         this.AbsoluteTextBox.TabStop = false;
         this.AbsoluteTextBox.Text = "";
         // 
         // PreviewPanel
         // 
         this.PreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.PreviewPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.WebBrowser});
         this.PreviewPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.PreviewPanel.Location = new System.Drawing.Point(0, 120);
         this.PreviewPanel.Name = "PreviewPanel";
         this.PreviewPanel.Size = new System.Drawing.Size(722, 276);
         this.PreviewPanel.TabIndex = 9;
         // 
         // WebBrowser
         // 
         this.WebBrowser.Parent = this;
         this.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
         this.WebBrowser.Enabled = true;
         this.WebBrowser.Size = new System.Drawing.Size(720, 274);
         this.WebBrowser.TabIndex = 174;
         // 
         // PreviewButton
         // 
         this.PreviewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.PreviewButton.Location = new System.Drawing.Point(8, 88);
         this.PreviewButton.Name = "PreviewButton";
         this.PreviewButton.Size = new System.Drawing.Size(103, 27);
         this.PreviewButton.TabIndex = 10;
         this.PreviewButton.Text = "Просмотр >>";
         this.PreviewButton.Click += new System.EventHandler(this.PreviewButton_Click);
         // 
         // RefreshButton
         // 
         this.RefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.RefreshButton.Location = new System.Drawing.Point(124, 88);
         this.RefreshButton.Name = "RefreshButton";
         this.RefreshButton.Size = new System.Drawing.Size(103, 27);
         this.RefreshButton.TabIndex = 11;
         this.RefreshButton.Text = "Обновить";
         this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
         // 
         // MaterialsForm
         // 
         this.AcceptButton = this.SaveButton;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.ExitButton;
         this.ClientSize = new System.Drawing.Size(722, 396);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.RefreshButton,
                                                                      this.PreviewButton,
                                                                      this.PreviewPanel,
                                                                      this.RelativleTextBox,
                                                                      this.BrowseButton,
                                                                      this.ExitButton,
                                                                      this.SaveButton,
                                                                      this.label3,
                                                                      this.label1,
                                                                      this.AbsoluteTextBox});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "MaterialsForm";
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Выбор материала";
         this.PreviewPanel.ResumeLayout(false);
         this.WebBrowser.ResumeLayout();
         this.ResumeLayout(false);

      }
		#endregion

      private void BrowseButton_Click(object sender, System.EventArgs e)
      {
         string ast = this.AbsoluteTextBox.Text + this.RelativleTextBox.Text;
         ast = ast.ToLower();

         //System.IO.Path.GetDirectoryName()
         this.OpenFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(ast);

         if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
         {
            string rst = OpenFileDialog.FileName.ToLower();

            int pos = rst.IndexOf(AbsoluteTextBox.Text.ToLower());
            if ( (pos == -1) || ( pos != 0) )
               System.Windows.Forms.MessageBox.Show("Файл находиться вне абсолютного пути!", "Ошибка", MessageBoxButtons.OK);
            else
            {
               rst = rst.Substring(AbsoluteTextBox.Text.Length, rst.Length - AbsoluteTextBox.Text.Length);
               this.RelativleTextBox.Text = rst;

               if (preview)
                  MakePreview(ast, rst);
            }
         }
      }

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         this.Path = this.RelativleTextBox.Text.ToString();
         this.Close();
      }

      private void ExitButton_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private bool preview = false;
      private void PreviewButton_Click(object sender, System.EventArgs e)
      {
         preview = !preview;
         ChangeLayout();      
      }

      private void RefreshButton_Click(object sender, System.EventArgs e)
      {
         ChangeLayout();      
      }
	}
}
