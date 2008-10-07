<%@ Page
		Title=""
		Language="C#"
		Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Lms.Items.TrainingWorkflow.TrainingTicket, N2.Lms]], N2.Templates" %>
<%@ Register Src="~/Lms/UI/Player/Player.ascx" TagName="Content" TagPrefix="player" %>


<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="N2.Lms.Items" %>

<asp:Content runat="server" ContentPlaceHolderID="Top">
	<h3><%= this.CurrentItem.Training.Title %></h3>
</asp:Content>

<asp:Content
		ID="Content2"
		ContentPlaceHolderID="PageWrapper"
		Runat="Server">
	<player:Content Runat="Server" />
</asp:Content>