<%@ Page
		Title=""
		Language="C#"
		Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Lms.Items.TrainingList, N2.Lms]], N2.Templates" %>
<%@ Register Src="~/Lms/UI/TrainingSelector.ascx" TagName="TrainingSelector" TagPrefix="lms" %>

<asp:Content
		ID="Content2"
		ContentPlaceHolderID="ContentAndSidebar"
		Runat="Server">
	<lms:TrainingSelector ID="TrainingSelector1" runat="server" />
</asp:Content>



