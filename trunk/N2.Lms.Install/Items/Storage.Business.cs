namespace N2.Lms.Items
{
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Persistence;
	using N2.Templates.Items;
	using N2.Integrity;
	using N2.Details;
	using N2.Messaging;

	[AllowedChildren(
		typeof(CourseContainer),
		typeof(RequestContainer),
		typeof(Messaging.MessageStore),
		typeof(ACalendar.ACalendarContainer))]
	partial class Storage: IStorageItem
	{
		[EditableItem("CourseContainer", "", 3, Title = "Course container")]
		public CourseContainer Courses { get {
				return this.GetOrFindOrCreateChild<CourseContainer>("CourseContainer", null);
		} set { this.SetDetail<CourseContainer>("CourseContainer", value); } }

		[EditableItem("RequestContainer", "", 5, Title = "Request container")]
		public RequestContainer Requests { get {
				return this.GetOrFindOrCreateChild<RequestContainer>("RequestContainer", null);
		} }

		[EditableItem("MessageStore", "", 7, Title = "Message store")]
		public MessageStore Messages {
			get {
				return this.GetOrFindOrCreateChild<MessageStore>("MessageStore", null);
			}
		}
	}
}
