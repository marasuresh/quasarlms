using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using DCEAccessLib;

namespace DCEInternalSystem
{

	/// <summary>
	/// Главная форма
	/// </summary>
	/// 
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.StatusBar MainStatusBar;
		private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.MainMenu MainMenu;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Panel, which showing in right side of the form
		/// </summary>
		private System.Windows.Forms.UserControl fCurrentControl = null;
		private System.Windows.Forms.ContextMenu CurrentContextMenu;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItemSettings;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Panel ClientPanel;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.TreeView TreeView;
		private System.Windows.Forms.Panel TitlePanel;
		private System.Windows.Forms.Panel ControlsPanel;
		private System.Windows.Forms.Label TitleLabel;
		private System.Windows.Forms.Button CloseButton;

		private RootNode root;
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Создание нод дерева и соответствующих им объектов
			NodeControl.NodeLabel = this.TitleLabel;
			root = new RootNode(this.TreeView);
			root.Expand();
			this.CloseButton.Text = "\u00d7";
			this.Text = "Система дистанционного обучения [" + DCEUser.CurrentUser.Login + "]";
			Validate_Handler = new System.EventHandler(this.OnNeedValidate);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (root != null) {
				root.Dispose();
				root = null;
			}
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
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
			this.menuItem20 = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemSettings = new System.Windows.Forms.MenuItem();
			this.MainStatusBar = new System.Windows.Forms.StatusBar();
			this.MainPanel = new System.Windows.Forms.Panel();
			this.ClientPanel = new System.Windows.Forms.Panel();
			this.ControlsPanel = new System.Windows.Forms.Panel();
			this.TitlePanel = new System.Windows.Forms.Panel();
			this.CloseButton = new System.Windows.Forms.Button();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.TreeView = new System.Windows.Forms.TreeView();
			this.CurrentContextMenu = new System.Windows.Forms.ContextMenu();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.MainPanel.SuspendLayout();
			this.ClientPanel.SuspendLayout();
			this.TitlePanel.SuspendLayout();
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
                                                                                  this.menuItem20});
			this.menuItem2.Text = "Файл";
			// 
			// menuItem20
			// 
			this.menuItem20.Index = 0;
			this.menuItem20.Text = "Выход";
			this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
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
			// MainStatusBar
			// 
			this.MainStatusBar.Location = new System.Drawing.Point(0, 474);
			this.MainStatusBar.Name = "MainStatusBar";
			this.MainStatusBar.Size = new System.Drawing.Size(1052, 50);
			this.MainStatusBar.TabIndex = 8;
			// 
			// MainPanel
			// 
			this.MainPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.ClientPanel});
			this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(1052, 474);
			this.MainPanel.TabIndex = 9;
			// 
			// ClientPanel
			// 
			this.ClientPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.ControlsPanel,
                                                                                  this.TitlePanel,
                                                                                  this.splitter1,
                                                                                  this.TreeView});
			this.ClientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ClientPanel.Name = "ClientPanel";
			this.ClientPanel.Size = new System.Drawing.Size(1052, 474);
			this.ClientPanel.TabIndex = 1;
			this.ClientPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ClientPanel_Paint);
			// 
			// ControlsPanel
			// 
			this.ControlsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ControlsPanel.Location = new System.Drawing.Point(337, 30);
			this.ControlsPanel.Name = "ControlsPanel";
			this.ControlsPanel.Size = new System.Drawing.Size(715, 444);
			this.ControlsPanel.TabIndex = 19;
			// 
			// TitlePanel
			// 
			this.TitlePanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.CloseButton,
                                                                                 this.TitleLabel});
			this.TitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TitlePanel.Location = new System.Drawing.Point(337, 0);
			this.TitlePanel.Name = "TitlePanel";
			this.TitlePanel.Size = new System.Drawing.Size(715, 30);
			this.TitlePanel.TabIndex = 18;
			// 
			// CloseButton
			// 
			this.CloseButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.CloseButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.CloseButton.Location = new System.Drawing.Point(681, 5);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(26, 23);
			this.CloseButton.TabIndex = 11;
			this.CloseButton.TabStop = false;
			this.CloseButton.Text = "?";
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// TitleLabel
			// 
			this.TitleLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(715, 30);
			this.TitleLabel.TabIndex = 10;
			this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(326, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(11, 474);
			this.splitter1.TabIndex = 12;
			this.splitter1.TabStop = false;
			// 
			// TreeView
			// 
			this.TreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TreeView.ContextMenu = this.CurrentContextMenu;
			this.TreeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.TreeView.FullRowSelect = true;
			this.TreeView.HideSelection = false;
			this.TreeView.ImageIndex = -1;
			this.TreeView.Name = "TreeView";
			this.TreeView.SelectedImageIndex = -1;
			this.TreeView.Size = new System.Drawing.Size(326, 474);
			this.TreeView.TabIndex = 199;
			this.TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
			this.TreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeSelect);
			this.TreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeExpand);
			// 
			// CurrentContextMenu
			// 
			this.CurrentContextMenu.Popup += new System.EventHandler(this.CurrentContextMenu_Popup);
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.Popup += new System.EventHandler(this.MainContextMenu_Popup);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(1052, 524);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.MainPanel,
                                                                      this.MainStatusBar});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.MainMenu;
			this.Name = "MainForm";
			this.Text = "Система дистанционного обучения [admin]";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.MainPanel.ResumeLayout(false);
			this.ClientPanel.ResumeLayout(false);
			this.TitlePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.ThreadException += new ThreadExceptionEventHandler(DCEException.OnThreadException);
			DCEAccessLib.AuthenticationForm f = new DCEAccessLib.AuthenticationForm();

			if (f.ShowDialog() == DialogResult.OK) {
				DCEAccessLib.Settings.LoadSettings();
				Application.Run(new MainForm());
			}
		}

		private void TreeView_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if (e.Node.Tag != null)
				((NodeControl)e.Node.Tag).Expand();
		}

		private void TreeView_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if (e.Node.Tag != null) {
				if (NodeControl.SelectedNode != null) {
					if (((NodeControl)e.Node.Tag) != NodeControl.SelectedNode) {
						if (this.fCurrentControl != null) {
							this.ControlsPanel.Controls.Remove(this.fCurrentControl);
							this.fCurrentControl = null;
							//                     foreach (System.Windows.Forms.Control ctl in this.ClientPanel.Controls)
							//                     {
							//                        if (ctl.GetType() == typeof(System.Windows.Forms.ToolBar))
							//                        {
							//                         
							//                           this.ClientPanel.Controls.Remove(ctl);
							//                           break;
							//                        }
							//                     }
						}
						((NodeControl)e.Node.Tag).ReleaseControl();
					}
				}
				NodeControl.SelectedNode = null;
			}
		}


		// forces comboboxes and checkboxes to 
		// update binding data when checked or selection changed
		private void OnNeedValidate(object sender, System.EventArgs e)
		{
			if (this.fCurrentControl != null) {
				this.fCurrentControl.Validate();
			}
		}

		private System.EventHandler Validate_Handler;

		private void AttachValidateEvent(System.Windows.Forms.Control ctl)
		{
			foreach (System.Windows.Forms.Control ictl in ctl.Controls) {
				AttachValidateEvent(ictl);
			}
			if (ctl is System.Windows.Forms.CheckBox) {
				((System.Windows.Forms.CheckBox)ctl).CheckStateChanged += Validate_Handler;
			}
			if (ctl is System.Windows.Forms.ComboBox) {
				((System.Windows.Forms.ComboBox)ctl).SelectedIndexChanged += Validate_Handler;
			}
			if (ctl is System.Windows.Forms.NumericUpDown) {
				((System.Windows.Forms.NumericUpDown)ctl).ValueChanged += Validate_Handler;
			}
		}

		private void TreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (this.TreeView.SelectedNode.Tag != null) {
				NodeControl.SelectedNode = ((NodeControl)this.TreeView.SelectedNode.Tag);
				this.fCurrentControl = NodeControl.SelectedNode.GetControl();
				if (this.fCurrentControl != null) {
					//               foreach (System.Windows.Forms.Control ctl in this.fCurrentControl.Controls)
					//               {
					//                  if (ctl.GetType() == typeof(System.Windows.Forms.ToolBar))
					//                  {
					//                     ctl.Dock = DockStyle.Top;
					//                     this.ClientPanel.Controls.Add(ctl);
					//                     this.ClientPanel.Controls.SetChildIndex(ctl,
					//                        this.ClientPanel.Controls.GetChildIndex(this.TitlePanel)/*+1*/);
					//                     break;
					//                  }
					//               }
					this.fCurrentControl.Dock = DockStyle.Fill;
					this.ControlsPanel.Controls.Add(this.fCurrentControl);
					this.AttachValidateEvent(this.fCurrentControl);
					this.fCurrentControl.Show();

				}
				if (NodeControl.SelectedNode != null) {
					this.CloseButton.Visible = NodeControl.SelectedNode.CanClose();
				} else
					this.TitleLabel.Text = "";
			}
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{

		}

		private void MainContextMenu_Popup(object sender, System.EventArgs e)
		{
			//         foreach (System.Windows.Forms.MenuItem item in this.MainContextMenu.MenuItems)
			//         {
			//            if (item.IsParent)
			//            {
			//               System.Windows.Forms.TreeNode node = this.TreeView.SelectedNode;
			//               if (node.Tag != null)
			//               {
			//                  this.CurrentContextMenu.MenuItems.Clear();
			//                  System.Windows.Forms.ContextMenu contextmenu = ((NodeControl)node.Tag).GetPopupMenu();
			//                  
			//                  this.CurrentContextMenu.MenuItems.a;
			//               }
			//            }
			//         }
		}

		private void TreeView_Click(object sender, System.EventArgs e)
		{
			MouseButtons buttons = System.Windows.Forms.Control.MouseButtons;
		}

		private void TreeView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			MouseButtons buttons = e.Button;
		}

		private void TreeView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			MouseButtons buttons = e.Button;
		}

		private void menuItem21_Click(object sender, System.EventArgs e)
		{

		}

		private void CurrentContextMenu_Popup(object sender, System.EventArgs e)
		{
			this.CurrentContextMenu.MenuItems.Clear();

			foreach (System.Object item in this.MainContextMenu.MenuItems) {
				this.CurrentContextMenu.MenuItems.Add(((System.Windows.Forms.MenuItem)item).CloneMenu());
			}
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите выйти?","Выход",MessageBoxButtons.YesNo) == DialogResult.Yes)
			e.Cancel = false;
			//else
			//            e.Cancel = true;
			Dispose();
		}

		private void menuItem20_Click(object sender, System.EventArgs e)
		{
			this.Close();
			this.Dispose();
		}

		private void ControlsPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{

		}

		private void ClientPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{

		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			if (NodeControl.SelectedNode != null) {
				NodeControl.SelectedNode.Dispose();
			}
		}

		private void menuItemSettings_Click(object sender, System.EventArgs e)
		{/*
			SettingsDialog sDialog = new SettingsDialog();
			if (DialogResult.OK == sDialog.ShowDialog(this)) {
				root.Dispose();
				this.TreeView.Nodes.Clear();
				root = new RootNode(this.TreeView);
				root.Expand();
			}*/
		}

		private void MainToolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{

		}
	}
}
