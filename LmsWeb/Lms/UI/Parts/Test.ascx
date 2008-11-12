<%@ Control
	Language="C#"
	AutoEventWireup="true"
	CodeBehind="Test.ascx.cs"
	ClassName="TestControl"
	Inherits="N2.Lms.UI.Parts.TestControl" %>

<%@ Reference Control="~/Lms/UI/TestQuestion.ascx" %>

<n2:Zone runat="server" ZoneName="Questions" OnAddedItemTemplate="zone_AddedItemTemplate">
	<HeaderTemplate><h1><%= this.CurrentItem.Title %></h1>
	<h3>Score: <%= this.Score %> / <%= this.CurrentItem.Points %></h3></HeaderTemplate>
	<SeparatorTemplate><hr /></SeparatorTemplate>
</n2:Zone>