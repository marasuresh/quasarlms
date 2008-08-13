namespace DCE
{
   using System;
   using System.ComponentModel;
   using System.Text.RegularExpressions;
   using System.Data.SqlClient;
   using System.Data;
   using System.Drawing;
   using System.Web;
   using System.Web.UI.WebControls;
   using System.Web.UI.HtmlControls;
   using System.Xml;
   using System.Xml.XPath;
   using System.Xml.Xsl;
   using System.Data.SqlTypes;
	using System.Collections.Generic;

	/// <summary>
	/// Вывод таблицы из БД
	/// </summary>
	public partial  class dbTableControl : System.Web.UI.UserControl
	{
      public bool IsAdmin = false;
      public string Index = ""; // индекс контрола на странице - для случая если на одной стр. несколько контролов
      protected int currPage;
      protected bool order;
		
      protected string XsltPath;
      protected string IEVersion = "";

      public string langXML = "";
      public string InputSQL = "";
      public string InputIterSQL = "";
      public string InputAddSQL = "";
      public string InputVariables = "";
      public string SQLparam = "";
      public string InputXSL = "_view.xsl";
      public string InputOrderBy = "1";
      public bool InputOrder = true;
      public int PNPageSize = 10;
      public bool Paging = false;
      public bool IsOne = false;
      public bool ShowXml = false;
      public bool ApplySorting = false;
      public string InputTitle = "";

      protected string CookValue = "123";

      public dbData md = new dbData();
		
      public virtual void AddActions(){}

      protected static int ConvertToInt(object obj, int defaultValue)
      {
			int _result;
			string _strInput = obj as string;
			return !string.IsNullOrEmpty(_strInput) && Int32.TryParse(_strInput, out _result)
					? _result
					: defaultValue;
      }
      /// <summary>
      /// Получает значение id из QueryString и Form
      /// </summary>
      /// <param name="paramName">Имя параметра</param>
      /// <returns>Guid.Empty если параметр отсутствует</returns>
		private Guid getGuidParamValue(string paramName)
		{
			string strID = Request.QueryString[paramName];
			if(string.IsNullOrEmpty(strID)) {
				strID = Request.Form[paramName];
			}
			Guid _result;
			return GuidService.TryParse(strID, out _result) ? _result : Guid.Empty;
		}
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
         //
         Response.Expires = 0;
         //

         XsltPath = this.MapPath("~/" + Settings.XsltPath);
         currPage = ConvertToInt(Request.QueryString["page"+this.Index],1);
         order = ((""+Request.QueryString["order"+this.Index])!="0");

         IEVersion = "" + Request.ServerVariables["HTTP_USER_AGENT"];
         IEVersion = Regex.Match(IEVersion, "MSIE[^;]+;").Value;
         IEVersion = Regex.Replace(IEVersion, "(MSIE )|;", "");
         IEVersion = (IEVersion=="")?"0":IEVersion;

         md.Connection = new SqlConnection(Settings.ConnectionString);
         string selectSQL = Settings.GetSqlQuery(InputSQL);
         string resultSQL = "";
         if(this.SQLparam=="")
            this.SQLparam = Request.QueryString["p"];
         md.ResultStrSqlSelect = selectSQL;
         //md.ResultStrSqlSelect = md.ResultStrSqlSelect.Replace("%date%", ""+this.selectedDate.DayTicks);
         md.ResultStrSqlSelect = md.ResultStrSqlSelect.Replace("%param%",SQLparam);
         if(Paging)	md.PageSize = PNPageSize;
         else md.PageSize = 99999;

			Dictionary<string, object> _paramValueColl = new Dictionary<string, object>();
			
         Guid groupId  = this.getGuidParamValue("groupId");
         Guid pId      = this.getGuidParamValue("ID");
         Guid recordId = this.getGuidParamValue("recordId");
			_paramValueColl.Add("groupId", groupId);
			_paramValueColl.Add("paramId", pId);
			_paramValueColl.Add("recordId", recordId);
			_paramValueColl.Add("studentId", CurrentUser.UserID);
			_paramValueColl.Add("dceLanguage", LocalisationService.Language);
			_paramValueColl.Add("dceDefLang", LocalisationService.DefaultLanguage);
			_paramValueColl.Add("trainingId", DCE.Service.TrainingID);

			List<string> vars = new List<string>();
			
			if(!string.IsNullOrEmpty(this.InputVariables)) {
				vars.AddRange(this.InputVariables.Split(';'));
			}
			//Detect placeholder params directly from the query
			MatchCollection _paramColl = Regex.Matches(md.ResultStrSqlSelect, @"%(?'paramName'\w+)%");
			foreach(Match _match in _paramColl) {
				string _newValue = _match.Groups["paramName"].Value;
				if (!vars.Contains(_newValue)) {
					vars.Add(_newValue);
				}
			}
			
			// Обработка подставляемых переменных сессии
			foreach(string strVarName in vars) {
				object _value;

				if (_paramValueColl.ContainsKey(strVarName)) {
					_value = _paramValueColl[strVarName];
					md.ResultStrSqlSelect = md.ResultStrSqlSelect.Replace("%" + strVarName + "%", _value + "");
				} else {
					_value = Session[strVarName] + "";
					if (null == _value) {
						md.ResultStrSqlSelect = Regex.Replace(md.ResultStrSqlSelect, @"\S+\s*=\s*'%" + strVarName + "%'", " 1=0 ");
					}
				}
			}

			if (!IsOne) {
				md.ResultStrSqlSelect = Regex.Replace(
						md.ResultStrSqlSelect,
						@"\S+\s*=\s*'00000000-0000-0000-0000-000000000000'",
						" 1 = 1 ");
			}

         //Response.Write("<br><br><b>" + md.ResultStrSqlSelect + "</b><br><br>");
         int page = 1;
         if(this.Paging)page = this.currPage;
         string field = Request.QueryString["field"+this.Index];
         if(this.ApplySorting)
         {				
            if(field!=null) InputOrderBy = field;
            if(Request.QueryString["order"+this.Index]!=null)
               InputOrder = this.order;
         }			

         this.AddActions();//Something special

         XmlDocument doc = new XmlDocument();
         string strXml;
         int count = 0;
         md.Connection.Open();		
			
         if(this.Paging)
         {
            strXml = md.GetDataPage(page, InputOrderBy, InputOrder).GetXml();
            count = md.CountData;
         }
         else
         {
            string strOrderCommand = " ORDER BY ";
            if(InputOrderBy != "1" & md.ResultStrSqlSelect.ToUpper().IndexOf(strOrderCommand.Trim())<0)
            {
               md.ResultStrSqlSelect += strOrderCommand+InputOrderBy+" "+(InputOrder?"ASC":"DESC");
            }
            strXml = md.GetDataAll().GetXml();
         }

         if(this.langXML!="")
         {
            XmlDocument docLng = new XmlDocument();
            DCE.Service.LoadXmlDoc(this.Page, docLng, this.langXML+".xml");
            if(strXml.Length<11)
               strXml = "<xml>"+docLng.InnerXml+"</xml>";
            else
               strXml = strXml.Replace("</xml>",docLng.InnerXml+"</xml>");
         }
         doc.LoadXml(strXml);			
			
         md.Connection.Close();
         Xml1.Document = doc;
			
         XslTransform trans = new XslTransform();			
         trans.Load(this.XsltPath + @"\" + InputXSL);
         Xml1.Transform = trans;
         Xml1.TransformArgumentList = new XsltArgumentList();

         Xml1.TransformArgumentList.AddParam("Index", "", this.Index);			
         Xml1.TransformArgumentList.AddParam("recordsCount", "", count);			
         Xml1.TransformArgumentList.AddParam("pageSize", "", PNPageSize);
         Xml1.TransformArgumentList.AddParam("currentPage", "", currPage);
         Xml1.TransformArgumentList.AddParam("order", "", Convert.ToInt16(InputOrder));
         if(field != null)
            Xml1.TransformArgumentList.AddParam("field", "", field);
         Xml1.TransformArgumentList.AddParam("linkCount", "", Settings.PageNavigatorPortion);

         Xml1.TransformArgumentList.AddParam("GroupId", "", groupId.ToString());
         if(this.SQLparam!=null)
            Xml1.TransformArgumentList.AddParam("SQLparam", "", this.SQLparam);
         if(pId!=Guid.Empty)
            Xml1.TransformArgumentList.AddParam("paramId", "", pId.ToString());
         if(recordId!=Guid.Empty)
            Xml1.TransformArgumentList.AddParam("recordId", "", recordId.ToString());
         Xml1.TransformArgumentList.AddParam("IE", "", IEVersion);
         if(this.IsAdmin)Xml1.TransformArgumentList.AddParam("isAdmin", "", "1");
         Xml1.TransformArgumentList.AddParam("CookValue", "", CookValue);
         if(this.InputTitle != "")
            Xml1.TransformArgumentList.AddParam("title", "", this.InputTitle);
      }
	}
}
