//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\common\Stub_mainmenucontrol_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'common\mainmenucontrol.ascx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;

using System.Text.RegularExpressions;
using System.Web.Security;

namespace DCE.Common
{
	/// <summary>
	/// Главное меню
	/// </summary>
	public partial  class Migrated_MainMenuControl : System.Web.UI.UserControl
	{
      /// <summary>
      /// Активный пункт меню
      /// </summary>
     
      /// <summary>
      /// переключение текущего выбранного пункта главного меню
      /// </summary>
      /// <returns>Активный пункт меню</returns>
//      public int SwitchMenu()
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.Page.ClientScript.RegisterClientScriptInclude(
					this.GetType(),
					"UrlParamScript",
					this.ResolveUrl("~/js/urlparam.js"));
			
			this.Page.ClientScript.RegisterStartupScript(
					this.GetType(),
					"InsertMenuItemSeparators",
					string.Format(
							@"
<script language='JavaScript'>
try {{
	var _tblMenu = $get('{0}');
	for(var _td in _tblMenu.rows[0].cells){{
		var _cell = _tblMenu.rows[0].cells[_td];
		if(''+_cell.innerHTML+''==''
				&& _cell.style.width == '3px') {{
			_cell.className = 'gtsep';
			_cell.innerHTML = '|';
		}}
	}}
}} catch(e){{}};
</script>
",							this.mnMain.ClientID));
		}

		protected void mnLang_MenuItemClick(object sender, MenuEventArgs e)
		{
			string _lang = null;
			switch (e.Item.Value) {
				case "Ukrainian":
					_lang = "ua";
					break;
				case "English":
					_lang = "en";
					break;
				case "Russian":
					_lang = "ru";
					break;
			}
			
			if (!string.IsNullOrEmpty(_lang)) {
				string _url = this.Request.RawUrl;

				if (!string.IsNullOrEmpty(this.Request.QueryString["lang"])) {
					Regex _re = new Regex(@"lang=(?<lang>\w*)");
					_url = _re.Replace(_url, "lang=" + _lang);
				} else {
					if (0 == this.Request.QueryString.Count) {
						_url += "?lang=" + _lang;
					} else {
						_url += "&lang=" + _lang;
					}
				}
				
				this.Response.Redirect(_url);
			}
		}
		protected void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
		{
/*			Login _loginCtl = sender as Login;
			string _login = _loginCtl.UserName;
			
			if (!Membership.ValidateUser(_login, _loginCtl.Password)) {
				if (DceAccessLib.DAL.StudentController.CheckCredentials(_login, _loginCtl.Password)) {
					MembershipUser _mUser = Membership.GetUser(_login);
					DceUser _dceUser = DceUserService.GetUserByLogin(_login);
					if (null == _mUser) {
						_mUser = Membership.CreateUser(_login, _loginCtl.Password, _dceUser.EMail);
					}
				}
			} else {
				DceUser _dceUser = DceUserService.GetUserByLogin(_login);
				if (null == _dceUser) {
					DceUserService.CreateUser(_login);
				}
			}*/
		}

		protected void Login1_LoggedIn(object sender, EventArgs e)
		{
			//Legacy code to support DceUserService.LastEntry
		}

		protected void mnLang_PreRender(object sender, EventArgs e)
		{
			if (!this.IsPostBack) {
				string _lang = this.Page.UICulture.ToLower();
				Menu _menu = sender as Menu;
				foreach (MenuItem _item in _menu.Items) {
					if (_item.Value.Equals(_lang, StringComparison.OrdinalIgnoreCase)) {
						_item.Selected = true;
						break;
					}
				}
			}
		}
}
}
