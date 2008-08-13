using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.SessionState;
using System.Collections;
using System.Collections.Generic;

using System.Web.Security;

namespace DCE
{
    /// <summary>
    /// Базовый класс Web формы для проекта
    /// </summary>
    public partial class BaseWebPage : Page
    {
		protected bool isLoginOk {
			get {
				MembershipUser _mUser = CurrentUser.GetCurrentMembershipUserSafe();
				return null != _mUser;
			}
		}

        /// <summary>
        /// указатель на LeftMenu контрол
        /// </summary>
        protected DCE.Common.LeftMenu leftMenu = null;
    }
}