using System;
using System.Data;
using System.Web.UI;
using System.Xml;

namespace DCE
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Xml.Linq;
	using N2.Lms.Items;
	/// <summary>
	/// ќсновна€ страница обучени€
	/// </summary>
	public partial class Training : DCE.BaseWebPage
	{
		private XmlDocument doc = null;
		Guid? m_courseId;
		Guid? m_selectedId;
		Guid? m_trainingId;

		protected override void OnInit(EventArgs e)
		{
			MenuItem _root = new MenuItem();

			base.OnInit(e);

			this.m_selectedId = PageParameters.ID;
			this.m_courseId = PageParameters.courseId;
			this.m_trainingId = PageParameters.trId;

			DataSet dsCourse = null;
			DataTable tableCourse = null;
			Course rowCourse = null;

			if (this.m_trainingId.HasValue) {
				DCE.Service.LoadXmlDoc(this.Page, this.LeftMenu.doc, @"TrainingsLeft.xml");

				Debug.WriteLine("#36" + this.LeftMenu.doc.InnerXml, "sty");
			} else {
				Debug.WriteLine("#38", "sty");
				if (this.m_courseId.HasValue) {
					if (!DceAccessLib.DAL.CourseController.Exists(this.m_courseId.Value)) {
						this.m_courseId = null;
					}
				}
				DCE.Service.courseId = this.m_courseId;
				
				this.LeftMenu.doc.InnerXml = new XElement("Items",
					new XElement("item",
						new XAttribute("text", Resources.TrainingsLeft.Welcome_item_text),
						new XAttribute("alt", Resources.TrainingsLeft.Welcome_item_alt),
						new XAttribute("page", Resources.TrainingsLeft.Welcome_item_page),
						new XAttribute("control", Resources.TrainingsLeft.Welcome_item_control)
					)
				).Value;
			}

			DCE.Service.TrainingID = this.m_trainingId;

			if (this.m_trainingId.HasValue && CurrentUser.UserID.HasValue) {
				var _training = DceAccessLib.DAL.TrainingController.Select(PageParameters.trId.Value, CurrentUser.UserID.Value);
			
				if (null != _training) {
					this.m_courseId = GuidService.Parse(_training.Course.Name);
				
					if (!_training.TestOnly) {

						_root.Children.Concat(
							(new N2.ContentItem[] {
									new MenuItem {
										Name = "Welcome",
										Title = Resources.TrainingsLeft.Welcome_item_alt,
										NavigateUrl = "Training.aspx?cset=Welcome"},
									new MenuItem {
										Name = "Class",
										Title = Resources.TrainingsLeft.Forum_item_alt,
										NavigateUrl = "Training.aspx?cset=classRoomForum",
										Children = (new N2.ContentItem[] {
											new MenuItem {
												Name = "Forum",
												Title = Resources.TrainingsLeft.Forum_item_forum_alt,
												NavigateUrl = "Training.aspx?cset=classRoomForum",
											},
											new MenuItem {
												Name = "Assignments",
												Title = Resources.TrainingsLeft.Forum_item_task_alt,
												NavigateUrl = "Training.aspx?cset=classRoomForum",
											},
										}).ToList()
									},
								}).ToList());
					}
				} else {
					this.Response.Redirect(Resources.PageUrl.PAGE_TRAININGS + "?index=4");
				}
			}
			
			if(this.m_courseId.HasValue) {
				var _course = DceAccessLib.DAL.CourseController.Select(this.m_courseId.Value);
				
				if (null != _course) {

					Debug.WriteLine("course not null", "sty");

					rowCourse = _course;
					this.m_courseId = GuidService.Parse(_course.Name);
					//DCE.Service.CourseLanguage = _course["CourseLanguage"].ToString();
				}
				DCE.Service.courseId = this.m_courseId;
			} else {
				this.Response.Redirect(Resources.PageUrl.PAGE_MAIN + "?index=1");
			}

			this.onLoadCenter();
			
			Debug.WriteLine("Row course: " + rowCourse.Title, "sty");
			Debug.WriteLine("Course id: " + this.m_courseId, "sty");
			Debug.WriteLine("Training id: " + this.m_trainingId, "sty");

			if (rowCourse != null) {
				if (!string.IsNullOrEmpty(rowCourse.Title)) {
					this.Session["courseName"] = rowCourse.Title;
				}
				
				doc = this.LeftMenu.doc;
				XmlNode items = doc.DocumentElement.SelectSingleNode("Items");
				Debug.WriteLine(doc.InnerXml, "sty");
				if (items != null) {
					this.addThemes(
							this.m_courseId,
							items,
							this.m_trainingId,
							10,
							"ContentView");
				}
			}

			base.Tree.CurrentItem = base.Tree.RootItem = _root;
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

			if (!parentId.HasValue) {
				return true;
			}
			
			bool AllowNext = true; // ƒоступность следующих тем
			IEnumerable<TrainingSchedule> _schedule;

			// запрос дл€ свободного курса
			if (!trId.HasValue) {
				_schedule = GetFreeCourseSchedule(parentId);
			} else {
				
				using (var _ctx = new Lms.LmsDataContext()) {
					_schedule = (
						from _theme in _ctx.Themes
						join _training in _ctx.Trainings
							on _theme.Parent equals parentId.Value
						join _sch in _ctx.Schedules
							on new { th = _theme.id, tr = _training.id } equals
								new { th = _sch.Theme ?? Guid.Empty, tr = _sch.Training ?? Guid.Empty }
							into _sched
						where _training.id == trId
							&& _theme.Type == (int)DCEAccessLib.ThemeType.theme
						from _sch in _sched.DefaultIfEmpty()
						let _level0 = null != _sch || level == 0
						select new TrainingSchedule {
							SortOrder = _theme.TOrder,
							Theme = new Topic {
								Name = _theme.id.ToString(),
								Duration = _theme.Duration,
								Practice = _theme.Practice == null
									? new Test {
										Name = _theme.Practice.ToString()
									}
									: null,
								Parent = new N2.Lms.Items.Training {
									Name = _training.id.ToString(),
									TestOnly = _training.TestOnly,
								},
								ContentUrl = _theme.Content.ToString(),
								Title = _ctx.GetStrContentAlt(
									_theme.Name,
									LocalisationService.Language,
									LocalisationService.DefaultLanguage),
							},
							Published = _level0 ? _sch.StartDate : default(DateTime?),
							Expires = _level0 ? _sch.EndDate : default(DateTime?),
							IsOpen = _level0 ? _sch.isOpen : false,
							Training = new N2.Lms.Items.Training {
								Name = _training.id.ToString(),
								TestOnly = _training.TestOnly,
							},
						}).ToList();
				}

				isTraining = true;
			}

			bool testOnly = !_schedule.Any() && level == 0;

			if (_schedule.Any()
					&& (!isTraining || !(testOnly = _schedule.First().Training.TestOnly))) {
				
				Debug.WriteLine("Schedules #: " + _schedule.Count(), "sty");
				
				foreach (var _sch in _schedule) {
					// Is Practice
					var practice = _sch.Theme.Practice;
					bool isDateComplete = true;

					var _node = new XElement("item");

					if (_sch.Published.HasValue) {
						string _alt = _sch.Published.Value.ToString("dd.MM.yyyy");
						isDateComplete = _sch.Published.Value <= DateTime.Now;

						if (_sch.Expires.HasValue) {
							_alt += " / " + _sch.Expires.Value.ToString("dd.MM.yyyy");
						}
						_node.Add(new XElement("alt", _alt));
					}

					if (practice != null) { // ѕрактика
						_node.Add(new XElement("control", "Practice"),
							new XElement("id", practice.Name),
							new XElement("text", practice.Title));
					}

					if (trId.HasValue && level == 0 && (!AllowNext || (!_sch.IsOpen && !isDateComplete))) { // “ема закрыта
						_node.Add(new XAttribute("disabled", true));
						AllowNext = false;
					}

					if (level == 0) {
						if (!this.addThemes(
									GuidService.Parse(_sch.Theme.Name),
									null,
									trId,
									level + 1,
									control)) {
							AllowNext = false;
						}
					}
				}

				//parentNode.InnerXml += tdoc.SelectSingleNode("dataSet").InnerXml;
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

			return AllowNext; // ƒопустимо ли прохожнение следующих тем - определ€етс€ тестом
		}

		static IEnumerable<TrainingSchedule> GetFreeCourseSchedule(Guid? parentId)
		{
				using (var _ctx = new Lms.LmsDataContext()) {
					return (
						from _theme in _ctx.Themes
						where _theme.Parent == parentId.Value
							&& !_theme.Practice.HasValue
							&& _theme.Type == (int)DCEAccessLib.ThemeType.theme
						select new TrainingSchedule {
							SortOrder = _theme.TOrder,
							IsOpen = true,
							Theme = new Topic {
								Name = _theme.id.ToString(),
								Practice = _theme.Practice.HasValue
									? new Test {
										Name = _theme.Practice.ToString()
									}
									: null,
								Title = _ctx.GetStrContentAlt(
									_theme.Name,
									LocalisationService.Language,
									LocalisationService.DefaultLanguage),
								Mandatory = _theme.Mandatory,
								Duration = _theme.Duration,
								ContentUrl = _theme.Content.ToString(),
							}
						}).ToList();
				}
		}

		static void AddTest(Guid? parentId, XmlDocument xmlDoc, ref XmlNode parentNode, int level, ref bool AllowNext, bool testOnly)
		{
			Guid? studentId = CurrentUser.UserID;
			DataSet dsTest = Test_Select(parentId, studentId);
			DataTable tableTest = dsTest.Tables["item"];

			if (tableTest != null && tableTest.Rows.Count > 0 && (AllowNext || level > 0)) { // ƒопустимо ли прохожнение следующих тем
				AllowNext = AllowNext && (bool)tableTest.Rows[0]["AllowNext"];

				string test = dsTest.GetXml();
				XmlDocument tdoc = new XmlDocument();
				tdoc.LoadXml(test);
				var _node = tdoc.SelectSingleNode("dataSet");
				var _c = tdoc.CreateElement("text");
				_c.InnerText = (level > 0 || testOnly)
						? Resources.TrainingsLeft.Test
						: Resources.TrainingsLeft.FinalTest;
				_node.SelectSingleNode("item").AppendChild(_c);

				_c = tdoc.CreateElement("control");
				_c.InnerText = "TestWork";
				_node.SelectSingleNode("item").AppendChild(_c);

				
				parentNode.InnerXml += _node.InnerXml;
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

		static DataSet Test_Select(Guid? parentId, Guid? studentId)
		{
			string select0;
			if (studentId.HasValue) {
				select0 = string.Format(@"
select	t.id,
		(ISNULL(tr.Skipped,0) | ISNULL(tr.Complete, 0)) as AllowNext
from	dbo.Tests t
	left join dbo.TestResults tr
		on (tr.Test=t.id and tr.Student='{1}')
where	Type={2}
		and t.Parent='{3}'",
						string.Empty,
						studentId,
						(int)DCEAccessLib.TestType.test,
						parentId);
			} else {
				select0 = string.Format(@"
select	id, CONVERT(bit, 1) AS AllowNext 
from	dbo.Tests
where	Type=1
		and Parent='{0}'",
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
