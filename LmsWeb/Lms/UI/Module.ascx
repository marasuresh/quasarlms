<%@ Control
		Language="c#"
		ClassName="Module"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.ScheduledTopic, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="N2.Definitions" %>
<%@ Import Namespace="N2.Templates.Web.UI" %>
<%@ Import Namespace="N2.Templates.Items" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		foreach (var _topic in this.CurrentItem.FlatHierarchy) {
			TemplatedWizardStep _step = new TemplatedWizardStep();
			_step.ID = _topic.Name;
			_step.Title = _topic.Title;
			_step.StepType = WizardStepType.Auto;
			this.wzTopics.WizardSteps.Add(_step);

			var _uc = (TemplateUserControl<AbstractContentPage, Topic>)((N2.Definitions.IContainable)_topic).AddTo(_step);
			_uc.CurrentItem = _topic;

			this.wzTopics.ActiveStepIndex = 0;
		}
		
		base.OnInit(e);
	}
</script>

<asp:Wizard
		runat="server"
		ID="wzTopics"
		BackColor="#EFF3FB" 
		BorderWidth="0px" 
		Font-Names="Verdana"
		Width="100%"
		Height="100%"
		CssClass="wztr">
	<StepStyle ForeColor="#333333" />
	<SideBarButtonStyle
		ForeColor="White"
		Font-Names="Verdana" />
	<NavigationButtonStyle
		BackColor="White"
		BorderColor="#507CD1" 
		BorderStyle="Solid"
		BorderWidth="1px"
		Font-Names="Verdana"
		ForeColor="#284E98" />
	<SideBarStyle
		BackColor="Beige"
		VerticalAlign="Top"
		Font-Size="0.8em"
		Width="150px"
		Wrap="true" />
	<HeaderStyle
		BackColor="#284E98"
		BorderStyle="Solid"
		Font-Bold="True" 
		ForeColor="White"
		HorizontalAlign="Center" 
		BorderColor="#EFF3FB"
		BorderWidth="2px" />
	
	<SideBarTemplate>
		<asp:DataList
				runat="server"
				ID="SideBarList"
				RepeatLayout="Flow"
				CssClass="sbl"
				SelectedItemStyle-Font-Italic="true">
			<ItemTemplate>
				<asp:LinkButton
					runat="server"
					ID="SideBarButton"
					CssClass="sba" />
			</ItemTemplate>
		</asp:DataList>
		
	</SideBarTemplate>
</asp:Wizard>

