using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCEAccessLib;

namespace DCECourseEditor
{
   public enum ThemeListNodeTypes {tlnMain, tlnWithWork, tlnOther};

   /// <summary>
   /// Список тем
   /// </summary>
   public class ThemeList : System.Windows.Forms.UserControl
   {
      #region Form variables
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;
      private System.Windows.Forms.ContextMenu ThemesContextMenu;
      private System.Windows.Forms.MenuItem CreateMenuItem;
      private System.Windows.Forms.MenuItem menuItem2;
      private System.Windows.Forms.MenuItem EditMenuItem;
      private System.Windows.Forms.MenuItem DeleteMenuItem;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.MenuItem MoveUpMenuItem;
      private System.Windows.Forms.MenuItem MoveDownMenuItem;

      private DCEAccessLib.DataList ThemesDataList;
      private System.Windows.Forms.Button DownButton;
      private System.Windows.Forms.Button UpButton;
      private DCEAccessLib.DataColumnHeader dataColumnHeaderName;
      private DCEAccessLib.DataColumnHeader dataColumnHeaderMandatory;
      private DCEAccessLib.DataColumnHeader dataColumnHeaderDuration;
      private System.Windows.Forms.Panel ButtonPanel;
      private System.Windows.Forms.Panel LeftPanel;
      private System.Windows.Forms.MenuItem CreateWorkMenuItem;
      private System.Data.DataSet dataSet;
      private System.Data.DataView dataView;
      private System.Windows.Forms.MenuItem CopyMenuItem;
      #endregion
      
      private ThemeListNode Node;
      private ThemeListNodeTypes nodeType;
      
      private string ParentId;
		
      public ThemeList()
      {
         InitializeComponent();
      }

      private bool isfreezed = false;
      public ThemeList(ThemeListNode parent, string parentid, ThemeListNodeTypes nodetype)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         Node = parent;

         if (Node.Restricted)
         {
            this.ThemesDataList.Columns.RemoveAt(1);
            this.ThemesDataList.Columns.RemoveAt(1);
            this.CopyMenuItem.Visible = true;
         }

         nodeType = nodetype;

         if (nodeType == ThemeListNodeTypes.tlnWithWork)
         {
            this.ThemesDataList.Columns.RemoveAt(1);
            this.ThemesDataList.Columns.RemoveAt(1);
            this.CreateWorkMenuItem.Visible = true;
         }
         else if (nodeType == ThemeListNodeTypes.tlnMain)
            this.CreateMenuItem.Text = "Создать тему";

         if (DCEUser.CurrentUser.ReadOnlyCourses)
         {
            this.CreateMenuItem.Enabled = false;
            this.DeleteMenuItem.Enabled = false;
            this.CreateWorkMenuItem.Enabled = false;
            this.MoveDownMenuItem.Enabled = false;
            this.MoveUpMenuItem.Enabled = false;
            this.CopyMenuItem.Enabled = false;

            this.DownButton.Enabled = false;
            this.UpButton.Enabled = false;
         }

         CourseEditNode coursenode = (CourseEditNode)Node.GetSpecifiedParentNode("DCECourseEditor.CourseEditNode");
         if ( coursenode != null)
         {
            if (coursenode.IsCourseActive)
            {
               this.UpButton.Enabled = false;
               this.DownButton.Enabled = false;
               this.CreateWorkMenuItem.Enabled = false;
               this.CreateMenuItem.Enabled = false;
               //this.EditMenuItem.Enabled = false;
               this.CopyMenuItem.Enabled = false;
               this.DeleteMenuItem.Enabled = false;
               this.MoveDownMenuItem.Enabled = false;
               this.MoveUpMenuItem.Enabled = false;
               isfreezed = true;
            }
         }
         
         this.DownButton.Text = "\u25bc";
         this.UpButton.Text = "\u25b2";

         ParentId = parentid;

