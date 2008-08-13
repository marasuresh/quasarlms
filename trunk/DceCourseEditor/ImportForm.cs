using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DCECourseEditor
{
	/// <summary>
	/// Форма импорта курсов
	/// </summary>
	public class ImportForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Button buttonDomain;
      private System.Windows.Forms.Label labelCourse;
      private DCECourseEditor.CourseFolderChooser courseFolderChooser;
      private System.Windows.Forms.Label labelDomain;
      private System.Windows.Forms.Button button2;
      private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.ComboBox TypeCB;

      // по умолчанию выбрана область "Общие курсы"
      private DomainForm domainform;
      
      private void FillComboBox(ComboBox c, string query, string tablename, string displaymember, string valuemember)
      {
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            query,
            tablename);

         ds.Tables[tablename].Rows.Add(new object[]{System.DBNull.Value, "<Пусто>"});
         
         c.DisplayMember = displaymember;
         c.ValueMember = valuemember;
         c.DataSource = ds.Tables[tablename];

         c.SelectedValue = System.DBNull.Value;
      }

      public ImportForm(string domainid)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         domainform = new DomainForm( domainid == "" ? null : domainid, false);

         DomainId = domainid;

         this.labelDomain.Text = domainform.SelectedDomainName;

         FillComboBox(
            TypeCB, 
            "select id, dbo.GetStrContentAlt(Name, 'RU', 'EN') as RName from CourseType", 
            "CourseType", 
            "RName", 
            "id");
      }

      public string CourseName
      {
         set { this.labelCourse.Text = value; }
      }
      
      private string domainid = null;
      public string DomainId
      {
         get { return domainid; }
         set 
         { 
            domainid = value;
            domainform.OldAreaId = domainid;
         }
      }

      public string DiskFolder
      {
         get { return courseFolderChooser.DiskFolder.Text; }
      }
      
      public string TypeId
      {
         get { return TypeCB.SelectedValue.ToString(); }
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
         this.label4 = new System.Windows.Forms.Label();
         this.labelCourse = new System.Windows.Forms.Label();
         this.courseFolderChooser = new DCECourseEditor.CourseFolderChooser();
         this.labelDomain = new System.Windows.Forms.Label();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.buttonDomain = new System.Windows.Forms.Button();
         this.button2 = new System.Windows.Forms.Button();
         this.button1 = new System.Windows.Forms.Button();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.TypeCB = new System.Windows.Forms.ComboBox();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.SuspendLayout();
         // 
         // label4
         // 
         this.label4.Location = new System.Drawing.Point(12, 12);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(112, 20);
         this.label4.TabIndex = 9;
         this.label4.Text = "Название курса";
         // 
         // labelCourse
         // 
         this.labelCourse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelCourse.Location = new System.Drawing.Point(132, 12);
         this.labelCourse.Name = "labelCourse";
         this.labelCourse.Size = new System.Drawing.Size(256, 48);
         this.labelCourse.TabIndex = 8;
         // 
         // courseFolderChooser
         // 
         this.courseFolderChooser.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
         this.courseFolderChooser.Location = new System.Drawing.Point(8, 196);
         this.courseFolderChooser.Name = "courseFolderChooser";
         this.courseFolderChooser.Size = new System.Drawing.Size(380, 56);
         this.courseFolderChooser.TabIndex = 10;
         this.courseFolderChooser.PathChoosed += new System.EventHandler(this.courseFolderChooser_PathChoosed);
         // 
         // labelDomain
         // 
         this.labelDomain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelDomain.Location = new System.Drawing.Point(8, 24);
         this.labelDomain.Name = "labelDomain";
         this.labelDomain.Size = new System.Drawing.Size(272, 20);
         this.labelDomain.TabIndex = 11;
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.buttonDomain,
                                                                                this.labelDomain});
         this.groupBox1.Location = new System.Drawing.Point(8, 68);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(380, 56);
         this.groupBox1.TabIndex = 12;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Область курса";
         // 
         // buttonDomain
         // 
         this.buttonDomain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonDomain.Location = new System.Drawing.Point(284, 24);
         this.buttonDomain.Name = "buttonDomain";
         this.buttonDomain.Size = new System.Drawing.Size(84, 20);
         this.buttonDomain.TabIndex = 12;
         this.buttonDomain.Text = "Выбрать ...";
         this.buttonDomain.Click += new System.EventHandler(this.buttonDomain_Click);
         // 
         // button2
         // 
         this.button2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
         this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button2.Location = new System.Drawing.Point(223, 260);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(96, 28);
         this.button2.TabIndex = 14;
         this.button2.Text = "Отмена";
         // 
         // button1
         // 
         this.button1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
         this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.button1.Enabled = false;
         this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button1.Location = new System.Drawing.Point(79, 260);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(96, 28);
         this.button1.TabIndex = 13;
         this.button1.Text = "OK";
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.TypeCB});
         this.groupBox2.Location = new System.Drawing.Point(8, 132);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(380, 56);
         this.groupBox2.TabIndex = 15;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Тип курса";
         // 
         // TypeCB
         // 
         this.TypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.TypeCB.Location = new System.Drawing.Point(8, 24);
         this.TypeCB.Name = "TypeCB";
         this.TypeCB.Size = new System.Drawing.Size(364, 21);
         this.TypeCB.TabIndex = 0;
         // 
         // ImportForm
         // 
         this.AcceptButton = this.button1;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.button2;
         this.ClientSize = new System.Drawing.Size(398, 296);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.groupBox2,
                                                                      this.button2,
                                                                      this.button1,
                                                                      this.groupBox1,
                                                                      this.courseFolderChooser,
                                                                      this.label4,
                                                                      this.labelCourse});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ImportForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Импортирование курса";
         this.groupBox1.ResumeLayout(false);
         this.groupBox2.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void buttonDomain_Click(object sender, System.EventArgs e)
      {
         domainform.CourseName = labelCourse.Text;
         if (domainform.ShowDialog() == DialogResult.OK)
         {
            this.labelDomain.Text = domainform.SelectedDomainName;
            domainid = domainform.SelectedDomainId;
         }
      }

      private void courseFolderChooser_PathChoosed(object sender, System.EventArgs e)
      {
         if (this.courseFolderChooser.IsChoosed)
            this.button1.Enabled = true;
      }
	}
}
