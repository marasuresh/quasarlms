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
using System.Collections.Generic;

public partial class Player : ContentUserControl<AbstractContentPage, TrainingTicket>
{
	protected Wizard wz;

	Stack<int> Indent = new Stack<int>();

	protected override void CreateChildControls()
	{
		base.CreateChildControls();
		this.CreateControlHierarchy();
		this.ClearChildViewState();
	}

	Func<T, string> GetItemClassifier<T>(IEnumerable<T> sequence)
	{
		if (!sequence.Any()) {
			return item => string.Empty;
		}

		var _first = sequence.First();
		var _last = sequence.Last();

		return object.ReferenceEquals(_first, _last)
			? new Func<T, string>(item => "S")
			: new Func<T, string>(item => object.ReferenceEquals(item, _first)
				? "F"
				: object.ReferenceEquals(item, _last)
					? "L"
					: "M");
	}

	protected virtual void CreateControlHierarchy()
	{
		this.Indent.Push(0);
		
		var _items = this.CurrentItem.Training.Modules.Cast<N2.ContentItem>().ToList();
		
		//TODO: take test from training schedule rather then from course definition
		var _test = this.CurrentItem.Training.Course.Test;
		if (null != _test) {
			_items.Add(_test);
		}

		var _classifier = this.GetItemClassifier<N2.ContentItem>(_items);

		foreach (var _item in _items) {
			if (_item is ScheduledTopic) {
				this.AddModuleStep(_item as ScheduledTopic, _classifier(_item));
			} else if(_item is Test) {
				this.AddPracticeStep(_item as Test, _classifier(_item)).StepType = WizardStepType.Finish;
			}

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

	protected WizardStepBase AddModuleStep(ScheduledTopic module, string tag)
	{
		WizardStep _step = new WizardStep {
			ID = module.Name,
			Title = module.Title,
			StepType = WizardStepType.Auto,
			SkinID = module.Topic.Topics.Any() ? tag + "c" : tag,
		};
		this.wz.WizardSteps.Add(_step);

		(
			(TemplateUserControl<AbstractContentPage, Topic>)((IContainable)module.Topic).AddTo(_step)
		).CurrentItem = module.Topic;
		
		this.CreateControlHierarchy(module.Topic);
		return _step;
	}

	protected WizardStepBase AddPracticeStep(Test test, string tag)
	{
		WizardStep _step = new WizardStep {
			ID = test.Name,
			Title = test.Title,
			StepType = WizardStepType.Auto,
			SkinID = test.Questions.Any() ? tag + "c" : tag,
		};

		this.wz.WizardSteps.Add(_step);
		this.CreateControlHierarchy(test);
		return _step;
	}

	void CreateControlHierarchy(Test test)
	{
		var _questions = test.Questions.ToList();

		if (_questions.Any()) {
			var _classifier = this.GetItemClassifier<TestQuestion>(test.Questions);

			foreach (var _q in _questions) {

				var _step = new WizardStep {
					ID = _q.Name,
					StepType = WizardStepType.Auto,
					Title = _q.Title,
					SkinID = _classifier(_q),//inject begin sub-topic mark on the first/last step
				};

				this.wz.WizardSteps.Add(_step);
				((TemplateUserControl<AbstractContentPage, TestQuestion>)((IContainable)_q).AddTo(_step))
					.CurrentItem = _q;
			}
		}
	}

	protected WizardStepBase AddTopicStep(Topic topic, string tag)
	{
		WizardStep _step = new WizardStep {
			ID = topic.Name,
			Title = topic.Title,
			StepType = WizardStepType.Auto,
			SkinID = topic.Topics.Any() ? tag + "c" : tag,
		};

		this.wz.WizardSteps.Add(_step);

		((TemplateUserControl<AbstractContentPage, Topic>)((IContainable)topic)
			.AddTo(_step))
			.CurrentItem = topic;

		this.CreateControlHierarchy(topic);

		return _step;
	}

	void CreateControlHierarchy(Topic topic)
	{
		var _topics = topic.Topics.Cast<N2.ContentItem>().ToList();

		if (null != topic.Practice) {
			_topics.Add(topic.Practice);
		}

		if (_topics.Any()) {
			var _classifier = this.GetItemClassifier<N2.ContentItem>(_topics);

			foreach (var _topic in _topics) {
				if (_topic is Topic) {
					this.AddTopicStep(_topic as Topic, _classifier(_topic));
				} else if (_topic is Test) {
					this.AddPracticeStep(_topic as Test, _classifier(_topic));
				}
			}
		}
	}

	#region GUI methods

	protected string RenderEndHtml(WizardStep step)
	{
		string _result = "</li>";

		int _indent = this.Indent.Count > 0 ? this.Indent.Peek() : -1;
			
		if (step.SkinID.Length == 2) {
			_result = "<ul>";
		} else if (step.SkinID == "M" || step.SkinID == "F") {
			_result = "</li>";
		} else if (step.SkinID == "S" || step.SkinID == "L") {
			_result = string.Join(string.Empty, Enumerable.Repeat(
				"</li></ul></li>",
				_indent >= 0 ? this.Indent.Pop() : 0).ToArray());
		}
		_result += "<!--" + _indent.ToString() + "-->";
		return _result;
	}

	protected string RenderBeginHtml(WizardStep step)
	{
		if (step.SkinID == "Fc" || step.SkinID == "Sc") {
			this.Indent.Push(1);
		} else if (/*step.SkinID == "F" || step.SkinID == "S" ||*/ step.SkinID == "Lc") {
			if (this.Indent.Count > 0) {
				var i = this.Indent.Pop();
				this.Indent.Push(++i);
			}
		} else if(step.SkinID == "Mc") {
			this.Indent.Push(1);
		}
		
		return "<li><!--" + step.SkinID + "-->";
	}

	#endregion GUI methods

	//TODO move to common library
	public static bool IsSelected(WizardStep step)
	{
		return step == step.Wizard.ActiveStep;
	}
}
