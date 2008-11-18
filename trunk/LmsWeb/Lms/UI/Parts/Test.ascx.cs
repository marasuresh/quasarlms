using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace N2.Lms.UI.Parts
{
	using N2.Lms.Web.UI.WebControls;
	using N2.Templates.Items;
	using N2.Templates.Web.UI;
	using N2.Web.UI;
	
	public partial class TestControl : TemplateUserControl<AbstractContentPage, N2.Lms.Items.Test>
	{
		public int Score {
			get { return (int?)this.ViewState["Score"] ?? 0; }
			set { this.ViewState["Score"] = value; }
		}
		
		protected DateTimeOffset? StartedOn {
			get { return (DateTimeOffset?)this.ViewState["StartedOn"]; }
			set { this.ViewState["StartedOn"] = value; }
		}

		protected bool InstantCheckEnabled {
			get { return false && this.CurrentItem.InstantCheckEnabled; }
		}

		#region Rendering helpers

		TimeSpan ElapsedTime {
			get {
				return
					this.StartedOn.HasValue
						? DateTimeOffset.Now - this.StartedOn.Value
						: TimeSpan.MinValue;
			}
		}

		protected string ElapsedTimeString {
			get {
				var _et = this.ElapsedTime;
				return string.Join(":", new[] {
						_et.Hours,
						_et.Minutes,
						_et.Seconds }
					.Select(i => i.ToString("00"))
					.ToArray());
			}
		}

		#endregion Rendering helpers

		protected override void OnLoad(EventArgs e)
		{
			if (!this.StartedOn.HasValue) {
				this.StartedOn = DateTimeOffset.Now;
			}

			base.OnLoad(e);
		}

		protected void StartSession()
		{
		}

		protected void ResumeSession()
		{
		}

		protected void FinishSession()
		{
			this.mv.SetActiveView(this.vTimeExpired);
		}

		protected override void OnPreRender(EventArgs e)
		{
			if (this.ElapsedTime.TotalMinutes >= 1 + 0 * this.CurrentItem.Duration) {
				this.FinishSession();
			}

			this.btnCheck.Visible = !this.InstantCheckEnabled;
			base.OnPreRender(e);
		}

		protected virtual IEnumerable<TestQuestionControl> QuestionControls {
			get { return this.qz.Controls.OfType<TestQuestionControl>(); }
		}

		int CalculateTotalScore()
		{
			return this.QuestionControls
					.Select(_q => _q.Score)
					.Sum();
		}

		#region Event handlers
		
		protected void zone_AddedItemTemplate(object sender, ControlEventArgs e)
		{
			var _ctl = (TestQuestionControl)e.Control;

			_ctl.AnswerChanged += (_sender, _e) => {
				if (_e.IsCorrect) {
					if (this.InstantCheckEnabled) {
						this.Score += _e.Question.Points;
					}
				}
			};

			_ctl.InstantCheckEnabled = this.InstantCheckEnabled;
		}

		protected void btnCheck_Click(object sender, EventArgs e)
		{
			this.Score = this.CalculateTotalScore();
		}

		#endregion Event handlers
	}
}