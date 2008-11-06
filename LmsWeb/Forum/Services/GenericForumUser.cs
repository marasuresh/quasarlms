using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Security.Principal;

namespace N2.Templates.Forum.Services
{
    /// <summary>
    /// Class that represents a forum user. This class integrates the forum with an existing user store
    /// </summary>
    public class GenericForumUser : AbstractN2ForumUser
    {
        #region Variables
        #endregion

        #region Constructor & destructor
        /// <summary>
        /// Initializes a new ForumUser object based on the currently logged on user
        /// </summary>
        public GenericForumUser()
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the ForumUser
        /// </summary>
        protected override void Initialize()
        {
            // Set default values
            _isAuthenticated = false;
            _userName = "";

            try
            {
                // Check wether the user is authenticated
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // Yes, retrieve the user object
                    IIdentity user = HttpContext.Current.User.Identity;

                    // Get the data
                    _userID = 0;
                    _userName = user.Name;
                    _email = user.Name;
                    _location = "";
                    _homePage = "";
                    _isAuthenticated = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to find user info", ex);
            }
        }
        #endregion
    }
}
