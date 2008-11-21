using System.Linq;

namespace System.Web.UI.WebControls
{
	[ValidationProperty("CheckedMask")]
	public class ValidatableCheckBoxList : CheckBoxList
	{
		const char SELECTED = '1';
		const char UNSELECTED = '0';

		public string CheckedMask {
			get {
				return
					-1 == this.SelectedIndex
						? null //..instead of a string with all zeroes
						: new string((
							from _item in this.Items.Cast<ListItem>()
							select _item.Selected ? SELECTED : UNSELECTED
						).ToArray());
			}
			set {
				//reset check boxes state
				if(string.IsNullOrEmpty(value)) {
					foreach(var _item in this.Items.Cast<ListItem>()) {
						_item.Selected = false;
					}
					return;
				}

				if(value.Length != this.Items.Count) {
					throw new ArgumentException(
						"Checked items mask does not correspond to item count",
						"CheckedMask");
				}

				for (var i = 0; i < value.Length; i++) {
					this.Items[i].Selected = value[i] == SELECTED;
				}
			}
		}
	}
}