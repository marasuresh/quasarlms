using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DCEInternalSystem
{
	/// <summary>
	/// Выбор тренинга
	/// </summary>
	public class CourseSelect : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button OkButton;
      private System.Windows.Forms.Button CancelBtn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public CoursesList list;
      public CourseSelect()
		{
			//
			// Required for Windows Form Designer support
			//
         list = new CoursesList();
         list.Dock = DockStyle.Fill;
         this.Controls.Add(list);
         InitializeComponent();
         this.list.dataList.DoubleClick += new System.EventHandler(this.list_DoubleClick);
      }

      private void list_DoubleClick(object sender, System.EventArgs e)
      {
         this.DialogResult = DialogResult.OK;
         this.Close();
      }
      /// <summary>
      /// получить список курсов
      /// </summary>
      /// <param name="excludes"></param>
      /// <returns></returns>
      public static DataRowView SelectCourse(DataView excludes)
      {
         CourseSelect sel = new CourseSelect();
         sel.list.GenList(excludes);
         sel.list.ContextMenu = null;

         if (sel.ShowDialog() ==  DialogResult.OK)
         {
            if (sel.list.dataList.SelectedItems.Count>0)
            {
               return (DataRowView) sel.list.dataList.SelectedItems[0].Tag;
            }
         }
         return null;
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
         this.panel1 = new System.Windows.Forms.Panel();
         this.OkButton = new System.Windows.Forms.Button();
         this.CancelBtn = new System.Windows.Forms.Button();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.OkButton,
                                                                             this.CancelBtn});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 276);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(522, 50);
         this.panel1.TabIndex = 19;
         // 
         // OkButton
         // 
         this.OkButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.OkButton.Location = new System.Drawing.Point(253, 10);
         this.OkButton.Name = "OkButton";
         this.OkButton.Size = new System.Drawing.Size(123, 29);
         this.OkButton.TabIndex = 200;
         this.OkButton.Text = "Ok";
         // 
         // CancelBtn
         // 
         this.CancelBtn.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.CancelBtn.Location = new System.Drawing.Point(386, 10);
         this.CancelBtn.Name = "CancelBtn";
         this.CancelBtn.Size = new System.Drawing.Size(123, 29);
         this.CancelBtn.TabIndex = 201;
         this.CancelBtn.Text = "Отменить";
         // 
         // CourseSelect
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
         this.ClientSize = new System.Drawing.Size(522, 326);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.panel1});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Name = "CourseSelect";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Выберите курс";
         this.panel1.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion
	}
}
