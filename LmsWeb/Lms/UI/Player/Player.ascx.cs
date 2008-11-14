using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using N2.Definitions;
using N2.Lms.Items;
using N2.Lms.Items.TrainingWorkflow;
using N2.Resources;
using N2.Templates.Items;
using N2.Templates.Web.UI;
using N2.Web.UI;

/// <summary>
/// <remarks>SkinID is used to tagging nodes to reconstruct their hierarchy in HTML from a plain wizard step collection.
/// f(irst), l(ast), m(iddle), s(ingle) = f && l,
/// capitalized means "with children", lowercase -- otherwise
/// </remarks>
/// </summary>
public partial class Player : ContentUserControl<AbstractContentPage, TrainingTicket>
{
	protected Wizard wz;
	
	WizardStepDecorator Decorator;
	
	protected override void CreateChildControls()
	{
		base.CreateChildControls();
		this.Decorator = new WizardStepDecorator();
		this.CreateControlHierarchy();
		this.ClearChildViewState();
	}

	void CreateWizard()
	{
		this.wz = new Wizard {
			ID = "wz",
			SideBarTemplate = new GenericTemplate(_container => {
				
				DataList _dl = new DataList {
					ID = "SideBarList",
					HeaderTemplate = new GenericTemplate(_header => {
						_header.Controls.Add(new LiteralControl("<ul id='moduleTree' class='coursetree'>"));
					}),
					FooterTemplate = new GenericTemplate(_footer => {
						_footer.Controls.Add(new LiteralControl("</ul>"));
					}),
					ItemTemplate = new GenericTemplate(_item => {
						var _beginLiteral = new DataBoundLiteralControl(1, 1);
						_beginLiteral.DataBinding += (sender, e) => {
							var _target = (DataBoundLiteralControl)sender;
							var _step = (WizardStep)(
											(DataListItem)(
												(Control)sender
											).BindingContainer
										).DataItem;
							_target.SetDataBoundString(0, this.Decorator.RenderBeginHtml(_step));
						};
						
						var _spanLiteral = new DataBoundLiteralControl(2, 2);
						_spanLiteral.SetStaticString(0, "<span class='");
						_spanLiteral.SetStaticString(1, "'>");

						_spanLiteral.DataBinding += (sender, e) => {
							var _target = (DataBoundLiteralControl)sender;
							var _step = (WizardStep)(
											(DataListItem)(
												(Control)sender
											).BindingContainer
										).DataItem;
							
							string _className = this.Decorator.GetClassName(_step);
							
							if (_step.IsSelected()) {
								_className += " selected";
							}

							_target.SetDataBoundString(0, _className);
						};

						var _endLiteral = new DataBoundLiteralControl(1, 1);

						_endLiteral.DataBinding += (sender, e) => {
							var _target = (DataBoundLiteralControl)sender;
							var _step = (WizardStep)(
											(DataListItem)(
												(Control)sender
											).BindingContainer
										).DataItem;
							_target.SetDataBoundString(0, this.Decorator.RenderEndHtml(_step));
						};

						_item.Controls.Add(_beginLiteral);
						_item.Controls.Add(_spanLiteral);
						_item.Controls.Add(new LinkButton {
							ID = "SideBarButton", CssClass = "sba",
						});
						_item.Controls.Add(new LiteralControl("</span>"));
						_item.Controls.Add(_endLiteral);
					}),
				};

				_container.Controls.Add(_dl);
			}),
		};
		
		this.Controls.Add(new LiteralControl("<span class='player'>"));
		this.Controls.Add(this.wz);
		this.Controls.Add(new LiteralControl("</span>"));
	}

