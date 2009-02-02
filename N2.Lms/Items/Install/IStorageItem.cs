using System.Collections.Generic;

namespace N2.Lms.Items
{
	public interface IStorageItem
	{
		CourseContainer Courses { get; }
		RequestContainer Requests { get; }
	//	Messaging.MessageStore Messages { get; }
	}
}
