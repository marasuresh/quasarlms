using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCECourseEditor
{
   /// <summary>
   /// Нода, описывающая список курсов для конкретной области
   /// </summary>
   public class CoursesListNode : RecordEditNode
   {
      private bool lastDomainNode = false;
      private bool isRoot = false;
      public bool IsRoot
      {
         get { return isRoot; }
      }

      private string domainid;
      public string DomainId
      {
         get { return domainid; }
      }
      
      private string domainName;
      protected string DomainName
      {
         get { return domainName; }

         set
         {
            domainName = value;
            this.CaptionChanged();
         }
      }

      private static EditRowContent<string>[] GetEditRowContent(string domainid)
      {
         // при добавлении второго элемента EditRowContent необходимо 
         // в классе использовать this.GetEditContent для доступа к полям
         
         return !string.IsNullOrEmpty(domainid)
				? new EditRowContent<string>[] {
						new EditRowContent<string>(
						"select dbo.GetStrContentAlt(Name, 'RU', 'EN') as RName, * from dbo.CourseDomain", 
						"CourseDomain",
						"id",
						domainid,
						"CourseDomain")
						}
				: new EditRowContent<string>[0];
      }
	   /*
      public CoursesListNode(System.Windows.Forms.TreeView treeview)
         : base(null, new EditRowContent[0])
      {
         isRoot = true;
         
         this.treeNode = new System.Windows.Forms.TreeNode();
         treeNode.Tag = this;
         treeNode.Text = this.GetCaption();
         treeview.Nodes.Add(treeNode);
         
         this.OnReloadContent += new RecordEditNode.ReloadContentHandler(ReloadContent);

         this.Expand();
      }*/

      public CoursesListNode(NodeControl parent, string domainid, string domainname, bool commondomain)
         : base(parent, GetEditRowContent(domainid))
      {
         isRoot = commondomain;
         this.domainid = this.Id;

         if (!isRoot)
         {
            if (this.EditRow.IsNew)
               DomainName = "[Новая область]";
            else
               DomainName = domainname;
         }
         else
            DomainName = "Общие курсы";

         this.CaptionChanged();

         this.OnReloadContent += new RecordEditNode.ReloadContentHandler(ReloadContent);

         this.Expand();
      }

      public void ReloadContent()
      {
         if (this.EditRow != null)
            DomainName = this.EditRow["RName"].ToString();
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new DCECourseEditor.CoursesList(this);
         }
         
         if (needRefresh)
         {
            ((CoursesList)this.fControl).RefreshData();
            needRefresh = false;
         }

         return this.fControl;
      }

      public override bool HaveChildNodes() 
      { 
         // не работает, метод вызывается до инициализации поля!
         return !this.lastDomainNode; 
      }
      
      private DataSet ds;
      private DataView dv;

      public override void CreateChilds()
      {
         string domqualif = (
            this.isRoot ? 
            " is NULL" : 
            " = '" + this.Id + "'");

         ds = DCEWebAccess.WebAccess.GetDataSet(
            
            "select dbo.GetStrContentAlt(Name, 'RU', 'EN') as RName, " + 
            
            "* from dbo.CourseDomain where " + 
            
            "Parent " + domqualif + 
            
            " order by RName",
            
            "CourseDomain");

         dv = new DataView();
         dv.Table = ds.Tables["CourseDomain"];
         
         foreach(DataRowView row in dv)
         {
            new CoursesListNode(this, row["id"].ToString(), row["RName"].ToString(), false);
         }
      }

      public override String GetCaption()
      {
         if (this.isRoot)
            return "Общие курсы";
         else
            return "Область : " + domainName;
      }
      
      protected override void InitNewRecord(string qualifier)
      {
         // filling all non-nullable fields
         EditRow ["id"] = System.Guid.NewGuid().ToString();
         EditRow ["Name"] = System.Guid.NewGuid().ToString();
         
         if (this.NodeParent is CoursesListNode)
         {
            CoursesListNode n = this.NodeParent as CoursesListNode;
            if (!n.IsRoot)
               EditRow ["Parent"] = n.Id;
         }
      }

      private void CreateItem_Click(object sender, System.EventArgs e)
      {
         CoursesListNode node = new CoursesListNode(this, "", "", false);
         node.Select();
      
         DomainEdit dialog = new DomainEdit(node);
      
         DialogResult res = dialog.ShowDialog();
         if (res != DialogResult.OK)
         {
            node.Dispose();
         }
      }
      
      private void EditItem_Click(object sender, System.EventArgs e)
      {
         DomainEdit dialog = new DomainEdit(this);
         DialogResult res = dialog.ShowDialog();
         this.DomainName = this.EditRow["RName"].ToString();
      }
      
      private void RemoveItem_Click(object sender, System.EventArgs e)
      {
         // проверка состояния дочерних нод

         if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данную запись?", "Удалить", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from CourseDomain where id = '" + this.Id + "'");
         
            this.Dispose();
         }
      }
      
      public override ArrayList GetPopupMenu()
      {
         int Count = base.GetPopupMenu().Count;

         #region initmenu code

         MenuItem CreateItem = new MenuItem("Создать", new EventHandler(CreateItem_Click));
         MenuItem Separator = new MenuItem("-");
         MenuItem EditItem = new MenuItem("Редактировать", new EventHandler(EditItem_Click));
         MenuItem RemoveItem = new MenuItem("Удалить", new EventHandler(RemoveItem_Click));

         if (Count != 0)
            this.menuItemCollection.Add(Separator);

         this.menuItemCollection.AddRange(
            new MenuItem[] {
                              CreateItem,
                              Separator,
                              EditItem,
                              RemoveItem
                           }
            );
            
         if (this.isRoot)
         {
            RemoveItem.Visible = false;
            EditItem.Visible = false;
            Separator.Visible = false;
         }
         if (DCEUser.CurrentUser.ReadOnlyCourses)
         {
            CreateItem.Enabled = false;
            RemoveItem.Enabled = false;
         }

         #endregion
         
         return this.menuItemCollection;
      }
   }
   /// <summary>
	/// Список курсов
	/// </summary>
	public class CoursesList : System.Windows.Forms.UserControl
	{
      #region Form variables
      private DCEAccessLib.DataColumnHeader dataColumnHeader1;
      private DCEAccessLib.DataColumnHeader dataColumnHeader2;
      private DCEAccessLib.DataColumnHeader dataColumnHeader3;
      private DCEAccessLib.DataColumnHeader dataColumnHeader5;
      private DCEAccessLib.DataColumnHeader dataColumnHeader4;
      
      private System.Windows.Forms.ContextMenu CourseContextMenu;
      private System.Windows.Forms.MenuItem CreateMenuItem;
      private System.Windows.Forms.MenuItem Separator1;
      private System.Windows.Forms.MenuItem EditMenuItem;
      private System.Windows.Forms.MenuItem RemoveMenuItem;
      private System.Windows.Forms.MenuItem Separator2;
      private System.Windows.Forms.MenuItem RefreshMenuItem;
		
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      private System.Data.DataSet dataSet;
      private DCEAccessLib.DataList CoursesDataList;
      private System.Windows.Forms.MenuItem VersionMenuItem;
      private System.Windows.Forms.MenuItem DomainMenuItem;
      private System.Data.DataView dataView;
      private DCEAccessLib.DataColumnHeader dataColumnHeader6;
      private DCEAccessLib.DataColumnHeader dataColumnHeader7;
      private DCEAccessLib.DataColumnHeader dataColumnHeader8;
      private DCEAccessLib.DataColumnHeader dataColumnHeader9;
      private DCEAccessLib.DataColumnHeader dataColumnHeader10;
      private System.Windows.Forms.MenuItem Separator3;
      private System.Windows.Forms.MenuItem ImportMenuItem;
      private System.Windows.Forms.MenuItem ExportMenuItem;
      private System.Windows.Forms.MenuItem menuItemSCORM;
      private System.Windows.Forms.MenuItem menuItemIndex;
      #endregion

      private CoursesListNode Node;
      
      public CoursesList()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public CoursesList(CoursesListNode node)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
         
         Node = node;

         if (DCEUser.CurrentUser.ReadOnlyCourses)
         {
            this.CreateMenuItem.Enabled = false;
            this.RemoveMenuItem.Enabled = false;
            this.VersionMenuItem.Enabled = false;
            this.DomainMenuItem.Enabled = false;
            this.ImportMenuItem.Enabled = false;
            this.ExportMenuItem.Enabled = false;
            this.menuItemSCORM.Enabled = false;
            this.menuItemIndex.Enabled = false;
         }

         RefreshData();
      }
      
      private string GetSql()
      {
         string query = this.Node.Id == "" ? 
            "Area is NULL or Area not in (select id from dbo.CourseDomain)" 
            : 
            "Area = '" + this.Node.Id + "'";
      
         return "select dbo.GetStrContentAlt(Name, 'RU', 'EN') as RName, " + 
            
            "dbo.CourseIsActive(c.id) as Active, dbo.CourseDuration(c.id) as Duration, " +
      
            "dbo.GetStrContentAlt(Author, 'RU', 'EN') as RAuthor, " + 
            
            "dbo.GetStrContentOrderer(Additions, 1, 0) as Add1, " + 

            "dbo.GetStrContentOrderer(Additions, 1, 1) as Add2, " + 

            "dbo.GetStrContentOrderer(Additions, 1, 2) as Add3, " + 
            
            "dbo.GetStrContentOrderer(Additions, 1, 3) as Add4, " + 

//            "dbo.GetStrContent(t.Name, 1) as RType, " + 

            "* from dbo.Courses c where " + query; //+ " and Type = t.id";
         
         // TODO: need l/r join !
      }

      public void RefreshData()
      {
         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            GetSql(), 
            "Courses");

         dataView.Table = dataSet.Tables["Courses"];
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
         this.CoursesDataList = new DCEAccessLib.DataList();
         this.dataColumnHeader1 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader10 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader2 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader3 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader5 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader4 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader6 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader7 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader8 = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeader9 = new DCEAccessLib.DataColumnHeader();
         this.CourseContextMenu = new System.Windows.Forms.ContextMenu();
         this.CreateMenuItem = new System.Windows.Forms.MenuItem();
         this.Separator1 = new System.Windows.Forms.MenuItem();
         this.EditMenuItem = new System.Windows.Forms.MenuItem();
         this.RemoveMenuItem = new System.Windows.Forms.MenuItem();
         this.Separator2 = new System.Windows.Forms.MenuItem();
         this.VersionMenuItem = new System.Windows.Forms.MenuItem();
         this.DomainMenuItem = new System.Windows.Forms.MenuItem();
         this.ImportMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItemSCORM = new System.Windows.Forms.MenuItem();
         this.ExportMenuItem = new System.Windows.Forms.MenuItem();
         this.Separator3 = new System.Windows.Forms.MenuItem();
         this.RefreshMenuItem = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.menuItemIndex = new System.Windows.Forms.MenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // CoursesDataList
         // 
         this.CoursesDataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.CoursesDataList.AllowColumnReorder = true;
         this.CoursesDataList.AllowSorting = true;
         this.CoursesDataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.CoursesDataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                                      this.dataColumnHeader1,
                                                                                      this.dataColumnHeader10,
                                                                                      this.dataColumnHeader2,
                                                                                      this.dataColumnHeader3,
                                                                                      this.dataColumnHeader5,
                                                                                      this.dataColumnHeader4,
                                                                                      this.dataColumnHeader6,
                                                                                      this.dataColumnHeader7,
                                                                                      this.dataColumnHeader8,
                                                                                      this.dataColumnHeader9});
         this.CoursesDataList.ContextMenu = this.CourseContextMenu;
         this.CoursesDataList.DataView = this.dataView;
         this.CoursesDataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.CoursesDataList.FullRowSelect = true;
         this.CoursesDataList.GridLines = true;
         this.CoursesDataList.HideSelection = false;
         this.CoursesDataList.MultiSelect = false;
         this.CoursesDataList.Name = "CoursesDataList";
         this.CoursesDataList.Size = new System.Drawing.Size(680, 408);
         this.CoursesDataList.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.CoursesDataList.TabIndex = 10;
         this.CoursesDataList.View = System.Windows.Forms.View.Details;
         this.CoursesDataList.DoubleClick += new System.EventHandler(this.EditMenuItem_Click);
         // 
         // dataColumnHeader1
         // 
         this.dataColumnHeader1.FieldName = "RName";
         this.dataColumnHeader1.Text = "Название";
         this.dataColumnHeader1.Width = 450;
         // 
         // dataColumnHeader10
         // 
         this.dataColumnHeader10.FieldName = "Version";
         this.dataColumnHeader10.Text = "Версия";
         // 
         // dataColumnHeader2
         // 
         this.dataColumnHeader2.FieldName = "Duration";
         this.dataColumnHeader2.Text = "Длительность";
         this.dataColumnHeader2.Width = 85;
         // 
         // dataColumnHeader3
         // 
         this.dataColumnHeader3.FieldName = "RAuthor";
         this.dataColumnHeader3.Text = "Автор курса";
         this.dataColumnHeader3.Width = 100;
         // 
         // dataColumnHeader5
         // 
         this.dataColumnHeader5.FieldName = "Active";
         this.dataColumnHeader5.Text = "Активность";
         this.dataColumnHeader5.Width = 80;
         // 
         // dataColumnHeader4
         // 
         this.dataColumnHeader4.FieldName = "CPublic";
         this.dataColumnHeader4.Text = "Публичность";
         this.dataColumnHeader4.Width = 80;
         // 
         // dataColumnHeader6
         // 
         this.dataColumnHeader6.FieldName = "Add1";
         this.dataColumnHeader6.Text = "Дополнение 1";
         this.dataColumnHeader6.Width = 150;
         // 
         // dataColumnHeader7
         // 
         this.dataColumnHeader7.FieldName = "Add2";
         this.dataColumnHeader7.Text = "Дополнение 2";
         this.dataColumnHeader7.Width = 150;
         // 
         // dataColumnHeader8
         // 
         this.dataColumnHeader8.FieldName = "Add3";
         this.dataColumnHeader8.Text = "Дополнение 3";
         this.dataColumnHeader8.Width = 150;
         // 
         // dataColumnHeader9
         // 
         this.dataColumnHeader9.FieldName = "Add4";
         this.dataColumnHeader9.Text = "Дополнение 4";
         this.dataColumnHeader9.Width = 150;
         // 
         // CourseContextMenu
         // 
         this.CourseContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                          this.CreateMenuItem,
                                                                                          this.Separator1,
                                                                                          this.EditMenuItem,
                                                                                          this.RemoveMenuItem,
                                                                                          this.Separator2,
                                                                                          this.VersionMenuItem,
                                                                                          this.DomainMenuItem,
                                                                                          this.ImportMenuItem,
                                                                                          this.menuItemSCORM,
                                                                                          this.ExportMenuItem,
                                                                                          this.menuItemIndex,
                                                                                          this.Separator3,
                                                                                          this.RefreshMenuItem});
         // 
         // CreateMenuItem
         // 
         this.CreateMenuItem.Index = 0;
         this.CreateMenuItem.Text = "Создать";
         this.CreateMenuItem.Click += new System.EventHandler(this.CreateMenuItem_Click);
         // 
         // Separator1
         // 
         this.Separator1.Index = 1;
         this.Separator1.Text = "-";
         // 
         // EditMenuItem
         // 
         this.EditMenuItem.DefaultItem = true;
         this.EditMenuItem.Index = 2;
         this.EditMenuItem.Text = "Редактировать";
         this.EditMenuItem.Click += new System.EventHandler(this.EditMenuItem_Click);
         // 
         // RemoveMenuItem
         // 
         this.RemoveMenuItem.Index = 3;
         this.RemoveMenuItem.Text = "Удалить";
         this.RemoveMenuItem.Click += new System.EventHandler(this.RemoveMenuItem_Click);
         // 
         // Separator2
         // 
         this.Separator2.Index = 4;
         this.Separator2.Text = "-";
         // 
         // VersionMenuItem
         // 
         this.VersionMenuItem.Index = 5;
         this.VersionMenuItem.Text = "Создать новую версию...";
         this.VersionMenuItem.Click += new System.EventHandler(this.VersionMenuItem_Click);
         // 
         // DomainMenuItem
         // 
         this.DomainMenuItem.Index = 6;
         this.DomainMenuItem.Text = "Переместить курс...";
         this.DomainMenuItem.Click += new System.EventHandler(this.DomainMenuItem_Click);
         // 
         // ImportMenuItem
         // 
         this.ImportMenuItem.Index = 7;
         this.ImportMenuItem.Text = "Импортировать курс из файла...";
         this.ImportMenuItem.Click += new System.EventHandler(this.ImportMenuItem_Click);
         // 
         // menuItemSCORM
         // 
         this.menuItemSCORM.Index = 8;
         this.menuItemSCORM.Text = "Импортировать SCORM ...";
         this.menuItemSCORM.Click += new System.EventHandler(this.menuItemSCORM_Click);
         // 
         // ExportMenuItem
         // 
         this.ExportMenuItem.Index = 9;
         this.ExportMenuItem.Text = "Экспортировать курс в файл...";
         this.ExportMenuItem.Click += new System.EventHandler(this.ExportMenuItem_Click);
         // 
         // Separator3
         // 
         this.Separator3.Index = 11;
         this.Separator3.Text = "-";
         // 
         // RefreshMenuItem
         // 
         this.RefreshMenuItem.Index = 12;
         this.RefreshMenuItem.Text = "Обновить";
         this.RefreshMenuItem.Click += new System.EventHandler(this.RefreshMenuItem_Click);
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("ru-RU");
         // 
         // menuItemIndex
         // 
         this.menuItemIndex.Index = 10;
         this.menuItemIndex.Text = "Создать HTML index файл...";
         this.menuItemIndex.Click += new System.EventHandler(this.menuItemIndex_Click);
         // 
         // CoursesList
         // 
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.CoursesDataList});
         this.Name = "CoursesList";
         this.Size = new System.Drawing.Size(680, 408);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void CreateMenuItem_Click(object sender, System.EventArgs e)
      {
         CourseFolderForm form = new CourseFolderForm();
         if (form.ShowDialog() == DialogResult.OK)
         {
            CourseEditNode node = new CourseEditNode(Node, "", this.Node.Id, form.Path);
            node.Select();
         }
      }

      private void EditMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.CoursesDataList.SelectedItems.Count > 0 && 
            this.CoursesDataList.SelectedItems[0].Tag != null)
         {
            DataRowView row = this.CoursesDataList.SelectedItems[0].Tag as DataRowView;

            bool neednew = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(CourseEditNode) )
               {
                  if ( ((CourseEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     node.Select();
                     neednew = false;
                     break;
                  }
               }
            }
            if (neednew)
            {
               CourseEditNode node = new CourseEditNode(Node, row["id"].ToString(), this.Node.Id, null);
               node.Select();
            }
         }
      }

      private void RemoveMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.CoursesDataList.SelectedItems.Count > 0 && 
            this.CoursesDataList.SelectedItems[0].Tag != null)      
         {
            DataRowView row = (DataRowView)this.CoursesDataList.SelectedItems[0].Tag;
            
            bool candelete = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node.GetType() == typeof(CourseEditNode) )
               {
                  if ( ((CourseEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     System.Windows.Forms.MessageBox.Show("Перед удалением курса, закройте его окно редактирования.","Удалить");
                     node.Select();
                     candelete = false;
                     break;
                  }
               }
            }
            if (candelete)
            {
               if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данную запись?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
               {
                  row.Delete();
                  
                  dataSet = DCEAccessLib.DCEWebAccess.WebAccess.UpdateDataSet(
                     GetSql(),
                     "Courses", 
                     ref dataSet);
                  
                  dataView.Table = dataSet.Tables["Courses"];
               }
            }
         }
      }

      private void RefreshMenuItem_Click(object sender, System.EventArgs e)
      {
         RefreshData();
      }

      private void VersionMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.CoursesDataList.SelectedItems.Count > 0 && 
            this.CoursesDataList.SelectedItems[0].Tag != null)
         {
            DataRowView row = this.CoursesDataList.SelectedItems[0].Tag as DataRowView;

            VersionForm form = new VersionForm();

            form.CourseName = row["RName"].ToString();
            form.OldVersion = row["Version"].ToString();

            if (form.ShowDialog() == DialogResult.OK)
            {
               string ver = form.NewVersion;
               string disk = form.CourseDiskFolder;

               DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("exec MakeCourseCopy '" + 
                  
                  Guid.NewGuid().ToString() + "', '" + row["id"].ToString() + 
                  
                  "', '" + ver + "', '" + disk + "'");
               
               RefreshData();
            }
         }
      }

      private void DomainMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.CoursesDataList.SelectedItems.Count > 0 && 
            this.CoursesDataList.SelectedItems[0].Tag != null)
         {
            DataRowView row = this.CoursesDataList.SelectedItems[0].Tag as DataRowView;

            bool canmove = true;

            if ((bool)row["Active"])
            {
               canmove = false;
               MessageBox.Show("Активный курс нельзя перемещать в другую облать", "Сообщение");
            }
            else
            {

               foreach( NodeControl node in this.Node.Nodes)
               {
                  if (node.GetType() == typeof(CourseEditNode) )
                  {
                     if ( ((CourseEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                     {
                        System.Windows.Forms.MessageBox.Show("Перед перемещением курса, закройте его окно редактирования.","Удалить");
                        node.Select();
                        canmove = false;
                        break;
                     }
                  }
               }
            }

            if (canmove)
            {
               DomainForm form = new DomainForm(row["Area"].ToString(), true);

               form.CourseName = row["RName"].ToString();
               form.CourseId = row["id"].ToString();

               if (form.ShowDialog()== DialogResult.OK)
                  RefreshData();
            }
         }
      }

      private void ImportMenuItem_Click(object sender, System.EventArgs e)
      {
         ExportImportService service = new ExportImportService();

         string courseid;
         
         // this method may throw exception
         service.ImportCourse(out courseid, Node.DomainId); 

         this.RefreshData();
      }

      private void ExportMenuItem_Click(object sender, System.EventArgs e)
      {
         if (this.CoursesDataList.SelectedItems.Count > 0 && 
            this.CoursesDataList.SelectedItems[0].Tag != null)
         {
            DataRowView row = this.CoursesDataList.SelectedItems[0].Tag as DataRowView;

            bool canexport = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node is CourseEditNode )
               {
                  if ( ((CourseEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     System.Windows.Forms.MessageBox.Show("Перед экспортированием курса, закройте его окно редактирования.","Удалить");
                     node.Select();
                     canexport = false;
                     break;
                  }
               }
            }

            if (canexport)
            {
               ExportImportService service = new ExportImportService();

               // this method may throw exception
               service.ExportCourse(row["id"].ToString(), row["RName"].ToString());
            }
         }
      }

      private void menuItemSCORM_Click(object sender, System.EventArgs e)
      {
         ExportImportService service = new ExportImportService();

         string courseid;
         
         // this method may throw exception
         service.ImportSCORM(out courseid, Node.DomainId); 

         this.RefreshData();
      }

      private void menuItemIndex_Click(object sender, System.EventArgs e)
      {
         if (this.CoursesDataList.SelectedItems.Count > 0 && 
            this.CoursesDataList.SelectedItems[0].Tag != null)
         {
            DataRowView row = this.CoursesDataList.SelectedItems[0].Tag as DataRowView;

            this.CreateIndex(row["id"].ToString());
         }
      }
      /// <summary>
      /// Создать индексный файл
      /// </summary>
      /// <param name="courseId"></param>
      private void CreateIndex(string courseId)
      {
         string coursePath = DCEAccessLib.DCEUser.CourseRootPath;
         if (coursePath[coursePath.Length-1] != '\\')
            coursePath += "\\";

         DataSet lds = DCEWebAccess.GetdataSet(@"select * from Languages","l");
         foreach (DataRow lrow in lds.Tables["l"].Rows)
         {
            System.Data.DataSet dsCourse = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(@"
            select id, DiskFolder,
               dbo.GetStrContent(Name,"+lrow["id"].ToString()+@" ) as Name,
               dbo.GetStrContent(DescriptionShort, "+lrow["id"].ToString()+@") as DescriptionShort,
               dbo.GetStrContent(DescriptionLong, "+lrow["id"].ToString()+@") as DescriptionLong
            from Courses 
            where id = '"+courseId+"'"
               , "Course");

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(dsCourse.GetXml());
            System.Data.DataTable ct = dsCourse.Tables["Course"];
            if (ct != null && ct.Rows.Count ==1)
            {
               string folder = ct.Rows[0]["DiskFolder"].ToString();
               if (folder == null)
                  folder = "";

               System.Data.DataSet dsThemes = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(@"
            select id, dbo.GetStrContent(Name, "+lrow["id"].ToString()+@") as Name
            from Themes 
            where Parent = '"+courseId+"' and Type=1 order by TOrder"
                  , "Themes");
               System.Xml.XmlDocument tdoc = new System.Xml.XmlDocument();
               tdoc.LoadXml(dsThemes.GetXml());
               foreach (System.Xml.XmlNode theme in tdoc.DocumentElement.ChildNodes)
               {
                  this.addThemes(theme,lrow);
               }
               doc.DocumentElement.InnerXml += tdoc.DocumentElement.InnerXml;
               DCEAccessLib.XmlReports.ProduceReport(doc.OuterXml, "DCECourseEditor.Res.Index.xslt", coursePath+folder+"!Index_"+lrow["Abbr"].ToString().Trim()+".html");
               //MessageBox.Show(this, "Генерация индекса завершена", "Экспорт");
            }
         }
      }

      /// <summary>
      /// Добавление тем и подтем в индексный файл
      /// </summary>
      /// <param name="parent"></param>
      /// <param name="lrow"></param>
      private void addThemes(System.Xml.XmlNode parent, DataRow lrow)
      {
         System.Xml.XmlNode id = parent.SelectSingleNode("id");
         if (id != null)
         {
            string themeId = id.InnerText;

            System.Data.DataSet dsContents = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(@"
               SELECT 
                  ct.DataStr AS url, l.Abbr AS lang, cl.Abbr as DefLang
                  
               FROM  Content ct INNER JOIN Themes t ON ct.eid = t.Content 
                  INNER JOIN Languages l ON ct.Lang = l.id 
                  inner join Courses c on c.id=dbo.CourseofTheme(t.id)
                  INNER JOIN Languages cl ON c.CourseLanguage = cl.id 
               WHERE (t.id = '"+themeId+@"') 
                  AND (l.id = " +lrow["id"].ToString() +@" )
               ORDER BY ct.COrder, ct.Lang"
               , "contentItems");
            System.Xml.XmlDocument cdoc = new System.Xml.XmlDocument();
            cdoc.LoadXml(dsContents.GetXml());
            parent.InnerXml += cdoc.DocumentElement.InnerXml;

            System.Data.DataSet dsThemes = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(@"
               select id, dbo.GetStrContent(Name, " +lrow["id"].ToString() +@") as Name
               from Themes 
               where Parent = '"+themeId+"' and Type=1 order by TOrder"
               , "Themes");

            System.Xml.XmlDocument tdoc = new System.Xml.XmlDocument();
            tdoc.LoadXml(dsThemes.GetXml());
            foreach (System.Xml.XmlNode theme in tdoc.DocumentElement.ChildNodes)
            {
               this.addThemes(theme,lrow);
            }
            parent.InnerXml += tdoc.DocumentElement.InnerXml;
         }
      }
   }
}
