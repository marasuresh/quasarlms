using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Workflow.Items
{
	public interface IWorkflowItemContainer
	{
		Workflow Workflow { get; }
	}
}
