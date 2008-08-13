using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace DCE
{
	/// <summary>
	/// Summary description for ContentPage.
	/// </summary>
	public partial class ContentPage : BaseWebPage
	{
      /// <summary>
      /// ѕолучить корневой каталог материалов курса
      /// </summary>
      public string CoursesRootUrl
      {
         get
         {
            string root = Settings.getValue("Courses/root");
            if (root.Length > 0 && root[root.Length-1] != '/')
               root += "/";

            Guid? studentId = CurrentUser.UserID;
            Guid? trainingId = DCE.Service.TrainingID;
            if(studentId.HasValue && trainingId.HasValue) {
               if (Request.Browser.Browser.ToUpper().IndexOf("IE") > -1)
               {

                  DataSet ds = dbData.Instance.getDataSet(@"
                     select cdPath, useCDLib
                     from CDPath 
                     where studentId='"+studentId+"' and trainingId='"+trainingId+"'",
                     "dataSet", "CDPath");
                  System.Data.DataTable tablePath = ds.Tables["CDPath"];
                  if (tablePath != null && tablePath.Rows.Count == 1 
                     && (bool)tablePath.Rows[0]["useCDLib"] 
                     && tablePath.Rows[0]["cdPath"] != System.DBNull.Value)
                  {
                     System.Uri url = new System.Uri(this.Request.Url, ".");
                     string croot=tablePath.Rows[0]["cdPath"].ToString().Replace("\\","/");
                     if (croot.Length > 0 && croot[croot.Length-1] != '/')
                        croot+= "/";
                  
                     return "file:///"+croot;
                     //                  return "dce://" + "<" 
                     //                     + url.ToString() + root + ">";
                  }
               }
            }

            return root;
         }
      }
      /// <summary>
      /// ѕолучить каталог материалов курса тренинга
      /// </summary>
      public string CourseRoot
      {
         get
         {
            string croot="";
            Guid? trainingId = DCE.Service.TrainingID;
            if(trainingId.HasValue) {

				DataSet ds = dbData.Instance.getDataSet(@"
                     select c.DiskFolder
                     from Courses c inner join Trainings t on c.id=t.Course
                     where t.id='"+trainingId+"'",
                  "dataSet", "Students");
               System.Data.DataTable tableStudents = ds.Tables["Students"];
               if (tableStudents != null && tableStudents.Rows.Count == 1
                  && tableStudents.Rows[0]["DiskFolder"] != System.DBNull.Value)
               {
                  croot=tableStudents.Rows[0]["DiskFolder"].ToString().Replace("\\","/");
                  if (croot.Length > 0 && croot[croot.Length-1] != '/')
                     croot+= "/";
               }
            }

            return croot;
         }
      }
	}
}
