using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCECourseEditor
{
   public class CurrencyEditNode : RecordEditNode
   {
      public CurrencyEditNode(NodeControl aParent, string currencyId, string currencyName)
         : base(aParent, "select * from dbo.Currency", "Currency", "id", currencyId)
      {
         this.currencyId = currencyId;
         this.currencyName = currencyName;

         this.CaptionChanged();
      }
      
      private string currencyName;
      public string CurrencyName
      {
         get 
         { 
            return currencyName; 
         }
         set
         {
            currencyName = value;
            this.CaptionChanged();
         }
      }
      private string currencyId;

      public override bool HaveChildNodes() 
      { 
         // ������������ ����� ��������� ����� ������� ����� 
         // ������ ����� ����������� ���� �� ����� ������ �����

         return false; 
      }
      
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new CurrencyEdit(this);
         }
         if (needRefresh)
         {
            ((CurrencyEdit)this.fControl).RefreshData();
            needRefresh = false;
         }

         return this.fControl;
      }

      public override string GetCaption()
      {
         if (this.IsNew)
         {
            return "[����� ������]";
         }
         else
         {
            return currencyName;
         }
      }

      public override void ReleaseControl()
      {
         // do nothing
      }
      
      public override bool CanClose()
      {
         // ���� "������" ��������� � ������������ �����
         
         return true;
      }

      /// <summary>
      /// Initialize new record for editing (EditRow)
      /// </summary>
      protected override void InitNewRecord()
      {
         // filling all non-nullable fields
         EditRow ["id"] = System.Guid.NewGuid().ToString();
         EditRow ["Name"] = System.Guid.NewGuid().ToString();
      }
   }

   /// <summary>
	/// Summary description for CurrencyEdit.
	/// </summary>
	public class CurrencyEdit : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.Panel ButtonPanel;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Panel LangPanel;
      private DCEAccessLib.LangSwitcher LangSwitcher;
      private System.Windows.Forms.Panel MainPanel;
      private DCEAccessLib.MLEdit NameBox;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label1;

      private CurrencyEditNode Node;

		public CurrencyEdit(CurrencyEditNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			Node = node;

         NameBox.SetParentNode(Node);

         this.Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(RebindControls);

         this.RebindControls();
		}
      
      public void RebindControls()
      {
         NameBox.DataBindings.Clear();
         NameBox.DataBindings.Add("eid", Node.EditRow, "Name");
      }

      public void RefreshData()
      {
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
         this.ButtonPanel = new System.Windows.Forms.Panel();
         this.SaveButton = new System.Windows.Forms.Button();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.LangPanel = new System.Windows.Forms.Panel();
         this.LangSwitcher = new DCEAccessLib.LangSwitcher();
         this.MainPanel = new System.Windows.Forms.Panel();
         this.NameBox = new DCEAccessLib.MLEdit();
         this.label3 = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.ButtonPanel.SuspendLayout();
         this.LangPanel.SuspendLayout();
         this.MainPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // ButtonPanel
         // 
         this.ButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ButtonPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.SaveButton,
                                                                                  this.buttonCancel});
         this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.ButtonPanel.Location = new System.Drawing.Point(0, 236);
         this.ButtonPanel.Name = "ButtonPanel";
         this.ButtonPanel.Size = new System.Drawing.Size(560, 44);
         this.ButtonPanel.TabIndex = 11;
         // 
         // SaveButton
         // 
         this.SaveButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SaveButton.Location = new System.Drawing.Point(350, 6);
         this.SaveButton.Name = "SaveButton";
         this.SaveButton.Size = new System.Drawing.Size(96, 28);
         this.SaveButton.TabIndex = 4;
         this.SaveButton.Text = "���������";
         this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
         // 
         // buttonCancel
         // 
         this.buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.buttonCancel.Location = new System.Drawing.Point(454, 6);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(96, 28);
         this.buttonCancel.TabIndex = 3;
         this.buttonCancel.Text = "��������";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // LangPanel
         // 
         this.LangPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.LangPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.LangSwitcher});
         this.LangPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.LangPanel.Name = "LangPanel";
         this.LangPanel.Size = new System.Drawing.Size(560, 40);
         this.LangPanel.TabIndex = 12;
         // 
         // LangSwitcher
         // 
         this.LangSwitcher.Location = new System.Drawing.Point(8, 8);
         this.LangSwitcher.Name = "LangSwitcher";
         this.LangSwitcher.Size = new System.Drawing.Size(172, 24);
         this.LangSwitcher.TabIndex = 1;
         this.LangSwitcher.TextLabel = "����";
         // 
         // MainPanel
         // 
         this.MainPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.NameBox,
                                                                                this.label3,
                                                                                this.label1});
         this.MainPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.MainPanel.Location = new System.Drawing.Point(0, 40);
         this.MainPanel.Name = "MainPanel";
         this.MainPanel.Size = new System.Drawing.Size(560, 64);
         this.MainPanel.TabIndex = 175;
         // 
         // NameBox
         // 
         this.NameBox.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.NameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.NameBox.CaptionLabel = null;
         this.NameBox.DataType = DCEAccessLib.dataFieldType.nvarchar255;
         this.NameBox.eId = null;
         this.NameBox.EntType = DCEAccessLib.ContentType._string;
         this.NameBox.LanguageSwitcher = this.LangSwitcher;
         this.NameBox.Location = new System.Drawing.Point(132, 32);
         this.NameBox.MaxLength = 255;
         this.NameBox.Name = "NameBox";
         this.NameBox.Size = new System.Drawing.Size(420, 20);
         this.NameBox.TabIndex = 176;
         this.NameBox.Text = "";
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(8, 28);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(120, 28);
         this.label3.TabIndex = 174;
         this.label3.Text = "�������� ������";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label1
         // 
         this.label1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label1.Dock = System.Windows.Forms.DockStyle.Top;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label1.ForeColor = System.Drawing.SystemColors.Info;
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(560, 20);
         this.label1.TabIndex = 173;
         this.label1.Text = "��������";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // CurrencyEdit
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.MainPanel,
                                                                      this.LangPanel,
                                                                      this.ButtonPanel});
         this.Name = "CurrencyEdit";
         this.Size = new System.Drawing.Size(560, 280);
         this.ButtonPanel.ResumeLayout(false);
         this.LangPanel.ResumeLayout(false);
         this.MainPanel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         string Name;
         if (NameBox.GetLangContentString(1, out Name))
            Node.CurrencyName = Name;        
         
         Node.EndEdit(false, false);
      }

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         Node.EndEdit(true, Node.IsNew);
      }
	}
}
