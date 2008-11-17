using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace N2.Calendar.Curriculum.UI.Views
{
	using N2.Lms.Items;
	using N2.Details;

	public partial class Curriculum : System.Web.UI.UserControl
	{
		/// <summary>
		/// Holds an ID of a container object -- an absolute minimum information that is needed to recreate it
		/// </summary>
		public int? CourseContainerId {
			get { return (int?)this.ViewState["CourseContainerId"]; }
			protected set { this.ViewState["CourseContainerId"] = value; }
		}

		/// <summary>
		/// Holds a collection name currently being edited
		/// </summary>
		public string CurrentCurriculumName {
			get { return (string)this.ViewState["CurriculumName"]; }
			set { this.ViewState["CurriculumName"] = value; }
		}

		CourseContainer m_container;
		/// <summary>
		/// An instance of a container, that can be recreated if neccessary
		/// </summary>
		public CourseContainer CourseContainer {
			get {
				return
					this.m_container
					?? (this.m_container = this.CourseContainerId.HasValue
						? N2.Context.Persister.Get<CourseContainer>(this.CourseContainerId.Value)
						: null);
			}
			set {
				if (null != value) {
					this.CourseContainerId = value.ID;
				}
				this.m_container = value;
			}
		}

		IDictionary<string, int> m_curriculum;
		/// <summary>
		/// Detail collection, wrapped into dictionary
		/// </summary>
		protected IDictionary<string, int> CurrentCurriculum {
			get {
				if (null == this.m_curriculum
						&& !string.IsNullOrEmpty(this.CurrentCurriculumName)
						&& null != this.CourseContainer) {
					
					this.m_curriculum = this.CourseContainer
							.GetDetailCollection(this.CurrentCurriculumName, true)
							.AsDictionary<int>();
				}

				return m_curriculum;
			}
		}

		/// <summary>
		/// Merge Detail collection content with that in edit interface
		/// </summary>
		public bool Update()
		{
			var _curriculum = this.CurrentCurriculum;
			if (null != _curriculum) {
				return this.UpdateCurriculumFromControls(ref _curriculum);
			}

			return false;
		}

		/// <summary>
		/// Internal merging implementation ..
		/// </summary>
		/// <param name="original"></param>
		bool UpdateCurriculumFromControls(ref IDictionary<string, int> original)
		{
			var _dataFromControls = (
						from _item in this.rpt.Items.Cast<RepeaterItem>()
						let _rbl = _item.FindControl("rbl") as RadioButtonList
						let _hf = _item.FindControl("hf") as HiddenField
						select new {
							Key = _hf.Value,
							Value = _rbl.SelectedIndex,
						}
					).ToDictionary(i => i.Key, i => i.Value);

			var _result = false;

			foreach(var _name in _dataFromControls
					.Keys
					.Concat(original.Keys)
					.Distinct()) {
				if(original[_name] != _dataFromControls[_name]) {
					original[_name] = _dataFromControls[_name];
					_result = true;
				}
			}

			return _result;
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
			OnChanged(EventArgs.Empty);
		}

		protected void ds_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
		{
			e.InputParameters["curriculumName"] = this.CurrentCurriculumName;
		}

		protected void ds_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
		{
			e.ObjectInstance = this.CourseContainer;
		}
	}
}