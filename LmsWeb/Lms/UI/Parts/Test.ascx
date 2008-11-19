<%@ Control
	Language="C#"
	AutoEventWireup="true"
	CodeBehind="Test.ascx.cs"
	ClassName="TestControl"
	Inherits="N2.Lms.UI.Parts.TestControl" %>

<%@ Reference Control="~/Lms/UI/TestQuestion.ascx" %>

<asp:PlaceHolder runat="server" ID="phExpired" Visible="false">
	<div class="notification">Time has expired</div>
</asp:PlaceHolder>

<asp:Panel runat="server" ID="phQuestions">
	<h1><%= this.CurrentItem.Title %></h1>
	<h3>Score: <%= this.Score %> / <%= this.CurrentItem.Points %></h3>
	<h4>Elapsed: <span id='elapsedTime'<%= this.StartedOn.HasValue
		? string.Concat(
			" title='",
			this.StartedOn.Value.ToString(),
			"'")
		: string.Empty %>><%= this.ElapsedTimeString %></span></h4>
	<asp:Button
			runat="server"
			Text="Submit Answers"
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
</asp:Panel>