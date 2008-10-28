<%@ Control
		Language="C#"
		ClassName="TrainingPlayer"
		Inherits="N2.Web.UI.ContentUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.TrainingWorkflow.TrainingTicket, N2.Lms]], N2" %>
<%@ Reference Control="~/Lms/UI/Test.ascx" %>
<%@ Reference Control="~/Lms/UI/Module.ascx" %>

<script runat="server">
	protected override void CreateChildControls()
	{
		base.CreateChildControls();
		this.CreateControlHierarchy();
		this.ClearChildViewState();
	}

	protected virtual void CreateControlHierarchy()
	{
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
	}
	
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		N2.Resources.Register.StyleSheet(this.Page, "~/Lms/UI/Player/Player.css");
		this.EnsureChildControls();
		this.wzModules.ActiveStepIndex = 0;
		
		Register.JQuery(this.Page);
		
		this.Page.ClientScript.RegisterClientScriptInclude(
				"jQuery.splitter",
				this.Page.ResolveClientUrl("~/Edit/Js/plugins/jquery.splitter.js")
		);

		Register.JavaScript(this.Page, this.Page.ResolveClientUrl("~/Lms/UI/Js/Player.js"));
	}

	protected WizardStepBase AddModuleStep(ScheduledTopic module)
	{
		WizardStep _step = new WizardStep {
			ID = module.Name,
			Title = "<img src='" + this.Page.ResolveClientUrl(module.IconUrl) + "' /> " + module.Title,
			StepType = WizardStepType.Auto
		};
		this.wzModules.WizardSteps.Add(_step);

		((N2.Templates.Web.UI.TemplateUserControl<N2.Templates.Items.AbstractContentPage, N2.Lms.Items.ScheduledTopic>)((IContainable)module).AddTo(_step)).CurrentItem = module;

		return _step;
	}
	
	protected WizardStepBase AddPracticeStep(Test test)
	{
		WizardStep _step = new WizardStep {
			ID = test.Name,
			Title = test.Title,
			StepType = WizardStepType.Auto,
		};
		
		this.wzModules.WizardSteps.Add(_step);
		
		((ASP.TestPlayer)((IContainable)test).AddTo(_step)).CurrentItem = test;
		
		return _step;
	}
</script>
<asp:Wizard
		runat="server"
		ID="wzModules"
		BackColor="#EFF3FB"
		BorderColor="#B5C7DE"
		BorderStyle="None"
		Font-Names="Verdana"
		CssClass="wzm">
	<NavigationStyle Height="0" CssClass="hidden" />
	<StepStyle ForeColor="#333333" VerticalAlign="Top" />
	<SideBarButtonStyle
			CssClass="sbLink" />
	<NavigationButtonStyle
			BackColor="White"
			BorderColor="#507CD1"
			BorderStyle="Solid"
			BorderWidth="1px"
			Font-Names="Verdana"
			Font-Size="0.8em"
			ForeColor="#284E98" />
	<SideBarStyle
			VerticalAlign="Top"
			CssClass="sb"
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
	<SideBarTemplate>
		<asp:DataList
				runat="server"
				ID="SideBarList"
				CssClass="sbl"
				SelectedItemStyle-CssClass="selected"
				SelectedItemStyle-Font-Bold="true">
			<ItemTemplate>
				<asp:LinkButton
					runat="server"
					ID="SideBarButton"
					CssClass="sba" />
			</ItemTemplate>
		</asp:DataList>
		
	</SideBarTemplate>
</asp:Wizard>
