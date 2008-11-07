<%@ Control
		Language="C#"
		ClassName="TrainingPlayer"
		Inherits="Player"
		CodeBehind="Player.ascx.cs" %>
<%--<span class="player">
<asp:Wizard
		runat="server"
		ID="wz">
	<SideBarTemplate>
		<asp:DataList
				runat="server"
				ID="SideBarList">
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
</span>--%>