namespace N2.Lms.Items
{
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Persistence;
	using N2.Templates.Items;

	partial class Storage
	{
		public CourseContainer Courses { get {
				return this.GetOrFindOrCreateChild<CourseContainer>("CourseContainer", null);
		} }

		public RequestContainer Requests { get {
				return this.GetOrFindOrCreateChild<RequestContainer>("RequestContainer", null);
		} }
	}
}
