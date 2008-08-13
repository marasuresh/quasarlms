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
	/// Свойства области курсов
	/// </summary>
	public class DomainEdit : System.Windows.Forms.Form
	{
      private DCEAccessLib.LangSwitcher langSwitcher1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button ButtonOk;
      private System.Windows.Forms.Button ButtonCancel;
      public DCEAccessLib.MLEdit NameEdit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      
      private CoursesListNode Node;

      public DomainEdit(CoursesListNode node)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         Node = node;

         this.ButtonOk.Enabled = DCEUser.CurrentUser.EditableCourses;

         this.NameEdit.DataBindings.Add("eid", Node.EditRow, "Name");
         this.NameEdit.SetParentNode(Node);
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
         this.langSwitcher1 = new DCEAccessLib.LangSwitcher();
         this.NameEdit = new DCEAccessLib.MLEdit();
         this.ButtonOk = new System.Windows.Forms.Button();
         this.ButtonCancel = new System.Windows.Forms.Button();
         this.label1 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // langSwitcher1
         // 
         this.langSwitcher1.Location = new System.Drawing.Point(8, 8);
         this.langSwitcher1.Name = "langSwitcher1";
         this.langSwitcher1.Size = new System.Drawing.Size(192, 24);
         this.langSwitcher1.TabIndex = 0;
         // 
         // NameEdit
         // 
         this.NameEdit.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.NameEdit.AutoSize = false;
         this.NameEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.NameEdit.eId = null;
         this.NameEdit.LanguageSwitcher = this.langSwitcher1;
         this.NameEdit.Location = new System.Drawing.Point(148, 40);
         this.NameEdit.Name = "NameEdit";
         this.NameEdit.Size = new System.Drawing.Size(472, 24);
         this.NameEdit.TabIndex = 1;
         this.NameEdit.Text = "mlEdit1";
         // 
         // ButtonOk
         // 
         this.ButtonOk.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.ButtonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ButtonOk.Location = new System.Drawing.Point(436, 72);
         this.ButtonOk.Name = "ButtonOk";
         this.ButtonOk.Size = new System.Drawing.Size(88, 28);
         this.ButtonOk.TabIndex = 2;
         this.ButtonOk.Text = "OK";
         this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
         // 
         // ButtonCancel
         // 
         this.ButtonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ButtonCancel.Location = new System.Drawing.Point(532, 72);
         this.ButtonCancel.Name = "ButtonCancel";
         this.ButtonCancel.Size = new System.Drawing.Size(88, 28);
         this.ButtonCancel.TabIndex = 3;
         this.ButtonCancel.Text = "Отмена";
         this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(8, 40);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(140, 24);
         this.label1.TabIndex = 4;
         this.label1.Text = "Название области :";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // DomainEdit
         // 
         this.AcceptButton = this.ButtonOk;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.ButtonCancel;
         this.ClientSize = new System.Drawing.Size(628, 106);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.label1,
                                                                      this.ButtonCancel,
                                                                      this.ButtonOk,
                                                                      this.NameEdit,
                                                                      this.langSwitcher1});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "DomainEdit";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Редактирование области";
         this.Closed += new System.EventHandler(this.DomainEdit_Closed);
         this.ResumeLayout(false);

      }
		#endregion

      private void ButtonOk_Click(object sender, System.EventArgs e)
      {
         Node.EndEdit(false, false);
      }

      private void ButtonCancel_Click(object sender, System.EventArgs e)
      {
         Node.EndEdit(true, false);
      }

      private void DomainEdit_Closed(object sender, System.EventArgs e)
      {
         if (Node.EditRow.IsEdit)
            Node.EndEdit(true, false);
      }
	}
}
