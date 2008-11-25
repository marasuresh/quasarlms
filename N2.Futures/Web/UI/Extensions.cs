namespace System.Web.UI
{
	using WebControls;
	
	public static class Extensions
	{
		public static bool IsSelected(this WizardStep step)
		{
			return step == step.Wizard.ActiveStep;
		}

        public static void WriteJScript(this HtmlTextWriter writer, string script)
        {
            writer.Write(@"<script type='text/javascript'>" + script + "</script>");
        }
	}
}
