using System.Diagnostics;
using System;
using System.Linq;
using N2.Web.UI;
using System.Web.UI.WebControls;
using N2.Web.UI.WebControls;
using N2.Workflow;
using N2.Workflow.Items;
using N2.Resources;
using N2;
using System.Web.UI;

public partial class WorkflowAction: UserControl, IWorkflowActionControl
{


	protected override void OnInit(System.EventArgs e)
	{
		base.OnInit(e);
		this.BindControls();
	}

	void BindControls()
	{
		if (null != ((IWorkflowActionControl)this).CurrentItem) {
			this.rptActionList.DataSource = this.InitialState.ToState.Actions;
			this.rptActionList.DataBind();

			if (vEditState.Visible) {
				this.ie.CurrentItem = this.CurrentState;
				this.ie.DataBind();
			}
		}
	}

	protected void Action_Command(object sender, RepeaterCommandEventArgs e)
	{
		ActionDefinition _selectedAction = this.InitialState
			.ToState
			.Actions
			.Single(_a => _a.Name == (string)e.CommandArgument);

		ItemState _newState = (ItemState)N2.Context.Definitions.CreateInstance(
			_selectedAction.StateType
				?? typeof(ItemState),
			((IWorkflowActionControl)this).CurrentItem);

		_newState.ToState = _selectedAction.LeadsTo;
		
		//not required
		_newState.Action = _selectedAction;
		_newState.FromState = this.InitialState.ToState;
		
		_newState.AddTo(((IWorkflowActionControl)this).CurrentItem);

		((IWorkflowActionControl)this).CurrentItem.AssignCurrentState(_newState);

		this.mv.SetActiveView(vEditState);
		this.BindControls();
		//this.ie.DataBind();
	}

	protected void Cancel_Click(object sender, EventArgs e)
	{
		//Restore original state
		if (this.CurrentState != this.InitialState) {
			((IWorkflowActionControl)this).CurrentItem.Children.Remove(this.CurrentState);
			((IWorkflowActionControl)this).CurrentItem.AssignCurrentState(this.InitialState);
		}
		
		this.mv.SetActiveView(vActionList);
	}

	#region IWorkflowActionControl Members

	ContentItem m_ci;
	ContentItem IWorkflowActionControl.CurrentItem {
		get { return this.m_ci; }
		set { this.m_ci = value; this.BindControls(); }
	}

	#endregion
}
