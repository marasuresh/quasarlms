namespace N2.Lms.Items
{
	using System.Collections.Generic;
	using System.Linq;

	using N2.Details;
	using N2.Installation;
	using N2.Integrity;
	using N2.Edit.Trash;
	using N2.Templates.Items;
	using N2.Definitions;

	partial class Topic
	{
		public bool IsTopLevel { get { return this.Parent is TopicList; } }
		
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
	}
}
