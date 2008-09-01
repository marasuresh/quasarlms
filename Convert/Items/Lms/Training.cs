using System;
using System.Collections.Generic;
using System.Linq;

namespace N2.Lms.Items
{
	using N2.Security.Items;
	using N2.Details;
	using N2.Integrity;
	using N2.Templates.Items;
	using N2.Web.UI;

	[RestrictParents(typeof(TrainingList))]
	[Definition]
	[WithEditableDateRange("Validity period", 50, "StartOn", "FinishOn", ContainerName="lms")]
	[TabPanel("lms", "LMS", 200)]
	public class Training : AbstractContentPage
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

		[EditableCheckBox("Test Only", 40, ContainerName="lms")]
		public bool TestOnly {
			get { return (bool?)this.GetDetail("TestOnly") ?? false; }
			set { this.SetDetail<bool>("TestOnly", value); }
		}

		public DateTime StartOn {
			get { return (DateTime?)this.GetDetail("StartOn") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("StartOn", value); }
		}

		public DateTime FinishOn {
			get { return (DateTime?)this.GetDetail("FinishOn") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("FinishOn", value); }
		}

		public IEnumerable<User> Members {
			get;
			set;
		}

		#endregion Lms Properties
	}
}