	protected virtual void CreateControlHierarchy()
	{
		this.CreateWizard();

		this.Page.Title = this.CurrentItem.Training.Title;

		var _items = this.CurrentItem.Training.Modules.Cast<N2.ContentItem>().ToList();
		
		//TODO: take test from training schedule rather then from course definition
		var _test = this.CurrentItem.Training.Course.Test;
		if (null != _test) {
			_items.Add(_test);
		}

		var _classifier = this.Decorator.GetItemClassifier<N2.ContentItem>(_items);

		foreach (var _item in _items) {
			if (_item is ScheduledTopic) {
				this.AddModuleStep(_item as ScheduledTopic, _classifier(_item));
			} else if(_item is Test) {
				this.AddPracticeStep(_item as Test, _classifier(_item));//.StepType = WizardStepType.Auto;
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
		Register.JavaScript(this.Page, this.Page.ResolveClientUrl("~/Lms/UI/Js/Player.js"));
	}

	protected WizardStepBase AddModuleStep(ScheduledTopic module, string tag)
	{
		WizardStep _step = new WizardStep {
			ID = module.Topic.ID.ToString(),
			Title = module.Title,
			StepType = WizardStepType.Auto,
			SkinID = module.Topic.Topics.Any() || null != module.Topic.Practice
				? tag.ToUpper()
				: tag,
		};
		this.Decorator.Items.Add(_step.ID, module);
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
			ID = test.ID.ToString(),
			Title = test.Title,
			StepType = WizardStepType.Auto,
			SkinID = test.Questions.Any() && test.DisplayMultiplePages
				? tag.ToUpper()
				: tag,
		};
		
		this.Decorator.Items.Add(_step.ID, test);
		this.wz.WizardSteps.Add(_step);

		(
			(TemplateUserControl<AbstractContentPage, Test>)((IContainable)test).AddTo(_step)
		).CurrentItem = test;

		if (test.DisplayMultiplePages) {
			this.CreateControlHierarchy(test);
		}
		
		return _step;
	}

	void CreateControlHierarchy(Test test)
	{
		var _questions = test.Questions.ToList();

		if (_questions.Any()) {
			var _classifier = Decorator.GetItemClassifier<TestQuestion>(test.Questions);

			foreach (var _q in _questions) {

				var _step = new WizardStep {
					ID = _q.ID.ToString(),
					StepType = WizardStepType.Auto,
					Title = _q.Title,
					SkinID = _classifier(_q),//inject begin sub-topic mark on the first/last step
				};

				this.Decorator.Items.Add(_step.ID, _q);
				this.wz.WizardSteps.Add(_step);
				((TemplateUserControl<AbstractContentPage, TestQuestion>)((IContainable)_q).AddTo(_step))
					.CurrentItem = _q;
			}
		}
	}

	protected WizardStepBase AddTopicStep(Topic topic, string tag)
	{
		WizardStep _step = new WizardStep {
			ID = topic.ID.ToString(),
			Title = topic.Title,
			StepType = WizardStepType.Auto,
			SkinID = topic.Topics.Any() || null != topic.Practice ? tag.ToUpper() : tag,
		};
		this.Decorator.Items.Add(_step.ID, topic);
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
			var _classifier = this.Decorator.GetItemClassifier<N2.ContentItem>(_topics);

			foreach (var _topic in _topics) {
				if (_topic is Topic) {
					this.AddTopicStep(_topic as Topic, _classifier(_topic));
				} else if (_topic is Test) {
					this.AddPracticeStep(_topic as Test, _classifier(_topic));
				}
			}
		}
	}

	class WizardStepDecorator
	{
		Stack<int> MenuLevel = new Stack<int>();

		public IDictionary<string, N2.ContentItem> Items = new Dictionary<string, N2.ContentItem>();
		
		public WizardStepDecorator()
		{
			this.MenuLevel.Push(0);
		}

		public Func<T, string> GetItemClassifier<T>(IEnumerable<T> sequence)
		{
			if (!sequence.Any()) {
				return item => string.Empty;
			}

			var _first = sequence.First();
			var _last = sequence.Last();

			return object.ReferenceEquals(_first, _last)
				? new Func<T, string>(item => "s")
				: new Func<T, string>(item => object.ReferenceEquals(item, _first)
					? "f"
					: object.ReferenceEquals(item, _last)
						? "l"
						: "m");
		}

		#region GUI methods

		public string GetClassName(WizardStep step)
		{
			return
				this.Items.ContainsKey(step.ID)
				? this.Items[step.ID].GetType().Name.ToLower()
				: "file";
		}

		public string RenderEndHtml(WizardStep step)
		{
			string _result = "</li>";

			int _indent = this.MenuLevel.Count > 0 ? this.MenuLevel.Peek() : -1;

			if (new[] { "F", "S", "M", "L" }.Contains(step.SkinID)) {
				_result = "<ul>";
			} else if (step.SkinID == "m" || step.SkinID == "f") {
				_result = "</li>";
			} else if (step.SkinID == "s" || step.SkinID == "l") {
				_result = string.Join(string.Empty, Enumerable.Repeat(
					"</li></ul></li>",
					_indent >= 0 ? this.MenuLevel.Pop() : 0).ToArray());
			}
			_result += "<!--" + _indent.ToString() + "-->";
			return _result;
		}

		public string RenderBeginHtml(WizardStep step)
		{
			if (new[] { "F", "S", "M" }.Contains(step.SkinID)) {
				this.MenuLevel.Push(1);
			} else if (/*step.SkinID == "f" || step.SkinID == "s" ||*/ step.SkinID == "L") {
				if (this.MenuLevel.Count > 0) {
					var i = this.MenuLevel.Pop();
					this.MenuLevel.Push(++i);
				}
			}

			return "<li><!--" + step.SkinID + "-->";
		}

		#endregion GUI methods
	}
}
