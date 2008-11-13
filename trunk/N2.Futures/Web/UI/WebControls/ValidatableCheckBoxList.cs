using System.Linq;

namespace System.Web.UI.WebControls
{
	[ValidationProperty("CheckedMask")]
	public class ValidatableCheckBoxList : CheckBoxList
	{
		public string CheckedMask {
			get {
				return new string((
					from _item in this.Items.Cast<ListItem>()
					select _item.Selected ? '1' : '0'
				).ToArray());
			}
		}
	}
}