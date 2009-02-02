using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace N2.Details
{
	using Lms;
	using Lms.Items;
	
	[AttributeUsage(AttributeTargets.Property)]
	public class EditableCourseDropDownAttribute: EditableDropDownAttribute
	{
		protected override ListItem[] GetListItems()
		{
			var _provider = Context.Current.Resolve<ILmsProvider>();

			return (
				from _course in _provider.Storage.Courses.Items
				select new ListItem {
					Text = _course.Title,
					Value = _course.ID.ToString(),
				}
			).ToArray();
		}

		protected override object GetValue(ListControl ddl)
		{
			return Context.Persister.Get(int.Parse(base.GetValue(ddl) as string));
		}

		protected override string GetValue(ContentItem item)
		{
			var _course = item[this.Name] as Course;
			return null != _course
				? _course.ID.ToString()
				: "0";
		}
	}
}
