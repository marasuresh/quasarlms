namespace N2.Web.UI.WebControls
{
	using N2.Workflow.Items;
	
	public interface IWorkflowActionControl
	{
		ContentItem CurrentItem { get; set; }
	}
}
