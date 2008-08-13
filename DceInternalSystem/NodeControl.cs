using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DCEInternalSystem
{
   /// <summary>
   /// Базовый класс ноды
   /// </summary>
   public class NodeControl : IDisposable
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
      private  System.Collections.ArrayList nodes;
      public   NodeControl NodeParent;
      protected System.Windows.Forms.UserControl fControl;
      public   System.Collections.ArrayList Nodes
      {
         get
         {
            return nodes;
         }
      }

      public TreeNode treeNode=null;

		public NodeControl( NodeControl parent)
		{
         nodes = new System.Collections.ArrayList();
         this.NodeParent = parent;
         if (parent != null)
         {
            this.treeNode = new System.Windows.Forms.TreeNode();
            treeNode.Tag = this;
            treeNode.Text = this.GetCaption();
            parent.Nodes.Add(this);
            parent.treeNode.Nodes.Add(this.treeNode);
            if (this.HaveChildNodes())
            {
               IsExpandedOnce = false;
               treeNode.Nodes.Add("");
            }
            else
               IsExpandedOnce = true;
         }
      }

      protected virtual void DoDispose()
      {

      }
      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      public void Dispose( )
      {
         this.DoDispose();
         if (fControl != null)
         {
            fControl.Dispose();
            fControl = null;
         }
         if (NodeParent != null)
         {
            NodeParent.Select();
            NodeParent.nodes.Remove(this);
            NodeParent.treeNode.Nodes.Remove(this.treeNode);
            NodeParent = null;
         }
         if (nodes != null)
         {
            while (nodes.Count>0)
            {
               ((NodeControl)nodes[0]).Dispose();
            }
            nodes.Clear(); //already removed in foreach loop, but in case...
            nodes = null;
         }

         if (treeNode !=null)
         {
            treeNode.Nodes.Clear();
            treeNode.Tag = null;
            treeNode = null;
         }
      }

      public bool IsExpandedOnce = false;

      public void ExpandTreeNode()
      {
         if (!this.treeNode.IsExpanded)
            this.treeNode.Expand();
      }
      public void Expand()
      {
         if (!IsExpandedOnce)
         {
            IsExpandedOnce = true;
            this.treeNode.Nodes.Clear();
            this.nodes.Clear();
            
            this.CreateChilds();
            ExpandTreeNode();
         }
      }

      public virtual void CreateChilds()
      {

      }
      public virtual bool CanClose()
      {
         return false;
      }


      protected bool needRefresh = false;

      /// <summary>
      /// Уведомляет ноду, что ее контрол при следующем показе должен перечитать данные
      /// </summary>
      public void NeedRefresh()
      {
         needRefresh = true;
      }

      public void Select()
      {
         if (this.treeNode !=null)
         {
            if (this.treeNode.TreeView !=null)
               this.treeNode.TreeView.SelectedNode = this.treeNode;
         }
      }
      /// <summary>
      /// Получить контрол ноды для отображения в форме
      /// </summary>
      /// 
      public virtual System.Windows.Forms.UserControl GetControl()
      {
         return null;
      }

      /// <summary>
      /// Дает возможность ноде освободить ресурсы связанные с контролом
      /// так как он более не нужен
      /// </summary>
      public virtual void ReleaseControl()
      {
         if (this.fControl != null)
         {
            this.fControl.Dispose();
            this.fControl = null;
         }
      }

      /// <summary>
      /// Виртуальный метод возвращения надписи
      /// </summary>
      public virtual String GetCaption()
      {
         return "Виртуальная нода";
      }
   
      /// <summary>
      /// Виртуальный метод сообщающий будет ли у ноды дочерние ноды
      /// </summary>
      public virtual bool HaveChildNodes() { return false; }

      protected System.Windows.Forms.ContextMenu fContextMenu;

      /// <summary>
      /// Возвращает контекстное меню
      /// </summary>
      public virtual System.Windows.Forms.ContextMenu GetPopupMenu()
      {
         return null;
      }

      /// <summary>
      /// Освобождает контекстное меню
      /// </summary>
      public virtual void ReleasePopupMenu()
      {
         if (fContextMenu != null)
            fContextMenu.Dispose();
         fContextMenu = null;
      }
   }

   public class RecordEditNode : NodeControl
   {
      public DataRowView EditRow;
      protected DataSet ds;
      protected DataView dv;

      protected string queryString;
      public string QueryString {get{ return queryString; }}

      protected string tableName;
      public string TableName {get{ return tableName; }}

      protected string id;
      public string Id {get{ return id; }}

      public bool IsNew 
      {
         get
         {
            if (EditRow!=null) 
               return EditRow.IsNew;
            else
               return false;
         }
      }

      /// <summary>
      /// Initialize new record for editing (EditRow)
      /// </summary>
      protected virtual void InitNewRecord()
      {
      }

      /// <summary>
      /// Finish editing, closing the node
      /// </summary>
      public void EndEdit(bool cancel)
      {
         if (cancel)
            EditRow.CancelEdit();
         else
         {
            EditRow.EndEdit();
            DCEAccessLib.DCEWebAccess.WebAccess.UpdateDataSet(this.queryString,this.tableName,ref ds);
            this.NodeParent.NeedRefresh();
         }
         Dispose();
      }

      protected override void DoDispose()
      {
         if (EditRow!=null)
         {
            if (EditRow.IsEdit)
               EditRow.CancelEdit();
            EditRow=null;
         }
         if (dv !=null)
         {
            dv.Dispose();
            ds.Dispose();
            dv=null;
            ds=null;
            queryString = null;
            tableName = null;
            id = null;
         }
         base.DoDispose();
      }

      public RecordEditNode(NodeControl parent, string query, string tablename, string IdField, string _id)
         : base(parent)
      {
         queryString = query;
         tableName = tablename;
         id = _id;

         ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(QueryString,TableName);
         dv = new DataView(ds.Tables[TableName]);

         if (id != null && id.Length>0)
         {
            dv.Sort = IdField;
            DataRowView[] rows = dv.FindRows(id);
            if (rows.Length == 1)
            {
               EditRow = rows[0];
               EditRow.BeginEdit();
            }
         }
         else
         {
            EditRow = dv.AddNew();
            InitNewRecord();
         }

         if (EditRow ==null)
            throw new System.Exception(); 
         this.treeNode.Text = GetCaption();
      }
   }

}
