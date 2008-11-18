<%@ Control
	Language="C#"
	AutoEventWireup="true"
	CodeBehind="Test.ascx.cs"
	ClassName="TestControl"
	Inherits="N2.Lms.UI.Parts.TestControl" %>

<%@ Reference Control="~/Lms/UI/TestQuestion.ascx" %>

<asp:MultiView runat="server" ActiveViewIndex="0" ID="mv">
	<asp:View runat="server" ID="vQuestion">
		<h1><%= this.CurrentItem.Title %></h1>
		<h3>Score: <%= this.Score %> / <%= this.CurrentItem.Points %></h3>
		<h4>Elapsed: <span id='elapsedTime'><%= this.ElapsedTimeString %></span></h4>
		<asp:Button
			runat="server"
			Text="Check All"
			Visible="false"
			ID="btnCheck"
			OnClick="btnCheck_Click" />
		<n2:Zone
				runat="server"
				ZoneName="Questions"
				OnAddedItemTemplate="zone_AddedItemTemplate"
				ID="qz">
			<HeaderTemplate>
			</HeaderTemplate>
			<SeparatorTemplate><hr /></SeparatorTemplate>
		</n2:Zone>
	</asp:View>

	<asp:View ID="vTimeExpired" runat="server">
		<div class="notification">
		Your test has expired
		</div>
	</asp:View>
</asp:MultiView>