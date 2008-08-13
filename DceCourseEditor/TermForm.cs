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
	/// Редактирование термина
	/// </summary>
	public class TermForm : System.Windows.Forms.Form, IEditNode
	{
      private DCEAccessLib.LangSwitcher langSwitcher1;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private DCEAccessLib.MLEdit TermBox;
      private DCEAccessLib.MLEdit TextBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public void ChangeNotify()
      {

      }

      public event RecordEditNode.EnumDataSetsHandler OnUpdateDataSet
      {
         add
         {
            EventTable.Add( value ); 
         }
         remove
         {
            EventTable.Remove( value );
         }
      }
      public event RecordEditNode.ReloadContentHandler OnReloadContent
      {
         add {}
         remove {}
      }

      public TermForm(DataRowView row)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.SaveButton.Enabled = DCEUser.CurrentUser.EditableCourses;

         this.TermBox.SetParentNode(this);
         this.TextBox.SetParentNode(this);

         this.TermBox.DataBindings.Add("eid", row, "Name");
         this.TextBox.DataBindings.Add("eid", row, "Text");
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

      System.Collections.ArrayList EventTable = new System.Collections.ArrayList();

      public DCEAccessLib.DataSetBatchUpdate[] GetDialogDataSets()
      {
         DCEAccessLib.DataSetBatchUpdate dataSet = null;

         System.Collections.ArrayList  updates = new  System.Collections.ArrayList();

         foreach (RecordEditNode.EnumDataSetsHandler handler in EventTable)
         {
            dataSet = new DCEAccessLib.DataSetBatchUpdate();
            if (handler("", ref dataSet))
            {
               updates.Add(dataSet);               
            }
         }
         
         DCEAccessLib.DataSetBatchUpdate[] ret = new DCEAccessLib.DataSetBatchUpdate[updates.Count];
         for(int i=0; i<updates.Count; i++)
         {
            ret[i] = (DCEAccessLib.DataSetBatchUpdate)updates[i];
         }
         
         return ret;
      }

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.langSwitcher1 = new DCEAccessLib.LangSwitcher();
         this.TermBox = new DCEAccessLib.MLEdit();
         this.TextBox = new DCEAccessLib.MLEdit();
         this.SaveButton = new System.Windows.Forms.Button();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // langSwitcher1
         // 
         this.langSwitcher1.Location = new System.Drawing.Point(12, 8);
         this.langSwitcher1.Name = "langSwitcher1";
         this.langSwitcher1.Size = new System.Drawing.Size(168, 24);
         this.langSwitcher1.TabIndex = 0;
         this.langSwitcher1.TextLabel = "Язык";
         // 
         // TermBox
         // 
         this.TermBox.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.TermBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TermBox.CaptionLabel = null;
         this.TermBox.DataType = DCEAccessLib.dataFieldType.nvarchar255;
         this.TermBox.eId = null;
         this.TermBox.EntType = DCEAccessLib.ContentType._string;
         this.TermBox.LanguageSwitcher = this.langSwitcher1;
         this.TermBox.Location = new System.Drawing.Point(136, 40);
         this.TermBox.MaxLength = 255;
         this.TermBox.Name = "TermBox";
         this.TermBox.Size = new System.Drawing.Size(428, 20);
         this.TermBox.TabIndex = 1;
         this.TermBox.Text = "";
         // 
         // TextBox
         // 
         this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TextBox.CaptionLabel = null;
         this.TextBox.DataType = DCEAccessLib.dataFieldType.nvarchar255;
         this.TextBox.eId = null;
         this.TextBox.EntType = DCEAccessLib.ContentType._string;
         this.TextBox.LanguageSwitcher = this.langSwitcher1;
         this.TextBox.Location = new System.Drawing.Point(136, 68);
         this.TextBox.MaxLength = 255;
         this.TextBox.Multiline = true;
         this.TextBox.Name = "TextBox";
         this.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.TextBox.Size = new System.Drawing.Size(428, 60);
         this.TextBox.TabIndex = 2;
         this.TextBox.Text = "";
         // 
         // SaveButton
         // 
         this.SaveButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SaveButton.Location = new System.Drawing.Point(368, 134);
         this.SaveButton.Name = "SaveButton";
         this.SaveButton.Size = new System.Drawing.Size(96, 28);
         this.SaveButton.TabIndex = 6;
         this.SaveButton.Text = "Сохранить";
         this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
         // 
         // buttonCancel
         // 
         this.buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.buttonCancel.Location = new System.Drawing.Point(472, 134);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(96, 28);
         this.buttonCancel.TabIndex = 5;
         this.buttonCancel.Text = "Отменить";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(12, 40);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(104, 16);
         this.label1.TabIndex = 7;
         this.label1.Text = "Термин";
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(12, 88);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(104, 20);
         this.label2.TabIndex = 8;
         this.label2.Text = "Описание";
         // 
         // TermForm
         // 
         this.AcceptButton = this.SaveButton;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.buttonCancel;
         this.ClientSize = new System.Drawing.Size(576, 168);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.SaveButton,
                                                                      this.buttonCancel,
                                                                      this.langSwitcher1,
                                                                      this.TextBox,
                                                                      this.label2,
                                                                      this.label1,
                                                                      this.TermBox});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "TermForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Редактирование термина";
         this.ResumeLayout(false);

      }
		#endregion

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         Close();
      }

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         Close();
      }

	}
}
