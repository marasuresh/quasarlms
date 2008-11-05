<%@ Control
		Language="C#"
		ClassName="TrainingPlayer"
		Inherits="Player"
		CodeBehind="Player.ascx.cs" %>
<span class="player">
<asp:Wizard
		runat="server"
		ID="wz">
	<NavigationStyle Height="0" />
	<NavigationButtonStyle
			BackColor="White"
			BorderColor="#507CD1"
			BorderStyle="Solid"
			BorderWidth="1px"
			Font-Names="Verdana"
			Font-Size="0.8em"
			ForeColor="#284E98" />
	<SideBarTemplate>
		<asp:DataList
				runat="server"
				ID="SideBarList"
				RepeatLayout="Flow"
				RepeatDirection="Horizontal">
			<HeaderTemplate><ul id="moduleTree" class="filetree"></HeaderTemplate>
			<FooterTemplate></ul></FooterTemplate>
			<ItemTemplate>
				<%# this.RenderBeginHtml((WizardStep)Container.DataItem) %>
				<span class='file<%# IsSelected((WizardStep)Container.DataItem) ? " selected" : string.Empty %>'><asp:LinkButton
					runat="server"
					ID="SideBarButton"
					CssClass='sba' />
				</span>
				<%# this.RenderEndHtml((WizardStep)Container.DataItem) %>
			</ItemTemplate>
		</asp:DataList>
		
	</SideBarTemplate>
</asp:Wizard>
</span>