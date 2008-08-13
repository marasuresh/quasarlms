using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace DCE
{
	/// <summary>
	/// ќсновна€ страница обучени€
	/// </summary>
	public partial class Training : DCE.BaseWebPage
	{
		private XmlDocument doc = null;
		Guid? m_courseId;
		Guid? m_selectedId;
		Guid? m_trainingId;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.m_selectedId = PageParameters.ID;
			this.m_courseId = PageParameters.courseId;
			this.m_trainingId = PageParameters.trId;

			DataSet dsCourse = null;
			DataTable tableCourse = null;
			DataRow rowCourse = null;
			this.leftMenu = this.LeftMenu1;

			if (this.m_trainingId.HasValue) {
				DCE.Service.LoadXmlDoc(this.Page, this.leftMenu.doc, @"TrainingsLeft.xml");
			} else {
				if (this.m_courseId.HasValue) {
					if (!DceAccessLib.DAL.CourseController.Exists(this.m_courseId.Value)) {
						this.m_courseId = null;
					}
				}
				DCE.Service.courseId = this.m_courseId;
				DCE.Service.LoadXmlDoc(this.Page, this.leftMenu.doc, @"TrainingsLeft.xml");
				XmlNode welcome = this.leftMenu.doc.DocumentElement.SelectSingleNode("FreeIntro");
				XmlNode items = this.leftMenu.doc.DocumentElement.SelectSingleNode("Items");
				items.InnerXml += welcome.InnerXml;
			}

			DCE.Service.TrainingID = this.m_trainingId;

			if (this.m_trainingId.HasValue && CurrentUser.UserID.HasValue) {
				DataRow _training = DceAccessLib.DAL.TrainingController.Select(PageParameters.trId.Value, CurrentUser.UserID.Value);
			
				if (null != _training) {
					this.m_courseId = (Guid)_training["Course"];
				
					if (!((bool)_training["TestOnly"])) {
						XmlNode welcome = this.leftMenu.doc.DocumentElement.SelectSingleNode("Welcome");
						XmlNode forum = this.leftMenu.doc.DocumentElement.SelectSingleNode("Forum");
						XmlNode items = this.leftMenu.doc.DocumentElement.SelectSingleNode("Items");
						items.InnerXml += welcome.InnerXml;
						items.InnerXml += forum.InnerXml;
					}
				} else {
					this.Response.Redirect(Resources.PageUrl.PAGE_TRAININGS + "?index=4");
				}
			}
			
			if(this.m_courseId.HasValue) {
				DataRow _course = DceAccessLib.DAL.CourseController.Select(this.m_courseId.Value);
				
				if (null != _course) {
					rowCourse = _course;
					this.m_courseId = (Guid?)_course["id"];
					Session["CourseLanguage"] = _course["CourseLanguage"].ToString();
				}
			}
			DCE.Service.courseId = this.m_courseId;

			if(!this.m_courseId.HasValue) {
				this.Response.Redirect(Resources.PageUrl.PAGE_MAIN + "?index=1");
			}

			this.onLoadCenter();

			if (rowCourse != null) {
				if (rowCourse["Name"] != null) {
					this.Session["courseName"] = (string)rowCourse["Name"];
				}
				
				doc = this.leftMenu.doc;
				XmlNode items = doc.DocumentElement.SelectSingleNode("Items");
				
				if (items != null) {
					this.addThemes(
							this.m_courseId,
							items,
							this.m_trainingId,
							0,
							"ContentView");
				}
			}
		}

      /// <summary>
      /// ƒобавление тем и подтем в меню
      /// </summary>
      /// <param name="parentId">id родител€</param>
      /// <param name="parentNode">xml node родител€</param>
      /// <param name="isCourseTraining">ƒобавл€ть ли тесты и практику (не публичный курс)</param>
      /// <param name="trId"></param>
      /// <param name="level">уровень вложенности</param>
      /// <returns>ƒопустимо ли прохожнение следующих тем - определ€етс€ тестом</returns>
		bool addThemes(
				Guid? parentId,
				XmlNode parentNode,
				Guid? trId,
				int level,
				string control)
		{
			bool isTraining = false;
			bool AllowNext = true; // ƒоступность следующих тем
			
			if (parentId.HasValue) {
				// запрос дл€ свободного курса
				string select = string.Format(@"
SELECT	id,
		TOrder,
		Duration,
		Practice,
		Mandatory,
		CONVERT(bit,1) as isOpen,
		dbo.GetStrContentAlt(
			Name,
			'{0}',
			'{1}') as text,
		'{2}' as control,
		Content,
		Duration,
		Practice,
		Mandatory,
		{3} as selected
from dbo.Themes
where	Parent='{4}'
		and Practice is NULL
		and Type={5}
order by TOrder",
		LocalisationService.Language,
		LocalisationService.DefaultLanguage,
		control,
		this.m_selectedId.HasValue
			? string.Format("dbo.IsChildTheme(id, '{0}')", this.m_selectedId.Value.ToString())
			: "0",
		parentId.Value,
		(int)DCEAccessLib.ThemeType.theme);

				if(trId.HasValue) {
					if (level != 0) {
						select = string.Format(@"
select	t.id,
		t.TOrder,
		t.Duration,
		t.Practice,
		dbo.GetStrContentAlt(t.Name, '{0}', '{1}') as text,
		'{2}' as control,
		t.Content,
		tr.TestOnly {3}
from	dbo.Themes t,
		dbo.Trainings tr 
where	t.Parent='{4}' 
		and tr.id='{5}'
		and Type={6}
order by	t.TOrder",
									LocalisationService.Language,
									LocalisationService.DefaultLanguage,
									control,
									this.m_selectedId.HasValue
											? string.Format(",dbo.IsChildTheme(t.id, '{0}') as selected ", this.m_selectedId.Value)
											: string.Empty,
									parentId,
									trId,
									(int)DCEAccessLib.ThemeType.theme);
					} else {
						select = string.Format(@"
select	t.id,
		t.TOrder,
		t.Duration,
		t.Practice,
		dbo.GetStrContentAlt(t.Name, '{0}','{1}') as text,
		'{2}' as control,
		t.Content,
		(t.Mandatory & s.Mandatory) as Mandatory,
		s.isOpen,
		s.StartDate,
		s.EndDate,
		tr.TestOnly {3}
from	dbo.Themes t,
		dbo.Schedule s,
		dbo.Trainings tr 
where	t.Parent='{4}' 
		and s.Theme=t.id
		and s.Training=tr.id
		and tr.id='{5}'
		and Type={6}
order by	t.TOrder",
									LocalisationService.Language,
									LocalisationService.DefaultLanguage,
									control,
									this.m_selectedId.HasValue
										? string.Format(",dbo.IsChildTheme(t.id, '{0}') as selected ", this.m_selectedId.Value)
										: string.Empty,
									parentId,
									trId,
									(int)DCEAccessLib.ThemeType.theme);
					}
					isTraining = true;
				}
				
				DataSet dsThemes = dbData.Instance.getDataSet(select,  "dataSet", "item");
				DataTable tableThemes = dsThemes.Tables["item"];
				bool testOnly = tableThemes != null && tableThemes.Rows.Count == 0 && level == 0;
				
				if (tableThemes != null 
						&& tableThemes.Rows.Count > 0 
						&& (!isTraining || !(testOnly = (bool) tableThemes.Rows[0]["TestOnly"]))) {
					string themes = dsThemes.GetXml();
					XmlDocument tdoc = new XmlDocument();
					tdoc.LoadXml(themes);
					
					foreach (System.Xml.XmlNode theme in tdoc.SelectNodes("dataSet/item")) {
						// Is Practice
						XmlNode practice = theme.SelectSingleNode("Practice");
						XmlNode name = theme.SelectSingleNode("text");
						bool isDateComplete = true;
						
						XmlNode startDateNode = theme.SelectSingleNode("StartDate");
						XmlNode endDateNode = theme.SelectSingleNode("EndDate");
						DateTime startDate = DateTime.Now;
						DateTime endDate = DateTime.Now;
						
						if (startDateNode != null) {
							XmlNode alt = tdoc.CreateNode(XmlNodeType.Element, "alt", string.Empty);
							startDate = DateTime.Parse(startDateNode.InnerText);
							startDateNode.InnerText = startDate.ToString("dd.MM.yyyy");
							alt.InnerText += startDateNode.InnerText;
							isDateComplete = startDate <= DateTime.Now;
							
							if (endDateNode != null) {
								endDate = DateTime.Parse(endDateNode.InnerText);
								endDateNode.InnerText = endDate.ToString("dd.MM.yyyy");
								alt.InnerText += " / "+endDateNode.InnerText;
							}
							
							theme.AppendChild(alt);
						}
						
						if (practice != null && practice.InnerText != "") { // ѕрактика
							theme.SelectSingleNode("control").InnerText = "Practice";
							theme.SelectSingleNode("id").InnerText = practice.InnerText;
							XmlNode practiceA = doc.DocumentElement.SelectSingleNode("Practic");
							
							if (practiceA != null) {
								if (name == null) {
									name = tdoc.CreateNode(XmlNodeType.Element, "text", "");
								}
								name.InnerText = practiceA.InnerText + name.InnerText;
								theme.AppendChild(name);
							}
						}
						
						XmlNode openNode = theme.SelectSingleNode("isOpen");
						bool isOpen = openNode != null && openNode.InnerText == "true";
						
						if(trId.HasValue && level == 0 && (!AllowNext || (!isOpen && !isDateComplete))) { // “ема закрыта
							XmlAttribute a = tdoc.CreateAttribute("disabled");
							a.Value = "true";
							theme.Attributes.Append(a);
							AllowNext = false;
						}
						
						XmlNode selNode = theme.SelectSingleNode("selected");
						if (level == 0 || (selNode != null && selNode.InnerText == "true")) {
							if (!this.addThemes(
										GuidService.Parse(theme.SelectSingleNode("id").InnerText),
										theme,
										trId,
										level + 1,
										control)) {
								AllowNext = false;
							}
						}
					}
					
					parentNode.InnerXml += tdoc.SelectSingleNode("dataSet").InnerXml;
				}
				
				if (trId.HasValue) { // ƒобавление теста темы или курса
					AddTest(parentId, this.doc, ref parentNode, level, ref AllowNext, testOnly);
				}
				
				// ƒобавить библиотеку
				AddLibrary(parentId, level, testOnly, this.doc, ref parentNode);
				
				// ƒобавить словарь
				if (level == 0 && !testOnly) {
					AddVocabulary(parentId, this.m_courseId, this.doc, ref parentNode);
				}
			}
			return AllowNext; // ƒопустимо ли прохожнение следующих тем - определ€етс€ тестом
		}

		static void AddTest(Guid? parentId, XmlDocument xmlDoc, ref XmlNode parentNode, int level, ref bool AllowNext, bool testOnly)
		{
			Guid? studentId = CurrentUser.UserID;
			XmlNode testNode = xmlDoc.SelectSingleNode((level > 0 || testOnly)
						? "/xml/Test"
						: "/xml/FinalTest");
			
			string test = testNode != null ? testNode.InnerText : "Test";
			DataSet dsTest = Test_Select(parentId, studentId, test);
			DataTable tableTest = dsTest.Tables["item"];

			if (tableTest != null && tableTest.Rows.Count > 0 && (AllowNext || level > 0)) { // ƒопустимо ли прохожнение следующих тем
				AllowNext = AllowNext && (bool)tableTest.Rows[0]["AllowNext"];

				test = dsTest.GetXml();
				XmlDocument tdoc = new XmlDocument();
				tdoc.LoadXml(test);
				parentNode.InnerXml += tdoc.SelectSingleNode("dataSet").InnerXml;
			}
		}

		void AddLibrary(Guid? parentId, int level, bool testOnly, XmlDocument xmlDoc, ref XmlNode parentNode)
		{
			DataSet dsLib = Test_SelectThemes(parentId);
			DataTable tableLib = dsLib.Tables["item"];

			if (tableLib != null && tableLib.Rows.Count > 0 && !testOnly) {
				XmlNode themeA = xmlDoc.DocumentElement.SelectSingleNode("Library");

				if (themeA != null && level == 0) {
					tableLib.Rows[0]["text"] = themeA.InnerText;
				}

				string themes = dsLib.GetXml();
				XmlDocument tdoc = new XmlDocument();
				tdoc.LoadXml(themes);

				foreach (XmlNode theme in tdoc.SelectNodes("dataSet/item")) {
					this.addThemes(
						GuidService.Parse(theme.SelectSingleNode("id").InnerText),
						theme,
						null,
						level + 1,
						"Library");
				}

				parentNode.InnerXml += tdoc.SelectSingleNode("dataSet").InnerXml;
			}
		}

		static void AddVocabulary(Guid? parentId, Guid? courseId, XmlDocument xmlDoc, ref XmlNode parentNode)
		{
			DataSet dsTerms = Test_SelectTerms(courseId);
			DataTable tableTerms = dsTerms.Tables["term"];

			if (tableTerms != null && tableTerms.Rows.Count > 0) {
				DataSet dsVocabulary = Test_SelectVocabulary(parentId);
				DataTable tableVocabulary = dsVocabulary.Tables["item"];

				if (tableVocabulary != null && tableVocabulary.Rows.Count > 0) {
					XmlNode themeA = xmlDoc.DocumentElement.SelectSingleNode("Vocabulary");

					if (themeA != null) {
						tableVocabulary.Rows[0]["text"] = themeA.InnerText;
					}

					string themes = dsVocabulary.GetXml();
					XmlDocument tdoc = new XmlDocument();
					tdoc.LoadXml(themes);
					parentNode.InnerXml += tdoc.SelectSingleNode("dataSet").InnerXml;
				}
			}
		}

		static DataSet Test_SelectVocabulary(Guid? parentId)
		{
			string select0 = string.Format(@"
select	top 1
		id,
		'Vocabulary' as text,
		'Vocabulary' as control
from	dbo.Vocabulary
where Course='{0}'",
					parentId);
			return dbData.Instance.getDataSet(select0, "dataSet", "item");
		}

		static DataSet Test_SelectTerms(Guid? courseId)
		{
			string select0 = string.Format(@"
SELECT	t.id
FROM	VTerms t,
		Vocabulary v
WHERE	v.Term = t.id
		and v.Course =  '{0}'",
						courseId);
			return dbData.Instance.getDataSet(select0, "ds", "term");
		}
		
		static DataSet Test_SelectThemes(Guid? parentId)
		{
			string select0 = string.Format(@"
select	id,
		dbo.GetStrContentAlt(Name, '{0}',
		'{1}') as text,
		'Library' as control,
		Content, TOrder
from	dbo.Themes
where Parent='{2}'
		and Type={3}
order by TOrder",
					LocalisationService.Language,
					LocalisationService.DefaultLanguage,
					parentId,
					(int)DCEAccessLib.ThemeType.library);
			
			return dbData.Instance.getDataSet(select0, "dataSet", "item");
		}

		static DataSet Test_Select(Guid? parentId, Guid? studentId, string test)
		{
			string select0;
			if (studentId.HasValue) {
				select0 = string.Format(@"
select	t.id,
		(/*((dbo.IsTestMandatory(t.id) ^ 1) & ISNULL(tr.Skipped,0)) | */ISNULL(tr.Skipped,0) | ISNULL(tr.Complete, 0)) as AllowNext,
		'{0}' as text,
		'TestWork' as control
from	dbo.Tests t
	left join dbo.TestResults tr
		on (tr.Test=t.id and tr.Student='{1}')
where	Type={2}
		and t.Parent='{3}'",
						test,
						studentId,
						(int)DCEAccessLib.TestType.test,
						parentId);
			} else {
				select0 = string.Format(@"
select	id,
		'{0}' as text,
		'TestWork' as control, 
		CONVERT(bit, 1) AS AllowNext 
from	dbo.Tests
where	Type=1
		and Parent='{1}'",
						test,
						parentId);
			}

			DataSet dsTest = dbData.Instance.getDataSet(select0, "dataSet", "item");
			return dsTest;
		}

		void onLoadCenter()
		{
			string _cset = this.Request["cset"];
			
			Control _ctl = null;

			if (!string.IsNullOrEmpty(_cset)) {
				_ctl = this.LoadControl(@"~\Common\" + _cset + ".ascx");
			}
			
			if(null == _ctl) {
				_ctl = !DCE.Service.TrainingID.HasValue 
					? this.LoadControl(@"~\Common\FreeIntro.ascx")
					: this.LoadControl(@"~\Common\Welcome.ascx");
			}

			this.PlaceHolder1.Controls.Add(_ctl);
		}
   }
}
