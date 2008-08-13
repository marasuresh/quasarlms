using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DCEAccessLib
{
   /// <summary>
   /// Базовый класс ноды
   /// </summary>
   public class NodeControl : IDisposable
	{
      /// <summary>
      /// Контрол метка, отображающий текущее название ноды
      /// </summary>
      public static System.Windows.Forms.Label NodeLabel = null;
      /// <summary>
      /// Ссылка на текущую выделенную ноду в дереве
      /// </summary>
      protected static NodeControl selectedNode = null;
      /// <summary>
      /// Текущая выделенная нода в дереве
      /// </summary>
      public static NodeControl SelectedNode
      {
         get
         {
            return selectedNode;
         }
         set
         {
            selectedNode = value;

            if (selectedNode != null)
               selectedNode.CaptionChanged();
         }
      }

      /// <summary>
      /// Данные, содержащиеся в ноде не могут быть редактированы
      /// </summary>
      protected bool rdonly = true;
      public bool CanModify
      {
         get 
         {
            return !rdonly;
         }
      }

      public bool Readonly 
      {
         get 
         {
            return rdonly;
         }
      }

      /// <summary>
      /// Уведомление ноды о смене заголовка (текста)
      /// </summary>
      public virtual void CaptionChanged()
      {
         if (NodeControl.SelectedNode == this)
         {
            if (NodeControl.NodeLabel != null)
               NodeControl.NodeLabel.Text = this.GetCaption();
         }
         if (this.treeNode != null)
         {
            this.treeNode.Text = this.GetCaption();
         }
      }
      /// <summary>
      /// Ссылка на родительскую ноду
      /// </summary>
      public NodeControl NodeParent;
      /// <summary>
      /// Ссылка на контрол, связанный с нодой
      /// </summary>
      protected System.Windows.Forms.UserControl fControl;
      /// <summary>
      /// Ссылка на список дочерних нод
      /// </summary>
      private System.Collections.ArrayList nodes;
      /// <summary>
      /// Дочерние ноды
      /// </summary>
      public System.Collections.ArrayList Nodes
      {
         get
         {
            return nodes;
         }
      }

      /// <summary>
      /// Ссылка на ноду элемента управления Treeview
      /// </summary>
      public TreeNode treeNode = null;

		public NodeControl( NodeControl parent)
		{
         nodes = new System.Collections.ArrayList();
         this.NodeParent = parent;
         
         if (!this.HaveChildNodes())
             IsExpandedOnce = true;

         if (parent != null)
         {
            this.treeNode = new System.Windows.Forms.TreeNode();
            treeNode.Tag = this;
            parent.Nodes.Add(this);
            parent.treeNode.Nodes.Add(this.treeNode);
            
            if (this.HaveChildNodes())
               treeNode.Nodes.Add("");
         }
         this.CaptionChanged();
      }
      
      /// <summary>
      /// Виртуальный метод освобождения ресурсов связанных с нодой
      /// </summary>
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
            // Comment by dya
            //NodeParent.Select();

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

      /// <summary>
      /// Флаг, указывающий была ли нода хоть один раз развернута
      /// </summary>
      protected bool IsExpandedOnce = false;

      /// <summary>
      /// Развертывание ноды элемента управления
      /// </summary>
      public void ExpandTreeNode()
      {
         if (!this.treeNode.IsExpanded)
            this.treeNode.Expand();
      }
      
      /// <summary>
      /// Развертывание ноды, создание всех дочерних нод (если это необходимо)
      /// </summary>
      public void Expand()
      {
         if (!IsExpandedOnce)
         {
            IsExpandedOnce = true;
            for (int i=this.treeNode.Nodes.Count-1; i>=0; i--)
            {
               if (this.treeNode.Nodes[i].Tag == null)
               {
                  this.treeNode.Nodes.RemoveAt(i);
                  break;
               }
            }
            this.CreateChilds();
            ExpandTreeNode();
         }
      }

      /// <summary>
      /// Виртуальный метод для создания всех дочерних нод 
      /// при обычном развороте родительской ноды, если нода 
      /// не была развернута до этого момента
      /// </summary>
      public virtual void CreateChilds()
      {

      }
      
      /// <summary>
      /// Сообщает о том является ли данная нода динамической, 
      /// т.е. можно ли ее закрыть кнопкой закрытия окна
      /// </summary>
      public virtual bool CanClose()
      {
         return false;
      }

      /// <summary>
      /// Флаг, указывающий классам наследникам необходимость перерисовки ноды при показе
      /// </summary>
      protected bool needRefresh = false;

      /// <summary>
      /// Уведомление родительской ноды о необходимости 
      /// перерисовки дочерней ноды, указанной в параметре
      /// </summary>
      public virtual void ChildChanged(NodeControl child)
      {
         this.needRefresh = true;
      }
      
      /// <summary>
      /// Уведомление дочерних ноды о необходимости 
      /// изменить свое состояние, зависящее от родительской ноды
      /// </summary>
      public virtual void ParentChanged()
      {
         this.needRefresh = true;
      }
      
      /// <summary>
      /// Уведомляет ноду, что ее контрол при 
      /// следующем показе должен перечитать данные
      /// </summary>
      public void NeedRefresh()
      {
         needRefresh = true;
      }

      /// <summary>
      /// Выделение ноды, текущей становится данная нода
      /// </summary>
      public void Select()
      {
         if (this.treeNode !=null)
         {
            if (this.treeNode.TreeView !=null)
               this.treeNode.TreeView.SelectedNode = this.treeNode;
         }
         NodeControl.SelectedNode = this;
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
      public virtual string GetCaption()
      {
         return "Виртуальная нода";
      }

      public string Caption
      {
         get 
         {
            return GetCaption();
         }
      }
   
      /// <summary>
      /// Виртуальный метод сообщающий о том возможно ли просто развернуть
      /// дочерние ноды, либо для этого необходимо взаимодействовать
      /// с диалоговым окном
      /// </summary>
      public virtual bool HaveChildNodes() { return false; }

      /// <summary>
      /// Коллекция пунктов, отображаемая по нажатию правой клавиши мыши на ноде
      /// </summary>
      protected ArrayList menuItemCollection;

      /// <summary>
      /// Возвращает контекстное меню
      /// </summary>
      public virtual ArrayList GetPopupMenu()
      {
         if (menuItemCollection == null)
            menuItemCollection = new ArrayList();

         menuItemCollection.Clear();

         return menuItemCollection;
      }

      /// <summary>
      /// Освобождает контекстное меню
      /// </summary>
      public virtual void ReleasePopupMenu()
      {
      }
      
      /// <summary>
      /// Get parent node with type Type
      /// </summary>
      public NodeControl GetSpecifiedParentNode(string type)
      {
         NodeControl node = this.NodeParent;

         while (node != null && node.GetType().ToString() != type)
            node = node.NodeParent;

         return node;
      }
   }
}
