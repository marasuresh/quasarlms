using System;  
using DCEAccessLib;
using System.Data;


namespace DCEInternalSystem
{
	/// <summary>
	/// Summary description for TrainingWorks.
	/// </summary>
	public class TrainingWorksNode : ChildNodeListControl
	{
      public TrainingWorksNode(NodeControl parent, string trainingId)
         : base(parent)
      {

         new TrainingTasksNode(this,trainingId);
         new TrainingForumNode(this,trainingId);

         bool CanModify = DCEUser.CurrentUser.Trainings != DCEUser.Access.No;

         DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
            "SELECT tr.id from Trainings tr, GroupMembers gm where gm.id='"
            +DCEUser.CurrentUser.id+
            "' and gm.MGroup = tr.Curators and tr.id='"+ trainingId +"'","tr");
         if (ds.Tables["tr"].Rows.Count>0)
         {
            CanModify = true;
         }

         if (CanModify)
         {
            new TrainingTestsNode(this,trainingId);
            new TrainingBlockingNode(this,trainingId);
         }
      }
      public override string GetCaption()
      {
         return "Работа со студентами";

      }
      public override bool HaveChildNodes() { return false; }

	}
}
