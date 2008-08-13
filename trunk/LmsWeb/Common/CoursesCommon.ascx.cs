namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Xml;
	using System.Xml.Xsl;
	/// <summary>
	/// Список курсов
	/// </summary>
	public partial  class CoursesCommon : DCE.BaseWebControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			DCE.Service.LoadXmlDoc(this.Page, doc, "CoursesCommon.xml");
			
			string searchStr = this.Request.Form["keywords"];
			
			if(!string.IsNullOrEmpty(searchStr)) {
				DataSet dsCourses = Course_Select0();
				DataTable tableCourses = dsCourses.Tables["Courses"];
				XmlNode cNode = doc.SelectSingleNode("xml/CoursesList");
				
				if (tableCourses != null && tableCourses.Rows.Count > 0 && cNode != null) {
					
					for (int i=tableCourses.Rows.Count-1; i >= 0; i--) {
						DataRow row = tableCourses.Rows[i];
						
						if (!Search(searchStr, row)) {
							tableCourses.Rows.RemoveAt(i);
						}
					}
					
					cNode.InnerXml += dsCourses.GetXml();
					
					if (tableCourses.Rows.Count == 0) {
						doc.DocumentElement.InnerXml += "<NotFound>true</NotFound>";
					}
					doc.DocumentElement.InnerXml += "<searchStr>"+searchStr+"</searchStr>";
				}
			} else {
				Guid? areaId  = PageParameters.ID;
				
				if(areaId.HasValue) {
					DataSet dsCourses0 = Course_SelectByArea(areaId);
					DataTable tableCourses = dsCourses0.Tables["Courses"];
					
					XmlNode cNode = doc.SelectSingleNode("xml/CoursesList");
					
					if (cNode != null) {
						cNode.InnerXml += dsCourses0.GetXml();
					}
					
					DataSet dsAreas = Area_Select(areaId);
					DataTable tableAreas = dsAreas.Tables["Area"];
					
					XmlDocument adoc = new XmlDocument();
					adoc.InnerXml += dsAreas.GetXml();
					
					if (tableAreas != null && tableAreas.Rows.Count > 0) {
						this.addAreas(adoc.DocumentElement.SelectSingleNode("Area"));
					}
					doc.DocumentElement.InnerXml += adoc.DocumentElement.InnerXml;
				} else {
					DataSet dsCourses0 = Course_Select();
					DataTable tableCourses = dsCourses0.Tables["Courses"];
					
					XmlNode cNode = doc.SelectSingleNode("xml/CoursesList");
					
					if (tableCourses != null && tableCourses.Rows.Count > 0 && cNode != null) {
						cNode.InnerXml += dsCourses0.GetXml();
					}
					
					DataSet dsAreas = Area_Select();
					doc.DocumentElement.InnerXml += dsAreas.GetXml();
				}
			}

			XslTransform trans = new XslTransform();
			trans.Load(this.Page.MapPath(@"~/xsl/CoursesCommon.xslt"));
			this.Xml1.Document = doc;
			this.Xml1.TransformArgumentList = new XsltArgumentList();
			this.Xml1.Transform = trans;
		}

		static DataSet Course_Select0()
		{
			string select = @"
               select dbo.GetStrContentAlt(c.Name,'" + LocalisationService.Language + @"', l.Abbr) as Name, 
                  dbo.GetContentAlt(c.DescriptionShort,'" + LocalisationService.Language + @"', l.Abbr) as Description,
                  dbo.GetStrContentAlt(cd.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Area,
                  dbo.GetStrContentAlt(ct.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Type,
                  dbo.GetStrContentAlt(c.Keywords,'" + LocalisationService.Language + @"', l.Abbr) as Keywords,
                  dbo.GetStrContentAlt(c.Author,'" + LocalisationService.Language + @"', l.Abbr) as Author,
                  dbo.CourseDuration(c.id) as Duration, c.Code, c.id as cId, c.isReady,
                  c.CPublic, c.Cost1, c.Cost2
               from dbo.CourseType ct right join
                  dbo.Courses c on c.Type=ct.id
                  inner join dbo.Languages l on l.id=c.CourseLanguage
                  left outer join CourseDomain cd on cd.id=c.Area 
               where c.IsReady=1
               order by dbo.GetStrContentAlt(c.Name,'" + LocalisationService.Language + @"',l.Abbr)";

			DataSet dsCourses = dbData.Instance.getDataSet(select, "DataSet", "Courses");
			return dsCourses;
		}

		static DataSet Course_SelectByArea(Guid? areaId)
		{
			string select = "select dbo.GetStrContentAlt(c.Name, '" +
			   LocalisationService.Language + @"', l.Abbr) as Name, 
                  dbo.GetContentAlt(c.DescriptionShort,'" + LocalisationService.Language + @"', l.Abbr) as Description,
                  dbo.GetStrContentAlt(cd.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Area,
                  dbo.GetStrContentAlt(ct.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Type,
                  dbo.CourseDuration(c.id) as Duration, c.Code, c.isReady, c.id as cId,
                  c.CPublic, c.Cost1, c.Cost2
               from dbo.CourseType ct right join
                  dbo.Courses c inner join dbo.Languages l on l.id=c.CourseLanguage
                  on c.Type=ct.id, CourseDomain cd 
               where Area ='" + areaId + @"' and c.IsReady=1 and cd.id=c.Area 
               order by dbo.GetStrContentAlt(c.Name, '" +
			   LocalisationService.Language + @"', l.Abbr)";

			DataSet dsCourses0 = dbData.Instance.getDataSet(select, "DataSet", "Courses");
			return dsCourses0;
		}

		static DataSet Course_Select()
		{
			string select = "select dbo.GetStrContentAlt(c.Name, '" +
			   LocalisationService.Language + @"', l.Abbr) as Name, 
                  dbo.GetContentAlt(c.DescriptionShort,'" + LocalisationService.Language + @"', l.Abbr) as Description,
                  dbo.GetStrContentAlt(ct.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Type,
                  dbo.CourseDuration(c.id) as Duration, c.Code, c.isReady, c.id as cId,
                  c.CPublic, c.Cost1, c.Cost2
               from dbo.CourseType ct right join
                  dbo.Courses c inner join dbo.Languages l on l.id=c.CourseLanguage
                  on c.Type=ct.id 
               where Area is NULL order by dbo.GetStrContentAlt(c.Name, '" +
			   LocalisationService.Language + @"', l.Abbr)";

			DataSet dsCourses0 = dbData.Instance.getDataSet(select, "DataSet", "Courses");
			return dsCourses0;
		}
		
		static DataSet Area_Select(Guid? areaId)
		{
			string select0 = @"select cd.id,
                  dbo.GetStrContentAlt(cd.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Name
               from dbo.CourseDomain cd
               where cd.id ='" + areaId + "'";
			DataSet dsAreas = dbData.Instance.getDataSet(select0, "Main", "Area");
			return dsAreas;
		}

		static DataSet Area_Select()
		{
			string select0 = @"
select	cd.id,
		dbo.GetStrContentAlt(cd.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Name
from	dbo.CourseDomain cd
where	cd.Parent is NULL
		and dbo.isAreaHasCourses(cd.id)=1";
			return dbData.Instance.getDataSet(select0, "ds", "Area");
		}
		
		private static bool Search(string searchStr, DataRow row)
		{
			string[] searchWords = searchStr.Split(' ', ';', ':', '.', ',', '/', '\\', '?', '\'', '|', '+');
			string[] _fields = {"Name", "Area", "Keywords", "Author", "Code"};
			
			return Array.Exists(
				searchWords,
				new Predicate<string>(
					delegate(string pattern)
					{
						return Array.Exists(
							_fields,
							new Predicate<string>(
								delegate(string fieldName)
								{
									object fieldValue = row[fieldName];
									return !Convert.IsDBNull(fieldValue)
										&& fieldValue.ToString().ToLower().Contains(pattern.ToLower());
								}));
					}));
		}
		
		void addAreas(System.Xml.XmlNode parent)
		{
			XmlNode id = parent.SelectSingleNode("id");
			
			if (id != null) {
				string select = @"
select	cd.id,
		dbo.GetStrContentAlt(cd.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Name
from	dbo.CourseDomain cd
where	cd.Parent ='" +id.InnerText+@"'
		and dbo.isAreaHasCourses(cd.id)=1";
			DataSet dsAreas = dbData.Instance.getDataSet(select, "ds", "Area");
            System.Data.DataTable tableAreas = dsAreas.Tables["Area"];

            System.Xml.XmlDocument adoc = new System.Xml.XmlDocument();
            adoc.InnerXml += dsAreas.GetXml();
            foreach(System.Xml.XmlNode area in adoc.DocumentElement.ChildNodes)
            {
               select = "select dbo.GetStrContentAlt(c.Name, '" +
					 LocalisationService.Language + @"', l.Abbr) as Name, 
                     dbo.GetContentAlt(c.DescriptionShort,'" + LocalisationService.Language + @"', l.Abbr) as Description,
                     dbo.GetStrContentAlt(cd.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Area,
                     dbo.GetStrContentAlt(ct.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Type,
                     dbo.CourseDuration(c.id) as Duration, c.Code, c.isReady, c.id as cId,
                     c.CPublic, c.Cost1, c.Cost2
                  from dbo.CourseType ct right join
                     dbo.Courses c inner join dbo.Languages l on l.id=c.CourseLanguage
                     on c.Type=ct.id, CourseDomain cd 
                  where Area ='" +area.SelectSingleNode("id").InnerText+@"' 
                     and cd.id=c.Area order by dbo.GetStrContentAlt(c.Name, '" +
					 LocalisationService.Language + @"', l.Abbr)";

				DataSet dsCourses = dbData.Instance.getDataSet(select, "DataSet", "Courses");
               System.Data.DataTable tableCourses = dsCourses.Tables["Courses"];

               area.InnerXml += dsCourses.GetXml();

               this.addAreas(area);
               parent.InnerXml += area.OuterXml;
            }
         }
      }
	}
}
