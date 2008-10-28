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
			WizardStep _step = new WizardStep {
				ID = _topic.Name,
				Title = _topic.Title,
				StepType = WizardStepType.Auto,
			};
			
			this.wzTopics.WizardSteps.Add(_step);

			((TemplateUserControl<AbstractContentPage, Topic>)((N2.Definitions.IContainable)_topic)
				.AddTo(_step))
				.CurrentItem = _topic;

			this.wzTopics.ActiveStepIndex = 0;
		}
		
		base.OnInit(e);
	}
</script>
<asp:Wizard
		runat="server"
		ID="wzTopics"
		BorderWidth="0px" 
		Font-Names="Verdana"
		Width="100%"
		Height="100%"
		CssClass="wztr">
	<StepStyle BackColor="White" />
	<NavigationStyle Height="0" CssClass="hidden" />
	
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
		CssClass="sb"
		Wrap="true" />

	<SideBarTemplate>
		<asp:DataList
				runat="server"
				ID="SideBarList"
				RepeatLayout="Flow"
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