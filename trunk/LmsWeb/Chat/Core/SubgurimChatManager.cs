using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Subgurim.Chat
{
    [ToolboxData("<{0}:SubgurimChatManager runat=server></{0}:SubgurimChatManager>")]
    public class SubgurimChatManager : WebControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            addJS("Ajax");
            addJS("CategoriesAndChannels");
            addJS("GlobalVariables");
            addJS("LoginUser");
            addJS("InsertMessage");
            addJS("JSOO");
            addJS("ReadMessages");
            addJS("ReadUsers");
            addJS("List");
            addJS("ListMessages");
            addJS("ListUsers");
            addJS("ListChannels");
            addJS("Message");
            addJS("Scroll");
            //addJS("Tools");
            addJS("User");
            addJS("xml");

            base.OnPreRender(e);
        }

        protected void addJS(string key)
        {
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(key))
            {
                Page.ClientScript.RegisterClientScriptInclude(
                    key,
                    Page.ClientScript.GetWebResourceUrl(
                        GetType(),
                        "Chat.Scripts." + key + ".js"
                        )
                    );
            }
        }


        protected override void Render(HtmlTextWriter output)
        {
            string retorno = string.Empty;
            if (DesignMode)
            {
                retorno = "<input type=\"button\" value=\"SubgurimChatManager\" />";
            }

            output.WriteLine("<!--");
            output.WriteLine("////////******* Subgurim Chat ******** ///////");
            output.WriteLine("//////// ******* http://chat.subgurim.net ******** ///////");
            output.WriteLine("-->");

            output.WriteLine(retorno);
        }
    }
}