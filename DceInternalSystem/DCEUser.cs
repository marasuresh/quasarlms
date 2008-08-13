using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;

namespace DCEInternalSystem
{
   public class DCEUser
   {
      public static DCEUser CurrentUser = new DCEUser();

      public enum Access
      {
         No = 0,
         View = 1,
         Modify = 2
      }

      public bool Authorized = false;
      public Access Users = Access.No;
      public Access Students = Access.No;
      public Access Courses = Access.No;
      public Access Trainings = Access.No;
      public Access Requests = Access.No;
      public Access Shedule = Access.No;
      public Access Tests = Access.No;
      public Access Questionnaire = Access.No;
   }
}