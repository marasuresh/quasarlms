namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// Словарь курса
	/// </summary>
	public partial  class Vocabulary : DCE.BaseTrainingControl
	{
      public string AbsoluteViewPath;
      private int glsId = 0;

      protected void Page_Load(object sender, System.EventArgs e)
		{
         Guid? trId = DCE.Service.TrainingID;
         Guid? courseId = DCE.Service.courseId;

         System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

         Guid? themeId  = PageParameters.ID;
         if(themeId.HasValue && courseId.HasValue) {
            DCE.Service.LoadXmlDoc(this.Page, doc, "Vocabulary.xml");
            object cn = this.Session["courseName"];
            if (cn != null)
            {
               string courseName = (string) cn;
               doc.DocumentElement.InnerXml += "<Course>"+courseName+"</Course>";
            }

			System.Data.DataSet dsTerms = Course_GetVocabulary(courseId);
            System.Data.DataTable tableTerms = dsTerms.Tables["term"];
            if (tableTerms != null && tableTerms.Rows.Count > 0)
            {
               System.Xml.XmlDocument tdoc = new System.Xml.XmlDocument();
               tdoc.LoadXml(dsTerms.GetXml());
               char currChar = (char) 0;
               char lastChar = (char) 0;
			   if (LocalisationService.Language.Equals("EN", StringComparison.InvariantCultureIgnoreCase)) {
				   lastChar = 'z';
			   }

               System.Xml.XmlNode symbols = doc.SelectSingleNode("xml/Symbols");
               System.Xml.XmlNode letter = null;
               string lastname = null;
               foreach (System.Xml.XmlNode term in tdoc.DocumentElement.ChildNodes)
               {
                  System.Xml.XmlNode name = term.SelectSingleNode("name");
                  if (name != null && name.InnerText != "" && lastname != name.InnerText)
                  {
                     char ch = name.InnerText.ToUpper()[0];
                     // Для английского языка выводятся только английские термины
                     if (lastChar > (char) 0 && ch > lastChar) 
                        break;

                     if (ch > currChar)
                     {
                        currChar = ch;
                        letter = doc.CreateNode(System.Xml.XmlNodeType.Element, "letter", "");
                        symbols.AppendChild(letter);
                        System.Xml.XmlNode symbol = doc.CreateNode(System.Xml.XmlNodeType.Element, "symbol", "");
                        if (ch < 'A') // Не буквы
                           symbol.InnerText = doc.DocumentElement.SelectSingleNode("SymbolsA").InnerText;
                        else
                           symbol.InnerText = new string(currChar, 1);

                        letter.AppendChild(symbol);
                        letter.InnerXml += "<id>gls_"+ (++this.glsId).ToString() +"</id>";
                     }
                     if (letter != null)
                     {
                        letter.InnerXml += term.OuterXml;
                     }
                     lastname = name.InnerText;
                  }
               }
            }
         }
         
         System.Xml.Xsl.XslTransform trans = new System.Xml.Xsl.XslTransform();
         trans.Load(this.Page.MapPath(@"~/xsl/Vocabulary.xslt"));
         
         this.Xml1.Document = doc;
         this.Xml1.TransformArgumentList = new System.Xml.Xsl.XsltArgumentList();
         this.Xml1.Transform = trans;
      }

		private System.Data.DataSet Course_GetVocabulary(Guid? courseId)
		{
			string select = @"SELECT t.id, L.Abbr, dbo.GetStrContentAlt(t.Name, 'EN') as nameEN,
               c.DataStr as name,
               dbo.GetStrContentAlt(t.Text, '" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as text
               FROM  dbo.Content c, Languages L, VTerms t, Vocabulary v
               WHERE v.Term = t.id 
               and L.id=c.Lang and c.eid=t.Name 
               and (L.Abbr='EN' or L.Abbr=dbo.TargetLang(t.Name, '" + LocalisationService.Language + "', '" + LocalisationService.DefaultLanguage + @"')) 
               and v.Course =  '" + courseId
			   + "' ORDER BY c.DataStr,L.Abbr";
			DataSet dsTerms = dbData.Instance.getDataSet(select, "ds", "term");
			return dsTerms;
		}

//      protected override void Render(System.Web.UI.HtmlTextWriter output)
//      {
//         base.Render(output);
//         this.Server.Execute("http://www.google.com");
//      }
	}
}
