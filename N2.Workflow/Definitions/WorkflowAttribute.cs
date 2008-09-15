using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Definitions
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public class WorkflowAttribute: Attribute
	{
		public string WorkflowName { get; set; }
	}
}
