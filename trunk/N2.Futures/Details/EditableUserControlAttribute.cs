using System;
using System.Web.UI;

namespace N2.Details
{
	/// <summary>
	/// Allows item's property to be edited with a User Web Control (.ascx)
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class EditableUserControlAttribute: EditableAttribute
	{
		public string UserControlPath { get; set; }
		public string CurrentItemPropertyName { set; get; }

		public EditableUserControlAttribute()
		{
		}

		/// <summary>
		/// Constructs an ionstance of the attribute
		/// </summary>
		/// <param name="title">A caption for this field in N2 Edit interface</param>
		/// <param name="userControlPath">Site-relative path to .ascx control</param>
		/// <param name="editorPropertyName">Control's property to associate with a ContentItem's field</param>
		/// <param name="currentItemPropertyName">Controls property to assign a CurrentItem to</param>
		/// <param name="sortOrder">An order to display this field in in N2 Edit interface</param>
		public EditableUserControlAttribute(
			string title,
			string userControlPath,
			string editorPropertyName,
			string currentItemPropertyName,
			int sortOrder)
			: base(title, null, editorPropertyName, sortOrder)
		{
			this.UserControlPath = userControlPath;
			this.CurrentItemPropertyName = currentItemPropertyName;
		}

		protected override Control CreateEditor(Control container)
		{
			return container.Page.LoadControl(this.UserControlPath);
		}

		public override void UpdateEditor(ContentItem item, Control editor)
		{
			if (!string.IsNullOrEmpty(this.CurrentItemPropertyName)) {
				var _ctlPropName = this.ControlPropertyName;
				//temporary change role of ControlPropertyName field to reuse SetEditorValue logic
				lock (this.ControlPropertyName) {
					this.ControlPropertyName = this.CurrentItemPropertyName;
					base.SetEditorValue(editor, item);
					this.ControlPropertyName = _ctlPropName;
				}
			}

			if (!string.IsNullOrEmpty(this.ControlPropertyName)) {
				base.UpdateEditor(item, editor);
			}
		}

		protected override object GetEditorValue(Control editor)
		{
			return
				!string.IsNullOrEmpty(this.ControlPropertyName)
					? base.GetEditorValue(editor)
					: null;
		}
	}
}
