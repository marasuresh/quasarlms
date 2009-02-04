using System.Collections.Generic;

namespace N2.Lms.Items
{
	using Messaging;
	
	public interface IStorageItem
	{
		CourseContainer Courses { get; }
		RequestContainer Requests { get; }
		MessageStore Messages { get; }
	}
}
