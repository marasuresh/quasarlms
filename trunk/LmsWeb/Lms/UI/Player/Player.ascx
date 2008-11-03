<%@ Control
		Language="C#"
		ClassName="TrainingPlayer"
		Inherits="Player"
		CodeBehind="Player.ascx.cs" %>

<asp:Wizard
		runat="server"
		ID="wz"
		BackColor="#EFF3FB"
		BorderColor="#B5C7DE"
		BorderStyle="None"
		Font-Names="Verdana"
		CssClass="wzm">
	<NavigationStyle Height="0" />
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
				SelectedItemStyle-Font-Bold="true"
				RepeatLayout="Flow"
				RepeatDirection="Horizontal">
			<HeaderTemplate></span><ul id="moduleTree" class="filetree"><span></HeaderTemplate>
			<FooterTemplate></span></ul><span></FooterTemplate>
			<ItemTemplate></span>
				<%# this.RenderBeginHtml((WizardStep)Container.DataItem) %>
				<span class="file"><asp:LinkButton
					runat="server"
					ID="SideBarButton"
					CssClass='sba' />
				</span>
				<%# this.RenderEndHtml((WizardStep)Container.DataItem) %>
			<span></ItemTemplate>
		</asp:DataList>
		
	</SideBarTemplate>
</asp:Wizard>
