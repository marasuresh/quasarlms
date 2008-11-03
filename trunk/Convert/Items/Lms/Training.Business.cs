﻿using System.Linq;
using System.Collections.Generic;

namespace N2.Lms.Items
{
	using N2.Details;
	using N2.Workflow.Items;
	
	partial class Training
	{
		Workflow m_workflow;
		
		[EditableItem("Workflow", 44, ContainerName = "lms")]
		internal Workflow Workflow {
			get {
				if (null == this.m_workflow) {
					this.m_workflow = this.GetChild("workflow") as Workflow;

					if (null == this.m_workflow) {
						this.m_workflow = this.GenerateDefaultWorkflow();
						Context.Current.Persister.Save(this.m_workflow);
					}
				}
				return this.m_workflow;
			}
		}

		public IEnumerable<ScheduledTopic> Modules { get {
			return this.Workflow.Children.OfType<ScheduledTopic>();
		} }
	}
}
