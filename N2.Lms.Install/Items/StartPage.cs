namespace N2.Lms.Items
{
	using N2;
	using N2.Persistence;
	using n2 = N2.Templates.Items;
	using N2.Details;
	using N2.Web.UI.WebControls;
	
	[Definition(
		"LMS Start Page",
		"LmsStartPage",
		"",
		"", 0)]
	[WithEditable(
		"Assignment List",
		typeof(ItemEditorList),
		"ParentItem", 4, "MyLearningPage")]
	public class LmsStartPage: n2.StartPage, ILmsStartPage, IActiveContent
	{
		#region Pages
		
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

		#endregion Pages

		[EditableItem("Storage", 5)]
		public IStorageItem Storage { get {
				return this.GetOrFindOrCreateChild<Storage>("Storage", null);
			}
		}

		#region IActiveContent Members

		ContentItem N2.Persistence.IActiveContent.CopyTo(ContentItem destination)
		{
			throw new System.NotImplementedException();
		}

		void N2.Persistence.IActiveContent.Delete()
		{
			N2.Context.Persister.Delete(this);
		}

		void N2.Persistence.IActiveContent.MoveTo(ContentItem destination)
		{
			throw new System.NotImplementedException();
		}

		void N2.Persistence.IActiveContent.Save()
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
