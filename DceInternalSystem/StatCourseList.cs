using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DCEInternalSystem
{
	/// <summary>
	/// Summary description for StatCourseList.
	/// </summary>
	public class StatCourseList : System.Windows.Forms.UserControl
	{
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader3;
      private DCEAccessLib.DataColumnHeader dataColumnHeader4;
      private DCEAccessLib.DataColumnHeader dataColumnHeader5;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StatCourseList()
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
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader3 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader4 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader5 = new DCEAccessLib.DataColumnHeader();
         this.SuspendLayout();
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.dataColumnHeader1,
                                                                               this.dataColumnHeader2,
                                                                               this.dataColumnHeader3,
                                                                               this.dataColumnHeader4,
                                                                               this.dataColumnHeader5});
         this.dataList.DataView = null;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(833, 670);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 21;
         this.dataList.View = System.Windows.Forms.View.Details;
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "StudentName";
         this.dataColumnHeader1.Text = "Студент/Абитуриент";
         this.dataColumnHeader1.Width = 200;
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "CourseName";
         this.dataColumnHeader2.Text = "Курс";
         this.dataColumnHeader2.Width = 200;
         // 
         // dataColumnHeader3
         // 
         this.dataColumnHeader3.FieldName = "RequestDate";
         this.dataColumnHeader3.Text = "Дата заявки";
         this.dataColumnHeader3.Width = 100;
         // 
         // dataColumnHeader4
         // 
         this.dataColumnHeader4.FieldName = "StartDate";
         this.dataColumnHeader4.Text = "Желаемое начало обучения";
         this.dataColumnHeader4.Width = 150;
         // 
         // dataColumnHeader5
         // 
         this.dataColumnHeader5.FieldName = "Comments";
         this.dataColumnHeader5.Text = "Коментарии";
         this.dataColumnHeader5.Width = 300;
         // 
         // StatCourseList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.dataList});
         this.Name = "StatCourseList";
         this.Size = new System.Drawing.Size(833, 670);
         this.ResumeLayout(false);

      }
		#endregion
	}
}
