using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections;
using System.Xml;
using System.Globalization;
using System.Threading;
using System.IO;

using DCE.Common;

namespace DCE
{
	/// <summary>
	/// Summary description for Service
	/// </summary>
	public static partial class Service
	{
		/// <summary>
		/// Получение текущего языка интерфейса
		/// </summary>
		/// <param name="pg"></param>
		/// <returns></returns>
		[Obsolete("Added for DCE compatibility")]
		internal static string getCurrentLanguage()
		{
			string _lang = HttpContext.Current.Session["dceLanguage"] as string;
			CultureInfo _culture;
			string _code;

			if(!string.IsNullOrEmpty(_lang)) {
				try {
					_culture = CultureInfo.GetCultureInfo(_lang.ToLower().Replace("ua", "uk"));
					_code = _culture.TwoLetterISOLanguageName.ToLower().Replace("uk", "ua");

					/// Update language string to a more precise value
					if(!_code.Equals(_lang, StringComparison.InvariantCultureIgnoreCase)) {
						HttpContext.Current.Session["dceLanguage"] = _code;
					}

					return _code;
				} catch(ArgumentException) {
				}
			}
			
			HttpCookie _cookie = HttpContext.Current.Request.Cookies["dceLanguage"];
			
			if(null == _cookie) {
				_cookie = new HttpCookie("dceLanguage", LocalisationService.DefaultLanguage);
				HttpContext.Current.Response.Cookies.Add(_cookie);
			}

			try {
				_culture = CultureInfo.GetCultureInfo(_cookie.Value.ToLower().Replace("ua", "uk"));
				_code = _culture.TwoLetterISOLanguageName.ToLower().Replace("uk", "ua");
				HttpContext.Current.Session["dceLanguage"] = _code;
			} catch(ArgumentException) {
				_code = "ru";
				HttpContext.Current.Session["dceLanguage"] = _code;
				_cookie.Value = _code;
			}

			return _code;
		}

		/// <summary>
		/// Путь к текщей языковой директории
		/// </summary>
		/// <param name="pg"></param>
		/// <returns></returns>
		[Obsolete("Added for DCE compatibility")]
		public static string GetLanguagePath(System.Web.UI.Page pg)
		{
			return DCE.Settings.getValue("dceLangPath")
					+Service.getCurrentLanguage() + "\\";
		}
		
		/// <summary>
		/// Установить язык интерфейса
		/// </summary>
		/// <param name="lang"></param>
		[Obsolete("Added for DCE compatibility")]
		public static void SetLanguage(string lang)
		{
			HttpContext.Current.Session["dceLanguage"] = lang;
			HttpContext.Current.Response.Cookies.Add(new HttpCookie("dceLanguage", lang));
			HttpContext.Current.Session["dceLangPath"] = DCE.Settings.getValue("dceLangPath")
				+lang+"\\";
		}

