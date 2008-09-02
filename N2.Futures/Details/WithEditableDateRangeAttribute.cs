using System;

namespace N2.Details
{
	using System.Web.UI;
	using N2.Web.UI.WebControls;
	
	/// <summary>
	/// Editable date/time range, defined by a pair of DateTime properties
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public class WithEditableDateRangeAttribute
		: WithEditablePublishedRangeAttribute
	{
		#region Constructors
		
		public WithEditableDateRangeAttribute(
			string title,
			int sortOrder,
			string name,
			string nameEndRange)
			: base(title, sortOrder)
		{
			this.Name = name;
			this.NameEndRange = nameEndRange;
		}
		
		#endregion Constructors

		#region Properties

		/// <summary>End of range detail (property) on the content item's object</summary>
		public string NameEndRange { get; set; }

		#endregion Properties

		#region Methods

		protected override Control AddEditor(Control container)
		{
			Control _editor = base.AddEditor(container);
			_editor.ID = Name + NameEndRange;
			return _editor;
		}

		public override void UpdateEditor(ContentItem item, Control editor)
		{
			DateRange range = (DateRange)editor;
			range.From = (DateTime?)item[this.Name];
			range.To = (DateTime?)item[this.NameEndRange];
		}

		public override bool UpdateItem(ContentItem item, Control editor)
		{
			DateRange range = editor as DateRange;
			if ((DateTime?)item[this.Name] != range.From || (DateTime?)item[this.NameEndRange] != range.To) {
				item[this.Name] = range.From;
				item[this.NameEndRange] = range.To;
				return true;
			}
			return false;
		}
		
		#endregion Methods
	}
}
