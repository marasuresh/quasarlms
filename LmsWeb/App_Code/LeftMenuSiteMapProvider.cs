using System;
using System.Data;
using System.Configuration;
using System.Configuration.Provider;
using System.Web;
using System.Web.Security;
using System.Collections.Specialized;
using System.Text;
using System.Runtime.CompilerServices;

/// <summary>
/// Summary description for LeftMenuSiteMapProvider
/// </summary>
public class LeftMenuSiteMapProvider: StaticSiteMapProvider
{
	/// <summary>
	/// Resembles a basic query string parsing functionality of the HttpValueCollection class
	/// </summary>
	class ParamCollection : NameValueCollection
	{
		readonly string m_rawUrl;
		readonly string m_basePageName;
		
		public string BasePageName {
			get {
				return this.m_basePageName;
			}
		}
		
		public ParamCollection(string rawUrl)
		{
			this.m_rawUrl = rawUrl;
			int _baseNameStart = Math.Max(rawUrl.LastIndexOf('/'), 0);
			int _start = rawUrl.IndexOf('?');

			this.m_basePageName = _start >= _baseNameStart + 1 ? rawUrl.Substring(_baseNameStart + 1, _start - _baseNameStart - 1) : rawUrl.Substring(_baseNameStart);
			string _queryString = _start >= 0 ? rawUrl.Substring(_start + 1) : string.Empty;
			this.FillFromString(_queryString, false, null);
		}

		/// <summary>
		/// Taken from System.Web.HttpValueCollection
		/// </summary>
		/// <param name="s"></param>
		/// <param name="urlencoded"></param>
		/// <param name="encoding"></param>
		internal void FillFromString(string s, bool urlencoded, Encoding encoding)
		{
			int num = (s != null) ? s.Length : 0;
			for (int i = 0; i < num; i++) {
				int startIndex = i;
				int num4 = -1;
				while (i < num) {
					char ch = s[i];
					if (ch == '=') {
						if (num4 < 0) {
							num4 = i;
						}
					} else if (ch == '&') {
						break;
					}
					i++;
				}
				string str = null;
				string text2 = null;
				if (num4 >= 0) {
					str = s.Substring(startIndex, num4 - startIndex);
					text2 = s.Substring(num4 + 1, (i - num4) - 1);
				} else {
					text2 = s.Substring(startIndex, i - startIndex);
				}
				if (urlencoded) {
					base.Add(HttpUtility.UrlDecode(str, encoding), HttpUtility.UrlDecode(text2, encoding));
				} else {
					base.Add(str, text2);
				}
				if ((i == (num - 1)) && (s[i] == '&')) {
					base.Add(null, string.Empty);
				}
			}
		}
	}

	SiteMapNode m_rootNode = null;
	string m_lang;

	public LeftMenuSiteMapProvider()
	{
	}
	
	[MethodImpl(MethodImplOptions.Synchronized)]
	public override SiteMapNode BuildSiteMap()
	{
		string _lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToLower().Replace("uk", "ua");
		
		if (null != this.m_rootNode) {
			if (_lang == this.m_lang) {
				return this.m_rootNode;
			} else {
				this.m_lang = _lang;
			}
		}
		
		NameValueCollection _attributes = new NameValueCollection();
		_attributes.Add("FullDescr", global::Resources.MainMenu.Item2_Full);
		
		this.m_rootNode = new SiteMapNode(
				this,
				Resources.PageUrl.PAGE_SUBSCRIBE,
				Resources.PageUrl.PAGE_SUBSCRIBE,
				global::Resources.MainMenu.Item2_Short,
				global::Resources.MainMenu.Item2_Alt,
				null,
				_attributes,
				null,
				string.Empty);
		
		m_rootNode.Description = global::Resources.MainMenu.Item2_Alt;
		
		
		string _defLang = LocalisationService.DefaultLanguage;
		DCE.dbData db = new DCE.dbData();
		
		string select = @"
SELECT	cd.id,
		'TrainingRequest' as control,
		dbo.GetStrContentAlt(cd.Name,'" + _lang + "','" + _defLang + @"') as text
FROM	dbo.CourseDomain cd
WHERE	cd.Parent IS NULL 
		AND	dbo.isAreaHasCourses(cd.id)=1";
		
		System.Data.DataSet dsAreas = db.getDataSet(select, "dataSet", "item");
		System.Data.DataTable tableAreas = dsAreas.Tables["item"];
		if (tableAreas != null && tableAreas.Rows.Count > 0) {
			foreach(DataRow _row in tableAreas.Rows) {
				SiteMapNode _node = new SiteMapNode(
						this,
						((Guid)_row["id"]).ToString(),
						string.Format(
								"~/CourseRequests.aspx?cset={0}&id={1}",
								(string)_row["control"],
								((Guid)_row["id"]).ToString()),
						(string)_row["text"]);
				
				this.AddNode(_node, this.m_rootNode);
			}
		}

		return this.m_rootNode;
	}

	protected override SiteMapNode GetRootNodeCore()
	{
		this.BuildSiteMap();
		return m_rootNode;
	}
}
