<%@ Page
		Title=""
		Language="C#"
		EnableViewState="true"
		Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Lms.Items.TrainingWorkflow.TrainingTicket, N2.Lms]], N2.Templates" %>
<%@ Register Src="~/Lms/UI/Player/Player.ascx" TagName="Content" TagPrefix="player" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="N2.Lms.Items" %>

<asp:Content runat="server" ContentPlaceHolderID="Top">
	<div id="Header">
	<n2:EditableDisplay runat="server" PropertyName="Title" />
	</div>
</asp:Content>

<asp:Content
		ID="Content2"
		ContentPlaceHolderID="PageWrapper"
		Runat="Server">
	<player:Content Runat="Server" />
</asp:Content>