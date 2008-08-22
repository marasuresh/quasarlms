namespace N2.Lms
{
	using N2.Definitions;
	using N2.Persistence;
	using N2.Persistence.Finder;
	using N2.Web;

	public class LmsProvider
	{
		#region Fields

		readonly IDefinitionManager m_definitions;
		readonly IItemFinder m_finder;
		readonly IPersister m_persister;
		string m_courseContainerName = "CourseList";
		int m_courseContainerParentID = 1;

		#endregion Fields

		#region Constructor

		public LmsProvider(
				IDefinitionManager definitions,
				IItemFinder finder,
				IPersister persister,
				IHost host)
		{
			this.m_definitions = definitions;
			this.m_finder = finder;
			this.m_persister = persister;
			this.m_courseContainerParentID = host.DefaultSite.StartPageID;
			System.Diagnostics.Debug.WriteLine("Start Page: " + host.DefaultSite.StartPageID.ToString());
			this.GetCourseContainer(true);
		}

		#endregion Constructor

		#region Properties

		public IItemFinder Finder { get { return this.m_finder; } }

		protected int CourseContainerParentID {
			get { return this.m_courseContainerParentID; }
			set { this.m_courseContainerParentID = value; }
		}

		public string CourseContainerName {
			get { return this.m_courseContainerName; }
			set { this.m_courseContainerName = value; }
		}

		#endregion Properties

		#region Methods

		public virtual Items.CourseList GetCourseContainer(bool create)
		{
			ContentItem parent = this.m_persister.Get(this.CourseContainerParentID);
			var m = parent.GetChild(this.CourseContainerName) as Items.CourseList;
			if (m == null && create) {
				m = this.CreateCourseContainer(parent);
			}
			return m;
		}

		protected Items.CourseList CreateCourseContainer(ContentItem parent)
		{
			var m = this.m_definitions.CreateInstance<Items.CourseList>(parent);
			m.Title = "Courses";
			m.Name = this.CourseContainerName;
			this.m_persister.Save(m);
			return m;
		}

		#endregion Methods
	}
}
