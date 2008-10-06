namespace N2.Lms.Items
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Diagnostics;

	using N2.Details;
	using N2.Definitions;
	using N2.Installation;
	using N2.Integrity;
	using N2.Edit.Trash;
	using N2.Templates.Items;
	using N2.Serialization;
	using N2.Web.UI;
	using N2.Collections;

	partial class Course
	{
		#region Lms Collection Properties

		/// <summary>
		/// Storage node for topic hierarchy
		/// </summary>
		internal TopicList TopicContainer {
			//Cannot just GetChild by Course.TopicContainerName
			// because TopicList may be created not only by [EnsureChild]
			// but as a result of the import procedure, in which case
			// Name will be assigned an arbitrary value, such as course code.
			get { return this.GetChildren(new TypeFilter(typeof(TopicList)))
				.Cast<TopicList>()
				.First(); }
		}

		public IEnumerable<Topic> Topics { get { return this.TopicContainer.Topics; } }

		/// <summary>
		/// Storage node for trainings
		/// </summary>
		internal TrainingContainer TrainingContainer {
			get { return this.GetChildren(new TypeFilter(typeof(TrainingContainer)))
				.Cast<TrainingContainer>()
				.First(); }
		}

		public IEnumerable<Training> Trainings { get { return this.TrainingContainer.Trainings; } }

		#endregion Lms Collection Properties
	}
}