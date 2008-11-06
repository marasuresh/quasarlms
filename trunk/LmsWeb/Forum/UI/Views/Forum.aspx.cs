using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace N2.Templates.Forum.UI.Views
{
    public partial class Forum : N2.Templates.Web.UI.TemplatePage<Items.Forum>
    {
        #region Variables
        #endregion

        #region Constructor & destructor
        #endregion

        #region Properties
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            /*try
            {
                forum.BoardID = int.Parse(Settings["forumboardid"].ToString());

                string cID = Settings["forumcategoryid"].ToString();
                if (cID != string.Empty)
                    Forum1.CategoryID = int.Parse(cID);
            }
            catch (Exception)
            {
                Forum1.BoardID = 1;
            }

            // Put user code to initialize the page here
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                DotNetNuke.Entities.Users.UserInfo userInfo;
                DotNetNuke.Entities.Portals.PortalSettings _portalSettings = (DotNetNuke.Entities.Portals.PortalSettings)HttpContext.Current.Items["PortalSettings"];
                userInfo = DotNetNuke.Entities.Users.UserController.GetUser(_portalSettings.PortalId, this.UserId, false);

                m_userID = userInfo.UserID;
                m_userName = userInfo.Username;
                m_email = userInfo.Membership.Email;

                if (m_userID == _portalSettings.AdministratorId)
                {
                    /*
                    try
                    {
                        DataRow row = DB.dnn_board_match(m_portalid); 
                        int brdid = (int)row["boardID"];
                        //There is a match so do nothing and continue
                    }
                    catch(Exception)
                    {
                        //Create a new board
                        //Find out last boardID created
                        DataTable tbl = DB.board_list(null);
                        int numboards = tbl.Rows.Count;
                        numboards +=1;
                        string newBoardName = "New Board " + numboards.ToString();
                        DB.board_create(newBoardName,false,m_userName,m_email,"password",m_portalid);
                    }
                    */
            /*    }
            }*/
        }

        public void Page_Error(object sender, System.EventArgs e)
        {
            Exception x = Server.GetLastError();
            string exceptionInfo = "";
            while (x != null)
            {
                exceptionInfo += DateTime.Now.ToString("g");
                exceptionInfo += " in " + x.Source + "\r\n";
                exceptionInfo += x.Message + "\r\n" + x.StackTrace + "\r\n-----------------------------\r\n";
                x = x.InnerException;
            }
            yaf.DB.eventlog_create(forum.PageUserID, this, exceptionInfo);
            yaf.Utils.LogToMail(x);
        }
        #endregion
    }
}
