using System.Collections.Generic;

namespace N2.Lms.Items
{
	using System.Linq;
	using N2.Details;
	using N2.Collections;

	partial class Topic
	{
		public bool IsTopLevel { get { return this.Parent is TopicContainer; } }
		
		Course m_course;
		public Course Course {
			get {
				return
					this.m_course ?? (
						this.m_course = this.Parent.Parent as Course //speed up for modules
						?? N2.Find.EnumerateParents(this)
							.OfType<Course>()
							.FirstOrDefault());
			}
		}

		//[EditableItem("Practice", 90, Required = false)]
		public Test Practice {
			get { return this.GetChildren(new TypeFilter(typeof(Test))).FirstOrDefault() as Test; }
		}

		public IEnumerable<Topic> Topics {
			get { return this.GetChildren(new TypeFilter(typeof(Topic))).OfType<Topic>(); }
		}
	}
}
