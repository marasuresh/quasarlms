<%@ Control
		Language="C#"
		ClassName="TrainingPlayer"
		Inherits="N2.Web.UI.ContentUserControl`1[[N2.Lms.Items.TrainingWorkflow.TrainingTicket, N2.Lms]], N2" %>
<%@ Reference Control="~/Lms/UI/Test.ascx" %>
<%@ Reference Control="~/Lms/UI/Module.ascx" %>

<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		N2.Resources.Register.StyleSheet(this.Page, "~/Lms/UI/Player/Player.css");
		
		foreach (var _scheduledTopic in this.CurrentItem.Training.Workflow.Children.OfType<ScheduledTopic>()) {
			AddModuleStep(_scheduledTopic);

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
		
		this.wzModules.ActiveStepIndex = 0;
		
		base.OnInit(e);
	}

	protected WizardStepBase AddModuleStep(ScheduledTopic module)
	{
		TemplatedWizardStep _step = new TemplatedWizardStep();
		_step.ID = module.Name;
		_step.Title = module.Title;
		_step.StepType = WizardStepType.Auto;
		this.wzModules.WizardSteps.Add(_step);

		var _uc = (ASP.Module)((IContainable)module).AddTo(_step);
		_uc.CurrentItem = module;

		return _step;
	}
	
	protected WizardStepBase AddPracticeStep(Test test)
	{
		TemplatedWizardStep _step = new TemplatedWizardStep();
		_step.ID = test.Name;
		_step.Title = test.Title;
		_step.StepType = WizardStepType.Auto;
		this.wzModules.WizardSteps.Add(_step);
		
		var _uc = (ASP.TestPlayer)((IContainable)test).AddTo(_step);
		
		return _step;
	}
</script>
<asp:Wizard
		runat="server"
		ID="wzModules"
		BackColor="#EFF3FB"
		BorderColor="#B5C7DE"
		BorderWidth="1px"
		Font-Names="Verdana"
		Font-Size="0.8em"
		Height="100%">
	<StepStyle ForeColor="#333333" />
	<SideBarButtonStyle
			BackColor="#507CD1"
			Font-Names="Verdana"
			ForeColor="White" />
	<NavigationButtonStyle
			BackColor="White"
			BorderColor="#507CD1"
			BorderStyle="Solid"
			BorderWidth="1px"
			Font-Names="Verdana"
			Font-Size="0.8em"
			ForeColor="#284E98" />
	<SideBarStyle
			BackColor="#507CD1"
			Font-Size="0.9em"
			VerticalAlign="Top"
			Width="200px"
			Wrap="true" />
	<HeaderStyle
			BackColor="#284E98"
			BorderColor="#EFF3FB"
			BorderStyle="Solid"
			BorderWidth="2px"
			Font-Bold="True"
			Font-Size="0.9em"
			ForeColor="White"
			HorizontalAlign="Center" />
</asp:Wizard>
