using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using Microsoft.Win32;

namespace DCEAccessLib
{
   /// <summary>
   /// Текущие установки пользователя
   /// </summary>
   public class DCEUser
   {
      public static DCEUser CurrentUser = new DCEUser();

      public enum Access
      {
         No = 0,
         View = 1,
         Modify = 2
      }

      /// <summary>
      /// user id
      /// </summary>
      public string id; // user id
      /// <summary>
      /// user login
      /// </summary>
      public string Login; // user login
      /// <summary>
      /// флаг автоизации
      /// </summary>
      public bool Authorized;

      private static string courseRootPath;
      /// <summary>
      /// Корневой каталог материалов курсов
      /// </summary>
      static public string CourseRootPath {
         get {
			DCEUser.courseRootPath = global::DCEAccessLib.Properties.Settings.Default.CoursesRoot;
			return courseRootPath;
         }
         set {
            
            DCEUser.courseRootPath = value;
            global::DCEAccessLib.Properties.Settings.Default.CoursesRoot = DCEUser.courseRootPath;
         }
      }

      public Access Users = Access.No;
      public Access Students = Access.No;
      public Access Courses = Access.No;
      public Access Trainings = Access.No;
      public Access Requests = Access.No;
      public Access Shedule = Access.No;
      public Access Tests = Access.No;
      public Access Questionnaire = Access.No;
      public Access News = Access.No;

      /// <summary>
      /// Просмотр курсов
      /// </summary>
      public bool ReadOnlyCourses
      {
         get { return DCEUser.CurrentUser.Courses == DCEUser.Access.View; }
      }

      /// <summary>
      /// Возможность редактирования курсов
      /// </summary>
      public bool EditableCourses
      {
         get { return DCEUser.CurrentUser.Courses == DCEUser.Access.Modify; }
      }

      private Access RtoA (string R, int pos)
      {
         char c = R[pos];
         if (c == 'W')
            return Access.Modify;
         if (c== 'R')
            return Access.View;
         return Access.No;
      }

      /// <summary>
      /// Начальная установка прав
      /// </summary>
      /// <param name="access">строка прав из БД</param>
      
		public void FillAccessRights(string access)
		{
			//Override old-style explicit permissions with a new role-based permissions
			SecurityServices.UserManager _usrMgr = new DCEAccessLib.SecurityServices.UserManager();
			if (_usrMgr.IsInRole(DCEUser.CurrentUser.Login, "Administrator")) {
				access = "WWWWWWWWW";
			}
			
			this.Users = RtoA(access, 0);
			this.Courses = RtoA(access, 1);
			this.Trainings = RtoA(access, 2);
			this.Requests = RtoA(access, 3);
			this.Shedule = RtoA(access, 4);
			this.Tests = RtoA(access, 5);
			this.Questionnaire = RtoA(access, 6);
			this.Students = RtoA(access, 7);
			this.News = RtoA(access, 8);
      }

      public bool isInstructor(string trainingId)
      {
         try
         {
            DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               "SELECT Count(*) from Trainings tr, GroupMembers gm where gm.id='"
               +this.id+
               "' and gm.MGroup = tr.Instructors and tr.id='"+ trainingId +"'","tr");

            return ((int)ds.Tables["tr"].Rows[0][0] >0);
         }
         catch
         {
            return false;
         }
      }

      public bool isCurator(string trainingId)
      {
         try
         {
            new System.Guid(trainingId);
            DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               "SELECT Count(*) from Trainings tr, GroupMembers gm where gm.id='"
               +this.id+
               "' and gm.MGroup = tr.Curators and tr.id='"+ trainingId +"'","tr");
            return ((int)ds.Tables["tr"].Rows[0][0] >0);
         }
         catch
         {
            return false;
         }
      }

      public bool isCuratorOrInstructor(string trainingId)
      {
         try
         {
            DataSet ds = DCEAccessLib.DCEWebAccess.WebAccess.GetDataSet(
               "SELECT Count(*) from Trainings tr, GroupMembers gm where gm.id='"
               +this.id+
               "' and (gm.MGroup = tr.Instructors or gm.MGroup = tr.Curators) and tr.id='"+ trainingId +"'","tr");

            return ((int)ds.Tables["tr"].Rows[0][0] >0);
         }
         catch
         {
            return false;
         }
      }
   }
}