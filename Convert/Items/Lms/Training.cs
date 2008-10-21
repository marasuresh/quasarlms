﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace N2.Lms.Items
{
	using N2.Security.Items;
	using N2.Details;
	using N2.Integrity;
	using N2.Templates.Items;
	using N2.Web.UI;
	using N2.Collections;
	using N2.Workflow.Items;
	
	[RestrictParents(typeof(TrainingContainer))]
	[Definition, WithEditableTitle]
	[WithEditableDateRange("Validity period", 50, "StartOn", "FinishOn", ContainerName="lms")]
	[TabPanel("lms", "LMS", 200)]
	public partial class Training : AbstractContentPage
	{
		#region System properties

		public override string IconUrl { get { return "~/Lms/UI/Img/01/43.png"; } }
		public override string TemplateUrl { get { return "~/Lms/UI/Training.aspx"; } }

		public override string Title {
			get { return base.Title ?? (base.Title = this.Course.Title); }
			set { base.Title = value; }
		}

		#endregion System properties

		#region Lms Properties

		public Course Course {
			get { return Find.EnumerateParents(this).OfType<Course>().FirstOrDefault(); }
		}

		[EditableCheckBox("Test Only", 40, ContainerName="lms")]
		public bool TestOnly {
			get { return (bool?)this.GetDetail("TestOnly") ?? false; }
			set { this.SetDetail<bool>("TestOnly", value); }
		}

		public DateTime StartOn {
			get { return (DateTime?)this.GetDetail("StartOn") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("StartOn", value); }
		}

		public DateTime FinishOn {
			get { return (DateTime?)this.GetDetail("FinishOn") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("FinishOn", value); }
		}

		public IEnumerable<User> Members {
			get;
			set;
		}

		#endregion Lms Properties

		Workflow m_workflow;
		
		[EditableItem("Workflow", 44, ContainerName = "lms")]
		public Workflow Workflow {
			get {
				if (null == this.m_workflow) {
					this.m_workflow = this.GetChild("workflow") as Workflow;

					if (null == this.m_workflow) {
						this.m_workflow = this.GenerateDefaultWorkflow();
					}
				}
				return this.m_workflow;
			}
		}
	}
}
