<%@ Page
	Inherits="N2.Templates.Web.UI.TemplatePage`1[[N2.Lms.Items.Topic, N2.Lms]], N2.Templates"
	Language="C#" %>
<%@ Register src="Topic.ascx" tagname="Topic" tagprefix="uc2" %>
<asp:Content runat="server" ContentPlaceHolderID="TextContent">
	<uc2:Topic ID="Topic1" runat="server" />
</asp:Content>