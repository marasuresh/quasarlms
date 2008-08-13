using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Xml;
using System.Web.UI.HtmlControls;

namespace DCE
{
	/// <summary>
	/// Подача заявок на обучение
	/// </summary>
	public partial class CourseRequests : DCE.BaseWebPage
	{
		XmlDocument doc = null;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.leftMenu = this.LeftMenu1;
			this.leftMenu.doc.LoadXml("<xml><Items></Items></xml>");
			this.onLoadCenter();

			doc = this.leftMenu.doc;
			XmlNode items = doc.DocumentElement.SelectSingleNode("Items");
			
			if (items != null) {
				// Заявки
				string select = @"
select	id
from	dbo.Courses
where	isReady=1
		and CPublic=0";
				DataSet dsCourses = dbData.Instance.getDataSet(select,  "DataSet", "Courses");
				DataTable tableCourses = dsCourses.Tables["Courses"];
				
				if (tableCourses != null && tableCourses.Rows.Count > 0) {
					this.addAreas(null, items);
				}
				// Треки курсов
				select = @"
select	c.id
from	dbo.Courses c,
		CTracks ctr 
where	c.id in (
			select	id
			from	GroupMembers
			where mGroup=ctr.Courses)
		and c.IsReady=1";

				dsCourses = dbData.Instance.getDataSet(select,  "DataSet", "Courses");
				tableCourses = dsCourses.Tables["Courses"];
				
				if (tableCourses != null && tableCourses.Rows.Count > 0) {
					XmlNode citem = doc.CreateNode(XmlNodeType.Element, "item", string.Empty);
					citem.InnerXml ="<text>"+Resources.HomeLeft.CTracks+"</text><alt>"+Resources.HomeLeft.CTracksAlt+"</alt>"
						+"<control>TrackRequest</control>";
					items.AppendChild(citem);
				}
			}
		}

      /// <summary>
      /// Создание дерева областей курсов
      /// </summary>
      /// <param name="parentId"></param>
      /// <param name="parentNode"></param>
		private void addAreas(string parentId, System.Xml.XmlNode parentNode)
		{
			DataSet dsAreas = Training_GetAreas(parentId);
			DataTable tableAreas = dsAreas.Tables["item"];
			
			if (tableAreas != null && tableAreas.Rows.Count > 0) {
				string areas = dsAreas.GetXml();
				XmlDocument tdoc = new XmlDocument();
				tdoc.LoadXml(areas);
				parentNode.InnerXml += tdoc.SelectSingleNode("dataSet").InnerXml;
			}
		}

		static DataSet Training_GetAreas(string parentId)
		{
			string select = @"
select	cd.id,
		'TrainingRequest' as control,
		dbo.GetStrContentAlt(cd.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as text
from	dbo.CourseDomain cd
where	cd.Parent " + (parentId == null ? ("is NULL") : ("='" + parentId + "'")) + @"
		and dbo.isAreaHasCourses(cd.id)=1";
			
			return dbData.Instance.getDataSet(select, "dataSet", "item");
		}

		void onLoadCenter()
		{
			string _cset = this.Request["cset"] as string;

			Control _ctl = string.IsNullOrEmpty(_cset)
					? this.LoadControl(@"~/Common/TrainingRequest.ascx")
					: this.LoadControl(@"~/Common/" + _cset + ".ascx")
						?? this.LoadControl(@"~/Common/TrainingRequest.ascx");
			
			this.PlaceHolder1.Controls.Add(_ctl);
		}
	}
}
