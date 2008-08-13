namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// Расписание
	/// </summary>
	public partial  class Schedule : DCE.BaseWebControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
         System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
         DCE.Service.LoadXmlDoc(this.Page, doc, "CoursesCommon.xml");
         
         System.Data.DataSet dsTrainings = null;
         System.Data.DataTable tableTrainings = null;
         
         string select = @"
            select dbo.GetStrContentAlt(t.Name,'"+LocalisationService.Language+@"', l.Abbr) as Name,
               t.isActive,
               (select MIN(StartDate) from Schedule where Training=t.id) as StartDate,
               (select MAX(EndDate) from Schedule where Training=t.id) as EndDate
            from dbo.Trainings t, dbo.Courses c inner join dbo.Languages l on l.id=c.CourseLanguage
            where t.Course=c.id and t.isActive=1 and t.Expires=0 and (select MIN(StartDate) from Schedule where Training=t.id) > {fn NOW()}
            order by c.Type";

         dsTrainings = dbData.Instance.getDataSet(select,  "DataSet", "Trainings");
         tableTrainings = dsTrainings.Tables["Trainings"];


         System.Xml.XmlNode tNode = doc.SelectSingleNode("xml/TrainingsList");
         if (tableTrainings != null && tableTrainings.Rows.Count > 0 && tNode != null)
         {
            tNode.InnerXml += dsTrainings.GetXml();
         }

         System.Xml.Xsl.XslTransform trans = new System.Xml.Xsl.XslTransform();
         trans.Load(this.Page.MapPath(@"~/xsl/CoursesCommon.xslt"));
         
         this.Xml1.Document = doc;
         this.Xml1.TransformArgumentList = new System.Xml.Xsl.XsltArgumentList();
         this.Xml1.Transform = trans;
      }
	}
}
