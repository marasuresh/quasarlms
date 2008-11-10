using System;
using System.Web.UI.WebControls;

namespace N2.Lms.Web.UI
{
	using N2.Lms.Items;
	using N2.Templates.Items;
	using N2.Templates.Web.UI;
	
	public class MyAssignmentListControl: TemplateUserControl<AbstractContentPage, MyAssignmentList>
	{
		protected void ds_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
		{
			e.ObjectInstance = this.CurrentItem ?? this.FallbackCurrentItem;
		}

		/// <summary>
		/// Fix for an obscure bug in N2 which prevents from getting CurrentItem property of control,
		/// which was instantiated not by N2 itself.
		/// </summary>
		protected MyAssignmentList FallbackCurrentItem {
			get {
				return ((TemplateUserControl<AbstractContentPage, MyAssignmentList>)this
					.NamingContainer//Wizard
					.NamingContainer//MyAssignmentList
				).CurrentItem;
			}
		}
	}
}
