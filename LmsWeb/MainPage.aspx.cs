using System;
using System.Xml;

namespace DCE
{
	using System.Linq;
	using System.Xml.Linq;
	
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
			DCE.Service.LoadXmlDoc(this.Page, this.LeftMenu.doc, "HomeLeft.xml");
			this.onLoadCenter();
			doc = this.LeftMenu.doc;
			
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
			parentNode.InnerXml += 
				new XElement("item",
					from _area in DceAccessLib.DAL.CourseController.SelectAreas(parentId)
					select new XElement("CourseDomain",
						new XElement("id", _area.Name),
						new XElement("control", "CourseCommon"),
						new XElement("text", _area.Title))
				).Value;
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
						var _ctl = this.LoadControl(@"News\UI\NewsList.ascx");
						((N2.Templates.News.UI.NewsList)_ctl).CurrentItem = DceAccessLib.DAL.NewsController.Select();
						this.PlaceHolder1.Controls.Add(_ctl);
						break;
				}
			} else {
				var _ctl = this.LoadControl(@"Common\" + _cset + ".ascx");
				
				if (null == _ctl) {
					_ctl = this.LoadControl(@"News\UI\NewsList.ascx");
					((N2.Templates.News.UI.NewsList)_ctl).CurrentItem = DceAccessLib.DAL.NewsController.Select();
				}
				
				this.PlaceHolder1.Controls.Add(_ctl);
			}
      }
	}
}
