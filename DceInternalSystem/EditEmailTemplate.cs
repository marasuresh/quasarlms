using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace DCEInternalSystem
{
	/// <summary>
	/// Редактор шаблонов писем
	/// </summary>
	public class EditEmailTemplate : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox templateText;
      private System.Windows.Forms.TextBox NameEdit;
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.Button btnOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EditEmailTemplate(string id, string name, string templatebody)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         if (id=="")
         {
            this.Text = "Новый шаблон";
            this.NameEdit.Text = "Новый шаблон";

            this.templateText.Text = @"
<?xml version=""1.0"" encoding=""windows-1251""?>
<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">
<xsl:output method=""html"" encoding=""windows-1251"" omit-xml-declaration=""yes"" indent=""no""/>
<xsl:template match=""/Request"">
<!--
Значимые поля в шаблоне
FirstName      - имя студента
LastName       - фамилия студента
Patronymic     - отчество студента
FirstNameEng   - имя студента (English)
LastNameEng    - фамилия студента (English)
Login          - логин студента
Password       - пароль студента
CourseName     - название курса
StartDate      - дата начала тренинга по курсу
-->
<HTML>
<BODY>
Здравствуйте 
<xsl:value-of select=""LastName""/>&#160;
<xsl:value-of select=""FirstName""/>&#160;
<xsl:value-of select=""Patronymic""/>&#160;.
<br/>
Ваша заявка на курс <xsl:value-of select=""CourseName""/> удовлетворена.
<br/>
Занятия начнутся <xsl:value-of select=""StartDate""/>
<br/>
</BODY>
</HTML>
</xsl:template>
</xsl:stylesheet>
         ";
         }
         else
         {
            this.NameEdit.Text = name;
            this.Text = "Редактирование шаблона";
            this.templateText.Text = templatebody.Replace("\n","\r\n");
         }
		}

      public static bool EditTemplate(string id,ref string name,ref string templatebody)
      {
         EditEmailTemplate et = new EditEmailTemplate(id,name, templatebody);
         while (true)
         {
            if (et.ShowDialog() == DialogResult.OK)
            {
               XmlDocument doc = new XmlDocument();
               try
               {
                  doc.LoadXml(et.templateText.Text);
               }
               catch (Exception e)
               {
                  MessageBox.Show("Ошибка при обработке XML :\n"+e.Message);
                  continue;
               }
               templatebody = et.templateText.Text;
               name = et.NameEdit.Text;
               return true;
            }
            else
               return false;
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
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.NameEdit = new System.Windows.Forms.TextBox();
         this.templateText = new System.Windows.Forms.TextBox();
         this.btnCancel = new System.Windows.Forms.Button();
         this.btnOk = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(8, 4);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(100, 20);
         this.label1.TabIndex = 0;
         this.label1.Text = "Название";
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(8, 32);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(120, 16);
         this.label2.TabIndex = 1;
         this.label2.Text = "Шаблон";
         // 
         // NameEdit
         // 
         this.NameEdit.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.NameEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.NameEdit.Location = new System.Drawing.Point(116, 4);
         this.NameEdit.MaxLength = 255;
         this.NameEdit.Name = "NameEdit";
         this.NameEdit.Size = new System.Drawing.Size(460, 20);
         this.NameEdit.TabIndex = 1;
         this.NameEdit.Text = "";
         // 
         // templateText
         // 
         this.templateText.AcceptsReturn = true;
         this.templateText.AcceptsTab = true;
         this.templateText.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.templateText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.templateText.Location = new System.Drawing.Point(8, 52);
         this.templateText.MaxLength = 8000;
         this.templateText.Multiline = true;
         this.templateText.Name = "templateText";
         this.templateText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.templateText.Size = new System.Drawing.Size(568, 304);
         this.templateText.TabIndex = 2;
         this.templateText.Text = "";
         // 
         // btnCancel
         // 
         this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.btnCancel.Location = new System.Drawing.Point(468, 364);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(108, 28);
         this.btnCancel.TabIndex = 4;
         this.btnCancel.Text = "Отменить";
         // 
         // btnOk
         // 
         this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.btnOk.Location = new System.Drawing.Point(352, 364);
         this.btnOk.Name = "btnOk";
         this.btnOk.Size = new System.Drawing.Size(108, 28);
         this.btnOk.TabIndex = 3;
         this.btnOk.Text = "Ок";
         // 
         // EditEmailTemplate
         // 
         this.AcceptButton = this.btnOk;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.btnCancel;
         this.ClientSize = new System.Drawing.Size(584, 397);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.btnOk,
                                                                      this.btnCancel,
                                                                      this.templateText,
                                                                      this.NameEdit,
                                                                      this.label2,
                                                                      this.label1});
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "EditEmailTemplate";
         this.ResumeLayout(false);

      }
		#endregion

	}
}
