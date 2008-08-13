using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DCEAccessLib;
using System.Threading;

namespace DCECourseEditor
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.StatusBar StatusBar;
      private System.Windows.Forms.Panel TopPanel;
      private System.Windows.Forms.Panel MainPanel;
      private System.Windows.Forms.TreeView TreeView;
      private System.Windows.Forms.Splitter MainSplitter;
      private System.Windows.Forms.MainMenu MainMenu;
      private System.Windows.Forms.MenuItem ExitMenuItem;
      private System.Windows.Forms.UserControl fCurrentControl = null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.Button CloseButton;
      private System.Windows.Forms.Panel ContainerPanel;
      private System.Windows.Forms.Panel ControlsPanel;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItemSettings;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.ContextMenu TreeviewContextMenu;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Label TitleLabel;

      private RootNode root;
      
      // forces comboboxes and checkboxes to 
      // update binding data when checked or selection changed
      private void OnNeedValidate(object sender, System.EventArgs e)
      {
         if (this.fCurrentControl !=null)
         {
            this.fCurrentControl.Validate();
         }
      }
//      public void OnNeedValidate_Key( object sender, KeyEventArgs e )
//      {
//         if (this.fCurrentControl !=null)
//         {
//            if (sender is System.Windows.Forms.NumericUpDown)
//            {
//            
//            }
//            this.fCurrentControl.Validate();
//         }
//      }

      private System.EventHandler Validate_Handler;

      private void AttachValidateEvent(System.Windows.Forms.Control ctl)
      {
         foreach (System.Windows.Forms.Control ictl in ctl.Controls)
         {
            AttachValidateEvent(ictl);
         }
         if (ctl is System.Windows.Forms.CheckBox)
         {
            ((System.Windows.Forms.CheckBox) ctl).CheckStateChanged += Validate_Handler;
         }
         if (ctl is System.Windows.Forms.ComboBox)
         {
            ((System.Windows.Forms.ComboBox) ctl).SelectedIndexChanged += Validate_Handler;
         }
         if (ctl is System.Windows.Forms.NumericUpDown)
         {
            ((System.Windows.Forms.NumericUpDown) ctl).ValueChanged += Validate_Handler;
//            ((System.Windows.Forms.NumericUpDown) ctl).KeyDown += new System.Windows.Forms.KeyEventHandler(OnNeedValidate_Key);
         }
      }

      public MainForm()
      {
         //
         // Required for Windows Form Designer support
			//
			InitializeComponent();

         NodeControl.NodeLabel = this.TitleLabel;
         
         Validate_Handler = new System.EventHandler(this.OnNeedValidate);
         CreateRootNode();
      }

      protected void CreateRootNode()
      {
         if (root != null)
            root.Dispose();
         
         this.TreeView.Nodes.Clear();

         root = new RootNode(this.TreeView);
         root.Select();
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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
         this.MainMenu = new System.Windows.Forms.MainMenu();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.ExitMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItemSettings = new System.Windows.Forms.MenuItem();
         this.StatusBar = new System.Windows.Forms.StatusBar();
         this.TopPanel = new System.Windows.Forms.Panel();
         this.MainPanel = new System.Windows.Forms.Panel();
         this.ContainerPanel = new System.Windows.Forms.Panel();
         this.ControlsPanel = new System.Windows.Forms.Panel();
         this.panel1 = new System.Windows.Forms.Panel();
         this.CloseButton = new System.Windows.Forms.Button();
         this.TitleLabel = new System.Windows.Forms.Label();
         this.MainSplitter = new System.Windows.Forms.Splitter();
         this.TreeView = new System.Windows.Forms.TreeView();
         this.TreeviewContextMenu = new System.Windows.Forms.ContextMenu();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.MainPanel.SuspendLayout();
         this.ContainerPanel.SuspendLayout();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // MainMenu
         // 
         this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuItem2,
                                                                                 this.menuItem1});
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 0;
         this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                  this.ExitMenuItem});
         this.menuItem2.Text = "Файл";
         // 
         // ExitMenuItem
         // 
         this.ExitMenuItem.Index = 0;
         this.ExitMenuItem.Text = "Выход";
         this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 1;
         this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                  this.menuItemSettings});
         this.menuItem1.Text = "Сервис";
         // 
         // menuItemSettings
         // 
         this.menuItemSettings.Index = 0;
         this.menuItemSettings.Text = "Настройки";
         this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
         // 
         // StatusBar
         // 
         this.StatusBar.Location = new System.Drawing.Point(0, 731);
         this.StatusBar.Name = "StatusBar";
         this.StatusBar.Size = new System.Drawing.Size(1167, 10);
         this.StatusBar.TabIndex = 0;
         // 
         // TopPanel
         // 
         this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.TopPanel.Name = "TopPanel";
         this.TopPanel.Size = new System.Drawing.Size(1167, 14);
         this.TopPanel.TabIndex = 1;
         // 
         // MainPanel
         // 
         this.MainPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.ContainerPanel,
                                                                                this.MainSplitter,
                                                                                this.TreeView});
         this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.MainPanel.Location = new System.Drawing.Point(0, 14);
         this.MainPanel.Name = "MainPanel";
         this.MainPanel.Size = new System.Drawing.Size(1167, 717);
         this.MainPanel.TabIndex = 2;
         // 
         // ContainerPanel
         // 
         this.ContainerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ContainerPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                     this.ControlsPanel,
                                                                                     this.panel1});
         this.ContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.ContainerPanel.Location = new System.Drawing.Point(407, 0);
         this.ContainerPanel.Name = "ContainerPanel";
         this.ContainerPanel.Size = new System.Drawing.Size(760, 717);
         this.ContainerPanel.TabIndex = 2;
         // 
         // ControlsPanel
         // 
         this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.ControlsPanel.Location = new System.Drawing.Point(0, 44);
         this.ControlsPanel.Name = "ControlsPanel";
         this.ControlsPanel.Size = new System.Drawing.Size(758, 671);
         this.ControlsPanel.TabIndex = 9;
         // 
         // panel1
         // 
         this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
         this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.CloseButton,
                                                                             this.TitleLabel});
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(758, 44);
         this.panel1.TabIndex = 10;
         // 
         // CloseButton
         // 
         this.CloseButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
         this.CloseButton.BackColor = System.Drawing.SystemColors.Control;
         this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
         this.CloseButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
         this.CloseButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
         this.CloseButton.Location = new System.Drawing.Point(722, 7);
         this.CloseButton.Name = "CloseButton";
         this.CloseButton.Size = new System.Drawing.Size(31, 30);
         this.CloseButton.TabIndex = 8;
         this.CloseButton.TabStop = false;
         this.CloseButton.Text = "×";
         this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
         // 
         // TitleLabel
         // 
         this.TitleLabel.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
         this.TitleLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
         this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
         this.TitleLabel.Location = new System.Drawing.Point(5, 2);
         this.TitleLabel.Name = "TitleLabel";
         this.TitleLabel.Size = new System.Drawing.Size(712, 40);
         this.TitleLabel.TabIndex = 7;
         this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // MainSplitter
         // 
         this.MainSplitter.Location = new System.Drawing.Point(398, 0);
         this.MainSplitter.Name = "MainSplitter";
         this.MainSplitter.Size = new System.Drawing.Size(9, 717);
         this.MainSplitter.TabIndex = 1;
         this.MainSplitter.TabStop = false;
         // 
         // TreeView
         // 
         this.TreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TreeView.ContextMenu = this.TreeviewContextMenu;
         this.TreeView.Dock = System.Windows.Forms.DockStyle.Left;
         this.TreeView.HideSelection = false;
         this.TreeView.ImageIndex = -1;
         this.TreeView.Name = "TreeView";
         this.TreeView.SelectedImageIndex = -1;
         this.TreeView.Size = new System.Drawing.Size(398, 717);
         this.TreeView.TabIndex = 0;
         this.TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
         this.TreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeSelect);
         this.TreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeExpand);
         // 
         // TreeviewContextMenu
         // 
         this.TreeviewContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                            this.menuItem3});
         this.TreeviewContextMenu.Popup += new System.EventHandler(this.TreeviewContextMenu_Popup);
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 0;
         this.menuItem3.Text = "!!!";
         // 
         // MainForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
         this.ClientSize = new System.Drawing.Size(1167, 741);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.MainPanel,
                                                                      this.TopPanel,
                                                                      this.StatusBar});
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Menu = this.MainMenu;
         this.Name = "MainForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = " Конструктор курсов";
         this.MainPanel.ResumeLayout(false);
         this.ContainerPanel.ResumeLayout(false);
         this.panel1.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      [STAThread]
      static void Main() 
      {
         Application.ThreadException += new ThreadExceptionEventHandler(DCEException.OnThreadException);

         DCEAccessLib.AuthenticationForm f = new DCEAccessLib.AuthenticationForm();

         while (f.ShowDialog() == DialogResult.OK)
         {
            if (DCEUser.CurrentUser.Courses != DCEUser.Access.No)
            {
               DCEAccessLib.Settings.LoadSettings();
               Application.Run(new MainForm());
               return;
            }
            else
            {
               MessageBox.Show("У вас нет прав на просмотр или изменение содержимого курсов.");
            }
         }
      }

      private void ExitMenuItem_Click(object sender, System.EventArgs e)
      {
         this.Close();
         this.Dispose();
      }

      private void TreeView_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
      {
         if (e.Node.Tag != null)
            ((NodeControl)e.Node.Tag).Expand();
      }

      private void TreeView_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
      {
         if (e.Node.Tag != null) 
         {
            NodeControl node = ((NodeControl)e.Node.Tag);
            if (NodeControl.SelectedNode != null)
            {
               if ( node != NodeControl.SelectedNode)
               {
                  if (this.fCurrentControl != null)
                  {
                     this.ControlsPanel.Controls.Remove(this.fCurrentControl);
                     this.fCurrentControl = null;
                  }
                  node.ReleaseControl();
               }
            }
            NodeControl.SelectedNode = null;
         }
      }

      private void TreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
      {
         if (this.TreeView.SelectedNode.Tag != null)
         {
            NodeControl.SelectedNode = ((NodeControl)this.TreeView.SelectedNode.Tag);
            this.fCurrentControl = NodeControl.SelectedNode.GetControl();
            if (this.fCurrentControl !=null)
            {
               this.fCurrentControl.Dock = DockStyle.Fill;
               this.ControlsPanel.Controls.Add(this.fCurrentControl);
               this.AttachValidateEvent(this.fCurrentControl);
               this.fCurrentControl.Show();
            }
            if (NodeControl.SelectedNode != null)
            {
               this.CloseButton.Visible = NodeControl.SelectedNode.CanClose();
            }
         }
      }

      private void CloseButton_Click(object sender, System.EventArgs e)
      {
         if (NodeControl.SelectedNode !=null)
         {
            NodeControl.SelectedNode.Dispose();
         }
      }

      private void menuItemSettings_Click(object sender, System.EventArgs e)
      {
         DCECourseEditor.Settings sDialog = new DCECourseEditor.Settings();
         if (DialogResult.OK == sDialog.ShowDialog(this))
         {
            CreateRootNode();
         }
      }

      private void TreeviewContextMenu_Popup(object sender, System.EventArgs e)
      {
         this.TreeviewContextMenu.MenuItems.Clear();

         Point p = this.TreeView.PointToClient(Cursor.Position);
        
         TreeNode treenode = this.TreeView.GetNodeAt(p);

         if (treenode != null && treenode.Tag != null)
         {
            NodeControl node = treenode.Tag as NodeControl;
            ArrayList coll = node.GetPopupMenu();

            if (coll != null)
            {
               foreach(MenuItem item in coll)
               {
                  this.TreeviewContextMenu.MenuItems.Add(item.CloneMenu());
               }
            }
         }
      }

      private void menuItemQuestionnaire_Click(object sender, System.EventArgs e)
      {
         MessageBox.Show("menuItemQuestionnaire_Click");
      }

      private void TreeView_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
      {
         MessageBox.Show("TreeView_DragDrop\n" + e.ToString());
      }

      private void TreeView_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
      {
         //MessageBox.Show("TreeView_DragOver\n" + e.ToString());
         e.Effect = DragDropEffects.Move;
      }

      private void TreeView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
      {
         //MessageBox.Show("TreeView_ItemDrag\n" + e.ToString());
         this.DoDragDrop(this, DragDropEffects.Move);
      }

      private void TreeView_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
      {
         //MessageBox.Show("TreeView_DragEnter");
      }

      private void TreeView_QueryContinueDrag(object sender, System.Windows.Forms.QueryContinueDragEventArgs e)
      {
         //MessageBox.Show("TreeView_QueryContinueDrag");
      }

      private void TreeView_DragLeave(object sender, System.EventArgs e)
      {
         //MessageBox.Show("TreeView_DragLeave");
      }
   }
}
