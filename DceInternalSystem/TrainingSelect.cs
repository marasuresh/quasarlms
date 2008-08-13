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
	public class TrainingSelect : System.Windows.Forms.Form
	{
      private DCEInternalSystem.TrainingList trainingList1;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button OkButton;
      private System.Windows.Forms.Button CancelBtn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TrainingSelect()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
         this.trainingList1.dataList.DoubleClick += new System.EventHandler(this.trainingList1_DoubleClick);
      }

      public static DataRowView SelectTraining(DataView excludes)
      {
         TrainingSelect sel = new TrainingSelect();
         sel.trainingList1.GenList(excludes);
         sel.trainingList1.ContextMenu = null;

         if (sel.ShowDialog() ==  DialogResult.OK)
         {
            if (sel.trainingList1.dataList.SelectedItems.Count>0)
            {
               return (DataRowView) sel.trainingList1.dataList.SelectedItems[0].Tag;
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
         this.trainingList1 = new DCEInternalSystem.TrainingList();
         this.panel1 = new System.Windows.Forms.Panel();
         this.OkButton = new System.Windows.Forms.Button();
         this.CancelBtn = new System.Windows.Forms.Button();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // trainingList1
         // 
         this.trainingList1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.trainingList1.Name = "trainingList1";
         this.trainingList1.Size = new System.Drawing.Size(901, 415);
         this.trainingList1.TabIndex = 0;
         // 
         // panel1
         // 
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.OkButton,
                                                                             this.CancelBtn});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 365);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(901, 50);
         this.panel1.TabIndex = 19;
         // 
         // OkButton
         // 
         this.OkButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.OkButton.Location = new System.Drawing.Point(632, 10);
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
         this.CancelBtn.Location = new System.Drawing.Point(765, 10);
         this.CancelBtn.Name = "CancelBtn";
         this.CancelBtn.Size = new System.Drawing.Size(123, 29);
         this.CancelBtn.TabIndex = 201;
         this.CancelBtn.Text = "Отменить";
         // 
         // TrainingSelect
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
         this.ClientSize = new System.Drawing.Size(901, 415);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.panel1,
                                                                      this.trainingList1});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Name = "TrainingSelect";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Выберите тренинг";
         this.panel1.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void trainingList1_DoubleClick(object sender, System.EventArgs e)
      {
         this.DialogResult = DialogResult.OK;
         this.Close();
      }
	}
}
