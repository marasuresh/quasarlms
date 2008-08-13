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
	/// Редакторивание трека тренингов
	/// </summary>
	public class TrackEdit : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private DCEAccessLib.LangSwitcher langSwitcher1;
      private System.Windows.Forms.Label TitleLabel;
      private System.Windows.Forms.Panel panel1;
      private DCEAccessLib.MLEdit CComment;
      private DCEAccessLib.MLEdit CName;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Button OkButton;
      private System.Windows.Forms.Button CancelButton;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;

      protected TrackEditControl Node;
		public TrackEdit(TrackEditControl node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         Node = node;
         this.CName.SetParentNode(Node);
         this.CComment.SetParentNode(Node);
         this.Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(RebindControls);
         RebindControls();
         if (DCEUser.CurrentUser.Trainings != DCEUser.Access.Modify)
         {
            this.OkButton.Enabled = false;
         }
      }

      public void RebindControls()
      {
         CName.DataBindings.Clear();
         CName.DataBindings.Add("eId",Node.EditRow,"Name");
         CComment.DataBindings.Clear();
         CComment.DataBindings.Add("eId",Node.EditRow,"Description");
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
			this.langSwitcher1 = new DCEAccessLib.LangSwitcher();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.CComment = new DCEAccessLib.MLEdit();
			this.CName = new DCEAccessLib.MLEdit();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// langSwitcher1
			// 
			this.langSwitcher1.Dock = System.Windows.Forms.DockStyle.Top;
			this.langSwitcher1.Enabled = global::DCEInternalSystem.Properties.Settings.Default.LangSwitchEnabled;
			this.langSwitcher1.Location = new System.Drawing.Point(0, 0);
			this.langSwitcher1.Name = "langSwitcher1";
			this.langSwitcher1.Size = new System.Drawing.Size(616, 20);
			this.langSwitcher1.TabIndex = 1;
			this.langSwitcher1.TextLabel = "Язык";
			this.langSwitcher1.Visible = global::DCEInternalSystem.Properties.Settings.Default.LangSwitchVisible;
			// 
			// TitleLabel
			// 
			this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
			this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
			this.TitleLabel.Location = new System.Drawing.Point(0, 20);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(616, 20);
			this.TitleLabel.TabIndex = 184;
			this.TitleLabel.Text = "Свойства";
			this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.CComment);
			this.panel1.Controls.Add(this.CName);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 40);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(616, 80);
			this.panel1.TabIndex = 185;
			// 
			// CComment
			// 
			this.CComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.CComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CComment.CaptionLabel = null;
			this.CComment.eId = "";
			this.CComment.LanguageSwitcher = this.langSwitcher1;
			this.CComment.Location = new System.Drawing.Point(112, 32);
			this.CComment.MaxLength = 255;
			this.CComment.Multiline = true;
			this.CComment.Name = "CComment";
			this.CComment.Size = new System.Drawing.Size(496, 44);
			this.CComment.TabIndex = 3;
			// 
			// CName
			// 
			this.CName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.CName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CName.CaptionLabel = null;
			this.CName.eId = "";
			this.CName.LanguageSwitcher = this.langSwitcher1;
			this.CName.Location = new System.Drawing.Point(112, 4);
			this.CName.MaxLength = 255;
			this.CName.Name = "CName";
			this.CName.Size = new System.Drawing.Size(496, 20);
			this.CName.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 16);
			this.label3.TabIndex = 184;
			this.label3.Text = "Описание";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 12);
			this.label2.TabIndex = 183;
			this.label2.Text = "Название";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.OkButton);
			this.panel2.Controls.Add(this.CancelButton);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 284);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(616, 32);
			this.panel2.TabIndex = 188;
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.OkButton.Location = new System.Drawing.Point(410, 2);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(96, 24);
			this.OkButton.TabIndex = 4;
			this.OkButton.Text = "Сохранить";
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CancelButton.Location = new System.Drawing.Point(514, 2);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(96, 24);
			this.CancelButton.TabIndex = 5;
			this.CancelButton.Text = "Отменить";
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Добавить";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Удалить";
			// 
			// TrackEdit
			// 
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.langSwitcher1);
			this.Name = "TrackEdit";
			this.Size = new System.Drawing.Size(616, 316);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

      }
		#endregion

      private void OkButton_Click(object sender, System.EventArgs e)
      {
         // saving changes
         this.Node.EndEdit(false,false);
         Node.CaptionChanged();
         this.Node.CreateChilds();
      }

      private void CancelButton_Click(object sender, System.EventArgs e)
      {
         // canceling changes
         this.Node.EndEdit(true,false);
      }

	}

   public class TrackEditControl : RecordEditNode
   {
      public TrackEditControl(NodeControl parent , string id)
         : base(parent, "select * from Tracks" , "Tracks", "id", id)
      {
         this.CaptionChanged();
         this.Expand();
      }

      public override string GetCaption()
      {
         if (EditRow!=null)
         {
            if (this.IsNew)
               return "[Новый трек]";
            DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("select dbo.GetStrContentAlt('"
               + this.EditRow["Name"].ToString() +"','RU','EN')" , "Name");
            string TrackName;
            if (ds.Tables["Name"].Rows.Count>0)
               TrackName = ds.Tables["Name"].Rows[0][0].ToString();
            else
               TrackName = "";
            return "Трек: " + TrackName;
         }
         return "";
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl==null)
         {
            this.fControl = new TrackEdit(this);
         }
         return this.fControl;
      }

      public override bool HaveChildNodes()
      {
         return true;
      }

      public override bool CanClose()
      {
         return true;
      }

      public override void CreateChilds()
      {
         if (!this.IsNew)
         {
            if (this.Nodes.Count==0)
            {
               new GroupEditNode(this,this.EditRow["Students"].ToString(),
                  EntityType.student,false,"Студенты","",false,false);
               new GroupEditNode(this,this.EditRow["Trainings"].ToString(),
                  EntityType.training,true,"Тренинги","",false,false);
            }
         }
      }

      public override void ReleaseControl()
      {

      }

      protected override void InitNewRecord()
      {
         this.EditRow["Students"] = System.Guid.NewGuid();
         this.EditRow["id"] = System.Guid.NewGuid();
         this.EditRow["Trainings"] = System.Guid.NewGuid();
         this.EditRow["Name"] = System.Guid.NewGuid();
         this.EditRow["Description"] = System.Guid.NewGuid();
      }
   }
}
