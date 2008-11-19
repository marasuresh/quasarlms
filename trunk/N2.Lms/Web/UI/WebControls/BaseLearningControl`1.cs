namespace N2.Lms.Web.UI.WebControls
{
	using N2.Lms.Items.TrainingWorkflow;
	using N2.Templates.Items;
	using N2.Templates.Web.UI;
	
	/// <summary>
	/// Common base for User Controls displayed within a Course Player.
	/// </summary>
	/// <typeparam name="TLearningItem"></typeparam>
	public class BaseLearningControl<TLearningItem> : TemplateUserControl<AbstractContentPage, TLearningItem>
		where TLearningItem: ContentItem
	{
		#region Properties

		protected int? AttemptItemId {
			get { return (int?)this.ViewState["AttemptItemId"]; }
			set { this.ViewState["AttemptItemId"] = value; }
		}

		/// <summary>
		/// Holds a user-specific assignment information
		/// </summary>
		TrainingTicket m_AttemptItemInstance;
		public TrainingTicket AttemptItem {
			protected get {
				if (null == this.m_AttemptItemInstance && this.AttemptItemId.HasValue) {
					this.m_AttemptItemInstance = N2.Context.Persister.Get<TrainingTicket>(this.AttemptItemId.Value);
				}
				return this.m_AttemptItemInstance;
			}
			set {
				this.m_AttemptItemInstance = value;

				if (null != value) {
					this.AttemptItemId = value.ID;
				}
			}
		}

		#endregion Properties
	}
}
