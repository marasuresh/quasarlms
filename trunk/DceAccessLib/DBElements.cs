using System;
using System.Data;
using System.Data.SqlClient;

namespace DCEAccessLib
{
   public enum EntityType
   {
      // note, these values is used in database with different triggers
      // do not modify them ever!
      student = 1,
      user = 2,
      content = 3,
      questionnaire = 4,
      test = 5,
      course = 6,
      courserequest = 7,
      training = 9,
      group = 10,
      theme = 11,
      task = 12,
      track = 13,
      coursedomain = 14,
      globalquestionnaire = 15
   };

   public enum ContentType
   {
      /// <summary>
      /// Plain text nvarchar255
      /// </summary>
      _string = 1,      
      /// <summary>
      /// Xml content max length 4000, saved in TData
      /// </summary>
      _xml  =   2,      
      /// <summary>
      /// Ambigious object nvarchar255
      /// </summary>
      _object = 3 ,
      /// <summary>
      /// image data
      /// </summary>
      _image = 4,        // binary image 
      /// <summary>
      /// Url locator nvarchar255
      /// </summary>
      _url = 5,
      /// <summary>
      /// html content max length 4000, saved in TData
      /// </summary>
      _html = 6,
      /// <summary>
      /// Email template (DataStr - template name,  TData - template xsl
      /// </summary>
      _emailTemplate = 7,
      /// <summary>
      /// Long string max length 4000, saved in TData
      /// </summary>
      _longString = 8
   }

   public enum TestType
   {
      test = 1,         
      practice = 2,
      // all questionnaire x%3=0
      questionnaire = 3,
      globalquestionnaire = 6
   }
   

   public enum HintType
   {
      none = 1,         
      one = 2,
      both = 3
   }

   public enum ThemeType
   {
      theme = 1,         
      library = 2,
   }

   public class TaskComplete
   {
      public const int NotChecked = 0;
      public const int Incomplete = 1;
      public const int Incorrect = 2;
      public const int Partially = 3;
      public const int Almost = 4;
      public const int Right = 5;
   }

   #region class Rights
   /// <summary>
   /// ¬спомогательный класс доступа к правам
   /// </summary>
   class Rights
   {
      enum type
      {
         unknown = 0,


      };
      public bool read, write, delete; 
      public int mod;
      
      Rights() { read=false; write=false; delete=false; 
         mod = 0;
      }

      public static bool Get(string id, string RequestedId, ref Rights rights)
      {

         string query =
            "         SELECT     r.[read], r.[write], r.[delete], e1.Parent AS e1parent, e2.Parent AS e2parent" +
            "         FROM         Entities e1 LEFT OUTER JOIN" + 
            "                      Rights r ON r.id = e1.id LEFT OUTER JOIN" +
            "                      Entities e2 ON r.eid = e2.id" +
            "         WHERE     (e1.id = '"+id+"') AND (e2.id = '"+ RequestedId + "')";

         System.Data.DataSet dset = DCEWebAccess.WebAccess.GetDataSet(query,"Rights");
         DataTable table = dset.Tables["Rights"];

         bool found = false;

         if ( table.Rows[0][0] != null )
         {
            rights.read = table.Rows[0][0].ToString() == "1";
            rights.write = table.Rows[0][1].ToString() == "1";
            rights.delete = table.Rows[0][2].ToString() == "1";
            return true;
         }
         else
         {
            if (table.Rows[0][4] != null)
            {
               found = Get(id, table.Rows[0][4].ToString(), ref rights);
               if (!found)
                  found = Get(table.Rows[0][3].ToString(),RequestedId,ref rights);
            }
            else
            if (table.Rows[0][3] != null )
               found = Get(table.Rows[0][3].ToString(),RequestedId, ref rights);
         }
         return found;
      }

      public static bool CanRead(string id, string RequestedId)
      {
         Rights r = new Rights();
         bool found = Get(id,RequestedId,ref r);
         
         if (!found)
            return false;
         else
            return r.read;
      }

      public static bool CanWrite(string id, string RequestedId)
      {
         Rights r = new Rights();
         bool found = Get(id,RequestedId,ref r);
         
         if (!found)
            return false;
         else
            return r.write;
      }

      public static bool CanDelete(string id, string RequestedId)
      {
         Rights r = new Rights();
         bool found = Get(id,RequestedId,ref r);
         
         if (!found)
            return false;
         else
            return r.delete;
      }

   };
   #endregion

   #region class Settings
   public class Settings
   {
      public static string DateFormat = "d";
      public static string DateTimeFormat = "dd.MM.yyyy HH:mm:ss";

      public static string ToSQLDate(DateTime date)
      {
         return date.ToString("s", System.Globalization.DateTimeFormatInfo.InvariantInfo);
      }
      public static string DCEWebAddr ="";

      public static void LoadSettings()
      {
		 DCEWebAddr = global::DCEAccessLib.Properties.Settings.Default.DCEWeb;
      }
      public static bool validateEmail(string email)
      {
         if (email == null || email == "")
            return false;
         int i = email.IndexOf('@');
         if (i <=0 || i == email.Length-1)
            return false;
         else if (email.IndexOf('@', i+1) >= 0)
         {
            return false;
         }
         if (email.IndexOfAny(new char[]{'%', '$', '&', '^', '!', '~', '\'', '\"', '/', '\\', '*', ',', '{', '}', ':', ';', '<', '>', '?', '[', ']', '+', '='}) >= 0)
            return false;
         return true;
      }
   }
   #endregion
}

