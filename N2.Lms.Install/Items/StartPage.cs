namespace N2.Lms.Items
{
	using n2 = N2.Templates.Items;
	using N2.Details;
	
	[Definition(
		"LMS Start Page",
		"LmsStartPage",
		"",
		"", 0)]
	public class LmsStartPage: n2.StartPage
	{
		[EditableItem("MyLearningPage", 3)]
		public n2.TextPage MyLearningPage { get {
				return this.GetOrFindOrCreateChild<n2.TextPage>("MyLearningPage", _page => {

					_page.Name = "Learn";
					_page.Title = "Рабочий кабинет";
					_page.GetOrFindOrCreateChild<MyAssignmentList>(
						"MyAssignments", null);
					
					return _page;
				});
			}
		}

		[EditableItem("Storage", 5)]
		public Storage Storage { get {
				return this.GetOrFindOrCreateChild<Storage>("Storage", null);
			}
		}
	}
}
