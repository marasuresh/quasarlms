namespace N2.Lms.Items
{
	using System.Collections.Generic;
	using System.Linq;
	using N2.Collections;
	using N2.Details;
	using N2.Templates.Items;

	partial class Test
	{
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

		public virtual IEnumerable<TestQuestion> Questions
		{
			get { return
				this.GetChildren(new TypeFilter(typeof(TestQuestion)))
					.Cast<TestQuestion>();
			}
		}
	}
}
