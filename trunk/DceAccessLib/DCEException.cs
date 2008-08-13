using System;
using System.Windows.Forms;
using System.Threading;

namespace DCEAccessLib
{
	/// <summary>
	/// Обработка собственных исключений системы
	/// </summary>
	public class DCEException : System.Exception
	{
      public enum ExceptionLevel
      {
         InvalidAction = 0, // user actions are cause of exceptions , continue
         ErrorContinue = 1, // non-critical error, continue
         ErrorCritical = 2  // critical error, abort program
      }

      protected ExceptionLevel Level;
      public DCEException(string message, ExceptionLevel level)
         : base(message)
      {
         this.Level = level;
      }

      public static void ShowMessage(string message, string caption, string stack)
      {
         MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
//         DCEAccessLib.ShowExceptionForm d = new DCEAccessLib.ShowExceptionForm();
//         d.Message = message;
//         d.Caption = caption;
//         d.Stack = stack;
//         d.ShowDialog();
      }
      public void ShowMessage()
      {
         string title = "Ошибка";
         MessageBoxIcon icon = MessageBoxIcon.Stop;
         switch (this.Level)
         {
            case ExceptionLevel.InvalidAction:
               icon = MessageBoxIcon.Exclamation;
               break;
            case ExceptionLevel.ErrorContinue:
               icon = MessageBoxIcon.Error;
               break;
            case ExceptionLevel.ErrorCritical:
               title = "Критическая ошибка";
               icon = MessageBoxIcon.Stop;
               break;
            default:
               icon = MessageBoxIcon.Stop;
               break;
         }
         MessageBox.Show(Message, title, MessageBoxButtons.OK, icon);
      }

      /// <summary>
      /// Глобальный обработчик исключений
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="t"></param>
      public static void OnThreadException(object sender, ThreadExceptionEventArgs t) 
      {
         string caption = "Ошибка";
         string message = "Ошибка:";

         if (t.Exception is DCEException)
         {
            ((DCEException)t.Exception).ShowMessage();
            
            if (((DCEException)t.Exception).Level == ExceptionLevel.ErrorCritical)
            {
               Application.Exit();
            }
         }
         else
         {
            if (t.Exception is System.Net.WebException)
            {
               caption = "Ошибка подключения";
               message = "Подключение к Web сервису не возможно";
               System.Net.WebException e = (System.Net.WebException) t.Exception;
               switch (e.Status)
               {
                  case System.Net.WebExceptionStatus.ProtocolError:
                     if (e.Response.GetType() == typeof(System.Net.HttpWebResponse))
                     {
                        System.Net.HttpWebResponse response 
                           = (System.Net.HttpWebResponse) e.Response;
                        switch (response.StatusCode)
                        {
                           case System.Net.HttpStatusCode.UseProxy:
                           case System.Net.HttpStatusCode.Forbidden:
                           case System.Net.HttpStatusCode.ProxyAuthenticationRequired:
                           case System.Net.HttpStatusCode.Unauthorized:
                              message = "Проверьте правильность настроек подключения к Web сервису";
                              break;
                           case System.Net.HttpStatusCode.NotFound:
                              message = "Web сервис не найден";
                              break;
                           default:
                              break;
                        }
                     }
                     break;
                  case System.Net.WebExceptionStatus.ConnectFailure:
                  case System.Net.WebExceptionStatus.ConnectionClosed:
                  default:
                     break;
               }
            }
            else if (t.Exception is System.InvalidOperationException)
            {
               caption = "System.InvalidOperationException";
               message = "Подключение к Web сервису не возможно:";
               message +=  "\r\n" 
                  + t.Exception.Message;
               caption = "System.ApplicationException";
            }
            else if (t.Exception is System.ApplicationException)
            {
               message = "Ошибка:";
               message +=  "\r\n" 
                  + t.Exception.Message;
               caption = "System.ApplicationException";
            }
            else
            {
               message = "Ошибка:";
               message +=  "\r\n" 
                  + t.Exception.Message;
               caption = "System.Exception";
#if DEBUG
#else
            //return;
#endif
            }
            string stack = "";
#if DEBUG
            if (t.Exception.StackTrace != null)
               stack = t.Exception.StackTrace;
#endif
            ShowMessage(message, caption, stack);
         }
      }
   }
}
