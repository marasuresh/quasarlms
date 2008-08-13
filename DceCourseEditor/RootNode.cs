using System;
using DCEAccessLib;

namespace DCECourseEditor
{
	/// <summary>
	/// Корневая нода редактора курсов
	/// </summary>
   public class RootNode : ChildNodeListControl
   {
      public override String GetCaption()
      {
         return "Конструктор курсов";
      }

      public static RootNode Root = null;
      
      public RootNode(System.Windows.Forms.TreeView treeview)
         : base(null)
      {
         treeview.Nodes.Clear();
         this.treeNode = new System.Windows.Forms.TreeNode();
         treeNode.Tag = this;
         treeNode.Text = this.GetCaption();
         treeview.Nodes.Add(treeNode);
         Root = this;
         this.Expand();
      }

      public override void CreateChilds()
      {
         new CoursesListNode(this, null, "", true);
         new QuestionnaireListNode(this);
         new TypeListNode(this);
         //new CurrencyListNode(this);
      }
      public override bool HaveChildNodes() { return true; }

      public void RecreateNodes()
      {
         while (Nodes.Count>0)
         {
            ((NodeControl)Nodes[0]).Dispose();
         }
         Nodes.Clear(); //already removed in foreach loop, but in case...
         CreateChilds();
      }
      
      public override System.Windows.Forms.UserControl GetControl()
      {
         if (this.fControl == null)
         {
            this.fControl = new ChildNodeList(this.Nodes);
         }
         if (!this.treeNode.IsExpanded)
            this.ExpandTreeNode();
         return this.fControl;
      }
   }
}
