using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace N2.Lms.Items
{
	using N2.Security.Items;
	using N2.Details;
	using N2.Integrity;

	[RestrictParents(typeof(TrainingList))]
	[Definition]
	[WithEditableTitle, WithEditableName]
	public class Training: ContentItem
	{
		#region Properties

		public override string IconUrl { get { return "~/Lms/UI/Img/01/43.png"; } }
		
		#endregion Properties

		#region Lms Properties

		internal TrainingList TrainingList {
			get { return this.Parent as TrainingList; }
		}

		public Course Course {
			get { return ((ContentItem)this.TrainingList ?? this).Parent as Course; }
			set { ((ContentItem)this.TrainingList ?? this).Parent = value; }
		}

		[EditableCheckBox("Test Only", 40)]
		public bool TestOnly {
			get { return (bool?)this.GetDetail("TestOnly") ?? false; }
			set { this.SetDetail<bool>("TestOnly", value); }
		}

		[EditableTextBox("Starts on", 50)]
		public DateTime StartOn
		{
			get { return (DateTime?)this.GetDetail("StartOn") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("StartOn", value); }
		}

		[EditableTextBox("Finishes on", 60)]
		public DateTime FinishOn
		{
			get { return (DateTime?)this.GetDetail("FinishOn") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("FinishOn", value); }
		}

		public IEnumerable<User> Members {
			get;
			set;
		}

		[EditableChildren("Schedule", "", 80)]
		public IEnumerable<TrainingSchedule> Schedule
		{
			get {
				return this
				  .GetChildren(new N2.Collections
					  .TypeFilter(typeof(TrainingSchedule)))
				  .Cast<TrainingSchedule>();
			}
		}

		#endregion Lms Properties
	}
}
