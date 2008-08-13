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
	/// Форма свойств области курсов
	/// </summary>
	public class DomainForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.TreeView treeView1;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.Button button2;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public string CourseName 
      {
         set { this.label2.Text = value; }
      }
      
      private string courseId = "";
      public string CourseId 
      {
         set { this.courseId = value; }
      }

      private string oldAreaId = "";
      public string OldAreaId
      {
         set { oldAreaId = value; }
      }

      public class NodeTagClass
      {
         public NodeTagClass(TreeNode treenode, string id, string Name)
         {
            this.treenode = treenode;
            this.id = id;
            this.Name = Name;
         }

         public string id;
         public string Name;
         public TreeNode treenode;
      }

      protected void MakeChildsNodes(NodeTagClass node)
      {
         if (node == null)
         {
            string name = "Общие курсы";
            TreeNode itemtreenode = new TreeNode(name);
         
            NodeTagClass item = new NodeTagClass(
               itemtreenode, null, name);
         
            itemtreenode.Tag = item;

            this.treeView1.Nodes.Add(itemtreenode);

            MakeChildsNodes(item);
         
            itemtreenode.Expand();
            
            if ( this.oldAreaId == null || this.oldAreaId == "")
               this.treeView1.SelectedNode = itemtreenode;
         }
         else
         {
            string condition = ((node.id != null)? 
               
               "where Parent = '" + node.id + "'" :
               
               "where Parent is NULL");

            DataSet domainds = DCEWebAccess.GetdataSet(
            
               "select id, dbo.GetStrContentAlt(Name, 'RU', 'EN') as RName " +

               "from CourseDomain " +
            
               condition + " order by RName", "Domains");
      
            DataView domaindv = new DataView(domainds.Tables["Domains"]);

            foreach (DataRowView row in domaindv)
            {
               string name = row["RName"].ToString();
               TreeNode itemtreenode = new TreeNode(name);
         
               NodeTagClass item = new NodeTagClass(
                  itemtreenode, row["id"].ToString(), name);
         
               itemtreenode.Tag = item;

               node.treenode.Nodes.Add(itemtreenode);

               MakeChildsNodes(item);
         
               itemtreenode.Expand();

               if (item.id == this.oldAreaId)
                  this.treeView1.SelectedNode = itemtreenode;
            }
         }
      }

      public string SelectedDomainName
      {
         get 
         { 
            if (this.treeView1.SelectedNode != null)
               return ((NodeTagClass)this.treeView1.SelectedNode.Tag).Name; 
            else
               return null;
         }
      }
      
      public string SelectedDomainId
      {
         get 
         { 
            if (this.treeView1.SelectedNode != null)
               return ((NodeTagClass)this.treeView1.SelectedNode.Tag).id; 
            else
               return null;
         }
      }

      bool needupdate;
      public DomainForm(string oldAreaId, bool needupdate)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.oldAreaId = oldAreaId;

         this.needupdate = needupdate;

         MakeChildsNodes(null);
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
         this.label2 = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
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
         this.treeView1.Location = new System.Drawing.Point(0, 76);
         this.treeView1.Name = "treeView1";
         this.treeView1.SelectedImageIndex = -1;
         this.treeView1.Size = new System.Drawing.Size(537, 331);
         this.treeView1.TabIndex = 0;
         // 
         // panel1
         // 
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.button2,
                                                                             this.button1});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 407);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(537, 59);
         this.panel1.TabIndex = 1;
         // 
         // button2
         // 
         this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
         this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button2.Location = new System.Drawing.Point(293, 15);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(118, 34);
         this.button2.TabIndex = 1;
         this.button2.Text = "Отмена";
         this.button2.Click += new System.EventHandler(this.button2_Click);
         // 
         // button1
         // 
         this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
         this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button1.Location = new System.Drawing.Point(124, 15);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(118, 34);
         this.button1.TabIndex = 0;
         this.button1.Text = "Выбрать";
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // panel2
         // 
         this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.label2,
                                                                             this.label1});
         this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(537, 76);
         this.panel2.TabIndex = 2;
         // 
         // label2
         // 
         this.label2.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.label2.Location = new System.Drawing.Point(179, 15);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(342, 49);
         this.label2.TabIndex = 1;
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(15, 15);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(149, 24);
         this.label1.TabIndex = 0;
         this.label1.Text = "Название курса";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // DomainForm
         // 
         this.AcceptButton = this.button1;
         this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
         this.CancelButton = this.button2;
         this.ClientSize = new System.Drawing.Size(537, 466);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.treeView1,
                                                                      this.panel2,
                                                                      this.panel1});
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "DomainForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Выбор области";
         this.panel1.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void button1_Click(object sender, System.EventArgs e)
      {
         NodeTagClass area = this.treeView1.SelectedNode.Tag as NodeTagClass;

         if (needupdate)
         {
         
            if (courseId != null && courseId != "")
            {
               if (area.id == null || area.id == "")
               {
                  DCEWebAccess.execSQL(
                     @"update dbo.courses set area = null 
                     where id = '" + this.courseId + "'");
               }
               else
               {
                  DCEWebAccess.execSQL(
                     "update dbo.courses set area = '" + 
                     area.id + "' where id = '" + this.courseId + "'");
               }
            }
         }
         
         Close();
      }

      private void button2_Click(object sender, System.EventArgs e)
      {
         Close();
      }
	}
}