		/// <summary>
		/// Загрузить xml документ
		/// </summary>
		/// <param name="pg"></param>
		/// <param name="doc"></param>
		/// <param name="path">имя файла</param>
		[Obsolete("Added for DCE compatibility")]
		public static void LoadXmlDoc(System.Web.UI.Page pg, System.Xml.XmlDocument doc, string path)
		{
			string langPath = GetLanguagePath(pg);
			try {
				doc.Load(pg.MapPath(@"~\"+langPath + "xml\\" + path));
			} catch (System.IO.FileNotFoundException) {
				doc.Load(pg.MapPath(@"~\" + DCE.Service.DefaultLangPath + "xml\\" + path));
			}
		}
		
		/// <summary>
		/// Возвращает XML с языкозависимым контентом страницы
		/// </summary>
		/// <param name="fName">имя файла xml без пути и расширения</param>
		/// <param name="pg">ссылка на страницу</param>
		/// <returns>xml</returns>
		[Obsolete("Added for DCE compatibility")]
		public static string getLanguageXML(string fName, System.Web.UI.Page pg)
		{
			string lngPath = DCE.Service.GetLanguagePath(pg);
			lngPath +="\\xml\\"+fName+".xml";
			lngPath = pg.Server.MapPath(lngPath);
			
			XmlDocument docLng = new XmlDocument();
			string _altPath = pg.MapPath(@"~\" + DCE.Service.DefaultLangPath + "\\xml\\" + fName + ".xml");
			
			try {
				docLng.Load(lngPath);
			} catch (FileNotFoundException) {
				docLng.Load(_altPath);
			} catch (DirectoryNotFoundException) {
				docLng.Load(_altPath);
			}
			
			return docLng.InnerXml;
		}
		
		/// <summary>
		/// Аутентификация студента
		/// </summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		/// <returns>true - валидный логин/пароль</returns>
/*		[Obsolete("Added for DCE compatibility")]
		public static bool Registration(string login, string password)
		{
			bool _result = false;
			MembershipUser _mUser = Membership.GetUser(login);
			DceUser _dceUser = DceAccessLib.DAL.StudentController.GetByLogin(login);
			if(Membership.ValidateUser(login, password)) {
				if(null == _dceUser) {
					///create missing Dce record
					///This means that user was created from Membership for the first time
					DceAccessLib.DAL.StudentController.Insert(login, _mUser.Email);
					_dceUser = DceAccessLib.DAL.StudentController.GetByLogin(login);
				}
				_result = true;
			} else {
				//Migrate existing valid user to membership
				if(DceAccessLib.DAL.StudentController.CheckCredentials(login, password)) {
					if(null == _mUser) {
						_mUser = Membership.CreateUser(login, password, _dceUser.EMail);
						_result = Membership.ValidateUser(login, password);
					}
				}
			}

			if(_result) {
				//Legacy Dce logic
				DCE.Service.SetCook("ValidLogin");
				DCE.Service.SetCook("LastEntry");

				CurrentUser.SetAuthenticatedUser(
						_dceUser.ID,
						login,
						_dceUser.FirstName
							+(!string.IsNullOrEmpty(_dceUser.FirstName) && !string.IsNullOrEmpty(_dceUser.LastName)
								? " "
								: string.Empty)
							+_dceUser.LastName,
						_dceUser.EMail,
						null,
						null);
			}
			
			return _result;
		}
*/	}

		/// <summary>
		/// Базовый класс Web Control для проекта
		/// </summary>
		public abstract partial class BaseTrainingControl : BaseWebControl
		{
			protected string defLang {
				get {
					string _value = this.Session["CourseLanguage"] as string;
					return !string.IsNullOrEmpty(_value) ? _value : LocalisationService.DefaultLanguage;
				}
			}
		}

	/// <summary>
	/// Базовый класс Web Control для проекта
	/// </summary>
	public abstract partial class BaseWebControl : UserControl
	{
		#region xsl2asp
		XmlDocument m_doc;

		protected virtual XmlDocument doc {
			get {
				if (null == this.m_doc) {
					this.m_doc = new XmlDocument();
				}
				return this.m_doc;
			}
		}
		
		protected string SelectScalar(string xPathExpr)
		{
			return SelectScalar(this.doc, xPathExpr);
		}
		
		protected static string SelectScalar(object dataItem, string xPathExpr)
		{
			if (null == dataItem) {
				return string.Empty;
			}

			XmlNode _node = ((XmlNode)dataItem).SelectSingleNode(xPathExpr);
			return null == _node ? string.Empty : _node.InnerXml;
		}
		
		/// <summary>
		/// Extract boolean value either from a given XmlNode object or string
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		protected static bool GetBool(object node)
		{
			bool _result = false;
			XmlNode _el = node as XmlNode;
			if (null != _el) {
				Boolean.TryParse(_el.Value, out _result);
			} else {
				string _s = node as string;
				if (!string.IsNullOrEmpty(_s)) {
					Boolean.TryParse(_s, out _result);
				}
			}
			return _result;
		}
		
		/// <summary>
		/// Prevents an expression (such as assignment) returning a value
		/// inside a data-bind expression
		/// </summary>
		/// <example>
		/// &lt;%# Void(((GridViewRow)Container).BackColor = Color.FromName("#ffffff")) %&gt;
		/// </example>
		/// <param name="any">expression, which value must be supressed</param>
		/// <returns>empty string</returns>
		protected static string Void(object any)
		{
			return string.Empty;
		}
		
		protected static DateTime ParseDateTime(string strDateTime)
		{
			DateTime _result = DateTime.Now;
			DateTime.TryParse(strDateTime, out _result);
			return _result;
		}
		
		protected static string FormatDateTime(DateTime dateTime)
		{
			return string.Format("{0:d}", dateTime);
		}

		protected static string FormatDateTime(string strDateTime)
		{
			return FormatDateTime(ParseDateTime(strDateTime));
		}
		#endregion xsl2asp
	}

	/// <summary>
	/// Базовый класс Web формы для проекта
	/// </summary>
	public partial class BaseWebPage : Page
	{
		protected override void InitializeCulture()
		{
			this.selectLanguage();
			
			this.UICulture = DCE.Service.getCurrentLanguage().ToLower().Replace("ua", "uk");
			base.InitializeCulture();
		}
 
		void selectLanguage()
		{
			string _lang;
			
			if (detectLanguageFromParams(out _lang)) {
				DCE.Service.SetLanguage(_lang);
			}
		}

		bool detectLanguageFromParams(out string lang)
		{
			bool _result = false;
			lang = null;
			string _param = this.Request.Params["lang"];

			if (!string.IsNullOrEmpty(_param)) {
				switch (_param.ToLower()) {
					case "uk":
					case "ua":
						lang = "UA";
						_result = true;
						break;
					case "en":
						lang = "EN";
						_result = true;
						break;
					case "ru":
						lang = "RU";
						_result = true;
						break;
				}
			}
			return _result;
		}
		
		override protected void OnInit(EventArgs e)
		{
			this.Response.Cache.SetExpires(System.DateTime.Now);
			
			//Control control = this.FindControl("MainMenuControlID");
			//DCE.Common.MainMenuControl mainMenu = (DCE.Common.MainMenuControl)control;
			Session["dceDefLang"] = LocalisationService.DefaultLanguage;
			Service.SetCook("dceDefLang");
			//if (control != null) {
			//	this.Active = mainMenu.SwitchMenu();
			//}
			
			//this.isLoginOk = this.Login();
			//this.scriptManager = new ScriptManager();
			base.OnInit(e);
		}

		protected MainMenuControl MainMenuControl1 {
			get {
				return (MainMenuControl)this.Master.FindControl("MainMenuControl1");
			}
		}
		
		protected LeftMenu LeftMenu1 {
			get {
				return (LeftMenu)this.Master.FindControl("LeftMenu1");
			}
		}
	}
}