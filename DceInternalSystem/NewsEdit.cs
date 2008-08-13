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
   /// Редактор новостей
   /// </summary>
   public class NewsEdit : System.Windows.Forms.UserControl
   {
      private System.Data.DataSet photos = null;
      private System.Data.DataTable photoTable = null;

      private System.ComponentModel.IContainer components;
      private System.Windows.Forms.Panel TopPanel;
      private System.Windows.Forms.Panel MainPanel;
      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.Button ExitButton;
      private System.Windows.Forms.Panel ButtonPanel;
      private DCEAccessLib.LangSwitcher langSwitcher;
      private System.Windows.Forms.ToolTip toolTip1;
      private System.Windows.Forms.OpenFileDialog openFileDialog1;
   	private System.Windows.Forms.Label TitleLabel;
   	private System.Windows.Forms.Panel panel2;
   	private System.Windows.Forms.Label label4;
   	private DCEAccessLib.MLEdit mledHead;
   	private System.Windows.Forms.TextBox textBoxMoreHref;
   	private System.Windows.Forms.Label label3;
   	private System.Windows.Forms.Label label5;
   	private System.Windows.Forms.Label label1;
   	private System.Windows.Forms.DateTimePicker NewsDate;
   	private DCEAccessLib.MLEdit mlEditFull;
   	private DCEAccessLib.MLEdit mledShort;
   	private System.Windows.Forms.Label label16;
   	private System.Windows.Forms.Label label2;
   	private System.Windows.Forms.TextBox textBoxCode;
   	private System.Windows.Forms.Label label6;
   	private System.Windows.Forms.PictureBox pictureBox1;

      private NewsEditNode Node;
		
      public NewsEdit(NewsEditNode node)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         Node = node;

         this.NewsDate.Enabled = this.Node.CanModify;
         this.mledHead.Enabled = this.Node.CanModify;
         this.mledShort.Enabled = this.Node.CanModify;
         this.mlEditFull.Enabled = this.Node.CanModify;
         this.textBoxMoreHref.Enabled = this.Node.CanModify;
         this.textBoxCode.Enabled = this.Node.CanModify;
         this.SaveButton.Enabled = this.Node.CanModify;

         this.mlEditFull.SetParentNode(Node);
         this.mledShort.SetParentNode(Node);
         this.mledHead.SetParentNode(Node);
         
         this.RebindControls();
         this.Node.OnReloadContent += new RecordEditNode.ReloadContentHandler(this.RebindControls);
         Node.OnUpdateDataSet += new RecordEditNode.EnumDataSetsHandler(this.OnUpdateDataSet);

         RebindControls();
      }

      public bool OnUpdateDataSet(string TransactionId, ref DCEAccessLib.DataSetBatchUpdate dataSet)
      {
         dataSet.sql = "select * from Content where eid='"
            +this.Node.EditRow["Image"].ToString()+"'";
         dataSet.tableName ="Photos";
         dataSet.dataSet = this.photos;
         return this.photoTable.Rows[0]["Data"] != System.DBNull.Value;
      }

      public void RebindControls()
      {
         this.mlEditFull.DataBindings.Clear();
         this.mlEditFull.DataBindings.Add("eId", Node.EditRow, "Text");

         this.mledShort.DataBindings.Clear();
         this.mledShort.DataBindings.Add("eId", Node.EditRow, "Short");

         this.mledHead.DataBindings.Clear();
         this.mledHead.DataBindings.Add("eId", Node.EditRow, "Head");

         //this.NewsDate.Value = (System.DateTime)this.Node.EditRow["NewsDate"];

         this.NewsDate.DataBindings.Clear();
         this.NewsDate.DataBindings.Add("Value", Node.EditRow, "NewsDate");
         this.textBoxCode.DataBindings.Clear();
         this.textBoxCode.DataBindings.Add("Text", Node.EditRow, "CourseCode");
         this.textBoxMoreHref.DataBindings.Clear();
         this.textBoxMoreHref.DataBindings.Add("Text", Node.EditRow, "MoreHref");

         if (this.Node.EditRow["Image"] == System.DBNull.Value)
         {
            this.Node.EditRow["Image"] = System.Guid.NewGuid();
         }
         this.photos = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            "select * from Content where eid='"+this.Node.EditRow["Image"].ToString()
            +"'", "Photos");
         photoTable = photos.Tables["Photos"];
         System.Data.DataRow pRow = null;
         if (photoTable != null && photoTable.Rows.Count == 0)
         {
            pRow = photoTable.NewRow();
            pRow["eid"] = this.Node.EditRow["Image"];
            pRow["Type"] = DCEAccessLib.ContentType._image;
            photoTable.Rows.Add(pRow);
         }
         else
            pRow = photoTable.Rows[0];

         if (pRow["Data"] != System.DBNull.Value)
            this.pictureBox1.Image = new Bitmap(
               new System.IO.MemoryStream((byte[]) pRow["Data"]));
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
		  this.components = new System.ComponentModel.Container();
		  this.langSwitcher = new DCEAccessLib.LangSwitcher();
		  this.TopPanel = new System.Windows.Forms.Panel();
		  this.ButtonPanel = new System.Windows.Forms.Panel();
		  this.ExitButton = new System.Windows.Forms.Button();
		  this.SaveButton = new System.Windows.Forms.Button();
		  this.MainPanel = new System.Windows.Forms.Panel();
		  this.pictureBox1 = new System.Windows.Forms.PictureBox();
		  this.panel2 = new System.Windows.Forms.Panel();
		  this.label4 = new System.Windows.Forms.Label();
		  this.mledHead = new DCEAccessLib.MLEdit();
		  this.textBoxMoreHref = new System.Windows.Forms.TextBox();
		  this.label3 = new System.Windows.Forms.Label();
		  this.label5 = new System.Windows.Forms.Label();
		  this.label1 = new System.Windows.Forms.Label();
		  this.NewsDate = new System.Windows.Forms.DateTimePicker();
		  this.mlEditFull = new DCEAccessLib.MLEdit();
		  this.label2 = new System.Windows.Forms.Label();
		  this.mledShort = new DCEAccessLib.MLEdit();
		  this.label16 = new System.Windows.Forms.Label();
		  this.textBoxCode = new System.Windows.Forms.TextBox();
		  this.label6 = new System.Windows.Forms.Label();
		  this.TitleLabel = new System.Windows.Forms.Label();
		  this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		  this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		  this.TopPanel.SuspendLayout();
		  this.ButtonPanel.SuspendLayout();
		  this.MainPanel.SuspendLayout();
		  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
		  this.panel2.SuspendLayout();
		  this.SuspendLayout();
		  // 
		  // langSwitcher
		  // 
		  this.langSwitcher.Enabled = global::DCEInternalSystem.Properties.Settings.Default.LangSwitchEnabled;
		  this.langSwitcher.Location = new System.Drawing.Point(0, 4);
		  this.langSwitcher.Name = "langSwitcher";
		  this.langSwitcher.Size = new System.Drawing.Size(176, 24);
		  this.langSwitcher.TabIndex = 0;
		  this.langSwitcher.TextLabel = "Язык";
		  this.langSwitcher.Visible = global::DCEInternalSystem.Properties.Settings.Default.LangSwitchVisible;
		  // 
		  // TopPanel
		  // 
		  this.TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		  this.TopPanel.Controls.Add(this.langSwitcher);
		  this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
		  this.TopPanel.Location = new System.Drawing.Point(0, 0);
		  this.TopPanel.Name = "TopPanel";
		  this.TopPanel.Size = new System.Drawing.Size(696, 32);
		  this.TopPanel.TabIndex = 1;
		  // 
		  // ButtonPanel
		  // 
		  this.ButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		  this.ButtonPanel.Controls.Add(this.ExitButton);
		  this.ButtonPanel.Controls.Add(this.SaveButton);
		  this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
		  this.ButtonPanel.Location = new System.Drawing.Point(0, 444);
		  this.ButtonPanel.Name = "ButtonPanel";
		  this.ButtonPanel.Size = new System.Drawing.Size(696, 44);
		  this.ButtonPanel.TabIndex = 2;
		  // 
		  // ExitButton
		  // 
		  this.ExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		  this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		  this.ExitButton.Location = new System.Drawing.Point(590, 8);
		  this.ExitButton.Name = "ExitButton";
		  this.ExitButton.Size = new System.Drawing.Size(96, 28);
		  this.ExitButton.TabIndex = 8;
		  this.ExitButton.Text = "Отмена";
		  this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
		  // 
		  // SaveButton
		  // 
		  this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		  this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		  this.SaveButton.Location = new System.Drawing.Point(486, 8);
		  this.SaveButton.Name = "SaveButton";
		  this.SaveButton.Size = new System.Drawing.Size(96, 28);
		  this.SaveButton.TabIndex = 7;
		  this.SaveButton.Text = "Сохранить";
		  this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
		  // 
		  // MainPanel
		  // 
		  this.MainPanel.AutoScroll = true;
		  this.MainPanel.Controls.Add(this.pictureBox1);
		  this.MainPanel.Controls.Add(this.panel2);
		  this.MainPanel.Controls.Add(this.TitleLabel);
		  this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		  this.MainPanel.Location = new System.Drawing.Point(0, 32);
		  this.MainPanel.Name = "MainPanel";
		  this.MainPanel.Size = new System.Drawing.Size(696, 412);
		  this.MainPanel.TabIndex = 27;
		  // 
		  // pictureBox1
		  // 
		  this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		  this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
		  this.pictureBox1.Location = new System.Drawing.Point(8, 368);
		  this.pictureBox1.Name = "pictureBox1";
		  this.pictureBox1.Size = new System.Drawing.Size(60, 90);
		  this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
		  this.pictureBox1.TabIndex = 167;
		  this.pictureBox1.TabStop = false;
		  this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
		  // 
		  // panel2
		  // 
		  this.panel2.Controls.Add(this.label4);
		  this.panel2.Controls.Add(this.mledHead);
		  this.panel2.Controls.Add(this.textBoxMoreHref);
		  this.panel2.Controls.Add(this.label3);
		  this.panel2.Controls.Add(this.label5);
		  this.panel2.Controls.Add(this.label1);
		  this.panel2.Controls.Add(this.NewsDate);
		  this.panel2.Controls.Add(this.mlEditFull);
		  this.panel2.Controls.Add(this.mledShort);
		  this.panel2.Controls.Add(this.label16);
		  this.panel2.Controls.Add(this.label2);
		  this.panel2.Controls.Add(this.textBoxCode);
		  this.panel2.Controls.Add(this.label6);
		  this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		  this.panel2.Location = new System.Drawing.Point(0, 20);
		  this.panel2.Name = "panel2";
		  this.panel2.Size = new System.Drawing.Size(680, 340);
		  this.panel2.TabIndex = 154;
		  // 
		  // label4
		  // 
		  this.label4.Location = new System.Drawing.Point(4, 276);
		  this.label4.Name = "label4";
		  this.label4.Size = new System.Drawing.Size(196, 36);
		  this.label4.TabIndex = 164;
		  this.label4.Text = "Код курса для ссылки на страницу описания:";
		  // 
		  // mledHead
		  // 
		  this.mledHead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					  | System.Windows.Forms.AnchorStyles.Right)));
		  this.mledHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		  this.mledHead.CaptionLabel = null;
		  this.mledHead.eId = "";
		  this.mledHead.LanguageSwitcher = this.langSwitcher;
		  this.mledHead.Location = new System.Drawing.Point(204, 40);
		  this.mledHead.MaxLength = 150;
		  this.mledHead.Name = "mledHead";
		  this.mledHead.Size = new System.Drawing.Size(464, 20);
		  this.mledHead.TabIndex = 2;
		  // 
		  // textBoxMoreHref
		  // 
		  this.textBoxMoreHref.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					  | System.Windows.Forms.AnchorStyles.Right)));
		  this.textBoxMoreHref.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		  this.textBoxMoreHref.Location = new System.Drawing.Point(204, 240);
		  this.textBoxMoreHref.MaxLength = 128;
		  this.textBoxMoreHref.Name = "textBoxMoreHref";
		  this.textBoxMoreHref.Size = new System.Drawing.Size(464, 20);
		  this.textBoxMoreHref.TabIndex = 5;
		  this.toolTip1.SetToolTip(this.textBoxMoreHref, "Url для перехода по нажатию на гиперссылку \"Подробнее...\"");
		  // 
		  // label3
		  // 
		  this.label3.Location = new System.Drawing.Point(4, 244);
		  this.label3.Name = "label3";
		  this.label3.Size = new System.Drawing.Size(196, 20);
		  this.label3.TabIndex = 164;
		  this.label3.Text = "URL:";
		  this.toolTip1.SetToolTip(this.label3, "Url для гиперссылки \"Подробнее...\"");
		  // 
		  // label5
		  // 
		  this.label5.Location = new System.Drawing.Point(4, 72);
		  this.label5.Name = "label5";
		  this.label5.Size = new System.Drawing.Size(196, 20);
		  this.label5.TabIndex = 160;
		  this.label5.Text = "Краткое описание:";
		  this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		  // 
		  // label1
		  // 
		  this.label1.Location = new System.Drawing.Point(4, 16);
		  this.label1.Name = "label1";
		  this.label1.Size = new System.Drawing.Size(196, 20);
		  this.label1.TabIndex = 164;
		  this.label1.Text = "Дата:";
		  // 
		  // NewsDate
		  // 
		  this.NewsDate.Location = new System.Drawing.Point(204, 16);
		  this.NewsDate.Name = "NewsDate";
		  this.NewsDate.Size = new System.Drawing.Size(200, 20);
		  this.NewsDate.TabIndex = 1;
		  // 
		  // mlEditFull
		  // 
		  this.mlEditFull.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					  | System.Windows.Forms.AnchorStyles.Right)));
		  this.mlEditFull.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		  this.mlEditFull.CaptionLabel = this.label2;
		  this.mlEditFull.ContentType = DCEAccessLib.ContentType._html;
		  this.mlEditFull.DataType = DCEAccessLib.dataFieldType.ntext;
		  this.mlEditFull.eId = "";
		  this.mlEditFull.EntType = DCEAccessLib.ContentType._html;
		  this.mlEditFull.LanguageSwitcher = this.langSwitcher;
		  this.mlEditFull.Location = new System.Drawing.Point(204, 136);
		  this.mlEditFull.MaxLength = 32000;
		  this.mlEditFull.Multiline = true;
		  this.mlEditFull.Name = "mlEditFull";
		  this.mlEditFull.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		  this.mlEditFull.Size = new System.Drawing.Size(464, 96);
		  this.mlEditFull.TabIndex = 4;
		  // 
		  // label2
		  // 
		  this.label2.Location = new System.Drawing.Point(4, 140);
		  this.label2.Name = "label2";
		  this.label2.Size = new System.Drawing.Size(196, 20);
		  this.label2.TabIndex = 160;
		  this.label2.Text = "Основной текст:";
		  this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		  // 
		  // mledShort
		  // 
		  this.mledShort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					  | System.Windows.Forms.AnchorStyles.Right)));
		  this.mledShort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		  this.mledShort.CaptionLabel = null;
		  this.mledShort.eId = "";
		  this.mledShort.LanguageSwitcher = this.langSwitcher;
		  this.mledShort.Location = new System.Drawing.Point(204, 68);
		  this.mledShort.MaxLength = 255;
		  this.mledShort.Multiline = true;
		  this.mledShort.Name = "mledShort";
		  this.mledShort.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		  this.mledShort.Size = new System.Drawing.Size(464, 60);
		  this.mledShort.TabIndex = 3;
		  // 
		  // label16
		  // 
		  this.label16.Location = new System.Drawing.Point(4, 44);
		  this.label16.Name = "label16";
		  this.label16.Size = new System.Drawing.Size(196, 20);
		  this.label16.TabIndex = 164;
		  this.label16.Text = "Заголовок:";
		  // 
		  // textBoxCode
		  // 
		  this.textBoxCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		  this.textBoxCode.Location = new System.Drawing.Point(204, 288);
		  this.textBoxCode.MaxLength = 15;
		  this.textBoxCode.Name = "textBoxCode";
		  this.textBoxCode.Size = new System.Drawing.Size(164, 20);
		  this.textBoxCode.TabIndex = 6;
		  // 
		  // label6
		  // 
		  this.label6.Location = new System.Drawing.Point(4, 320);
		  this.label6.Name = "label6";
		  this.label6.Size = new System.Drawing.Size(64, 23);
		  this.label6.TabIndex = 1;
		  this.label6.Text = "Фото";
		  // 
		  // TitleLabel
		  // 
		  this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
		  this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		  this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
		  this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
		  this.TitleLabel.ForeColor = System.Drawing.SystemColors.Info;
		  this.TitleLabel.Location = new System.Drawing.Point(0, 0);
		  this.TitleLabel.Name = "TitleLabel";
		  this.TitleLabel.Size = new System.Drawing.Size(680, 20);
		  this.TitleLabel.TabIndex = 153;
		  this.TitleLabel.Text = "Новость";
		  this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		  // 
		  // openFileDialog1
		  // 
		  this.openFileDialog1.Filter = "Файлы изображений|*.jpg; *.gif; *.jpeg";
		  // 
		  // NewsEdit
		  // 
		  this.AutoScroll = true;
		  this.Controls.Add(this.MainPanel);
		  this.Controls.Add(this.ButtonPanel);
		  this.Controls.Add(this.TopPanel);
		  this.Name = "NewsEdit";
		  this.Size = new System.Drawing.Size(696, 488);
		  this.TopPanel.ResumeLayout(false);
		  this.ButtonPanel.ResumeLayout(false);
		  this.MainPanel.ResumeLayout(false);
		  this.MainPanel.PerformLayout();
		  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
		  this.panel2.ResumeLayout(false);
		  this.panel2.PerformLayout();
		  this.ResumeLayout(false);

	  }
      #endregion

      private void SaveButton_Click(object sender, System.EventArgs e)
      {
         this.Node.Save();
      }

      private void ExitButton_Click(object sender, System.EventArgs e)
      {
         this.Node.Reset();
      }

      private void Data_ValueChanged(object sender, System.EventArgs e)
      {
      }

      private void mled_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
      {
      }

      private void mlEditFull_TextChanged(object sender, System.EventArgs e)
      {
      
      }

      private void mledHead_TextChanged(object sender, System.EventArgs e)
      {
      
      }

      private void pictureBox1_Click(object sender, System.EventArgs e)
      {
         if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
         {
            System.IO.Stream pic = this.openFileDialog1.OpenFile();
            try
            {
               if (pic.Length > 0)
               {
                  byte[] Array = new byte[pic.Length];
                  pic.Read(Array,0,(int)pic.Length);
                  this.photoTable.Rows[0]["Data"] = Array;
                  this.pictureBox1.Image = new Bitmap(new System.IO.MemoryStream(Array));
               }
            }
            finally
            {
               pic.Close();
            }
         }
      }

   }

   /// <summary>
   /// Нода отвечающая за редактирование свойств курса
   /// </summary>
   public class NewsEditNode : HighlightedRecordEditNode 
   {
      public NewsEditNode(NodeControl parent, string id)
         : base(parent, "select * from dbo.News order by NewsDate", "News", "id", id, false)
      {
         this.rdonly = DCEUser.CurrentUser.News != DCEUser.Access.Modify;
      }

      void onChange(object o, System.Data.DataRowChangeEventArgs e)
      {
         this.IsNodeDirty = true;
      }

      public override UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new NewsEdit(this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         string ret = "Новость:";
         if (EditRow == null)
            return "";
         if (!IsNew)
            ret += ((System.DateTime)EditRow["NewsDate"]).ToShortDateString();
         if (this.IsNodeDirty)
            ret += " *";
         return ret;
      }

      public override void ReleaseControl()
      {
         // do nothing
      }
      
      public override bool CanClose()
      {
         return true;
      }

      /// <summary>
      /// Initialize new record for editing (EditRow)
      /// </summary>
      protected override void InitNewRecord()
      {
         // filling all non-nullable fields
         EditRow ["id"] = System.Guid.NewGuid();
         EditRow ["NewsDate"] = System.DateTime.Now;
         EditRow ["Head"]  = System.Guid.NewGuid();
         EditRow ["Short"]  = System.Guid.NewGuid();
         EditRow ["Text"]  = System.Guid.NewGuid();
         EditRow ["MoreText"]  = System.Guid.NewGuid();
         EditRow ["Image"]  = System.Guid.NewGuid();
      }
   }
}
