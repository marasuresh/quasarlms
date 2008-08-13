using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCEInternalSystem
{
	/// <summary>
	/// Класс для редактирования групп
	/// </summary>
	public class GroupEdit : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button OkButton;
      private System.Windows.Forms.Button CancelButton;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.ComponentModel.IContainer components;
      private System.Windows.Forms.Label TitleLabel;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox GDesc;
      private System.Windows.Forms.TextBox GName;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem4;
      private System.Windows.Forms.MenuItem AddGroupItem;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.MenuItem DeleteMemberItm;
      private System.Windows.Forms.MenuItem AddMemberItm;
      private System.Windows.Forms.MenuItem ItmProps;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.Panel CuratorsPane;
      private System.Windows.Forms.TreeView Mtree;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.ToolBar toolBar1;
      private System.Windows.Forms.ToolBarButton btnRefresh;
      private System.Windows.Forms.ToolBarButton btnAdd;
      private System.Windows.Forms.ToolBarButton btnDel;
      private System.Windows.Forms.ToolBarButton btnProps;

      GroupEditNode Node;
      bool canmodify = true;
      public GroupEdit(GroupEditNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.toolBar1.ImageList = DCEInternalSystem.ToolbarImages.Images.images;
         this.Node = node;
         if (Node.Flat)
         {
            AddGroupItem.Enabled = false;
            this.Mtree.ShowRootLines = false;
         }
         this.CuratorsPane.Visible = Node.ShowCurators;
         if (Node.ShowCurators)
         {
            GroupCurators c = new GroupCurators(this.Node);
            this.CuratorsPane.Controls.Add(c);
            c.Dock = DockStyle.Fill;
         }

         this.contextMenu1.Popup += new System.EventHandler(MenuPopup);
         Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(RebindControls);
         RebindControls();
         if (Node.ShowCurators)
         {
            canmodify = DCEUser.CurrentUser.Students == DCEUser.Access.Modify;
         }
         else if (this.Node.MembersType == EntityType.training)
            canmodify = (DCEUser.CurrentUser.Trainings == DCEUser.Access.Modify);
         else if (this.Node.MembersType == EntityType.course)
         {
            this.btnProps.Enabled = false;
            this.ItmProps.Enabled = false;
            canmodify = (DCEUser.CurrentUser.Trainings == DCEUser.Access.Modify);
         }
         else if (this.Node.MembersType == EntityType.user)
         {
            canmodify = (DCEUser.CurrentUser.Trainings == DCEUser.Access.Modify);
         }
         else if (this.Node.MembersType == EntityType.student)
         {
            canmodify = (DCEUser.CurrentUser.Trainings == DCEUser.Access.Modify)
               || DCEUser.CurrentUser.isCurator(Node.TrainingId);
         }
         else
            canmodify = false;


         this.btnDel.Enabled = this.btnAdd.Enabled = 
         AddMemberItm.Enabled = canmodify;
         AddGroupItem.Enabled = canmodify && !Node.Flat;
         DeleteMemberItm.Enabled = canmodify;

         OkButton.Enabled = canmodify;
         GDesc.Enabled = canmodify;
         GName.Enabled = canmodify;
		}

      protected void MenuPopup(object obj,System.EventArgs a)
      {
         TreeNode node=this.Mtree.SelectedNode;
         if (node !=null)
         {
            DataRowView row = (DataRowView)node.Tag;
            DeleteMemberItm.Enabled =canmodify && (row["MGroup"].ToString() == this.Node.Id);
            AddMemberItm.Enabled = canmodify;
            AddGroupItem.Enabled = canmodify && !Node.Flat;

            if (this.Node.MembersType != EntityType.course)
               ItmProps.Enabled = ((int) row["Type"]) != (int)(EntityType.group);
         }
         else
         {
            AddMemberItm.Enabled = canmodify;
            AddGroupItem.Enabled = canmodify && !Node.Flat;
            DeleteMemberItm.Enabled = false;
            ItmProps.Enabled = false;
         }
      }

      public void RebindControls()
      {
         this.GName.DataBindings.Clear();
         this.GName.DataBindings.Add("Text",Node.EditRow,"Name");
         this.GName.MaxLength = 100;
         this.GDesc.DataBindings.Clear();
         this.GDesc.DataBindings.Add("Text",Node.EditRow,"Description");
         this.GDesc.MaxLength = 255;
         // filling tree
         UpdateList();
      }
      
      protected void UpdateList()
      {
         // scan new dataview and refresh treeview
         this.Mtree.BeginUpdate();
         System.Collections.Hashtable groupHash = new System.Collections.Hashtable();

         try
         {
            Mtree.Nodes.Clear();
            IEnumerator en = Node.MembersDataView.GetEnumerator();
            en.Reset();
            groupHash.Add(Node.Id,this.Mtree.Nodes);

            while (en.MoveNext())
            {
               DataRowView row = (DataRowView)en.Current;
               TreeNodeCollection coll = (TreeNodeCollection)groupHash[row["MGroup"].ToString()];
               TreeNode node;
               if ( ((int) row["Type"]) == (int)(EntityType.group) )
               {
                  node = new TreeNode("Группа: " + row["GName"].ToString());
                  node.Tag = row;
                  groupHash[row["id"].ToString()] = node.Nodes;
               }
               else
               {
                  if (this.Node.MembersType == EntityType.training)
                     node = new TreeNode(row["TrName"].ToString());
                  else if (this.Node.MembersType == EntityType.course)
                     node = new TreeNode(row["CName"].ToString());
                  else
                     node = new TreeNode(row["LastName"].ToString() + " " +
                        row["FirstName"].ToString() + " " +
                        row["Patronymic"].ToString() );
                  node.Tag = row;
               }
               if (row["MGroup"].ToString() != this.Node.Id)
                  node.Text = node.Text + " *";
               if (coll!=null)
               {
                  coll.Add(node);
               }
               else
               {
                  if ( ((int) row["Type"]) == (int)(EntityType.group) )
                     this.Mtree.Nodes.Add(node);
               }
            }
         }
         finally
         {
            this.Mtree.ExpandAll();
            this.Mtree.EndUpdate();
            this.Mtree.Invalidate();
         }
      }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         Node = null;
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
         this.panel1 = new System.Windows.Forms.Panel();
         this.label4 = new System.Windows.Forms.Label();
         this.OkButton = new System.Windows.Forms.Button();
         this.CancelButton = new System.Windows.Forms.Button();
         this.panel2 = new System.Windows.Forms.Panel();
         this.label3 = new System.Windows.Forms.Label();
         this.GDesc = new System.Windows.Forms.TextBox();
         this.GName = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.TitleLabel = new System.Windows.Forms.Label();
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.ItmProps = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.AddMemberItm = new System.Windows.Forms.MenuItem();
         this.AddGroupItem = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.DeleteMemberItm = new System.Windows.Forms.MenuItem();
         this.CuratorsPane = new System.Windows.Forms.Panel();
         this.label5 = new System.Windows.Forms.Label();
         this.Mtree = new System.Windows.Forms.TreeView();
         this.toolBar1 = new System.Windows.Forms.ToolBar();
         this.btnRefresh = new System.Windows.Forms.ToolBarButton();
         this.btnAdd = new System.Windows.Forms.ToolBarButton();
         this.btnDel = new System.Windows.Forms.ToolBarButton();
         this.btnProps = new System.Windows.Forms.ToolBarButton();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.CuratorsPane.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.label4,
                                                                             this.OkButton,
                                                                             this.CancelButton});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 324);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(576, 52);
         this.panel1.TabIndex = 1;
         // 
         // label4
         // 
         this.label4.Location = new System.Drawing.Point(4, 4);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(332, 16);
         this.label4.TabIndex = 208;
         this.label4.Text = "* - удаление данных членов группы запрещено";
         // 
         // OkButton
         // 
         this.OkButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.OkButton.Location = new System.Drawing.Point(370, 22);
         this.OkButton.Name = "OkButton";
         this.OkButton.Size = new System.Drawing.Size(96, 24);
         this.OkButton.TabIndex = 5;
         this.OkButton.Text = "Сохранить";
         this.OkButton.Click += new System.EventHandler(this.SaveChangesClick);
         // 
         // CancelButton
         // 
         this.CancelButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.CancelButton.Location = new System.Drawing.Point(474, 22);
         this.CancelButton.Name = "CancelButton";
         this.CancelButton.Size = new System.Drawing.Size(96, 24);
         this.CancelButton.TabIndex = 6;
         this.CancelButton.Text = "Отменить";
         this.CancelButton.Click += new System.EventHandler(this.CancelChangesClick);
         // 
         // panel2
         // 
         this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.label3,
                                                                             this.GDesc,
                                                                             this.GName,
                                                                             this.label2,
                                                                             this.label1});
         this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel2.Location = new System.Drawing.Point(0, 37);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(576, 108);
         this.panel2.TabIndex = 2;
         // 
         // label3
         // 
         this.label3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label3.Dock = System.Windows.Forms.DockStyle.Top;
         this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label3.ForeColor = System.Drawing.SystemColors.Info;
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(576, 20);
         this.label3.TabIndex = 13;
         this.label3.Text = "Свойства группы";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // GDesc
         // 
         this.GDesc.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.GDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.GDesc.Location = new System.Drawing.Point(76, 52);
         this.GDesc.Multiline = true;
         this.GDesc.Name = "GDesc";
         this.GDesc.Size = new System.Drawing.Size(496, 52);
         this.GDesc.TabIndex = 2;
         this.GDesc.Text = "";
         // 
         // GName
         // 
         this.GName.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.GName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.GName.Location = new System.Drawing.Point(76, 24);
         this.GName.Name = "GName";
         this.GName.Size = new System.Drawing.Size(496, 20);
         this.GName.TabIndex = 1;
         this.GName.Text = "";
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(4, 52);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(64, 16);
         this.label2.TabIndex = 1;
         this.label2.Text = "Описание";
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(4, 28);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(64, 16);
         this.label1.TabIndex = 0;
         this.label1.Text = "Название";
         // 
         // TitleLabel
         // 
         this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
         this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
         this.TitleLabel.Location = new System.Drawing.Point(0, 145);
         this.TitleLabel.Name = "TitleLabel";
         this.TitleLabel.Size = new System.Drawing.Size(576, 20);
         this.TitleLabel.TabIndex = 12;
         this.TitleLabel.Text = "Члены группы";
         this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // contextMenu1
         // 
         this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.ItmProps,
                                                                                     this.menuItem2,
                                                                                     this.AddMemberItm,
                                                                                     this.AddGroupItem,
                                                                                     this.menuItem4,
                                                                                     this.DeleteMemberItm});
         // 
         // ItmProps
         // 
         this.ItmProps.Index = 0;
         this.ItmProps.Text = "Свойства";
         this.ItmProps.Click += new System.EventHandler(this.ItmProps_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 1;
         this.menuItem2.Text = "-";
         // 
         // AddMemberItm
         // 
         this.AddMemberItm.Index = 2;
         this.AddMemberItm.Text = "Добавить";
         this.AddMemberItm.Click += new System.EventHandler(this.AddMemberClick);
         // 
         // AddGroupItem
         // 
         this.AddGroupItem.Index = 3;
         this.AddGroupItem.Text = "Добавить группу";
         this.AddGroupItem.Click += new System.EventHandler(this.AddGroupClick);
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 4;
         this.menuItem4.Text = "-";
         // 
         // DeleteMemberItm
         // 
         this.DeleteMemberItm.Index = 5;
         this.DeleteMemberItm.Text = "Удалить";
         this.DeleteMemberItm.Click += new System.EventHandler(this.RemoveMemberClick);
         // 
         // CuratorsPane
         // 
         this.CuratorsPane.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.label5});
         this.CuratorsPane.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.CuratorsPane.Location = new System.Drawing.Point(0, 208);
         this.CuratorsPane.Name = "CuratorsPane";
         this.CuratorsPane.Size = new System.Drawing.Size(576, 116);
         this.CuratorsPane.TabIndex = 4;
         // 
         // label5
         // 
         this.label5.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label5.Dock = System.Windows.Forms.DockStyle.Top;
         this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label5.ForeColor = System.Drawing.SystemColors.Info;
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(576, 20);
         this.label5.TabIndex = 13;
         this.label5.Text = "Кураторы";
         this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // Mtree
         // 
         this.Mtree.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.Mtree.ContextMenu = this.contextMenu1;
         this.Mtree.Dock = System.Windows.Forms.DockStyle.Fill;
         this.Mtree.HideSelection = false;
         this.Mtree.ImageIndex = -1;
         this.Mtree.Location = new System.Drawing.Point(0, 165);
         this.Mtree.Name = "Mtree";
         this.Mtree.SelectedImageIndex = -1;
         this.Mtree.Size = new System.Drawing.Size(576, 43);
         this.Mtree.TabIndex = 3;
         this.Mtree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Mtree_MouseDown);
         // 
         // toolBar1
         // 
         this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
         this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                    this.btnRefresh,
                                                                                    this.btnAdd,
                                                                                    this.btnDel,
                                                                                    this.btnProps});
         this.toolBar1.ButtonSize = new System.Drawing.Size(28, 24);
         this.toolBar1.Divider = false;
         this.toolBar1.DropDownArrows = true;
         this.toolBar1.Name = "toolBar1";
         this.toolBar1.ShowToolTips = true;
         this.toolBar1.Size = new System.Drawing.Size(576, 37);
         this.toolBar1.TabIndex = 36;
         this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
         // 
         // btnRefresh
         // 
         this.btnRefresh.ImageIndex = 0;
         this.btnRefresh.Text = "Обновить";
         // 
         // btnAdd
         // 
         this.btnAdd.ImageIndex = 1;
         this.btnAdd.Text = "Добавить";
         // 
         // btnDel
         // 
         this.btnDel.ImageIndex = 2;
         this.btnDel.Text = "Удалить";
         // 
         // btnProps
         // 
         this.btnProps.ImageIndex = 3;
         this.btnProps.Text = "Свойства";
         // 
         // GroupEdit
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.Mtree,
                                                                      this.CuratorsPane,
                                                                      this.TitleLabel,
                                                                      this.panel2,
                                                                      this.panel1,
                                                                      this.toolBar1});
         this.Name = "GroupEdit";
         this.Size = new System.Drawing.Size(576, 376);
         this.panel1.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.CuratorsPane.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void AddMemberClick(object sender, System.EventArgs e)
      {
         switch (Node.MembersType)
         {
            case EntityType.user:
            {
               DataRowView user = UserSelect.SelectUser(this.Node.MembersDataView);
               if (user !=null)
               {
                  DataRowView row = this.Node.MembersDataView.AddNew();
                  row["mid"] = System.Guid.NewGuid();
                  row["id"] = user["id"].ToString();
                  row["MGroup"] = this.Node.Id;
                  row["FirstName"] = user["FirstName"].ToString();
                  row["LastName"] = user["LastName"].ToString();
                  row["Patronymic"] = user["Patronymic"].ToString();
                  row["Type"] =(int)EntityType.user;
                  row.EndEdit();
                  this.UpdateList();
                  this.Node.Changed = true;
               }
            }
               break;

            case EntityType.student:
            {
               DataRowView user = UserSelect.SelectStudent(this.Node.MembersDataView,"id");
               if (user !=null)
               {
                  DataRowView row = this.Node.MembersDataView.AddNew();
                  row["mid"] = System.Guid.NewGuid();
                  row["id"] = user["id"].ToString();
                  row["MGroup"] = this.Node.Id;
                  row["FirstName"] = user["FirstName"].ToString();
                  row["LastName"] = user["LastName"].ToString();
                  row["Patronymic"] = user["Patronymic"].ToString();
                  row["Type"] =(int)EntityType.student;
                  row.EndEdit();
                  this.UpdateList();
                  this.Node.Changed = true;
               }
            }
               break;

            case EntityType.training:
            {
               DataRowView training = TrainingSelect.SelectTraining(this.Node.MembersDataView);
               if (training !=null)
               {
                  DataRowView row = this.Node.MembersDataView.AddNew();
                  row["mid"] = System.Guid.NewGuid();
                  row["id"] = training["id"].ToString();
                  row["MGroup"] = this.Node.Id;
                  row["Type"] =(int)EntityType.training;
                  row["TrName"] = training["RName"];
                  row.EndEdit();
                  this.UpdateList();
                  this.Node.Changed = true;
               }
            }
               break;
            case EntityType.course:
            {
               DataRowView course = CourseSelect.SelectCourse(this.Node.MembersDataView);
               if (course !=null)
               {
                  DataRowView row = this.Node.MembersDataView.AddNew();
                  row["mid"] = System.Guid.NewGuid();
                  row["id"] = course["id"].ToString();
                  row["MGroup"] = this.Node.Id;
                  row["Type"] =(int)EntityType.course;
                  row["CName"] = course["CName"];
                  row.EndEdit();
                  this.UpdateList();
                  this.Node.Changed = true;
               }
            }
               break;
         }
      }

      private void AddGroupClick(object sender, System.EventArgs e)
      {
         // add group
         DataRowView grow = GroupSelect.SelectGroup(this.Node.MembersDataView,this.Node.Id, this.Node.MembersType);
         if (grow != null)
         {
            DataRowView row = this.Node.MembersDataView.AddNew();
            row["mid"] = System.Guid.NewGuid();
            row["id"] = grow["id"].ToString();
            row["MGroup"] = this.Node.Id;
            row["GName"] = grow["Name"].ToString();
            row["Type"] =(int)EntityType.group;
            row.EndEdit();
            this.UpdateList();
            this.Node.Changed = true;
         }
      }

      private void RemoveMemberClick(object sender, System.EventArgs e)
      {
         // removing member
         if (this.Mtree.SelectedNode != null)
         {
            DataRowView row = (DataRowView)this.Mtree.SelectedNode.Tag;
            if (row != null)
            {
               if  (row["MGroup"].ToString() != this.Node.Id)
               {
                  MessageBox.Show("Вы не можете удалить данного члена группы, так как он не принадлежит редактируемой группе напрямую.");
                  return;
               }
               string msg;
               if ( (int) row["Type"] == (int) EntityType.group)
                  msg = "Вы действительно хотите удалить подгруппу '";
               else
                  msg = "Вы действительно хотите удалить члена группы '";

               if (MessageBox.Show(msg + this.Mtree.SelectedNode.Text+"'?", "Удалить", MessageBoxButtons.YesNo) == DialogResult.Yes)
               {
                  row.Delete();
                  this.UpdateList();
                  this.Node.Changed = true;
               }
            }
         }
      }

      private void SaveChangesClick(object sender, System.EventArgs e)
      {
         // saving changes
         this.Node.EndEdit(false,false);
          
      }

      private void CancelChangesClick(object sender, System.EventArgs e)
      {
         // canceling changes
         this.Node.EndEdit(true,this.Node.IsNew);
      }

      private void ItmProps_Click(object sender, System.EventArgs e)
      {
         if (this.Mtree.SelectedNode != null)
         {
            DataRowView row = (DataRowView)this.Mtree.SelectedNode.Tag;
            if (this.Node.MembersType == DCEAccessLib.EntityType.student )
            {
               if (((int) row["Type"]) == (int)(EntityType.student))
               {
                  foreach (NodeControl node in this.Node.Nodes)
                  {
                     if (node.GetType() == typeof(StudentEditNode))
                     {
                        if (((StudentEditNode)node).Id ==row["id"].ToString())
                        {
                           node.Select();
                           return;
                        }
                     }
                  }
                  StudentEditNode n = new StudentEditNode(this.Node,row["id"].ToString());
                  n.Select();
               }
            }
            if (this.Node.MembersType == DCEAccessLib.EntityType.user )
            {  
               if (((int) row["Type"]) == (int)(EntityType.user))
               {
                  foreach (NodeControl nd in this.Node.Nodes)
                  {
                     if (nd.GetType() == typeof(UserEditNode))
                     {
                        if (((UserEditNode)nd).Id ==row["id"].ToString())
                        {
                           nd.Select();
                           return;
                        }
                     }
                  }
                  UserEditNode unode = new UserEditNode(this.Node,row["id"].ToString());
                  unode.Select();
               }
            }
         }
      }

      private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
      {
         if (e.Button == this.btnRefresh)      
         {
            this.Node.EndEdit(true,false);
         }
         if (e.Button == this.btnAdd)      
         {
            AddMemberClick(null,null);
         }
         if (e.Button == this.btnDel)      
         {
            RemoveMemberClick(null,null);
         }
         if (e.Button == this.btnProps)      
         {
            ItmProps_Click(null,null);
         }

      }

      private void Mtree_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.Mtree.SelectedNode = this.Mtree.GetNodeAt(e.X,e.Y);
      }
	}

   /// <summary>
   /// Нода для редактирования групп
   /// </summary>
   public class GroupEditNode : RecordEditNode
   {
//      public string Id;
      public DCEAccessLib.EntityType MembersType;
      public bool Flat;
      private bool AllowNew;
      public bool ShowCurators;
      public DataSet MembersDataSet;
      public DataView MembersDataView;

      private string GCaption;
      private string query;
      public string TrainingId;

      /// <summary>
      /// Нода для редактирования групп
      /// </summary>
      /// <param name="parent"></param>
      /// <param name="Id">id редактируемой группы</param>
      /// <param name="membersType">тип элементов в группе</param>
      /// <param name="flat">редактирование только верхних вхождений в группу(не позволять добавлять подгруппы)</param>
      /// <param name="caption">название группы</param>
      /// <param name="trainingId">id тренинга</param>
      /// <param name="allownew">позволять создание новой группы</param>
      /// <param name="showCurators">показывать список кураторов</param>
      public GroupEditNode(NodeControl parent, string Id, 
         DCEAccessLib.EntityType membersType, bool flat, string caption, 
         string trainingId , bool allownew , bool showCurators
         )
         : base(parent, "select * from dbo.Groups", "Groups", "id", Id)
      {

         this.ShowCurators = showCurators;
         this.AllowNew = allownew;
         this.MembersType = membersType;
         if (this.IsNew && !allownew)
         {
            // do not allow to create groups
            throw new DCEException("Неизвестная группа: "+ Id ,DCEAccessLib.DCEException.ExceptionLevel.ErrorContinue);
         }
         else
         if (this.IsNew)
         {
            this.EditRow["Type"] = (int) this.MembersType;
         }
           
         this.GCaption = caption;
         this.Flat = flat;
         this.CaptionChanged();
         this.TrainingId = trainingId;

         query="";
         switch (this.MembersType)
         {
            case EntityType.student:
               if (flat)
                  query = "select u.FirstName,u.LastName,u.Patronymic, gm.mid, gm.id , gm.MGroup, NULL as GName, e.Type from "
                     + " Students u, GroupMembers gm, Entities e where "   
                     + " e.id = u.id and e.Type="+ ((int)EntityType.student).ToString() + " and gm.id = e.id and gm.MGroup= '"+this.Id+"'"
                     ;
               else
               {
                  if (this.TrainingId != "" && this.TrainingId != null)
                     query = "select u.FirstName,u.LastName,u.Patronymic, gm.mid, gm.id , gm.MGroup, gm.name as GName, gm.Type from "
                        + " dbo.AllTrainingStudents('"+this.TrainingId+"') gm left join Students u on(u.id=gm.id)";
                        else
                     query = "select u.FirstName,u.LastName,u.Patronymic, gm.mid, gm.id , gm.MGroup, gm.name as GName, gm.Type from "
                        + " AllGroupMembers('"+this.Id+"') gm left join Students u on(u.id=gm.id)"
                        ;
               }
               break;
            case EntityType.user:
               if (flat)
               query = "select u.FirstName,u.LastName,u.Patronymic, gm.mid, gm.id , gm.MGroup, NULL as GName, e.Type  from "
                  + " Users u, GroupMembers gm, Entities e where "   
                  + " e.id = u.id and e.Type="+ ((int)EntityType.user).ToString() + " and gm.id = e.id and gm.MGroup= '"+this.Id+"'"
                  ;
               else
                  query = "select u.FirstName,u.LastName,u.Patronymic, gm.mid, gm.id , gm.MGroup, gm.name as GName, gm.Type from "
                     + " AllGroupMembers('"+this.Id+"') gm left join Users u on(u.id=gm.id)"
                  ;
               break;

            case EntityType.training:
               query = @"select gm.mid, gm.id, gm.MGroup , dbo.GetStrContentAlt(tr.Name,'RU', 'EN') as TrName, tr.Code , e.Type 
                  from GroupMembers gm, Trainings tr , Entities e where e.id = tr.id and gm.id=tr.id and gm.MGroup='"+this.Id+"'";
               break;
            case EntityType.course:
               query = @"select gm.mid, gm.id, gm.MGroup , dbo.GetStrContentAlt(c.Name,'RU', 'EN') as CName, c.Code , e.Type 
                  from GroupMembers gm, Courses c , Entities e where e.id = c.id and gm.id=c.id and gm.MGroup='"+this.Id+"'";
               break;
         }

         MembersDataView = new DataView();
         ReloadContent();
         this.OnUpdateDataSet += new EnumDataSetsHandler(UpdateMembers);
         this.OnReloadContent += new ReloadContentHandler(ReloadContent);
      }

      protected override void InitNewRecord()
      {
         this.EditRow["id"] = System.Guid.NewGuid();
      }

      public void ReloadContent()
      {
         MembersDataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(query,"GroupMembers");
         MembersDataView.Table = MembersDataSet.Tables["GroupMembers"];
      }
      
      bool UpdateMembers(string TransactionId, ref DCEAccessLib.DataSetBatchUpdate dataSet)
      {
         dataSet.sql = "Select * from GroupMembers where MGroup='" + this.Id +"'";
         dataSet.tableName = "GroupMembers";
         // removing unused columns
         DataColumnCollection cols = MembersDataSet.Tables["GroupMembers"].Columns;

         cols.Remove("Type");
         switch (this.MembersType)
         {
            case EntityType.training:
               // training
               cols.Remove("TrName");
               cols.Remove("Code");
               break;
            case EntityType.course:
               // course
               cols.Remove("CName");
               cols.Remove("Code");
               break;
            default:
               cols.Remove("FirstName");
               cols.Remove("LastName");
               cols.Remove("Patronymic");
               cols.Remove("GName");
               break;
         }

         dataSet.dataSet = MembersDataSet;
         return true;
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new GroupEdit(this);
         }
         return this.fControl;
      }

      /// <summary>
      /// Название ноды
      /// </summary>
      /// <returns></returns>
      public override String GetCaption()
      {
         if (this.AllowNew && this.EditRow!=null)
         {
            if (this.IsNew)
               return "[Новая группа]";
            return this.EditRow["Name"].ToString();
         }  
         return  GCaption;
      }
      public override void ReleaseControl()
      {}

      public override bool CanClose()
      {
         return this.AllowNew;
      }
   }
}
