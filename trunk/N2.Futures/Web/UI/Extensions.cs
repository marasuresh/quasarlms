namespace System.Web.UI
{
	using WebControls;
	
	public static class Extensions
	{
		public static bool IsSelected(this WizardStep step)
		{
			return step == step.Wizard.ActiveStep;
		}
	}
}
