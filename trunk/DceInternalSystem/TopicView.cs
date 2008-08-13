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
	/// Редактирование нити форума
	/// </summary>
	public class TopicView : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Button OkButton;
      private System.Windows.Forms.Button CancelButton;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.Panel NewTopic;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox MsgContent;
      private System.Windows.Forms.TextBox TopicName;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Panel Replies;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label DateLbl;
      private System.Windows.Forms.Label AuthorLbl;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Button button2;
      private System.Windows.Forms.Button button3;
      private System.Windows.Forms.TextBox StudentName;
      private System.Windows.Forms.Button SaveBtn;
      private System.Windows.Forms.WebBrowser WebBrowser;
      private System.Windows.Forms.CheckBox CBlocked;
      private DCEAccessLib.DataList dataList;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader3;
      private DCEAccessLib.DataColumnHeader AuthorColumn;
      private System.Windows.Forms.ContextMenu contextMenu1;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem menuItem4;
      

      TopicViewNode Node;
		public TopicView(TopicViewNode node)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.Node = node;
         this.button2.Enabled = this.Node.CanModify;
         this.button3.Enabled = this.Node.CanModify;
         this.TopicName.ReadOnly = this.Node.Readonly;
         this.MsgContent.ReadOnly = this.Node.Readonly;
         this.SaveBtn.Enabled = this.Node.CanModify;
         this.OkButton.Enabled = this.Node.CanModify;

         RefreshData();
		}

      public void RefreshData()
      {
         this.silent = true;
         OkButton.Enabled = !this.Node.IsNew;

		this.dataSet = DCEAccessLib.DAL.Reply.GetReplies(new Guid(this.Node.Id));
		this.dataView.Table = this.dataSet.Tables["replies"];

        
         if (!this.Node.IsNew)
         {
			DataRow row = DCEAccessLib.DAL.Reply.LoadTopic(new Guid(this.Node.Id));
			if (null != row)
            {
               string author;
               if ((int)row["Type"] == (int) EntityType.student)
                  author =row["StudentName"].ToString()+ " (студент)";
               else
                  author = row["UserName"].ToString()+ " (пользователь)";
               this.AuthorLbl.Text = "Автор: "+ author;
               this.DateLbl.Text = ((DateTime)row["PostDate"]).ToString(Settings.DateTimeFormat);
               this.TopicName.Text = row["Topic"].ToString();
               this.MsgContent.Text = row["Message"].ToString();
               this.CBlocked.Checked  = (bool)row["Blocked"];
            }

         }
         this.silent = false;
      }

      public void GenerateContent()
      {

         if (this.Node.EditRow["Student"].GetType() != typeof(System.DBNull))
         {
			StudentName.Text = DCEAccessLib.DAL.Student.GetName((Guid)this.Node.EditRow["Student"]);
         }
         else
            StudentName.Text = "[Публичная тема]";

         
            
         Object refmissing = System.Reflection.Missing.Value;
         WebBrowser.Navigate("about:blank");

//         System.Drawing.Font boldfnt = new System.Drawing.Font(Rreplies.Font,FontStyle.Bold);

      }
		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         this.Node = null;
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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TopicView));
         this.panel2 = new System.Windows.Forms.Panel();
         this.SaveBtn = new System.Windows.Forms.Button();
         this.OkButton = new System.Windows.Forms.Button();
         this.CancelButton = new System.Windows.Forms.Button();
         this.dataSet = new System.Data.DataSet();
         this.dataView = new System.Data.DataView();
         this.NewTopic = new System.Windows.Forms.Panel();
         this.CBlocked = new System.Windows.Forms.CheckBox();
         this.button3 = new System.Windows.Forms.Button();
         this.button2 = new System.Windows.Forms.Button();
         this.StudentName = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.DateLbl = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.AuthorLbl = new System.Windows.Forms.Label();
         this.TopicName = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.MsgContent = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.Replies = new System.Windows.Forms.Panel();
		 this.WebBrowser = new System.Windows.Forms.WebBrowser();
         this.dataList = new DCEAccessLib.DataList();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.AuthorColumn = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader3 = new DCEAccessLib.DataColumnHeader();
         this.contextMenu1 = new System.Windows.Forms.ContextMenu();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.menuItem3 = new System.Windows.Forms.MenuItem();
         this.menuItem4 = new System.Windows.Forms.MenuItem();
         this.panel2.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         this.NewTopic.SuspendLayout();
         this.Replies.SuspendLayout();
         this.WebBrowser.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel2
         // 
         this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panel2.Controls.Add(this.SaveBtn);
         this.panel2.Controls.Add(this.OkButton);
         this.panel2.Controls.Add(this.CancelButton);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel2.Location = new System.Drawing.Point(0, 376);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(624, 36);
         this.panel2.TabIndex = 1;
         // 
         // SaveBtn
         // 
         this.SaveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.SaveBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.SaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.SaveBtn.Location = new System.Drawing.Point(216, 6);
         this.SaveBtn.Name = "SaveBtn";
         this.SaveBtn.Size = new System.Drawing.Size(96, 24);
         this.SaveBtn.TabIndex = 7;
         this.SaveBtn.Text = "Сохранить";
         this.SaveBtn.Click += new System.EventHandler(this.button1_Click);
         // 
         // OkButton
         // 
         this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.OkButton.Location = new System.Drawing.Point(320, 6);
         this.OkButton.Name = "OkButton";
         this.OkButton.Size = new System.Drawing.Size(128, 24);
         this.OkButton.TabIndex = 8;
         this.OkButton.Text = "Добавить ответ ";
         this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
         // 
         // CancelButton
         // 
         this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.CancelButton.Location = new System.Drawing.Point(456, 6);
         this.CancelButton.Name = "CancelButton";
         this.CancelButton.Size = new System.Drawing.Size(160, 24);
         this.CancelButton.TabIndex = 9;
         this.CancelButton.Text = "Отменить/Перечитать ";
         this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // dataView
         // 
         this.dataView.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.dataView_ListChanged);
         // 
         // NewTopic
         // 
         this.NewTopic.Controls.Add(this.CBlocked);
         this.NewTopic.Controls.Add(this.button3);
         this.NewTopic.Controls.Add(this.button2);
         this.NewTopic.Controls.Add(this.StudentName);
         this.NewTopic.Controls.Add(this.label2);
         this.NewTopic.Controls.Add(this.DateLbl);
         this.NewTopic.Controls.Add(this.label5);
         this.NewTopic.Controls.Add(this.AuthorLbl);
         this.NewTopic.Controls.Add(this.TopicName);
         this.NewTopic.Controls.Add(this.label1);
         this.NewTopic.Controls.Add(this.MsgContent);
         this.NewTopic.Dock = System.Windows.Forms.DockStyle.Top;
         this.NewTopic.Location = new System.Drawing.Point(0, 0);
         this.NewTopic.Name = "NewTopic";
         this.NewTopic.Size = new System.Drawing.Size(624, 192);
         this.NewTopic.TabIndex = 3;
         // 
         // CBlocked
         // 
         this.CBlocked.Location = new System.Drawing.Point(8, 84);
         this.CBlocked.Name = "CBlocked";
         this.CBlocked.Size = new System.Drawing.Size(388, 16);
         this.CBlocked.TabIndex = 11;
         this.CBlocked.Text = "Заблокировать нить (не позволять студентам добавлять ответы)";
         // 
         // button3
         // 
         this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button3.Location = new System.Drawing.Point(464, 52);
         this.button3.Name = "button3";
         this.button3.Size = new System.Drawing.Size(144, 24);
         this.button3.TabIndex = 4;
         this.button3.Text = "Сделать публичной ";
         this.button3.Click += new System.EventHandler(this.button3_Click);
         // 
         // button2
         // 
         this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.button2.Location = new System.Drawing.Point(344, 52);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(112, 24);
         this.button2.TabIndex = 3;
         this.button2.Text = "Выбрать";
         this.button2.Click += new System.EventHandler(this.button2_Click);
         // 
         // StudentName
         // 
         this.StudentName.BackColor = System.Drawing.Color.White;
         this.StudentName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.StudentName.Enabled = false;
         this.StudentName.Location = new System.Drawing.Point(68, 56);
         this.StudentName.Name = "StudentName";
         this.StudentName.ReadOnly = true;
         this.StudentName.Size = new System.Drawing.Size(264, 20);
         this.StudentName.TabIndex = 2;
         this.StudentName.Text = "";
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(4, 60);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(60, 16);
         this.label2.TabIndex = 10;
         this.label2.Text = "Студент";
         // 
         // DateLbl
         // 
         this.DateLbl.Location = new System.Drawing.Point(400, 32);
         this.DateLbl.Name = "DateLbl";
         this.DateLbl.Size = new System.Drawing.Size(128, 16);
         this.DateLbl.TabIndex = 9;
         // 
         // label5
         // 
         this.label5.Location = new System.Drawing.Point(340, 32);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(52, 16);
         this.label5.TabIndex = 8;
         this.label5.Text = "Дата:";
         // 
         // AuthorLbl
         // 
         this.AuthorLbl.Location = new System.Drawing.Point(4, 32);
         this.AuthorLbl.Name = "AuthorLbl";
         this.AuthorLbl.Size = new System.Drawing.Size(328, 16);
         this.AuthorLbl.TabIndex = 7;
         this.AuthorLbl.Text = "Автор:";
         // 
         // TopicName
         // 
         this.TopicName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.TopicName.Location = new System.Drawing.Point(60, 4);
         this.TopicName.Name = "TopicName";
         this.TopicName.Size = new System.Drawing.Size(468, 20);
         this.TopicName.TabIndex = 1;
         this.TopicName.Text = "";
         this.TopicName.TextChanged += new System.EventHandler(this.TopicName_TextChanged);
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(4, 8);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(52, 16);
         this.label1.TabIndex = 4;
         this.label1.Text = "Тема";
         // 
         // MsgContent
         // 
         this.MsgContent.BackColor = System.Drawing.Color.White;
         this.MsgContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.MsgContent.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.MsgContent.Location = new System.Drawing.Point(0, 104);
         this.MsgContent.Multiline = true;
         this.MsgContent.Name = "MsgContent";
         this.MsgContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.MsgContent.Size = new System.Drawing.Size(624, 88);
         this.MsgContent.TabIndex = 5;
         this.MsgContent.Text = "";
         this.MsgContent.TextChanged += new System.EventHandler(this.TopicName_TextChanged);
         // 
         // label3
         // 
         this.label3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(122)), ((System.Byte)(170)), ((System.Byte)(190)));
         this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.label3.Dock = System.Windows.Forms.DockStyle.Top;
         this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.label3.ForeColor = System.Drawing.SystemColors.Info;
         this.label3.Location = new System.Drawing.Point(0, 192);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(624, 20);
         this.label3.TabIndex = 155;
         this.label3.Text = "Ответы";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // Replies
         // 
         this.Replies.Controls.Add(this.WebBrowser);
         this.Replies.Controls.Add(this.dataList);
         this.Replies.Dock = System.Windows.Forms.DockStyle.Fill;
         this.Replies.Location = new System.Drawing.Point(0, 212);
         this.Replies.Name = "Replies";
         this.Replies.Size = new System.Drawing.Size(624, 164);
         this.Replies.TabIndex = 156;
         // 
         // WebBrowser
         // 
         this.WebBrowser.Parent = this;
         this.WebBrowser.Enabled = true;
         this.WebBrowser.Location = new System.Drawing.Point(128, 48);
         this.WebBrowser.Size = new System.Drawing.Size(624, 164);
         this.WebBrowser.TabIndex = 6;
         this.WebBrowser.Visible = false;
		 this.WebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowser_DocumentComplete);
         // 
         // dataList
         // 
         this.dataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.dataList.AllowColumnReorder = true;
         this.dataList.AllowSorting = true;
         this.dataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.dataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                               this.dataColumnHeader2,
                                                                               this.AuthorColumn,
                                                                               this.dataColumnHeader3});
         this.dataList.ContextMenu = this.contextMenu1;
         this.dataList.DataView = this.dataView;
         this.dataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.dataList.FullRowSelect = true;
         this.dataList.GridLines = true;
         this.dataList.HideSelection = false;
         this.dataList.Location = new System.Drawing.Point(0, 0);
         this.dataList.MultiSelect = false;
         this.dataList.Name = "dataList";
         this.dataList.Size = new System.Drawing.Size(624, 164);
         this.dataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.dataList.TabIndex = 29;
         this.dataList.View = System.Windows.Forms.View.Details;
         this.dataList.DoubleClick += new System.EventHandler(this.dataList_DoubleClick);
         this.dataList.RowParsed += new DCEAccessLib.DataList.RowParseHandler(this.dataList_RowParsed);
         this.dataList.SelectedIndexChanged += new System.EventHandler(this.dataList_SelectedIndexChanged);
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "PostDate";
         this.dataColumnHeader2.Text = "Дата";
         this.dataColumnHeader2.Width = 85;
         // 
         // AuthorColumn
         // 
         this.AuthorColumn.FieldName = "ResName";
         this.AuthorColumn.Text = "Автор";
         this.AuthorColumn.Width = 204;
         // 
         // dataColumnHeader3
         // 
         this.dataColumnHeader3.FieldName = "Message";
         this.dataColumnHeader3.Text = "Текст";
         this.dataColumnHeader3.Width = 365;
         // 
         // contextMenu1
         // 
         this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItem1,
                                                                                     this.menuItem2,
                                                                                     this.menuItem3,
                                                                                     this.menuItem4});
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 0;
         this.menuItem1.Text = "Свойства";
         this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 1;
         this.menuItem2.Text = "-";
         // 
         // menuItem3
         // 
         this.menuItem3.Index = 2;
         this.menuItem3.Text = "Добавть ответ";
         this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
         // 
         // menuItem4
         // 
         this.menuItem4.Index = 3;
         this.menuItem4.Text = "Удалить";
         this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
         // 
         // TopicView
         // 
         this.Controls.Add(this.Replies);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.NewTopic);
         this.Controls.Add(this.panel2);
         this.Name = "TopicView";
         this.Size = new System.Drawing.Size(624, 412);
         this.panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         this.NewTopic.ResumeLayout(false);
         this.Replies.ResumeLayout(false);
         this.WebBrowser.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void dataView_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
      {
         GenerateContent();
      }

      private void CancelButton_Click(object sender, System.EventArgs e)
      {
         RefreshData();
      }

      private void OkButton_Click(object sender, System.EventArgs e)
      {
         string reply="";
         if (NewReply.NewReplyDialog(ref reply))
         {
            DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet("Select * from dbo.ForumReplies where Topic='"+this.Node.Id+"'","re");
            DataView view = new DataView(ds.Tables["re"]);
            DataRowView row = view.AddNew();
            row["id"] = System.Guid.NewGuid();
            row["Topic"] = this.Node.Id;
            row["Author"] = DCEUser.CurrentUser.id;
            row["Message"] = reply;
            row["PostDate"] = System.DateTime.Now;
            row.EndEdit();
            DCEAccessLib.DCEWebAccess.WebAccess.UpdateDataSet("Select * from dbo.ForumReplies where Topic='"+this.Node.Id+"'","re", ref ds);
            RefreshData();
         }
      }

      private void button2_Click(object sender, System.EventArgs e)
      {
         DataRowView row = UserSelect.SelectTrainingStudent(null,"",this.Node.TrainingId);
         if (row !=null)
         {
            this.Node.EditRow["Student"] = row["id"].ToString();
            this.StudentName.Text = row["LastName"].ToString()+" "+row["FirstName"].ToString()+" "+row["Patronymic"].ToString();
            this.Node.Changed = true;
         }
      }

      private void button1_Click(object sender, System.EventArgs e)
      {
         if (this.Node.IsNew)
         {
            this.Node.EditRow["PostDate"] = System.DateTime.Now;
         }

         this.Node.EditRow["Topic"] = this.TopicName.Text;
         this.Node.EditRow["Message"] = this.MsgContent.Text;
         this.Node.EditRow["Blocked"] = this.CBlocked.Checked;

         this.Node.EndEdit(false,false);
         RefreshData();
      }

      private void button3_Click(object sender, System.EventArgs e)
      {
         this.Node.EditRow["Student"] = System.DBNull.Value;
         StudentName.Text = "[Публичная тема]";
         this.Node.Changed = true;
      }


		private void WebBrowser_DocumentComplete(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
      {
         ///
         Object refmissing = System.Reflection.Missing.Value;
         HtmlDocument doc = WebBrowser.Document;
         
         string str=
@"<HTML>
<HEAD>
<style>
table {font-family:Microsoft Sans Serif;font-size:9pt;background-color:#000000 }
</style>
</HEAD>
<BODY style='margin-top: 0px;margin-left: 0px;margin-right: 0px;margin-bottom: 0px;padding: 0px;'>
<table border=0 cellspacing=1 cellpadding=3 width=100%>
<tr bgcolor=#7AAABE><th width=15% align=center>Дата</th>
<th align=center width=20%>Автор</th><th align=center>Текст</th><th width=10%>&nbsp;</th>
</tr>
";
         foreach (DataRowView r in this.dataView)
         {
            string author;
            if ((int)r["Type"] == (int) EntityType.student)
               author =r["StudentName"].ToString()+ " (студент)";
            else
               author = r["UserName"].ToString()+ " (пользователь)";

            str += "<tr bgcolor=#FFFFFF><td>"+((DateTime)r["PostDate"]).ToString(Settings.DateTimeFormat) + "</td><td>";
            str += " " + author + "</td><td>";
            str += r["Message"].ToString();
            str += "</td><td><button name='button1' id='"+ r["id"].ToString()+"'>Изменить</button></td></tr>";
         }

         str+= "</table>";

         doc.Write("<HTML><BODY>"+str+"</BODY></HTML>");

         setupHandlers();
      }

      public void setupHandlers()
      {
         HtmlDocument hdoc = this.WebBrowser.Document;
		 HtmlElementCollection coll = hdoc.GetElementsByTagName("button1");
		 foreach (HtmlElement elem in coll)
         {
            //mshtml.HTMLElementEvents.HTMLInput
			 HtmlElement button1 = elem;

            HtmlElementEventHandler h=
               new HtmlElementEventHandler(clickHandler);
            button1.Click += h;
         }
      }
      public void clickHandler(object sender, HtmlElementEventArgs e)
      {
         string id = e.FromElement.Id;
         if (id != null)
         {
            foreach (DataRowView row in this.dataView)
            {
               if (id == row["id"].ToString())
               {
                  DialogResult res = this.editReply(row);
                  if (res == DialogResult.Cancel)
                  {
                     setupHandlers();
                  }
                  break;
               }
            }
         }
      }

      private DialogResult editReply(DataRowView row)
      {
         string reply = row["Message"].ToString();
         string id = row["id"].ToString();
         //MessageBox.Show("Kaka");
         DialogResult res = NewReply.EditReplyDialog(ref reply);
                  
         if (res == DialogResult.OK)
         {
            try
            {
               DataSet ds = DCEWebAccess.WebAccess.GetDataSet("select id,Message from dbo.ForumReplies where id='"+id+"'","di");
               if (ds.Tables["di"].Rows.Count>0)
               {  
                  ds.Tables["di"].Rows[0].BeginEdit();
                  ds.Tables["di"].Rows[0]["Message"] = reply;
                  ds.Tables["di"].Rows[0].EndEdit();
                  DCEWebAccess.WebAccess.UpdateDataSet("select id,Message from dbo.ForumReplies where id='"+id+"'","di",ref ds);
               }
            }
            finally
            {
               this.RefreshData();
            }
         }
         if (res == DialogResult.Abort)
         {
            try
            {
               DCEWebAccess.WebAccess.ExecSQL("Delete from ForumReplies where id='"+id+"'");
            }
            finally
            {
               this.RefreshData();
            }
         }
         return res;
      }


      bool silent = true;
      private void TopicName_TextChanged(object sender, System.EventArgs e)
      {
         ///
         if (!silent)
            this.Node.Changed = true;
      }

      private void dataList_SelectedIndexChanged(object sender, System.EventArgs e)
      {
      
      }

      private void dataList_RowParsed(System.Data.DataRowView row, System.Windows.Forms.ListViewItem item)
      {
         string author;
         if ((int)row["Type"] == (int) EntityType.student)
            author =row["StudentName"].ToString()+ " (студент)";
         else
            author = row["UserName"].ToString()+ " (пользователь)";
         item.SubItems[1].Text = author;
         item.SubItems[2].Text = item.SubItems[2].Text.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");
      }

      private void dataList_DoubleClick(object sender, System.EventArgs e)
      {
         DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;
         DialogResult res = this.editReply(row);
      }

      private void menuItem1_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;
            DialogResult res = this.editReply(row);
         }
      }

      private void menuItem3_Click(object sender, System.EventArgs e)
      {
         this.OkButton_Click(sender,e);
      }

      private void menuItem4_Click(object sender, System.EventArgs e)
      {
         if (this.dataList.SelectedItems.Count>0)
         {
            DataRowView row = this.dataList.SelectedItems[0].Tag as DataRowView;
            string id = row["id"].ToString();
            try
            {
               DCEWebAccess.WebAccess.ExecSQL("Delete from ForumReplies where id='"+id+"'");
            }
            finally
            {
               this.RefreshData();
            }
         }
      }
	}

   public class TopicViewNode : RecordEditNode
   {
      public string TrainingId;

      public TopicViewNode (NodeControl parent,string topicId, string trainingId)
         : base(parent, "Select * from dbo.ForumTopics", "t", "id", topicId)
      {
         this.TrainingId=trainingId;
         if (this.IsNew)
            this.EditRow["Training"] = this.TrainingId;
         this.rdonly = DCEUser.CurrentUser.Trainings == DCEUser.Access.No
            && !DCEUser.CurrentUser.isCuratorOrInstructor(TrainingId);
      }

      protected override void InitNewRecord()
      {
         this.EditRow["id"] = System.Guid.NewGuid();
         this.EditRow["Author"] = DCEAccessLib.DCEUser.CurrentUser.id;
         this.EditRow["Student"] = System.DBNull.Value;
         this.EditRow["Topic"] = "";
         this.EditRow["Message"] = "";
      }
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new TopicView(this);
         }
         return this.fControl;
      }

      public override String GetCaption()
      {
         if (this.EditRow!=null)
            if (!this.IsNew)
            {
               DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
                  @"SELECT  Topic from ForumTopics where id= '"+this.Id+"'","t" );
               if (ds.Tables["t"].Rows.Count>0)
               {
                  return "Тема: "+ds.Tables["t"].Rows[0][0].ToString();
               }
            }
         return "[Новая тема]";
      }

      public override bool CanClose()
      {
         return true;
      }
   }

}
