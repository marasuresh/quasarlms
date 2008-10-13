<%@ Control
		Language="c#"
		ClassName="TestPlayer"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.Test, N2.Lms]], N2.Templates" %>
<script runat="server">
	
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		this.EnsureChildControls();
		this.wzTest.ActiveStepIndex = 0;
	}

	protected virtual void CreateControlHierarchy()
	{
		foreach(var _q in this.CurrentItem.Questions) {
			var _step = new WizardStep {
				ID = _q.Name,
				StepType = WizardStepType.Auto,
				Title = _q.Title
			};
			
			this.wzTest.WizardSteps.Add(_step);
			((TemplateUserControl<AbstractContentPage, TestQuestion>)((IContainable)_q).AddTo(_step))
				.CurrentItem = _q;
		}
	}
	
	protected override void CreateChildControls()
	{
		base.CreateChildControls();//delegate declarative Wizard creation to base
		this.CreateControlHierarchy();//append my steps to wizard
		this.ClearChildViewState();
	}
</script>

<asp:Wizard runat="server" ID="wzTest" BackColor="#F7F6F3" 
	BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
	Font-Names="Verdana" Font-Size="0.8em" Width="100%">
	<StepStyle BorderWidth="0px" ForeColor="#5D7B9D" />
	<SideBarButtonStyle
		BorderWidth="0px"
		Font-Names="Verdana"
		ForeColor="White" />
	<NavigationButtonStyle
		BackColor="#FFFBFF"
		BorderColor="#CCCCCC" 
		BorderStyle="Solid"
		BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
		ForeColor="#284775" />
	<SideBarStyle
		BackColor="#7C6F57"
		BorderWidth="0px"
		Font-Size="0.9em" 
		VerticalAlign="Top"
		Width="150px"
		Wrap="true" />
	<HeaderStyle
		BackColor="#5D7B9D"
		BorderStyle="Solid"
		Font-Bold="True" 
		Font-Size="0.9em"
		ForeColor="White"
		HorizontalAlign="Left" />
</asp:Wizard>

