using System;
using System.Diagnostics;
using System.Linq;
using System.Web.UI.WebControls;
using N2.Definitions;
using N2.Lms.Items;
using N2.Lms.Items.TrainingWorkflow;
using N2.Resources;
using N2.Templates.Items;
using N2.Templates.Web.UI;
using N2.Web.UI;

public partial class Player : ContentUserControl<AbstractContentPage, TrainingTicket>
{
	protected Wizard wz;

	int Indentation { get; set; }

	protected override void CreateChildControls()
	{
		base.CreateChildControls();
		this.CreateControlHierarchy();
		this.ClearChildViewState();
	}

	protected virtual void CreateControlHierarchy()
	{
		foreach (var _scheduledTopic in this.CurrentItem.Training.Modules) {
			AddTopicStep(_scheduledTopic);

			if (_scheduledTopic.HasTest) {
				this.AddPracticeStep(_scheduledTopic.Topic.Practice);
			}

		}

		//TODO: take test from training schedule rather then from course definition
		var _test = this.CurrentItem.Training.Course.Test;
		if (null != _test) {
			Debug.WriteLine(_test.Title, "Lms");
			this.AddPracticeStep(_test).StepType = WizardStepType.Finish;
		}
	}

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Register.StyleSheet(this.Page, "~/Lms/UI/Player/Player.css");
		Register.StyleSheet(this.Page, "~/Lms/UI/Js/jquery.treeview.css");
		this.EnsureChildControls();
		this.wz.ActiveStepIndex = 0;

		Register.JQuery(this.Page);
		Register.JavaScript(this.Page, "~/Lms/UI/Js/jQuery.treeview.js");

		this.Page.ClientScript.RegisterClientScriptInclude(
				"jQuery.splitter",
				this.Page.ResolveClientUrl("~/Edit/Js/plugins/jquery.splitter.js")
		);

		Register.JavaScript(this.Page, this.Page.ResolveClientUrl("~/Lms/UI/Js/Player.js"));
	}

	protected WizardStepBase AddTopicStep(ScheduledTopic module)
	{
		WizardStep _step = new WizardStep {
			ID = module.Name,
			Title = module.Title,
			StepType = WizardStepType.Auto,
			SkinID = "BeginTopic",
		};
		this.wz.WizardSteps.Add(_step);

		(
			(TemplateUserControl<AbstractContentPage, Topic>)((IContainable)module.Topic).AddTo(_step)
		).CurrentItem = module.Topic;
		
		this.CreateControlHierarchy(module);
		return _step;
	}

	protected WizardStepBase AddPracticeStep(Test test)
	{
		WizardStep _step = new WizardStep {
			ID = test.Name,
			Title = test.Title,
			StepType = WizardStepType.Auto,
			SkinID = "BeginTopic",
		};

		this.wz.WizardSteps.Add(_step);
		this.CreateControlHierarchy(test);
		return _step;
	}

	void CreateControlHierarchy(Test test)
	{
		var _questions = test.Questions.ToList();

		if (_questions.Any()) {
			if (1 == _questions.Count()) {
				_questions[0]["tag"] = "SingleChild";
			} else {
				_questions.Last()["tag"] = "LastChild";
				_questions.First()["tag"] = "FirstChild";
			}

			foreach (var _q in _questions) {

				var _step = new WizardStep {
					ID = _q.Name,
					StepType = WizardStepType.Auto,
					Title = _q.Title,
					SkinID = (string)_q["tag"],//inject begin sub-topic mark on the first/last step
				};

				this.wz.WizardSteps.Add(_step);
				((TemplateUserControl<AbstractContentPage, TestQuestion>)((IContainable)_q).AddTo(_step))
					.CurrentItem = _q;
			}
		}
	}
	
	void CreateControlHierarchy(Topic topic)
	{
		var _topics = topic.Topics.ToList();

		if (_topics.Any()) {

			if (1 == _topics.Count()) {
				_topics[0]["tag"] = "SingleChild";
			} else {
				_topics.Last()["tag"] = "LastChild";
				_topics.First()["tag"] = "FirstChild";
			}

			foreach (var _topic in _topics) {
				WizardStep _step = new WizardStep {
					ID = _topic.Name,
					Title = _topic.Title,
					StepType = WizardStepType.Auto,
					SkinID = (string)_topic["tag"],
				};

				this.wz.WizardSteps.Add(_step);

				((TemplateUserControl<AbstractContentPage, Topic>)((IContainable)_topic)
					.AddTo(_step))
					.CurrentItem = _topic;

				this.CreateControlHierarchy(_topic);
			}
		}
	}

	void CreateControlHierarchy(ScheduledTopic module)
	{
		this.CreateControlHierarchy(module.Topic);
	}

	#region GUI methods

	protected string RenderEndHtml(WizardStep step)
	{
		return
			step.SkinID == "LastChild" || step.SkinID == "SingleChild"
				? "</li></ul></li>"
				: step.SkinID == "BeginTopic" ? string.Empty : "</li>";
	}

	protected string RenderBeginHtml(WizardStep step)
	{
		if (step.SkinID == "FirstChild" || step.SkinID == "SingleChild") {
			this.Indentation++;
			return "<ul><li>";
		} else {
			return "<li>";
		}
	}

	#endregion GUI methods
}
