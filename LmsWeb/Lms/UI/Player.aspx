<%@ Page
		Title=""
		Language="C#"
		Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Lms.Items.TrainingWorkflow.TrainingTicket, N2.Lms]], N2.Templates" %>
<%@ Register Src="~/Lms/UI/Player/Content.ascx" TagName="Content" TagPrefix="player" %>
<%@ Register Src="~/Lms/UI/Player/Navigator.ascx" TagName="Navigator" TagPrefix="player" %>
<%@ Register Src="~/Lms/UI/Player/Controller.ascx" TagName="Controller" TagPrefix="player" %>
<%@ Import Namespace="System.Linq" %>

<asp:Content runat="server" ContentPlaceHolderID="Menu">
	<player:Navigator runat="server" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Top">
	<h3><%= this.CurrentItem.Training.Title %></h3>
	<player:Controller ID="Controller1" runat="server" />
	
</asp:Content>

<asp:Content
		ID="Content2"
		ContentPlaceHolderID="ContentAndSidebar"
		Runat="Server">
	<player:Content Runat="Server" />
</asp:Content>