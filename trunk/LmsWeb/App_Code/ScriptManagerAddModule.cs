using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ListSearch
{
   public class ScriptManagerAddModule : IHttpModule
   {
      void IHttpModule.Dispose() { }

      // This is where you can indicate what events in the request
      /// processing lifecycle you want to intercept
      void IHttpModule.Init(System.Web.HttpApplication context)
      {
         /*context.PreRequestHandlerExecute +=
            new EventHandler(HttpApplication_PreRequestHandlerExecute);*/
      }

      void HttpApplication_PreRequestHandlerExecute(object sender,
                                                    EventArgs e)
      {
         HttpApplication httpApplication = sender as HttpApplication;

         if (httpApplication != null)
         {
            Page page = httpApplication.Context.CurrentHandler as Page;
            if (page != null)
            {
               // When standard ASP.NET Pages are being used to handle
               // the request, intercept the PreInit phase of the
               // page's lifecycle because this is where we should
               // dynamically create controls
               page.PreInit += new EventHandler(Page_PreInit);
            }
         }
      }

      void Page_PreInit(object sender, EventArgs e)
      {
         Page page = sender as Page;
         if (page != null)
         {
            // ScriptManagers must be in forms -- look for forms
            foreach (Control control in page.Controls)
            {
               HtmlForm htmlForm = control as HtmlForm;
               if (htmlForm != null)
               {
                  // Look for an existing ScriptManager or a
                  // ScriptManagerProxy
                  bool foundScriptManager = false;
                  foreach (Control htmlFormChild in htmlForm.Controls)
                  {
                     if (htmlFormChild is ScriptManager ||
                         htmlFormChild is ScriptManagerProxy)
                     {
                        foundScriptManager = true;
                        break;
                     }
                  }

                  // If we didn't find a script manager or a script
                  // manager proxy, add one
                  if (!foundScriptManager)
                  {
                     htmlForm.Controls.Add(new ScriptManager());
                  }
               }
            }
         }
      }
   }
}