namespace N2.Lms.Items
{
	using System.Collections.Generic;
	using System.Linq;
	using N2.Collections;
	using N2.Details;
	using N2.Templates.Items;

	partial class Test
	{
		public virtual IEnumerable<TestQuestion> Questions
		{
			get { return
				this.GetChildren(new TypeFilter(typeof(TestQuestion)))
					.Cast<TestQuestion>();
			}
		}
	}
}
