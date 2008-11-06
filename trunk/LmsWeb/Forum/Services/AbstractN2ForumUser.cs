using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace N2.Templates.Forum.Services
{
    /// <summary>
    /// Class that represents a forum user. This class integrates the forum with an existing user store
    /// </summary>
    public abstract class AbstractN2ForumUser : yaf.IForumUser
    {
        #region Variables
        protected int _userID = 0;
        protected string _userName = "";
        protected string _email = "";
        protected string _location = "";
        protected string _homePage = "";
        protected bool _isAuthenticated = false;
        #endregion

        #region Constructor & destructor
        /// <summary>
        /// Initializes a new ForumUser object based on the currently logged on user
        /// </summary>
        public AbstractN2ForumUser()
        {
            // Initialize
            Initialize();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets whether the user can login (no, user cannot login via the forum)
        /// </summary>
        public bool CanLogin
        {
            get { return false; }
        }

        /// <summary>
        /// Gets whether the user is authenticated
        /// </summary>
        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }

        /// <summary>
        /// Gets the e-mail of the user
        /// </summary>
        public string Email
        {
            get { return _email; }
        }

        /// <summary>
        /// Gets the name of the user
        /// </summary>
        public string Name
        {
            get { return _userName; }
        }

        /// <summary>
        /// Gets the homepage of the user
        /// </summary>
        public object HomePage
        {
            get { return _homePage; }
        }

        /// <summary>
        /// Gets the location of the user
        /// </summary>
        public object Location
        {
            get { return _location; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the ForumUser
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Called when the user is updated
        /// </summary>
        /// <param name="userID">UserID of the user</param>
        public void UpdateUserInfo(int userID)
        {
            // Check for valid user
            if (userID <= 0) return;
            
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
            {
                cmd.CommandText = string.Format("update yaf_User set Email='{0}' where UserID={1}", _email, userID);
                yaf.DB.ExecuteNonQuery(cmd);

                // Make sure to make administrators admin for the forum as well
                if (HttpContext.Current.User.IsInRole("Administrators"))
                {
                    cmd.CommandText = string.Format("update yaf_User set Flags = Flags | 3 where UserID={0}", userID);
                    yaf.DB.ExecuteNonQuery(cmd);
                }
            }
        }
        #endregion
    }
}
