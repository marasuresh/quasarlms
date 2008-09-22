namespace N2.Workflow.Items
{
	/// <summary>
	/// Content item, implementing this interface
	/// can contain worflow-driven items.
	/// The major function of this interface is to provide a reference
	/// to workflow definition for underlying children.
	/// </summary>
	public interface IWorkflowItemContainer
	{
		/// <summary>
		/// Reference to workflow definition.
		/// It may be handy to decorate implementation with the 
		/// [N2.Details.EditableLink] attribute.
		/// </summary>
		Workflow Workflow { get; }
	}
}
