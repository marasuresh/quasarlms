using System;
using System.Xml;
using System.Xml.XPath;
using System.Configuration;
using System.Web;


namespace DCE
{
    public class Settings
    {
        private static XmlDocument doc = LoadDoc("~/setup.config");

        public static readonly XmlDocument docSQLSet = LoadDoc("~/SQLSet.config");

        private static XmlDocument LoadDoc(string p)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Request.MapPath(p));
            return doc;
        }

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Dce2005ConnectionString"].ConnectionString;
		public static string XsltPath = ConfigurationManager.AppSettings["XsltPath"];
        public static int PageNavigatorPortion = Int32.Parse(ConfigurationManager.AppSettings["PageNavigatorPortion"]);
        public static int MaxPhotoSize = Int32.Parse(ConfigurationManager.AppSettings["MaxPhotoSize"]);

        [Obsolete("Do not use Settings classes constructor. Let it be static.")]
        public Settings()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Получить SQL запрос
        /// </summary>
        /// <param name="queryName"></param>
        /// <returns></returns>
        public static string GetSqlQuery(string queryName)
        {
            XmlNode node = docSQLSet.SelectSingleNode("//SqlQueries/" + queryName);
            if (node != null) return node.InnerText;
            return "";
        }
        /// <summary>
        /// Получить значение параметра Setup.config
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static string getValue(string paramName)
        {
            string rv = "";
            try
            {
                XmlNode nn = doc.SelectSingleNode("xml/" + paramName + "/text()");
                if (nn != null)
                    rv = nn.Value; // read value
            }
            catch { }
            return rv;
        }
        /// <summary>
        /// Получить относительный путь к корневому каталогу курсов на сайте
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use CoursesRoot property.")]
        public static string getCoursesRoot()
        {
            return CoursesRoot;
        }

        public static string CoursesRoot
        {
            get
            {
                string result = ConfigurationManager.AppSettings["CoursesRoot"];
                if( result.EndsWith("/") || result.EndsWith("\\") )
                    return result;
                else
                    return result+"/";
            }
        }
    }
}