using System;
using System.Linq;

namespace N2.Details
{
	using System.Web.UI.WebControls;
	using N2;
	using N2.Engine;
	using N2.Workflow.Items;
	
	public class EditableStateTypeDropDownAttribute: EditableDropDownAttribute
	{
		protected override ListItem[] GetListItems()
		{
			return (
				from _type in Engine.Resolve<ITypeFinder>().Find(typeof(ItemState))
				select new ListItem(
					_type.FullName,
					GetFullyQualifiedTypeName(_type))
			).ToArray();
		}

		//DropDownList can only hold strings, so we need to convert System.Type back and forth
		protected override object GetValue(DropDownList ddl)
		{
			return Utility.TypeFromName((string)base.GetValue(ddl));
		}

		protected override string GetValue(ContentItem item)
		{
			return GetFullyQualifiedTypeName(item[Name] as Type);
		}

		static string GetFullyQualifiedTypeName(Type type)
		{
			return null != type
				? type.AssemblyQualifiedName
				: string.Empty;
		}
	}
}
