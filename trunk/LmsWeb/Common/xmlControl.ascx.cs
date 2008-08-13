namespace DCE
{
   using System;
   using System.ComponentModel;
   using System.Text.RegularExpressions;
   using System.Data;
   using System.Web;
   using System.Web.UI.WebControls;
   using System.Web.UI.HtmlControls;
   using System.Xml;
   using System.Xml.XPath;
   using System.Xml.Xsl;

	/// <summary>
	///		Summary description for dbTableControl.
	/// </summary>
	public partial  class xmlControl : System.Web.UI.UserControl
	{
		
      protected string XsltPath;
      protected string IEVersion = "";

      public string langXML = "";
      public string xmlVariables = "";
      public string InputVariables = "";
      public string InputXSL = "_view.xsl";
      public bool ShowXml = false;
      public string InputTitle = "";

      public virtual void AddActions(){}

      /// <summary>
      /// ѕолучает значение id из QueryString и Form
      /// </summary>
      /// <param name="paramName">»м€ параметра</param>
      /// <returns>Guid.Empty если параметр отсутствует</returns>
		private Guid getGuidParamValue(string paramName)
		{
			string strID = Request.QueryString[paramName];
			if(string.IsNullOrEmpty(strID)) {
				strID = Request.Form[paramName];
			}
			
			Guid rv;
			return GuidService.TryParse(strID, out rv) ? rv : System.Guid.Empty;
		}
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
         //
         Response.Expires = 0;
         //

         XsltPath = Request.ServerVariables["APPL_PHYSICAL_PATH"]+Session["PassPrefix"]
            + Settings.XsltPath + "/";			

         IEVersion = "" + Request.ServerVariables["HTTP_USER_AGENT"];
         IEVersion = Regex.Match(IEVersion, "MSIE[^;]+;").Value;
         IEVersion = Regex.Replace(IEVersion, "(MSIE )|;", "");
         IEVersion = (IEVersion=="")?"0":IEVersion;

         Xml1.TransformArgumentList = new XsltArgumentList();

         Guid pId      = this.getGuidParamValue("ID");
         Guid recordId = this.getGuidParamValue("recordId");

         if(this.InputVariables!="") // ќбработка подставл€емых переменных сессии
         {
            string[] vars = InputVariables.Split(';');
            foreach(string strVarName in vars)
            {
               Xml1.TransformArgumentList.AddParam(strVarName, "", Session[strVarName]+"");
            }
         }

         string strXml = "";

         if(this.xmlVariables!="") // ќбработка подставл€емых переменных
         {
            string[] vars = xmlVariables.Split(';');
            foreach(string strVarName in vars)
            {
               string strValue = Session[strVarName]+"";
               if(strValue!="")
                  strXml += "<"+strVarName+">"+ strValue+"</"+strVarName+">";
               else
               {
                  strValue = Request.QueryString[strVarName]+Request.Form[strVarName]+"";
                  if(strValue!="")
                     strXml += "<"+strVarName+">"+ strValue+"</"+strVarName+">";
               }
            }
         }

		 if (this.langXML != "") {
			 strXml += Service.getLanguageXML(this.langXML, this.Page);
		 }
   
         this.AddActions();//Something special

         Xml1.Document = new XmlDocument();
         Xml1.Document.LoadXml("<xml>"+strXml+"</xml>");
			
         XslTransform trans = new XslTransform();			
         trans.Load(this.XsltPath + InputXSL);
         Xml1.Transform = trans;

         Xml1.TransformArgumentList.AddParam("IE", "", IEVersion);
         if(pId!=Guid.Empty)
            Xml1.TransformArgumentList.AddParam("paramId", "", pId.ToString());
         if(recordId!=Guid.Empty)
            Xml1.TransformArgumentList.AddParam("recordId", "", pId.ToString());
         if(this.InputTitle != "")
            Xml1.TransformArgumentList.AddParam("title", "", this.InputTitle);
			
         if(this.ShowXml || (Request.QueryString["SQL"]!=null) )
         {
            Response.Write("<b><pre>" + strXml.Replace("<","&lt;") + "</pre></b>");	
         }
			
      }
	}
}
