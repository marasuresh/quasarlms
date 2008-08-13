using System;
using DCEAccessLib;
using System.Data;

namespace DCEInternalSystem
{
   /// <summary>
   /// Нода статистики
   /// </summary>
   public class StatNode : ChildNodeListControl
   {
      public StatNode (NodeControl parent)
         : base(parent)
      {
         new StatsTrainingListNode(this);
         new StatsStudentGroupsNode(this);
      }
      public override string GetCaption()
      {
         return "Статистика";
      }
      public override bool HaveChildNodes() { return false; }
   }
}