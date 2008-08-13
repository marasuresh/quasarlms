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
   /// Главная нода, появляется при запуске программы
   /// </summary>
   public class RootNode : ChildNodeListControl
   {
      public override String GetCaption()
      {
         return "Система обучения";
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
      }

      public override void CreateChilds()
      {
         if (DCEUser.CurrentUser.News != DCEUser.Access.No)
            new DCEInternalSystem.NewsListControl(this);
         if (DCEUser.CurrentUser.Users != DCEUser.Access.No)
            new DCEInternalSystem.UsersControl(this);
         //         if (DCEUser.CurrentUser.Courses != DCEUser.Access.No)
         //            new DCEInternalSystem.CoursesControl(this);
		  if (DCEUser.CurrentUser.Students != DCEUser.Access.No)
			  new DCEInternalSystem.StudentsAndGroupsControl(this,false);
		  else
		  {
			  // check if current user is curator of some student group(s)
			  string query =
				  "select count(*) from Groups g,Rights r where g.type ="+ ((int)EntityType.student).ToString()+
				  "and g.id=r.permid and r.eid='"+DCEUser.CurrentUser.id
				  +"' and g.id not in (SELECT Students from dbo.Trainings UNION (SELECT Students from dbo.Tracks))";

			  string ds = DCEAccessLib.DCEWebAccess.WebAccess.GetString(query);
			  if (System.Convert.ToUInt32(ds) > 0)
			  {
				new DCEInternalSystem.StudentsAndGroupsControl(this,true);
			  }
		  }

         // always create trainings node
         new DCEInternalSystem.TrainingAndTrackControl(this);
         if (DCEUser.CurrentUser.Trainings != DCEUser.Access.No)
            new DCEInternalSystem.CTracksControl(this);

         if (DCEUser.CurrentUser.Requests != DCEUser.Access.No)
            new DCEInternalSystem.RequestControl(this);
         new DCEInternalSystem.StatNode(this);
         if (DCEUser.CurrentUser.Questionnaire != DCEUser.Access.No)
            new DCEInternalSystem.GlobalQuestionnaireNode(this);
         new DCEInternalSystem.PersonalProfileNode(this);
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
   }
}