using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCECourseEditor
{
   public class TypeEditNode : RecordEditNode
   {
      public TypeEditNode(NodeControl aParent, string typeId, string typeName)
         : base(aParent, "select * from dbo.CourseType", "CourseType", "id", typeId)
      {
         this.typeId = typeId;
         this.typeName = typeName;

         this.CaptionChanged();
      }
      
      private string typeName;
      public string TypeName
      {
         get 
         { 
            return typeName; 
         }
         set
         {
            typeName = value;
            this.CaptionChanged();
         }
      }
      private string typeId;

      public override bool HaveChildNodes() 
      { 
         // Пользователь может открывать форму свойств типов 
         // только через контекстное меню на форме списка анкет

         return false; 
      }
      
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new TypeEdit(this);
         }
         if (needRefresh)
         {
            ((TypeEdit)this.fControl).RefreshData();
            needRefresh = false;
         }

         return this.fControl;
      }

      public override string GetCaption()
      {
         if (this.IsNew)
         {
            return "[Новый тип]";
         }
         else
         {
            return typeName;
         }
      }

      public override void ReleaseControl()
      {
         // do nothing
      }
      
      public override bool CanClose()
      {
         // Нода "Анкета" относится к динамическим нодам
         
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
	/// Тип курса
	/// </summary>
	public class TypeEdit : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.Panel MainPanel;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Panel ButtonPanel;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Panel LangPanel;
      private DCEAccessLib.LangSwitcher LangSwitcher;
      private DCEAccessLib.MLEdit NameBox;

      private TypeEditNode Node;

		public TypeEdit(TypeEditNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			Node = node;

         this.SaveButton.Enabled = DCEUser.CurrentUser.EditableCourses;

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
         this.MainPanel = new System.Windows.Forms.Panel();
         this.NameBox = new DCEAccessLib.MLEdit();
         this.LangSwitcher = new DCEAccessLib.LangSwitcher();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.ButtonPanel = new System.Windows.Forms.Panel();
         this.SaveButton = new System.Windows.Forms.Button();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.LangPanel = new System.Windows.Forms.Panel();
         this.MainPanel.SuspendLayout();
         this.ButtonPanel.SuspendLayout();
         this.LangPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // MainPanel
         // 
         this.MainPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.NameBox,
                                                                                this.label3,
                                                                                this.label2});
         this.MainPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.MainPanel.Location = new System.Drawing.Point(0, 40);
         this.MainPanel.Name = "MainPanel";
         this.MainPanel.Size = new System.Drawing.Size(544, 64);
         this.MainPanel.TabIndex = 9;
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
         this.NameBox.Size = new System.Drawing.Size(404, 20);
         this.NameBox.TabIndex = 176;
         this.NameBox.Text = "";
         // 
         // LangSwitcher
         // 
         this.LangSwitcher.Location = new System.Drawing.Point(8, 8);
         this.LangSwitcher.Name = "LangSwitcher";
         this.LangSwitcher.Size = new System.Drawing.Size(172, 24);
         this.LangSwitcher.TabIndex = 1;
         this.LangSwitcher.TextLabel = "Язык";
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(8, 28);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(120, 28);
         this.label3.TabIndex = 174;
         this.label3.Text = "Название типа";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // label2
         // 
         this.label2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label2.Dock = System.Windows.Forms.DockStyle.Top;
         this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label2.ForeColor = System.Drawing.SystemColors.Info;
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(544, 20);
         this.label2.TabIndex = 173;
         this.label2.Text = "Основные";
         this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // ButtonPanel
         // 
         this.ButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.ButtonPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.SaveButton,
                                                                                  this.buttonCancel});
         this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.ButtonPanel.Location = new System.Drawing.Point(0, 188);
         this.ButtonPanel.Name = "ButtonPanel";
         this.ButtonPanel.Size = new System.Drawing.Size(544, 44);
         this.ButtonPanel.TabIndex = 10;
         // 
         // SaveButton
         // 
         this.SaveButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SaveButton.Location = new System.Drawing.Point(334, 6);
         this.SaveButton.Name = "SaveButton";
         this.SaveButton.Size = new System.Drawing.Size(96, 28);
         this.SaveButton.TabIndex = 4;
         this.SaveButton.Text = "Сохранить";
         this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
         // 
         // buttonCancel
         // 
         this.buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.buttonCancel.Location = new System.Drawing.Point(438, 6);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(96, 28);
         this.buttonCancel.TabIndex = 3;
         this.buttonCancel.Text = "Отменить";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // LangPanel
         // 
         this.LangPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.LangPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.LangSwitcher});
         this.LangPanel.Dock = System.Windows.Forms.DockStyle.Top;
         this.LangPanel.Name = "LangPanel";
         this.LangPanel.Size = new System.Drawing.Size(544, 40);
         this.LangPanel.TabIndex = 11;
         // 
         // TypeEdit
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.MainPanel,
                                                                      this.LangPanel,
                                                                      this.ButtonPanel});
         this.Name = "TypeEdit";
         this.Size = new System.Drawing.Size(544, 232);
         this.MainPanel.ResumeLayout(false);
         this.ButtonPanel.ResumeLayout(false);
         this.LangPanel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         string Name;
         if (NameBox.GetLangContentString(1, out Name))
            Node.TypeName = Name;        
         
         Node.EndEdit(false, false);
      }

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         Node.EndEdit(true, Node.IsNew);
      }
	}
}
