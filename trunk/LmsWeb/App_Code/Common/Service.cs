using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.SessionState;
using System.Collections;
using System.IO;
using System.Xml.XPath;
using System.Collections.Generic;

namespace DCE
{
    /// <summary>
    /// Коллекция сервисных статических методов.
    /// </summary>
    public static partial class Service
    {
        public static string Language
        {
            get { return "UA"; }
        }

        /// <summary>
        /// Получение пути к папке default языка
        /// </summary>
        public static string DefaultLangPath
        {
			get { return DCE.Settings.getValue("dceLangPath") + LocalisationService.DefaultLanguage + "\\"; }
        }

		public static Guid? TrainingID {
			get {
				Guid? trId = PageParameters.trId;
				if(trId.HasValue) {
					HttpContext.Current.Session["trainingId"] = trId;
				} else {
					trId = (Guid?)HttpContext.Current.Session["trainingId"];
				}
				return trId;
			}
			set {
				HttpContext.Current.Session["trainingId"] = value;
			}
		}

        /// <summary>
        /// Текущий id курса
        /// </summary>
        /// <param name="pg"></param>
        /// <returns></returns>
		public static Guid? courseId {
			get {
				return (Guid?)HttpContext.Current.Session["courseId"];
			}
			set {
				HttpContext.Current.Session["courseId"] = value;
			}
		}

		/// <summary>
		/// Возвращает числовой эквивалент текущего языка для фильтра в SQL запросах 
		/// </summary>
		/// <returns></returns>
		public static string getLanguage(System.Web.UI.Page pg)
		{
			string lang = getCurrentLanguage();
			string rv = "3";
			switch(lang.ToUpper()) {
				case "UA": rv = "3"; break;
				case "EN": rv = "2"; break;
				case "RU": rv = "1"; break;
			}
			pg.Session["dceLangFlt"] = rv;
			return rv;
        }

        /// <summary>
        /// Получить cookie
        /// </summary>
        /// <param name="cookName"></param>
        /// <param name="pg"></param>
        /// <returns></returns>
		[Obsolete("No need to pass a page object", true)]
        public static string GetCook(string cookName, System.Web.UI.Page pg)
        {
            object oCook = pg.Session[cookName];
            if( oCook != null )
            {
                string sCook = (string)oCook;
                if( sCook != "" )
                    return sCook;
            }
            return (pg.Request.Cookies[cookName] != null ? pg.Request.Cookies[cookName].Value : "");
        }
		
		public static string GetCook(string name)
		{
			string _value = HttpContext.Current.Session[name] as string;
			if(!string.IsNullOrEmpty(_value)) {
				return _value;
			} else {
				HttpCookie _cookie = HttpContext.Current.Request.Cookies[name];
				if(null != _cookie) {
					_value = _cookie.Value;
				}
				return string.IsNullOrEmpty(_value) ? string.Empty : _value;
			}
		}

        /// <summary>
        /// Присвоить cookie
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="pg"></param>
        [Obsolete("No need to pass a page object", true)]
		public static void SetCook(string strName, System.Web.UI.Page pg)
        {
            HttpCookie MyCookie = new HttpCookie(strName);
            MyCookie.Value = pg.Session[strName] + "";
            MyCookie.Expires = DateTime.Now.AddDays(360);
			pg.Response.Cookies.Add(MyCookie);
        }

		public static void SetCook(string name)
		{
			HttpCookie _cookie = new HttpCookie(name);
			_cookie.Value = HttpContext.Current.Session[name] as string;
			_cookie.Expires = DateTime.Now.AddYears(1);
			HttpContext.Current.Response.Cookies.Add(_cookie);
		}

        static string MapPath(string path)
        {
            return HttpContext.Current.Request.MapPath(path);
        }

        static string MapLanguagePath(string langFile)
        {
            return MapPath("~/Lang/UA/xml/" + langFile);
        }

        public static XPathNavigator LoadXmlPathNavigator(string virtualPath)
        {
            return new XPathDocument(MapLanguagePath(virtualPath)).CreateNavigator();
        }

        static Dictionary<string, XmlDocument> cachedXmlDocuments = new Dictionary<string, XmlDocument>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Загрузить xml документ
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="doc"></param>
        /// <param name="path">имя файла</param>
        public static XmlDocument LoadXmlDoc(string virtualPath)
        {
            lock( cachedXmlDocuments )
            {
                XmlDocument doc;
                if( !cachedXmlDocuments.TryGetValue(virtualPath, out doc) )
                {
                    doc = new XmlDocument();
                    doc.Load(MapLanguagePath(virtualPath));
                    cachedXmlDocuments[virtualPath] = doc;
                }

                return (XmlDocument)doc.Clone();
            }
        }

        public static void SetTitle(string title, System.Web.UI.Page pg)
        {
            pg.Title = title;
            Control control = pg.FindControl("TitleId");
            if( control != null )
            {
                ((DCE.Common.Title)control).title = title;
            }
        }
    }
}