         RefreshData();
      }

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose( bool disposing )
      {
         Parent = null;
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
         this.ThemesContextMenu = new System.Windows.Forms.ContextMenu();
         this.CreateMenuItem = new System.Windows.Forms.MenuItem();
         this.CreateWorkMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem2 = new System.Windows.Forms.MenuItem();
         this.EditMenuItem = new System.Windows.Forms.MenuItem();
         this.DeleteMenuItem = new System.Windows.Forms.MenuItem();
         this.menuItem1 = new System.Windows.Forms.MenuItem();
         this.MoveUpMenuItem = new System.Windows.Forms.MenuItem();
         this.MoveDownMenuItem = new System.Windows.Forms.MenuItem();
         this.dataView = new System.Data.DataView();
         this.dataSet = new System.Data.DataSet();
         this.LeftPanel = new System.Windows.Forms.Panel();
         this.ButtonPanel = new System.Windows.Forms.Panel();
         this.DownButton = new System.Windows.Forms.Button();
         this.UpButton = new System.Windows.Forms.Button();
         this.ThemesDataList = new DCEAccessLib.DataList();
         this.dataColumnHeaderName = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeaderMandatory = new DCEAccessLib.DataColumnHeader();
         this.dataColumnHeaderDuration = new DCEAccessLib.DataColumnHeader();
         this.CopyMenuItem = new System.Windows.Forms.MenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
         this.LeftPanel.SuspendLayout();
         this.ButtonPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // ThemesContextMenu
         // 
         this.ThemesContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                          this.CreateMenuItem,
                                                                                          this.CreateWorkMenuItem,
                                                                                          this.CopyMenuItem,
                                                                                          this.menuItem2,
                                                                                          this.EditMenuItem,
                                                                                          this.DeleteMenuItem,
                                                                                          this.menuItem1,
                                                                                          this.MoveUpMenuItem,
                                                                                          this.MoveDownMenuItem});
         // 
         // CreateMenuItem
         // 
         this.CreateMenuItem.Index = 0;
         this.CreateMenuItem.Text = "Создать подтему";
         this.CreateMenuItem.Click += new System.EventHandler(this.CreateMenuItem_Click);
         // 
         // CreateWorkMenuItem
         // 
         this.CreateWorkMenuItem.Index = 1;
         this.CreateWorkMenuItem.Text = "Создать практическую работу";
         this.CreateWorkMenuItem.Visible = false;
         this.CreateWorkMenuItem.Click += new System.EventHandler(this.CreateWorkMenuItem_Click);
         // 
         // menuItem2
         // 
         this.menuItem2.Index = 3;
         this.menuItem2.Text = "-";
         // 
         // EditMenuItem
         // 
         this.EditMenuItem.DefaultItem = true;
         this.EditMenuItem.Index = 4;
         this.EditMenuItem.Text = "Редактировать";
         this.EditMenuItem.Click += new System.EventHandler(this.EditMenuItem_Click);
         // 
         // DeleteMenuItem
         // 
         this.DeleteMenuItem.Index = 5;
         this.DeleteMenuItem.Text = "Удалить";
         this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
         // 
         // menuItem1
         // 
         this.menuItem1.Index = 6;
         this.menuItem1.Text = "-";
         // 
         // MoveUpMenuItem
         // 
         this.MoveUpMenuItem.Index = 7;
         this.MoveUpMenuItem.Text = "Переместить вверх";
         this.MoveUpMenuItem.Click += new System.EventHandler(this.MoveUpMenuItem_Click);
         // 
         // MoveDownMenuItem
         // 
         this.MoveDownMenuItem.Index = 8;
         this.MoveDownMenuItem.Text = "Переместить вниз";
         this.MoveDownMenuItem.Click += new System.EventHandler(this.MoveDownMenuItem_Click);
         // 
         // dataSet
         // 
         this.dataSet.DataSetName = "NewDataSet";
         this.dataSet.Locale = new System.Globalization.CultureInfo("uk-UA");
         // 
         // LeftPanel
         // 
         this.LeftPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.ButtonPanel});
         this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
         this.LeftPanel.Name = "LeftPanel";
         this.LeftPanel.Size = new System.Drawing.Size(40, 416);
         this.LeftPanel.TabIndex = 0;
         // 
         // ButtonPanel
         // 
         this.ButtonPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
         this.ButtonPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.DownButton,
                                                                                  this.UpButton});
         this.ButtonPanel.Location = new System.Drawing.Point(8, 172);
         this.ButtonPanel.Name = "ButtonPanel";
         this.ButtonPanel.Size = new System.Drawing.Size(24, 72);
         this.ButtonPanel.TabIndex = 0;
         // 
         // DownButton
         // 
         this.DownButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
         this.DownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.DownButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.DownButton.Location = new System.Drawing.Point(0, 38);
         this.DownButton.Name = "DownButton";
         this.DownButton.Size = new System.Drawing.Size(24, 23);
         this.DownButton.TabIndex = 3;
         this.DownButton.Click += new System.EventHandler(this.MoveDownMenuItem_Click);
         // 
         // UpButton
         // 
         this.UpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.UpButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
         this.UpButton.Location = new System.Drawing.Point(0, 11);
         this.UpButton.Name = "UpButton";
         this.UpButton.Size = new System.Drawing.Size(24, 23);
         this.UpButton.TabIndex = 2;
         this.UpButton.Click += new System.EventHandler(this.MoveUpMenuItem_Click);
         // 
         // ThemesDataList
         // 
         this.ThemesDataList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.ThemesDataList.AllowColumnReorder = true;
         this.ThemesDataList.AllowSorting = false;
         this.ThemesDataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.ThemesDataList.Columns.AddRange(new DCEAccessLib.DataColumnHeader[] {
                                                                                     this.dataColumnHeaderName,
                                                                                     this.dataColumnHeaderMandatory,
                                                                                     this.dataColumnHeaderDuration});
         this.ThemesDataList.ContextMenu = this.ThemesContextMenu;
         this.ThemesDataList.DataView = this.dataView;
         this.ThemesDataList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.ThemesDataList.FullRowSelect = true;
         this.ThemesDataList.GridLines = true;
         this.ThemesDataList.HideSelection = false;
         this.ThemesDataList.Location = new System.Drawing.Point(40, 0);
         this.ThemesDataList.MultiSelect = false;
         this.ThemesDataList.Name = "ThemesDataList";
         this.ThemesDataList.Size = new System.Drawing.Size(512, 416);
         this.ThemesDataList.TabIndex = 170;
         this.ThemesDataList.View = System.Windows.Forms.View.Details;
         this.ThemesDataList.DoubleClick += new System.EventHandler(this.EditMenuItem_Click);
         this.ThemesDataList.RowParsed += new DCEAccessLib.DataList.RowParseHandler(this.ThemesDataList_RowParsed);
         // 
         // dataColumnHeaderName
         // 
         this.dataColumnHeaderName.FieldName = "RName";
         this.dataColumnHeaderName.Text = "Название";
         this.dataColumnHeaderName.Width = 300;
         // 
         // dataColumnHeaderMandatory
         // 
         this.dataColumnHeaderMandatory.FieldName = "Mandatory";
         this.dataColumnHeaderMandatory.Text = "Обязательность";
         this.dataColumnHeaderMandatory.Width = 100;
         // 
         // dataColumnHeaderDuration
         // 
         this.dataColumnHeaderDuration.FieldName = "Duration";
         this.dataColumnHeaderDuration.Text = "Длительность";
         this.dataColumnHeaderDuration.Width = 100;
         // 
         // CopyMenuItem
         // 
         this.CopyMenuItem.Index = 2;
         this.CopyMenuItem.Text = "Импортировать подтему";
         this.CopyMenuItem.Visible = false;
         this.CopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
         // 
         // ThemeList
         // 
         this.ContextMenu = this.ThemesContextMenu;
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.ThemesDataList,
                                                                      this.LeftPanel});
         this.Name = "ThemeList";
         this.Size = new System.Drawing.Size(552, 416);
         this.Resize += new System.EventHandler(this.ThemeList_Resize);
         ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
         this.LeftPanel.ResumeLayout(false);
         this.ButtonPanel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      #endregion

      private void CreateMenuItem_Click(object sender, System.EventArgs e)
      {
         // создание новой темы
         if (Node.Restricted)
         {
            LibraryEditNode node = new LibraryEditNode(Node, "", ParentId, "", false);
            node.Select();
         }
         else
         {
            ThemeEditNode node = new ThemeEditNode(Node, "", ParentId, Node.GetChildType(), "", Node.Restricted);
            node.Select();
         }
      }

      private void CreateWorkMenuItem_Click(object sender, System.EventArgs e)
      {
         // создание новой темы
         WorkEditNode node = new WorkEditNode(Node, "", ParentId, "");
         node.Select();
      }

      private void EditMenuItem_Click(object sender, System.EventArgs e)
      {
         if (Node.Nodes.Count == 0)
            Node.CreateChilds();

         // редактирование темы или порактической работы
         if (this.ThemesDataList.SelectedItems.Count>0 && this.ThemesDataList.SelectedItems[0].Tag!=null)
         {
            DataRowView row = this.ThemesDataList.SelectedItems[0].Tag as DataRowView;

            bool neednew = true;
            foreach( NodeControl node in this.Node.Nodes)
            {
               if (node is RecordEditNode )
               {
                  if ( ((RecordEditNode)node).EditRow["id"].ToString() == row["id"].ToString())
                  {
                     node.Select();
                     neednew = false;
                     break;
                  }
               }
            }
            if (neednew)
            {
               if (row["Practice"].ToString() == "")
               {
                  // создание ноды для подтемы
                  if (Node.Restricted)
                  {
                     LibraryEditNode node = new LibraryEditNode(Node, row["id"].ToString(), 
                        row["Parent"].ToString(), row["RName"].ToString(), false);
                     node.Select();
                  }
                  else
                  {
                     ThemeEditNode node = new ThemeEditNode(Node, row["id"].ToString(), 
                        row["Parent"].ToString(), Node.GetChildType(), row["RName"].ToString(), Node.Restricted);
                     node.Select();
                  }
               }
               else
               {
                  // создание ноды для практической работы
                  WorkEditNode node = new WorkEditNode(Node, row["id"].ToString(), 
                     row["Parent"].ToString(), row["RName"].ToString());
                  node.Select();
               }
            }
         }
      }

      private void DeleteMenuItem_Click(object sender, System.EventArgs e)
      {
         // удаление темы
         
         if (this.ThemesDataList.SelectedItems.Count>0 && this.ThemesDataList.SelectedItems[0].Tag!=null)      
         {
            DataRowView row = (DataRowView)this.ThemesDataList.SelectedItems[0].Tag;
            if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите удалить данную запись?","Удалить",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL("delete from Themes where id = '" + row["id"].ToString() + "'");
               foreach (NodeControl node in Node.Nodes)
               {
                  if (node is RecordEditNode)
                  {
                     if (((RecordEditNode)node).Id == row["id"].ToString())
                     {
                        node.Dispose();
                        break;
                     }
                  }
               }
               this.RefreshData();
            }
         }
      }

      private void SwapThemes(int index1, int index2)
      {
         string strid = ((DataRowView)this.ThemesDataList.SelectedItems[0].Tag).Row["id"].ToString();

         DataRowView row1 = this.ThemesDataList.Items[index1].Tag as DataRowView;
         string id1 = row1["id"].ToString();
         int order1 = (int)(row1["TOrder"]);
         DataRowView row2 = this.ThemesDataList.Items[index2].Tag as DataRowView; 
         string id2 = row2["id"].ToString();
         int order2 = (int)(row2["TOrder"]);

         string query = 
            "UPDATE dbo.Themes SET TOrder = " + order2 + " where id = '" + id1 + "' "+
            "UPDATE dbo.Themes SET TOrder = " + order1 + " where id = '" + id2 + "' ";
               
         DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL(query);

         RefreshData();

         foreach (ListViewItem item in this.ThemesDataList.Items)
         {
            if (((DataRowView)item.Tag).Row["id"].ToString() == strid)
            {
               item.Selected = true;
               break;
            }
         }
         
         this.Node.RefreshChilds();
      }
      
      private void MoveUpMenuItem_Click(object sender, System.EventArgs e)
      {
         // перемещение темы вперед по порядку
         
         if (this.ThemesDataList.SelectedItems.Count>0 && this.ThemesDataList.SelectedItems[0].Tag!=null)
         {
            int curindex = this.ThemesDataList.SelectedItems[0].Index;

            // если тему можно переместить вверх на одну позицию
            if (curindex > 0)
               SwapThemes(curindex, curindex-1);
         }
      }

      private void MoveDownMenuItem_Click(object sender, System.EventArgs e)
      {
         // перемещение темы вниз по порядку
         if (this.ThemesDataList.SelectedItems.Count>0 && this.ThemesDataList.SelectedItems[0].Tag!=null)
         {
            int curindex = this.ThemesDataList.SelectedItems[0].Index;
            
            // если тему можно переместить вверх на одну позицию
            if (curindex < this.ThemesDataList.Items.Count-1)
               SwapThemes(curindex, curindex+1);               
         }
      }
      
      public void RefreshData()
      {
         if (DCEUser.CurrentUser.EditableCourses && !isfreezed)
         {
            foreach(MenuItem item in this.ThemesContextMenu.MenuItems)
               item.Enabled = !Node.ParentTheme.IsNew;
         }

         dataSet = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
         
            "select *, dbo.GetStrContentAlt(Name, 'RU', 'EN') as RName " + 
            
            "from Themes " + 
         
            "where Parent = '" + this.Node.Themeid + "' and " + 

            "Type = " + (Node.Restricted ? (int)ThemeType.library : (int)ThemeType.theme).ToString() + 
   
            " ORDER BY TOrder", "Themes");

         dataView.Table = dataSet.Tables["Themes"];
      }

      private void ThemeList_Resize(object sender, System.EventArgs e)
      {
         this.ButtonPanel.SetBounds(
            this.ButtonPanel.Location.X,
            this.LeftPanel.Size.Height/2 - this.ButtonPanel.Size.Height/2,
            this.ButtonPanel.Size.Width,
            this.ButtonPanel.Size.Height
            );
      }

      private void ThemesDataList_RowParsed(System.Data.DataRowView row, System.Windows.Forms.ListViewItem item)
      {
         if (row["Practice"].ToString() == "")
         {
            // тема || подтема
            
            item.Text = (Node.NodeType == ThemeListNodeTypes.tlnMain ? "Тема : " : "Подтема : ") + item.Text;
         }
         else
         {
            // практика
            item.Text = "Практика : " + item.Text;
            item.BackColor = System.Drawing.Color.FromArgb(230,255,255);
         }
      }

      private void CopyMenuItem_Click(object sender, System.EventArgs e)
      {
         CourseEditNode node = (CourseEditNode)this.Node.GetSpecifiedParentNode("DCECourseEditor.CourseEditNode");
         
         if (node != null)
         {
            LibraryImportForm form = new LibraryImportForm(node.Id, Node.Themeid);
            
            form.ShowDialog();

            RefreshData();
         }
      }
   }
   
   /// <summary>
   /// Нода представляющая ноду "Темы" или ноду "Подтемы"
   /// </summary>
   public class ThemeListNode : NodeControl
   {
      public ThemeListNode(NodeControl parent, string parentid, ThemeListNodeTypes nodetype, bool restricted)
         : base(parent)
      {
         ThemeId = parentid;
         nodeType = nodetype;
         
         this.restricted = restricted;

         parentTheme = (RecordEditNode)parent;
         
         this.CaptionChanged();
      }
      
      private RecordEditNode parentTheme;
      public RecordEditNode ParentTheme
      {
         get { return parentTheme; }
      }
      /// <summary>
      /// Флаг, указывающий на ограниченны возможности темы. Флаг 
      /// устанавливается когда нода входит в библиотеку или в словарь
      /// </summary>
      bool restricted;
      public bool Restricted
      {
         get { return restricted; }
      }

      /// <summary>
      /// Идентификатор группы или курса
      /// </summary>
      private string ThemeId = "";
      private ThemeListNodeTypes nodeType;
      
      public string Themeid
      {
         get { return ThemeId; }
      }
      
      public ThemeListNodeTypes NodeType
      {
         get { return nodeType; }
      }

      public override bool HaveChildNodes() { return true; }
      
      public void RefreshChilds()
      {
         // Предназначен для отображения смены порядка нод

         if (Nodes.Count == 0) return;

         System.Data.DataView dv = new System.Data.DataView();
         System.Data.DataSet ds = GetSubthemes(this.ThemeId);
         dv.Table = ds.Tables["Themes"];
         
         int pos = 0;
         
         TreeView tr = this.treeNode.TreeView;
   
         // запретить перерисовку дерева
         tr.BeginUpdate();

         this.treeNode.Nodes.Clear();

         foreach (DataRowView row in dv)
         {
            // попытаться найти ноды для соответствующих идентификаторов

            for (int i=0; i<this.Nodes.Count; i++)
            {
               if (((RecordEditNode)this.Nodes[i]).EditRow["id"].ToString() == row["id"].ToString())
               {
                  this.treeNode.Nodes.Insert(pos, ((TreeNode)((NodeControl)this.Nodes[i]).treeNode));
                  break;
               }
            }
            pos++;
         }

         // разрешить перерисовку дерева
         tr.EndUpdate();
      }

      public ThemeEditNodeTypes GetChildType()
      {
         ThemeEditNodeTypes ret = ThemeEditNodeTypes.tenOther;

         switch (this.nodeType) 
         {
            case ThemeListNodeTypes.tlnMain:
               ret = ThemeEditNodeTypes.tenMain;
               break;
            case ThemeListNodeTypes.tlnWithWork:
            case ThemeListNodeTypes.tlnOther:
               ret =  ThemeEditNodeTypes.tenOther;
               break;
         }
         
         return ret ;
      }

      public override void CreateChilds()
      {
         // создание всех подтем и практических работ для данной темы
         
         System.Data.DataView dv = new System.Data.DataView();
         System.Data.DataSet ds = GetSubthemes(this.ThemeId);
         
         dv.Table = ds.Tables["Themes"];
         
         foreach (DataRowView row in dv)
         {
            // проверка не существует ли уже ноды с таким идентификатором
            bool b = false;
            foreach (object n in this.Nodes)
            {
               if (n is RecordEditNode)
               {
                  RecordEditNode r = n as RecordEditNode;
                  if (r.EditRow["id"].ToString() == row["id"].ToString())
                  {
                     b = true;
                     break;
                  }
               }
            }
            if (b) continue;

            if (row["Practice"].ToString() == "")
            {
               // создание подтемы

               ThemeEditNodeTypes childsType = GetChildType();

               if (restricted)
               {
                  new LibraryEditNode(this, row["id"].ToString(), ThemeId, row["RName"].ToString(), false);
               }
               else
               {
                  new ThemeEditNode(this, row["id"].ToString(), ThemeId, childsType, row["RName"].ToString(), restricted);
               }
            }
            else if (!restricted)
            {
               // создание практической работы
               
               new WorkEditNode(this, row["id"].ToString(), ThemeId, row["RName"].ToString());
            }
         }
      }

      private DataSet GetSubthemes(string id)
      {
         return DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
         
         "select *, dbo.GetStrContentAlt(Name, 'RU', 'EN') as RName, Parent from Themes " + 
         
         "where Parent = '" + id + "' and " + 
      
         "Type = " + (Restricted ? (int)ThemeType.library : (int)ThemeType.theme).ToString() +          

         " ORDER BY TOrder", "Themes");
      }
   

      public override void ChildChanged(NodeControl child)
      {
         System.Data.DataSet ds = GetSubthemes(ThemeId);

         RecordEditNode node = child as RecordEditNode;

         foreach (DataRow row in ds.Tables["Themes"].Rows)
         {
            if (node.Id == row["id"].ToString() )
            {
               if (node is ThemeEditNode)
               {
                  ThemeEditNode t = (ThemeEditNode)node;

                  t.ThemeName = row["RName"].ToString();
               }
               else if (node is WorkEditNode)
               {
                  WorkEditNode w = (WorkEditNode)node;

                  w.WorkName = row["RName"].ToString();
               }
            }
         }
      }

      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            fControl = new ThemeList(this, ThemeId, nodeType);
         }
         
         if (needRefresh)
         {
            ((ThemeList)this.fControl).RefreshData();
            needRefresh = false;
         }

         return this.fControl;
      }

      public override String GetCaption()
      {
         // "Темы"
         // "Подтемы и Практические Работы"
         // "Подтемы"

         string ret = "";

         switch (this.nodeType) 
         {
            case ThemeListNodeTypes.tlnMain:
               ret = "Темы";
               break;

            case ThemeListNodeTypes.tlnWithWork:
               ret = "Подтемы и Практические Работы";
               break;

            case ThemeListNodeTypes.tlnOther:
               ret = "Подтемы";
               break;
         }
         
         return ret;
      }

      public override void ReleaseControl()
      {
         // do nothing
      }
      
      public override bool CanClose()
      {
         return false;
      }
   }
}
