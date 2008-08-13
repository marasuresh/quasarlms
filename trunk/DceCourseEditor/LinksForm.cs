using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCECourseEditor
{
	/// <summary>
	/// Summary description for LinksForm.
	/// </summary>
	public class LinksForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Button RefreshButton;
      private System.Windows.Forms.Button PreviewButton;
      private System.Windows.Forms.Panel PreviewPanel;
      private WebBrowser WebBrowser;
      private System.Windows.Forms.Button ExitButton;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.TextBox LinkTextBox;
      private System.Windows.Forms.Panel OperatePanel;

      public string Url;
      
      public LinksForm(string path, string Title)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.SaveButton.Enabled = DCEUser.CurrentUser.EditableCourses;
         
         this.Text = Title;

         LinkTextBox.Text = path;

         bool b = LinkTextBox.Focus();
         
         //ChangeLayout();
      }
      
      private void MakePreview(string url)
      {
         Object refmissing = System.Reflection.Missing.Value;
         WebBrowser.Navigate(url);
      }

      private int h = 0;
      private void ChangeLayout()
      {
         if (preview)
         {
            this.PreviewButton.Text = "<< Просмотр";
            this.PreviewPanel.Visible = true;
            
            h = this.Height;
            this.Height += 400;

            MakePreview(LinkTextBox.Text);
         }
         else
         {
            this.Height = h;

            this.PreviewButton.Text = "Просмотр >>";
            this.PreviewPanel.Visible = false;
         }

      }

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

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         this.Url = this.LinkTextBox.Text.ToString();
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
         
         this.RefreshButton.Enabled = preview;
         
         ChangeLayout();      
      }

      private void RefreshButton_Click(object sender, System.EventArgs e)
      {
         MakePreview(this.LinkTextBox.Text);      
      }
   
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LinksForm));
         this.RefreshButton = new System.Windows.Forms.Button();
         this.PreviewButton = new System.Windows.Forms.Button();
         this.PreviewPanel = new System.Windows.Forms.Panel();
         this.WebBrowser = new WebBrowser();
         this.LinkTextBox = new System.Windows.Forms.TextBox();
         this.ExitButton = new System.Windows.Forms.Button();
         this.SaveButton = new System.Windows.Forms.Button();
         this.label3 = new System.Windows.Forms.Label();
         this.OperatePanel = new System.Windows.Forms.Panel();
         this.PreviewPanel.SuspendLayout();
         this.WebBrowser.SuspendLayout();
         this.OperatePanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // RefreshButton
         // 
         this.RefreshButton.Enabled = false;
         this.RefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.RefreshButton.Location = new System.Drawing.Point(124, 40);
         this.RefreshButton.Name = "RefreshButton";
         this.RefreshButton.Size = new System.Drawing.Size(103, 27);
         this.RefreshButton.TabIndex = 21;
         this.RefreshButton.Text = "Обновить";
         this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
         // 
         // PreviewButton
         // 
         this.PreviewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.PreviewButton.Location = new System.Drawing.Point(8, 40);
         this.PreviewButton.Name = "PreviewButton";
         this.PreviewButton.Size = new System.Drawing.Size(103, 27);
         this.PreviewButton.TabIndex = 20;
         this.PreviewButton.Text = "Просмотр >>";
         this.PreviewButton.Click += new System.EventHandler(this.PreviewButton_Click);
         // 
         // PreviewPanel
         // 
         this.PreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.PreviewPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.WebBrowser});
         this.PreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.PreviewPanel.Location = new System.Drawing.Point(0, 76);
         this.PreviewPanel.Name = "PreviewPanel";
         this.PreviewPanel.Size = new System.Drawing.Size(720, 0);
         this.PreviewPanel.TabIndex = 19;
         // 
         // WebBrowser
         // 
         this.WebBrowser.Parent = this;
         this.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
         this.WebBrowser.Enabled = true;
         this.WebBrowser.Size = new System.Drawing.Size(718, 0);
         this.WebBrowser.TabIndex = 174;
         // 
         // LinkTextBox
         // 
         this.LinkTextBox.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.LinkTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.LinkTextBox.Location = new System.Drawing.Point(132, 8);
         this.LinkTextBox.Name = "LinkTextBox";
         this.LinkTextBox.Size = new System.Drawing.Size(580, 20);
         this.LinkTextBox.TabIndex = 14;
         this.LinkTextBox.Text = "";
         // 
         // ExitButton
         // 
         this.ExitButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ExitButton.Location = new System.Drawing.Point(608, 40);
         this.ExitButton.Name = "ExitButton";
         this.ExitButton.Size = new System.Drawing.Size(103, 27);
         this.ExitButton.TabIndex = 17;
         this.ExitButton.Text = "Отмена";
         this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
         // 
         // SaveButton
         // 
         this.SaveButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
         this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SaveButton.Location = new System.Drawing.Point(492, 40);
         this.SaveButton.Name = "SaveButton";
         this.SaveButton.Size = new System.Drawing.Size(103, 27);
         this.SaveButton.TabIndex = 16;
         this.SaveButton.Text = "OK";
         this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(8, 8);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(116, 20);
         this.label3.TabIndex = 12;
         this.label3.Text = "Ссылка";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // OperatePanel
         // 
         this.OperatePanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.PreviewButton,
                                                                                   this.ExitButton,
                                                                                   this.SaveButton,
                                                                                   this.label3,
                                                                                   this.RefreshButton,
                                                                                   this.LinkTextBox});
         this.OperatePanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.OperatePanel.Name = "OperatePanel";
         this.OperatePanel.Size = new System.Drawing.Size(720, 76);
         this.OperatePanel.TabIndex = 22;
         // 
         // LinksForm
         // 
         this.AcceptButton = this.SaveButton;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.ExitButton;
         this.ClientSize = new System.Drawing.Size(720, 74);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.PreviewPanel,
                                                                      this.OperatePanel});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "LinksForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "LinksForm";
         this.PreviewPanel.ResumeLayout(false);
         this.WebBrowser.ResumeLayout();
         this.OperatePanel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion
	}
}
