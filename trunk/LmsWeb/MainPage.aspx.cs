using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Xml;

namespace DCE
{
	/// <summary>
	/// Главная страница
	/// </summary>
	public partial class MainPage : DCE.BaseWebPage
	{
		XmlDocument doc = null;
		
		protected int Active {
			get {
				return this.MainMenuControl1.Active;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.leftMenu = this.LeftMenu1;
			DCE.Service.LoadXmlDoc(this.Page, this.leftMenu.doc, "HomeLeft.xml");
			this.onLoadCenter();
			doc = this.leftMenu.doc;
			
			XmlNode items = doc.DocumentElement.SelectSingleNode("Items");
			
			if (items != null) {
				// Расписание
				if (DceAccessLib.DAL.TrainingController.RecordsExist()) {
					
					XmlNode scitem = doc.CreateNode(XmlNodeType.Element, "item", string.Empty);
					scitem.InnerXml = string.Format(@"
<text>{0}</text>
<alt>{1}</alt>
<control>Schedule</control>",
							Resources.HomeLeft.Schedule,
							Resources.HomeLeft.ScheduleAlt);
					items.AppendChild(scitem);
				}
				// Просмотр курсов
				if (DceAccessLib.DAL.CourseController.RecordsExist()) {
					
					XmlNode citem = doc.CreateNode(XmlNodeType.Element, "item", string.Empty);
					citem.InnerXml = string.Format(@"
<text>{0}</text>
<alt>{1}</alt>
<control>CoursesCommon</control>",
							Resources.HomeLeft.Courses,
							Resources.HomeLeft.CoursesAlt);
					items.AppendChild(citem);
					addAreas(null, citem);
				}
				// Треки курсов
				if (DceAccessLib.DAL.CourseController.TrackRecordsExist()) {
					XmlNode citem = doc.CreateNode(XmlNodeType.Element, "item", string.Empty);
					citem.InnerXml = string.Format(@"
<text>{0}</text>
<alt>{1}</alt>
<control>TracksCommon</control>",
							Resources.HomeLeft.CTracks,
							Resources.HomeLeft.CTracksAlt);
							items.AppendChild(citem);
				}
				// Анкетирование
				if (DceAccessLib.DAL.TestController.RecordsExist()) {
					XmlNode qitem = doc.CreateNode(XmlNodeType.Element, "item", "");
					qitem.InnerXml = string.Format(@"
<text>{0}</text>
<alt>{1}</alt>
<control>Questionnaire</control>",
							Resources.HomeLeft.Questionnaire,
							Resources.HomeLeft.QuestionnaireAlt);
					items.AppendChild(qitem);
				}
			}
		}

      /// <summary>
      /// Создание дерева областей курсов
      /// </summary>
      /// <param name="parentId"></param>
      /// <param name="parentNode"></param>
		static void addAreas(Guid? parentId, XmlNode parentNode)
		{
			DataSet dsAreas = DceAccessLib.DAL.CourseController.SelectAreas(parentId);
			DataTable tableAreas = dsAreas.Tables["item"];
			
			if (tableAreas != null && tableAreas.Rows.Count > 0) {
				string areas = dsAreas.GetXml();
				XmlDocument tdoc = new XmlDocument();
				tdoc.LoadXml(areas);
				parentNode.InnerXml += tdoc.SelectSingleNode("dataSet").InnerXml;
			}
		}
		
		void onLoadCenter()
		{
			string _cset = this.Request["cset"] as string;
			if(string.IsNullOrEmpty(_cset)) {
				switch (this.Active) {
					case 3:
						this.Response.Redirect(Resources.PageUrl.PAGE_SUBSCRIBE + "?index=3");
						break;
					case 5:
						this.PlaceHolder1.Controls.Add(this.LoadControl("Common\\Contacts.ascx"));
						break;
					case 6:
						this.PlaceHolder1.Controls.Add(this.LoadControl("Common\\HelpInfo.ascx"));
						break;
					default:
						this.PlaceHolder1.Controls.Add(this.LoadControl("Common\\News.ascx"));
						break;
				}
			} else {
				Control _ctl = this.LoadControl(@"Common\" + _cset + ".ascx");
				
				if (null == _ctl) {
					_ctl = this.LoadControl("Common\\News.ascx");
				}
				
				this.PlaceHolder1.Controls.Add(_ctl);
			}
      }
	}
}
