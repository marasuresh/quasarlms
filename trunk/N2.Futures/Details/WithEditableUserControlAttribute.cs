using System;

namespace N2.Details
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class WithEditableUserControlAttribute: EditableUserControlAttribute
	{
		public WithEditableUserControlAttribute(
			string title,
			string userControlPath,
			string editorPropertyName,
			string currentItemPropertyName,
			int sortOrder,
			string name)
			: base(title, userControlPath, editorPropertyName, currentItemPropertyName, sortOrder)
		{
			this.Name = name;
		}
	}
}
