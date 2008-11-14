using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using N2.Lms.Items;

namespace N2.Calendar.Curriculum.UI.Views
{
	public partial class Curriculum : System.Web.UI.UserControl
	{
		public int CourseContainerId { get; set; }

		string _currentCurriculumName = string.Empty;
		public string CurrentCurriculumName {
			get {
				return _currentCurriculumName;
			}
			set {
				_currentCurriculumName = value;
			}
		}

		protected IEnumerable<CourseInfo> CourseInfos {
			get {

				return
					from _data in this.CourseData
					let _id = int.Parse(_data.Key)
					let _course = N2.Context.Current.Persister.Get<Course>(_id)
					select new CourseInfo {
						CourseID = _id,
						CourseName = _course.Title,
						CourseExclude = _data.Value,
					};
			}
		}

		#region Types
		
		protected class CourseInfo
		{
			public int CourseID { get; set; }
			public string CourseName { get; set; }
			public int CourseExclude { get; set; }
			public bool CourseObligatory { get; set; }
			public bool CourseOptional { get; set; }
		}

		#endregion Types

		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack) {
				if (this.CurrentCurriculum != null) {
					this.rpt.DataSource = CourseInfos;
					this.rpt.DataBind();
				}
			}

		}
		
		protected CourseContainer CourseContainer {
			get {
				return string.IsNullOrEmpty(this.CourseContainerId.ToString()) ?
					 null : N2.Context.Current.Persister.Get<CourseContainer>(this.CourseContainerId);
			}
		}
		
		public N2.Details.DetailCollection CurrentCurriculum
		{
			get
			{
				return string.IsNullOrEmpty(_currentCurriculumName) ?
					  null : this.CourseContainer.GetDetailCollection(_currentCurriculumName, false);
				//add 
			}
		}

		protected IDictionary<string, int> m_courseData;
		public IDictionary<string, int> CourseData {
			get {
				if (m_courseData == null) {
					this.m_courseData = new Dictionary<string, int>();

					this.m_courseData = (
						from _item in this.rpt.Items.Cast<RepeaterItem>()
						let _rbl = _item.FindControl("rbl") as RadioButtonList
						let _hf = _item.FindControl("hf") as HiddenField
						select new {
							Key = _hf.Value,
							Value = _rbl.SelectedIndex,
						}).ToDictionary(i => i.Key, i => i.Value);
				}

				return m_courseData;
			}
			set {
				m_courseData = value;
				if (m_courseData.Any()) {
					this.rpt.DataSource = CourseInfos;
					this.rpt.DataBind();
				}
			}
		}

		protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			((Label)e.Item.FindControl("l10")).Text = ((HiddenField)e.Item.FindControl("hf")).Value;
		}

		public event EventHandler Changed;

		protected virtual void OnChanged(EventArgs e)
		{
			if (this.Changed != null) {
				this.Changed(this, e);
			}
		}

		protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
		{
			var _rbl = sender as RadioButtonList;
			var _hf = _rbl.NamingContainer.FindControl("hf") as HiddenField;
			var _selectedCourseId = _hf.Value;
			int _selectedValue = _rbl.SelectedIndex;

			OnChanged(EventArgs.Empty);

			if (this.CourseData.ContainsKey(_selectedCourseId)) {
				this.CourseData[_selectedCourseId] = _selectedValue;
			}
		}
	}
}