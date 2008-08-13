using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DCEInternalSystem
{
	/// <summary>
	/// Summary description for TrackCoursesList.
	/// </summary>
	public class TrackCoursesList : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Label label1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TrackCoursesList()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

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
         this.label1 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(24, 64);
         this.label1.Name = "label1";
         this.label1.TabIndex = 0;
         this.label1.Text = "TrackCoursesList";
         // 
         // TrackCoursesList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.label1});
         this.Name = "TrackCoursesList";
         this.ResumeLayout(false);

      }
		#endregion
	}
}
