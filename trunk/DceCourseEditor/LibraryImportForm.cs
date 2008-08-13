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
	/// Импорт библиотеки
	/// </summary>
	public class LibraryImportForm : System.Windows.Forms.Form
	{
      #region Form variables
      private System.Windows.Forms.TreeView treeView1;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button button2;
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.ComboBox comboBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      #endregion

      private class CourseTag
      {
         private string id;
         private string name;
         public CourseTag(string id, string name)
         {
            this.id = id;
            this.name = name;
         }
         
         public string Id
         {
            get { return id; }
         } 
 
         public string Name
         {
            get { return name; }
         } 
         
         public override string ToString()
         {
            return name;
         }
      }
      
      string parentthemeid;
      public LibraryImportForm(string courseid, string parentthemeid)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         // заполнение combobox
         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            
            "select id, dbo.GetStrContentAlt(Name,'RU','EN') as Name " + 
            
            " from Courses where id <> '" + courseid + "'", 
            
            "Courses");

         if (ds.Tables["Courses"].Rows.Count == 0)
            this.comboBox1.Items.Add(new CourseTag(null, "<Пусто>"));

         foreach(DataRow row in ds.Tables["Courses"].Rows)
         {
            this.comboBox1.Items.Add(new CourseTag(row["id"].ToString(), row["name"].ToString()));
         }

         this.comboBox1.SelectedIndex = 0;

         this.parentthemeid = parentthemeid;
      }

      private void MakeTree(string parentid, TreeNode node)
      {
         if (parentid == null || parentid == "") 
         {
            this.treeView1.Nodes.Clear();
            return;
         }

         if (node == null)
            this.treeView1.Nodes.Clear();

         DataSet librarys = DCEWebAccess.GetdataSet(
            
            "select id, dbo.GetStrContentAlt(Name, 'RU', 'EN') as RName " +

            "from Themes " +
            
            "where type = " + (int)ThemeType.library + " and Parent = '" + parentid + "' " + 
            
            "order by TOrder", "Themes");
      
         DataView themesdv = new DataView(librarys.Tables["Themes"]);

         foreach (DataRowView row in themesdv)
         {
            
            string name = row["RName"].ToString();
            TreeNode itemtreenode = new TreeNode(name);
         
            itemtreenode.Tag = row["id"].ToString();

            if (node == null)
            {
               this.treeView1.Nodes.Add(itemtreenode);
            }
            else
            {
               node.Nodes.Add(itemtreenode);
            }

            MakeTree((string)itemtreenode.Tag, itemtreenode);
         
            itemtreenode.Expand();
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
         this.treeView1 = new System.Windows.Forms.TreeView();
         this.panel1 = new System.Windows.Forms.Panel();
         this.button2 = new System.Windows.Forms.Button();
         this.button1 = new System.Windows.Forms.Button();
         this.panel2 = new System.Windows.Forms.Panel();
         this.label1 = new System.Windows.Forms.Label();
         this.comboBox1 = new System.Windows.Forms.ComboBox();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.SuspendLayout();
         // 
         // treeView1
         // 
         this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.treeView1.FullRowSelect = true;
         this.treeView1.ImageIndex = -1;
         this.treeView1.Location = new System.Drawing.Point(0, 40);
         this.treeView1.Name = "treeView1";
         this.treeView1.SelectedImageIndex = -1;
         this.treeView1.Size = new System.Drawing.Size(572, 250);
         this.treeView1.TabIndex = 3;
         // 
         // panel1
         // 
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.button2,
                                                                             this.button1});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 290);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(572, 56);
         this.panel1.TabIndex = 4;
         // 
         // button2
         // 
         this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
         this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button2.Location = new System.Drawing.Point(309, 14);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(107, 32);
         this.button2.TabIndex = 1;
         this.button2.Text = "Отмена";
         this.button2.Click += new System.EventHandler(this.button2_Click);
         // 
         // button1
         // 
         this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
         this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button1.Location = new System.Drawing.Point(154, 14);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(108, 32);
         this.button1.TabIndex = 0;
         this.button1.Text = "Выбрать";
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // panel2
         // 
         this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.comboBox1,
                                                                             this.label1});
         this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(572, 40);
         this.panel2.TabIndex = 5;
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(12, 8);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(136, 23);
         this.label1.TabIndex = 0;
         this.label1.Text = "Название курса";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // comboBox1
         // 
         this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBox1.Location = new System.Drawing.Point(160, 8);
         this.comboBox1.Name = "comboBox1";
         this.comboBox1.Size = new System.Drawing.Size(404, 21);
         this.comboBox1.TabIndex = 1;
         this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
         // 
         // LibraryImportForm
         // 
         this.AcceptButton = this.button1;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.button2;
         this.ClientSize = new System.Drawing.Size(572, 346);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.treeView1,
                                                                      this.panel1,
                                                                      this.panel2});
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "LibraryImportForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Импортирование библиотекм";
         this.panel1.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void button1_Click(object sender, System.EventArgs e)
      {
         TreeNode node = this.treeView1.SelectedNode;

         if (node != null)
         {
            string themeid = (string)node.Tag;
            string newthemeid = Guid.NewGuid().ToString();

            DCEWebAccess.execSQL(
               
               "exec dbo.MakeThemeCopy '" + newthemeid + "', '" + themeid + "', '" + parentthemeid + "' " + 
               
               "exec dbo.MakeThemesCopy '" + newthemeid + "', '" + themeid + "'");
         }

         Close();
      }

      private void button2_Click(object sender, System.EventArgs e)
      {
         Close();
      }

      private void comboBox1_SelectedValueChanged(object sender, System.EventArgs e)
      {
         MakeTree(((CourseTag)this.comboBox1.Items[this.comboBox1.SelectedIndex]).Id, null);
      }
	}
}
