using System;
using System.Data;
using System.Data.SqlClient;

namespace DCEAccessLib
{
   #region class Settings
   public sealed class Settings
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
         Microsoft.Win32.RegistryKey key = 
            Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\DCE");

         DCEWebAddr = key.GetValue("DCEWeb","http://").ToString();
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

